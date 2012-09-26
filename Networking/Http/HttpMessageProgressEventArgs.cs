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

namespace Carbon.Networking.Http
{
	/// <summary>
	/// Summary description for HttpMessageProgressEventArgs.
	/// </summary>
	public sealed class HttpMessageProgressEventArgs : EventArgs 
	{
		private HttpMessage _message;
        private bool _justHeaders;
        private byte[] _bytes;
        private int _totalBytes;
        private object _stateObject;

		/// <summary>
		/// Initializes a new instance of the X class
		/// </summary>
		/// <param name="message">The message being processed</param>
		/// <param name="justHeaders">A flag to indicated that only the message headers have been processed</param>
		/// <param name="bytes">The bytes that were just processed</param>
		/// <param name="totalBytes">The total number of bytes that have been processed</param>
		public HttpMessageProgressEventArgs(HttpMessage message, bool justHeaders, byte[] bytes, int totalBytes)
            : base()
		{
			_message = message;
			_justHeaders = justHeaders;
			_bytes = bytes;
			_totalBytes = totalBytes;
		}

		/// <summary>
		/// Initializes a new instance of the HttpMessageProgressEventArgs class
		/// </summary>
		/// <param name="message">The message being processed</param>
		/// <param name="justHeaders">A flag to indicated that only the message headers have been processed</param>
		/// <param name="bytes">The bytes that were just processed</param>
		/// <param name="totalBytes">The total number of bytes that have been processed</param>
		/// <param name="stateObject">A user defined object that can be used to hold state information about the event</param>
		public HttpMessageProgressEventArgs(HttpMessage message, bool justHeaders, byte[] bytes, int totalBytes, object stateObject)
            : base()
		{
			_message = message;
			_justHeaders = justHeaders;
			_bytes = bytes;
			_totalBytes = totalBytes;
			_stateObject = stateObject;
		}

		/// <summary>
		/// Returns the message that is being received
		/// </summary>
		public HttpMessage Message
		{
			get
			{
				return _message;				
			}
		}

		/// <summary>
		/// Returns a flag that indicates the progress just reflects upon the headers
		/// </summary>
		public bool JustHeaders
		{
			get
			{
				return _justHeaders;
			}
		}

		/// <summary>
		/// Returns the array of bytes that have just been received from the connection, but not yet processed
		/// </summary>
		public byte[] Bytes
		{
			get
			{
				return _bytes;
			}
		}

		/// <summary>
		/// Returns the total number of bytes that have been processed thus far
		/// </summary>
		public int TotalBytes
		{
			get
			{
				return _totalBytes;
			}
		}		

		/// <summary>
		/// Returns a user defined state object that can be used to determine the context of the event
		/// </summary>
		public object StateObject
		{
			get
			{
				return _stateObject;
			}
		}
	}

    //public delegate void EventHandler<HttpMessageProgressEventArgs>(object sender, HttpMessageProgressEventArgs e);
}
