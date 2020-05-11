using MultiplierLibrary.Controller;
using MultiplierLibrary.Model;
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
			StackLayout stack = new StackLayout();
			Button returnButton = new Button()
			{
				Text = "Return"

			};
			returnButton.Clicked += ReturnButtonClicked;
			//grid.Children.Insert(0, returnButton);
			//grid.Children.AddHorizontal(returnButton);

			for (Types type = 0; type < Types.Size; type++)
			{
				Label label = new Label()
				{
					FontSize = 24,
					Text = type.ToString(),
					HorizontalOptions = LayoutOptions.Center
				};

				LinkedSwitch Switch = new LinkedSwitch(type.ToString());
				Switch.HorizontalOptions = LayoutOptions.Center;

				grid.Children.Add(label, 0, (int)type+1);
				grid.Children.Add(Switch, 1, (int)type+1);
			}
			stack.Children.Insert(0, returnButton);
			stack.Children.Insert(1, grid);  
			scroll.Content = stack;
			this.Content = scroll;
			

		}
		public void ReturnButtonClicked(object sender, EventArgs e)
		{
			if(App.Current.Game.Page != null)
			{
				Navigator.GoToProblemsPage();
			}
			else
			{
				Navigator.GoHome();
			}
		}
	}
}