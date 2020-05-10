using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
namespace MultiplierLibrary.Model
{
	class LinkedSwitch : Switch
	{
		public static readonly BindableProperty LinkedProperty = BindableProperty.Create(
														 propertyName: "LinkedPropertyText",
														 returnType: typeof(string),
														 declaringType: typeof(LinkedSwitch),
														 defaultValue: "",
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
			}
			catch (Exception)
			{

				
			}
		}

		public LinkedSwitch()
		{

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
