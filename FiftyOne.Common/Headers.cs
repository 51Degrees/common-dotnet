using System;
using System.Collections.Generic;
using System.Text;

namespace FiftyOne.Common
{
    public class Headers
    {
        /// <summary>
        /// The separator character used by pseudo-headers to split 
        /// header names and values.
        /// </summary>
        public const char PSEUDEO_HEADER_SEPARATOR = '\x1f';

        public const string UACH_MODEL = "Sec-CH-UA-Model";
        public const string UACH_ARCH = "Sec-CH-UA-Arch";
        public const string UACH_MOBILE = "Sec-CH-UA-Mobile";
        public const string UACH_PLATFORM = "Sec-CH-UA-Platform";
        public const string UACH_PLATFORM_VERSION = "Sec-CH-UA-Platform-Version";
        public const string UACH_UA = "Sec-CH-UA";
        public const string UACH_UA_FULL_VERSION = "Sec-CH-UA-Full-Version";
        public const string UACH_UA_FULL_VERSION_LIST = "Sec-CH-UA-Full-Version-List";

        public static readonly string PSEUDO_HARDWARE =
            $"{UACH_PLATFORM}{PSEUDEO_HEADER_SEPARATOR}{UACH_MODEL}" +
            $"{PSEUDEO_HEADER_SEPARATOR}{UACH_MOBILE}";
        public static readonly string PSEUDO_BROWSER =
            $"{UACH_UA_FULL_VERSION_LIST}{PSEUDEO_HEADER_SEPARATOR}{UACH_MOBILE}" +
            $"{PSEUDEO_HEADER_SEPARATOR}{UACH_PLATFORM}";
        public static readonly string PSEUDO_BROWSER2 =
            $"{UACH_UA}{PSEUDEO_HEADER_SEPARATOR}{UACH_MOBILE}" +
            $"{PSEUDEO_HEADER_SEPARATOR}{UACH_PLATFORM}";
        public static readonly string PSEUDO_PLATFORM =
            $"{UACH_PLATFORM}{PSEUDEO_HEADER_SEPARATOR}{UACH_PLATFORM_VERSION}";
        public const string OPERA_MINI_UA = "X-OperaMini-Phone-UA";

    }
}
