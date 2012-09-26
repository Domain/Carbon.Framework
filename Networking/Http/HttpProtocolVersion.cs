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
	/// Encapsulates a definition of a protocol and version 
	/// </summary>
	[Serializable()]
	public class HttpProtocolVersion
	{		
		protected string _protocol;
		protected string _version;

		public readonly static string VERSION_1_0 = "HTTP/1.0";
		public readonly static string VERSION_1_1 = "HTTP/1.1";

		/// <summary>
		/// Returns a string in the format 'Protocol/Version'
		/// </summary>
		public readonly static string STRING_FORMAT = "{0}/{1}";

		/// <summary>
		/// Intitializes a new instance of the HttpProtocolVersion class
		/// </summary>
		public HttpProtocolVersion() : this(VERSION_1_1)
		{
			
		}

		/// <summary>
		/// Intitializes a new instance of the HttpProtocolVersion class
		/// </summary>
		/// <param name="httpVersion">The version string to be represented</param>
		public HttpProtocolVersion(string value)
		{			
			value = HttpUtils.StripCRLF(value);
			value = HttpUtils.TrimLeadingAndTrailingSpaces(value);			
			string[] parts = value.Split('/');
			this.Protocol = parts[0];
			this.Version = parts[1];
		}

		/// <summary>
		/// Initializes a new instance of the HttpProtocolVersion class
		/// </summary>
		/// <param name="protocol">The protocol in use</param>
		/// <param name="version">The version of the protocol</param>
		public HttpProtocolVersion(string protocol, string version)
		{
			this.Protocol = protocol;
			this.Version = version;
		}

		/// <summary>
		/// Returns the protocol in use
		/// </summary>
		public string Protocol
		{
			get
			{
				return _protocol;
			}
			set
			{
				HttpUtils.ValidateToken(@"Protocol", value);
				value = HttpUtils.TrimLeadingAndTrailingSpaces(value);
				_protocol = value;
			}
		}

		/// <summary>
		/// Returns the version of the protocol
		/// </summary>
		public string Version
		{
			get
			{
				return _version;
			}
			set
			{
				HttpUtils.ValidateToken(@"Version", value);
				value = HttpUtils.TrimLeadingAndTrailingSpaces(value);
				_version = value;
			}
		}

		/// <summary>
		/// Returns a string in the format 'Protocol/Version'
		/// </summary>
		public override string ToString()
		{
			return string.Format(STRING_FORMAT, _protocol, _version);
		}

		/// <summary>
		/// Parses a string in the foramt 'Protocol/Version' into an HttpProtocolVersion instance
		/// </summary>
		/// <param name="value">The string to parse. May contain CRLF.</param>
		/// <returns></returns>
		public static HttpProtocolVersion Parse(string value)
		{
			value = HttpUtils.StripCRLF(value);
			value = HttpUtils.TrimLeadingAndTrailingSpaces(value);			
			
            string[] parts = value.Split('/');
            
            if (parts.Length == 1)
                return null;

			return new HttpProtocolVersion(parts[0], parts[1]);
		}		
	}
}
