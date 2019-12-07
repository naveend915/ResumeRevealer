using System.Configuration;
using ResumeParser.Model;

namespace ResumeParser.ResumeProcessor
{
    internal class ConfigFileApplicationSettingsAdapter : IApplicationSettings
    {
        public string InputReaderLocation
        {
            get { return ConfigurationManager.AppSettings["InputReaderLocation"]; }
        }
    }
}
