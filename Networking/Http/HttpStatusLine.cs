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

namespace Carbon.Networking.Http
{
	/// <summary>
	/// 
	///     The first line of a Response message is the HttpStatus-Line, consisting
	///		of the protocol version followed by a numeric status code and its
	///		associated textual phrase, with each element separated by SP
	///		characters. No CR or LF is allowed except in the final CRLF sequence.
	///
	///			HttpStatus-Line = HTTP-Version SP HttpStatus-Code SP Reason-Phrase CRLF
	///			
	/// </summary>
	/// 
	[Serializable()]
	public sealed class HttpStatusLine 
	{	
		private HttpProtocolVersion _protocolVersion;
		private HttpStatus _status;

		/// <summary>
		/// Returns a string in the format 'HttpProtocolVersion SP HttpStatus-Code SP Reason-Phrase CRLF'
		/// </summary>
		private const string STRING_FORMAT = "{0} {1}{2}";

		/// <summary>
		/// Initializes a new instance of the HttpStatusLine class
		/// </summary>
		/// <param name="code">The status-code of the response</param>
		/// <param name="reason">The reason-phrase to describe the status-code</param>
        public HttpStatusLine(HttpStatusCode code, string reason)
            : this(new HttpProtocolVersion(), code, reason)
		{
						
		}
		
		/// <summary>
		/// Initializes a new instance of the HttpStatusLine class
		/// </summary>
		/// <param name="protocolVersion">The protocol version in use</param>
		/// <param name="code">The status-code of the response</param>
		/// <param name="reason">The reason-phrase to describe the status-code</param>
		public HttpStatusLine(HttpProtocolVersion protocolVersion, HttpStatusCode code, string reason)
		{
			this.ProtocolVersion = protocolVersion;
			_status = new HttpStatus(code, reason);			
		}

		/// <summary>
		/// Initializes a new instance of the HttpStatusLine class
		/// </summary>
		/// <param name="protocolVersion">The protocol version in use</param>
		/// <param name="status">The status-code and reason-phrase</param>
		public HttpStatusLine(HttpProtocolVersion protocolVersion, HttpStatus status)
		{
			if (protocolVersion == null)
				throw new ArgumentNullException("protocolVersion");

			if (status == null)
				throw new ArgumentNullException("status");

			_protocolVersion = protocolVersion;
			_status = status;
		}

		/// <summary>
		/// Initializes a new instance of the HttpStatusLine class
		/// </summary>
		/// <param name="status"></param>
		public HttpStatusLine(HttpStatus status) : this(new HttpProtocolVersion(), status)
		{

		}

		/// <summary>
		/// Returns the protocol version in use
		/// </summary>
		public HttpProtocolVersion ProtocolVersion
		{
			get
			{
				return _protocolVersion;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("ProtocolVersion");

				_protocolVersion = value;
			}
		}

		/// <summary>
		/// Return the Http status for the response
		/// </summary>
		public HttpStatus Status
		{
			get
			{
				return _status;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("Status");

				_status = value;
			}
		}

		/// <summary>
		/// Returns a string in the format 'Status-Line = HTTP-Version SP Status-Code SP Reason-Phrase CRLF'
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format(STRING_FORMAT, _protocolVersion, _status, HttpControlChars.CRLF);
		}

		/// <summary>
		/// Parses a string in the format 'HTTP-Version SP Status-Code SP Reason-Phrase CRLF' into an HttpStatusLine instance
		/// </summary>
		/// <example>
		/// HTTP/1.1 200 OK\r\n
		/// </example>
		/// <param name="value">The string to parse. May contain CRLF.</param>
		/// <returns></returns>
		public static HttpStatusLine Parse(string value)
		{
            //value = HttpUtils.StripCRLF(value);
            //string[] parts = value.Split(HttpControlChars.SP);

			int firstSpace = value.IndexOf(HttpControlChars.SP, 0);
			string a = value.Substring(0, firstSpace);
			string b = value.Substring(++firstSpace);            
			
			HttpProtocolVersion protocolVersion = HttpProtocolVersion.Parse(a);
			HttpStatus status = HttpStatus.Parse(b);
            if (status == null)
                return null;
			
			return new HttpStatusLine(protocolVersion, status);			
		}
	}	
}
