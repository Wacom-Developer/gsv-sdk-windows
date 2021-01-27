# Wacom Ink SDK for Verification - Windows

**NOTICE:**

All of the content provided in this repository is **classified as Wacom Confidential Material**, therefore the signed NON-DISCLOSURE AGREEMENT applies.
Be aware that the **technology components are still under active development** with minimal QA testing, and **API interfaces and functionality could be changed or removed**.
Moreover, the legal framework is not yet finalised, thus the service **MUST BE USED ONLY FOR ACADEMIC PURPOSE AND NOT WITH CUSTOMER DATA. WACOM DOES NOT STORE ANY DATA PERMANENTLY, ONLY TEMPORARILY FOR PROCESSING**.

---

## Introduction

The purpose of the GSV (General Signature Verification) SDK is to verify handwritten signatures.
It uses and extends the existing verification tools, including the SSV (Static Signature Verification) SDK for processing scanned images, 
and the DSV (Dynamic Signature Verification) SDK which handles signatures captured in Wacom's proprietary FSS (Forensic Signature Store) format.
GSV supports all the functionality of the previous SDKs but handles both formats in a single component and allows individual signing variability to be measured by enrolling signature samples in a template.


## Overview

The main GSV component is the **SignatureEngine** which is a COM component responsible for enrolling and verifying signatures. The SignatureEngine does not store any data but processes signatures and updates a **Template** for each person to record their signing characteristics. It is the responsibility of the calling application to supply the user's template with each signature being verified, and to store the updated version afterwards.

The main features of the GSV SignatureEngine include the following:-

- A single template can handle either dynamic signatures supplied in FSS format or static signatures supplied as scanned graphical images, or both. In most implementations it is expected than one type or other will be used, but when both types are handled the SignatureEngine checks that the two sets of data are of the same signature. The SignatureEngine will also accept FSS data which is embedded steganographically in a graphical image.

- When a new person is to be verified the calling system must first create a new template which will record their signing behaviour. There are a number of configuration options that must be set at this stage and which cannot be subsequently changed. These control the way in which the enrollment process is handled and subsequently updated.

- Once the template has been created signatures can be verified. This is done using the **VerifySignature** method. The method must be supplied with the user's template and the signature.

- After the signature has been processed the following is returned to the calling application:-
  - A score of between 0 (inconsistent signature) and 1 (consistent signature). Note that for the first signature no comparison is possible and the score is meaningless
  - A flag indicating the type of verification that was used i.e. dynamic or static
  - A flag indicating the nature of the comparison that was used to arrive at the score e.g. whether the signatures differed in their geometry, timing, pressure variability etc.
  - A summary of the status of the template, e.g enrolling, enrolled, updated etc.
  - The updated template which should be saved for the next verification for the user
  
- A user's template becomes fully enrolled when the required number of consistent signatures have been verified. By default the number needed is 6 but different values can be set when the template is created. If when the required number has been received one of the signatures is significantly inconsistent with the others then it is rejected and the enrollment process will continue. Some inconsistent signers may need to verify more than the nominal enrollment number of signatures to become fully enrolled. 

- During enrollment the verification score for each signature is determined using the conventional DSV or SSV engines depending on the data type. These use 1:1 comparisons which are assessed using average variability characteristics. Once enrollment has been completed each signature being verified is compared against the range of characteristics measured in the enrollment set, which generally reduces the false acceptance error rate significantly. 

- After enrollment has been completed the reference data set can be periodically updated to track the drift of a user's signing behaviour with time. The minimum elapsed period between updates is set when the template is created


## SDK Delivery


The GSV SDK includes the following:

| Name                      | Description |
| ------------------------- | ----------- |
| SignatureEngine Component | The core verification functionality is provided in the form of a COM component. The component is secured using a machine specific license |
| Documentation             | The SDK is supplied with a detailed doxygen API reference |
| Installer                 | An MSI installer is provided to install and register the SDK COM components. A licenser app is also included to report the machine identifier and install the machine license key |

