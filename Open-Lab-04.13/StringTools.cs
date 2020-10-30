using System;
using System.Collections.Generic;

namespace Open_Lab_04._13
{
    public class StringTools
    {
        public string GetLongestCommonSequence(string str1, string str2)
        {
            int[,] a = new int[str1.Length + 1, str2.Length + 1];
            int i1 = 0;
            int i2 = 0;

            for (int i = 0; i < str1.Length; i++)
                for (int k = 0; k < str2.Length; k++)
                    if (str1[i] == str2[k])
                    {
                        int gec = a[i + 1, k + 1] = a[i, k] + 1;
                        if (gec > a[i1, i2])
                        {
                            i1 = i + 1;
                            i2 = k + 1;
                        }
                    }

            return str1.Substring(i1 - a[i1, i2], a[i1, i2]);
        }
    }
}
