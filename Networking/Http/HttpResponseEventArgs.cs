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
	#region HttpResponseEventArgs

	/// <summary>
	/// Defines an EventArgs class for the HttpResponse class
	/// </summary>
	[Serializable()]
	public class HttpResponseEventArgs : HttpMessageEventArgs 
	{
		/// <summary>
		/// Initializes a new instance of the HttpResponseEventArgs class
		/// </summary>
		/// <param name="response">The message response context</param>
		public HttpResponseEventArgs(HttpResponse response) : base((HttpMessage)response)
		{
			
		}

		/// <summary>
		/// Returns the message response context
		/// </summary>
		public HttpResponse Response
		{
			get
			{
				return (HttpResponse)base.Context;
			}
		}
	}

    //public delegate void HttpResponseEventHandler(object sender, HttpResponseEventArgs e);

	#endregion
	
//	#region HttpResponseCancelEventArgs
//
//	/// <summary>
//	/// Defines an EventArgs class for the HttpMessage class that is cancellable
//	/// </summary>
//	public class HttpResponseCancelEventArgs : HttpMessageCancelEventArgs 
//	{
//		/// <summary>
//		/// Initializes a new instance of the HttpMessageCancelEventArgs class
//		/// </summary>
//		/// <param name="message">The message context</param>
//		/// <param name="cancel">A flag that indicates whether this event will be cancelled</param>
//		public HttpResponseCancelEventArgs(HttpResponse response, bool cancel) : base((HttpMessage)response, cancel)
//		{
//			
//		}
//
//		/// <summary>
//		/// Returns the message response context
//		/// </summary>
//		public new HttpResponse Context
//		{
//			get
//			{
//				return (HttpResponse)base.Context;
//			}
//		}
//	}
//
//	public delegate void HttpResponseCancelEventHandler(object sender, HttpResponseCancelEventArgs e);
//
//	#endregion
}
