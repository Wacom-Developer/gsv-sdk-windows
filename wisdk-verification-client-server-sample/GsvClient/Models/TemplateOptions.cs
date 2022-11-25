
namespace GsvClient.Models
{
    public class ImageOptions
    {
        public bool RemoveSpeckle { get; set; }
        public bool RemoveFold { get; set; }
        public bool RemoveBox { get; set; }
        public bool RemoveSigningLine { get; set; }
        public float MinSigningLineLength { get; set; }
        public float MaxSigningLineThickness { get; set; }
        public bool AdjustContrast { get; set; }
        public short Contrast { get; set; }
        public bool SetImageResolution { get; set; }
        public ushort ImageResolution { get; set; }
    }


    public enum SignatureStyle
    {
        Cursive = 0,
        Kanji = 1,
        AutoDetect = 2
    }
    public class ConfigurationOptions
    {
        public ushort TemplateSize { get; set; }
        public float EnrollmentScore { get; set; }
        public ushort UpdateInterval { get; set; }
        public SignatureStyle SignatureStyle { get; set; }
        public bool IgnoreDateTime { get; set; }        
        public bool ForceEnrollment { get; set; }
    }

    /// <summary>
    /// Singleton object for template and application options and settings
    /// </summary>
    public class TemplateOptions
    {
        /// <summary>
        /// Template configuration options 
        /// </summary>
        /// <remarks>Options which apply to all signature types</remarks>
        public ConfigurationOptions ConfigurationOptions { get; set; }

        /// <summary>
        /// Image processing options
        /// </summary>
        /// <remarks>Options which are used to control the way in which static images are processed</remarks>
        public ImageOptions ImageOptions { get; set; }

        /// <summary>
        /// Constructor. Initializes values with defaults.
        /// </summary>
        public TemplateOptions()
        {          
            ConfigurationOptions = new  ConfigurationOptions();
            ImageOptions = new ImageOptions();
        }

    }
}
