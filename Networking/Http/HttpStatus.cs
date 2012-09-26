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
using System.Net;

using Carbon.Common;

namespace Carbon.Networking.Http
{
    /// <summary>
	/// Provides a way of combining status-codes with reason-phrases
	/// </summary>
	[Serializable()]
	public class HttpStatus : DisposableObject 
	{
		private HttpStatusCode _code;
		private string _reason;	

		/// <summary>
		/// Returns a string in the format of 'Code SP Reason'
		/// </summary>
		private readonly static string Format = "{0} {1}";

		/// <summary>
		/// Initializes a new instance of the HttpStatus class.
		/// </summary>
		/// <param name="code">The status code of the response.</param>
		/// <param name="reason">The reason phrase of the response.</param>
        public HttpStatus(HttpStatusCode code, string reason)
		{
			_code = code;
			_reason = reason;				
		}

		/// <summary>
		/// Returns the status-code of the response.
		/// </summary>
        public HttpStatusCode Code
		{
			get { return _code; }
			set { _code = value; }
		}

		/// <summary>
		/// Returns the reason-phrase of the response.
		/// </summary>
		public string Reason
		{
			get { return _reason; }
			set { _reason = value; }
		}

		/// <summary>
		/// Returns a string in the format of 'Code SP Reason'
		/// </summary>
		public override string ToString()
		{
			return string.Format(Format, (int)_code, _reason);
		}

		/// <summary>
		/// Parses a string in the format of 'Code SP Reason' into an HttpStatus instance
		/// </summary>
		/// <param name="value">The string to parse. May include the CRLF.</param>
		/// <returns></returns>
		public static HttpStatus Parse(string value)
		{
			value = HttpUtils.StripCRLF(value);
			value = HttpUtils.TrimLeadingAndTrailingSpaces(value);
			
			string code = null;
			string reason = null;
			int indexOfSP = value.IndexOf(' ');

			if (indexOfSP > 0)
			{
				code = value.Substring(0, indexOfSP);
				reason = value.Substring(++indexOfSP);
			}
			else
			{
				System.Diagnostics.Debug.WriteLine(string.Format("Failed to parse the HTTP Status {0}.", value));
				code = value;
			}

            return new HttpStatus((HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), code), reason);
		}
	}
}
