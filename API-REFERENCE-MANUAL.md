- [Introduction](#introduction)
- [Data types summary](#data-types-summary)
- [Data types](#data-types)
  - [ConfigurationOptions](#configurationoptions)
  - [ImageOptions](#imageoptions)
  - [State](#state)
  - [EnrollmentStatus](#enrollmentstatus)
  - [TemplateStatus](#templatestatus)
  - [ComparisonType](#comparisontype)
  - [InconsistencyType](#inconsistencytype)
  - [VerificationResult](#verificationresult)
- [SignatureEngine Component](#signatureengine-component)
- [Methods summary](#methods-summary)
  - [CreateTemplate](#createtemplate)
  - [GetTemplateStatus](#gettemplatestatus)
  - [VerifySignature](#verifysignature)

## Introduction 

The **Wacom Ink SDK for verification**'s main component is the **SignatureEngine**. This document will cover the data types and methods that the component handles and possesses. 

## Data types summary

| Data types           |
| -------------------- |
| ConfigurationOptions |
| ImageOptions         |
| State                |
| EnrollmentStatus     |
| TemplateStatus       |
| ComparisonType       |
| InconsistencyType    |
| VerificationResult   |


## Data types

| Type             | Description                                                                                                                                                                                                                                                                                                                                      |
| ---------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| DynamicSignature | A signature can be supplied as SigObj signature object, binary signature data or signature data as hex- or base-64 encoded text                                                                                                                                                                                                                  |
| SignatureImage   | A signature image can be supplied in any of the common formats, including BMP, TIF, PNG, JPG etc. It can be a scanned image of an ink-on-paper signature, or it can be an image which contains FSS data which has been steganographically embedded. 4NB: Steganographic data has precedence over scanned images                                  |
| Template         | A template contains all the signing information for a single user. It includes the enrollment data for both types of signature (dynamic and static).      Templates are handled as BLOBs which are obfuscated and cannot be parsed except by the SignatureEngine. The BLOB data can be handled in either binary form or as base-64 encoded text. |

### ConfigurationOptions

Configuration options apply to all types of signatures. The enrollmentScore is the lowest found when comparing the most extreme signatures in the enrollment set. The updateInterval is defined in days here but must be converted to seconds internally.

| Type  | Name             | Description                                                                                                                                          |
| ----- | ---------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------- |
| UINT  | TemplateSize     | Number of signatures needed for enrollment. Min = 3, Max = 12, Default = 6                                                                           |
| FLOAT | EnrollmentScore  | The minimum score needed to define a consistent set of signatures. Default = 0.2                                                                     |
| UINT  | UpdateInterval   | The minimum number of days that must elapse before enrolled templates can be updated. Default = 30 days                                              |
| enum  | SignatureStyle   | Sets the classifier to type of signatures expected. Options: Cursive (default), Kanji, Auto (Auto not currently implemented in the engine.           |
| BOOL  | IgnoreDateTime*  | Instructs the system to ignore the data and time reported for each signature. Default = FALSE                                                        |
| BOOL  | ForceEnrollment* | Forces the system to complete the enrollment when the required number of signatures has been received, ignoring any inconsistencies. Default = FALSE |

***The asterisked options are provided for test purposes and shouldn't be used in production systems**

### ImageOptions

	
Image options are used to control the way in which static images are processed. They don't affect dynamic data in any way.

Some cleaning options are processor-intensive. This will increase the time required for verification.

| Type  | Name                    | Description                                                                                                                                                                                     |
| ----- | ----------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| BOOL  | RemoveSpeckle           | Remove small dots produced by poor scanning or low quality paper. Default: FALSE                                                                                                                |
| BOOL  | RemoveFold              | Remove straight lines resulting from folds in the scanned paper. Default: FALSE                                                                                                                 |
| BOOL  | RemoveBox               | Remove the rectangular box defining the signature area. Default: FALSE                                                                                                                          |
| BOOL  | RemoveSigningLine       | Remove the printed signature line. Default: FALSE                                                                                                                                               |
| FLOAT | MinSigningLineLength    | Only applies when **RemoveSigningLine** is TRUE. Forces the line removal to ignore lines shorter than the given length in millimetres. Default:0                                                |
| FLOAT | MaxSigningLineThickness | Only applies when **RemoveSigningLine** is TRUE. Forces the line removal to ignore lines that are thicker than the specified width in millimetres. Default: 0                                   |
| BOOL  | AdjustContrast          | Adjust the image contrast before extracting signature ink. Default: FALSE                                                                                                                       |
| INT   | Contrast                | Only applies when **AdjustContrast** is TRUE. Sets the contrast adjustment to be applied. Default: 0, Min: -100, Max: 100                                                                       |
| BOOL  | SetImageResolution      | The image resolution should normally be defined in the image metadata. However, some scanners fail to set the correct value and this option forces the correct value to be used. Default: FALSE |
| UINT  | ImageResolution         | The image resolution to be used when **SetImageResolution** is TRUE.                                                                                                                            |

### State


| Type | Name      | Description                                                                              |
| ---- | --------- | ---------------------------------------------------------------------------------------- |
| Enum | Blank     | No signatures have been added                                                            |
| Enum | Enrolling | One or more signatures have been verified but not enough for a consistent enrollment set |
| Enum | Enrolled  | Sufficient consistent signatures have been verified and the template is enrolled         |
| Enum | Updated   | Fully enrolled and last signature verified was used to update the reference data         |

**NB: there are three separate sets of enrollment: dynamic, static and mixed**

### EnrollmentStatus

Note that the NumSignatures could be equal to the number of signatures required for enrollment while the State is still Enrolling. This can occur if the supplied reference signatures being enrolled are too inconsistent.

| Type  | Name            | Description                                           |
| ----- | --------------- | ----------------------------------------------------- |
| State | EnrollmentState | Condition of enrollment for this type of signature    |
| UINT  | NumSignatures   | Current number of signatures stored                   |
| UINT  | EnrollmentSize  | Number of consistent signatures needed for enrollment |

### TemplateStatus

The mixed data consists of comparisons between dynamic and static types of data. This is only needed transitionally and will never reach full enrollment.

| Type             | Name          | Description                                 |
| ---------------- | ------------- | ------------------------------------------- |
| EnrollmentStatus | DynamicStatus | Current state of the dynamic reference data |
| EnrollmentStatus | StaticStatus  | Current state of the static reference data  |


### ComparisonType

| Type | Name             | Description                                                                                                         |
| ---- | ---------------- | ------------------------------------------------------------------------------------------------------------------- |
| Enum | NoComparisonDone | No comparison was done (usually when the first signature is verified)                                               |
| Enum | Static           | The static image comparison engine was used                                                                         |
| Enum | Dynamic          | The dynamic signature comparison engine was used.                                                                   |
| Enum | Mixed            | The static image comparison was used with one of the signatures being converted from dynamic to an artificial image |

### InconsistencyType

| Type | Name         | Description                                                                      |
| ---- | ------------ | -------------------------------------------------------------------------------- |
| Enum | Consistent   | When a verified signature is found to be a good match a score of 1.0 is returned |
| Enum | Geometry     | The overall shape of the verified signature differs from the reference signature |
| Enum | LocalShape   | Local inconsistencies in the shape of the signature were found                   |
| Enum | Speed        | The average speed of the signature is different                                  |
| Enum | Acceleration | The acceleration profile of the signature differs                                |
| Enum | Pressure     | The pressure profile is different                                                |
| Enum | Size         | There are significant differences in the overall size of the signature           |
| Enum | SVM          | The classification used a Support Vector Machine engine                          |

### VerificationResult

The verification result is returned from the verifier upon successful signature verification.

The MixedScore uses the static verifier to compare the reference signature to both the dynamic and static sets. It is only available when both types of data are being used.

| Type              | Name            | Description                                                                                                                                                                                                                      |
| ----------------- | --------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| FLOAT             | Score           | The comparison score, a value between 0 (inconsistent) and 1 (consistent)                                                                                                                                                        |
| ComparisonType    | Engine          | The type of comparison engine used                                                                                                                                                                                               |
| InconsistencyType | Inconsistency   | The main type of difference found                                                                                                                                                                                                |
| TemplateStatus    | State           | The enrollment status for each type of signature                                                                                                                                                                                 |
| FLOAT             | Complexity      | Indicates the complexity of the last signature verified. The value lies in the range 0 (trivially simple) to 1 (very long and with many features). This only applies to dynamic signatures; images always have a complexity of 0 |
| FLOAT             | MixedScore      | The verification score comparing the static and dynamic data when both types are being handled                                                                                                                                   |
| String            | UpdatedTemplate | Updated template data as base-64 encoded text                                                                                                                                                                                    |


## SignatureEngine Component

The **SignatureEngine** is a stateless component which is used to maintain a signature template. It has the following methods:

## Methods summary

| Methods           |
| ----------------- |
| CreateTemplate    |
| GetTemplateStatus |
| VerifySignature   |

### CreateTemplate

This method is used to create a new template. The template is returned as a BLOB containing the general configuration options.

| Parameter Type            | Parameter Name  | Description                                                                                                                                                          |
| ------------------------- | --------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| (IN) ConfigurationOptions | GeneralOptions  | Sets configuration options to control the way in when signatures are enrolled. These options are set when the template is created and cannot be subsequently changed |
| (IN) ImageOptions         | CleaningOptions | Used to control the way in which static images are processed. They are optional and can be ignored when only dynamic data is being handled.                          |
| (OUT) String              | TemplateData    | The template data is returned to the calling application in the form of a BLOB as base-64 encoded text                                                               |

### GetTemplateStatus

This method extracts information about the enrollment status of a template.

| Parameter Type       | Parameter Name | Description                                                                              |
| -------------------- | -------------- | ---------------------------------------------------------------------------------------- |
| (IN) Template        | TemplateData   | A BLOB previously created by the SignatureEngine and supplied by the calling application |
| (OUT) TemplateStatus | Status         | Summary of the current template status                                                   |

### VerifySignature

This method takes a single signature in either FSS format or an image and verifies it against the supplied template. The process depends on the state of the template.

For dynamic signatures (FSS format):

- If the template has no FSS data, then the signature is added to the dynamic reference set. If there are one or more static signatures then the new signature is verified against that using the SSV (Static Signature Verification) engine and the score returned, otherwise no verification result is possible.
- If the template has data but isn't fully enrolled then the signature is verified using the DSV (Dynamic Signature Verification) engine. The signature is added to the dynamic enrollment set.
For signature images:

- If the template has no static data then the signature is added to the static reference set. If there are one or more dynamic signatures then the new signature is verified against that using the DSV engine and the score returned, otherwise no verification result is possible.
- If the template has data but isn't fully enrolled then the signature is verified using the SSV engine. The signature is added to the static enrollment set.

In either case: If the template is fully enrolled then the signature is verified against the reference set of signatures and the verification result is returned. If sufficient time has passed since the template was last updated then the reference set can be updated.

| Parameter Type           | Parameter Name | Description                                                                                                                                                                                                                                                         |
| ------------------------ | -------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| (IN) String              | TemplateData   | The application must supply the template which was previously created by the SignatureEngine. After the signature has been processed the template is updated and returned, in the VerificationResult, to the calling application which must save it for future use. |
| (IN) DynamicSignature    | Signature      | Either: FSS data captured using any of the Signature SDKs, OR: The name of an image file containing the scanned image of an ink-on-paper signature.                                                                                                                 |
| (OUT) VerificationResult | Result         | Returns the verification score, verification type and failure type, the complexity of the signature being verified and a summary of the template status.                                                                                                            |