using ResumeParser.Model;
using ResumeParser.Model.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ResumeParser.ResumeProcessor.Parsers
{
    public class SummaryParser : IParser
    {
        public void Parse(Section section, Resume resume)
        {
            resume.SummaryDescription = string.Join("; ", section.Content);
            ExtractYOE(ref resume, section.Content);
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
                    if (string.IsNullOrWhiteSpace(resume.YearsOfExperience))
                    {
                        resume.YearsOfExperience = YOEN;
                    }

                }
            }
        }
    }
}
