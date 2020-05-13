using Plugin.SimpleAudioPlayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using Xamarin.Forms.Internals;

namespace MultiplierLibrary.Model
{
	class AudioPlayer
	{
		ISimpleAudioPlayer congratsPlayer;
		ISimpleAudioPlayer correctPlayer;
		ISimpleAudioPlayer wrongPlayer;

		public bool Initialized = false;

		public AudioPlayer()
		{
			Thread thread1 = new Thread(Init);
			thread1.Start();
		}

		void Init()
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

			Initialized = true;
		}

		public void PlayCorrect()
		{
			if(Initialized)
			{
				correctPlayer.Play();
			}
		}

		public void PlayWrong()
		{
			if(Initialized)
			{
				wrongPlayer.Play();
			}
		}

		public void PlayCongrats()
		{
			if(Initialized)
			{
				congratsPlayer.Play();
			}
		}

		Stream GetStreamFromFile(string filename)
		{
			var assembly = typeof(App).GetTypeInfo().Assembly;
			var stream = assembly.GetManifestResourceStream("MultiplierLibrary.Audio." + filename);
			return stream;
		}
	}
}
