REM This script registers the documentation in target help
REM system. That means the CHM file will be accessible from
REM other CHM files without specifying its full path. Moreover,
REM this documentation becomes context sensitive in Visual Studio .NET.
REM You should call the following line or this script during
REM installation of your product on the target machine.
REM All 3 files (EXE, CHM and XML) must be in the same directory.

HelixoftHelpReg.exe -c"ISE_TEST_V1.chm" -d"ISE_TEST_V1_dyn_help.xml"			

