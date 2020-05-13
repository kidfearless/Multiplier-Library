using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;
using Switch = Xamarin.Forms.Switch;

namespace MultiplierLibrary.Model
{
	// Linked slider provides a self contained interface for linking switches to a corresponding setting.
	// All while propagating the changes to other linked type controls
	class LinkedSlider : Slider
	{
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
				LinkedSlider linked = bindable as LinkedSlider;

				if(newValue != null)
				{
					double prop = Settings.GetProperty((string)newValue, 1.0);
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

		public LinkedSlider(string property)
		{
			this.LinkedProperty = property;
			Settings.SettingChanged += LinkedSlider_SettingChanged;
			this.ValueChanged += LinkedSlider_Toggled; ;
			this.Value = Settings.GetProperty(property, 1.0);
		}

		public LinkedSlider(string property, double DefaultValue)
		{
			this.LinkedProperty = property;
			Settings.SettingChanged += LinkedSlider_SettingChanged;
			this.ValueChanged += LinkedSlider_Toggled;
			this.Value = Settings.GetProperty(property, DefaultValue);
		}

		public LinkedSlider(string property, double DefaultValue, double min, double max)
		{
			this.LinkedProperty = property;
			Settings.SettingChanged += LinkedSlider_SettingChanged;
			this.ValueChanged += LinkedSlider_Toggled;
			this.Value = Settings.GetProperty(property, DefaultValue);
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
				Settings.SetProperty(this.LinkedProperty, e.NewValue);
			}
		}

		public void LinkedSlider_SettingChanged(object sender, SettingsChangedEventArgs args)
		{
			if(args.SettingChanged == this.LinkedProperty && this.Value != (double)args.NewValue)
			{
				this.Value = (double)args.NewValue;
			}
		}
	}
}
