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
using System.Net;

namespace Carbon.Networking.Http
{
	/*
	  "100"  ; Section 10.1.1: Continue
    | "101"  ; Section 10.1.2: Switching Protocols
    | "200"  ; Section 10.2.1: OK
    | "201"  ; Section 10.2.2: Created
    | "202"  ; Section 10.2.3: Accepted
    | "203"  ; Section 10.2.4: Non-Authoritative Information
    | "204"  ; Section 10.2.5: No Content
    | "205"  ; Section 10.2.6: Reset Content
    | "206"  ; Section 10.2.7: Partial Content
	| "300"  ; Section 10.3.1: Multiple Choices
	| "301"  ; Section 10.3.2: Moved Permanently
	| "302"  ; Section 10.3.3: Found
	| "303"  ; Section 10.3.4: See Other
	| "304"  ; Section 10.3.5: Not Modified
	| "305"  ; Section 10.3.6: Use Proxy
	| "307"  ; Section 10.3.8: Temporary Redirect
	| "400"  ; Section 10.4.1: Bad Request
	| "401"  ; Section 10.4.2: Unauthorized
	| "402"  ; Section 10.4.3: Payment Required
	| "403"  ; Section 10.4.4: Forbidden
	| "404"  ; Section 10.4.5: Not Found
	| "405"  ; Section 10.4.6: Method Not Allowed
	| "406"  ; Section 10.4.7: Not Acceptable
	| "407"  ; Section 10.4.8: Proxy Authentication Required
	| "408"  ; Section 10.4.9: Request Time-out
	| "409"  ; Section 10.4.10: Conflict
	| "410"  ; Section 10.4.11: Gone
	| "411"  ; Section 10.4.12: Length Required
	| "412"  ; Section 10.4.13: Precondition Failed
	| "413"  ; Section 10.4.14: Request Entity Too Large
	| "414"  ; Section 10.4.15: Request-URI Too Large
	| "415"  ; Section 10.4.16: Unsupported Media Type
	| "416"  ; Section 10.4.17: Requested range not satisfiable
	| "417"  ; Section 10.4.18: Expectation Failed
	| "500"  ; Section 10.5.1: Internal Server Error
	| "501"  ; Section 10.5.2: Not Implemented
	| "502"  ; Section 10.5.3: Bad Gateway
	| "503"  ; Section 10.5.4: Service Unavailable
	| "504"  ; Section 10.5.5: Gateway Time-out
	| "505"  ; Section 10.5.6: HTTP Version not supported

	*/

	[Serializable()]
	public class ContinueStatus : HttpStatus
	{
		public ContinueStatus() 
            : base(HttpStatusCode.Continue, "Continue")
		{
		}
	}

	[Serializable()]
	public class SwitchingProtocolsStatus : HttpStatus
	{
        public SwitchingProtocolsStatus()
            : base(HttpStatusCode.SwitchingProtocols, "Switching Protocols")
		{
		}
	}

	[Serializable()]
	public class OkStatus : HttpStatus
	{
		public OkStatus()
            : base(HttpStatusCode.OK, "OK")
		{
		}
	}

	[Serializable()]
	public class CreatedStatus : HttpStatus
	{
        public CreatedStatus()
            : base(HttpStatusCode.Created, "Created")
		{
		}
	}

	[Serializable()]
	public class AcceptedStatus : HttpStatus
	{
        public AcceptedStatus()
            : base(HttpStatusCode.Accepted, "Accepted")
		{
		}
	}

	[Serializable()]
	public class NonAuthoritativeInformationStatus : HttpStatus
	{
		public NonAuthoritativeInformationStatus()
            : base(HttpStatusCode.NonAuthoritativeInformation, "Non-Authoritative Information")
		{
		}
	}

	[Serializable()]
	public class NoContentStatus : HttpStatus
	{
        public NoContentStatus()
            : base(HttpStatusCode.NoContent, "No Content")
		{
		}
	}

	[Serializable()]
	public class ResetContentStatus : HttpStatus
	{
		public ResetContentStatus()
            : base(HttpStatusCode.ResetContent, "Reset Content")
		{
		}
	}

	[Serializable()]
	public class PartialContentStatus : HttpStatus
	{
        public PartialContentStatus()
            : base(HttpStatusCode.PartialContent, "Partial Content")
		{			
		}
	}

	[Serializable()]
	public class MultipleChoicesStatus : HttpStatus
	{
        public MultipleChoicesStatus()
            : base(HttpStatusCode.MultipleChoices, "Multiple Choices")
		{
		}
	}

	[Serializable()]
	public class MovedPermanentlyStatus : HttpStatus
	{
		public MovedPermanentlyStatus()
            : base(HttpStatusCode.MovedPermanently, "Moved Permanently")
		{
		}
	}

	[Serializable()]
	public class FoundStatus : HttpStatus
	{
		public FoundStatus()
            : base(HttpStatusCode.Found, "Found")
		{
		}
	}

	[Serializable()]
	public class SeeOtherStatus : HttpStatus
	{
		public SeeOtherStatus()
            : base(HttpStatusCode.SeeOther, "See Other")
		{
		}
	}

	[Serializable()]
	public class NotModifiedStatus : HttpStatus
	{
		public NotModifiedStatus()
            : base(HttpStatusCode.NotModified, "Not Modified")
		{
		}
	}

	[Serializable()]
	public class UseProxyStatus : HttpStatus
	{
        public UseProxyStatus()
            : base(HttpStatusCode.UseProxy, "Use Proxy")
		{
		}
	}

