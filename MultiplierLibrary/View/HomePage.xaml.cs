using MultiplierLibrary.Controller;
using MultiplierLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MultiplierLibrary.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
		public HomePage()
		{
			InitializeComponent();
		}

		private void Switch_PropertyChanged(object sender, ToggledEventArgs e)
		{
			Switch Switch = (Switch)sender;
			Settings.SetProperty(Switch.ClassId, Switch.IsToggled);
		}

		private void StartButton_Clicked(object sender, EventArgs e)
		{
			if(!string.IsNullOrEmpty(UserName.Text))
			{
				Navigator.StartGame(UserName.Text);
			}
		}

		private void SettingsButton_Clicked(object sender, EventArgs e)
		{
			Navigator.GoToSettings();
		}
	}
}