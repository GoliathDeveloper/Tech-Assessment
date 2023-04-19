using System.Collections.Generic;

namespace TechAssessment
{
    /// <summary>
    /// Interface incase of refactoring
    /// </summary>
    public interface IInputFile
    {
        string DictionaryFile { get; set; }
        string EndWord { get; set; }
        List<string> FileContents { get; set; }
        string ResultFile { get; set; }
        string StartWord { get; set; }
        int StringLength { get; set; }
        Dictionary<string, List<string>> Mappings { get; set; }
        List<string> Results { get; set; }
        IConsole console { get; set; }
    }
}