using ResumeParser.Model;
using ResumeParser.Model.Models;

namespace ResumeParser.ResumeProcessor.Parsers
{
    public class CoursesParser : IParser
    {        
        public void Parse(Section section, Resume resume)
        {
            resume.Courses = section.Content;
        }
    }
}
