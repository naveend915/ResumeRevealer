using System.Collections.Generic;

namespace ResumeParser.Model
{
    public class Project
    {
        public string Title { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }        
        public List<string> Summary { get; set; }

        public Project()
        {
            Summary = new List<string>();
        }
    }
}
