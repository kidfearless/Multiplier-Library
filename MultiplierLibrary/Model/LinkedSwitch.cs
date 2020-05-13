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
				LinkedSwitch linked = bindable as LinkedSwitch;

				if(newValue != null)
				{
					bool prop = Settings.GetProperty((string)newValue, true);
					if (linked.IsToggled != prop)
					{
						linked.IsToggled = prop;
					}
				}
			}
			catch (Exception e)
			{
				Debug.Fail($"[ERROR] Caught exception {e}");
			}
		}

		public LinkedSwitch(string property)
		{
			this.LinkedProperty = property;
			Settings.SettingChanged += LinkedSwitch_SettingChanged;
			this.Toggled += LinkedSwitch_Toggled;
			this.IsToggled = Settings.GetProperty(property, true);
		}
		public LinkedSwitch(string property, bool DefaultValue)
		{
			this.LinkedProperty = property;
			Settings.SettingChanged += LinkedSwitch_SettingChanged;
			this.Toggled += LinkedSwitch_Toggled;
			this.IsToggled = Settings.GetProperty(property, DefaultValue);
		}

		// This currently fires an extra time for each time you switch pages.
		// Please fix later
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
			if(args.SettingChanged == this.LinkedProperty && this.IsToggled != (bool)args.NewValue)
			{
				this.IsToggled = (bool)args.NewValue;
			}
		}
	}
}
