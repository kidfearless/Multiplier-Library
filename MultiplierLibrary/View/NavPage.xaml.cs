using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;
using System.Diagnostics;

namespace MultiplierLibrary
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NavPage : Xamarin.Forms.TabbedPage
	{
		public NavPage()
		{
			// by default is set to the root directory of the UWP project
			Xamarin.Forms.Application.Current.On<Windows>().SetImageDirectory("Assets");
			On<Windows>().SetHeaderIconsEnabled(true);
			On<Windows>().SetHeaderIconsSize(new Size(24, 24));

			InitializeComponent();

			// should set page properties after they have been initialized
			BarBackgroundColor = new Color(33.0/255.0, 150.0/255.0, 243.0/255.0);
			BarTextColor = Color.White;

			Debug.WriteLine($"[DEBUG] background color: {this.BarBackgroundColor}");

			CurrentPageChanged += NavPage_CurrentPageChanged;
		}

		private void NavPage_CurrentPageChanged(object sender, EventArgs e)
		{			
		}
	}
}