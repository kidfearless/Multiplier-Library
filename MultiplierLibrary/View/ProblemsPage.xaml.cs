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
			labelSkip.GestureRecognizers.Add(tap);

			// add enter event
			textBoxAnswer.Completed += OnTextBoxEnter;
		}

		protected void OnSkipTapped(object sender, EventArgs args)
		{

		}
		protected void OnTextBoxEnter(object sender, EventArgs args)
		{

		}
	}
}