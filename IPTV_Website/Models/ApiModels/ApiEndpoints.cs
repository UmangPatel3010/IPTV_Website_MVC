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

        public const string App_Channels = "/Dashboard/APP_Channels";
        public const string Images = "/Dashboard/Images";

        #endregion

        #region EPG

        public const string EPG_Get = "/EPG/Get";

        #endregion

        #region FingerPrint

        public const string FingerPrint_Get = "/FingerPrint/Get";

        #endregion

        #region Inventory

        public const string APP_DeviceStatus = "/Inventory/APP_DeviceStatus";

        #endregion

        #region OSD

        public const string OSD_Get = "/OSD/Get";

        #endregion

        #region Subscriber

        public const string Subscriber = "/Subscriber/APP_SubscribedChannels";

        #endregion

        #region Subcription

        public const string Subcription = "/Subscriber/GetSubcriptionBySubsciber";

        #endregion
    }
}
