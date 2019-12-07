using ResumeParser.Model;
using ResumeParser.Model.Models;

namespace ResumeParser.ResumeProcessor.Parsers
{
    public interface IParser
    {
        void Parse(Section section, Resume resume);
    }
}
