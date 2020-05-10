using MultiplierLibrary.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MultiplierLibrary.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoundResults : ContentPage
    {
        public RoundResults()
        {
            InitializeComponent();

        }
        public void OnReturnClicked(object sender, EventArgs e)
        {
            Navigator.GoToProblemsPage();
        }

        public void OnAgainClicked(object sender, EventArgs e)
        {
            Navigator.StartGame();
        }
    }

    
}