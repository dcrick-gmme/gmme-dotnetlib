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
//using System.Diagnostics.CodeAnalysis.NotNullWhenAttribute;
using System.IO;
using System.Text;

//namespace GMMLib
//{
	/// <summary>
	/// This a command line processor class for processing command line options.
	/// </summary>
	public partial class CMDLine
	{
//		private ArrayList? m_list = null;
		private SortedList<string, COptItem>? m_list = null;

		private bool m_init = false;


		//------------------------------------------------------------------------
		//-- initialize the internal optitem list
		private void initOptItemList_()
		{
			if (m_list != null)
				return;

//			m_list = new ArrayList();
			m_list = new SortedList<string, COptItem>();
		}
//SortedList<int, string> numberNames = new SortedList<int, string>();


		//------------------------------------------------------------------------
		//------------------------------------------------------------------------
		//------------------------------------------------------------------------
		/// <summary>
		///	ctor
		/// </summary>
		public CMDLine()
		{
			Console.WriteLine("We are in CMDLine()!");
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
		public CMDLine(string a_line)
		{
			AddArgsLine(a_line);
		}
/*
		public CMDLine(string[] a_args)
		{
			AddArgsArray(a_args);
		}
*/

		public bool IsInit
		{
			get		{ return m_init; }
		}


		//----------------------------------------------------------------------------------
		//----------------------------------------------------------------------------------
		//-- Dump
		//----------------------------------------------------------------------------------
		//----------------------------------------------------------------------------------
		public void Dump()
		{
			//-----------------------------------------------------------------------
			//-- see if anything to dump
	        if (!m_init || m_list == null || (m_list != null && m_list.Count == 0))
			{
				Console.WriteLine("CMDine: Dump - nothing exists...");
            	return;
			}

			//-----------------------------------------------------------------------
			//-- dump the list
			System.Console.WriteLine("CMDLine: Dump - beg:");
			if (m_list != null)
			{
				foreach(var l_kvp in m_list)
				{
					//-- determine output
					string l_opt = l_kvp.Key;
					string? l_val = l_kvp.Value.Val;
					if (l_val == null)
						l_val = "<null>";
					else if (l_val.Length == 0)
						l_val = "<empty>";

					//-- build output string
					StringBuilder l_dbgOut = new StringBuilder("   ");
					l_dbgOut.Append(l_opt).Append(" == [" + l_val + "]");
					System.Console.WriteLine(l_dbgOut);
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

/*
		//-- add Args members
		public void AddArgsArray(string[] a_args)
		{
			int argsLen = a_args.GetLength(0);

			string arg;

			COptItem item = new COptItem();


			//-- initialize array
			initOptItemList_();


			//-- loop thru all arguments and add in opt,val pears
			for (int i = 0; i < argsLen; i++)
			{
				//-- see if we have an arg
				arg = a_args[i];
				if (arg[0] == '-' || arg[0] == '/')
				{
					item.Opt = arg;
					item.Val = "";
					item.OptFile = "";
					item.SubCmd = null;

					if ((i + 1) < argsLen)
					{
						if (a_args[i + 1][0] != '-' 
							&& a_args[i + 1][0] != '/'
							&& a_args[i + 1][0] != '@')
						{
							//-- we have a value with the option
							item.Val = a_args[i + 1];
							i++;
						}
					}


					//-- add item to list
					item.Val = subEnv(item.Val);
					AddItemToList_(item);
				}
				else if (arg[0] == '@')
					AddArgsFile(subEnv(arg.Substring(1)));
			}

			m_init = true;
		}

		public void AddArgsFile(string a_file)
		{
			//-----------------------------------------------------------------
			//-- make sure option file exists, then determine length of file
			//-- and allocate space to hold entire file in memory
			FileInfo fi = new FileInfo(a_file);
			if (!fi.Exists)
				throw new System.IO.FileNotFoundException("OPT file could not be found: " + a_file);

			long filelen = fi.Length;
			byte[] optdata = new byte[filelen];

			FileStream fs = fi.OpenRead();
			int bytesread = fs.Read(optdata, 0, (int)filelen);
			fs.Close();
			if (bytesread != (int)filelen)
				throw new System.IO.EndOfStreamException("Unable to load OPT file: " + a_file);


			//--------------------------------------------------------------------------------
			//-- process each line in the file
			string line;

			int pos = 0;
			while (pos < filelen)
			{
				//-----------------------------------------------------------------------
				//-- pull a single line and see if its a comment line
				line = "";
				while (pos < filelen && optdata[pos] != '\r' && optdata[pos] != '\n')
					line += (char)optdata[pos++];
				while (pos < filelen && (optdata[pos] == '\r' || optdata[pos] == '\n'))
					pos++;
				line = line.Trim();
				if (line.Length > 1 && line[0] == '#')
					continue;
				if (line.Length > 2 && line[0] == '/' && line[1] == '/')
					continue;


				//-----------------------------------------------------------------------
				//-- process the options
				AddArgsLine(line, a_file);
			}
		}
*/
		//----------------------------------------------------------------------------------
		//-- AddArgsLine
		//----------------------------------------------------------------------------------
		public void AddArgsLine(string a_line)
		{
			AddArgsLine(a_line, null);
		}
		public void AddArgsLine(StringBuilder a_line)
		{
			AddArgsLine(a_line.ToString());
		}
		public void AddArgsLine(string a_line, string? a_file)
		{
			char endChr;

//			string fname;
			string tmp;

			int i;

//			COptItem item = new COptItem();


			//-- initialize array and item
			initOptItemList_();

//			item.OptFile = a_file;

			string? l_opt = null;
			string? l_val = null;


			//-- process the line
			tmp = a_line;
			while (tmp.Length > 0)
			{
				tmp = tmp.TrimStart(null);
				if (tmp.Length == 0)
					break;
				if (tmp[0] == '-' || tmp[0] == '/')
				{
					//-- find end of string
					l_val = "";
					if ((i = tmp.IndexOf(" ")) == -1)
					{
						l_opt = tmp;
						tmp = "";
					}
					else
					{
						//-- pull option
						l_opt = tmp.Substring(0, i);
						tmp = tmp.Remove(0, i);
						tmp = tmp.TrimStart(null);
						if (tmp.Length > 0)
						{
							if (tmp[0] != '-' && tmp[0] != '/' && tmp[0] != '@')
							{
								//-- see if double or single quotes are being used
								if (tmp[0] == '"' || tmp[0] == '\'')
								{
									endChr = tmp[0];
									tmp = tmp.Remove(0, 1);
								}
								else
									endChr = ' ';


								//-- find end of val
								if ((i = tmp.IndexOf(endChr)) == -1)
								{
									//-- remaining part of string is value
									l_val = tmp;
									tmp = "";
								}
								else
								{
									//-- pull value and remove from tmp
									l_val = tmp.Substring(0, i);
									tmp = tmp.Remove(0, i);
									if (endChr != ' ')
										tmp = tmp.Remove(0, 1);
									tmp = tmp.TrimStart(null);
								}
							}
						}
					}

					//----------------------------------------------------------
					//-- add item to list
					addItemToList_(l_opt, l_val, a_file);
//					addItemToList_(l_opt, l_opt, l_val, l_val, a_file);
//					addItemToList_(l_opt, subEnv_(l_opt), l_val, subEnv_(l_val), a_file);
//					COptItem l_item = new COptItem();
/*
//					item.SubCmd = null;
					item.Opt = subEnv_(item.OptOrig);
					if (item.Opt is not null)
						item.Opt = item.Opt.ToUpper();
					if (item.ValOrig is not null)
						item.Val = subEnv_(item.ValOrig);

					addItemToList_(item);
*/
				}
/*
				else if (tmp[0] == '@')
				{
					//-- pull filename
					if ((i = tmp.IndexOf(' ')) == -1)
					{
						fname = tmp.Substring(1);
						tmp = "";
					}
					else
					{
						fname = tmp.Substring(1, i - 1);
						tmp = tmp.Remove(0, i);
						tmp = tmp.TrimStart(null);
					}

					if (fname.Length != 0)
						AddArgsFile(subEnv(fname));
				}
*/
			}

			m_init = true;
		}
//		private void addItemToList_(string? a_opt, string a_optOrig, string? a_val, string? a_valOrig, string? a_file)
		private void addItemToList_(string? a_opt, string? a_val, string? a_file)
		{
			//------------------------------------------------------------------
			//-- prepare a_opt
			if (a_opt is null)
				return;
			string? l_opt = null;
			l_opt = a_opt.ToUpper();
			l_opt = subEnv_(l_opt);
			if (l_opt is not null)
				l_opt = l_opt.ToUpper();

			//------------------------------------------------------------------
			//-- prepare a_val
			string ?l_val = subEnv_(a_val);


			//-- add item to m_list
			COptItem l_item = new COptItem(l_opt, a_opt, l_val, a_val, a_file);
			if (m_list is not null && l_opt is not null)
				m_list[l_opt] = l_item;
		}

		private string? subEnv_(string? a_str)
		{
			int p1;
			int p2;

			string env;
			string? envStr;


			//-----------------------------------------------------------------------
			//-- see if we have an environment
			if (a_str == null || a_str.Length == 0)
				return a_str;


			//-----------------------------------------------------------------------
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



		//----------------------------------------------------------------------------------
		//----------------------------------------------------------------------------------
		//-- access routines
		//----------------------------------------------------------------------------------
		//----------------------------------------------------------------------------------
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

		public string GetOptValue(string a_optName)
		{
			return GetOptValue(a_optName, true);
		}
		public string GetOptValue(string a_optName, bool a_ignoreCase)
		{
			if (!m_init)
				return null;

			return getOptValueHelper(a_optName, a_ignoreCase);
		}

		public string GetOptValueDef(string a_optName, string a_defValue)
		{
			return GetOptValueDef(a_optName, a_defValue, true);
		}
		public string GetOptValueDef(string a_optName, string a_defValue, bool a_ignoreCase)
		{
			if (!m_init)
				return a_defValue;

			string val = getOptValueHelper(a_optName, a_ignoreCase);
			if (val == null || val.Length == 0)
				return a_defValue;

			return val;
		}

		public string GetPathOpt(string a_optName)
		{
			return GetPathOpt(a_optName, null, null, true);
		}
		public string GetPathOpt(string a_optName, string a_defValue)
		{
			return GetPathOpt(a_optName, a_defValue, null, true);
		}
		public string GetPathOpt(string a_optName, string? a_defValue, string? a_subValue)
		{
			return GetPathOpt(a_optName, a_defValue, a_subValue, true);
		}
		public string GetPathOpt(string a_optName, string? a_defValue, string? a_subValue, bool a_ignoreCase)
		{
			//-------------------------------------------------------------------
			//-- set default value and see if option exists
			string str = GetOptValueDef(a_optName, a_defValue, a_ignoreCase);
			if (str == null || str.Length == 0)
				return "";


			//-------------------------------------------------------------------
			//-- see if substitution should be done, if '%' exists then replace
			//-- all with value of <a_subValue>
			if (a_subValue != null && a_subValue.Length > 0)
				str = str.Replace("%", a_subValue);


			//-------------------------------------------------------------------
			//-- make sure that path ends with a '\'
			if (!str.EndsWith("\\"))
				str += '\\';

			return str;
		}
*/
/*
		public SubCmd GetSubCmds(string a_optName)
		{
			return GetSubCmds(a_optName, ',', true);
		}
		public SubCmd GetSubCmds(string a_optName, char a_sep)
		{
			return GetSubCmds(a_optName, a_sep, true);
		}
		public SubCmd GetSubCmds(string a_optName, bool a_ignoreCase)
		{
			return GetSubCmds(a_optName, ',', a_ignoreCase);
		}
		public SubCmd GetSubCmds(string a_optName, char a_sep, bool a_ignoreCase)
		{
			//-----------------------------------------------------------------------
			//-- make sure the option exists
			COptItem item = findOptHelper(a_optName, a_ignoreCase);
			if (item == null)
				return null;


			//-----------------------------------------------------------------------
			//-- see if this option has been sent into sub commands
			if (item.Val.Length == 0)
				return null;
			if (item.SubCmd != null)
				return item.SubCmd;


			//-----------------------------------------------------------------------
			//-- see if we have sub commands
			item.SubCmd = new SubCmd(item.Val, a_sep);

			return item.SubCmd;
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

		public bool GetYesNoOpt(string a_optName, bool a_defValue)
		{
			return GetYesNoOpt(a_optName, a_defValue, true);
		}
		public bool GetYesNoOpt(string a_optName, bool a_defValue, bool a_ignoreCase)
		{
			//-----------------------------------------------------------------------
			//-- if opt does not exists, or does exists and is empty, then return
			//-- default value
			//-----------------------------------------------------------------------
			if (!IsOpt(a_optName))
				return a_defValue;

			string l_opt = GetOptValue(a_optName, a_ignoreCase);
			if (l_opt == null || l_opt.Length == 0)
				return a_defValue;


			//-----------------------------------------------------------------------
			//-- check if true or false
			//-----------------------------------------------------------------------
			l_opt = l_opt.ToUpper();
			if (l_opt == "1" || l_opt == "T" || l_opt == "TRUE" || l_opt == "ON" || l_opt == "Y" || l_opt == "YES")
				return true;
			if (l_opt == "0" || l_opt == "F" || l_opt == "FALSE" || l_opt == "OFF" || l_opt == "N" || l_opt == "NO")
				return false;

			return a_defValue;
		}

		public bool IsOpt(string a_optName)
		{
			return IsOpt(a_optName, true);
		}
		public bool IsOpt(string a_optName, bool a_ignoreCase)
		{
			if (!m_init)
				return false;

			return findOptHelper(a_optName, a_ignoreCase) != null;
		}


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
		private COptItem? xFindOptHelper(string? a_optName, bool a_ignoreCase)
		{
			//-- make sure list is not empty
			if (m_list?.Count == 0)
				return null;

			//-- determine compare function and search for option
//			foreach (COptItem item in m_list)
			foreach(var l_kvp in m_list)
			{
				if (string.Compare(a_optName, item.Opt, a_ignoreCase) == 0)
					return item;
			}

			return null;
		}
*/

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

		private string getOptValueHelper(string a_optName, bool a_ignoreCase)
		{
			//-- look for the option
			COptItem item = findOptHelper(a_optName, a_ignoreCase);
			if (item == null)
				return null;

			if (item.Val.Length == 0)
				return null;

			return item.Val;
		}
*/
/*
		//------------------------------------------------------------------------
		//------------------------------------------------------------------------
		//------------------------------------------------------------------------
		//------------------------------------------------------------------------
		public class SubCmd
		{
			private class CSubCmdOptItem
			{
				private string opt;
				private string val;

				public CSubCmdOptItem()
				{
					opt = "";
					val = "";
				}
				public CSubCmdOptItem(CSubCmdOptItem a_src)
				{
					opt = a_src.opt;
					val = a_src.val;
				}
				public string Opt
				{
					get		{ return opt; }
					set		{ opt = value; }
				}
				public string Val
				{
					get		{ return val; }
					set		{ val = value; }
				}
			};

			private ArrayList m_list = null;
			private char m_sep = (char)0x00;


			//--------------------------------------------------------------------
			//--------------------------------------------------------------------
			public SubCmd()
			{
			}
			public SubCmd(string a_cmds)
			{
				addCmds(a_cmds, ',');
			}
			public SubCmd(string a_cmds, char a_sep)
			{
				addCmds(a_cmds, a_sep);
			}


			//--------------------------------------------------------------------
			//--------------------------------------------------------------------
			public string GetOptValue(string a_optName)
			{
				return GetOptValue(a_optName, true);
			}
			public string GetOptValue(string a_optName, bool a_ignoreCase)
			{
				//-- look for the option
				CSubCmdOptItem item = findOptHelper(a_optName, a_ignoreCase);
				if (item == null)
					return null;
				if (item.Val.Length == 0)
					return null;

				return item.Val;
			}

			public bool HasCmds
			{
				get		{ return m_list.Count > 0; }
			}

			public bool IsOpt(string a_optName)
			{
				return IsOpt(a_optName, true);
			}
			public bool IsOpt(string a_optName, bool a_ignoreCase)
			{
				return findOptHelper(a_optName, a_ignoreCase) != null;
			}

			public char Sep
			{
				get		{ return m_sep; }
			}

			//--------------------------------------------------------------------
			//--------------------------------------------------------------------
			//--------------------------------------------------------------------
			private void addCmds(string a_cmds, char a_sep)
			{
				int pos;


				//----------------------------------------------------------------
				//- save and initialize array
				m_sep = a_sep;
				if (m_list == null)
					m_list = new ArrayList();


				//----------------------------------------------------------------
				//-- split the <a_cmds> up by <a_sep>, first seeing if it starts
				//-- with single or double quotes
				string cmds = a_cmds;

				while (cmds.Length > 0 && (cmds[0] == '\"' || cmds[0] == '\''))
					cmds = cmds.TrimStart(cmds[0]);
				while (cmds.Length > 0 && (cmds.EndsWith("\"") || cmds.EndsWith("\'")))
					cmds = cmds.TrimEnd(cmds[cmds.Length - 1]);


				//----------------------------------------------------------------
				//-- extract sub commands
				string subCmd;
				CSubCmdOptItem item = new CSubCmdOptItem();

				while (cmds.Length > 0)
				{
					//-- find end of subcmd and remove
					if ((pos = cmds.IndexOf(a_sep)) == -1)
					{
						subCmd = cmds;
						cmds = "";
					}
					else
					{
						subCmd = cmds.Substring(0, pos);
						cmds = cmds.Substring(pos + 1);
					}


					//-- see if subcmd has = sign
					if ((pos = subCmd.IndexOf('=')) == -1)
					{
						item.Opt = subCmd;
						item.Val = "";
					}
					else
					{
						item.Opt = subCmd.Substring(0, pos);
						item.Val = subCmd.Substring(pos + 1);
					}


					//-- insert the item
					m_list.Add(new CSubCmdOptItem(item));
				}
			}

			private CSubCmdOptItem findOptHelper(string a_optName, bool a_ignoreCase)
			{
				//-- make sure list is not empty
				if (m_list.Count == 0)
					return null;

				//-- determine compare function and search for option
				foreach (CSubCmdOptItem item in m_list)
				{
					if (string.Compare(a_optName, item.Opt, a_ignoreCase) == 0)
						return item;
				}

				return null;
			}
		}
*/
	}
//}