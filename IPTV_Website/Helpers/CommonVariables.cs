namespace IPTV_Website.Helpers
{
    public class CommonVariables
    {
        private static IHttpContextAccessor _HttpContextAccessor;
        static CommonVariables()
        {
            _HttpContextAccessor = new HttpContextAccessor();
        }

        public static void setToken(string token)
        {
            _HttpContextAccessor.HttpContext.Session.SetString("Token", token);
        }
        public static string? getToken()
        {
            if (_HttpContextAccessor.HttpContext.Session.GetString("Token") == null)
            {
                return null;
            }
            return _HttpContextAccessor.HttpContext.Session.GetString("Token");
        }
        public static void setSubNo(string subNo)
        {
            _HttpContextAccessor.HttpContext.Session.SetString("SubNo", subNo);
        }
        public static string? getSubNo()
        {
            if (_HttpContextAccessor.HttpContext.Session.GetString("SubNo") == null)
            {
                //for tempory testing purpose : return 1 other return null
                return "1";
            }
            return _HttpContextAccessor.HttpContext.Session.GetString("SubNo");
        }
        public static void setDeviceNo(string deviceNo)
        {
            _HttpContextAccessor.HttpContext.Session.SetString("DeviceNo", deviceNo);
        }
        public static string? getDeviceNo()
        {
            if (_HttpContextAccessor.HttpContext.Session.GetString("DeviceNo") == null)
            {
                return null;
            }
            return _HttpContextAccessor.HttpContext.Session.GetString("DeviceNo");
        }
    }
}
