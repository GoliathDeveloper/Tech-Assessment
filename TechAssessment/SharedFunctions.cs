using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechAssessment
{
    public class SharedFunctions
    {
        public static IInputFile input;
        /// <summary>
        /// Reads the file directly to the class and sorts alphabetically
        /// will always return false unless the file is processed without exception
        /// </summary>
        /// <param name="input"></param>
        /// <returns>boolean</returns>
        public static bool ReadFile()
        {
            bool FileProcessed = false;
            List<string> ListOutput = new List<string>();
            try
            {
                input.FileContents = File.ReadLines(input.DictionaryFile.Replace("\\\\", "\\")).ToList();
                //Use default sort alphabetically A-> Z
                input.FileContents.Sort();
                FileProcessed = true;
            }
            catch (Exception ex)
            {
                input.FileContents = new List<string>();
                Console.WriteLine("Error Processing: " + ex.ToString());
            }
            return FileProcessed;
        }
        
        /// <summary>
        /// Used to write the results sorted in the class out to the specified file
        /// </summary>
        /// <param name="input"></param>
        /// <returns>bool</returns>
        public static bool WriteFile()
        {
            try
            {
                File.AppendAllLines(input.ResultFile, input.Results);
                SharedFunctions.OutputMessage("Results wrote to file");
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Compare two strings character by character in sequence
        /// </summary>
        /// <param name="StartWord"></param>
        /// <param name="EndWord"></param>
        /// <returns>int count of the differences</returns>
        public static int CompareTwoStrings(string StartWord, string EndWord)
        {
            return StartWord.ToCharArray().Zip(EndWord.ToCharArray(), (FirstCharacter, SecondCharacter) => new { FirstCharacter, SecondCharacter }).Count(m => m.FirstCharacter != m.SecondCharacter);
        }

        /// <summary>
        /// Reusable method to output messages
        /// can easily be changed so in the future if a logger is requied
        /// so minimal refactoring is required
        /// </summary>
        /// <param name="Message"></param>
        public static void OutputMessage(string Message)
        {
            input.console.WriteLine("Output: " + Message);
        }

        /// <summary>
        /// Creates a string dictionary of all the words remaining
        /// stores a list against the key where they have 1 character difference
        /// so we can minimise down the amount of steps to get from one word to another
        /// </summary>
        /// <param name="input"></param>
        /// <param name="DesiredLength"></param>
        /// <returns></returns>
        public static void GenerateDictonaryOfFileContent()
        {
            //Finds the index of the start & end word and the desired string length in the input list to minimise down the search criteria
            var StartIndex = input.FileContents.FindIndex(r => r.Equals(input.StartWord.ToLower()));
            var EndIndex = input.FileContents.FindIndex(r => r.Equals(input.EndWord.ToLower()));
            if (StartIndex != -1 && EndIndex != -1)
            {
                List<string> SearchList = input.FileContents.Skip(StartIndex).Take(EndIndex + 1 - StartIndex + 1).Where(x => x.Length == input.StringLength).ToList();

                //foreach entry in the search go and find the matches where they only have 1 character difference
                input.Mappings = SearchList.ToLookup(w => w).ToDictionary(
                    w => w.Key,
                    w => SearchList.Where(a => SharedFunctions.CompareTwoStrings(a, w.Key) == 1).ToList());
                //Sort all the matched entries alphabetically for ease of processing
                foreach (var dict in input.Mappings)
                {
                    dict.Value.Sort();
                }
            }
        }

        /// <summary>
        /// Simple method to check if the arguments passed are correct
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns>bool</returns>
        public static bool Checkargumentsmatch(Dictionary<string,string> arguments)
        {
            List<string> ListOfRequiredArguments = new List<string> { "DictionaryFile", "ResultFile", "StartWord", "EndWord" };
            return ListOfRequiredArguments.All(a => arguments.ContainsKey(a));
        }

        /// <summary>
        /// Contains a foreach loop slowly narrowing down the search finding the shortest link between the start word and the end
        /// </summary>
        /// <param name="input"></param>
        public static void FindResults()
        {
            //Store current entry to ensure we know which key to process next
            string CurrentEntry = "";
            input.Results = new List<string>();
            //As we already know the start add this to the list first
            input.Results.Add(input.StartWord);
            foreach (var map in input.Mappings)
            {
                //As we know the first entry is the start word then specify the CurrentEntry as the next logic link
                if (map.Key == input.Mappings.First().Key)
                {
                    CurrentEntry = map.Value[0];
                    if (input.Results.Count == 1 && map.Value.Count == 1)
                    {
                        //if the current map only has one result add the entry to the list of results otherwise it gets missed off the results list 
                        if (!input.Results.Contains(CurrentEntry))
                            input.Results.Add(CurrentEntry);
                    }
                }
                //Compares the list in the current entry and find the entries where they have the lowest number of differences
                List<string> SearchCriteria = input.Mappings[CurrentEntry]
                    .Where(search => SharedFunctions.CompareTwoStrings(search, input.EndWord) == input.Mappings[CurrentEntry]
                    .Min(search2 => SharedFunctions.CompareTwoStrings(input.EndWord, search2))).ToList();

                if (!CurrentEntry.Equals(input.EndWord))
                {
                    foreach (var value in SearchCriteria)
                    {
                        //If the results do not already contain the string and also it is only 1 character different between the current
                        if (!input.Results.Contains(value) && SharedFunctions.CompareTwoStrings(CurrentEntry, value) == 1)
                        {
                            //add this to the result and then skip the rest as we want the shortest number of results between the start and end
                            input.Results.Add(value);
                            break;
                        }
                        //If we have reached the end word or there is no maping then stop processing
                        if (CurrentEntry.Equals(input.EndWord) || CurrentEntry == input.Results.Last())
                        {
                            SharedFunctions.OutputMessage("End of search, no mapping found");
                            input.Results = new List<string>();
                            return;
                        }
                    }
                    //Set the next entry to the last entry that was found so we can find the next appropiate match
                    CurrentEntry = input.Results[input.Results.Count - 1];
                }
                else
                {
                    //Finishing processing mapping
                    break;
                }
            }
        }

        public static void ReadLineAndExit()
        {
            input.console.ReadLine();
            input.console.Exit(0);
        }

        public static void Process()
        {
            //If comparing two string of the same length, check you must
            if (SharedFunctions.CheckStrings())
            {
                SharedFunctions.OutputMessage(String.Format("The start or end words do not have the same length of {0}, please check and try again", SharedFunctions.input.StringLength));
                SharedFunctions.ReadLineAndExit();
            }
            else
            {
                if (SharedFunctions.ReadFile())
                {
                    SharedFunctions.GenerateDictonaryOfFileContent();
                    if (input.Mappings.Count == 0)
                    {
                        SharedFunctions.OutputMessage("No mappings found, are the specified words in the dictionary?");
                        SharedFunctions.ReadLineAndExit();
                    }
                    //As we already know the specified start word, add this to the list first
                    if (input.Mappings.Count == 1)
                    {
                        SharedFunctions.OutputMessage("Only 1 result found is this the same word?");
                    }
                    else
                    {
                        SharedFunctions.OutputMessage("Processing results");
                        SharedFunctions.FindResults();
                    }
                    //Write Results to output file
                    SharedFunctions.OutputMessage(String.Format("Results Found: {0}", input.Results.Count()));
                    SharedFunctions.WriteFile();
                    SharedFunctions.OutputMessage("Please enter press any key to exit");
                    input.console.ReadLine();
                }
                else
                {
                    SharedFunctions.OutputMessage("Error dictionary file, please check and try again.");
                }
            }
        }

        public static bool CheckStrings()
        {
            return input.StartWord.Length != input.StringLength | input.EndWord.Length != input.StringLength | input.StartWord.Length != input.EndWord.Length;
        }
    }
}
