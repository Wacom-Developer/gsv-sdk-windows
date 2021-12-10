namespace GsvClient.Models
{
    public enum ComparisonType
    {
        NoComparisonDone = 0,
        Static = 1,
        Dynamic = 2,
        Mixed = 3
    }

    public enum InconsistencyType
    {
        Consistent = 0,
        Geometry = 1,
        LocalShape = 2,
        Speed = 3,
        Acceleration = 4,
        Pressure = 5,
        Size = 6,
        SVM = 7
    }

    public class VerificationResult
    {
        public float Score { get; set; }

        public ComparisonType Engine { get; set; }

        public string Reason { get; set; }

        public InconsistencyType Inconsistency { get; set; }

        public TemplateStatus State { get; set; }

        public float Complexity { get; set; }

        //public string UpdatedTemplate { get; }

        public float MixedScore { get; set; }        
    }
}
