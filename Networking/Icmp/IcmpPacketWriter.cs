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
using System.Net;
using System.Net.Sockets;

namespace Carbon.Networking.Icmp
{
	/// <summary>
	/// Summary description for IcmpPacketWriter.
	/// </summary>
	public class IcmpPacketWriter
	{
		/// <summary>
		/// Initializes a new instance of the IcmpPacketWriter class
		/// </summary>
		public IcmpPacketWriter()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Writes the IcmpPacket to the wire over the specified socket to the specified end point
		/// </summary>
		/// <param name="socket">The socket to write to</param>
		/// <param name="packet">The packet to write</param>
		/// <param name="ep">The end point to write to</param>
		/// <returns></returns>
		public virtual int Write(Socket socket, IcmpPacket packet, EndPoint ep)
		{
			/*
			 * check the parameters
			 * */

			if (socket == null)
				throw new ArgumentNullException("socket");

			if (socket == null)
				throw new ArgumentNullException("packet");

			if (socket == null)
				throw new ArgumentNullException("ep");

			// convert the packet to a byte array
			byte[] bytes = IcmpPacket.GetBytes(packet);

			// send the data using the specified socket, returning the number of bytes sent
			int bytesSent = socket.SendTo(bytes, bytes.Length, SocketFlags.None, ep);

			/*
			 * validate bytes sent
			 * */

			return bytesSent;
		}
	}
}
