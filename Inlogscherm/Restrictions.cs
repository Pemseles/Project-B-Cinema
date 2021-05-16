using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Restrictions
    {
        private static bool Len(int len, string s)
        {
            if(s.Length < len)
            {
                return false;
            }
            return true;
        }

        private static bool Caps(string s)
        {
            for(int i  = 0; i < s.Length; i++)
            {
                if (char.IsUpper(s[i]))
                {
                    return true; 
                }
            }
            return false;
        }

        private static bool Digit(string s)
        {
            int x = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsDigit(s[i]))
                {
                    x += 1;
                }
            }
            if(x == s.Length)
            {
                return true;
            }
            return false;
        }
        private static bool Letter(string s)
        {
            int x = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsLetter(s[i]))
                {
                    x += 1;
                }
            }
            if (x == s.Length)
            {
                return true;
            }
            return false;
        }

        private static bool Sym(string s)
        {
            for (int i = 0; i < s.Length; i++) {
                if (char.IsSymbol(s[i]))
                {
                    return true;
                }
            }
            return true;
        }

        public static bool SW(int x, string s, int len)
        {
            switch (x)
            {
                case 1:
                    return Restrictions.Len(len,s);
                case 2:
                    return Restrictions.Caps(s);
                case 3:
                    return Restrictions.Digit(s);
                case 4:
                    return Restrictions.Sym(s);
                case 5:
                    return Restrictions.Letter(s);
            }
            return false;
        }
    }
}
