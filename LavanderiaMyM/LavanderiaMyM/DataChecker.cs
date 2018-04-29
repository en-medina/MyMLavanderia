using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LavanderiaMyM
{
    class DataChecker
    {
        public bool CheckString(string a, int size = 100)
        {
            return (a != "" && a.Length <= size);
        }
        public int ConvertStringtoInt(string a)
        {
            if (a == null || a == "") return 0;
            return Int32.Parse(a);
        }
        public bool ConvertBoolean(string a)
        {
            return a == "1";
        }
        public bool CheckPhone(string a)
        {
            bool ans = CheckString(a);
            foreach (char l in a)
                if(l != '-')
                    ans &= Char.IsNumber(l);
            return ans;        
        }
        public bool CheckNumeric(string a)
        {
            bool ans = CheckString(a);
            foreach (char l in a)
                ans &= Char.IsNumber(l);
            return ans;
        }
        public bool CheckEmail(string a)
        {
            char[] must = { ' ', '@', ' ', '.' };
            int i = 0;
            bool ans = false;
            foreach (char l in a)
            {
                if (i == must.Length)
                {
                    ans = true;
                    break;
                }
                if (must[i] == ' ' && (l == '@' || Char.IsDigit(l) || l == '.'))
                    break;
                else {
                    i++;
                    continue;
                    }
                if (l == must[i])
                    i++;
            }
            return ans;
        }
    }
}
