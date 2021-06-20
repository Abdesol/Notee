using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notee
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotePage : ContentPage
    {
        public double new_opc = 0.5;
        public double non_new_opc = 10.0;

        public bool new_blocked = true;
        public bool non_new_blocked = false;
        public NotePage(bool isnew)
        {
            BindingContext = new NoteViewModel(Navigation);
            InitializeComponent();
            if (isnew)
            {
                new_opc = 10.0;
                non_new_opc = 0.5;
                new_blocked = false;
                non_new_blocked = true;
            }
        }

        public async void BackClicked(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;
            await btn.ScaleTo(0.75, 60);
            await btn.ScaleTo(1, 60);

            await Navigation.PopModalAsync(true);
        }

        public bool towrite = true;
        public async void EditClicked(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;
            await btn.ScaleTo(0.75, 60);
            await btn.ScaleTo(1, 60);
        }
        public async void DeleteClicked(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;
            await btn.ScaleTo(0.75, 60);
            await btn.ScaleTo(1, 60);
        }
    }
}