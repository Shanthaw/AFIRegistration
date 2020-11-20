using System;
using System.Globalization;

namespace AFIRegistration.Api.Utils
{
    public class EntityException : Exception
    {
        public EntityException(string message)
       : base(message)
        {
        }
        public EntityException(
            string message,
            params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
