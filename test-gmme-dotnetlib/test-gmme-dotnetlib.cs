﻿//---------------------------------------------------------
//---------------------------------------------------------
#define TestCMDLine
#define TestCMDLine_AddArgsArray
#define xTestCMDLine_AddArgsLine


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
//#if TestCMDLineArgs

#endif
/*
#if TestCMDLineString
			StringBuilder l_sb = new StringBuilder();
			l_sb.Append("-testftp1 host1,user1,password1 ").
				 Append("-testftp2 host2,user2,password2,type2 ").
				 Append("-TestFtp3 host3,user3,password3,type3 ");
			l_sb.Append("-testoptvalue1 TestOptValue1 ").
				 Append("-TestOptValue2 TestOptValue2 ").
				 Append("-TestOptValue3 TestOptValue3 ");
			l_sb.Append("-testisopt1 ").
				 Append("-TestIsOpt2 ");

			TestCmdLine.TestString(l_sb.ToString());
#endif
*/


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
			"@./opt/custperf.opt",
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
		//-- build test string to use for testing
		StringBuilder l_sb = new StringBuilder();

		l_sb.Append("-test${username}ftp1 host1,user1,password1 ").
				Append("-testftp2 host2,user2,password2,type2 ").
				Append("-TestFtp3 host3,user3,password3,type3 ").
				Append("-testftp2 host2a,user2a,password2a,type2a ");
		l_sb.Append("-testoptvalue1 TestOptValue1 ").
				Append("-TestOptValue2 TestOptValue2 ").
				Append("-TestOptValue3 TestOptValue3 ");
		l_sb.Append("-testisopt1 ").
				Append("-TestIsOpt2 ");
		l_sb.Append("-testsub01 '${CMDLINE_TESTSUB01}' ").
				Append("-testsub02 '${CMDLINE_TESTSUB02}' ");
		l_sb.Append("-testflag1 -testflag2 ");
//		l_sb.Append("-testsub01 '${CMDLINE_TESTSUB01}' ");

		//-- test 
		GMMELib.Utils.CMDLine l_cmdline = new GMMELib.Utils.CMDLine();
		l_cmdline.AddArgsLine(l_sb.ToString());
		l_cmdline.Dump();
	}
#endif
}
#endif