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
    }
}
