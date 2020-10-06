using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Models
{
    public class Enums
    {      
        public enum ResponseType
        {
            Unknown = 0,
            Success = 1,
            ServerError = 2,
            NoResult = 3,
        }

        public enum CacheDuration
        {
            // 4 minutes cache
            Small,

            // 30 minutes cache
            Medium,

            // 2 hours cache
            Large,

            // 4 hours cache
            Huge,
        }

    }
}