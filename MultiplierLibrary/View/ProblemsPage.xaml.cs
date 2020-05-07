using System;
using Plugin.SimpleAudioPlayer;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reflection;
using System.IO;
namespace MultiplierLibrary.View
{[XamlCompilation(XamlCompilationOptions.Compile)]public partial class ProblemsPage : ContentPage
		//Declares Audio players using type definition from Simple Audio Player Plugin
	{ ISimpleAudioPlayer congratsPlayer;
		ISimpleAudioPlayer correctPlayer;
		ISimpleAudioPlayer wrongPlayer;
			public ProblemsPage()
		{
			//loads audio players on page load
			correctPlayer = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
			congratsPlayer = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
			wrongPlayer = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
			
			//sets audio streams on page load
			var streamCorrect = GetStreamFromFile("Correct.mp3");
			var streamWrong = GetStreamFromFile("Wrong.mp3");
			var streamCongrats = GetStreamFromFile("Congrats.mp3");

			//Loads audio stream to audio player
			correctPlayer.Load(streamCorrect);
			wrongPlayer.Load(streamWrong);
			congratsPlayer.Load(streamCongrats);

			InitializeComponent();
			
			// Add skip event
			TapGestureRecognizer tap = new TapGestureRecognizer();
			tap.Tapped += OnSkipTapped;
			labelSkip.GestureRecognizers.Add(tap);

			// add enter event
			textBoxAnswer.Completed += OnTextBoxEnter;
			
		}

		
		Stream GetStreamFromFile(string filename)
		{
			var assembly = typeof(App).GetTypeInfo().Assembly;
			var stream = assembly.GetManifestResourceStream("MultiplierLibrary.Audio." + filename);
			return stream;
		}
		//test method for 'correct' audio
		private void OnCorrect(object sender, EventArgs args)
		{
			correctPlayer.Play();
		}
		//test method for 'wrong' audio
		private void OnWrong(object sender, EventArgs args)
		{
			wrongPlayer.Play();
		}
		private void Congratulations(object sender, EventArgs args)
		{

			congratsPlayer.Play();



			//button1.IsVisible = false;

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
		public bool isRoundDone = false;
		public Label LabelCorrect => labelCorrect;
		public Label LabelTotal => labelTotal;
		public Label LabelWrong => labelWrong;
		public Label LabelLeft => labelLeft;
		public Label LabelRight => labelRight;
		public Entry TextBoxAnswer => textBoxAnswer;
		public Label LabelSkip => labelSkip;

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