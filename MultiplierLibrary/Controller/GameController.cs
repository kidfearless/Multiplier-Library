﻿using MultiplierLibrary.Model;
using MultiplierLibrary.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace MultiplierLibrary.Controller
{
	public class GameController
	{
		Problem currentProblem;
		ProblemsPage page;
		int correct;
		int wrong;
		public GameController(ProblemsPage page)
		{
			this.page = page;
			CreateNewProblem();
		}

		void CheckAnswer(int answer)
		{
			Debug.WriteLine($"CheckAnswer:");
			if(this.currentProblem.GetAnswer() == answer)
			{
				this.correct++;
				page.LabelCorrect.Text = correct.ToString();
				this.currentProblem.Correct = true;
			}
			else
			{
				this.wrong++;
				page.LabelWrong.Text = wrong.ToString();
			}

			page.LabelTotal.Text = (wrong + correct).ToString();
		}

		void CreateNewProblem()
		{
			Debug.WriteLine($"CreateNewProblem:");

			this.currentProblem = App.Current.Multiplier.DoWarmup();
			page.LabelLeft.Text = currentProblem.Left.ToString();
			page.LabelRight.Text = currentProblem.Right.ToString();
		}

		public void OnTextBoxEnter(Entry textbox)
		{
			Debug.WriteLine($"OnTextBoxEnter:");
			string answer = textbox.Text;


			int.TryParse(answer, out int intAnswer);
			CheckAnswer(intAnswer);

			CreateNewProblem();

			// Clear answer
			textbox.Text = string.Empty;
		}

		public void OnProblemSkipped()
		{
			Debug.WriteLine($"OnProblemSkipped: {this.currentProblem.Left} X {this.currentProblem.Right}");
		}
	}
}