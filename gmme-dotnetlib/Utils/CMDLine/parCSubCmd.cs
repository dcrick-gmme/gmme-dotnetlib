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