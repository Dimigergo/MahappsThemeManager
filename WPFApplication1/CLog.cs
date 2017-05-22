using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApplication1
{
    public class CLog
    {
        public static void Log(DateTime timeStamp, string str)
        {
            str = timeStamp.ToString() + " - " + str;

            Console.WriteLine(str);

            using (StreamWriter sw = new StreamWriter("valami.log", true))
            {
                sw.WriteLine(str);
            }
        }
    }
}
