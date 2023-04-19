using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechAssessment
{
    /// <summary>
    /// Class for input arguments and processing
    /// </summary>
    public class InputFile : IInputFile
    {
        //Used specifically for console
        public InputFile()
        {
            console = new WrapConsole();
        }

        //Let the constructor parse the arguments
        public InputFile(Dictionary<string, string> parsedArgs,IConsole newconsole, int NewLength = 4)
        {
            DictionaryFile = parsedArgs["DictionaryFile"];
            ResultFile = parsedArgs["ResultFile"];
            StartWord = parsedArgs["StartWord"].ToLower();
            EndWord = parsedArgs["EndWord"].ToLower();
            StringLength = NewLength;
            console = newconsole;
        }

        public List<string> FileContents { get; set; } = new List<string>();
        public string DictionaryFile { get; set; } = "";
        public string ResultFile { get; set; } = "";
        public string StartWord { get; set; } = "";
        public string EndWord { get; set; } = "";
        public int StringLength { get; set; }
        public Dictionary<string, List<string>> Mappings { get; set; } = new Dictionary<string, List<string>>();
        public List<string> Results { get; set; } = new List<string>();
        public IConsole console { get; set; }
    }
}
