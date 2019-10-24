using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix A = new Matrix();
            A.Reverse();
            A.Display();
            Console.ReadKey();
        }
    }
}
