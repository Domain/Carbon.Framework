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
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;
using System.Windows.Forms;

namespace Carbon.Networking.Http.Hosting
{
	/// <summary>
	/// Summary description for AspHost.
	/// </summary>
	public class AspHost : MarshalByRefObject, IDisposable
	{
		protected bool _disposed;
		protected AppDomain _appDomain;
		protected string _virtualDirectory;
		protected string _physicalDirectory;

		protected string _lowerCasedVirtualPath;
		protected string _lowerCasedVirtualPathWithTrailingSlash;
		protected string _installPath;
		protected string _physicalClientScriptPath;
		protected string _lowerCasedClientScriptPathWithTrailingSlashV10;
		protected string _lowerCasedClientScriptPathWithTrailingSlashV11;

        /// <summary>
        /// Initializes a new instance of the AspHost class
        /// </summary>				
		public AspHost()
		{
			this.VirtualDirectory = @"/";
			this.PhysicalDirectory = Application.StartupPath;

			_installPath = AspRuntime.AspNetInstallDirectory;
			_physicalClientScriptPath = _installPath + "\\asp.netclientfiles\\";

			string version4 = FileVersionInfo.GetVersionInfo(typeof(HttpRuntime).Module.FullyQualifiedName).FileVersion;
			string version3 = version4.Substring(0, version4.LastIndexOf('.'));
			_lowerCasedClientScriptPathWithTrailingSlashV10 = "/aspnet_client/system_web/" + version4.Replace('.', '_') + "/";
			_lowerCasedClientScriptPathWithTrailingSlashV11 = "/aspnet_client/system_web/" + version3.Replace('.', '_') + "/";
		}		

		#region IDisposable Members

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					this.Unload();
				}
				_disposed = true;
			}
		}

		#endregion

		/// <summary>
		/// Keep this object around forever
		/// </summary>
		/// <returns></returns>
		public override object InitializeLifetimeService()
		{
			return null;
		}

		/// <summary>
		/// Gets or sets this host's virtual directory
		/// </summary>
		public string VirtualDirectory
		{
			get
			{
				return _virtualDirectory;
			}
			set
			{
				_virtualDirectory = value;
				_lowerCasedVirtualPath = CultureInfo.InvariantCulture.TextInfo.ToLower(_virtualDirectory);
				_lowerCasedVirtualPathWithTrailingSlash = value.EndsWith("/") ? value : value + "/";
				_lowerCasedVirtualPathWithTrailingSlash = CultureInfo.InvariantCulture.TextInfo.ToLower(_lowerCasedVirtualPathWithTrailingSlash);
			}
		}

		/// <summary>
		/// Gets or sets this host's physical directory to which the virtual directory will be mapped
		/// </summary>
		public string PhysicalDirectory
		{
			get
			{
				return _physicalDirectory;
			}
			set
			{
				_physicalDirectory = value;
			}
		}

		/// <summary>
		/// Gets or sets the AppDomain that this host is executing in
		/// </summary>
		public AppDomain AppDomain
		{
			get
			{
				return _appDomain;
			}
			set
			{
				_appDomain = value;								
			}
		}

		public string NormalizedVirtualPath
		{
			get
			{
				return _lowerCasedVirtualPathWithTrailingSlash;
			}
		}

		public string PhysicalClientScriptPath
		{
			get
			{
				return _physicalClientScriptPath;
			}
		}

		public bool IsVirtualPathInApp(String path) 
		{
			bool isClientScriptPath;
			String clientScript;
			return IsVirtualPathInApp(path, out isClientScriptPath, out clientScript);
		}

		public bool IsVirtualPathInApp(String path, out bool isClientScriptPath, out String clientScript) 
		{
			isClientScriptPath = false;
			clientScript = null;

			if (path == null)
				return false;

			if (_virtualDirectory == "/" && path.StartsWith("/")) 
			{
				if (path.StartsWith(_lowerCasedClientScriptPathWithTrailingSlashV10)) 
				{
					isClientScriptPath = true;
					clientScript = path.Substring(_lowerCasedClientScriptPathWithTrailingSlashV10.Length);
				}

				if (path.StartsWith(_lowerCasedClientScriptPathWithTrailingSlashV11)) 
				{
					isClientScriptPath = true;
					clientScript = path.Substring(_lowerCasedClientScriptPathWithTrailingSlashV11.Length);
				}

				return true;
			}

			path = CultureInfo.InvariantCulture.TextInfo.ToLower(path);

			if (path.StartsWith(_lowerCasedVirtualPathWithTrailingSlash))
				return true;

			if (path == _lowerCasedVirtualPath)
				return true;

			if (path.StartsWith(_lowerCasedClientScriptPathWithTrailingSlashV10)) 
			{
				isClientScriptPath = true;
				clientScript = path.Substring(_lowerCasedClientScriptPathWithTrailingSlashV10.Length);
				return true;
			}

			if (path.StartsWith(_lowerCasedClientScriptPathWithTrailingSlashV11)) 
			{
				isClientScriptPath = true;
				clientScript = path.Substring(_lowerCasedClientScriptPathWithTrailingSlashV11.Length);
				return true;
			}

			return false;
		}

		public bool IsVirtualPathAppPath(String path) 
		{
			if (path == null)
				return false;

			path = CultureInfo.InvariantCulture.TextInfo.ToLower(path);
			return (path == _lowerCasedVirtualPath || path == _lowerCasedVirtualPathWithTrailingSlash);
		}

		/// <summary>
		/// Unloads the AspHost's AppDomain from the CLR and stops the host's execution
		/// </summary>
		public void Unload()
		{
			if (_appDomain != null)
			{
				AppDomain.Unload(_appDomain);
				_appDomain = null;
			}
		}

		/// <summary>
		/// Processes the request using the specified connection and the asp runtime
		/// </summary>
		/// <param name="connection"></param>
		/// <param name="e"></param>
		public void ProcessRequest(HttpConnection connection, ref HttpRequestCancelEventArgs e)
		{			
			// if the request cannot be handled, simply bail out (this will cover the known http methods)
			if (!this.CanHandleRequest(e.Request))
				return;
			
			// create a new asp worker request
			AspWorkerRequest aspRequest = new AspWorkerRequest(this, connection, e.Request);

			// use the asp runtime to process the request
			HttpRuntime.ProcessRequest(aspRequest);

			// return the response that the asp request generated
			e.Response = aspRequest.Response;
		}

		/// <summary>
		/// Determines if the request can be handled based on the request's method
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		private bool CanHandleRequest(HttpRequest request)
		{			
			switch(request.Method)
			{
				case HttpMethods.Options:
				case HttpMethods.Get:
				case HttpMethods.Head:
				case HttpMethods.Post:
				case HttpMethods.Put:
				case HttpMethods.Delete:
				case HttpMethods.Trace:
					return true;					
			};

			return false;
		}		
	}
}
