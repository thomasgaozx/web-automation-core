using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAutomation.Core
{
    public static class ZDebugUtil
    {
        public static void PrintError(Exception e)
        {
            Console.WriteLine($"{e.Message}\n\n{e.Source}\n\n{e.StackTrace}\n\n{e.InnerException}");
        }

        public static void PrintError(Exception e, int num)
        {
            Console.WriteLine($"Error #{num}\n");
            PrintError(e);
        }
    }
}
