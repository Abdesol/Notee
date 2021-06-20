using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Notee
{

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            BindingContext = new NoteViewModel(Navigation);
            InitializeComponent();
        }

        public async void NoteSelected(object sender, EventArgs e)
        {
            var noteframe = (Frame)sender;
            await noteframe.ScaleTo(0.9, 70);
            await noteframe.ScaleTo(1, 70);

            int index = NotesList.TabIndex;
            Console.WriteLine($"You clicked on {index}");


        }

        public async void AddClicked(object sender, EventArgs e)
        {
            await addbtn.ScaleTo(0.75, 60);
            await addbtn.ScaleTo(1, 60);
            await Navigation.PushModalAsync(new NotePage(true), true);
        }

        public async void SortClicked(object sender, EventArgs e)
        {
            await sortbtn.ScaleTo(0.75, 60);
            await sortbtn.ScaleTo(1, 60);
        }
    }
}
