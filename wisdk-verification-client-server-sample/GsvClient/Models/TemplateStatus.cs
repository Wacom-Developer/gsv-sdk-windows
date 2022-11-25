namespace GsvClient.Models
{
    public enum EnrollmentState
    {
        Blank = 0,
        Enrolling = 1,
        Enrolled = 2,
        Updated = 3
    }

    public class EnrollmentStatus
    {
        public EnrollmentState EnrollmentState { get; set; }
        public ushort NumSignatures { get; set; }
        public ushort EnrollmentSize { get; set; }
    }

    public class TemplateStatus
    {
        public EnrollmentStatus DynamicStatus { get; set; }
        public EnrollmentStatus StaticStatus { get; set; }
    }

}
