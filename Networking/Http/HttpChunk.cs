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
using System.IO;
using System.Text;

namespace Carbon.Networking.Http
{
	/// <summary>
	/// Summary description for HttpChunk.
	/// </summary>
	internal class HttpChunk
	{
		protected HttpChunkSizeLine _sizeLine;
		protected byte[] _data;
		
		/// <summary>
		/// Initializes a new instance of the HttpChunk class
		/// </summary>
		/// <param name="data"></param>
		public HttpChunk(byte[] data)
		{
			_sizeLine = new HttpChunkSizeLine(data.Length, null);
			_data = HttpUtils.Clone(data, 0, data.Length);
		}

		/// <summary>
		/// Initializes a new instance of the HttpChunk class
		/// </summary>
		/// <param name="size"></param>
		/// <param name="extension"></param>
		/// <param name="data"></param>
		public HttpChunk(int size, string extension, byte[] data)
		{
			_sizeLine = new HttpChunkSizeLine(size, extension);
			_data = HttpUtils.Clone(data, 0, data.Length);			
		}

		/// <summary>
		/// Initializes a new instance of the HttpChunk class
		/// </summary>
		/// <param name="sizeLine"></param>
		/// <param name="data"></param>
		public HttpChunk(HttpChunkSizeLine sizeLine, byte[] data)
		{
			_sizeLine = sizeLine;
			_data = data;
		}

		/// <summary>
		/// Gets or sets the size of the chunk
		/// </summary>
		public int Size
		{
			get
			{
				return _sizeLine.Size;
			}
			set
			{
				_sizeLine.Size = value;
			}
		}

		/// <summary>
		/// Gets or sets the extension (A name/value pair delimited by a '=' separator) of the chunk
		/// </summary>
		public string Extension
		{
			get
			{
				return _sizeLine.Extension;
			}
			set
			{	
				_sizeLine.Extension = value;
			}
		}

		/// <summary>
		/// Returns a flag that indicates whether this chunk has an extension present or not
		/// </summary>
		public bool HasExtension
		{
			get
			{
				return _sizeLine.HasExtension;
			}
		}

		/// <summary>
		/// Determines if the chunk is empty, if so then it is the last chunk, optional trailers may follow
		/// </summary>
		public bool IsEmpty
		{
			get
			{
				return _sizeLine.IsEmpty;
			}
		}

		/// <summary>
		/// Gets or sets the byte array which composes the majority of the chunk
		/// </summary>
		public byte[] Data
		{
			get
			{
				return _data;
			}
			set
			{
				_data = value;
			}
		}

		/// <summary>
		/// Returns the number of bytes in the chunk that were not data related
		/// </summary>
		/// <returns></returns>
		public int GetNonDataByteCount()
		{
			// size line
			byte[] sizeLineBytes = HttpUtils.Encoding.GetBytes(_sizeLine.ToString());
			byte[] endingBytes = HttpUtils.Encoding.GetBytes(HttpControlChars.CRLF);
			int byteCount = sizeLineBytes.Length + endingBytes.Length;
			return byteCount;
		}	

		public virtual byte[] ToByteArray()
		{
			byte[] buffer = new byte[] {};		
			byte[] sizeLineBytes = HttpUtils.Encoding.GetBytes(_sizeLine.ToString());
			byte[] endingBytes = HttpUtils.Encoding.GetBytes(HttpControlChars.CRLF);

			buffer = HttpUtils.Combine(sizeLineBytes, buffer);
			buffer = HttpUtils.Combine(buffer, this.Data);
			buffer = HttpUtils.Combine(buffer, endingBytes);
			
			return buffer;
		}

		/// <summary>
		/// Returns a byte array representation of the HttpChunk
		/// </summary>
//		/// <returns></returns>
//		public virtual byte[] ToByteArray()
//		{
//			byte[] buffer = null;
//
//			// create a stream
//			using (MemoryStream stream = new MemoryStream())
//			{
//				// create a writer
//				using (BinaryWriter writer = new BinaryWriter(stream, HttpUtils.Encoding))
//				{
//					// write the size
//					writer.Write(this.Size.ToString("X"));
//					
//					// write the extension if there is one
//					if (this.HasExtension)
//						writer.Write(this.ExtensionFormated);
//					
//					// terminate the size line
//					writer.Write(HttpControlChars.CRLF);					
//
//					// write the data
//					writer.Write(this.Data);
//
//					// terminate the data line
//					writer.Write(HttpControlChars.CRLF);
//
//					writer.Flush();
//
//					// retrieve the buffer from the stream
//					buffer = stream.GetBuffer();
//
//					// close the writer and the underlying stream
//					writer.Close();
//				}
//			}
//
//			return buffer;
//		}

		/// <summary>
		/// Returns a string in the format 'chunk-size [chunk-extension] CRLF chunk-data CRLF'
		/// </summary>
		public override string ToString()
		{
			string sizeLine =  _sizeLine.ToString();
			return string.Format("{0}\r\n{1}\r\n", sizeLine, HttpUtils.Encoding.GetString(this.Data));
		}	
	}
}
