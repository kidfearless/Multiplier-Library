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
	}
}