using ModelWrapper.Utilities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace ModelWrapper.Extensions.Notifications
{
    public static class GetNotificationsExtensions
    {
        public static string GetMessage(this Dictionary<string, object> source)
        {
            return source.GetValue(Constants.CONST_NOTIFICATIONS_MESSAGE) as string;
        }
        public static bool HasInner(this Dictionary<string, object> source)
        {
            return source.Any(x => x.Key == Constants.CONST_NOTIFICATIONS_INNER);
        }
        public static Dictionary<string, object> GetInner(this Dictionary<string, object> source)
        {
            if (source.HasInner())
                return source.GetValue(Constants.CONST_NOTIFICATIONS_INNER) as Dictionary<string, object>;

            return null;
        }
        public static bool HasRequest(this Dictionary<string, object> source)
        {
            return source.Any(x => x.Key == Constants.CONST_NOTIFICATIONS_REQUEST);
        }
        public static Dictionary<string, object> GetRequest(this Dictionary<string, object> source)
        {
            if (source.HasRequest())
                return source.GetValue(Constants.CONST_NOTIFICATIONS_REQUEST) as Dictionary<string, object>;

            return null;
        }
        public static bool HasEntity(this Dictionary<string, object> source)
        {
            return source.Any(x => x.Key == Constants.CONST_NOTIFICATIONS_ENTITY);
        }
        public static Dictionary<string, object> GetEntity(this Dictionary<string, object> source)
        {
            if (source.HasEntity())
                return source.GetValue(Constants.CONST_NOTIFICATIONS_ENTITY) as Dictionary<string, object>;

            return null;
        }
        public static bool HasDomain(this Dictionary<string, object> source)
        {
            return source.Any(x => x.Key == Constants.CONST_NOTIFICATIONS_DOMAIN);
        }
        public static Dictionary<string, object> GetDomain(this Dictionary<string, object> source)
        {
            if (source.HasDomain())
                return source.GetValue(Constants.CONST_NOTIFICATIONS_DOMAIN) as Dictionary<string, object>;

            return null;
        }
        public static string GetJson(this Dictionary<string, object> source)
        {
            return JsonConvert.SerializeObject(source);
        }
    }
}
