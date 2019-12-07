using ResumeParser.Model.Models;

namespace ResumeParser.Model
{
    public interface IOutputFormatter
    {
        string Format(Resume resume);
    }
}
