// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace xTFS.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
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

		private const string TfsAddressKey = "TfsAddress";
		private static readonly string TfsAddressDefault = string.Empty;

		private const string UsernameKey = "Username";
		private static readonly string UsernameDefault = string.Empty;

		private const string PasswordKey = "Password";
		private static readonly string PasswordDefault = string.Empty;

		#endregion


		public static string TfsAddress
		{
			get
			{
				return AppSettings.GetValueOrDefault<string>(TfsAddressKey, TfsAddressDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue<string>(TfsAddressKey, value);
			}
		}

		public static string Username
		{
			get
			{
				return AppSettings.GetValueOrDefault<string>(UsernameKey, UsernameDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue<string>(UsernameKey, value);
			}
		}

		public static string Password
		{
			get
			{
				return AppSettings.GetValueOrDefault<string>(PasswordKey, PasswordDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue<string>(PasswordKey, value);
			}
		}
	}
}