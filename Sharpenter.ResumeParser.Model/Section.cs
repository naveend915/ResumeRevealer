using System.Collections.Generic;
using ResumeParser.Model.Models;

namespace ResumeParser.Model
{
    public class Section
    {
        public SectionType Type { get; set; }
        public List<string> Content { get; set; }

        public Section()
        {
            Content = new List<string>();
        }
    }
}