## SDK Sample Application

The C# .NET sample application is supplied with source code and demonstrates the following features :

| Feature                       | Description |
| ----------------------------- | ----------- |
| Options                        | A menu item opens a dialog which allows the user to modify the ConfigurationOptions, ImageOptions and the template folder. The user options are used whenever a new template is created |
| Templates                      | Every template is given a name when it is created. A list of all the templates is displayed in a list box and one of them highlighted as the current template. Controls are provided to delete and reset templates.  Templates are stored in the folder shown on the options dialog |
|  Verify signature from file    | The app allows the user to drag and drop signature files which are then verified using the currently selected template and the results displayed. Dynamic signatures may be in .FSS or .TXT (Base-64 encoded) form. Signature image files can be in any common format, including .PNG, .TIF, .BMP, .JPG etc.  Images containing stegangraphically embedded data will be processed as FSS data. |
|  Capture and verify signature  |  A button is provided to capture a signature using the Signature SDK and verify it against the currently selected template  |

---

## Verification SDK API

### Data types

The **SignatureEngine** component handles the following data types:

**Data: DynamicSignature**

A signature can be supplied as SigObj signature object, binary signature data or signature data as hex- or base-64 encoded text.

**Data: SignatureImage**

A signature image can be supplied in any of the common formats, including BMP, TIF, PNG, JPG etc.  
It can be a scanned image of an ink-on-paper signature, or it can be an image which contains FSS data which has been steganographically embedded.

*Steganographic data has precedence over scanned images.*

**Data: Template**

A template contains all the signing information for a single user. It includes the enrollment data for both types of signature (dynamic and static).  
Templates are handled as BLOBs which are obfuscated and cannot be parsed except by the SignatureEngine. The BLOB data can be handled in either binary form or as base-64 encoded text.

**Data: ConfigurationOptions**

*Configuration options apply to all types of signatures.*  
*The enrollmentScore is the lowest found when comparing the most extreme signatures in the enrollment set.*  
*The updateInterval is defined in days here but is converted to seconds internally.*

| Type   | Name             | Description |
| ----   | -----------      | ----------- |
| UINT   | templateSize     | Number of signatures needed for enrollment.<br/>Min = 3, Max = 12, Default = 6 |
| FLOAT  | enrollmentScore  | The minimum score needed to define a consistent set of signatures.<br/>Default = 0.2 |
| UINT   | updateInterval   | The minimum number of days that must elapse before enrolled templates can be updated.<br/>Default = 30 days |

**Data: ImageOptions**

*Image options are used to control the way in which static images are processed and don't affect dynamic data in any way.*  
*Some cleaning options are processor intensive and will increase the verification time.*


| Type   | Name               | Description |
| ----   | -----------        | ----------- |
| BOOL   | removeSpeckle      | Remove small dots produced by poor scanning or low quality paper<br/>Default: FALSE |
| BOOL   | removeFold         | Remove straight lines resulting from folds in the scanned paper<br/>Default: FALSE |
| BOOL   | removeBox          | Remove the rectangular box defining the signature area<br/>Default: FALSE |
| BOOL   | removeSigningLine  | Remove the printed signature line.<br/>Default: FALSE |
| FLOAT  | minSigningLineLength | Only applies when *removeSigningLine* is TRUE. Forces the line removal to ignore lines shorter than the given length in millimetres.<br/>Default: 0 |
| FLOAT  | maxSigningLineThickness   | Only applies when *removeSigningLine* is TRUE. Forces the line removal to ignore lines that are thicker than the specified width in millimetres.<br/>Default: 0 |
| BOOL   | adjustContrast     | Adjust the image contrast before extracting signature ink.<br/>Default: FALSE |
| INT    | Contrast           | Only applies when *adjustContrast* is TRUE. Sets the contrast adjustment to be applied.<br/>Default: 0, Min: -100, Max: 100 |
| BOOL   | setImageResolution | The image resolution should normally be defined in the image metadata. However, some scanners fail to set the correct value and this option forces the correct value to be used.<br/>Default: FALSE |
| UINT   | ImageResolution    |  The image resolution to be used when *setImageResolution* is TRUE |

