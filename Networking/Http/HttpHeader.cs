//	============================================================================
//
//  .,-:::::   :::.    :::::::..   :::::::.      ...   :::.    :::.
//	,;;;'````'   ;;`;;   ;;;;``;;;;   ;;;'';;'  .;;;;;;;.`;;;;,  `;;;
//	[[[         ,[[ '[[,  [[[,/[[['   [[[__[[\.,[[     \[[,[[[[[. '[[
//	$$$        c$$$cc$$$c $$$$$$c     $$""""Y$$$$$,     $$$$$$ "Y$c$$
//	`88bo,__,o, 888   888,888b "88bo,_88o,,od8P"888,_ _,88P888    Y88
//	"YUMMMMMP"YMM   ""` MMMM   "W" ""YUMMMP"   "YMMMMMP" MMM     YM
//
//	============================================================================
//
//	This file is a part of the Carbon Framework.
//
//	Copyright (C) 2005 Mark (Code6) Belles 
//
//	This library is free software; you can redistribute it and/or
//	modify it under the terms of the GNU Lesser General Public
//	License as published by the Free Software Foundation; either
//	version 2.1 of the License, or (at your option) any later version.
//
//	This library is distributed in the hope that it will be useful,
//	but WITHOUT ANY WARRANTY; without even the implied warranty of
//	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//	Lesser General Public License for more details.
//
//	You should have received a copy of the GNU Lesser General Public
//	License along with this library; if not, write to the Free Software
//	Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//
//	============================================================================

using System;
using System.Diagnostics;
using System.Collections;

using Carbon.Common;

namespace Carbon.Networking.Http
{
	/// <summary>
	/// Summary description for HttpHeader.
	/// </summary>
	[Serializable()]
	public sealed class HttpHeader : DisposableObject 
	{		
		private string _name;
		private string _value;
		        
		/// <summary>
		/// Returns a string in the format 'message-header = token'
		/// </summary>
		private readonly static string STRING_FORMAT = "{0}: {1}{2}";

		/// <summary>
		/// Initializes a new instance of the HttpHeader class
		/// </summary>
		/// <param name="name">The name of the header</param>
		/// <param name="value">The value of the header</param>
		public HttpHeader(string name, string value)
		{			
			this.Name = name;
			this.Value = value;
		}

		/// <summary>
		/// Gets or sets the name of the header
		/// </summary>
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				HttpUtils.ValidateToken("Name", value);
				value = HttpUtils.TrimLeadingAndTrailingSpaces(value);
				_name = value;
			}
		}

		/// <summary>
		/// Gets or sets the value of the header
		/// </summary>
		public string Value
		{
			get
			{
				return _value;
			}
			set
			{
				HttpUtils.ValidateToken("Value", value);
				value = HttpUtils.TrimLeadingAndTrailingSpaces(value);
				_value = value;
			}
		}

		/// <summary>
		/// Determines if this is a known header
		/// </summary>
		public bool IsKnownHeader
		{
			get
			{
				return (System.Web.HttpWorkerRequest.GetKnownRequestHeaderIndex(_name) != -1);
			}
		}

		/// <summary>
		/// Returns the header as an array of strings like {HeaderName, HeaderValue}
		/// </summary>
		/// <returns></returns>
		public string[] ToArray()
		{
			return new string[] {_name, _value};
		}

		/// <summary>
		/// Returns a string in the format 'message-header : token'
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format(STRING_FORMAT, _name, _value, HttpControlChars.CRLF);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static HttpHeader Parse(string value)
		{
            // strip the crlf if it's there
            value = HttpUtils.StripCRLF(value);

            // split it on the header token
            int indexOfDelimiter = value.IndexOf(':');
            string headerName = value.Substring(0, indexOfDelimiter);
            string headerValue = value.Substring(++indexOfDelimiter);

            // trim the leading and trailing spaces from the fields
            headerName = HttpUtils.TrimLeadingAndTrailingSpaces(headerName);
            headerValue = HttpUtils.TrimLeadingAndTrailingSpaces(headerValue);

            // return a new header
            return new HttpHeader(headerName, headerValue);
		}
	}
}
