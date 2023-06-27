namespace GMMELib.Utils;
//----------------------------------------------------------------------------------------
//	GMMLib.Net.7x for .NET Core 7.0
//	Copyright (c) 2006-2023, GMM Enterprises, LLC.  Licensed under the GMM Software
//	License
//	All rights reserved 
//----------------------------------------------------------------------------------------
//
//	File:	COptItem.cs
//	Author:	David Crickenberger
//
//	Desc:	Handles private class COptItem that is part of CMDLine.
//
//--------------------------------------------------------------------------------

using System;
using System.Collections;
using System.IO;
using System.Text;

public partial class CMDLine
{
	private class COptItem
	{
		private string? m_file;
		private string? m_opt;
		private string? m_optOrig;
		private string? m_tags;
		private string? m_val;
		private string? m_valOrig;

//			public SubCmd? m_subCmd;

		public COptItem()
		{
			m_file = null;
			m_opt = null;
			m_optOrig = null;
			m_tags = null;
			m_val = null;
			m_valOrig = null;
//				m_subCmd = null;
		}
		public COptItem(COptItem a_src)
		{
			m_file = a_src.m_file;
			m_opt = a_src.m_opt;
			m_optOrig = a_src.m_optOrig;
			m_tags = a_src.m_tags;
			m_val = a_src.m_val;
			m_valOrig = a_src.m_valOrig;
//				m_subCmd = a_src.m_subCmd;
		}
		public COptItem(string? a_opt, string? a_optOrig, string? a_val, string? a_valOrig, string? a_file)
		{
			m_file = a_file;
			m_opt = a_opt;
			m_optOrig = a_optOrig;
//				m_tags = a_.m_tags;
			m_val = a_val;
			m_valOrig = a_valOrig;
//				m_subCmd = a_src.m_subCmd;
		}

		public string? File
		{
			get		{ return m_file; }
			set		{ m_file = value; }
		}
		public string? Opt
		{
			get		{ return m_opt; }
			set		{ m_opt = value; }
		}
		public string? OptOrig
		{
			get		{ return m_optOrig; }
			set		{ m_optOrig = value; }
		}
		public string? Val
		{
			get		{ return m_val; }
			set		{ m_val = value; }
		}
		public string? ValOrig
		{
			get		{ return m_valOrig; }
			set		{ m_valOrig = value; }
		}
/*
		public SubCmd SubCmd
		{
			get		{ return m_subCmd; }
			set		{ m_subCmd = value; }
		}
*/
	};
}
