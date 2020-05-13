using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;
using Switch = Xamarin.Forms.Switch;

namespace MultiplierLibrary.Model
{
	// Linked switch provides a self contained interface for linking switches to a corresponding setting.
	// All while propagating the changes to other switches
	class LinkedStepper : Stepper
	{
		public new int Increment
		{
			get
			{
				return Convert.ToInt32(base.Increment);
			}

			set
			{
				base.Increment = Convert.ToDouble(value);
			}
		}
		public new int Maximum
		{
			get
			{
				return Convert.ToInt32(base.Maximum);
			}

			set
			{
				base.Maximum = Convert.ToDouble(value);
			}
		}
		public new int Minimum
		{
			get
			{
				return Convert.ToInt32(base.Minimum);
			}

			set
			{
				base.Minimum = Convert.ToDouble(value);
			}
		}
		public new int Value
		{
			get
			{
				return Convert.ToInt32(base.Value);
			}

			set
			{
				base.Value = Convert.ToDouble(value);
			}
		}

		public static readonly BindableProperty LinkedBindableProperty = BindableProperty.Create(
														 propertyName: nameof(LinkedProperty),
														 returnType: typeof(string),
														 declaringType: typeof(LinkedSlider),
														 defaultBindingMode: BindingMode.TwoWay,
														 propertyChanged: LinkedPropertyChanged);

		public string LinkedProperty
		{
			get
			{
				return GetValue(LinkedBindableProperty).ToString();
			}
			set
			{
				SetValue(LinkedBindableProperty, value);
			}
		}

		private static void LinkedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			try
			{
				LinkedStepper linked = bindable as LinkedStepper;

				if(newValue != null)
				{
					int prop = Settings.GetProperty((string)newValue, 1);
					if (linked.Value != prop)
					{
						linked.Value = prop;
					}
				}
			}
			catch (Exception e)
			{
				Debug.Fail($"[ERROR] Caught exception {e}");
			}
		}

		public LinkedStepper(string property)
		{
			this.LinkedProperty = property;
			Settings.SettingChanged += LinkedSlider_SettingChanged;
			this.ValueChanged += LinkedSlider_Toggled; ;
			this.Value = Settings.GetProperty(property, 1);
		}

		public LinkedStepper(string property, int DefaultValue)
		{
			this.LinkedProperty = property;
			Settings.SettingChanged += LinkedSlider_SettingChanged;
			this.ValueChanged += LinkedSlider_Toggled;
			this.Value = Settings.GetProperty(property, DefaultValue);
		}

		public LinkedStepper(string property, int DefaultValue, int stepsize)
		{
			this.LinkedProperty = property;
			Settings.SettingChanged += LinkedSlider_SettingChanged;
			this.ValueChanged += LinkedSlider_Toggled;
			this.Value = Settings.GetProperty(property, DefaultValue);
			this.Increment = stepsize;
		}

		public LinkedStepper(string property, int DefaultValue, int min, int max)
		{
			this.LinkedProperty = property;
			Settings.SettingChanged += LinkedSlider_SettingChanged;
			this.ValueChanged += LinkedSlider_Toggled;
			this.Value = Settings.GetProperty(property, DefaultValue);
			this.Minimum = min;
			this.Maximum = max;
		}
		public LinkedStepper(string property, int DefaultValue, int stepsize, int min, int max)
		{
			this.LinkedProperty = property;
			Settings.SettingChanged += LinkedSlider_SettingChanged;
			this.ValueChanged += LinkedSlider_Toggled;
			this.Value = Settings.GetProperty(property, DefaultValue);
			this.Increment = stepsize;
			this.Minimum = min;
			this.Maximum = max;
		}

		// This currently fires an extra time for each time you switch pages.
		// Please fix later
		public void LinkedSlider_Toggled(object sender, ValueChangedEventArgs e)
		{
			if(string.IsNullOrEmpty(this.LinkedProperty))
			{
				Debug.Fail("[ERROR] LinkedSlider was toggled without a linked setting");
			}
			else
			{
				Settings.SetProperty(this.LinkedProperty, Convert.ToInt32(e.NewValue));
			}
		}

		public void LinkedSlider_SettingChanged(object sender, SettingsChangedEventArgs args)
		{
			if(args.SettingChanged == this.LinkedProperty && this.Value != (int)args.NewValue)
			{
				this.Value = (int)args.NewValue;
			}
		}
	}
}
