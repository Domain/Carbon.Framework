using System;
using System.Collections.Generic;
using System.Text;

using Carbon.Common;

namespace Carbon.AutoUpdate.Common
{
	/// <summary>
	/// Defines the default AutoUpdate options
	/// </summary>
	public class DefaultAutoUpdateOptions : AutoUpdateOptions
	{
		/// <summary>
		/// Initializes a new instance of the DefaultAutoUpdateOptions class
		/// </summary>
		public DefaultAutoUpdateOptions()
			: base()
		{
			/*
			 * The default options provide the following behaviors unless modified
			 * at runtime by a behavior modifier.
			 * 
			 * 1. Automatically check for updates, but do not download them.
			 * 2. Once downloaded, do not automatically switch to the new version, continue to allow the user to run the current version.
			 * 3. Once downloaded, if the alternate path is set, copy the update to the specified location. By default the path is not set.			
			 * */
			base.BeginInit();

			base.AutomaticallyCheckForUpdates = true;
			base.AutomaticallyDownloadUpdates = false;
			base.AutomaticallyInstallUpdates = true;
			base.AutomaticallySwitchToNewVersion = false;
			base.AutomaticallyUpdateAlternatePath = true;
			base.AlternatePath = string.Empty;
			base.WebServiceUrl = CarbonConfig.AutoUpdateWebServiceUrl;

			base.EndInit();
		}
	}
}
