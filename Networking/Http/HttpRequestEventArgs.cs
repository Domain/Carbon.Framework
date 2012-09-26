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
	#region HttpRequestEventArgs

	/// <summary>
	/// Defines an EventArgs class for the HttpRequest class
	/// </summary>
	[Serializable()]
	public class HttpRequestEventArgs : HttpMessageEventArgs 
	{
		protected HttpResponse _response;

		/// <summary>
		/// Initializes a new instance of the HttpRequestEventArgs class
		/// </summary>
		/// <param name="request">The message request context</param>
		public HttpRequestEventArgs(HttpRequest request) : base((HttpMessage)request)
		{
			
		}

		/// <summary>
		/// Returns the message request context
		/// </summary>
		public HttpRequest Request
		{
			get
			{
				return (HttpRequest)base.Context;
			}
		}

		/// <summary>
		/// Gets or sets the response that will be sent to the user-agent of this request.
		/// </summary>
		public HttpResponse Response
		{
			get
			{
				return _response;
			}
			set
			{
				_response = value;
			}
		}
	}

    //public delegate void HttpRequestEventHandler(object sender, HttpRequestEventArgs e);

	#endregion

	#region HttpRequestCancelEventArgs

	/// <summary>
	/// Defines an EventArgs class containing a HttpRequest and HttpResponse as the context of the event.
	/// </summary>
	[Serializable()]
	public class HttpRequestCancelEventArgs : HttpMessageCancelEventArgs 
	{		
		private HttpResponse _response;

		/// <summary>
        /// Initializes a new instance of the HttpRequestCancelEventArgs class
		/// </summary>
		/// <param name="message">The message context</param>
		/// <param name="cancel">A flag that indicates whether this event will be cancelled</param>
		public HttpRequestCancelEventArgs(HttpRequest request, bool cancel) 
            : base((HttpMessage)request, cancel)
		{
			
		}

        /// <summary>
        /// Initializes a new instance of the HttpRequestCancelEventArgs class.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="cancel"></param>
        public HttpRequestCancelEventArgs(HttpRequest request, HttpResponse response, bool cancel)
            : base((HttpMessage)request, cancel)
        {
            _response = response;
        }

		/// <summary>
		/// Returns the message request context
		/// </summary>
		public HttpRequest Request
		{
			get
			{
				return (HttpRequest)base.Context;
			}
		}

		public override bool Cancel
		{
			get
			{
				return base.Cancel;
			}
			set
			{
				base.Cancel = value;

				// if this event is cancelled, there will be not response sent automatically
				if (_cancel)
					_response = null;
			}
		}

		/// <summary>
		/// Gets or sets the response that will be sent to the user-agent of this request.
		/// </summary>
		public HttpResponse Response
		{
			get
			{
				return _response;
			}
			set
			{
				_response = value;

				// you cannot assign the event an response, and then cancel it
				// the only way to cancel it is to cancel the event and respond manually
				if (_response != null)
					_cancel = false;
			}
		}
	}

    //public delegate void EventHandler<HttpRequestCancelEventArgs>(object sender, HttpRequestCancelEventArgs e);

	#endregion
}
