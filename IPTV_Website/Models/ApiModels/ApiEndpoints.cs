namespace IPTV_Website.Models.ApiModels
{
    public class ApiEndpoints
    {
        #region Token

        public const string GenerateToken = "/Token/GenerateToken";

        #endregion

        #region App

        public const string CheckApp = "/App/CheckApp";
        public const string DownloadApk = "/App/DownloadApk";
        public const string VersionUpdate = "/App/DownloadApk";

        #endregion

        #region Channel

        public const string App_ViewLogs = "/Channel/APP_ViewLogs";

        #endregion

        #region Dashboard

        public const string App_Channels = "/Dashboard/APP_Channels?SubNo={0}";
        public const string Images = "/Dashboard/Images";

        #endregion

        #region EPG

        public const string EPG_Get = "/EPG/Get?ServiceID={0}";

        #endregion

        #region FingerPrint

        public const string FingerPrint_Get = "/FingerPrint/Get?deviceNo={0}";

        #endregion

        #region Inventory

        public const string APP_DeviceStatus = "/Inventory/APP_DeviceStatus?DeviceNo={0}";

        #endregion

        #region OSD

        public const string OSD_Get = "/OSD/Get?deviceNo={0}";

        #endregion

        #region Subscriber

        public const string APP_SubscribedChannels = "/Subscriber/APP_SubscribedChannels?SubNo={0}";
        public const string GetSubcriptionBySubsciber = "/Subscriber/GetSubcriptionBySubsciber?DeviceNo={0}";

        #endregion
    }
}
