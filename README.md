# Wacom Ink SDK for Verification - Windows

**NOTICE:**

All of the content provided in this repository is **classified as Wacom Confidential Material**, therefore the signed NON-DISCLOSURE AGREEMENT applies.
Be aware that the **technology components are still under active development** with minimal QA testing, and **API interfaces and functionality could be changed or removed**.
Moreover, the legal framework is not yet finalised, thus the service **MUST BE USED ONLY FOR ACADEMIC PURPOSE AND NOT WITH CUSTOMER DATA. WACOM DOES NOT STORE ANY DATA PERMANENTLY, ONLY TEMPORARILY FOR PROCESSING**.

---

## Introduction

The  Wacom Ink SDK for Verification is used to verify handwritten signatures. The main features are:


*	 The Signature Engine can handle both dynamic and static signatures: 
 
  *	 Dynamic signatures are captured using a suitable pen tablet or signature pad and must be supplied in FSS format.
    
  *  Static images are obtained by scanning ink-on-paper originals. A wide range of image formats is supported including BMP, PNG, TIF and JPEG.
    
*	 The Signature Engine enrolls a template for each person so that their individual characteristics and variability can be assessed accurately. The template is created by the Signature Engine when the first signature is received from a person and then updated as additional signatures are received. It is the responsibility of the calling application to store the template for each person and supply it with each new signature that needs to be verified. The Signature Engine does not store any user data.
 
*	 The number of signatures needed for enrollment is configurable with the default value set at 6. During the enrollment phase new signatures are enrolled using either the DSV or SSV engine as appropriate and returns the verification result as a score between 0 (inconsistent) and 1 (consistent).
 
*	 When the required number of consistent signatures have been verified the template is considered to be enrolled. Subsequent signatures are then processed using the individual's enrollment data instead of the DSV or SSV. The results are still reported as scores between 0 and 1 but with lower error rates.
 
*	 After enrollment has been completed the template can be updated to track signature drift with time. The period that must have elapsed between updates is set at one month by default but can be configured.
 
*  Static and dynamic signature are enrolled independently but cross-checked for consistency.

*  Access to the Signature Engine is made through the ActiveX/COM interface. An application verifies a signature by calling the API, supplying the signature data, the individual's template data and a license.
The Signature Engine returns an updated template and the result of the verification.



## Overview

The main verification component is the **SignatureEngine** which is a COM component responsible for enrolling and verifying signatures. The SignatureEngine does not store any data but processes signatures and updates a **Template** for each person to record their signing characteristics. It is the responsibility of the calling application to supply the user's template with each signature being verified, and to store the updated version afterwards.


The main features of the SignatureEngine include the following:-

-  A single template can handle either dynamic signatures supplied in FSS format or static signatures supplied as scanned graphical images, or both. In most implementations it is expected that one type or other will be used, but when both types are handled the SignatureEngine checks that the two sets of data are of the same signature. The SignatureEngine will also accept FSS data which is embedded steganographically in a graphical image.


- When a new person is to be verified the calling system must first create a new template which will record their signing behaviour. There are a number of configuration options that must be set at this stage and which cannot be subsequently changed. These control the way in which the enrollment process is handled and subsequently updated.

- Once the template has been created signatures can be verified. This is done using the **VerifySignature** method. The method must be supplied with the user's template and the signature.


- After the signature has been processed the following is returned to the calling application:-
    - A score of between 0 (inconsistent signature) and 1 (consistent signature). Note that for the first signature no comparison is possible and the score is meaningless
    - A  flag indicating the type of verification that was used i.e. dynamic or static
    - A flag indicating the nature of the comparison that was used to arrive at the score e.g. whether the signatures differed in their geometry, timing, pressure variability etc.
    - A summary of the status of the template, e.g. enrolling, enrolled, updated etc.
    - The updated template which should be saved for the next verification for the user.

