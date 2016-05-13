using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSViewer
{
    class Util
    {
        public static int HexToInt(String hex)
        {
            String sample = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int result = 0;
            for(int i =0; i<hex.Length; i++)
            {
                result *= 36;
                for(int j = 0; j<sample.Length; j++)
                {
                    if(hex[i] == sample[j])
                    {
                        result += j;
                    }
                }
            }

            return result;
        }
    }
}
