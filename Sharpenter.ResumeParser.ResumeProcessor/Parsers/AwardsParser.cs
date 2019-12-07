using ResumeParser.Model;
using ResumeParser.Model.Models;

namespace ResumeParser.ResumeProcessor.Parsers
{
    public class AwardsParser : IParser
    {        
        public void Parse(Section section, Resume resume)
        {
            resume.Awards = section.Content;
        }
    }
}
