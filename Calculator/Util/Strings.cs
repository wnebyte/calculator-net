using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Util
{
    public static class Strings
    {

        public static List<string> MySplit(this string s, params char[] targets) 
        {
            List<string> elements = new List<string>();
            if (s.IsNullOrEmpty()) 
                return elements;
            char[] arr = s.ToCharArray();
            string substring;
            int start = 0;

            for (int i = 0; i < arr.Length; i++) {
                for (int j = 0; j < targets.Length; j++) {
                    if (arr[i] == targets[j]) {
                        int len = i - start;
                        substring = s.Substring(start, len);
                        if (substring.Length != 0)
                        {
                            elements.Add(substring);
                        }
                        elements.Add(targets[j].ToString());
                        start = i + 1;
                    }
                }
            }
            substring = s.Substring(start);
            if (substring.Length != 0) {
                elements.Add(substring);
            }
            return elements;
        }

        public static bool IsNullOrEmpty(this string s) 
        {
            return (s == null) || (s == "");
        }

        public static int CountOccurences(this string s, char target)
        {
            if (s.IsNullOrEmpty()) return 0;
            char[] arr = s.ToCharArray();
            int occurences = 0;

            foreach (char c in arr)
            {
                if (c == target)
                {
                    occurences++;
                }
            }

            return occurences;
        }

        public static bool Contains(this string s, params char[] values)
        {
            foreach (char c in values)
            {
                if (s.Contains(c))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
