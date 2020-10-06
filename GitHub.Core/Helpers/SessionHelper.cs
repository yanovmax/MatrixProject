using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Helpers
{
    public interface ISessionService
    {
        void Set(string key, object value);
        T Get<T>(string key);
    }

    public class SessionService : ISessionService
    {
        public void Set(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }
        public T Get<T>(string key)
        {
            return (T)HttpContext.Current.Session[key];
        }
    }
}