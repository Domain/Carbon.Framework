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
using System.Net;
using System.Net.Sockets;
using System.Threading;

using Carbon.Networking;

namespace Carbon.Networking.Http
{
	/// <summary>
	/// Defines a class that provides an Http 1.1 request message.
	/// </summary>
	public sealed class HttpRequest : HttpMessage 
	{
		private HttpRequestLine _requestLine;

		/// <summary>
		/// Initializes a new instance of the HttpRequest class
		/// </summary>
		public HttpRequest() : base()
		{
			_requestLine = new HttpRequestLine();
            
			this.InitHeaders();
		}

		/// <summary>
		/// Initializes a new instance of the HttpRequest class
		/// </summary>
		/// <param name="message">The incoming message that will construct this request</param>
		public HttpRequest(HttpMessage message) 
            : base(message)
		{
			_requestLine = HttpRequestLine.Parse(base.FirstLine);
		}

		#region My Overrides

		/// <summary>
		/// Override the first line functionality to include our request line object
		/// </summary>
		internal override string FirstLine
		{
			get
			{
				return _requestLine.ToString();
			}
			set
			{
				_requestLine = HttpRequestLine.Parse(value);
			}
		}

		#endregion

		#region My Public Properties

		/// <summary>
		/// Gets or sets the method contained in this request
		/// </summary>
		public string Method
		{
			get
			{
				return _requestLine.Method;
			}
			set
			{
				_requestLine.Method = value;
			}
		}

		/// <summary>
		/// Gets or sets the request-uri contained in this request
		/// </summary>
		public string RequestUri
		{
			get
			{
				return _requestLine.RequestUri;
			}
			set
			{
				_requestLine.RequestUri = value;
			}
		}

		/// <summary>
		/// Returns the request-uri contained in the request without the query string
		/// </summary>
		public string RequestUriWithoutQueryString
		{
			get
			{
				string uri = this.RequestUri;
				int sep = uri.IndexOf("?");
				if (sep > 0)
					uri = uri.Substring(0, sep);			
				return uri;
			}
		}

		public string QueryString
		{
			get
			{
				string uri = this.RequestUri;
				int sep = uri.IndexOf("?");
				if (sep > 0)
					uri = uri.Substring(++sep);
				return uri;
			}
		}

		/// <summary>
		/// Gets or sets the protocol version contained in this request
		/// </summary>
		public HttpProtocolVersion ProtocolVersion
		{
			get
			{
				return _requestLine.ProtocolVersion;
			}
			set
			{
				_requestLine.ProtocolVersion = value;
			}
		}

		#region Request Headers

		/// <summary>
		/// Gets or sets the Request header 'Accept'. (Ex: "Accept: audio/*; q=0.2, audio/basic") The Accept request-header field can be used to specify certain media types which are acceptable for the response. Accept headers can be used to indicate that the request is specifically limited to a small set of desired types, as in the case of a request for an in-line image.
		/// </summary>
		public string Accept
		{
			get
			{
				return this.ReadHeaderValue(HttpHeaders.RequestHeaders.Accept);
			}
			set
			{
				this.WriteHeaderValue(HttpHeaders.RequestHeaders.Accept, value, true);
			}
		}
		
		/// <summary>
		/// Gets or sets the Request header 'Accept-Encoding'
		/// </summary>
		public string AcceptEncoding
		{
			get
			{
				return this.ReadHeaderValue(HttpHeaders.RequestHeaders.AcceptEncoding);
			}
			set
			{
				this.WriteHeaderValue(HttpHeaders.RequestHeaders.AcceptEncoding, value, true);
			}
		}
		
		/// <summary>
		/// Gets or sets the Request header 'Accept-Language'
		/// </summary>
		public string AcceptLanguage
		{
			get
			{
				return this.ReadHeaderValue(HttpHeaders.RequestHeaders.AcceptLanguage);
			}
			set
			{
				this.WriteHeaderValue(HttpHeaders.RequestHeaders.AcceptLanguage, value, true);
			}
		}
		
