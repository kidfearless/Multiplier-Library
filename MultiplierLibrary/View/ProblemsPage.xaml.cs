using System;
using Plugin.SimpleAudioPlayer;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reflection;
using System.IO;
using MultiplierLibrary.Controller;
using MultiplierLibrary.Model;

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

		//test method for 'correct' audio
		public void OnCorrect()
		{
			HideSolution();
		}

		//test method for 'wrong' audio
		public void OnWrong()
		{
			ShowSolution();
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
				textBox.Focus();
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
			qButton.IsVisible = true;
			numStack.IsVisible = true;
			#endregion
			HideSolution();
		}

		public void OnRoundEnd()
		{
			#region Show Congrats
						
			qButton.IsVisible = false;
			CongratStack.IsVisible = true;
			//ScoreboardLayout.IsVisible = false;

			numStack.IsVisible = false;
			LabelSkip.IsVisible = false;
			TextBoxAnswer.IsVisible = false;
			#endregion
			HideSolution();
		}

		private void AgainButton_Clicked(object sender, EventArgs e)
		{
			App.Current.Game.StartNewGame();
		}

		private void SettingsButton_Clicked(object sender, EventArgs e)
		{
			Navigator.GoToSettings();
			//App.Current.Navigation.CurrentPage = App.Current.Navigation.SettingsPage;
		}

		private void HomeButton_Clicked(object sender, EventArgs e)
		{
			Navigator.GoHome();
			
			//App.Current.Navigation.CurrentPage = App.Current.Navigation.HomePage;

		}

		private void ResultsButton_Clicked(object sender, EventArgs e)
		{
			Navigator.CheckResults();
			//App.Current.Navigation.CurrentPage = App.Current.Navigation.RoundResultsPage;
		}

		//show Solurion 
		public void ShowSolution()
		{
			var current =  App.Current.Game.CurrentProblem;
			LabelAnswer.Text = $"Correct Answer: {current.LeftHand} X {current.RightHand} = {current.GetAnswer()}";
			LabelAnswer.IsVisible = true;
		}

		//Hide Solurion 
		public void HideSolution()
		{
			LabelAnswer.Text = String.Empty;
			LabelAnswer.IsVisible = false;
		}
	}
}