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

namespace Carbon.Networking
{
	/// <summary>
	/// Defines an EventArgs class used to display information about DNS lookups.
	/// </summary>
	public sealed class AddressResolutionEventArgs : EventArgs
	{
		private readonly string _address;
		private readonly int _port;
		private readonly object _stateObject;

        /// <summary>
        /// Initializes a new instance of the AddressResolutionEventArgs class.
        /// </summary>
        /// <param name="address">The address that is being resolved.</param>
        /// <param name="port">The port that will be used in the connection.</param>
		public AddressResolutionEventArgs(string address, int port) : 
            base()
		{
			_address = address;
			_port = port;
		}

        /// <summary>
        /// Initializes a new instance of the AddressResolutionEventArgs class.
        /// </summary>
        /// <param name="address">The address that is being resolved.</param>
        /// <param name="port">The port that will be used in the connection.</param>
        /// <param name="stateObject">The user defined state object that can be used to determine the context of the event.</param>
		public AddressResolutionEventArgs(string address, int port, object stateObject) :
            this(address, port)
		{
			_stateObject = stateObject;
		}

		/// <summary>
		/// Returns address being resolved (one of the following, IPv4 dotted quad, IPv6 colon separated hex, or Dns name).
		/// </summary>
		public string Address
		{
			get
			{
				return _address;
			}
		}
		
		/// <summary>
		/// Returns the port number that will be used in the connection.
		/// </summary>
		public int Port
		{
			get
			{
				return _port;
			}
		}

		/// <summary>
		/// Returns a user defined state object that can be used to determine the context of the event.
		/// </summary>
		public object StateObject
		{
			get
			{
				return _stateObject;
			}
		}
	}
}
