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
using System.IO;
using System.Net;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

using Carbon;
using Carbon.AutoUpdate.Common;
using Carbon.AutoUpdate.Common.Xml;
using Carbon.UI;

namespace Carbon.AutoUpdate
{
	/// <summary>
	/// Provides an AutoUpdateDownloader implementation that can query a web service and download updates
	/// from a web server. This class is the primary downloader used by the AutoUpdate system.
	/// </summary>
	public sealed class HttpAutoUpdateDownloader : AutoUpdateDownloader
	{		
		private const string MY_TRACE_CATEGORY = @"'HttpAutoUpdateDownloader'";

		/// <summary>
		/// Initializes a new instance of the HttpAutoUpdateDownloader class
		/// </summary>
		public HttpAutoUpdateDownloader() 
			: base()
		{
			
		}

		/// <summary>
		/// Instructs the AutoUpdateDownloader to query for the latest version available 
		/// </summary>
		/// <param name="progressViewer">The progress viewer by which progress should be displayed</param>
		/// <param name="options">The options that affect this downloader</param>
		/// <param name="productToUpdate">The product descriptor for the product that should be updated</param>
		/// <param name="updateAvailable">The download descriptor that describes the download that could potentially occur</param>
		/// <returns></returns>
		public override bool QueryLatestVersion(IProgressViewer progressViewer, AutoUpdateOptions options, AutoUpdateProductDescriptor productToUpdate, out AutoUpdateDownloadDescriptor updateAvailable)
		{
			updateAvailable = null;
			
			try
			{
				// create a manual web service proxy based on the url specified in the options
				Debug.WriteLine(string.Format("Creating a web service proxy to the following url.\n\tThe web service url is '{0}'.", options.WebServiceUrl), MY_TRACE_CATEGORY);			
				AutoUpdateWebServiceProxy service = new AutoUpdateWebServiceProxy(options.WebServiceUrl);

				// use the web service to query for updates
				Debug.WriteLine(string.Format("Querying the web service for the latest version of '{0}'.\n\tThe current product's version is '{1}'.\n\tThe current product's id is '{2}'.\n\tThe web service url is '{3}'.", productToUpdate.Name, productToUpdate.Version.ToString(), productToUpdate.Id, options.WebServiceUrl), MY_TRACE_CATEGORY);			
				XmlNode node = service.QueryLatestVersion(productToUpdate.Name, productToUpdate.Version.ToString(), productToUpdate.Id);
				
				// if the service returned no results, then there is no update availabe
				if (node == null)
				{
					// bail out 
					Debug.WriteLine(string.Format("No updates are available from the web service at '{0}' for this product.", options.WebServiceUrl), MY_TRACE_CATEGORY);
					return false;
				}

				// otherwise create a reader and try and read the xml from the xml node returned from the web service
				XmlAutoUpdateManifestReader reader = new XmlAutoUpdateManifestReader(node);

				// using the reader we can recreate the manifeset from the xml
				AutoUpdateManifest manifest = reader.Read();	

				/*
				* now create a download descriptor that says, yes we have found an update.
				* we are capable of downloading it, according to these options.
				* the autoupdate manager will decide which downloader to use to download the update
				* */
				updateAvailable = new AutoUpdateDownloadDescriptor(manifest, this, options);
				
				// just to let everyone know that there is a version available
				Debug.WriteLine(string.Format("Version '{0}' of '{1}' is available for download.\n\tThe download url is '{2}'.\n\tThe size of the download is {3}.", updateAvailable.Manifest.Product.Version.ToString(), updateAvailable.Manifest.Product.Name, updateAvailable.Manifest.UrlOfUpdate, this.FormatFileLengthForDisplay(updateAvailable.Manifest.SizeOfUpdate)), MY_TRACE_CATEGORY);

				// we've successfully queried for the latest version of the product to update
				return true;
			}
			catch(ThreadAbortException)
			{

			}
			catch (WebException ex)
			{
                Debug.WriteLine(ex.Message, MY_TRACE_CATEGORY);
			}
			return false;
		}
	}
}
