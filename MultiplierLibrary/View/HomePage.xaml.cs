using MultiplierLibrary.Controller;
using MultiplierLibrary.Data;
using MultiplierLibrary.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
			AddGrid();
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

		void AddGrid()
		{
			Grid grid = new Grid();
			int index = 0;
			#region OneByOne
			{
				var label = new Label
				{
					Text = "One By One",
					HorizontalOptions = LayoutOptions.Center
				};
				var linkedSwitch = new LinkedSwitch(Types.OneByOne.ToString());
				linkedSwitch.HorizontalOptions = LayoutOptions.Center;
				grid.Children.Add(label, (int)Side.Left, index);
				grid.Children.Add(linkedSwitch, (int)Side.Right, index++);
			}
			#endregion
			#region TwoByOne
			{
				var label1 = new Label
				{
					Text = "Two By One",
					HorizontalOptions = LayoutOptions.Center
				};
				var linkedSwitch1 = new LinkedSwitch(Types.TwoByOne.ToString());
				linkedSwitch1.HorizontalOptions = LayoutOptions.Center;
				grid.Children.Add(label1, (int)Side.Left, index);
				grid.Children.Add(linkedSwitch1, (int)Side.Right, index++);
			}
			#endregion
			#region ThreeByOne
			{
				var label2 = new Label
				{
					Text = "Three By One",
					HorizontalOptions = LayoutOptions.Center
				};
				var linkedSwitch2 = new LinkedSwitch(Types.ThreeByOne.ToString());
				linkedSwitch2.HorizontalOptions = LayoutOptions.Center;
				grid.Children.Add(label2, (int)Side.Left, index);
				grid.Children.Add(linkedSwitch2, (int)Side.Right, index++);
			}
			#endregion
			#region Factors
			{
				var label3 = new Label
				{
					Text = "Factors",
					HorizontalOptions = LayoutOptions.Center
				};
				var linkedSwitch3 = new LinkedSwitch(Types.Factored.ToString());
				linkedSwitch3.HorizontalOptions = LayoutOptions.Center;
				grid.Children.Add(label3, (int)Side.Left, index);
				grid.Children.Add(linkedSwitch3, (int)Side.Right, index++);
			}
			#endregion
			#region MaxProblems
			{
				
				var label4 = new Label
				{
					Text = $"Max Problems: {Settings.MaxProblems}",
					HorizontalOptions = LayoutOptions.CenterAndExpand
				};
				var stepper = new LinkedStepper(nameof(Settings.MaxProblems), 5, 1, 100);
				stepper.ValueChanged += delegate (object sender, ValueChangedEventArgs e)
				{
					label4.Text = $"Max Problems: {Settings.MaxProblems}";
				};
				stepper.HorizontalOptions = LayoutOptions.CenterAndExpand;
				grid.Children.Add(label4, (int)Side.Left, index);
				grid.Children.Add(stepper, (int)Side.Right, index++);
			}
			#endregion
			layout.Children.Insert(2, grid);
		}

		private void layout_ChildAdded(object sender, ElementEventArgs e)
		{
			Debug.WriteLine(sender);
		}
	}
}
 