	[Serializable()]
	public class TemporaryRedirectStatus : HttpStatus
	{
        public TemporaryRedirectStatus()
            : base(HttpStatusCode.TemporaryRedirect, "Temporary Redirect")
		{
		}
	}

	[Serializable()]
	public class BadRequestStatus : HttpStatus
	{
        public BadRequestStatus()
            : base(HttpStatusCode.BadRequest, "Bad Request")
		{
		}
	}

	[Serializable()]
	public class UnauthorizedStatus : HttpStatus
	{
        public UnauthorizedStatus()
            : base(HttpStatusCode.Unauthorized, "Unauthorized")
		{
		}
	}

	[Serializable()]
	public class PaymentRequiredStatus : HttpStatus
	{
        public PaymentRequiredStatus()
            : base(HttpStatusCode.PaymentRequired, "Payment Required")
		{
		}
	}

	[Serializable()]
	public class ForbiddenStatus : HttpStatus
	{
        public ForbiddenStatus()
            : base(HttpStatusCode.Forbidden, "Forbidden")
		{
		}
	}

	[Serializable()]
	public class NotFoundStatus : HttpStatus
	{
        public NotFoundStatus()
            : base(HttpStatusCode.NotFound, "Not Found")
		{
		}
	}

	[Serializable()]
	public class MethodNotAllowedStatus : HttpStatus
	{
        public MethodNotAllowedStatus()
            : base(HttpStatusCode.MethodNotAllowed, "Method Not Allowed")
		{
		}
	}

	[Serializable()]
	public class NotAcceptableStatus : HttpStatus
	{
        public NotAcceptableStatus()
            : base(HttpStatusCode.NotAcceptable, "Not Acceptable")
		{
		}
	}

	[Serializable()]
	public class ProxyAuthenticationRequiredStatus : HttpStatus
	{
		public ProxyAuthenticationRequiredStatus()
            : base(HttpStatusCode.ProxyAuthenticationRequired, "Proxy Authentication Required")
		{
		}
	}

	[Serializable()]
	public class RequestTimeoutStatus : HttpStatus
	{
        public RequestTimeoutStatus()
            : base(HttpStatusCode.RequestTimeout, "Request Time-out")
		{
		}
	}

	[Serializable()]
	public class ConflictStatus : HttpStatus
	{
        public ConflictStatus()
            : base(HttpStatusCode.Conflict, "Conflict")
		{
		}
	}

	[Serializable()]
	public class GoneStatus : HttpStatus
	{
        public GoneStatus()
            : base(HttpStatusCode.Gone, "Gone")
		{
		}
	}

	[Serializable()]
	public class LengthRequiredStatus : HttpStatus
	{
        public LengthRequiredStatus()
            : base(HttpStatusCode.LengthRequired, "Length Required")
		{
		}
	}

	[Serializable()]
	public class PreconditionFailedStatus : HttpStatus
	{
        public PreconditionFailedStatus()
            : base(HttpStatusCode.PreconditionFailed, "Precondition Failed")
		{
		}
	}

	[Serializable()]
	public class RequestEntityTooLargeStatus : HttpStatus
	{
        public RequestEntityTooLargeStatus()
            : base(HttpStatusCode.RequestEntityTooLarge, "Request-Entity Too Large")
		{
		}
	}

	[Serializable()]
	public class RequestUriTooLargeStatus : HttpStatus
	{
        public RequestUriTooLargeStatus()
            : base(HttpStatusCode.RequestUriTooLong, "Request-Uri Too Large")
		{
		}
	}

	[Serializable()]
	public class UnsupportedMediaTypeStatus : HttpStatus
	{
        public UnsupportedMediaTypeStatus()
            : base(HttpStatusCode.UnsupportedMediaType, "Unsupported Media Type")
		{
		}
	}

	[Serializable()]
	public class RequestedRangeNotSatisfiableStatus : HttpStatus
	{
        public RequestedRangeNotSatisfiableStatus()
            : base(HttpStatusCode.RequestedRangeNotSatisfiable, "Requested range not satisfiable")
		{
		}
	}

	[Serializable()]
	public class ExpectationFailedStatus : HttpStatus
	{
        public ExpectationFailedStatus()
            : base(HttpStatusCode.ExpectationFailed, "Expectation Failed")
		{
		}
	}

	[Serializable()]
	public class InternalServerErrorStatus : HttpStatus
	{
        public InternalServerErrorStatus()
            : base(HttpStatusCode.InternalServerError, "Internal Server Error")
		{
		}
	}

	[Serializable()]
	public class NotImplementedStatus : HttpStatus
	{
        public NotImplementedStatus()
            : base(HttpStatusCode.NotImplemented, "Not Implemented")
		{
		}
	}

	[Serializable()]
	public class BadGatewayStatus : HttpStatus
	{
        public BadGatewayStatus()
            : base(HttpStatusCode.BadGateway, "Bad Gateway")
		{
		}
	}

	[Serializable()]
	public class ServiceUnavailableStatus : HttpStatus
	{
        public ServiceUnavailableStatus()
            : base(HttpStatusCode.ServiceUnavailable, "Service Unavailable")
		{
		}
	}

	[Serializable()]
	public class GatewayTimeoutStatus : HttpStatus
	{
        public GatewayTimeoutStatus()
            : base(HttpStatusCode.GatewayTimeout, "Gateway Time-out")
		{
		}
	}

	[Serializable()]
	public class HttpVersionNotSupportedStatus : HttpStatus
	{
        public HttpVersionNotSupportedStatus()
            : base(HttpStatusCode.HttpVersionNotSupported, "HTTP Version not supported")
		{
		}
	}
}
