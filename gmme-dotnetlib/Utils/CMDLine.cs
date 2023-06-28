namespace GMMELib.Utils;
//----------------------------------------------------------------------------------------
//	GMMLib.Net.35 for .NET 3.5
//	Copyright (c) 2006, GMM Enterprises, LLC.  Licensed under the GMM Software License 
//	All rights reserved 
//----------------------------------------------------------------------------------------
//
//	File:	CMDLine.cs
//	Author:	David Crickenberger
//
//	Desc:	Command line processing class.
//
//--------------------------------------------------------------------------------

using System;
using System.Collections;
using System.IO;
using System.Text;

//namespace GMMLib
//{
	/// <summary>
	/// This a command line processor class for processing command line options.
	/// </summary>
	public partial class CMDLine
	{
		private SortedList<string, COptItem>? m_list = null;
		private SortedList<string, COptItem>? m_list2 = null;

		private bool m_init = false;


		//------------------------------------------------------------------------
		//------------------------------------------------------------------------
		//-- ctor's
		//------------------------------------------------------------------------
		//------------------------------------------------------------------------
		public CMDLine()
		{
		}
/*
		public CMDLine(bool a_initFromEnvironment)
		{
			if (!a_initFromEnvironment)
				return;
			
			string[] args1 = Environment.GetCommandLineArgs();
			if (args1.GetLength(0) == 1)
				return;

			string[] args2 = new string[args1.GetLength(0) - 1];
			Array.Copy(args1, 1, args2, 0, args1.GetLength(0) - 1);
			AddArgsArray(args2);
		}
*/
		public CMDLine(string[] a_args, string? a_file = null)
		{
			AddArgsArray(a_args, a_file);
		}
		public CMDLine(string a_line, string? a_file = null)
		{
			AddArgsLine(a_line, a_file);
		}
		public CMDLine(StringBuilder a_line, string? a_file = null)
		{
			AddArgsLine(a_line, a_file);
		}


		public bool IsInit
		{
			get		{ return m_init; }
		}


		//------------------------------------------------------------------------
		//-- initialize the internal optitem list
		private void initOptItemList_()
		{
			if (m_list == null)
				m_list = new SortedList<string, COptItem>();

			if (m_list2 == null)
				m_list2 = new SortedList<string, COptItem>();
		}


		//----------------------------------------------------------------------
		//-- Dump
		//----------------------------------------------------------------------
		public void Dump()
		{
			//------------------------------------------------------------------
			//-- see if anything to dump
	        if (!m_init || m_list == null || (m_list != null && m_list.Count == 0))
			{
				Console.WriteLine("CMDine: Dump - nothing exists...");
            	return;
			}

			//------------------------------------------------------------------
			//-- dump the list
			System.Console.WriteLine("CMDLine: Dump - beg:");
			if (m_list != null)
			{
				foreach(var l_kvp in m_list)
				{
					//----------------------------------------------------------
					//-- determine output
					string l_opt = l_kvp.Key;
					string? l_val = l_kvp.Value.Val;
					if (l_val == null)
						l_val = "<null>";
					else if (l_val.Length == 0)
						l_val = "<empty>";

					//----------------------------------------------------------
					//-- build output string
					StringBuilder l_dbgOut = new StringBuilder("   ");
					l_dbgOut.Append(l_opt).Append(" = [" + l_val + "]");
					if (l_kvp.Value.File is not null)
						l_dbgOut.Append(", file = " + l_kvp.Value.File);

					//----------------------------------------------------------
					//-- build output string for orig settings
					StringBuilder l_dbgOutOrig = new StringBuilder();
					if (l_kvp.Value.Opt != l_kvp.Value.OptOrig)
						l_dbgOutOrig.Append("opt = " + l_kvp.Value.OptOrig);
					if (l_kvp.Value.Val != l_kvp.Value.ValOrig)
					{
						if (l_dbgOutOrig.Length > 0)
							l_dbgOutOrig.Append(", ");
						l_dbgOutOrig.Append("val = " + l_kvp.Value.ValOrig);
					}
					if (l_kvp.Value.File != l_kvp.Value.FileOrig)
					{
						if (l_dbgOutOrig.Length > 0)
							l_dbgOutOrig.Append(", ");
						l_dbgOutOrig.Append("file = " + l_kvp.Value.FileOrig);
					}
					if (l_dbgOutOrig.Length > 0)
						l_dbgOut.Append(" :::: Orig => [" + l_dbgOutOrig + "]");

					System.Console.WriteLine(@l_dbgOut);
				}
			}
			System.Console.WriteLine("CMDLine: Dump - beg:");
/*
        print("CmdLine: Dump - beg")
        l_opts = list(self.m_opts.keys())
        l_opts.sort()
        for l_opt in l_opts:
            l_val, l_tags = (self.m_opts[l_opt]['val'], self.m_opts[l_opt]['tags'])
            if l_val is None:           l_val = '<none>'
            elif l_val == '':           l_val = '<empty string>'
            else:
                if len(l_tags) > 0:     l_val = 'X' * random.sample(range(10,20),1)[0]
            print('   ' + l_opt + ' == [' + l_val + ']')
        print("CmdLine: Dump - end")
*/
		}

		//----------------------------------------------------------------------
		//-- support functions to adding items, subenv and finding items
		//----------------------------------------------------------------------
		private void addItemToList_(string? a_opt, string? a_val)
		{
			addItemToList_(a_opt, a_val, null);
		}
		private void addItemToList_(string? a_opt, string? a_val, string? a_file)
		{
			//------------------------------------------------------------------
			//-- prepare opt and val
			if (a_opt is null)
				return;
			string? l_opt = subEnv_(a_opt);
			string? l_val = subEnv_(a_val);
			string? l_file = subEnv_(a_file);

			//------------------------------------------------------------------
			//-- add item to m_list
			COptItem l_item = new COptItem(l_opt, a_opt, l_val, a_val, l_file, a_file);
			if (l_opt is not null && l_opt.Length > 1)
			{
				if (m_list is not null)
					m_list[l_opt.ToUpper()] = l_item;
				if (m_list2 is not null)
					m_list2[l_opt] = l_item;
			}
		}

		private string? subEnv_(string? a_str)
		{
			int p1;
			int p2;

			string env;
			string? envStr;


			//------------------------------------------------------------------
			//-- see if we have an environment
			if (a_str == null || a_str.Length == 0)
				return a_str;


			//------------------------------------------------------------------
			//-- loop thru the string and see what we have.  Cmd line enviornment
			//-- variables are substitured with "$(VAR)"
			while ((p1 = a_str.IndexOf("${")) != -1)
			{
				//-- find end of environment variable
				if ((p2 = a_str.IndexOf("}", p1 + 2)) == -1)
					return a_str;


				//-- pull the string, get environment and subst
				env = a_str.Substring(p1 + 2, p2 - p1 - 2);
				envStr = Environment.GetEnvironmentVariable(env);
				a_str = a_str.Replace("${" + env + "}", envStr);
			}

			return a_str;
		}
		private COptItem? findOptHelper_(string a_opt, bool a_ucase = true)
		{
			//-- make sure list is not empty
			SortedList<string, COptItem>? l_list = (a_ucase) ? m_list : m_list2;
			if (l_list is not null)
				if (l_list.Count == 0)
					return null;

			//-- determine compare function and search for option
			COptItem? l_item = null;
			string l_key = (a_ucase) ? a_opt.ToUpper() : a_opt;
			if (l_list is not null)
				if (l_list.ContainsKey(l_key))
					l_item = l_list[l_key];

			return l_item;
		}


		//----------------------------------------------------------------------
		//----------------------------------------------------------------------
		//-- access routines
		//----------------------------------------------------------------------
		private string? getOptValueHelper_(string a_opt, bool a_ucase)
		{
			if (!m_init)
				return null;

			//-- look for the option
			COptItem? l_item = findOptHelper_(a_opt, a_ucase);
			if (l_item is not null && l_item.Val is not null)
			{
				if (l_item.Val.Length == 0)
					return null;
				return l_item.Val;
			}

			return null;
		}

		//----------------------------------------------------------------------
		//-- access routines - GetOptXXX
		//----------------------------------------------------------------------
		public string? GetOptValue(string a_opt, bool a_ucase = true)
		{
			return getOptValueHelper_(a_opt, a_ucase);
		}
		public string? GetOptValue(string a_opt, string? a_default, bool a_ucase = true)
		{
			string? l_val = getOptValueHelper_(a_opt, a_ucase);
			if (l_val == null || l_val.Length == 0)
				return a_default;

			return l_val;
		}

		//----------------------------------------------------------------------
		//-- access routines - GetPathOptXXX
		//----------------------------------------------------------------------
		public string? GetPathOpt(string a_opt, bool a_ucase = true)
		{
			return getPathOptHelper_(a_opt, null, null, a_ucase);
		}
		public string? GetPathOpt(string a_opt, string? a_default, bool a_ucase = true)
		{
			return getPathOptHelper_(a_opt, a_default, null, a_ucase);
		}
		public string? GetPathOpt(string a_opt, string? a_default, string a_subst, bool a_ucase = true)
		{
			return getPathOptHelper_(a_opt, a_default, a_subst, a_ucase);
		}
		private string? getPathOptHelper_(string a_opt, string? a_default, string? a_subst, bool a_ucase)
		{
			//------------------------------------------------------------------
			//-- get option with default value
			string? l_str = GetOptValue(a_opt, a_default, a_ucase);
			if (l_str == null || l_str.Length == 0)
				return "";

			//------------------------------------------------------------------
			//-- see if substitution should be done, if '%' exists then replace
			//-- all with value of <a_subValue>
			if (a_subst is not null && a_subst.Length > 0)
				l_str = l_str.Replace("%", a_subst);

			//------------------------------------------------------------------
			//-- make sure that path ends with a path separator char
			if (!l_str.EndsWith(Path.DirectorySeparatorChar))
				l_str += Path.DirectorySeparatorChar;

			return l_str;
		}

		//----------------------------------------------------------------------
		//-- access routines - GetBooleanOpt
		//----------------------------------------------------------------------
		public bool GetBooleanOpt(string a_opt, bool a_ucase = true)
		{
			return getBooleanOptHelper_(a_opt, false, true, false, a_ucase);
		}
		public bool GetBooleanOpt(string a_opt, bool a_default, bool a_ucase = true)
		{
			return getBooleanOptHelper_(a_opt, a_default, true, false, a_ucase);
		}
		public bool GetBooleanOpt(string a_opt, bool a_default, bool a_true, bool a_false, bool a_ucase = true)
		{
			return getBooleanOptHelper_(a_opt, a_default, a_true, a_false, a_ucase);
		}
		private bool getBooleanOptHelper_(string a_opt, bool a_default, bool a_true, bool a_false, bool a_ucase)
		{
			//------------------------------------------------------------------
			//-- if opt does not exists, or does exists and is empty, then
			//-- return default value
			if (!IsOpt(a_opt))
				return a_default;

			string? l_opt = GetOptValue(a_opt, a_ucase);
			if (l_opt == null || l_opt.Length == 0)
				return a_default;


			//------------------------------------------------------------------
			//-- check if true or false
			l_opt = l_opt.ToUpper();
			if (l_opt == "1" || l_opt == "T" || l_opt == "TRUE" || l_opt == "ON" || l_opt == "Y" || l_opt == "YES")
				return a_true;
			if (l_opt == "0" || l_opt == "F" || l_opt == "FALSE" || l_opt == "OFF" || l_opt == "N" || l_opt == "NO")
				return a_false;

			return a_default;
		}

		//----------------------------------------------------------------------
		//-- access routines - IsOpt
		//----------------------------------------------------------------------
		public bool IsOpt(string a_opt, bool a_ucase = true)
		{
			return IsOpt(a_opt, false, a_ucase);
		}
		public bool IsOpt(string a_opt, bool a_default, bool a_ucase = true)
		{
			return (findOptHelper_(a_opt, a_ucase) != null) ? true : a_default;
		}


/*
		public bool GetDBLogonOpt(string a_optName, out string a_retSrv, out string a_retUid,
								  out string a_retPwd)
		{
			string db = "";
			string type = "";

			return GetDBLogonOpt(a_optName, out a_retSrv, out a_retUid, out a_retPwd, out db, out type, true);
		}
		public bool GetDBLogonOpt(string a_optName, out string a_retSrv, out string a_retUid,
								  out string a_retPwd, out string a_retDB)
		{
			string type = "";

			return GetDBLogonOpt(a_optName, out a_retSrv, out a_retUid, out a_retPwd, out a_retDB, out type, true);
		}
		public bool GetDBLogonOpt(string a_optName, out string a_retSrv, out string a_retUid,
								  out string a_retPwd, bool a_ignoreCase)
		{
			string db = "";
			string type = "";

			return GetDBLogonOpt(a_optName, out a_retSrv, out a_retUid, out a_retPwd, out db, out type, a_ignoreCase);
		}
		public bool GetDBLogonOpt(string a_optName, out string a_retSrv, out string a_retUid, out string a_retPwd,
								  out string a_retDB, bool a_ignoreCase)
		{
			string type = "";

			return GetDBLogonOpt(a_optName, out a_retSrv, out a_retUid, out a_retPwd, out a_retDB, out type, a_ignoreCase);
		}
		public bool GetDBLogonOpt(string a_optName, out string a_retSrv, out string a_retUid,
								  out string a_retPwd, out string a_retDB, out string a_retType, bool a_ignoreCase)
		{
			//-----------------------------------------------------------------------
			//-- default return values to empty string
			a_retSrv = "";
			a_retUid = "";
			a_retPwd = "";
			a_retDB = "";
			a_retType = "";


			//-----------------------------------------------------------------------
			//-- make sure the option exists
			string opt = GetOptValue(a_optName, a_ignoreCase);
			if (opt == null || opt.Length == 0)
				return false;


			//-----------------------------------------------------------------------
			//-- determine type we are working with
			if (opt[0] == '[')
			{
				//-- user specified type, so pull type and valid
				int pos = opt.IndexOf(']');
				if (pos == -1)
					return false;



/*
			//-----------------------------------------------------------------------
			//-- pull the options if following order (all seperate by ','):
			//--	1 - host
			//--	2 - uid
			//--	3 - pwd
			//--	4 - type
			if (!getInfoOptPullData(ref opt, ref a_retHost))
				return false;
			if (!getInfoOptPullData(ref opt, ref a_retUid))
				return false;
			if (!getInfoOptPullData(ref opt, ref a_retPwd))
				return false;
			a_retType = opt;
/
			return true;
		}
*/
/*
		public bool GetFTPInfoOpt(string a_optName, out string a_retHost, out string a_retUid, out string a_retPwd)
		{
			string type = "";
			return GetFTPInfoOpt(a_optName, out a_retHost, out a_retUid, out a_retPwd, out type, true);
		}
		public bool GetFTPInfoOpt(string a_optName, out string a_retHost, out string a_retUid,	
								  out string a_retPwd, out string a_retType)
		{
			return GetFTPInfoOpt(a_optName, out a_retHost, out a_retUid, out a_retPwd, out a_retType, true);
		}
		public bool GetFTPInfoOpt(string a_optName, out string a_retHost, out string a_retUid,
								  out string a_retPwd, out string a_retType, bool a_ignoreCase)
		{
			//-----------------------------------------------------------------------
			//-- default return values to empty string
			a_retHost = "";
			a_retUid = "";
			a_retPwd = "";
			a_retType = "";


			//-----------------------------------------------------------------------
			//-- make sure the option exists
			string opt = GetOptValue(a_optName, a_ignoreCase);
			if (opt == null || opt.Length == 0)
				return false;


			//-----------------------------------------------------------------------
			//-- pull the options if following order (all seperate by ','):
			//--	1 - host
			//--	2 - uid
			//--	3 - pwd
			//--	4 - type
			if (!getInfoOptPullData(ref opt, ref a_retHost))
				return false;
			if (!getInfoOptPullData(ref opt, ref a_retUid))
				return false;
			if (!getInfoOptPullData(ref opt, ref a_retPwd))
				a_retPwd = opt;
			else
				a_retType = opt;

			return true;
		}
*/
/*
		public bool GetWNetInfoOpt(string a_optName, out string a_retServer,
			out string a_retUid, out string a_retPwd)
		{
			return GetWNetInfoOpt(a_optName, out a_retServer, out a_retUid, out a_retPwd, true);
		}
		public bool GetWNetInfoOpt(string a_optName, out string a_retServer,
			out string a_retUid, out string a_retPwd, bool a_ignoreCase)
		{
			//-----------------------------------------------------------------------
			//-- default return values to empty string
			a_retServer =
				a_retUid =
				a_retPwd = "";


			//-----------------------------------------------------------------------
			//-- make sure the option exists
			string opt = GetOptValue(a_optName, a_ignoreCase);
			if (opt == null || opt.Length == 0)
				return false;

			//-----------------------------------------------------------------------
			//-- pull the options if following order (all seperate by ','):
			//--	1 - server
			//--	2 - uid
			//--	3 - pwd
			if (!getInfoOptPullData(ref opt, ref a_retServer))
				return false;
			if (!getInfoOptPullData(ref opt, ref a_retUid))
				return false;
			if (opt.Length == 0)
				return false;
			a_retPwd = opt;

			return true;
		}
*/
/*
		//----------------------------------------------------------------------------------
		//----------------------------------------------------------------------------------
		public void LogOptions()
		{
			//-----------------------------------------------------------------------
			//-- see if anything to output or has user turned off
			if (m_list == null)
				return;
			if (m_list.Count == 0)
				return;

			if (!IsOpt("-cmdlinelog"))
				return;


			//-----------------------------------------------------------------------
			//-- log the list
			System.Console.WriteLine("cmd line options - beg:");
			foreach (COptItem item in m_list)
			{
				System.Console.Write("   " + item.Opt);
				if (item.Val.Length > 0)
					System.Console.Write(" " + item.Val);
				if (item.OptFile.Length > 0)
					System.Console.Write("    >>> " + item.OptFile);
				System.Console.WriteLine();
			}
			System.Console.WriteLine("cmd line options - end:");
		}
*/

		//------------------------------------------------------------------------
		//------------------------------------------------------------------------
		//------------------------------------------------------------------------

/*
		private bool getInfoOptPullData(ref string a_optStr, ref string a_retData)
		{
			int pos;


			if ((pos = a_optStr.IndexOf(',')) == -1)
				return false;

			a_retData = a_optStr.Substring(0, pos);
			a_optStr = a_optStr.Substring(pos + 1);

			return true;
		}
*/
	}
//}