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
	/// Summary description for HttpChunkList.
	/// </summary>
	internal class HttpChunkCollection : CollectionBase
	{
		/// <summary>
		/// Initializes a new 
		/// </summary>
		public HttpChunkCollection()
		{
			
		}

		/// <summary>
		/// Adds a chunk to the list
		/// </summary>
		/// <param name="chunk"></param>
		public void Add(HttpChunk chunk)
		{
			base.InnerList.Add(chunk);
		}

		/// <summary>
		/// Adds an array of chunks to the list
		/// </summary>
		/// <param name="chunks"></param>
		public void AddRange(HttpChunk[] chunks)
		{
			foreach(HttpChunk chunk in chunks)
				this.Add(chunk);
		}

		/// <summary>
		/// Removes a chunk from the list
		/// </summary>
		/// <param name="chunk"></param>
		public void Remove(HttpChunk chunk)
		{

		}		

		/// <summary>
		/// Returns a chunk from the specified index in the list
		/// </summary>
		public HttpChunk this[int index]
		{
			get
			{
				return (HttpChunk)base.InnerList[index];
			}
		}

		/// <summary>
		/// Returns a byte array representation of the chunk list
		/// </summary>
		/// <returns></returns>
		public virtual byte[] ToByteArray()
		{
			byte[] buffer = null;

			// create a stream
			using (MemoryStream stream = new MemoryStream())
			{
				// create a writer
				using (BinaryWriter writer = new BinaryWriter(stream, HttpUtils.Encoding))
				{
					// write each chunk to the stream
					foreach(HttpChunk chunk in base.InnerList)
						writer.Write(chunk.ToByteArray());

					// retrieve the buffer from the stream
					buffer = stream.GetBuffer();

					// close the writer and the underlying stream
					writer.Close();
				}
			}

			return buffer;
		}

		/// <summary>
		/// Returns a string repsentation of the chunk list
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			// append each chunk
			foreach(HttpChunk chunk in base.InnerList)
				sb.Append(chunk.ToString());
			
			return sb.ToString();
		}

	}
}
