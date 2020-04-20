using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Familias3._1
{
    public class Pagina
    {
        public String page;
        public String desS;
        public String desE;
        public int sel;
        public String area;
        public Boolean enMenu;
        public Pagina( String p, String es, String en, int s, String a, Boolean m)
        {
            page = p;
            desS = es;
            desE = en;
            sel = s;
            area = a;
            enMenu = m;
        }
    }
}