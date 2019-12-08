using System;

namespace ResumeParser.Model
{
    public class Candidate
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Gender { get; set; }
        public string Designation { get; set; }
        public string YearsOfExperience { get; set; }
        public string Skills { get; set; }
        public string Certifications { get; set; }
        public string Path { get; set; }
        public string Status { get; set; }
        public DateTime	ScheduleDateTime { get; set; }
        public string L1Comments { get; set; }
        public int L1UserId { get; set; }
        public string L2Comments { get; set; }
        public int L2UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime	LastUpdatedDate { get; set; }
        public int LastUpdatedBy { get; set; }
        public bool Isfavorite { get; set; }
        public double Rating { get; set; }

    }
}
