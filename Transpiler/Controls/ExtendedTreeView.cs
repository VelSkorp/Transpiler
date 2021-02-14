using System.Windows;
using System.Windows.Controls;

namespace Transpiler
{
	public class ExtendedTreeView : TreeView
	{
		public ExtendedTreeView()
			: base()
		{
			SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(ItemChanged);
		}

		private void ItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			if (SelectedItem != null)
			{
				SetValue(SelectedItemProperty, SelectedItem);
			}
		}

		public object SelectedItem
		{
			get => GetValue(TreeView.SelectedItemProperty);
			set => SetValue(TreeView.SelectedItemProperty, value);
		}

		public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(ExtendedTreeView), new UIPropertyMetadata(null));
	}
}