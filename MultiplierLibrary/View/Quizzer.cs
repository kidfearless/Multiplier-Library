using MultiplierLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MultiplierLibrary.View
{
	public class Quizzer : ContentPage
	{
		Label Correct;
		Label Wrong;
		Label ProblemLabel;
		
		Entry TextBox;

		public Quizzer()
		{
			this.Correct = new Label
			{
				Text = "Correct: 0"
			};
			this.Wrong = new Label
			{
				Text = "Wrong: 0"
			};
			Problem problem = App.Current.Multiplier.DoWarmup();
			this.ProblemLabel = new Label
			{
				Text = $"{problem.Left} X {problem.Right}"
			};
			TextBox = new Entry();
			TextBox.Completed += OnTextBoxEnter;
			this.Content = new StackLayout
			{
				Children =
				{
					Correct, Wrong, ProblemLabel, TextBox
				}
			};
		}

		protected void OnTextBoxEnter(object sender, EventArgs args)
		{

		}
	}
}