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
	public partial class ProblemsPage : ContentPage
	{
		public ProblemsPage()
		{
			InitializeComponent();
			// Add skip event
			TapGestureRecognizer tap = new TapGestureRecognizer();
			tap.Tapped += OnSkipTapped;
			LabelSkip.GestureRecognizers.Add(tap);

			// add enter event
			TextBoxAnswer.Completed += OnTextBoxEnter;
		}

		protected void OnSkipTapped(object sender, EventArgs args)
		{
			App.Current.Game.OnProblemSkipped();
		}

		protected void OnTextBoxEnter(object sender, EventArgs args)
		{
			Entry textBox = sender as Entry;
			if(!String.IsNullOrEmpty(textBox.Text) && !String.IsNullOrWhiteSpace(textBox.Text))
			{
				App.Current.Game.OnTextBoxEnter(textBox);
			}
		}

		public void OnRoundStart()
		{
			#region Hide Congrats
			CongratStack.IsVisible = false;
			ScoreboardLayout.IsVisible = true;

			LabelSkip.IsVisible = true;
			TextBoxAnswer.IsVisible = true;
			#endregion
		}

		public void OnRoundEnd()
		{
			#region Show Congrats
			CongratStack.IsVisible = true;
			ScoreboardLayout.IsVisible = false;

			LabelSkip.IsVisible = false;
			TextBoxAnswer.IsVisible = false;
			#endregion
		}

		private void AgainButton_Clicked(object sender, EventArgs e)
		{
			App.Current.Game.StartNewGame();
		}

		private void SettingsButton_Clicked(object sender, EventArgs e)
		{
			App.Current.Navigation.CurrentPage = App.Current.Navigation.SettingsPage;
		}

		private void HomeButton_Clicked(object sender, EventArgs e)
		{
			App.Current.Navigation.CurrentPage = App.Current.Navigation.HomePage;

		}

		private void ResultsButton_Clicked(object sender, EventArgs e)
		{
			App.Current.Navigation.CurrentPage = App.Current.Navigation.RoundResultsPage;
		}
	}
}