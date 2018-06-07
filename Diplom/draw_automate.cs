﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class DrawHTML
    {
        int countV = 0;
        private string html_style = "<style>";
        private string html_v = "";
        private string html_e = "<svg width = 800 height = 800>";

        public string GetHTML()
        {
            return String.Format("<!doctype html><html> <head> {0} </style></head><body> {1} </svg> {2}</body></html>", 
                                                html_style, html_e, html_v);
           
        }

        public void DrawE(int x1, int y1, int x2, int y2)
        {
            html_e += String.Format("<line stroke-width=\"1\" stroke=\"black\" x1 = \"{0}\"  y1 = \"{1}\" x2 = \"{2}\" y2 = \"{3}\"/>",
                x1.ToString(), y1.ToString(), x2.ToString(), y2.ToString());

            html_e += String.Format("<circle cx=\"{0}\" cy=\"{1}\" r=\"2\" stroke-width=\"1\" stroke=\"black\"/>",
                x2.ToString(), y2.ToString());

        }

        public void DrawV(string name, int left, int top)
        {
            html_style += String.Format(".v{0}", countV.ToString()) + "{ "
                        + String.Format(" padding: 13px 0 0 20px; width: 30px; height: 30px; position: absolute;" +
                                                "border: 4px double black; border-radius: 100%;" +
                                                "top:  {0}px; left:  {1}px;", 
                                                top.ToString(), left.ToString()) + "}";
            html_v += String.Format("<div class=\"v{0}\">{1}</div>", countV.ToString(), name);

            countV += 1;
        }

    }
}