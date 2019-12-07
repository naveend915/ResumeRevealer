using System.Collections.Generic;
using System.Linq;
using ResumeParser.Model;
using ResumeParser.Model.Models;
using ResumeParser.ResumeProcessor.Helpers;

namespace ResumeParser.ResumeProcessor.Parsers
{
    public class ResumeBuilder
    {
        private readonly Dictionary<SectionType, dynamic> _parserRegistry;
        public ResumeBuilder(IResourceLoader resourceLoader)
        {
            _parserRegistry = new Dictionary<SectionType, dynamic>
            {
                {SectionType.Personal, new PersonalParser(resourceLoader)},
                {SectionType.Summary, new SummaryParser(resourceLoader)},
                {SectionType.Education, new EducationParser()},
                {SectionType.Projects, new ProjectsParser()},
                {SectionType.WorkExperience, new WorkExperienceParser(resourceLoader)},
                {SectionType.Skills, new SkillsParser()},
                {SectionType.Courses, new CoursesParser()},
                {SectionType.Awards, new AwardsParser()}
            };
        }
        
        public Resume Build(IList<Section> sections)
        {
            var resume = new Resume();

            foreach (var section in sections.Where(section => _parserRegistry.ContainsKey(section.Type)))
            {
                _parserRegistry[section.Type].Parse(section, resume);
            }


            if (string.IsNullOrWhiteSpace(resume.Designation))
            {
                resume.Designation = _parserRegistry[SectionType.WorkExperience].FindJobTitle(resume.Summarydescription);
            }
            if (string.IsNullOrWhiteSpace(resume.Designation))
            {
                var personalSection = sections.Where(x => x.Type == SectionType.Personal).ToList();
                foreach (var summary in personalSection)
                {
                    foreach (var summaryContent in summary.Content)
                    {
                        if (summaryContent.ToLower().Contains("working as"))
                        {
                            resume.Designation = _parserRegistry[SectionType.WorkExperience].FindJobTitle(summaryContent);
                        }
                        if (!string.IsNullOrWhiteSpace(resume.Designation))
                        {
                            break;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(resume.Designation))
                    {
                        break;
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(resume.Designation))
            {
                var summarySections = sections.Where(x=>x.Type == SectionType.Summary).ToList();
                foreach(var summary in summarySections)
                {
                    var summaryContent = string.Join("; ", summary.Content);
                    resume.Designation = _parserRegistry[SectionType.WorkExperience].FindJobTitle(summaryContent);
                    if (!string.IsNullOrWhiteSpace(resume.Designation))
                    {
                        break;
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(resume.Designation))
            {
                var workExperienceSections = sections.Where(x => x.Type == SectionType.WorkExperience).ToList();
                foreach (var summary in workExperienceSections)
                {
                    var summaryContent = string.Join("; ", summary.Content);
                    resume.Designation = _parserRegistry[SectionType.WorkExperience].FindJobTitle(summaryContent);
                    if (!string.IsNullOrWhiteSpace(resume.Designation))
                    {
                        break;
                    }
                }
            }

            return resume;
        }
    }
}
