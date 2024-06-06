# Wacom Ink SDK for Verification Java wrapper - Windows

## Requirements

The sample application is supplied for Windows 10 and above to demonstrate using the SDK.

For signature capture use one of the supported devices (STU signature tablet or Pen Display tablet such as DTU-1141)

## Summary

Wacom Ink SDK for verification is a software-based method of digitally verifying handwritten eSignatures in real time, helping to prevent fraud of signature-centric workflows.

## Licensing

A valid license is needed for using the Java wrapper. 

The demo code includes an evaluation license.

### Setting up the license

You may obtain either an evaluation license or commercial license at: https://developer.wacom.com/en-us/developer-dashboard/license-keys.

An evaluation license is available within GETTING-STARTED.md.

NB: this particular license is only valid for a limited time.

## Instructions

To install, run the x64 or x86 installer obtained from https://developer-docs.wacom.com.
The PATHs in the build.bat and run.bat will require adjustment. The JARs and DLLs are installed to C:\Program Files (x86)\Common Files\WacomGSS.

Your CLS_PATH should reference the wgssGSV and wgssJNI.jar files in C:\Program Files (x86)\Common Files\WacomGSS.
Similarly, your LIB_PATH should reference the location of your GsvJavaTest.java.

The JAVA_EXE may also require adjustment to reference your javac.exe and java.exe.

For further details on using the SDK, see the Java documentation installed by the .msi in %ProgramFiles%\Wacom\Signature Verification\Documentation\Java\index.html

Additionally, see https://developer-docs.wacom.com
Navigate to: Wacom Ink SDK for verification
References are included to the SDK sample code on GitHub: https://github.com/Wacom-Developer/gsv-sdk-windows

Copyright © 2022 Wacom, Co., Ltd. All Rights Reserved.