using System.Collections.Generic;
using System.Linq;
using ResumeParser.Model.Models;

namespace ResumeParser.ResumeProcessor
{
    public class SectionMatchingService
    {
        private readonly Dictionary<SectionType, List<string>> _keyWordRegistry = new Dictionary<SectionType, List<string>>
        {                        
            {SectionType.Education, new List<string> {"education", "study", "school","degree","institution", "academic", "qualification"}},
            {SectionType.Courses, new List<string> {"coursework", "course"}},
            {SectionType.Summary, new List<string> {"summary","profile"}},
            {SectionType.WorkExperience, new List<string> {"experience", "work", "employment"}},
            {SectionType.Projects, new List<string> {"project"}},
<<<<<<< HEAD
            {SectionType.Skills, new List<string> { "technical skills", "ability", "tool", "computer skills"}},
=======
            {SectionType.Skills, new List<string> {"skill", "ability", "tool","Technical"}},
>>>>>>> 7eb0c280770c698747c6b1fe75e037be3df2091b
            {SectionType.Awards, new List<string> {"award", "certification", "certificate"}}
        };

        public SectionType FindSectionTypeMatching(string input)
        {
            return
                (from sectionType in _keyWordRegistry
                 where sectionType.Value.Any(input.Contains)
                 select sectionType.Key).FirstOrDefault();
        }
    }
}
