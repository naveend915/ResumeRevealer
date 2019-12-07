using System.Collections.Generic;

namespace ResumeParser.Model
{
    public interface IInputReader
    {
        IInputReader NextReader { get; set; }
        IList<string> ReadIntoList(string location);        
    }
}
