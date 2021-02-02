using System;
using System.Diagnostics;
using System.Globalization;

namespace Transpiler
{
	public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			// Find the appropriate page
			switch ((ApplicationPage)value)
			{
				case ApplicationPage.Main:
					return new MainPage();

				default:
					Debugger.Break();
					return null;
			}
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}