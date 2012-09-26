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
	/// Summary description for IcmpPacketReader.
	/// </summary>
	public class IcmpPacketReader
	{
		/// <summary>
		/// Initializes a new instance of the IcmpPacketReader class
		/// </summary>
		public IcmpPacketReader()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Reads an IcmpPacket from the wire using the specified socket from the specified end point
		/// </summary>
		/// <param name="socket">The socket to read</param>
		/// <param name="packet">The packet read</param>
		/// <param name="ep">The end point read from</param>
		/// <returns></returns>
		public virtual bool Read(Socket socket, EndPoint ep, int timeout, out IcmpPacket packet, out int bytesReceived)
		{
			const int MAX_PATH = 256;
			packet = null;
			bytesReceived = 0;

			/*
			 * check the parameters
			 * */

			if (socket == null)
				throw new ArgumentNullException("socket");
			
			if (socket == null)
				throw new ArgumentNullException("ep");		
						
			// see if any data is readable on the socket
			bool success = socket.Poll(timeout * 1000, SelectMode.SelectRead);

			// if there is data waiting to be read
			if (success)
			{
				// prepare to receive data
				byte[] bytes = new byte[MAX_PATH];
				
				bytesReceived = socket.ReceiveFrom(bytes, bytes.Length, SocketFlags.None, ref ep);

				/*
				 * convert the bytes to an icmp packet
				 * */
//				packet = IcmpPacket.FromBytes(bytes);
			}						

			return success;
		}
	}
}
