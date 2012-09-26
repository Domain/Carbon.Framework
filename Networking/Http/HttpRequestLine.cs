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

namespace Carbon.Networking.Http
{
	/// <summary>
	/// Summary description for HttpRequestLine.
	/// </summary>
	[Serializable()]
	public sealed class HttpRequestLine
	{
		private string _method;
        private string _requestUri;
        private HttpProtocolVersion _protocolVersion;

		/// <summary>
		/// Returns a string in the format 'Method SP Request-Uri SP Http-Version CRLF'
		/// </summary>
		public readonly static string STRING_FORMAT = "{0} {1} {2}{3}";

		/// <summary>
		/// Initializes a new instance of the HttpRequestLine class
		/// </summary>
		public HttpRequestLine()
		{
			this.Method = HttpMethods.Get;
			this.RequestUri = "/";
			this.ProtocolVersion = new HttpProtocolVersion();
		}

		/// <summary>
		/// Initializes a new instance of the HttpRequestLine class
		/// </summary>
		/// <param name="method">The method of the request</param>
		/// <param name="requestUri">The request-uri</param>
		/// <param name="protocolVersion">The protocol version</param>
		public HttpRequestLine(string method, string requestUri, string protocolVersion)
		{
			this.Method = method;
			this.RequestUri = requestUri;
			_protocolVersion = new HttpProtocolVersion(protocolVersion);
		}

		/// <summary>
		/// Initializes a new instance of the HttpRequestLine class
		/// </summary>
		/// <param name="method">The method of the request</param>
		/// <param name="requestUri">The request-uri</param>
		/// <param name="protocolVersion">The protocol version</param>
		public HttpRequestLine(string method, string requestUri, HttpProtocolVersion protocolVersion)
		{
			this.Method = method;
			this.RequestUri = requestUri;
			this.ProtocolVersion = protocolVersion;
		}

		/// <summary>
		/// Gets or sets the method contained in this request line
		/// </summary>
		public string Method
		{
			get
			{
				return _method;
			}
			set
			{
				HttpUtils.ValidateToken(@"Method", value);
				value = HttpUtils.TrimLeadingAndTrailingSpaces(value);
				_method = value;
			}
		}

		/// <summary>
		/// Gets or sets the request-uri contained in this request line
		/// </summary>
		public string RequestUri
		{
			get
			{
				return _requestUri;
			}
			set
			{
				HttpUtils.ValidateToken(@"RequestUri", value);
				value = HttpUtils.TrimLeadingAndTrailingSpaces(value);
				_requestUri = value;
			}
		}

		/// <summary>
		/// Gets or sets the protocol version contained in this request line
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
		/// Returns a string in the format 'Method SP Request-Uri SP Http-Version CRLF'
		/// </summary>
		public override string ToString()
		{
			return string.Format(STRING_FORMAT, _method, _requestUri, _protocolVersion, HttpControlChars.CRLF);
		}
		
		/// <summary>
		/// Parses a string in the format 'Method SP Request-Uri SP Http-Version CRLF' into an HttpRequestLine instance
		/// </summary>
		/// <param name="value">The string to parse. May contain CRLF.</param>
		/// <returns></returns>
		public static HttpRequestLine Parse(string value)
		{
			string[] parts = value.Split(' ');
            
            if (parts.Length < 3)
                return null;
            
			HttpProtocolVersion protocolVersion = HttpProtocolVersion.Parse(parts[2]);
            if (protocolVersion == null)
                return null;

			return new HttpRequestLine(parts[0], parts[1], protocolVersion);
		}
	}
}
