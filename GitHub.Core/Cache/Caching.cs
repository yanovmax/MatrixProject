using Core.Logger;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace Core.Cache
{
    public class Caching
    {
        public static object GetCache(string key)
        {
            try
            {
                var cacheObject = HttpRuntime.Cache;
                return cacheObject.Get(key);
            }

            catch (Exception ex)
            {
                Log.Write(ex.Message, "Helpers", "Caching", "GetCache");
                return null;
            }
        }

        public static T GetCache<T>(string key)
        {
            try
            {
                var cacheObject = HttpRuntime.Cache;
                return (T)cacheObject.Get(key);
            }

            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                Log.Write(ex.Message, "Helpers", "Caching", "GetCache");
                return default(T);
            }
        }
        public static void SetCache(string key, object value)
        {
            try
            {
                SetCache(key, value, null);
            }

            catch (Exception ex)
            {
                Log.Write(ex.Message, "Helpers", "Caching", "SetCache");
            }
        }

        public static void SetCache(string key, object value, DateTime? expirationDate)
        {
            var cacheObject = HttpRuntime.Cache;
            try
            {
                if (!string.IsNullOrEmpty(key) && value != null)
                {
                    if (expirationDate.HasValue)
                    {
                        cacheObject.Insert(key, value, null, expirationDate.Value, System.Web.Caching.Cache.NoSlidingExpiration);
                    }
                    else
                    {
                        cacheObject.Insert(key, value);
                    }
                }
            }

            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                Log.Write(ex.Message, "Helpers", "Caching", "SetCache");
            }
        }

        public static void SetCache(string key, object value, Enums.CacheDuration duration)
        {

            SetCache(key, value, getCacheDutaion(duration));

        }

        private static DateTime getCacheDutaion(Enums.CacheDuration duration)
        {
            switch (duration)
            {
                case Enums.CacheDuration.Small:
                    return DateTime.Now.AddMinutes(4);
                case Enums.CacheDuration.Medium:
                    return DateTime.Now.AddMinutes(30);
                case Enums.CacheDuration.Large:
                    return DateTime.Now.AddHours(2);
                case Enums.CacheDuration.Huge:
                    return DateTime.Now.AddHours(4);
                default:
                    return DateTime.Now.AddMinutes(10);
            }
        }
    }
}