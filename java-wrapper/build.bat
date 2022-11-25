@ECHO OFF
SETLOCAL
SET BC_PATH="C:\Program Files (x86)\Java\jre1.8.0_321\lib\rt.jar"
SET CLS_PATH="C:\Program Files (x86)\Common Files\WacomGSS\wgssGSV.jar"

javac -classpath %CLS_PATH% -bootclasspath %BC_PATH% -source 1.7 -target 1.7 -Xlint:deprecation GsvJavaTest.java