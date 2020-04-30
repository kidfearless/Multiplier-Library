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
		public bool isRoundDone = false;
		public Label LabelCorrect => labelCorrect;
		public Label LabelTotal => labelTotal;
		public Label LabelWrong => labelWrong;
		public Label LabelLeft => labelLeft;
		public Label LabelRight => labelRight;
		public Entry TextBoxAnswer => textBoxAnswer;
		public Label LabelSkip => labelSkip;

		public ProblemsPage()
		{
			InitializeComponent();
			// Add skip event
			TapGestureRecognizer tap = new TapGestureRecognizer();
			tap.Tapped += OnSkipTapped;
			labelSkip.GestureRecognizers.Add(tap);

			// add enter event
			textBoxAnswer.Completed += OnTextBoxEnter;
		}

		protected void OnSkipTapped(object sender, EventArgs args)
		{

		}

		protected void OnTextBoxEnter(object sender, EventArgs args)
		{
			Entry textBox = sender as Entry;
			if(!String.IsNullOrEmpty(textBox.Text) && !String.IsNullOrWhiteSpace(textBox.Text))
			{
				App.Current.Game.OnTextBoxEnter(textBox);
			}
		}

		private void Congratulations(object sender, EventArgs args)
		{
			if (isRoundDone)
			{
				label1.FontSize = 40;
				label1.TextColor = Color.Black;
				label2.FontSize = 40;
				label2.TextColor = Color.Black;
				settingsButton.IsVisible = true;
				againButton.IsVisible = true;
				homeButton.IsVisible = true;
				resultsButton.IsVisible = true;

				labelSkip.IsVisible = false;
				textBoxAnswer.IsVisible = false;
				labelLeft.IsVisible = false;
				labelRight.IsVisible = false;
				labelX.IsVisible = false;
				labelTitle.IsVisible = false;
			}
			

		}
	}
}