- A user's template becomes fully enrolled when the required number of consistent signatures have been verified. By default the number needed is 6 but different values can be set when the template is created. If when the required number has been received one of the signatures is significantly inconsistent with the others then it is rejected and the enrollment process will continue. Some inconsistent signers may need to verify more than the nominal enrollment number of signatures to become fully enrolled. 

- During enrollment the verification score for each signature is determined using the conventional DSV or SSV engines depending on the data type. These use 1:1 comparisons which are assessed using average variability characteristics. Once enrollment has been completed each signature being verified is compared against the range of characteristics measured in the enrollment set, which generally reduces the false acceptance error rate significantly. 

- After enrollment has been completed the reference data set can be periodically updated to track the drift of a user's signing behaviour with time. The minimum elapsed period between updates is set when the template is created. 

## SDK Delivery


The Verification SDK includes the following:

| Name                      | Description |
| ------------------------- | ----------- |
| SignatureEngine Component | The core verification functionality is provided in the form of a WPF COM component. The component is secured using a conventional machine license |
| Documentation             | The SDK is supplied with a detailed doxygen API reference |
| Installer                 | An MSI installer is provided to install and register the SDK COM components. A licenser app is also included to report the machine identifier and install the machine license key |

## SDK Sample Application

The C# .NET sample application is supplied with source code and demonstrates the following features :

| Feature                       | Description |
| ----------------------------- | ----------- |
| Options	                      | A menu item opens a dialog which allows the user to modify the ConfigurationOptions, ImageOptions and the template folder. The user options are used whenever a new template is created |
| Templates	                    | Every template is given a name when it is created. A list of all the templates is displayed in a list box and one of them highlighted as the current template. Controls are provided to delete and reset templates.	Templates are stored in the folder shown on the options dialog |
|	Verify signature from file	  | The app allows the user to drag and drop signature files which are then verified using the currently selected template and the results displayed. Dynamic signatures may be in .FSS or .TXT (Base-64 encoded) form. Signature image files can be in any common format, including .PNG, .TIF, .BMP, .JPG etc.	Images containing stegangraphically embedded data will be processed as FSS data. |
|	Capture and verify signature  |	A button is provided to capture a signature using the Signature SDK and verify it against the currently selected template	|



---

## Feedback / Support
Participants of the Wacom Beta Program will be provided with optional access to our Slack channel: 

- [Slack channel](https://wacom-will.slack.com)

Product Managers and a support engineer will be available in the channel to answer questions and receive valuable feedback.

If you experience issues with the technology components, please file a ticket in our Developer Support Portal:

- [Developer Support Portal](https://developer.wacom.com/developer-dashboard/support)

## Technology Usage
**No Commercial Use**. NOTWITHSTANDING ANYTHING TO THE CONTRARY, THIS AGREEMENT DOES NOT CONVEY ANY LICENSE TO USE THE EVALUATION MATERIALS IN PRODUCTION, OR TO DISTRIBUTE THE EVALUATION MATERIALS TO ANY THIRD PARTY. THE PARTNER ARE REQUIRED TO EXECUTE A SEPARATE LICENSE AGREEMENT WITH WACOM BEFORE MANUFACTURING OR DISTRIBUTING THE EVALUATION MATERIALS OR ANY PRODUCTS THAT CONTAIN THE EVALUATION MATERIALS. The Partner hereby acknowledge and agree that: (i) any use by The Partner of the Evaluation Materials in production, or any other distribution of the Evaluation Materials is a material breach of this Agreement; and (ii) any such unauthorized use or distribution will be at The Partner sole risk. No such unauthorized use or distribution shall impose any liability on Wacom, or any of its licensors, whether by implication, by estoppel, through course of dealing, or otherwise. The Partner hereby agree to indemnify Wacom, its affiliates and licensors against any and all claims, losses, and damages based on The Partner use or distribution of the Evaluation Materials in breach of this Agreement.


