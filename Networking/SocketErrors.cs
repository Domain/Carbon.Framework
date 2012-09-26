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

namespace Carbon.Networking
{
	/// <summary>
	/// Defines the possible Winsock error codes.
	/// MSDN Topic: Windows Sockets Error Codes
	/// MSDN Library Url: ms-help://MS.MSDNQTR.2003FEB.1033/winsock/winsock/windows_sockets_error_codes_2.htm
	/// </summary>
	public enum SocketErrors
	{
		/// <summary>
		/// The base error code upon which all error codes are offset from.
		/// </summary>
		WSABASEERR				= (10000),
		
		/// <summary>
		/// Interrupted function call.  
		/// </summary>
		WSAEINTR                = (WSABASEERR+4),

		/// <summary>
		/// 
		/// </summary>
		WSAEBADF                = (WSABASEERR+9),

		/// <summary>
		/// Permission denied. 
		/// </summary>
		WSAEACCES               = (WSABASEERR+13),

		/// <summary>
		/// Bad address. 
		/// </summary>
		WSAEFAULT               = (WSABASEERR+14),

		/// <summary>
		/// Invalid argument. 
		/// </summary>
		WSAEINVAL               = (WSABASEERR+22),

		/// <summary>
		/// Too many open files. 
		/// </summary>
		WSAEMFILE               = (WSABASEERR+24),

		/// <summary>
		/// Resource temporarily unavailable. 
		/// </summary>
		WSAEWOULDBLOCK          = (WSABASEERR+35),

		/// <summary>
		/// Operation now in progress. 
		/// </summary>
		WSAEINPROGRESS          = (WSABASEERR+36),

		/// <summary>
		/// Operation already in progress. 
		/// </summary>
		WSAEALREADY             = (WSABASEERR+37),

		/// <summary>
		/// Socket operation on nonsocket. 
		/// </summary>
		WSAENOTSOCK             = (WSABASEERR+38),

		/// <summary>
		/// Destination address required. 
		/// </summary>
		WSAEDESTADDRREQ         = (WSABASEERR+39),

		/// <summary>
		/// Message too long. 
		/// </summary>
		WSAEMSGSIZE             = (WSABASEERR+40),

		/// <summary>
		/// Protocol wrong type for socket. 
		/// </summary>
		WSAEPROTOTYPE           = (WSABASEERR+41),

		/// <summary>
		/// Bad protocol option. 
		/// </summary>
		WSAENOPROTOOPT          = (WSABASEERR+42),

		/// <summary>
		/// Socket type not supported. 
		/// </summary>
		WSAEPROTONOSUPPORT      = (WSABASEERR+43),

		/// <summary>
		/// Socket type not supported. 
		/// </summary>
		WSAESOCKTNOSUPPORT      = (WSABASEERR+44),

		/// <summary>
		/// Operation not supported. 
		/// </summary>
		WSAEOPNOTSUPP           = (WSABASEERR+45),

		/// <summary>
		/// Protocol family not supported. 
		/// </summary>
		WSAEPFNOSUPPORT         = (WSABASEERR+46),

		/// <summary>
		/// Address family not supported by protocol family. 
		/// </summary>
		WSAEAFNOSUPPORT         = (WSABASEERR+47),

		/// <summary>
		/// Address already in use. 
		/// </summary>
		WSAEADDRINUSE           = (WSABASEERR+48),

		/// <summary>
		/// Cannot assign requested address. 
		/// </summary>
		WSAEADDRNOTAVAIL        = (WSABASEERR+49),

		/// <summary>
		/// Network is down. 
		/// </summary>
		WSAENETDOWN             = (WSABASEERR+50),

		/// <summary>
		/// Network is unreachable. 
		/// </summary>
		WSAENETUNREACH          = (WSABASEERR+51),

		/// <summary>
		/// Network dropped connection on reset. 
		/// </summary>
		WSAENETRESET            = (WSABASEERR+52),

		/// <summary>
		/// Software caused connection abort. 
		/// </summary>
		WSAECONNABORTED         = (WSABASEERR+53),

		/// <summary>
		/// Connection reset by peer. 
		/// </summary>
		WSAECONNRESET           = (WSABASEERR+54),

		/// <summary>
		/// No buffer space available. 
		/// </summary>
		WSAENOBUFS              = (WSABASEERR+55),

		/// <summary>
		/// Socket is already connected. 
		/// </summary>
		WSAEISCONN              = (WSABASEERR+56),

		/// <summary>
		/// Socket is not connected. 
		/// </summary>
		WSAENOTCONN             = (WSABASEERR+57),

		/// <summary>
		/// Cannot send after socket shutdown. 
		/// </summary>
		WSAESHUTDOWN            = (WSABASEERR+58),

		/// <summary>
		/// Unknown.
		/// </summary>
		WSAETOOMANYREFS         = (WSABASEERR+59),
		
		/// <summary>
		/// Connection timed out. 
		/// </summary>
		WSAETIMEDOUT            = (WSABASEERR+60),

		/// <summary>
		/// Connection refused. 
		/// </summary>
		WSAECONNREFUSED         = (WSABASEERR+61),

		/// <summary>
		/// Unknown.
		/// </summary>
		WSAELOOP                = (WSABASEERR+62),

		/// <summary>
		/// Unknown.
		/// </summary>
		WSAENAMETOOLONG         = (WSABASEERR+63),

		/// <summary>
		/// Host is down. 
		/// </summary>
		WSAEHOSTDOWN            = (WSABASEERR+64),

		/// <summary>
		/// No route to host. 
		/// </summary>
		WSAEHOSTUNREACH         = (WSABASEERR+65),

		/// <summary>
		/// Unknown.
		/// </summary>
		WSAENOTEMPTY            = (WSABASEERR+66),

		/// <summary>
		/// Too many processes. 
		/// </summary>
		WSAEPROCLIM             = (WSABASEERR+67),

		/// <summary>
		/// Unknown.
		/// </summary>
		WSAEUSERS               = (WSABASEERR+68),

		/// <summary>
		/// Unknown.
		/// </summary>
		WSAEDQUOT               = (WSABASEERR+69),

		/// <summary>
		/// Unknown.
		/// </summary>
		WSAESTALE               = (WSABASEERR+70),

		/// <summary>
		/// Unknown.
		/// </summary>
		WSAEREMOTE              = (WSABASEERR+71),

		/// <summary>
		/// Graceful shutdown in progress. 
		/// </summary>
		WSAEDISCON              = (WSABASEERR+101),

		/// <summary>
		/// Network subsystem is unavailable. 
		/// </summary>
		WSASYSNOTREADY          = (WSABASEERR+91),

		/// <summary>
		/// Winsock.dll version out of range. 
		/// </summary>
		WSAVERNOTSUPPORTED      = (WSABASEERR+92),

		/// <summary>
		/// Successful WSAStartup not yet performed. 
		/// </summary>
		WSANOTINITIALISED       = (WSABASEERR+93),
		
		/// <summary>
		/// Host not found. 
		/// </summary>
		WSAHOST_NOT_FOUND       = (WSABASEERR+1001),

		/// <summary>
		/// Nonauthoritative host not found. 
		/// </summary>
		WSATRY_AGAIN            = (WSABASEERR+1002),

		/// <summary>
		/// This is a nonrecoverable error. 
		/// </summary>
		WSANO_RECOVERY          = (WSABASEERR+1003),

		/// <summary>
		/// Valid name, no data record of requested type. 		
		/// </summary>
		WSANO_DATA              = (WSABASEERR+1004)		
	}
}
