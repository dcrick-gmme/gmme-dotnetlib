#===============================================================================
#-- test powershell program for loading and using gmme-donetlib.dll
#===============================================================================

#-------------------------------------------------------------------------------
#-- load .dll and output version
#-------------------------------------------------------------------------------
add-type -path ..\gmme-dotnetlib\bin\Debug\net7.0\gmme-dotnetlib.dll
"GMMELib Version = " + [GMMELib.Info]::Version()


#-------------------------------------------------------------------------------
#-- test cmdline
#-------------------------------------------------------------------------------
$l_cmdline = New-Object GMMELib.Utils.CMDLine
#("-testa")
$l_cmdline.Dump()
"we are here"