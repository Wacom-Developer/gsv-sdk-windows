import java.util.ArrayList; 
import java.io.*;
import java.nio.file.*;
import com.WacomGSS.GSV.*;

class GsvJavaTest 
{
  private static String       licenseStr = "<<<license>>>";

  private SignatureEngine     mSigEngine;
  private String              mTemplateFile;
  private String              mTemplateStr;
  private ArrayList<String>   mSigFiles;
  private boolean             mKanji;
  private boolean             mPrintSummary;
  private short               mNumSigs;
  private short               mUpdateTime;
  private boolean             mNoUpdate;
  private short               mContrast;
  private boolean             mRemoveSpeckle;
  private boolean             mRemoveBox;
  private boolean             mRemoveLine;
  private float               mMaxLineThickness;
  private float               mMinLineLength;
  private boolean             mRemoveFold;
  private short               mImageDPI;
  
  private void procCmdLine(String args[]) 
  {
    for (int i = 0; i < args.length; ++i) 
    {
      String s = args[i];
      if (s.startsWith("-")) 
      {
        switch (s.substring(1, 2))
        {
          case "D": // Pause to allow attaching debugger
            try 
            {
              System.out.println("Attach debugger, press Enter");
              System.in.read();
            }
            catch (Exception e)
            {
            }
            break;
            
          case "d": // print summary of template contents after processing. Use -d+ for full details
            mPrintSummary = true;
            break;
            
          case "k": // kanji
            mKanji = true;
            break;
            
          case "n": // number of signatures required to enrol a new template
            mNumSigs = Short.parseShort( (s.length() == 2) ? args[++i] : s.substring(2) );
            break;

          case "t": // time (in DAYS) before a reference signature can be replaced by a newer one
            mUpdateTime = Short.parseShort( (s.length() == 2) ? args[++i] : s.substring(2) );
            break;
            
          case "x": // ignore signature date/time and prevent template updating
            mNoUpdate = true;
            break;
            
          case "c": // adjust image contrast. -100 < contrast < 100, e.g. -c50
            mContrast = Short.parseShort( (s.length() == 2) ? args[++i] : s.substring(2) );
            break;
            
          case "i": // clean image (removes speckle)
            mRemoveSpeckle = true;
            break;
            
          case "j": // remove signature box lines
            mRemoveBox = true;
            break;
            
          case "l": // remove horizontal signature lines
                    // use -la,b where a is the maximum line thickness and b is the minimum line length
            {
              String arg = (s.length() == 2) ? args[++i] : s.substring(2);
              String[] values = arg.split(",");
              mRemoveLine = true;
              mMaxLineThickness = Float.parseFloat(values[0]);
              mMinLineLength = Float.parseFloat(values[1]); 
            }
            break;
                    
          case "m": // removed fold marks
            mRemoveFold = true;
            break;
            
          case "r": // Image DPI
            mImageDPI = Short.parseShort( (s.length() == 2) ? args[++i] : s.substring(2) );           
            break;
            
          default:
            throw new RuntimeException("Unknown command line switch " + s);
        }
      }
      else if (mTemplateFile == null)
      {
        mTemplateFile  = s;
      }
      else 
      {
        mSigFiles.add(s);
      }
    }  
  }
  
  private static void printHelp()
  {
      System.out.println("Usage:");
      System.out.println("GsvJavaTest TemplateFile SigFile1 <SigFile2 ...> <-d> <-k> <-nNN> -t<NN> <-x> -c<NN> <-i> <-j> <-la,b> <-m> <-rNN>");
      System.out.println("Updates an existing template or creates if file not found\n");
      System.out.println("SigFile: dynamic signature in FSS or TXT format, or scanned image in TIF, PNG, BMP or JPG formats\n");
      System.out.println("General options");
      System.out.println("---------------");
      System.out.println("-d : dump summary of template contents after processing");
      System.out.println("-k : use classifier optimised for Kanji signatures");
      System.out.println("-n : set the number of signatures required to enrol a new template");
      System.out.println("-t : specify the elapsed time (in days) before a reference signature can be replaced by a newer one");
      System.out.println("-x : ignore signature date/time and prevent template updating");
      System.out.println("");
      System.out.println("Static image options (not applicable to dynamic data)");
      System.out.println("-----------------------------------------------------");
      System.out.println("-c : adjust image contrast. -100 < contrast < 100, e.g. -c50");
      System.out.println("-i : clean image (removes speckle)");
      System.out.println("-j : remove signature box lines");
      System.out.println("-l : remove horizontal signature lines");
      System.out.println("   : use -la,b where a is the maximum line thickness and b is the minimum line length");
      System.out.println("-m : removed fold marks");
      System.out.println("-r : specify image DPI. Only needed if the image metadata is incorrect");
  }
  
  private String readTemplate() throws IOException, RuntimeException
  {
    if (mTemplateFile == null)
    {
      throw new RuntimeException("Template file not specified");
    }
    Path path = Paths.get(mTemplateFile);
    
    if (Files.exists(path))
    {
      return new String(Files.readAllBytes(path));
    }
    else 
    {
      return null;
    }
  }
  