		/// <summary>
		/// Gets or sets the Request header 'Authorization'
		/// </summary>
		public string Authorization
		{
			get
			{
				return this.ReadHeaderValue(HttpHeaders.RequestHeaders.Authorization);
			}
			set
			{
				this.WriteHeaderValue(HttpHeaders.RequestHeaders.Authorization, value, true);
			}
		}
		
		/// <summary>
		/// Gets or sets the Request header 'Expect'
		/// </summary>
		public string Expect
		{
			get
			{
				return this.ReadHeaderValue(HttpHeaders.RequestHeaders.Expect);
			}
			set
			{
				this.WriteHeaderValue(HttpHeaders.RequestHeaders.Expect, value, true);
			}
		}
		
		/// <summary>
		/// Gets or sets the Request header 'From'
		/// </summary>
		public string From
		{
			get
			{
				return this.ReadHeaderValue(HttpHeaders.RequestHeaders.From);
			}
			set
			{
				this.WriteHeaderValue(HttpHeaders.RequestHeaders.From, value, true);
			}
		}
		
		/// <summary>
		/// Gets or sets the Request header 'Host'. (Host = "Host" ":" host [ ":" port ] ; Section 3.2.2 p.79). Specifies the internet host and port number of the resource being requested. A client MUST include a Host header field in all HTTP/1.1 request messages . If the requested URI does not include an Internet host name for the service being requested, then the Host header field MUST be given with an empty value. An HTTP/1.1 proxy MUST ensure that any request message it forwards does contain an appropriate	Host header field that identifies the service being requested by the proxy. All Internet-based HTTP/1.1 servers																																																																					MUST respond with a 400 (Bad Request) status code to any HTTP/1.1 request message which lacks a Host header	field.
		/// </summary>
		public string Host
		{
			get
			{
				return this.ReadHeaderValue(HttpHeaders.RequestHeaders.Host);
			}
			set
			{
				this.WriteHeaderValue(HttpHeaders.RequestHeaders.Host, value, true);
			}
		}
		
		/// <summary>
		/// Gets or sets the Request header 'If-Match'
		/// </summary>
		public string IfMatch
		{
			get
			{
				return this.ReadHeaderValue(HttpHeaders.RequestHeaders.IfMatch);
			}
			set
			{
				this.WriteHeaderValue(HttpHeaders.RequestHeaders.IfMatch, value, true);
			}
		}
		
		/// <summary>
		/// Gets or sets the Request header 'If-Modified-Since'
		/// </summary>
		public string IfModifiedSince
		{
			get
			{
				return this.ReadHeaderValue(HttpHeaders.RequestHeaders.IfModifiedSince);
			}
			set
			{
				this.WriteHeaderValue(HttpHeaders.RequestHeaders.IfModifiedSince, value, true);
			}
		}

		/// <summary>
		/// Gets or sets the Request header 'If-None-Match'
		/// </summary>
		public string IfNoneMatch
		{
			get
			{
				return this.ReadHeaderValue(HttpHeaders.RequestHeaders.IfNoneMatch);
			}
			set
			{
				this.WriteHeaderValue(HttpHeaders.RequestHeaders.IfNoneMatch, value, true);
			}
		}
		
		/// <summary>
		/// Gets or sets the Request header 'If-Range'
		/// </summary>
		public string IfRange
		{
			get
			{
				return this.ReadHeaderValue(HttpHeaders.RequestHeaders.IfRange);
			}
			set
			{
				this.WriteHeaderValue(HttpHeaders.RequestHeaders.IfRange, value, true);
			}
		}
		
		/// <summary>
		/// Gets or sets the Request header 'If-Unmodified-Since'
		/// </summary>
		public string IfUnmodifiedSince
		{
			get
			{
				return this.ReadHeaderValue(HttpHeaders.RequestHeaders.IfUnmodifiedSince);
			}
			set
			{
				this.WriteHeaderValue(HttpHeaders.RequestHeaders.IfUnmodifiedSince, value, true);
			}
		}
		
		/// <summary>
		/// Gets or sets the Request header 'Max-Forwards'
		/// </summary>
		public string MaxForwards
		{
			get
			{
				return this.ReadHeaderValue(HttpHeaders.RequestHeaders.MaxForwards);
			}
			set
			{
				this.WriteHeaderValue(HttpHeaders.RequestHeaders.MaxForwards, value, true);
			}
		}
		
