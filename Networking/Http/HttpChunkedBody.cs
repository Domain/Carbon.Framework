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
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.IO;

namespace Carbon.Networking.Http
{
	/// <summary>
	/// Summary description for HttpChunkedBody.
	/// </summary>
	internal class HttpChunkedBody
	{
		protected HttpChunkCollection _chunks;
		protected HttpHeaderCollection _trailer;

		/// <summary>
		/// Initializes a new instance of the HttpChunkedBody class
		/// </summary>
		public HttpChunkedBody()
		{
			_chunks = new HttpChunkCollection();
			_trailer = new HttpHeaderCollection();
		}

		/// <summary>
		/// Returns the list of chunks contained in this entity body
		/// </summary>
		public HttpChunkCollection Chunks
		{
			get
			{
				return _chunks;
			}
		}

		/// <summary>
		/// Returns the list of trailer headers contained in this entity body
		/// </summary>
		public HttpHeaderCollection TrailerHeaders
		{
			get
			{
				return _trailer;
			}
		}

		private int GetTotalChunkDataSize()
		{
			int size = 0;
			foreach(HttpChunk chunk in _chunks)
				size += chunk.Size;
			return size;
		}

		/// <summary>
		/// Returns a byte array representing this chunked entity body
		/// </summary>
		/// <returns></returns>
		public virtual byte[] ToByteArray()
		{
			byte[] buffer = new byte[this.GetTotalChunkDataSize()];

			int offset = 0;
			foreach(HttpChunk chunk in _chunks)
			{
				Buffer.BlockCopy(chunk.Data, 0, buffer, offset, chunk.Size);
				offset += chunk.Size;
			}

			return buffer;
		}
	}
}
