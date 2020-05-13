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
		public enum Side : int
		{
			Left = 0,
			Right = 1
		}

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

			int offset = 0;
			stack.Children.Add(returnButton);

			//grid.Children.Insert(0, returnButton);
			//grid.Children.AddHorizontal(returnButton);

			#region Sound Switch
			{
				var soundLabel = new Label()
				{
					Text = "Play Sounds"
				};

				var soundSwitch = new LinkedSwitch(nameof(Settings.ShouldPlaySound));

				grid.Children.Add(soundLabel, (int)Side.Left, offset);
				grid.Children.Add(soundSwitch, (int)Side.Right, offset++);
			}
			#endregion
			#region Old Problems Percentage
			{
				// Creates a label and a linked slider to the old problems percentage.
				// When the slider changes we update the label text
				var dropOffLabel = new Label()
				{
					Text = $"Old Problems Percentage: {Settings.RepeatProblemDropOff * 100.0}"
				};
				var dropOffSlider = new LinkedSlider(nameof(Settings.RepeatProblemDropOff), 0.9, -1.0, 1.0);
				dropOffSlider.Margin = new Thickness(0.0, 0.0, 5.0, 0.0);
				dropOffSlider.ValueChanged += delegate (object sender, ValueChangedEventArgs e)
				{
					dropOffLabel.Text = $"Old Problems Percentage: {Settings.RepeatProblemDropOff * 100.0}";
				};

				grid.Children.Add(dropOffLabel, (int)Side.Left, offset);
				grid.Children.Add(dropOffSlider, (int)Side.Right, offset++);
			}
			#endregion
			#region Old Problems Drop Off
			{
				// Creates a label and a linked slider to the Old Problems Drop Off
				// When the slider changes we update the label text
				var label = new Label()
				{
					Text = $"Old Problems Minimum: {Settings.RepeatProblemMinimum} problems"
				};
				var slider = new LinkedStepper(nameof(Settings.RepeatProblemMinimum), 15, 1, 25);
				slider.Margin = new Thickness(0.0, 0.0, 5.0, 0.0);
				slider.ValueChanged += delegate (object sender, ValueChangedEventArgs e)
				{
					label.Text = $"Old Problems Minimum: {Settings.RepeatProblemMinimum} problems";
				};

				grid.Children.Add(label, (int)Side.Left, offset);
				grid.Children.Add(slider, (int)Side.Right, offset++);
			}
			#endregion

			for (Types type = 0; type < Types.Size; type++)
			{
				Label label = new Label()
				{
					FontSize = 16,
					Text = Model.TypeConverter.ToString(type),
					HorizontalOptions = LayoutOptions.End,
				};

				LinkedSwitch Switch = new LinkedSwitch(type.ToString());
				Switch.HorizontalOptions = LayoutOptions.Center;

				grid.Children.Add(label, (int)Side.Left, (int)type+ offset);
				grid.Children.Add(Switch, (int)Side.Right, (int)type+ offset);
			}

			stack.Children.Add(grid);  
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