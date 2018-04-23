using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LavanderiaMyM
{
    class DataChecker
    {
        public bool checkString(string a, int size = 100)
        {
            return (a != "" && a.Length <= size);
        }
        public int convertStringtoInt(string a)
        {
            if (a == null || a == "") return 0;
            return Int32.Parse(a);
        }
        public bool ConvertBoolean(string a)
        {
            return a == "1";
        }
    }
}