  public GsvJavaTest(String args[]) throws Exception
  {
    mSigEngine  = new SignatureEngine();
    mSigFiles   = new ArrayList<String>();

    mSigEngine.license(licenseStr);
    if (!mSigEngine.isLicensed()) {
      throw new RuntimeException("GSV not licensed");
    }

    // Preset defaults
    mKanji          = false;
    mPrintSummary   = false;
    mNumSigs        = -1; // Use default
    mUpdateTime     = -1; // Use default     
    mNoUpdate       = false;
    mContrast       = 0;
    mRemoveSpeckle  = false;
    mRemoveBox      = false;
    mRemoveLine     = false;
    mRemoveFold     = false;
    mImageDPI       = -1; // Use value from image metadata
    
    procCmdLine(args);

    mTemplateStr = readTemplate();
    if (mTemplateStr == null)
    {
      ConfigurationOptions genOptions = new ConfigurationOptions();
      ImageOptions cleanOptions = initImageOptions();
      
      mTemplateStr = mSigEngine.createTemplate(genOptions, cleanOptions);
    }
    
  }

  private ConfigurationOptions initConfigOptions()
  {
    ConfigurationOptions genOptions = new ConfigurationOptions();
     
    if (mNumSigs > 0)
    {
      genOptions.templateSize(mNumSigs);
    }
    if (mUpdateTime > 0)
    {
      genOptions.updateInterval(mUpdateTime);
    }
    if (mKanji)
    {
      genOptions.signatureStyle(ConfigurationOptions.SignatureStyle.Kanji);
    }
    if (mNoUpdate)
    {
      genOptions.ignoreDateTime(true);
    }
    return genOptions;
  }
  
  private ImageOptions initImageOptions()
  {
    ImageOptions cleanOptions = new ImageOptions();
    if (mContrast != 0)
    {
      cleanOptions.adjustContrast(true);
      cleanOptions.contrast(mContrast);
    }
    if (mRemoveSpeckle)
    {
      cleanOptions.removeSpeckle(true);
    }
    if (mRemoveBox)
    {
      cleanOptions.removeBox(true);
    }
    if (mRemoveLine)
    {
      cleanOptions.maxSigningLineThickness(mMaxLineThickness);
      cleanOptions.minSigningLineLength(mMinLineLength);
    }
    if (mRemoveFold)
    {
      cleanOptions.removeFold(true);
    }
    if (mImageDPI > 0)
    {
      cleanOptions.setImageResolution(true);
      cleanOptions.imageResolution(mImageDPI);
    }
    return cleanOptions;
  }
  
  private void run() throws Exception
  {
    for (String sigFile : mSigFiles) 
    {
      verifySignature(sigFile);
    }
    
    // If we verified signatures, template wil have been updated so save it back to disk
    if (mSigFiles.size() > 0)
    {
      Files.write(Paths.get(mTemplateFile), mTemplateStr.getBytes());
    }
    if (mPrintSummary)
    {
      printTemplateStatus();
    }
  }
  
  private void verifySignature(String sigFile)
  {
      VerificationResult result = mSigEngine.verifySignature(mTemplateStr, sigFile);
      printVerificationResult(result);
      mTemplateStr = result.updatedTemplate();
  }
  
  private void printTemplateStatus()
  {
    TemplateStatus tmpltStatus = mSigEngine.getTemplateStatus(mTemplateStr);
    
    System.out.println("Template Status:");

    EnrollmentStatus dynStatus = tmpltStatus.dynamicStatus();
    System.out.println("Dynamic:");
    System.out.println("  State:      " + dynStatus.enrollmentState());
    System.out.println("  Signatures: " + dynStatus.numSignatures());
    System.out.println("  Size:       " + dynStatus.enrollmentSize());
    
    EnrollmentStatus statStatus = tmpltStatus.staticStatus();
    System.out.println("Static:");
    System.out.println("  State:      " + statStatus.enrollmentState());
    System.out.println("  Signatures: " + statStatus.numSignatures());
    System.out.println("  Size:       " + statStatus.enrollmentSize());
  }
 
  private static void printVerificationResult(VerificationResult result)
  {
    StringBuilder sb = new StringBuilder("Result {");
    
    sb.append("Score = ");
    sb.append(result.score());
    sb.append(" (");
    sb.append(result.inconsistency());
    sb.append(")");
    sb.append(", Type = ");
    sb.append(result.engine());

    TemplateStatus tmpltStatus = result.state();
    EnrollmentStatus fssStatus = tmpltStatus.dynamicStatus();
    EnrollmentStatus imgStatus = tmpltStatus.staticStatus();
    
    appendEnrollmentStatus(sb, "FSS", fssStatus);
    appendEnrollmentStatus(sb, "IMG", imgStatus);
    
    if (fssStatus.enrollmentState() != EnrollmentStatus.State.Blank && imgStatus.enrollmentState() != EnrollmentStatus.State.Blank)
    {
      sb.append(", Consistency: ");
      sb.append(result.mixedScore());;
      sb.append(", complexity = ");
      sb.append(result.complexity());
      sb.append("}"); 
    }
    
    System.out.println(sb.toString());
  }
  
  private static void appendEnrollmentStatus(StringBuilder sb, String name, EnrollmentStatus state)
  {
    sb.append(", ");
    sb.append(name);
    sb.append(" Template: ");
    sb.append(state.enrollmentState());
    if (state.enrollmentState() == EnrollmentStatus.State.Enrolling)
    {
      sb.append("(");
      sb.append(state.numSignatures());  
      sb.append("/");
      sb.append(state.enrollmentSize());
      sb.append(")");
    }
  }
  
  public static void main(String args[]) 
  {
    try 
    {
      if (args.length == 0)
      {
        printHelp();
        return;
      }

      GsvJavaTest gsvTest = new GsvJavaTest(args);

      gsvTest.run();
    }
    catch( java.lang.Exception ex ) 
    { 
        System.out.println("Exception: " + ex.toString());
    }
  }
}