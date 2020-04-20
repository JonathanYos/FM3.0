using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Familias3._1
{
    public class Funcion
    {
        public String code;
        public String page;
        public int sel;
        public String area;
        public Funcion(String p, int s, String a, String c)
        {
            code = c;
            page = p;
            sel = s;
            area = a;
        }
    }
}
