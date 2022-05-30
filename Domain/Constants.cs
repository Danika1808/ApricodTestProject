﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Constants
    {
        public const string ResponseTitleFailed = "The server was unable to process the request correctly";
        public const string ResponseTitleSuceess = "Operation was successfully completed";
        public const string OkResponseType = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.3.1";
        public const string BadRequestResponseType = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
        public const string UnauthorizedResponseType = "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1";
        public const string ForbiddenResponseType = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3";
        public const string NotFoundResponseType = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
        public const string ConflictResponseType = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8";
        public const string InternalServerErrorResponseType = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1";
    }
}
