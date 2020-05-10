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
		public static readonly BindableProperty LinkedProperty = BindableProperty.Create(
														 propertyName: "LinkedPropertyText",
														 returnType: typeof(string),
														 declaringType: typeof(LinkedSwitch),
														 defaultBindingMode: BindingMode.TwoWay,
														 propertyChanged: LinkedPropertyChanged);

		public string LinkedPropertyText
		{
			get { return base.GetValue(LinkedProperty).ToString(); }
			set { base.SetValue(LinkedProperty, value); }
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

		public void LinkedSwitch_Toggled(object sender, ToggledEventArgs e)
		{
			if(string.IsNullOrEmpty(this.LinkedPropertyText))
			{
				Debug.Fail("[ERROR] LinkedSwitch was toggled without a linked setting");
			}
			else
			{
				Settings.SetProperty(this.LinkedPropertyText, e.Value);
			}
		}

		public LinkedSwitch(string property)
		{
			this.LinkedPropertyText = property;
			Settings.SettingChanged += LinkedSwitch_SettingChanged;
		}

		public void LinkedSwitch_SettingChanged(object sender, SettingsChangedEventArgs args)
		{
			if(args.SettingChanged == this.LinkedPropertyText)
			{
				this.IsToggled = (bool)args.NewValue;
			}
		}
	}
}
