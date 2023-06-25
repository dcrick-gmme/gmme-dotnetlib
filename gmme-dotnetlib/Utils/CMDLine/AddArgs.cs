namespace GMMELib.Utils;
//----------------------------------------------------------------------------------------
//	GMMLib.Net.7x for .NET Core 7.0
//	Copyright (c) 2006-2023, GMM Enterprises, LLC.  Licensed under the GMM Software
//	License
//	All rights reserved 
//----------------------------------------------------------------------------------------
//
//	File:	AddArgs.cs
//	Author:	David Crickenberger
//
//	Desc:	Handles methods AddArgsXXX() which is part of CMDLine.
//
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.IO;
using System.Text;

public partial class CMDLine
{
    //--------------------------------------------------------------------------
    //-- AddArgsArray
    //--------------------------------------------------------------------------
    public void AddArgsArray(string[] a_args)
    {
        int argsLen = a_args.GetLength(0);

        string arg;


        //----------------------------------------------------------------------
        //-- initialize array and local variables
        initOptItemList_();

        string? l_opt;
        string? l_val;


        //----------------------------------------------------------------------
        //-- loop thru all arguments and add in opt,val pears
        for (int i = 0; i < argsLen; i++)
        {
            //------------------------------------------------------------------
            //-- see if we have an arg
            arg = a_args[i];
            if (arg[0] == '-' || arg[0] == '/')
            {
                l_opt = arg;
                l_val = null;
                if ((i + 1) < argsLen)
                {
                    if (a_args[i + 1][0] != '-' 
                        && a_args[i + 1][0] != '/'
                        && a_args[i + 1][0] != '@')
                    {
                        //-- we have a value with the option
                        l_val = a_args[i + 1];
                        i++;
                    }
                }


                //-- add item to list
                addItemToList_(l_opt, l_val);
            }
            else if (arg[0] == '@')
                AddArgsFile(subEnv_(arg.Substring(1)));
        }

        m_init = true;
    }


    //--------------------------------------------------------------------------
    //-- AddArgsFile
    //--------------------------------------------------------------------------
    public void AddArgsFile(string? a_file)
    {
        //----------------------------------------------------------------------
        //-- make sure option file exists, then determine length of file and
        //-- allocate space to hold entire file in memory
        if (a_file is null)
            return;
        FileInfo l_fi = new FileInfo(a_file);
        if (!l_fi.Exists)
           throw new System.IO.FileNotFoundException("OPT file could not be found: " + a_file);

        long l_filelen = l_fi.Length;
        byte[] l_optdata = new byte[l_filelen];

        //----------------------------------------------------------------------
        //-- load file into memory
        FileStream l_fs = l_fi.OpenRead();
        int l_bytesread = l_fs.Read(l_optdata, 0, (int)l_filelen);
        l_fs.Close();
        if (l_bytesread != (int)l_filelen)
            throw new System.IO.EndOfStreamException("Unable to load OPT file: " + a_file);

        //----------------------------------------------------------------------
        //-- process each line in the file, by spliting up and calling
        //-- AddArgsLine()
        string l_line;

        int l_pos = 0;
        while (l_pos < l_filelen)
        {
            //------------------------------------------------------------------
            //-- pull a single line and see if its a comment line
            l_line = "";
            while (l_pos < l_filelen && l_optdata[l_pos] != '\r' && l_optdata[l_pos] != '\n')
                l_line += (char)l_optdata[l_pos++];
            while (l_pos < l_filelen && (l_optdata[l_pos] == '\r' || l_optdata[l_pos] == '\n'))
                l_pos++;
            l_line = l_line.Trim();
            if (l_line.Length > 1 && l_line[0] == '#')
                continue;
            if (l_line.Length > 2 && l_line[0] == '/' && l_line[1] == '/')
                continue;


            //-----------------------------------------------------------------------
            //-- process the options
            AddArgsLine(l_line, a_file);
        }
    }


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

        string tmp;

        int i;


        //-- initialize array and item
        initOptItemList_();

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
            }
            else if (tmp[0] == '@')
            {
                string l_fname;

                //-- pull filename
                if ((i = tmp.IndexOf(' ')) == -1)
                {
                    l_fname = tmp.Substring(1);
                    tmp = "";
                }
                else
                {
                    l_fname = tmp.Substring(1, i - 1);
                    tmp = tmp.Remove(0, i);
                    tmp = tmp.TrimStart(null);
                }

                if (l_fname.Length != 0)
                    AddArgsFile(subEnv_(l_fname));
            }
        }

        m_init = true;
    }
}