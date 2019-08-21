using System;
using System.Net;

namespace Simple.Api.Common
{
	public class HttpException : Exception
	{
		public HttpException(string errorMessage, HttpStatusCode? responseStatusCode)
		{
			ErrorMessage = errorMessage;
			ResponseStatusCode = responseStatusCode;
		}

		public string ErrorMessage { get; private set; }
		public HttpStatusCode? ResponseStatusCode { get; private set; }
	}
}
