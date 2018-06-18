using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Diplom
{
    class Vertex
    {
        public string Name;
        public Point coordinate;
        public int count_e = 0;
        public int is_end;

        public Vertex()
        { }

        public Vertex(string name, int is_end)
        {
            Name = name;
            this.is_end = is_end;
        }

        public void set_point(Point p) {  coordinate = p; }
    }

    class Edge
    {
        public string Name;
        public Vertex from, to;

        public Edge()
        { }

        public Edge(string name, Vertex f, Vertex t)
        {
            Name = name;
            from = f;
            to = t;
        }
    }

    class Automate
    {
        List<Vertex> list_v = new List<Vertex>();
        List<Edge>   list_e = new List<Edge>();
        DrawHTML dh = new DrawHTML();

        public Automate(string[] str)
        {
            for (int i = 1; i < str.Length; i++)
            {
                string[] sub_str = str[i].Split(' ');

                if (sub_str[0] == "вершина")
                {
                    if (sub_str[1] == "финальная")
                        list_v.Add(new Vertex(sub_str[2], 1));
                    else
                        list_v.Add(new Vertex(sub_str[1], 0));
                }

                if (sub_str[0] == "ребро")
                {
                    Vertex v1 = new Vertex();
                    Vertex v2 = new Vertex();
                    foreach (var v in list_v)
                    {
                        if (v.Name == sub_str[2])
                            v1 = v;
                        if (v.Name == sub_str[3])
                            v2 = v;
                    }
                    //Обработчик исключения
                    v1.count_e += 1;
                    v2.count_e += 1;
                    list_e.Add(new Edge(sub_str[1], v1, v2));
                }
            }

            create_coord();
            draw_au();

            System.IO.File.WriteAllText("html_test.html", dh.GetHTML());
        }

        Point centr = new Point(400, 200);      

        private void create_coord()
        {
            list_v.Sort((v1, v2) => v2.count_e.CompareTo(v1.count_e));

            list_v[0].set_point(centr);
            list_v[1].set_point(new Point(centr.X - 100, centr.Y - 100));
            int f = 0;
            int c_only = 1;
            int c_all = 2;
            for (int i = 2; i < list_v.Count; i++)
            {
                list_v[i].set_point(new_p(list_v[i - 1].coordinate, f, c_only, c_all));

                if (c_only == c_all)
                {
                    c_only = 1;
                    c_all *= 2;
                    f = (f + 1) % 2;
                }
                else
                    c_only += 1;
            }
        }

        Point new_p(Point p, int f, int c_only, int c_all) //f==0 -> |_ , f==1 <- |^
        {
            int x0 = 0;
            int y0 = 0;



            if (f == 0)
            {
                if (c_all / c_only >= 2)
                    x0 += 200;

                if (c_all / c_only < 2)
                    y0 += 200;
            }

            if (f == 1)
            {
                if (c_all / c_only >= 2)
                    x0 -= 200;

                if (c_all / c_only < 2)
                    y0 -= 200;
            }

            return new Point(p.X + x0, p.Y + y0);
        }

        private void draw_au()
        {
            foreach (var v in list_v)
                dh.DrawV(v.Name.ToString(), v.coordinate.X, v.coordinate.Y, v.is_end);

            foreach (var e in list_e)
            {
                if (e.from == e.to)
                {
                    dh.DrawE(e.from.coordinate.X + 17, e.from.coordinate.Y + 36,
                    e.from.coordinate.X + 17 - 50, e.from.coordinate.Y + 17 + 50,
                    e.to.coordinate.X + 17 + 50, e.to.coordinate.Y + 17 + 50,
                    e.to.coordinate.X + 17, e.to.coordinate.Y + 36, e.Name);
                }
                else
                {
                    int x1 = e.from.coordinate.X;
                    int y1 = e.from.coordinate.Y;
                    int x2 = e.to.coordinate.X;
                    int y2 = e.to.coordinate.Y;

                    bool p_intersection = false;
                    int xc = 0;
                    int yc = 0;
                    foreach (var v in list_v)
                    {
                        if (v != e.from && v != e.to)
                        {
                            double H = getH(x1, y1, x2, y2, v.coordinate.X, v.coordinate.Y);
                            if (H < 40 && H != -1)
                            {
                                p_intersection = true;

                                int cx1 = Math.Sign(v.coordinate.X - x1);
                                int cx2 = Math.Sign(v.coordinate.X - x2);
                                xc = cx1 * cx2 * 50;

                                int cy1 = Math.Sign(v.coordinate.Y - y1);
                                int cy2 = Math.Sign(v.coordinate.Y - y2);
                                yc = cy1 * cy2 * 50;

                                break;
                            }
                        }
                    }



                    Point p1 = PointOfBorder(e.from.coordinate.X + 17, e.from.coordinate.Y + 17, e.to.coordinate.X + 17,
                        e.to.coordinate.Y + 17);
                    Point p2 = PointOfBorder(e.to.coordinate.X + 17, e.to.coordinate.Y + 17, e.from.coordinate.X + 17,
                            e.from.coordinate.Y + 17);

                    if (p_intersection)
                    {
                        int xw = (p1.X + p2.X) / 2 + xc;
                        int yw = (p1.Y + p2.Y) / 2 + yc;
                        dh.DrawE(p1.X, p1.Y, xw, yw,
                                 p2.X, p2.Y, e.Name);
                    }
                    else
                        dh.DrawE(p1.X, p1.Y,
                                 p2.X + 17, p2.Y, e.Name);

                }
            }
        }

        private Point PointOfBorder(int x1, int y1, int x2, int y2)
        {
            double A1 = y1 - y2;
            double B1 = x2 - x1;
            double C1 = x1 * y2 - x2 * y1;
            A1 = -A1 / B1;
            B1 = -C1 / B1;
            double a = A1 * A1 + 1;
            double b = 2 * A1 * B1 - 2 * x1 - 2 * A1 * y1;
            double c = Math.Pow(x1, 2) +
                       Math.Pow(y1, 2) +
                       Math.Pow(B1, 2) - 2 * B1 * y1 - 600;
            double sD1 = Math.Sqrt(b * b - 4 * a * c);
            int x11, x21, iA1, iB1;
            int.TryParse(Math.Floor((-b - sD1) / (2 * a)).ToString(), out x11);
            //x11 = System.Convert.ToInt32((-b - sD1) / (2 * a));
            int.TryParse(Math.Floor((-b + sD1) / (2 * a)).ToString(), out x21);
            //x21 = System.Convert.ToInt32((-b + sD1) / (2 * a));
            int.TryParse(A1.ToString(), out iA1);
            //iA1 = System.Convert.ToInt32(A1);
            int.TryParse(B1.ToString(), out iB1);
            //iB1 = System.Convert.ToInt32(B1);
            int y11 = iA1 * x11 + iB1;
            int y21 = iA1 * x21 + iB1;
            double d1 = Math.Sqrt((x11 - x2) * (x11 - x2) + (y11 - y2) * (y11 - y2));
            double d2 = Math.Sqrt((x21 - x2) * (x21 - x2) + (y21 - y2) * (y21 - y2));
            if (d1 < d2)
                return new Point(x11, y11);
            else
                return new Point(x21, y21);
        }

        private double getH(int x1, int y1, int x2, int y2, int vx, int vy)
        {
            double a = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            double b = Math.Sqrt(Math.Pow(vx - x2, 2) + Math.Pow(vy - y2, 2));
            double c = Math.Sqrt(Math.Pow(x1 - vx, 2) + Math.Pow(y1 - vy, 2));

            if (a < c || a < b)
                return -1;

            double p = (a+b+c) / 2;
            double S = Math.Sqrt(p*(p-a)*(p-b)*(p-c));

            double h = S * 2 / a;

            return h;
        }
    }
}