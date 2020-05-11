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
	class LinkedSwitch : Switch
	{
		public static readonly BindableProperty LinkedBindableProperty = BindableProperty.Create(
														 propertyName: nameof(LinkedProperty),
														 returnType: typeof(string),
														 declaringType: typeof(LinkedSwitch),
														 defaultBindingMode: BindingMode.TwoWay,
														 propertyChanged: LinkedPropertyChanged);

		public string LinkedProperty
		{
			get
			{
				var actualValue = base.GetValue(LinkedBindableProperty)?.ToString();
				if (!string.IsNullOrEmpty(actualValue))
				{
					this.IsToggled = Settings.GetProperty(actualValue, true);
				}
				return base.GetValue(LinkedBindableProperty).ToString();
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this.IsToggled = Settings.GetProperty(value, true);
				}
				base.SetValue(LinkedBindableProperty, value);
			}
		}

		private static void LinkedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			try
			{
				LinkedSwitch linked = bindable as LinkedSwitch;
				if(oldValue == null && newValue != null)
				{
					Settings.SettingChanged += linked.LinkedSwitch_SettingChanged;
				}
				else if (newValue == null && oldValue != null)
				{
					Settings.SettingChanged -= linked.LinkedSwitch_SettingChanged;
				}

				if(newValue != null)
				{
					linked.IsToggled = Settings.GetProperty((string)newValue, true);
				}
			}
			catch (Exception e)
			{
				Debug.Fail($"[ERROR] Caught exception {e}");
			}
		}

		public LinkedSwitch()
		{
			this.Toggled += LinkedSwitch_Toggled;
		}
		public LinkedSwitch(string property, bool DefaultValue = true)
		{
			this.LinkedProperty = property;
			Settings.SettingChanged += LinkedSwitch_SettingChanged;
			this.IsToggled = Settings.GetProperty(property, DefaultValue);
		}

		public void LinkedSwitch_Toggled(object sender, ToggledEventArgs e)
		{
			if(string.IsNullOrEmpty(this.LinkedProperty))
			{
				Debug.Fail("[ERROR] LinkedSwitch was toggled without a linked setting");
			}
			else
			{
				Settings.SetProperty(this.LinkedProperty, e.Value);
			}
		}

		public void LinkedSwitch_SettingChanged(object sender, SettingsChangedEventArgs args)
		{
			if(args.SettingChanged == this.LinkedProperty)
			{
				this.IsToggled = (bool)args.NewValue;
			}
		}
	}
}
