using System.Collections.Generic;

namespace ResumeParser.Model
{
    public class Resume
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public string yoe { get; set; }
        public string Emailaddress { get; set; }
        public string Designation { get; set; }
        public string Phonenumbers { get; set; }
        public string Languages { get; set; }
        public string Summarydescription { get; set; }
        public string Path { get; set; }
        public List<string> Certifications { get; set; }
        public List<string> Skills { get; set; }
        public string Location { get; set; }
        public List<Position> Positions { get; set; }
        public List<Project> Projects { get; set; }
        public List<string> SocialProfiles { get; set; }
        public List<Education> Educations { get; set; }
        public List<string> Courses { get; set; }
        public List<string> Awards { get; set; }

        public Resume()
        {
            Skills = new List<string>();
            Positions = new List<Position>();
            Projects = new List<Project>();
            SocialProfiles = new List<string>();
            Educations = new List<Education>();
            Courses = new List<string>();
            Awards = new List<string>();
        }
    }
}
