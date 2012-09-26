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
	/// Summary description for HttpHeaders.
	/// </summary>
	public class HttpHeaders
	{
		/// <summary>
		/// Headers that apply to request messages.
        /// Headers include Accept, Accept-Encoding, Accept-Language, Authorization, Cookie, Cookie2, Expect, From,
        /// Host, If-Match, If-Modified-Since, If-None-Match, If-Range, If-Unmodified-Since, Max-Forwards,
        /// Proxy-Authorization, Range, Referrer, TE, User-Agent
		/// </summary>
		public static class RequestHeaders
		{
			public readonly static string Accept = "Accept";
			public readonly static string AcceptEncoding = "Accept-Encoding";
			public readonly static string AcceptLanguage = "Accept-Language";
			public readonly static string Authorization	= "Authorization";

            /// <summary>
            /// The Cookie header is an extension header used for client identification and tracking.           
            /// Example: Cookie: ink=IUOK16
            /// </summary>
            public readonly static string Cookie = "Cookie";

            /// <summary>
            /// The Cookie2 header is an extension header used for client identification and tracking.
            /// It is used to identify what version of cookies a requestor understands. 
            /// It is defined in RFC 2965.
            /// </summary>
            public readonly static string Cookie2 = "Cookie2";

			public readonly static string Expect = "Expect";
			public readonly static string From = "From";
			
            /// <summary>
            /// The Host header is used by clients to provide the server with the Internet hostname and 
            /// port number of the machine from which the client wants to make the request. The hostname
            /// and port are those from the URL the client was requesting.
            /// 
            /// The Host header allows servers to differentiate different relative URLs based on the hostname,
            /// giving the server the ability to host several different hostnames on the same machine (i.e., the same IP address).
            /// </summary>
            public readonly static string Host = "Host";

			public readonly static string IfMatch = "If-Match";
			public readonly static string IfModifiedSince = "If-Modified-Since";
			public readonly static string IfNoneMatch = "If-None-Match";
			public readonly static string IfRange = "If-Range";
			public readonly static string IfUnmodifiedSince	= "If-Unmodified-Since";
			public readonly static string MaxForwards = "Max-Forwards";
			public readonly static string ProxyAuthorization = "Proxy-Authorization";
			public readonly static string Range = "Range";
			public readonly static string Referer = "Referer";
			public readonly static string TE = "TE";
			public readonly static string UserAgent = "User-Agent";
		}

		/// <summary>
		/// Headers that apply to response messages.
        /// Headers include Accept-Ranges, Age, ETag, Location, Proxy-Authenticate, Retry-After, Server,
        /// Vary, WWW-Authenticate
		/// </summary>
		public static class ResponseHeaders
		{
			public readonly static string AcceptRanges = "Accept-Ranges";
			public readonly static string Age = "Age";
            public readonly static string SetCookie = "Set-Cookie";
            public readonly static string SetCookie2 = "Set-Cookie2";
			public readonly static string ETag = "ETag";
			public readonly static string Location = "Location";
			public readonly static string ProxyAuthenticate	= "Proxy-Authenticate";
			public readonly static string RetryAfter = "RetryAfter";
			public readonly static string Server = "Server";
			public readonly static string Vary = "Vary";
			public readonly static string WWWAuthenticate = "WWW-Authenticate";
		}

		/// <summary>
		/// Headers that apply to messages in general (i.e, both requests and responses).
        /// Headers include Cache-Control, Connection, Date, Pragma, Proxy-Connection, Trailer, Transfer-Encoding,
        /// Upgrade, Via, Warning, Response-Needed (A Razor|Carbon only extension header)
		/// </summary>
		public static class GeneralHeaders
		{
			public readonly static string CacheControl = "Cache-Control";
			public readonly static string Connection = "Connection";
			public readonly static string Date = "Date";
			public readonly static string Pragma = "Pragma";
            public readonly static string ProxyConnection = "Proxy-Connection";
			public readonly static string Trailer = "Trailer";
			public readonly static string TransferEncoding = "Transfer-Encoding";
			public readonly static string Upgrade = "Upgrade";
			public readonly static string Via = "Via";
			public readonly static string Warning = "Warning";
			public readonly static string ResponseNeeded = "Response-Needed";
		}

		/// <summary>
		/// Headers that apply to message entities (i.e., the message body).
        /// Headers include Allow, Content-Encoding, Content-Language, Content-Length, Content-Location, Content-Range,
        /// Content-Type, Expires, Last-Expired
		/// </summary>
		public static class EntityHeaders
		{
			public readonly static string Allow	= "Allow";
			public readonly static string ContentEncoding = "Content-Encoding";
			public readonly static string ContentLanguage = "Content-Language";
			public readonly static string ContentLength	= "Content-Length";
			public readonly static string ContentLocation = "Content-Location";
			public readonly static string ContentRange = "Content-Range";
			public readonly static string ContentType = "Content-Type";
			public readonly static string Expires = "Expires";
			public readonly static string LastModified = "Last-Modified";
		}
	}
}