		/// <summary>
		/// Gets or sets the Request header 'Proxy-Authorization'
		/// </summary>
		public string ProxyAuthorization
		{
			get
			{
				return this.ReadHeaderValue(HttpHeaders.RequestHeaders.ProxyAuthorization);
			}
			set
			{
				this.WriteHeaderValue(HttpHeaders.RequestHeaders.ProxyAuthorization, value, true);
			}
		}
		
		/// <summary>
		/// Gets or sets the Request header 'Range'
		/// </summary>
		public string Range
		{
			get
			{
				return this.ReadHeaderValue(HttpHeaders.RequestHeaders.Range);
			}
			set
			{
				this.WriteHeaderValue(HttpHeaders.RequestHeaders.Range, value, true);
			}
		}
		
		/// <summary>
		/// Gets or sets the Request header 'Referrer'
		/// </summary>
		public string Referrer
		{
			get
			{
				return this.ReadHeaderValue(HttpHeaders.RequestHeaders.Referer);
			}
			set
			{
				this.WriteHeaderValue(HttpHeaders.RequestHeaders.Referer, value, true);
			}
		}
		
		/// <summary>
		/// Gets or sets the Request header 'TE'
		/// </summary>
		public string TE
		{
			get
			{
				return this.ReadHeaderValue(HttpHeaders.RequestHeaders.TE);
			}
			set
			{
				this.WriteHeaderValue(HttpHeaders.RequestHeaders.TE, value, true);
			}
		}

		/// <summary>
		/// Gets or sets the Request header 'User-Agent'
		/// </summary>
		public string UserAgent
		{
			get
			{
				return this.ReadHeaderValue(HttpHeaders.RequestHeaders.UserAgent);
			}
			set
			{
				this.WriteHeaderValue(HttpHeaders.RequestHeaders.UserAgent, value, true);
			}
		}

		#endregion

		/// <summary>
		/// Gets or sets the Razor General header 'Response-Needed' which can short-circuit the response mechanism to not respond to requests.
		/// </summary>
		public bool ResponseNeeded
		{
			get
			{		
				string value = string.Empty;		
				try
				{					
					value = this.ReadHeaderValue(HttpHeaders.GeneralHeaders.ResponseNeeded);
                    if (!HttpUtils.IsEmptryString(value))
                    {
                        value = HttpUtils.StripCRLF(value);
                        value = HttpUtils.TrimLeadingAndTrailingSpaces(value);
                        return Convert.ToBoolean(value);
                    }
					return true;
				}
				catch (Exception)
				{					
					// Debug.WriteLine(ex);
				}

				// always fall back on true becuase the http/1.1 specifications state that there must be a response for every request
				return true;
			}
			set
			{				
				this.WriteHeaderValue(HttpHeaders.GeneralHeaders.ResponseNeeded, value.ToString(), true);								
			}
		}

		#endregion

        /// <summary>
        /// Returns the value of a query string parameter.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
		public string GetQueryStringValueByKey(string key)
		{
			try
			{
				string url = string.Format("http://{0}{1}", this.Host, this.RequestUri);
				Uri uri = new Uri(url);
								
				string queryString = System.Web.HttpUtility.UrlDecode(uri.Query);
				if (queryString != null)
				{
					string myKey = key + "=";
					int i = queryString.IndexOf(myKey);
					if (i > -1)
					{
						queryString = queryString.Substring(i + myKey.Length);
						i = queryString.IndexOf("&");
						if (i == -1)
							return queryString;
						else
							return queryString.Substring(0, i);
					}
					return string.Empty;
				}
			}
			catch(Exception ex)
			{
				Debug.WriteLine(ex);
			}
			return string.Empty;
		}

		/// <summary>
		/// Initializes the needed headers
		/// </summary>
		private void InitHeaders()
		{
			// this.ContentEncoding = HttpContentEncodings.i
			this.ContentType = MIME.Text.Plain;
			this.ContentLength = 0;
			this.Connection = HttpConnections.Close;
			this.UserAgent = new CarbonClientProtocolVersion().ToString();
		}

