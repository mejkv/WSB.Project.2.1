using System.Collections;
using System.Reflection;
using System.Runtime.Serialization;

namespace AirShop.WebApiPostgre.Data.ApiExceptions
{
    [Serializable]
    public class UserNotExistException : Exception
    {
        public UserNotExistException()
        {
        }

        public UserNotExistException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        protected UserNotExistException(SerializationInfo info, StreamingContext context)
        : base(info, context)
        {
        }
    }
}
