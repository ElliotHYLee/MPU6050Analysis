/*============================================================
// Made by: Elliot Hongyun Lee
// Undergrad Research Project
============================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokenizer
{
    static class Tokenizer
    {
        public static string trim(string x)
        {
            if (x.Length < 2)
            {
                return x;
            }
            int beginPos = findFirstChar(x, '[', true);
            int endPos = findFirstChar(x, ']', false);
            string result="";
            try
            {
                int length = endPos - beginPos + 1;
                if (length > 0)
                {
                    result = x.Substring(beginPos, endPos - beginPos + 1);
                    //Console.WriteLine("the string to trim:    " + x);
                    
                    if (result.ToCharArray()[0] == '['  && result.ToCharArray()[result.Length-1] == ']')
                    {
                        //Console.WriteLine("the string after trim: " + result + "    =============");
                    }
                    else
                    {
                        result = "abort";
                    }
                    //Console.Write(result.ToCharArray()[0] + " " + (result.ToCharArray()[0] != '[') + " ");
                    //Console.WriteLine(result.ToCharArray()[0] + " " + (result.ToCharArray()[result.Length - 1] != ']'));
                }
                else
                {
                    result = "abort";
                }
                
            }
            catch(Exception damn){
                Console.WriteLine("Abnormal Case - the string to trim: " + x);
                Console.WriteLine("Abnormal Case - the string to trim: " + damn.GetType());
            }
           
            return result;
        }

        private static int findFirstChar(string str, char x, bool fromFront)
        {
            if (!str.Contains(x.ToString()))
            {
                return 0;
            }
            int result = 0;
            if (fromFront)
            {
                result = str.IndexOf(x.ToString());
            }
            else
            {
                string revStr = reverse(str);
                int position = revStr.IndexOf(x.ToString()) + 1;
                result = str.Length - position;
            }
            return result;
        }

        private static string reverse(string str)
        {
            char[] array = str.ToCharArray();
            Array.Reverse(array);
            return new String(array);
        }

        public static List<string> getTokens(string str)
        {
            List<string> result = new List<string>();
            int beginPos = findFirstChar(str, '[', true);
            int endPos = findFirstChar(str, ']', true);
            string temp = null;
            try
            {
                int i = 0;
                while (str.Length > 0)
                {
                    // get first token of the string
                    temp = str.Substring(beginPos + 1, endPos - beginPos - 1);
                    if (ignoreOdd(temp))
                    {
                        result.Add(temp);
                    }
                    
                    //Console.WriteLine(ignoreOdd(temp));
                   
                    // prepare the next string
                    str = str.Substring(endPos + 1, str.Length - endPos - 1);
                    beginPos = findFirstChar(str, '[', true);
                    endPos = findFirstChar(str, ']', true);
                    i++;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return result;
        }

        private static bool ignoreOdd(String x)
        {            
            bool result=false;
            String criterion = "qwertyuiopasdfghjklzxcvbnm[]";
            List<String> temp = new List<String>();
            
            char[] chArr = new char[criterion.Length];
            for (int i = 0; i < criterion.Length; i++)
            {
                temp.Add(chArr[i].ToString());
            }

            for (int i = 0; i < x.Length; i++)
            {
                if (x.Contains(temp[i]))
                {
                    result = false;
                }
                else
                {
                    result = true;
                }

            }
            temp.Clear();
            return result;
        }

    }
}
