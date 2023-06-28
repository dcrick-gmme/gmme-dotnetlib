//---------------------------------------------------------
//---------------------------------------------------------
#define TestCMDLine
#define xTestCMDLine_AddArgsArray
#define xTestCMDLine_AddArgsLine
#define TestCMDLine_AddArgsLineSB
#define TestCMDLine_GetOptXXX


#define xTestCMDLineString

#define xTestLogger1
#define xTestLogger2
#define xTestLogger3
#define xTestLogger4
#define xTestLogger5


using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Net;
//using System.Reflection;
using System.Text;
//using System.Threading;

using GMMELib;

class TestGMMELib
{
	static void Main(string[] a_args)
	{
		Console.WriteLine("Hello, World!");
		Console.WriteLine("Version = " + GMMELib.Info.Version());


#if TestCMDLine
#if TestCMDLine_AddArgsArray
		TestCMDLine.AddArgsArray();
#endif
#if TestCMDLine_AddArgsLine
		TestCMDLine.AddArgsLine();
#endif
#if TestCMDLine_AddArgsLineSB
		TestCMDLine.AddArgsLineSB();
#endif

#endif
	}
}

#if TestCMDLine
class TestCMDLine
{
#if TestCMDLine_AddArgsArray
	public static void AddArgsArray()
	{
		string[] l_args = 
		{
			"@./opt/test01.opt",
			"-test1", "test1val",
			"-test3opt",
			"-test2", "test2val"
		};
		GMMELib.Utils.CMDLine l_cmdline = new GMMELib.Utils.CMDLine();
		l_cmdline.AddArgsArray(l_args);
		l_cmdline.Dump();
	}
#endif

#if TestCMDLine_AddArgsLine
	public static void AddArgsLine()
	{
		GMMELib.Utils.CMDLine l_cmdline = new GMMELib.Utils.CMDLine();
		l_cmdline.AddArgsLine("-test${username}ftp1 host1,user1,password1");
		l_cmdline.Dump();
	}
#endif

#if TestCMDLine_AddArgsLineSB
	public static void AddArgsLineSB()
	{
		//-- build test string to use for testing
		StringBuilder l_sb = new StringBuilder();

//		l_sb.Append("@./opt/test01.opt ");
		l_sb.Append("-bool1 1 -boolt t -booltrue true -boolon on -booly y -boolyes yes ");
		l_sb.Append("-bool0 0 -boolf f -boolfalse false -booloff off -booln n -boolno no ");
/*
		l_sb.Append("-testpath1 c:\\test\\test\\test02 ");
		l_sb.Append("-testpath2 c:\\test\\%\\test02\\");
*/
/*
		l_sb.Append("-test${username}ftp1 host1,user1,password1${username} ").
				Append("-testftp2 host2,user2,password2,type2 ").
				Append("-TestFtp3 host3,user3,password3,type3 ").
				Append("-testftp2 host2a,user2a,password2a,type2a ");
*/
/*
		l_sb.Append("-testoptvalue1 TestOptValue1 ").
				Append("-TestOptValue2 TestOptValue2 ").
				Append("-TestOptValue3 TestOptValue3 ");
		l_sb.Append("-testisopt1 ").
				Append("-TestIsOpt2 ");
		l_sb.Append("-testsub01 '${CMDLINE_TESTSUB01}' ").
				Append("-testsub02 '${CMDLINE_TESTSUB02}' ");
		l_sb.Append("-testflag1 -testflag2 ");
*/
		//-- test 
		GMMELib.Utils.CMDLine l_cmdline = new GMMELib.Utils.CMDLine();
		l_cmdline.AddArgsLine(l_sb.ToString(), "c:\\testfile${username}.opt");
		l_cmdline.Dump();

#if TestCMDLine_GetOptXXX
		bool l_bool0 = l_cmdline.GetBooleanOpt("-bool0");
		bool l_bool1 = l_cmdline.GetBooleanOpt("-bool1");
		bool l_boolf = l_cmdline.GetBooleanOpt("-boolf");
		bool l_boolt = l_cmdline.GetBooleanOpt("-boolt");
		bool l_boolfalse = l_cmdline.GetBooleanOpt("-boolfalse");
		bool l_booltrue = l_cmdline.GetBooleanOpt("-booltrue");
		bool l_booloff = l_cmdline.GetBooleanOpt("-booloff");
		bool l_boolon = l_cmdline.GetBooleanOpt("-boolon");
		bool l_booln = l_cmdline.GetBooleanOpt("-booln");
		bool l_booly = l_cmdline.GetBooleanOpt("-booly");
		bool l_boolno = l_cmdline.GetBooleanOpt("-boolno");
		bool l_boolyes = l_cmdline.GetBooleanOpt("-boolyes");

		bool l_bool0as1 = l_cmdline.GetBooleanOpt("-bool0", true, false, true);
		
//		.GetBooleanOpt("-bool1");
		l_sb.Append("-bool1 1 -boolt t -booltrue true -boolon on -booly y -boolyes yes ");
		l_sb.Append("-bool0 0 -boolf f -boolfalse false -booloff off -booln n -boolno no ");

/*
		string? l_path1 = l_cmdline.GetPathOpt("-testpath1");
		string? l_path2a = l_cmdline.GetPathOpt("-testpath2");
		string? l_path2b = l_cmdline.GetPathOpt("-testpath2", null, "crick");

		string? l_opt1 = l_cmdline.GetOptValue("-testftp2");
		string? l_opt2 = l_cmdline.GetOptValue("-testftp2x", "default value");
		string? l_opt1a = l_cmdline.GetOptValue("-testftp2", false);
*/
#endif
	}
#endif
}
#endif