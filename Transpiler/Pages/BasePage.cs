using System.Windows.Controls;
using Transpiler.Core;

namespace Transpiler
{
	/// <summary>
	/// A base page for all pages to gain base functionality
	/// </summary>
	public class BasePage<VM> : Page
		where VM : BaseViewModel, new()
	{
		#region Private Member

		private VM mViewModel;

		#endregion

		#region Public Properties

		public VM ViewModel
		{
			get => mViewModel;
			set
			{
				if (mViewModel == value)
					return;

				mViewModel = value;

				DataContext = mViewModel;
			}
		}

		#endregion

		#region Constructor

		public BasePage()
		{
			ViewModel = new VM();
		}

		#endregion
	}
}