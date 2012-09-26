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
using System.Text;

using Carbon.Common;

namespace Carbon.Networking.Http
{
	/// <summary>
	/// Summary description for HttpHeaderList.
	/// </summary>
	[Serializable()]
	public sealed class HttpHeaderCollection : DisposableCollection 
	{
		public HttpHeaderCollection()
		{
			
		}

		public void Add(HttpHeader header)
		{
            lock (base.SyncRoot)
            {
                base.InnerList.Add(header);
            }
		}

		public void AddRange(params HttpHeader[] headers)
		{
			if (headers == null)
				throw new ArgumentNullException("headers");

			foreach(HttpHeader header in headers)
				this.Add(header);
		}

		public void Remove(HttpHeader header)
		{
            if (this.Contains(header))
            {
                lock (base.SyncRoot)
                {
                    base.InnerList.Remove(header);
                }
            }
		}

		public void Remove(string headerName)
		{
            lock (base.SyncRoot)
            {
                for (int i = 0; i < base.InnerList.Count; i++)
                {
                    HttpHeader header = (HttpHeader)base.InnerList[i];
                    if (string.Compare(header.Name, headerName, true) == 0)
                    {
                        base.InnerList.RemoveAt(i);
                        return;
                    }
                }
            }
		}

        public void RemoveAllByName(string headerName)
        {
            lock (base.SyncRoot)
            {
                for (int i = 0; i < base.InnerList.Count; i++)
                {
                    HttpHeader header = (HttpHeader)base.InnerList[i];
                    if (string.Compare(header.Name, headerName, true) == 0)
                    {
                        base.InnerList.RemoveAt(i);
                        i--; // careful to backup and check again
                    }
                }
            }
        }

		public new void RemoveAt(int index)
		{
            lock (base.SyncRoot)
            {
                base.InnerList.RemoveAt(index);
            }
		}

		public bool Contains(HttpHeader header)
		{
			if (header == null)
				throw new ArgumentNullException("header");

			return this.Contains(header.Name);
		}

		public bool Contains(string headerName)
		{
			foreach(HttpHeader header in base.InnerList)
				if (string.Compare(header.Name, headerName, true) == 0)
					return true;
			return false;
		}

		public HttpHeader this[int index]
		{
			get
			{
				return (HttpHeader)base.InnerList[index];
			}
		}

		public HttpHeader this[string headerName]
		{
			get
			{
				foreach(HttpHeader header in base.InnerList)
					if (string.Compare(header.Name, headerName, true) == 0)
						return header;			
				return null;
			}
		}

		public string[][] GetUnknownHeaders()
		{
			int unknownHeaders = 0;
			for(int i = 0; i < base.InnerList.Count; i++)
				if (!this[i].IsKnownHeader)
					unknownHeaders++;

			string[][] headers = new string[unknownHeaders][];

			for(int i = 0; i < unknownHeaders; i++)
			{
				HttpHeader hdr = this[i];
				if (!hdr.IsKnownHeader)
				{
					string[] header = hdr.ToArray();
					headers[i] = new string[2];
					headers[i][0] = header[0];
					headers[i][1] = header[1];
				}
			}
		
			return headers;			
		}
        
		public string[][] ToArray()
		{
			string[][] headers = new string[base.InnerList.Count][];
			
			for(int i = 0; i < base.InnerList.Count; i++)
			{
				HttpHeader hdr = this[i];
				string[] header = hdr.ToArray();
				headers[i] = new string[2];
				headers[i][0] = header[0];
				headers[i][1] = header[1];
			}
			
			return headers;
		}

		public override string ToString()
		{			
			StringBuilder sb = new StringBuilder();

			// append each header
			foreach(HttpHeader header in base.InnerList)
			{				
				if (!HttpUtils.IsEmptryString(header.Value))
					sb.Append(header.ToString());
			}

			// wrap up the headers with another crlf combo
			sb.Append(HttpControlChars.CRLF);
			
			return sb.ToString();
		}
	}

	#region HttpHeaderAlreadyExistsException

	/// <summary>
	/// Defines an exception that is throw when a header is added to a list of headers that is already in the list
	/// </summary>
	public class HttpHeaderAlreadyExistsException : Exception 
	{
		protected HttpHeader _header;

		public HttpHeaderAlreadyExistsException(HttpHeader header) : base(string.Format("A header with the name '{0}' already exists.", header.Name))
		{
			_header = header;
		}

		public HttpHeader Header
		{
			get
			{
				return _header;
			}
		}
	}

	#endregion
}
