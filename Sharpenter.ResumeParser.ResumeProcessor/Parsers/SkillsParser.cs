using ResumeParser.Model;
using ResumeParser.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ResumeParser.ResumeProcessor.Parsers
{
    public class SkillsParser : IParser
    {        
        public void Parse(Section section, Resume resume)
        {
            if (resume.Skills == null)
            {
                resume.Skills = new List<string>();
            }
            else if(resume.Skills.Count == 0)
            {
                resume.Skills = new List<string>();
            }
            foreach (var line in section.Content)
            {
                var indexOfColon = line.IndexOf(':');
             
                if (indexOfColon > -1)
                {
                    var skills = indexOfColon > -1 ? line.Substring(indexOfColon + 1) : line;
                    resume.Skills.AddRange(skills.Split(',').Select(e => e.Trim()));
                }
                else if (line.Contains("•"))
                {
                    var skills = line.Split('•');
                    resume.Skills.AddRange(skills.Select(e => e.Trim()));
                }
                else if(line.Contains(","))
                {
                    var skills = line.Split(',');
                    resume.Skills.AddRange(skills.Select(e => e.Trim()));
                }
                resume.Skills= resume.Skills.Where(w => w.Any(c => !Char.IsDigit(c))).ToList();
            }
        }
    }
}