**Data: State**

*Note that there are three separate sets of enrollment: dynamic, static and mixed.*

| Type   | Name        | Description |
| ----   | ----------- | ----------- |
| Enum   | Blank       | No signatures have been added |
|        | Enrolling   | One or more signatures have been verified but not enough for a consistent enrollment set |
|        | Enrolled    | Sufficient consistent signatures have been verified and the template is enrolled |
|        | Updated     | Fully enrolled and last signature verified was used to update the reference data |

**Data: EnrolmentStatus**

*Note that the NumSignatures could be equal to the number of signatures required for enrollment while the **State** is still **Enrolling**. This can occur if an excessively inconsistent set of reference signatures are enrolled.*

| Type   | Name           | Description |
| ----   | -----------    | ----------- |
| State  | EnrolmentState |  Condition of enrollment for this type of signature |
| UINT   | NumSignatures  | Current number of signatures stored |
| UINT   | EnrolmentSize  | Number of consistent signatures needed for enrollment|

**Data: TemplateStatus**

*The mixed data consists of comparisons between dynamic and static types of data.*
*This is only needed transitionally and will never reach full enrollment so is not reported here*

| Type             | Name          | Description |
| ----             | -----------   | ----------- |
| EnrolmentStatus  | DynamicStatus | Current state of the dynamic reference data |
| EnrolmentStatus  | StaticStatus  | Current state of the static reference data |

**Data: ComparisonType**

| Type   | Name             | Description |
| ----   | -----------      | ----------- |
| Enum   | NoComparisonDone | No comparison was done (usually when the first signature is verified) |
|        | Static           | The static image comparison engine was used |
|        | Dynamic          | The dynamic signature comparison engine was used |
|        | Mixed            | The static image comparison was used with one of the signatures being converted from dynamic to an artificial image|

**Data: InconsistencyType**

| Type   | Name             | Description |
| ----   | -----------      | ----------- |
| Enum   | Consistent       | When a verified signature is found to be a good match a score of 1.0 is returned |
|        | Geometry         | The overall shape of the verified signature differs from the reference signature |
|        | LocalShape       | Local inconsistencies in the shape of the signature were found |
|        | Speed            | The average speed of the signature is different |
|        | Acceleration     | The acceleration profile of the signature differs |
|        | Pressure         | The pressure profile is different |
|        | Size             | There are significant differences in the overall size of the signature |
|        | SVM              | The classification used a Support Vector Machine engine |

**Data: VerificationResult**

*Returned from the verifier when a signature is verified.*  
*The MixedScore uses the static verifier to compare the reference signature from the dynamic and static sets. It is only available when both types of data are being used.*

| Type              | Name             | Description |
| ----              | -----------      | ----------- |
| FLOAT             | Score            | The comparison score, a value between 0 (inconsistent) and 1 (consistent) |
| ComparisonType    | Engine           | The type of comparison engine used |
| InconsistencyType |  Inconsistency   | The main type of difference found |
| TemplateStatus    | State            | The enrollment status for each type of signature |
| FLOAT             | Complexity       | Indicates the complexity of the last signature verified. The value lies in the range 0 (trivially simple) to 1 (very long and with many features).<br/>This only applies to dynamic signatures; images always have a complexity of 0 |
| FLOAT             | MixedScore       | The verification score comparing the static and dynamic data when both types are being handled |
| String            |  UpdatedTemplate |   Updated template data as base-64 encoded text |


### SignatureEngine Component

The **SignatureEngine** is a stateless component which is used to maintain a signature **template**. It has the following methods:

**Method: CreateTemplate**

Used to create a new template which is returned as a BLOB containing the general configuration options.

