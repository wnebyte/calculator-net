using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Calculator.Util;

namespace Calculator.Struct
{
    /**
     * This class represents an enumeration of tokens.
     */
    public class TokenSequence : IEnumerable<string>
    {
        /*
        ###########################
        #      STATIC FIELDS      #
        ###########################
        */

        public static readonly char[] targets = new char[] 
        {
            '+', 
            '-',
            '*',
            '/',
            '(',
            ')',
            ' '
        };

        /*
        ###########################
        #      STATIC METHODS     #
        ###########################
        */

        public static TokenSequence Tokenize(string input, char[] targets)
        {
            List<string> tokens = input.MySplit(targets);
            return new TokenSequence(tokens);
        }

        public static TokenSequence Tokenize(string input)
        {
            return Tokenize(input, targets);
        }

        public static TokenSequence Tokenize(char[] input)
        {
            return Tokenize(String.Join("", input), targets);
        }

        public static TokenSequence Tokenize(char[] input, char[] targets)
        {
            return Tokenize(String.Join("", input), targets);
        }

        /*
        ###########################
        #          FIELDS         #
        ###########################
        */

        private readonly List<string> tokens;

        /*
        ###########################
        #        CONSTRUCTORS     #
        ###########################
        */

        private TokenSequence(IEnumerable<string> tokens)
        {
            this.tokens = new List<string>(tokens);
        }

        /*
        ###########################
        #          METHODS        #
        ###########################
        */

        public TokenSequence GetRange(int fromIndex, int toIndex)
        {
            return new TokenSequence(tokens.GetRange(fromIndex, toIndex));
        }

        public TokenSequence TakeWhileIndex(Predicate<int> predicate)
        {
            List<string> l = new();

            for (int i = 0; i < Count(); i++)
            {
                if (predicate(i))
                {
                    l.Add(tokens[i]);
                }
            }

            return new TokenSequence(l);
        }

        public TokenSequence TakeWhile(Predicate<string> predicate)
        {
            List<string> l = new();

            for (int i = 0; i < Count(); i++)
            {
                string s = tokens[i];

                if (predicate(s))
                {
                    l.Add(s);
                }
            }

            return new TokenSequence(l);
        }

        public int Count()
        {
            return tokens.Count;
        }

        public bool IsEmpty()
        {
            return (Count() == 0);
        }

        public bool IsNotEmpty()
        {
            return !IsEmpty();
        }

        public string ElementAt(int index)
        {
            return tokens.ElementAt(index);
        }

        public string TakeFirst()
        {
            string token = tokens[0];
            tokens.RemoveAt(0);
            return token;
        }

        public void SetElementAt(int index, string token)
        {
            tokens[index] = token;
        }

        public bool Contains(string token)
        {
            return tokens.Contains(token);
        }

        public bool HasElementContainingAny(params char[] chars)
        {
            foreach (string token in this)
            {
                foreach (char c in chars)
                {
                    if (token.Contains(c))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public int RemoveIf(Predicate<string> predicate)
        {
            int count = tokens.RemoveAll(predicate);
            return count;
        }

        public IEnumerator<string> GetEnumerator()
        {
            return tokens.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return tokens.GetEnumerator();
        }
    }
}
