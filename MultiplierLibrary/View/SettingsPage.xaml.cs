using MultiplierLibrary.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Switch = Xamarin.Forms.Switch;

namespace MultiplierLibrary.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage()
		{
			InitializeComponent();
			ScrollView scroll = new ScrollView();

			Grid grid = new Grid();
			

			for (Types type = 0; type < Types.Size; type++)
			{
				Label label = new Label()
				{
					FontSize = 24,
					Text = type.ToString(),
					HorizontalOptions = LayoutOptions.Center
				};

				Switch Switch = new Switch()
				{
					ClassId = type.ToString(),
					HorizontalOptions = LayoutOptions.Center
				};
				Switch.Toggled += Switch_Toggled;

				grid.Children.Add(label, 0, (int)type);
				grid.Children.Add(Switch, 1, (int)type);
			}

			scroll.Content = grid;
			this.Content = scroll; 
		}

		private void Switch_Toggled(object sender, ToggledEventArgs e)
		{
			Switch toggle = (Switch)sender;
			Settings.SetProperty(toggle.ClassId, toggle.IsToggled);

			DisplayAlert("test", "test23", "no cancel");
		}
	}
}