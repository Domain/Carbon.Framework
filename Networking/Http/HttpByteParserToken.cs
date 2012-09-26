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

namespace Carbon.Networking.Http
{
	/// <summary>
	/// Defines a class that holds a byte array of data created as a token from a byte parser, and adds string like functionality on top of the underlying bytes.
	/// </summary>
	internal class HttpByteParserToken 
	{
		protected byte[] _bytes;

		/// <summary>
		/// Initializes a new instance of the byte string
		/// </summary>
		/// <param name="buffer"></param>
		public HttpByteParserToken(byte[] bytes)
		{
			if (bytes == null)
				bytes = new byte[0];

			_bytes = HttpUtils.Clone(bytes, 0, bytes.Length);			
		}

		/// <summary>
		/// Initializes a new instance of the HttpByteParserToken class
		/// </summary>
		/// <param name="bytes"></param>
		/// <param name="startIndex"></param>
		/// <param name="length"></param>
		public HttpByteParserToken(byte[] bytes, int startIndex, int length) 
		{
			_bytes = HttpUtils.Clone(bytes, startIndex, length);						
		}
		
		/// <summary>
		/// Returns the underlying byte array
		/// </summary>
		public byte[] Bytes 
		{ 
			get 
			{ 
				return _bytes; 
			} 
		}

		/// <summary>
		/// Returns the length of the byte array
		/// </summary>
		public int Length 
		{ 
			get 
			{
				if (_bytes == null)
					return 0;
				return _bytes.Length; 
			} 
		}
			
		/// <summary>
		/// Returns a flag that indicates whether the object is empty
		/// </summary>
		public bool IsEmpty 
		{ 
			get 
			{ 
				if (_bytes == null)
					return true;

				if (_bytes.Length == 0)
					return true;

				return false;
			}  
		}

		/// <summary>
		/// Returns the byte at the specified index of the byte array
		/// </summary>
		public byte this[int index] 
		{
			get 
			{
				return _bytes[index];
			}
		}
		
		/// <summary>
		/// Returns the contents of the byte array as a UTF-8 encoded string
		/// </summary>
		/// <returns></returns>
		public string ToString(Encoding encoding) 
		{
			if (this.IsEmpty)
				return string.Empty;

			return encoding.GetString(_bytes, 0, _bytes.Length);
		}

		/// <summary>
		/// Returns a copy of the internal byte array
		/// </summary>
		/// <returns></returns>
		public byte[] ToByteArray() 
		{
			return HttpUtils.Clone(_bytes, 0, _bytes.Length);
		}

		/// <summary>
		/// Returns the index of the character specified, -1 if not found.
		/// </summary>
		/// <param name="ch">The character to search for</param>
		/// <returns></returns>
		public int IndexOf(char ch) 
		{
			return IndexOf(ch, 0);
		}

		/// <summary>
		/// Returns the index of the character specified, -1 if not found.
		/// </summary>
		/// <param name="ch">The character to search for</param>
		/// <param name="startIndex">The index to start searching at</param>
		/// <returns></returns>
		public int IndexOf(char ch, int startIndex) 
		{						
			for (int i = startIndex; i < _bytes.Length; i++) 
				if (this[i] == (byte)ch)
					return i;
			return -1;
		}

		/// <summary>
		/// Returns a portion of the bytes starting at the specified index
		/// </summary>
		/// <param name="startIndex"></param>
		/// <returns></returns>
		public HttpByteParserToken Substring(int startIndex) 
		{
			return Substring(startIndex, _bytes.Length - startIndex);
		}

		/// <summary>
		/// Returns a portion of the bytes starting at the specified index
		/// </summary>
		/// <param name="startIndex"></param>
		/// <returns></returns>
		public HttpByteParserToken Substring(int startIndex, int length) 
		{
			return new HttpByteParserToken(_bytes, startIndex, length);
		}

		/// <summary>
		/// Splits the byte array into an array of chunks using the specified character separator
		/// </summary>
		/// <param name="separator"></param>
		/// <returns></returns>
		public HttpByteParserToken[] Split(char separator) 
		{
			ArrayList list = new ArrayList();

			int pos = 0;

			while (pos <  _bytes.Length) 
			{
				int i = IndexOf(separator, pos);

				if (i < 0)
					break;

				list.Add(Substring(pos, i-pos));
				pos = i+1;

				while (this[pos] == (byte)separator && pos <  _bytes.Length)
					pos++;
			}

			if (pos <  _bytes.Length)
				list.Add(Substring(pos));

			int n = list.Count;
			HttpByteParserToken[] result = new HttpByteParserToken[n];
            
			for (int i = 0; i < n; i++)
				result[i] = (HttpByteParserToken)list[i];
            
			return result;
		}

		/// <summary>
		/// Returns the contents of the byte array as a UTF-8 encoded string
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return this.ToString(HttpUtils.Encoding);
		}
	}
}
