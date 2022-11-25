
namespace GsvService
{
    internal static class Extensions
    {
        public static WacomVerification.ConfigurationOptions ToWacomVerification(this GsvClient.Models.ConfigurationOptions configurationOptions)
        {
            return new WacomVerification.ConfigurationOptions()
            {
                TemplateSize = configurationOptions.TemplateSize,
                EnrollmentScore = configurationOptions.EnrollmentScore,
                UpdateInterval = configurationOptions.UpdateInterval,
                SignatureStyle = (WacomVerification.SignatureStyle)configurationOptions.SignatureStyle,
                IgnoreDateTime = configurationOptions.IgnoreDateTime,
                ForceEnrollment = configurationOptions.ForceEnrollment
            };            
        }

        public static WacomVerification.ImageOptions ToWacomVerification(this GsvClient.Models.ImageOptions imageOptions)
        {
            return new WacomVerification.ImageOptions()
            {

                RemoveSpeckle = imageOptions.RemoveSpeckle,
                RemoveFold = imageOptions.RemoveFold,
                RemoveBox = imageOptions.RemoveBox,
                RemoveSigningLine = imageOptions.RemoveSigningLine,
                MinSigningLineLength = imageOptions.MinSigningLineLength,
                MaxSigningLineThickness = imageOptions.MaxSigningLineThickness,
                AdjustContrast = imageOptions.AdjustContrast,
                Contrast = imageOptions.Contrast,
                SetImageResolution = imageOptions.SetImageResolution,
                ImageResolution = imageOptions.ImageResolution
            };
        }

        public static GsvClient.Models.TemplateStatus ToGsvClientModel(this WacomVerification.TemplateStatus templateStatus)
        {
            return new GsvClient.Models.TemplateStatus()
            {
                DynamicStatus = new GsvClient.Models.EnrollmentStatus()
                {
                    EnrollmentState = (GsvClient.Models.EnrollmentState)templateStatus.DynamicStatus.EnrollmentState,
                    NumSignatures = templateStatus.DynamicStatus.NumSignatures,
                    EnrollmentSize = templateStatus.DynamicStatus.EnrollmentSize
                },
                StaticStatus = new GsvClient.Models.EnrollmentStatus()
                {
                    EnrollmentState = (GsvClient.Models.EnrollmentState)templateStatus.StaticStatus.EnrollmentState,
                    NumSignatures = templateStatus.StaticStatus.NumSignatures,
                    EnrollmentSize = templateStatus.StaticStatus.EnrollmentSize
                }
            };
        }

    }
}
