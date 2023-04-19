using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechAssessment
{
    public interface IConsole
    {
        string ReadLine();
        void WriteLine(string message);
        void Exit(int code);
    }

    public class WrapConsole : IConsole
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void Exit(int code)
        {
            Environment.Exit(code);
        }
    }
}
