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
using System.IO;
using System.Runtime.Remoting;
using System.Web;
using System.Web.Hosting;
using Microsoft.Win32;

namespace Carbon.Networking.Http.Hosting
{
	/// <summary>
	/// Summary description for AspRuntime.
	/// </summary>
	public class AspRuntime
	{
		private static string _aspNetInstallDirectory;

		static AspRuntime()
		{
			// load the asp.net install path, or configure asp.net if it is not already configured
			_aspNetInstallDirectory = AspRuntime.GetAspRuntimeInstallPathAndConfigureAspRuntimeIfNeeded();
		}
	
		/// <summary>
		/// Returns the path to the directory where the ASP.NET runtime lives
		/// </summary>
		public static string AspNetInstallDirectory
		{
			get
			{
				return _aspNetInstallDirectory;
			}
		}

		/// <summary>
		/// Makes sure the ASP.NET registry keys exist, and if IIS was never registered will registers teh aspnet_isapi.dll
		/// </summary>
		/// <returns>The path where ASP.NET is installed</returns>
		private static string GetAspRuntimeInstallPathAndConfigureAspRuntimeIfNeeded()
		{
			const string aspNetKeyName = @"Software\Microsoft\ASP.NET";

			string installPath = null;
			RegistryKey aspNetKey = null;
			RegistryKey aspNetVersionKey = null;
			RegistryKey frameworkKey = null;			

			try
			{
				// get the version corresponding to the System.Web.dll currently loaded
				string aspNetVersion = FileVersionInfo.GetVersionInfo(typeof(HttpRuntime).Module.FullyQualifiedName).FileVersion;
				string aspNetVersionKeyName = Path.Combine(aspNetKeyName, aspNetVersion);

				// non 1.0 names should have 0 QFE in the registry
				if (!aspNetVersion.StartsWith("1.0.*"))
					aspNetVersionKeyName = aspNetVersionKeyName.Substring(0, aspNetVersionKeyName.LastIndexOf('.') + 1) + "0";

				// check if the subkey with version number already exists
				aspNetVersionKey = Registry.LocalMachine.OpenSubKey(aspNetVersionKeyName);

				if (aspNetVersionKey != null)
				{
					installPath = (string)aspNetVersionKey.GetValue("Path");
				}
				else
				{
					// try and open the key
					aspNetKey = Registry.LocalMachine.OpenSubKey(aspNetKeyName);

					// if it does not exist
					if (aspNetKey == null)
					{
						// create it
						aspNetKey = Registry.LocalMachine.CreateSubKey(aspNetKeyName);

						// add the root version value
						aspNetKey.SetValue("RootVer", aspNetVersion);
					}

					
					string versionDirName = "v" + aspNetVersion.Substring(0, aspNetVersion.LastIndexOf('.'));

					// install directory from "InstallRoot" under ".NETFramework" key
					frameworkKey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\.NETFramework");
					string rootDir = (string)frameworkKey.GetValue("InstallRoot");
					//					if (rootDir.EndsWith("\\"))
					//						rootDir = rootDir.Substring(0, rootDir.Length - 1);
					rootDir = rootDir.TrimEnd('\\');

					// create the version subkey
					aspNetVersionKey = Registry.LocalMachine.CreateSubKey(aspNetVersionKeyName);

					// install path
					installPath = Path.Combine(rootDir, versionDirName);

					// set the path adn the dllfullpath
					aspNetVersionKey.SetValue("Path", installPath);
					aspNetVersionKey.SetValue("DllFullPath", Path.Combine(installPath, @"aspnet_isapi.dll"));
				}				
			}
			catch(Exception ex)
			{
				Debug.WriteLine(ex);
			}
			finally
			{
				if (aspNetVersionKey != null)
					aspNetVersionKey.Close();
				
				if (aspNetKey != null)
					aspNetKey.Close();
				
				if (frameworkKey != null)
					frameworkKey.Close();
			}

			return installPath;
		}

		/// <summary>
		/// Creates and initializes a new AspHost instance on it's own AppDomain
		/// </summary>
		/// <param name="virtualDirectory"></param>
		/// <param name="physicalDirectory"></param>
		/// <param name="applicationBaseDirectory"></param>
		/// <param name="configurationFile"></param>
		/// <returns></returns>
		public static AspHost CreateAspHost(
			string virtualDirectory,
			string physicalDirectory,
			string applicationBaseDirectory,
			string configurationFile)
		{
			if (!physicalDirectory.EndsWith("\\"))
				physicalDirectory += "\\";
			
			string domainId = string.Format("AspHost_{0}", DateTime.Now.ToString().GetHashCode().ToString("x"));
			string appName = (virtualDirectory + physicalDirectory).GetHashCode().ToString("x");
			AppDomainSetup appDomainSetup = new AppDomainSetup();

			appDomainSetup.ApplicationName = appName;
			appDomainSetup.ConfigurationFile = configurationFile;
			if (!HttpUtils.IsEmptryString(applicationBaseDirectory) && !HttpUtils.IsNullString(applicationBaseDirectory))
				appDomainSetup.ApplicationBase = applicationBaseDirectory;

			AppDomain appDomain = AppDomain.CreateDomain(domainId, null, appDomainSetup);
			appDomain.SetData(@".appDomain", @"*");
			appDomain.SetData(@".appPath", physicalDirectory);
			appDomain.SetData(@".appVPath", virtualDirectory);
			appDomain.SetData(@".domainId", domainId);
			appDomain.SetData(@".hostingVirtualPath", virtualDirectory);
			appDomain.SetData(@".hostingInstallDir", HttpRuntime.AspInstallDirectory);

			Debug.WriteLine(string.Format("Creating 'AspHost' for requests in '{0}' mapped to '{1}'", virtualDirectory, physicalDirectory), "'AspRuntime'");

			ObjectHandle hInstance = appDomain.CreateInstance(typeof(AspHost).Module.Assembly.FullName, typeof(AspHost).FullName);
			AspHost host = (AspHost)hInstance.Unwrap();
			host.VirtualDirectory = virtualDirectory;
			host.PhysicalDirectory = physicalDirectory;
			host.AppDomain = appDomain;
			
			return host;
		}
	}
}
