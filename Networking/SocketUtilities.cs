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
using System.Threading;
using System.IO;

using Carbon.Common;

namespace Carbon.Networking
{
	/// <summary>
	/// Defines utility functions for common methods performed with sockets.
	/// </summary>
	public static class SocketUtilities
	{
        private static IPAddressRegex _ipRegex;

        static SocketUtilities()
        {
            _ipRegex = new IPAddressRegex();
        }

		/// <summary>
		/// Converts the specified bytes to an Int16. Byter order is considered.
		/// </summary>
		/// <param name="byteOrder">The byte order to use during the conversion.</param>
		/// <param name="bytes">The source byte array from which the bytes will be taken for conversion.</param>
		/// <param name="srcOffset">The offset into the source array where the bytes will be taken.</param>
		/// <returns></returns>
		public static short ToInt16(ByteOrder byteOrder, byte[] bytes, int srcOffset)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}

			short value = BitConverter.ToInt16(bytes, srcOffset);

			if (byteOrder == ByteOrder.BigEndian)
			{
				return IPAddress.NetworkToHostOrder(value);
			}

			return value;
		}

		/// <summary>
		/// Converts the specified bytes to an Int32. Byter order is considered.
		/// </summary>
		/// <param name="byteOrder">The byte order to use during the conversion.</param>
		/// <param name="bytes">The source byte array from which the bytes will be taken for conversion.</param>
		/// <param name="srcOffset">The offset into the source array where the bytes will be taken.</param>
		/// <returns></returns>
		public static int ToInt32(ByteOrder byteOrder, byte[] bytes, int srcOffset)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}

			int value = BitConverter.ToInt32(bytes, srcOffset);

			if (byteOrder == ByteOrder.BigEndian)
			{
				return IPAddress.NetworkToHostOrder(value);
			}

			return value;
		}

		/// <summary>
		/// Converts the specified bytes to an Int64. Byter order is considered.
		/// </summary>
		/// <param name="byteOrder">The byte order to use during the conversion.</param>
		/// <param name="bytes">The source byte array from which the bytes will be taken for conversion.</param>
		/// <param name="srcOffset">The offset into the source array where the bytes will be taken.</param>
		/// <returns></returns>
		public static long ToInt64(ByteOrder byteOrder, byte[] bytes, int srcOffset)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}

			long value = BitConverter.ToInt64(bytes, srcOffset);

			if (byteOrder == ByteOrder.BigEndian)
			{
				return IPAddress.NetworkToHostOrder(value);
			}	
		
			return value;
		}		

		/// <summary>
		/// Converts the specified bytes to a Guid. Byter order is considered.
		/// </summary>
		/// <param name="byteOrder">The byte order to use during the conversion.</param>
		/// <param name="bytes">The source byte array from which the bytes will be taken for conversion.</param>
		/// <param name="srcOffset">The offset into the source array where the bytes will be taken.</param>
		/// <returns></returns>
		public static Guid ToGuid(ByteOrder byteOrder, byte[] bytes, int srcOffset)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}

			int  segmentA = BitConverter.ToInt32(bytes, srcOffset);
			short segmentB = BitConverter.ToInt16(bytes, srcOffset + 4);
			short segmentC = BitConverter.ToInt16(bytes, srcOffset + 6);

			if (byteOrder == ByteOrder.BigEndian)
			{	
				segmentA = IPAddress.NetworkToHostOrder(segmentA);
				segmentB = IPAddress.NetworkToHostOrder(segmentB);
				segmentC = IPAddress.NetworkToHostOrder(segmentC);
			}
			
			byte[] resultBytes = new byte[16];
			
			// copy the bytes around to recreate the Guid
			Buffer.BlockCopy(BitConverter.GetBytes(segmentA), 0, resultBytes, 0, 4); 
			Buffer.BlockCopy(BitConverter.GetBytes(segmentB), 0, resultBytes, 4, 2); 
			Buffer.BlockCopy(BitConverter.GetBytes(segmentC), 0, resultBytes, 6, 2); 
            Buffer.BlockCopy(bytes, srcOffset + 8, resultBytes, 8, 8); 

			return new Guid(resultBytes);
		}		

		/// <summary>
		/// Converts the specified bytes to a Guid. Byte order is considered.
		/// </summary>
		/// <param name="byteOrder">The byte order to use during the conversion.</param>
		/// <param name="bytes">The source byte array from which the bytes will be taken for conversion.</param>
		/// <param name="srcOffset">The offset into the source array where the bytes will be taken.</param>
		/// <param name="numElements">The number of elements that should be returned in the final int array.</param>
		/// <returns></returns>
		public static int[] ToInt32Array(ByteOrder byteOrder, byte[] bytes, int srcOffset, int numElements)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}

			int[] values = new int[numElements];
			
			// this will consume 4 * N bytes
			for (int i = 0; i < numElements; i++)
			{
				values[i] = SocketUtilities.ToInt32(byteOrder, bytes, srcOffset + 4 * i);
			}

			return values;
		}

		/// <summary>
		/// Converts the specified Int16 into a byte array. Byter order is considered.
		/// </summary>
		/// <param name="byteOrder">The byte order to use during the conversion.</param>
		/// <param name="value">The short to convert.</param>
		/// <returns></returns>
		public static byte[] GetBytes(ByteOrder byteOrder, short value)
		{
			if (byteOrder == ByteOrder.BigEndian)
			{
				value = IPAddress.HostToNetworkOrder(value);
			}

			return BitConverter.GetBytes(value);
		}

		/// <summary>
		/// Converts the specified Int32 into a byte array. Byter order is considered.
		/// </summary>
		/// <param name="byteOrder">The byte order to use during the conversion.</param>
		/// <param name="value">The int to convert.</param>
		/// <returns></returns>
		public static byte[] GetBytes(ByteOrder byteOrder, int value)
		{
			if (byteOrder == ByteOrder.BigEndian)
			{
				value = IPAddress.HostToNetworkOrder(value);
			}

			return BitConverter.GetBytes(value);
		}

		/// <summary>
		/// Converts the specified Int64 into a byte array. Byter order is considered.
		/// </summary>
		/// <param name="byteOrder">The byte order to use during the conversion.</param>
		/// <param name="value">The long to convert.</param>
		/// <returns></returns>
		public static byte[] GetBytes(ByteOrder byteOrder, long value)
		{
			if (byteOrder == ByteOrder.BigEndian)
			{
				value = IPAddress.HostToNetworkOrder(value);
			}

			return BitConverter.GetBytes(value);
		}

		/// <summary>
		/// Converts the specified Guid into a byte array. Byter order is considered.
		/// </summary>
		/// <param name="byteOrder">The byte order to use during the conversion.</param>
		/// <param name="value">The Guid to convert.</param>
		/// <returns></returns>
		public static byte[] GetBytes(ByteOrder byteOrder, Guid value)
		{
			byte[] bytes = value.ToByteArray();

			if (byteOrder == ByteOrder.BigEndian)
			{
				int  segmentA = BitConverter.ToInt32(bytes, 0);
				short segmentB = BitConverter.ToInt16(bytes, 4);
				short segmentC = BitConverter.ToInt16(bytes, 6);

				segmentA = IPAddress.HostToNetworkOrder(segmentA);
				segmentB = IPAddress.HostToNetworkOrder(segmentB);
				segmentC = IPAddress.HostToNetworkOrder(segmentC);

				Buffer.BlockCopy(BitConverter.GetBytes(segmentA), 0, bytes, 0, 4);
				Buffer.BlockCopy(BitConverter.GetBytes(segmentB), 0, bytes, 4, 2);
				Buffer.BlockCopy(BitConverter.GetBytes(segmentC), 0, bytes, 6, 2);
			}

			return bytes;
		}

		/// <summary>
		/// Returns a byte array containing 4 * N bytes where N is the number of elements in the integer array.
		/// </summary>
		/// <param name="byteOrder">The byte order to use during the conversion.</param>
		/// <param name="values">The array of integers that will be used to create the byte array.</param>
		/// <returns></returns>
		public static byte[] GetBytes(ByteOrder byteOrder, int[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}

			int offset = 0;
			byte[] resultBuffer = new byte[4 * values.Length];

			foreach (int value in values)
			{
				int length = value;

				if (byteOrder == ByteOrder.BigEndian)
				{
					length = IPAddress.HostToNetworkOrder(length);
				}
				byte[] buffer = BitConverter.GetBytes(length);
				Buffer.BlockCopy(buffer, 0, resultBuffer, offset, buffer.Length);
				offset += buffer.Length;
			}
			
			return resultBuffer;
		}

		/// <summary>
		/// Reads the specified stream for the number of bytes specified.
		/// </summary>
		/// <exception cref="ArgumentNullException">Thrown when the stream is null.</exception>
		/// <exception cref="ArgumentNullException">Thrown when the bytes are null.</exception>
		/// <exception cref="ConnectionClosedByPeerException">Thrown when the connection is closed by the remote peer.</exception>
		/// <param name="stream">The stream to read from.</param>
		/// <param name="bytes">The byte array to read data into. Should be pre-initialized.</param>
		/// <returns>The number of bytes read from the socket.</returns>
		public static int ReceiveBytes(Stream stream, byte[] bytes)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}

			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}

			int result = 0;
			result = stream.Read(bytes, 0, bytes.Length);

			if (result == 0)
			{
				throw new ConnectionClosedByPeerException();
			}

			return result;
		}

		/// <summary>
		/// Reads data into the byte array using the specified socket up to the array's length
		/// </summary>
		/// <exception cref="ArgumentNullException">Thrown when the socket is null.</exception>
		/// <exception cref="ArgumentNullException">Thrown when the bytes are null.</exception>
		/// <exception cref="ConnectionClosedByPeerException">Thrown when the connection is closed by the remote peer.</exception>
		/// <param name="socket">The socket to read from.</param>
		/// <param name="bytes">The byte array to read data into. Should be pre-initialized.</param>
		/// <returns>The number of bytes read from the socket.</returns>
		//public static int ReceiveBytes(Socket socket, byte[] bytes)
		//{
		//    if (socket == null)
		//        throw new ArgumentNullException("socket");

		//    if (bytes == null)
		//        throw new ArgumentNullException("bytes");

		//    int result = 0;
		//    result = socket.Receive(bytes, 0, bytes.Length, SocketFlags.None);

		//    if (result == 0)
		//        throw new ConnectionClosedByPeerException();

		//    return result;
		//}

		/// <summary>
		/// Reads the specified stream for the ammount of data specified.
		/// </summary>
		/// <exception cref="ArgumentNullException">Thrown when the stream is null.</exception>
		/// <exception cref="ArgumentNullException">Thrown when the bytes are null.</exception>
		/// <exception cref="ConnectionClosedByPeerException">Thrown when the connection is closed by the remote peer.</exception>
		/// <param name="stream">The stream to read from.</param>
		/// <param name="bytes">The byte array to read data into. Should be pre-initialized.</param>
		/// <param name="offset">The offest into the byte array to begin sending from.</param>
		/// <param name="chunksize">The chunk size to use when sending</param>
		/// <returns>The number of bytes read from the socket.</returns>
		public static int ReceiveBytes(Stream stream, byte[] bytes, int offset, int chunksize)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}

			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}

			int result = 0;
			result = stream.Read(bytes, offset, chunksize);

			if (result == 0)
			{
				throw new ConnectionClosedByPeerException();
			}

			return result;
		}

		/// <summary>
		/// Reads data into the byte array using the specified socket up to the array's length
		/// </summary>
		/// <exception cref="ArgumentNullException">Thrown when the socket is null.</exception>
		/// <exception cref="ArgumentNullException">Thrown when the bytes are null.</exception>
		/// <exception cref="ConnectionClosedByPeerException">Thrown when the connection is closed by the remote peer.</exception>
		/// <param name="socket">The socket to read from.</param>
		/// <param name="bytes">The byte array to read data into. Should be pre-initialized.</param>
		/// <param name="offset">The offest into the byte array to begin sending from.</param>
		/// <param name="chunksize">The chunk size to use when sending</param>
		/// <returns>The number of bytes read from the socket.</returns>
		//public static int ReceiveBytes(Socket socket, byte[] bytes, int offset, int chunksize)
		//{
		//    if (socket == null)
		//        throw new ArgumentNullException("socket");

		//    if (bytes == null)
		//        throw new ArgumentNullException("bytes");

		//    int result = 0;
		//    result = socket.Receive(bytes, offset, chunksize, SocketFlags.None);

		//    if (result == 0)
		//        throw new ConnectionClosedByPeerException();

		//    return result;
		//}

		/// <summary>
		/// Reads the number of bytes specified, up to the max bytes, from the stream.
		/// </summary>
		/// <exception cref="ArgumentNullException">Thrown when the stream is null.</exception>
		/// <exception cref="ConnectionClosedByPeerException">Thrown when the connection is closed by the remote peer.</exception>
		/// <param name="stream">The stream to read from.</param>
		/// <param name="numBytes">The number of bytes to read.</param>
		/// <param name="maxBytes">The maximum number of bytes that can be read at a time.</param>
		/// <returns>A byte arra</returns>
		public static byte[] ReceiveBytes(Stream stream, int numBytes, int maxBytes)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}

			// cap the number of bytes we will read at one time if necessary
			if (numBytes > maxBytes)
			{
				numBytes = maxBytes;
			}

			// construct a new buffer into which we'll receive the bytes
			byte[] buffer = new byte[numBytes];
			int numReceived = 0;

			// receive the bytes specified
			numReceived = ReceiveBytes(stream, buffer, 0, numBytes);

			// adjust it down if we didn't read all of them 
			if (numReceived < numBytes)
			{
				// create a new smaller buffer
				byte[] tempBuffer = new byte[numReceived];

				// copy what we did get from the main buffer to the temp buffer
				if (numReceived > 0)
				{
					Buffer.BlockCopy(buffer, 0, tempBuffer, 0, numReceived);
				}

				// and finally make the main buffer a clone of the temp (effectively shrinking the main buffer)
				buffer = tempBuffer;
			}

			return buffer;
		}

		/// <summary>
		/// Reads the number of bytes specified, up to the max bytes, from the socket.
		/// </summary>
		/// <exception cref="ArgumentNullException">Thrown when the socket is null.</exception>
		/// <exception cref="ConnectionClosedByPeerException">Thrown when the connection is closed by the remote peer.</exception>
		/// <param name="socket">The socket to read from.</param>
		/// <param name="numBytes">The number of bytes to read.</param>
		/// <param name="maxBytes">The maximum number of bytes that can be read at a time.</param>
		/// <returns>A byte arra</returns>
		//public static byte[] ReceiveBytes(Socket socket, int numBytes, int maxBytes)
		//{
		//    if (socket == null)
		//        throw new ArgumentNullException("socket");

		//    // cap the number of bytes we will read at one time if necessary
		//    if (numBytes > maxBytes)
		//        numBytes = maxBytes;

		//    // construct a new buffer into which we'll receive the bytes
		//    byte[] buffer = new byte[numBytes];
		//    int numReceived = 0;

		//    // receive the bytes specified
		//    numReceived = ReceiveBytes(socket, buffer, 0, numBytes);

		//    // adjust it down if we didn't read all of them 
		//    if (numReceived < numBytes)
		//    {
		//        // create a new smaller buffer
		//        byte[] tempBuffer = new byte[numReceived];

		//        // copy what we did get from the main buffer to the temp buffer
		//        if (numReceived > 0)
		//            Buffer.BlockCopy(buffer, 0, tempBuffer, 0, numReceived);

		//        // and finally make the main buffer a clone of the temp (effectively shrinking the main buffer)
		//        buffer = tempBuffer;
		//    }

		//    return buffer;
		//}

		/// <summary>
		/// Writes the bytes to the specified stream.
		/// </summary>
		/// <exception cref="ArgumentNullException">Thrown when the stream is null.</exception>
		/// <exception cref="ArgumentNullException">Thrown when the bytes are null.</exception>
		/// <exception cref="ConnectionClosedByPeerException">Thrown when the connection is closed by the remote peer.</exception>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="bytes">The bytes to write to the socket.</param>
		/// <returns>The number of bytes written to the socket.</returns>
		public static void SendBytes(Stream stream, byte[] bytes)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}

			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}

			try
			{
				stream.Write(bytes, 0, bytes.Length);
				stream.Flush();
			}
			catch (IOException ex)
			{
				Debug.WriteLine(ex);
				throw new ConnectionClosedByPeerException();
			}
			catch (ObjectDisposedException ex)
			{
				Debug.WriteLine(ex);
				throw new ConnectionClosedByPeerException();
			}		
		}

		/// <summary>
		/// Writes the bytes to the specified socket
		/// </summary>
		/// <exception cref="ArgumentNullException">Thrown when the socket is null.</exception>
		/// <exception cref="ArgumentNullException">Thrown when the bytes are null.</exception>
		/// <exception cref="ConnectionClosedByPeerException">Thrown when the connection is closed by the remote peer.</exception>
		/// <param name="socket">The socket to write to.</param>
		/// <param name="bytes">The bytes to write to the socket.</param>
		/// <returns>The number of bytes written to the socket.</returns>
		//public static int SendBytes(Socket socket, byte[] bytes)
		//{
		//    if (socket == null)
		//        throw new ArgumentNullException("socket");

		//    if (bytes == null)
		//        throw new ArgumentNullException("bytes");

		//    int result = 0;
		//    result = socket.Send(bytes, 0, bytes.Length, SocketFlags.None);

		//    if (result == 0)
		//        throw new ConnectionClosedByPeerException();

		//    return result;
		//}

		/// <summary>
		/// Writes the bytes to the specified stream.
		/// </summary>
		/// <exception cref="ArgumentNullException">Thrown when the stream is null.</exception>
		/// <exception cref="ArgumentNullException">Thrown when the bytes are null.</exception>
		/// <exception cref="ConnectionClosedByPeerException">Thrown when the connection is closed by the remote peer.</exception>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="bytes">The bytes to write to the socket.</param>
		/// <param name="chunksize"></param>
		/// <param name="offset"></param>
		/// <returns>The number of bytes written to the socket.</returns>
		public static void SendBytes(Stream stream, byte[] bytes, int offset, int chunksize)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}

			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}

			try
			{
				stream.Write(bytes, offset, chunksize);
				stream.Flush();
			}
			catch (IOException ex)
			{
				Debug.WriteLine(ex);
				throw new ConnectionClosedByPeerException();
			}
			catch (ObjectDisposedException ex)
			{
				Debug.WriteLine(ex);
				throw new ConnectionClosedByPeerException();
			}
		}
 
		/// <summary>
		/// Writes the bytes to the specified socket
		/// </summary>
		/// <exception cref="ArgumentNullException">Thrown when the socket is null.</exception>
		/// <exception cref="ArgumentNullException">Thrown when the bytes are null.</exception>
		/// <exception cref="ConnectionClosedByPeerException">Thrown when the connection is closed by the remote peer.</exception>
		/// <param name="socket">The socket to write to.</param>
		/// <param name="bytes">The bytes to write to the socket.</param>
		/// <param name="chunksize"></param>
		/// <param name="offset"></param>
		/// <returns>The number of bytes written to the socket.</returns>
		//public static int SendBytes(Socket socket, byte[] bytes, int offset, int chunksize)
		//{
		//    if (socket == null)
		//        throw new ArgumentNullException("socket");

		//    if (bytes == null)
		//        throw new ArgumentNullException("bytes");

		//    int result = 0;
		//    result = socket.Send(bytes, offset, chunksize, SocketFlags.None);

		//    if (result == 0)
		//        throw new ConnectionClosedByPeerException();

		//    return result;
		//}

		/// <summary>
		/// Combines the buffers together into a final buffer containing the bytes of each buffer sequentially.
		/// </summary>
		/// <param name="buffers">An array of byte arrays to combine into one byte array.</param>
		/// <returns></returns>
		public static byte[] Combine(params byte[][] buffers)
		{
			if (buffers == null)
			{
				throw new ArgumentNullException("buffers");
			}

			int length = 0;
			foreach (byte[] buffer in buffers)
			{
				length += buffer.Length;
			}

			int offset = 0;
			byte[] resultBuffer = new byte[length];
			foreach (byte[] buffer in buffers)
			{
				Buffer.BlockCopy(buffer, 0, resultBuffer, offset, buffer.Length);
				offset += buffer.Length; 
			}
			
			return resultBuffer;
		}

		/// <summary>
		/// Copies bytes from the specified buffer into a new byte array.
		/// </summary>
		/// <exception cref="ArgumentNullException">Thrown if the byte array is null.</exception>
		/// <param name="buffer">The buffer to copy from.</param>
		/// <param name="startIndex">The index in the buffer to start copying.</param>
		/// <param name="length">The length of bytes copied to the new byte array.</param>
		/// <returns>A byte array containing the specified bytes from the source buffer.</returns>
		public static byte[] BlockCopy(byte[] buffer, int startIndex, int length)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}

			byte[] tempBuffer = new byte[length];
			if (tempBuffer.Length > 0)
			{
				Buffer.BlockCopy(buffer, startIndex, tempBuffer, 0, length);
			}

			return tempBuffer;			
		}

		/// <summary>
		/// Resolves an address and port to an IPEndPoint.
		/// </summary>
		/// <param name="address">The address to resolve. My be an IPv4 or IPv6 dotted quad or hex notation, or a valid dns hostname.</param>
		/// <param name="port">The remote port number</param>
		/// <param name="sender">The sender of the event as you would like to fire the event for resolution.</param>
		/// <param name="onResolving">The callback that will be notified before resolution occurs.</param>		
		/// <param name="stateObject">A user defined state object that can be used to determine context for the call.</param>
		/// <returns>An IPEndPoint containing the IPAddress and port specified.</returns>
		public static IPEndPoint Resolve(string address, int port, object sender, EventHandler<AddressResolutionEventArgs> onResolving, object stateObject)
		{
			if (address == null)
				throw new ArgumentNullException("address", @"The address cannot be null.");

			if (address == string.Empty)
				throw new ArgumentOutOfRangeException("address", "The address cannot be an empty string (i.e., \"\").");

			#region Addresss Resolution Events

			if (onResolving != null)
			{
				try
				{
					// notify the caller that we are trying to resolve the address 
					onResolving(sender, new AddressResolutionEventArgs(address, port, stateObject));
				}
				catch(Exception ex)
				{
					Log.WriteLine(ex);
				}
			}

			#endregion

			// if the address looks like an ip address, just parse it out and skip the whole dns thing
            if (_ipRegex.IsMatch(address))
            {
                try
		    	{
                    // first try and parse the address out
                    // it may be a IPv4 dotted quad or in IPv6 colon-hex notation
                    IPAddress ipAddress = IPAddress.Parse(address);

                    // return a new end point without ever hitting dns
                    return new IPEndPoint(ipAddress, port);
                }
                catch (FormatException ex)
                {
                    // address is not a valid IP address.
                    Log.WriteLine(string.Format("{0}. Dns will be used to resolve '{1}'.", ex.Message, address));
                }
			}
			
			// resolve the address using DNS
			IPHostEntry he = Dns.GetHostEntry(address);

			// create and return a new IP end point based on the address and port
			return new IPEndPoint(he.AddressList[0], port);
		}			

		/// <summary>
		/// Waits indefinitely for at least one byte of data to be available on the socket for reading
		/// </summary>
		/// <exception cref="ArgumentNullException">Thrown when the socket is null.</exception>
		/// <exception cref="ArgumentNullException">Thrown when the abort event is null.</exception>
		/// <exception cref="OperationAbortedException">Thrown when the operation is manually cancelled by signalling the abort event.</exception>
		/// <param name="socket">The socket to read from.</param>
		/// <param name="abortEvent">The abort event to watch in case the operation is cancelled.</param>	
		/// <returns>The number of bytes available on the socket.</returns>
		public static int WaitForAvailableBytes(Socket socket, ManualResetEvent abortEvent)
		{		
			return WaitForAvailableBytes(socket, abortEvent, 1 /* at least a byte */);		
		}

		/// <summary>
		/// Waits indifinitely for the specified number of bytes to become available for reading on the specified socket
		/// </summary>
		/// <exception cref="ArgumentNullException">Thrown when the socket is null.</exception>
		/// <exception cref="ArgumentNullException">Thrown when the abort event is null.</exception>
		/// <exception cref="OperationAbortedException">Thrown when the operation is manually cancelled by signalling the abort event.</exception>
		/// <param name="socket">The socket to read from.</param>
		/// <param name="abortEvent">The abort event to watch in case the operation is cancelled.</param>
		/// <param name="bytesNeeded">the number of bytes that should be available before the method returns.</param>
		/// <returns>The number of bytes available on the socket.</returns>
		public static int WaitForAvailableBytes(Socket socket, ManualResetEvent abortEvent, int bytesNeeded)
		{
			if (socket == null)
			{
				throw new ArgumentNullException("socket");
			}

//			if (abortEvent == null)
//				throw new ArgumentNullException("abortEvent");

			/* 100ms */
			const int microseconds = 10000; 

			int available = 0;
			bool readable = false;
			bool broken = false;

			// wait until there are bytes available, or the socket is readable, or the socket errors out
			while (true)
			{					
				try
				{										
					readable = socket.Poll(microseconds, SelectMode.SelectRead);
					broken = socket.Poll(microseconds, SelectMode.SelectError);
 
					// keep polling for data to read
					available = socket.Available;
				}
				catch (SocketException ex)
				{
					// when the socket is closed, this little guy will bail with not a socket exception
					if (ex.ErrorCode != (int)SocketErrors.WSAENOTSOCK)
						throw;
				}

				// any of these conditions can let us out of this loop of hell
				if (available >= bytesNeeded || readable || broken)
				{
					break;
				}

				// see if we can bail
				if (abortEvent != null)
				{
					if (abortEvent.WaitOne(1, false /* stay in context */))
					{
						throw new OperationAbortedException();
					}
				}
			}

			//Debug.WriteLine(string.Format("'{0}' bytes available.", available.ToString()), "'SocketUtilities'");

			// return the number of bytes available
			return available;		
		}

		/// <summary>
		/// Formats the number into KB 
		/// </summary>
		/// <param name="bytes">The number that should be formatted into the '###,###,##0 KB' format.</param>
		/// <returns>A string containing the representation of the bytes in the format '###,###,##0 KB'.</returns>
		public static string FormatInKiloBytes(int bytes)
		{
			decimal kb = 0;
			if (bytes > 0)
			{
				kb = Convert.ToDecimal(bytes / 1000.0);
				kb = Math.Round(kb);
				kb = Math.Max(1, kb);
			}
			int nkb = Convert.ToInt32(kb);
			return nkb.ToString("###,###,##0") + " KB";	
		}

		/// <summary>
		/// Calculates what percentage of the total the value currently is.
		/// </summary>
		/// <param name="value">The current value.</param>
		/// <param name="total">The total possible value that can be achieved.</param>
		/// <returns>An integer representing the percentage as calculated.</returns>
		public static int GetPercent(int value, int total)
		{
			if (value == 0 || total == 0)
			{
				return 0;
			}

			return (int)((((double)value) / ((double)total)) * 100d);
		}

		/// <summary>
		/// Creates a new Tcp socket.
		/// </summary>
		/// <param name="reuseAddress">A flag that indicates whether the socket can reuse addresses.</param>
		/// <param name="sendTimeout">A send timeout value (milliseconds).</param>
		/// <param name="recvTimeout">A recv timeout value (milliseconds).</param>
		/// <returns>A new Tcp stream based socket.</returns>
		public static Socket CreateTcpSocket(bool reuseAddress, int sendTimeout, int recvTimeout)
		{
			// create a new tcp socket
			Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			// reuse address
			socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, reuseAddress ? 1 : 0);

			// send timeout
			if (sendTimeout > 0)
			{
				socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, sendTimeout);
			}

			// recv timeout
			if (recvTimeout > 0)
			{
				socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, recvTimeout);
			}

			return socket;
		}
	}
}
