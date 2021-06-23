using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notee
{
    public static class IsNew
    {
        public static bool isit = true;
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotePage : ContentPage
    {
        private NoteModel note_model;
        private int index;
        public NotePage(bool isnew, NoteModel model, int ind)
        {
            BindingContext = new NoteViewModel();
            InitializeComponent();

            if (isnew)
            {
                IsNew.isit = true;
                SaveBtn.Opacity = 10.0; 
                EditBtn.Opacity = 0.2; EditBtn.IsEnabled = false;
                DelBtn.Opacity = 0.2; DelBtn.IsEnabled = false;

                TitleField.IsEnabled = true; NoteField.IsEnabled = true;
            }
            else
            {
                IsNew.isit = false;
                this.index = ind;
                this.note_model = model;
                SaveBtn.Opacity = 0.2; SaveBtn.IsTabStop = false;
                EditBtn.Opacity = 10; EditBtn.IsEnabled = true;
                DelBtn.Opacity = 10; DelBtn.IsEnabled = true;

                TitleField.Text = model.title;
                NoteField.Text = model.description;

                TitleField.IsEnabled = false; NoteField.IsEnabled = false;
            }

        }

        public async void BackClicked(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;
            await btn.ScaleTo(0.75, 60);
            await btn.ScaleTo(1, 60);
            if(String.IsNullOrWhiteSpace(NoteField.Text) == false)
            {
                if (IsNew.isit == true)
                {
                    var new_note_model = new NoteModel(TitleField.Text, NoteField.Text, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds());
                    MessagingCenter.Send<NoteModel>(new_note_model, "Add");
                }
                else
                {
                    MessagingCenter.Send<List<object>>(new List<object>() { note_model, index }, "Edit");
                }
            }

            await Navigation.PopModalAsync(true);
        }

        public async void EditClicked(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;
            await btn.ScaleTo(0.75, 60);
            await btn.ScaleTo(1, 60);

            SaveBtn.Opacity = 10.0; SaveBtn.IsTabStop = true;
            EditBtn.Opacity = 0.2; EditBtn.IsEnabled = false;
            DelBtn.Opacity = 0.2; DelBtn.IsEnabled = false;

            TitleField.IsEnabled = true; NoteField.IsEnabled = true;

        }
        public async void DeleteClicked(object sender, EventArgs e)
        {

            var btn = (ImageButton)sender;
            await btn.ScaleTo(0.75, 60);
            await btn.ScaleTo(1, 60);

            MessagingCenter.Send<NoteModel>(note_model, "Del");
            await (Application.Current as App).MainPage.Navigation.PopModalAsync(true);
        }

        public async void SaveClicked(object sender, EventArgs e)
        {
            if (IsNew.isit != true)
            {
                if (String.IsNullOrWhiteSpace(NoteField.Text) == true)
                {
                    Acr.UserDialogs.UserDialogs.Instance.Toast("Please add a Note!", new TimeSpan(1));
                }
                else
                {
                    note_model.title = TitleField.Text;
                    note_model.description = NoteField.Text;
                    MessagingCenter.Send<List<object>>(new List<object>() { note_model, index }, "Edit");
                    await (Application.Current as App).MainPage.Navigation.PopModalAsync(true);
                }
            }
        }
    }
}