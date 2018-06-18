using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    class DrawHTML
    {
        int countV = 0;
        private string html_style = "<style>";
        private string html_v = "";
        private string html_e = "<svg width = 800 height = 800>";

        public string GetHTML()
        {
            return String.Format("<!DOCTYPE html><html> <head> <meta http-equiv=\"X - UA - Compatible\" content=\"IE = edge, chrome = 1\"> {0} </style></head><body> {1} </svg> {2}</body></html>", 
                                                html_style, html_e, html_v);
           
        }

        public void DrawE(int x1, int y1, int sx1, int sy1, int sx2, int sy2, int x2, int y2, string name)
        {
            html_e += String.Format("<path d =\"M {0}, {1} C {2}, {3}, {4}, {5}, {6}, {7}\"  stroke=\"black\" fill=\"transparent\" />",
                x1.ToString(), y1.ToString(), 
                sx1.ToString(), sy1.ToString(), 
                sx2.ToString(), sy2.ToString(), 
                x2.ToString(), y2.ToString());

            html_e += String.Format("<circle cx=\"{0}\" cy=\"{1}\" r=\"2\" stroke-width=\"1\" stroke=\"black\"/>",
                x2.ToString(), y2.ToString());

            DrawNameV(name, (sx1 + sx2 - 34) / 2, (sy1 + sy2 - 34) / 2); // 17+17 == 34

        }

        public void DrawE(int x1, int y1, int sx1, int sy1, int x2, int y2, string name)
        {
            html_e += String.Format("<path d =\"M {0} {1} Q {2} {3} {4} {5}\" stroke=\"black\" fill=\"transparent\" />",
                x1.ToString(), y1.ToString(), sx1.ToString(), sy1.ToString(), x2.ToString(), y2.ToString());

            html_e += String.Format("<circle cx=\"{0}\" cy=\"{1}\" r=\"2\" stroke-width=\"1\" stroke=\"black\"/>",
                x2.ToString(), y2.ToString());

            DrawNameV(name, sx1, sy1); // 17+17 == 34
        }

        public void DrawE(int x1, int y1, int x2, int y2, string name)
        {
            html_e += String.Format("<line stroke-width=\"1\" stroke=\"black\" x1 = \"{0}\"  y1 = \"{1}\" x2 = \"{2}\" y2 = \"{3}\"/>",
                x1.ToString(), y1.ToString(), x2.ToString(), y2.ToString());

            html_e += String.Format("<circle cx=\"{0}\" cy=\"{1}\" r=\"2\" stroke-width=\"1\" stroke=\"black\"/>",
                x2.ToString(), y2.ToString());

            int c1 = (x1 + x2 - 30) / 2;
            int c2 = (y1 + y2 - 60) / 2;


            DrawNameV(name, c1, c2); // 17+17 == 34
        }

        public void DrawV(string name, int left, int top, int is_end)
        {
            string border = " border: 2px ";
            if (is_end == 0)
                border += "solid black;";
            else
                border += "double black;";

            html_style += String.Format(" .v{0}", countV.ToString()) + "{ "
                        + String.Format(" padding: 13px 0 0 20px; width: 30px; height: 30px; position: absolute;" +
                                                border + " border-radius: 100%;" +
                                                "top:  {0}px; left:  {1}px;", 
                                                top.ToString(), left.ToString()) + "}";
            html_v += String.Format(" <div class=\"v{0}\">{1}</div> ", countV.ToString(), name);

            countV += 1;
        }

        public void DrawNameV(string name, int left, int top)
        {
            html_style += String.Format(" .e{0}", name[0]) + "{ "
                        + String.Format(" padding: 13px 0 0 20px; width: 30px; height: 30px; position: absolute; color: #800080; font-size: 25px;" +
                                                "top:  {0}px; left:  {1}px;",
                                                top.ToString(), left.ToString()) + "}";
            html_v += String.Format(" <div class=\"e{0}\">{0}</div> ", name[0]);

            countV += 1;
        }
    }
}
