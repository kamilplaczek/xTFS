using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace xTFS.Resources
{
	// You exclude the 'Extension' suffix when using in Xaml markup
	[ContentProperty("Text")]
	public class TranslateExtension : IMarkupExtension
	{
		readonly CultureInfo ci;
		const string ResourceId = "xTFS.Resources.AppResources";

		public TranslateExtension()
		{
			// TODO: implement platform specific code for ILocalize interface to support automatic translation (https://developer.xamarin.com/guides/xamarin-forms/advanced/localization/)
			//if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Android)
			//{
			//	ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
			//}
		}

		public string Text { get; set; }

		public object ProvideValue(IServiceProvider serviceProvider)
		{
			if (Text == null)
				return String.Empty;

			ResourceManager resmgr = new ResourceManager(ResourceId
								, typeof(TranslateExtension).GetTypeInfo().Assembly);

			var translation = resmgr.GetString(Text, ci);

			if (translation == null)
			{
#if DEBUG
				throw new ArgumentException(
					String.Format("Key '{0}' was not found in resources '{1}'.", Text, ResourceId),
					"Text");
#else
                translation = Text; // HACK: returns the key, which GETS DISPLAYED TO THE USER
#endif
			}
			return translation;
		}
	}
}
