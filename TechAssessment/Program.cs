using System;
using System.Collections.Generic;
using System.Linq;

namespace TechAssessment
{
    class Program
    {
        static void Main(string[] args)
        {
            SharedFunctions.input = new InputFile();
            //If no arguments passed then return message
            if (args.Length == 0)
            {
                SharedFunctions.OutputMessage("No arguments have been passed, please check and try again");
                SharedFunctions.ReadLineAndExit();
            }

            //As arguments could be in any order handle use linq to handle as a dictionary
            var parsedArgs = args.Select(sArgs => sArgs.Split(new[] { ':' }, 2)).ToDictionary(sArgs => sArgs[0], sArgs => sArgs[1]);
            if (parsedArgs.Count < 4 || parsedArgs.Count > 4)
            {
                SharedFunctions.OutputMessage("Invalid number of arguments, please check and try again");
                SharedFunctions.ReadLineAndExit();
            }
            
            if(!SharedFunctions.Checkargumentsmatch(parsedArgs))
            {
                SharedFunctions.OutputMessage("The specified arguments do not match, please check and try again");
                SharedFunctions.ReadLineAndExit();
            }

            SharedFunctions.input = new InputFile(parsedArgs,new WrapConsole());

            SharedFunctions.Process();
        }
    }
}