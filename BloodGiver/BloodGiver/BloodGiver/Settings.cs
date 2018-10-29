using Plugin.Settings;
using Plugin.Settings.Abstractions;


namespace BloodGiver
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private static readonly string SettingsDefault = string.Empty;

        #endregion


        public static string UserName
        {
            get
            {
                return AppSettings.GetValueOrDefault(nameof(UserName), SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(UserName), value);
            }
        }

        public static string Password
        {
            get
            {
                return AppSettings.GetValueOrDefault(nameof(Password), SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(Password), value);
            }
        }

        public static string AccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault(nameof(AccessToken), SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(AccessToken), value);
            }
        }

        public static void ClearAllData()
        {
            AppSettings.Clear();
        }
    }
}
