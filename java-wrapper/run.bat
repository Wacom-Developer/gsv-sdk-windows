@ECHO OFF
SETLOCAL

SET JAVA_EXE="C:\Program Files\Java\jdk-17.0.2\bin\java.exe"
SET CLS_PATH="C:\Program Files (x86)\Common Files\WacomGSS\wgssGSV.jar";"C:\Program Files (x86)\Common Files\WacomGSS\wgssJNI.jar"
SET LIB_PATH="<<PUT PATH TO GSVJAVATEST.JAVA HERE>>"

%JAVA_EXE% -classpath %CLS_PATH% -Djava.library.path=%LIB_PATH% GsvJavaTest.java %*