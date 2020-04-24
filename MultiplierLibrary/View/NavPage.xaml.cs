using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;

namespace MultiplierLibrary
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavPage : Xamarin.Forms.TabbedPage
    {
        public NavPage()
        {
            // by default is set to the root directory of the UWP project
            Xamarin.Forms.Application.Current.On<Windows>().SetImageDirectory("Assets");
            On<Windows>().SetHeaderIconsEnabled(true);
            On<Windows>().SetHeaderIconsSize(new Size(24, 24));

            InitializeComponent();


            if(Device.RuntimePlatform != Device.iOS)
            {
                HomePage.IconImageSource = "ic_home_variant_grey600_24dp.png";
                ProblemPage.IconImageSource = "ic_sigma_grey600_24dp.png";
            }
        }
    }
}