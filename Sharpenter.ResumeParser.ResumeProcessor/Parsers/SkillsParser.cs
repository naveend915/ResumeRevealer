using ResumeParser.Model;
using ResumeParser.Model.Models;
using ResumeParser.ResumeProcessor.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ResumeParser.ResumeProcessor.Parsers
{
    public class SkillsParser : IParser
    {
        private readonly List<string> _technologylist;
        public SkillsParser() {
         
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("C:\\data\\Technology.txt"))
                {
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();
                    _technologylist = line.Split(',').ToList();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
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
                List<string> optimisedList = new List<string>();
                foreach(var skills in resume.Skills)
                {
                    foreach(var tech in _technologylist)
                    {
                        if (skills.Trim().ToLower().Contains(tech.ToLower()))
                        {
                            optimisedList.Add(skills);
                            break;
                        }
                    }
                }
                resume.Skills = optimisedList;
            }
        }
    }
}
