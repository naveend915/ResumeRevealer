using ResumeParser.Model;
using ResumeParser.Model.Models;
using ResumeParser.ResumeProcessor.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ResumeParser.ResumeProcessor.Parsers
{
    public class SummaryParser : IParser
    {
        private readonly List<string> _technologylist;
        private static readonly Regex SplitByWhiteSpaceRegex = new Regex(@"\s+|,", RegexOptions.Compiled);
        private readonly List<string> _jobLookUp;
        public SummaryParser(IResourceLoader resourceLoader)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (StreamReader sr = new StreamReader("C:\\data\\Technology.txt"))
            {
                // Read the stream to a string, and write the string to the console.
                String line = sr.ReadToEnd();
                _technologylist = line.Split(',').ToList();
            }
            _jobLookUp = new List<string>(resourceLoader.Load(assembly, "JobTitles.txt", ','));
        }
        public void Parse(Section section, Resume resume)
        {
            resume.Summarydescription = string.Join("; ", section.Content);
            if (string.IsNullOrWhiteSpace(resume.Designation) && resume.Summarydescription.ToLower().Contains("working as"))
            {
                resume.Designation = FindJobTitle(resume.Summarydescription);
            }
                ExtractYOE(ref resume, section.Content);
            ParseSkill(section, resume);
        }

        private string FindJobTitle(string line)
        {
            var elements = SplitByWhiteSpaceRegex.Split(line);
            //if (elements.Length > 4)
            //{
            //    return string.Empty;
            //}

            return _jobLookUp.FirstOrDefault(job => line.IndexOf(job, StringComparison.InvariantCultureIgnoreCase) > -1);
        }

        public void ParseSkill(Section section, Resume resume)
        {
            if (resume.Skills == null)
            {
                resume.Skills = new List<string>();
            }
            else if (resume.Skills.Count == 0)
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
                else if (line.Contains(","))
                {
                    var skills = line.Split(',');
                    resume.Skills.AddRange(skills.Select(e => e.Trim()));
                }
                resume.Skills = resume.Skills.Where(w => w.Any(c => !Char.IsDigit(c))).ToList();
                List<string> optimisedList = new List<string>();
                //var wordlist = SplitByWhiteSpaceRegex.Split(line);
                ////Consider the rest of the line as part of last name
                //for (var j = 0; j < wordlist.Count(); j++)
                //{
                //    var lastName = _technologylist.Where(name => name == wordlist[j].Trim()).FirstOrDefault();
                //    resume.LastName = string.IsNullOrWhiteSpace(resume.LastName) ? lastName : resume.LastName + lastName;
                //}
                foreach (var skills in resume.Skills)
                {
                    var skillFound = false;
                    foreach (var tech in _technologylist)
                    {
                        if (skills.Trim().ToLower().Equals(tech.ToLower()))
                        {
                            optimisedList.Add(tech);
                            skillFound = true;
                            break;
                        }
                    }
                    if (!skillFound)
                    {
                        foreach (var tech in _technologylist)
                        {
                            if (skills.Trim().ToLower().Contains(tech.ToLower()))
                            {
                                optimisedList.Add(tech);
                                break;
                            }
                        }
                    }
                }
                resume.Skills = optimisedList;
            }
        }

        private void ExtractYOE(ref Resume resume, List<string> line)
        {
            foreach (var summary in line)
            {
                var indexOf = summary.IndexOf("years of", StringComparison.InvariantCultureIgnoreCase);
                if (indexOf > -1)
                {
                    var YOE = summary.Substring(0, indexOf);
                    var YOEN = Regex.Match(YOE, @"\d*(\.\d*)").Value;
                    YOEN = string.IsNullOrWhiteSpace(YOEN) ? Regex.Match(YOE, @"\d+").Value : YOEN;
                    if (string.IsNullOrWhiteSpace(resume.yoe))
                    {
                        resume.yoe = YOEN;
                    }

                }
            }
        }
    }
}