| Parameter Type    | Parameter Name  | Description |
| --------------    | --------------  | ----------- |
| (IN) ConfigurationOptions	| GeneralOptions | Sets configuration options to control the way in when signatures are enrolled. These options are set when the template is created and cannot be subsequently changed |
| (IN) ImageOptions	| CleaningOptions	| Used to control the way in which static images are processed. They are optional and can be ignored when only dynamic data is being handled. |
| (OUT) String	    | TemplateData	  | The template data is returned to the calling application in the form of a BLOB as base-64 encoded text |

**Method: GetTemplateStatus**

Extracts information about the enrollment status of a template.

| Parameter Type    | Parameter Name  | Description |
| --------------    | --------------  | ----------- |
| (IN) Template     |	TemplateData	  | A BLOB previously created by the **SignatureEngine** and supplied by the calling application |
| (OUT) TemplateStatus	| Status	    | Summary of the current template status |

**Method: VerifySignature**
	
Takes a single signature in either FSS format or an image and verifies it against the supplied template. The process depends on the state of the template.

For dynamic signatures (FSS format):

- If the template has no FSS data then the signature is added to the dynamic reference set. If there are one or more static signatures then the new signature is verified against that using the SSV engine and the score returned, otherwise no verification result is possible.
- If the template has data but isn't fully enrolled then the signature is verified using the DSV engine. The signature is added to the dynamic enrollment set.

For signature images:

- If the template has no static data then the signature is added to the static reference set. If there are one or more dynamic signatures then the new signature is verified against that using the DSV engine and the score returned, otherwise no verification result is possible.
- If the template has data but isn't fully enrolled then the signature is verified using the SSV engine. The signature is added to the static enrollment set.

In either case:

- If the template is fully enrolled then the signature is verified against the reference set of signatures and the verification result is returned. If sufficient time has passed since the template was last updated then the reference set can be updated.

| Parameter Type    | Parameter Name  | Description |
| --------------    | --------------  | ----------- |
| (IN) String	      | TemplateData	  | The application must supply the template which was previously created by the SignatureEngine. After the signature has been processed the template is updated and returned, in the VerificationResult, to the calling application which must save it for future use. |
| (IN) DynamicSignature	|  Signature	|Either:<br/>- FSS data captured using any of the Signature SDKs<br/>- The name of an image file containing the scanned image of an ink-on-paper signature |
| (OUT) VerificationResult |	Result	| Returns the verification score, verification type and failure type, the complexity of the signature being verified and a summary of the template status. |



---

## Feedback / Support

If you experience issues with the technology components, please file a ticket in our Developer Support Portal:

- [Developer Support Portal](https://developer.wacom.com/developer-dashboard/support)

## Technology Usage
**No Commercial Use**. NOTWITHSTANDING ANYTHING TO THE CONTRARY, THIS AGREEMENT DOES NOT CONVEY ANY LICENSE TO USE THE EVALUATION MATERIALS IN PRODUCTION, OR TO DISTRIBUTE THE EVALUATION MATERIALS TO ANY THIRD PARTY. THE PARTNER ARE REQUIRED TO EXECUTE A SEPARATE LICENSE AGREEMENT WITH WACOM BEFORE MANUFACTURING OR DISTRIBUTING THE EVALUATION MATERIALS OR ANY PRODUCTS THAT CONTAIN THE EVALUATION MATERIALS. The Partner hereby acknowledge and agree that: (i) any use by The Partner of the Evaluation Materials in production, or any other distribution of the Evaluation Materials is a material breach of this Agreement; and (ii) any such unauthorized use or distribution will be at The Partner sole risk. No such unauthorized use or distribution shall impose any liability on Wacom, or any of its licensors, whether by implication, by estoppel, through course of dealing, or otherwise. The Partner hereby agree to indemnify Wacom, its affiliates and licensors against any and all claims, losses, and damages based on The Partner use or distribution of the Evaluation Materials in breach of this Agreement.


