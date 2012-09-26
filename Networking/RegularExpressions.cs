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
using System.Text.RegularExpressions;

namespace Carbon.Networking
{
    /// <summary>
    /// Defines the base Carbon Regex class which configures the Regex options for all regular expressions
    /// in the Carbon.Networking namespace.
    /// </summary>
    public class RegexBase : Regex
    {
        /// <summary>
        /// Initializes a new instance of the RegexBase class.
        /// </summary>
        /// <param name="pattern">The Regex pattern that will be used during matching.</param>
        public RegexBase(string pattern)
            : base(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase)
        {

        }
    }

    /// <summary>
    /// Defines a Regex class for matching HTML Anchor tags.
    /// </summary>
    public sealed class HtmlAnchorTagRegex : RegexBase
    {
        /// <summary>
        /// <a.*href\s*=\s*(?:(?:\"(?<Url>[^\"]*)\")|(?:\'(?<Url>[^\']*)\')|(?:(?<Url>[^>\s]*)))     
        /// </summary>        
        public readonly static string Pattern = "<a.*href\\s*=\\s*(?:(?:\\\"(?<Url>[^\\\"]*)\\\")|(?:\\\'(?<Url>[^\\\']*)\\\')|(?:(?<Url>[^>\\s]*)))";
        public readonly static string UrlMatchGroupName = "Url";

        /// <summary>
		/// Initializes a new instance of the HtmlAnchorTagRegex class.
        /// </summary>
        public HtmlAnchorTagRegex()
            : base(Pattern)
        {

        }
    }

	/// <summary>
	/// Defines a Regex class for matching HTML Base tags.
	/// </summary>
	public sealed class HtmlBaseTagRegex : RegexBase
	{
		/// <summary>
		/// <a.*href\s*=\s*(?:(?:\"(?<Url>[^\"]*)\")|(?:\'(?<Url>[^\']*)\')|(?:(?<Url>[^>\s]*)))		
		/// </summary>        
		public readonly static string Pattern = "<base.*href\\s*=\\s*(?:(?:\\\"(?<Url>[^\\\"]*)\\\")|(?:\\\'(?<Url>[^\\\']*)\\\')|(?:(?<Url>[^>\\s]*)))";
		public readonly static string UrlMatchGroupName = "Url";

		/// <summary>
		/// Initializes a new instance of the HtmlBaseTagRegex class.
		/// </summary>
		public HtmlBaseTagRegex()
			: base(Pattern)
		{

		}
	}

	/// <summary>
	/// Defines a Regex class for matching HTML Form tags.
	/// </summary>
	public sealed class HtmlFormTagRegex : RegexBase
	{
		/// <summary>
		/// <a.*href\s*=\s*(?:(?:\"(?<Url>[^\"]*)\")|(?:\'(?<Url>[^\']*)\')|(?:(?<Url>[^>\s]*)))		
		/// </summary>        
		public readonly static string Pattern = "<form.*action\\s*=\\s*(?:(?:\\\"(?<Url>[^\\\"]*)\\\")|(?:\\\'(?<Url>[^\\\']*)\\\')|(?:(?<Url>[^>\\s]*)))";
		public readonly static string UrlMatchGroupName = "Url";

		/// <summary>
		/// Initializes a new instance of the HtmlBaseTagRegex class.
		/// </summary>
		public HtmlFormTagRegex()
			: base(Pattern)
		{

		}
	}

	/// <summary>
	/// Defines a Regex class for matching HTML Img tags.
	/// </summary>
	public sealed class HtmlImgTagRegex : RegexBase
	{
		// <img src="location.png" />
				/// <summary>
		/// <a.*href\s*=\s*(?:(?:\"(?<Url>[^\"]*)\")|(?:\'(?<Url>[^\']*)\')|(?:(?<Url>[^>\s]*)))		
		/// </summary>        
		public readonly static string Pattern = "<img.*src\\s*=\\s*(?:(?:\\\"(?<Url>[^\\\"]*)\\\")|(?:\\\'(?<Url>[^\\\']*)\\\')|(?:(?<Url>[^>\\s]*)))";
		public readonly static string UrlMatchGroupName = "Url";

		/// <summary>
		/// Initializes a new instance of the HtmlBaseTagRegex class.
		/// </summary>
		public HtmlImgTagRegex()
			: base(Pattern)
		{

		}
	}

    /// <summary>
    /// Defines a Regex class for matching Urls.
    /// </summary>
    public sealed class UrlRegex : RegexBase
    {
        /// <summary>
        /// (?<Protocol>https?://)?(?<Host>[\w.]+)(?<Port>:\d{0,5})?(?<Resource>/?\S*)
        /// or from RegexLib.com an alternative:
        /// (?:(?<protocol>http(?:s?)|ftp)(?:\:\/\/)) (?:(?<usrpwd>\w+\:\w+)(?:\@))? (?<domain>[^/\r\n\:]+)? (?<port>\:\d+)? (?<path>(?:\/.*)*\/)? (?<filename>.*?\.(?<ext>\w{2,4}))? (?<qrystr>\??(?:\w+\=[^\#]+)(?:\&?\w+\=\w+)*)* (?<bkmrk>\#.*)?
        /// </summary>        
        public readonly static string Pattern = @"(?<Protocol>https?://)?(?<Host>[\w.]+)(?<Port>:\d{0,5})?(?<Resource>/?\S*)";
        public readonly static string ProtocolMatchGroupName = "Protocol";
        public readonly static string HostMatchGroupName = "Host";
        public readonly static string PortMatchGroupName = "Port";
        public readonly static string ResourceMatchGroupName = "Resource";

