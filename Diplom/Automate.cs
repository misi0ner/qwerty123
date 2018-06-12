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
        public char Name;
        public Point coordinate;
        public int count_e = 0;
        public int is_end;

        public Vertex()
        { }

        public Vertex(char name, int is_end)
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
                        list_v.Add(new Vertex(sub_str[2][0], 1));
                    else
                        list_v.Add(new Vertex(sub_str[1][0], 0));
                }

                if (sub_str[0] == "ребро")
                {
                    Vertex v1 = new Vertex();
                    Vertex v2 = new Vertex();
                    foreach (var v in list_v)
                    {
                        if (v.Name == sub_str[2][0])
                            v1 = v;
                        if (v.Name == sub_str[3][0])
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
            list_v[1].set_point(new Point(centr.X - 50, centr.Y - 50));
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
                    x0 += 100;

                if (c_all / c_only < 2)
                    y0 += 100;
            }

            if (f == 1)
            {
                if (c_all / c_only >= 2)
                    x0 -= 100;

                if (c_all / c_only < 2)
                    y0 -= 100;
            }

            return new Point(p.X + x0, p.Y + y0);
        }

        private void draw_au()
        {
            foreach (var v in list_v)
                dh.DrawV(v.Name.ToString(), v.coordinate.X, v.coordinate.Y, v.is_end);

            foreach (var e in list_e)
                dh.DrawE(e.from.coordinate.X+17, e.from.coordinate.Y + 17, 
                         e.to.coordinate.X + 17, e.to.coordinate.Y + 17);
        }
    }
}