		/// <summary>
		/// Sends the specified request and waits for a response using the specified connection.
		/// </summary>
		/// <param name="connection">The connection to use for communication</param>
		/// <param name="request">The request to send</param>
		/// <param name="exception">Any exception that is throw or encountered during the request/response session with the server</param>
		/// <returns></returns>
		public static HttpResponse GetResponse(
			HttpConnection connection, 
			HttpRequest request, 
			out Exception exception,
			EventHandler<HttpMessageProgressEventArgs> onSendProgress,
			EventHandler<HttpMessageProgressEventArgs> onRecvProgress,
			object stateObject)
		{
			#region Params Validation

			if (connection == null)
				throw new ArgumentNullException("connection");

			if (request == null)
				throw new ArgumentNullException("request");

			exception = null;

			#endregion

			// use the connection to get a response
			HttpResponse response = connection.GetResponse(request, onSendProgress, onRecvProgress, stateObject);

            exception = connection.GetLastException();

			// if there is no response, then obviously something went wrong, retrieve the last exception from the connection
			if (response == null)
			{
				if (exception != null)
					if (exception.GetType() == typeof(ThreadAbortException))
						throw exception;
			}
			
			// return the response
			return response;
		}

		/// <summary>
		/// Connects to the server specified by the endpoint, sends the request and waits for a response.
		/// </summary>
		/// <param name="ep">The address:port of the server</param>
		/// <param name="request">The request to send</param>
		/// <param name="exception">Any exception that is throw or encountered during the request/response session with the server</param>
		/// <param name="verbose">A flag that indicates the connection's verbosity level, true for more messages</param>
		/// <returns></returns>
		public static HttpResponse GetResponse(
			IPEndPoint ep,
			HttpRequest request, 
			out Exception exception, 
			bool verbose,
			EventHandler<HttpMessageProgressEventArgs> onSendProgress,
			EventHandler<HttpMessageProgressEventArgs> onRecvProgress,
			object stateObject)
		{
			#region Params Validation

			if (ep == null)
				throw new ArgumentNullException("ep");

			if (request == null)
				throw new ArgumentNullException("request");

			exception = null;

			#endregion

			// define a connection for this request/response session
			HttpConnection connection = null;

			try
			{
				// create a connection to the remote end point
				connection = new HttpConnection(ep, false, verbose, HttpOptions.SendTimeout, HttpOptions.RecvTimeout);
				
				// return a response from the server
				return HttpRequest.GetResponse(connection, request, out exception, onSendProgress, onRecvProgress, stateObject);
			}
            catch (ThreadAbortException)
            {
                
            }
			catch(Exception ex)
			{
                //Debug.WriteLine(ex);
				exception = ex;
			}
			finally
			{
				// always try and close the connect up afterwards
				if (connection != null)
					connection.Close();
			}
			return null;
		}

		/// <summary>
		/// Connects to the server specified by the address and port, sends the request and waits for a response.
		/// </summary>
		/// <param name="address">The address of the server, either an IPv4 address or Dns hostname</param>
		/// <param name="port">The port of the server</param>
		/// <param name="request">The request to send</param>
		/// <param name="exception">Any exception that is throw or encountered during the request/response session with the server</param>
		/// <param name="verbose">A flag that indicates the connection's verbosity level, true for more messages</param>
		/// <returns></returns>
		public static HttpResponse GetResponse(
			string address, 
			int port, 
			HttpRequest request, 
			out Exception exception, 
			bool verbose,
			EventHandler<AddressResolutionEventArgs> onResolvingAddress,
			EventHandler<HttpMessageProgressEventArgs> onSendProgress,
			EventHandler<HttpMessageProgressEventArgs> onRecvProgress,
			object stateObject)
		{
            exception = null;

			try
			{
				// parse the address using either IPv4 or Dns for hostnames into an end point with the port specified
				IPEndPoint ep = SocketUtilities.Resolve(address, port, null, onResolvingAddress, stateObject);
				
				// return a response from the server
				return HttpRequest.GetResponse(ep, request, out exception, verbose, onSendProgress, onRecvProgress, stateObject);
			}
            catch (ThreadAbortException)
            {
                
            }
			catch(Exception ex)
			{                
				exception = ex;
			}
			return null;
		}
	}
}