        /// <summary>
        /// Initializes a new instance of the UrlRegex class.
        /// </summary>
        public UrlRegex()
            : base(Pattern)
        {

        }
    }

    /// <summary>
    /// Defines a Regex class for matching IP Addresses.
    /// </summary>
    public sealed class IPAddressRegex : RegexBase
    {
        /// <summary>
        /// (?<First>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Second>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Third>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Fourth>2[0-4]\d|25[0-5]|[01]?\d\d?)
        /// </summary>
        public readonly static string Pattern = @"(?<First>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Second>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Third>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Fourth>2[0-4]\d|25[0-5]|[01]?\d\d?)";

        public IPAddressRegex()
            : base(Pattern)
        {

        }
    }

    /// <summary>
    /// Defines a Regex class for matching fully qualified domain names.
    /// </summary>
    public sealed class TopLevelDomainRegex : RegexBase
    {
        /// <summary>
        /// (?<Host>[a-zA-Z0-9\-\.]+\.(aero|biz|com|coop|edu|gov|info|int|mil|museum|name|net|org|pro|ac|ad|ae|af|ag|ai|al|am|an|ao|aq|ar|as|at|au|aw|az|ba|bb|bd|be|bf|bg|bh|bi|bi|bm|bn|bo|br|bs|bt|bv|bw|by|bz|ca|cc|cd|cf|cg|ch|ci|ck|cl|cm|cn|co|cr|cu|cv|cx|cy|cz|de|dj|dk|dm|do|dz|ec|ee|eg|eh|er|es|et|fi|fj|fk|fm|fo|fr|ga|gd|ge|gf|gg|gh|gi|gl|gm|gn|gp|gq|gr|gs|gt|gu|gw|gy|hk|hm|hn|hr|ht|hu|id|ie|il|im|in|io|iq|ir|is|it|je|jm|jo|jp|ke|kg|kh|kj|km|kn|kp|kr|kw|ky|kz|la|lb|lc|li|lk|lr|ls|lt|lu|lv|ly|ma|mc|md|mg|mh|mk|ml|mm|mn|mo|mp|mq|mr|ms|mt|mu|mv|mw|mx|my|mz|na|nc|ne|nf|ng|ni|nl|no|np|nr|nu|nz|om|pa|pe|pf|pg|ph|pk|pl|pm|pn|pr|ps|pt|pw|py|qa|re|ro|ru|rw|sa|sb|sc|sd|se|sg|sh|si|sj|sk|sl|sm|sn|so|sr|st|sv|sy|sz|tc|td|tf|tg|th|tj|tk|tm|tn|to|tp|tr|tt|tv|tw|tz|ua|uh|uk|um|us|uy|uz|va|vc|ve|vg|vi|vn|vu|wf|ws|ye|yt|yu|za|zm|zw))
        /// </summary>
        public readonly static string Pattern = @"\S+\.(?:aero|biz|com|coop|edu|gov|info|int|mil|museum|name|net|org|pro|ac|ad|ae|af|ag|ai|al|am|an|ao|aq|ar|as|at|au|aw|az|ba|bb|bd|be|bf|bg|bh|bi|bi|bm|bn|bo|br|bs|bt|bv|bw|by|bz|ca|cc|cd|cf|cg|ch|ci|ck|cl|cm|cn|co|cr|cu|cv|cx|cy|cz|de|dj|dk|dm|do|dz|ec|ee|eg|eh|er|es|et|fi|fj|fk|fm|fo|fr|ga|gd|ge|gf|gg|gh|gi|gl|gm|gn|gp|gq|gr|gs|gt|gu|gw|gy|hk|hm|hn|hr|ht|hu|id|ie|il|im|in|io|iq|ir|is|it|je|jm|jo|jp|ke|kg|kh|kj|km|kn|kp|kr|kw|ky|kz|la|lb|lc|li|lk|lr|ls|lt|lu|lv|ly|ma|mc|md|mg|mh|mk|ml|mm|mn|mo|mp|mq|mr|ms|mt|mu|mv|mw|mx|my|mz|na|nc|ne|nf|ng|ni|nl|no|np|nr|nu|nz|om|pa|pe|pf|pg|ph|pk|pl|pm|pn|pr|ps|pt|pw|py|qa|re|ro|ru|rw|sa|sb|sc|sd|se|sg|sh|si|sj|sk|sl|sm|sn|so|sr|st|sv|sy|sz|tc|td|tf|tg|th|tj|tk|tm|tn|to|tp|tr|tt|tv|tw|tz|ua|uh|uk|um|us|uy|uz|va|vc|ve|vg|vi|vn|vu|wf|ws|ye|yt|yu|za|zm|zw)/?$"; //@"[\w.]+.(aero|biz|com|coop|edu|gov|info|int|mil|museum|name|net|org|pro)";

        public TopLevelDomainRegex()
            : base(Pattern)
        {

        }
    }
}
