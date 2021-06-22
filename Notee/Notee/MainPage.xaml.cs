using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Notee
{

    public partial class MainPage : ContentPage
    {
        public DatabaseHandler db;
        public MainPage()
        {
            BindingContext = new NoteViewModel();
            InitializeComponent();

            db = new DatabaseHandler();

            MessagingCenter.Subscribe<NoteModel>(this, "Add", (Obj) =>
            {
                int id = db.AddNote(Obj);
                Obj.id = id;
                (BindingContext as NoteViewModel).AllNotes.Add(Obj);
            });

            MessagingCenter.Subscribe<NoteModel>(this, "Del", (Obj) =>
            {
                db.DelNote(Obj.id);
                (BindingContext as NoteViewModel).AllNotes.Remove(Obj);
            });

            MessagingCenter.Subscribe<List<object>>(this, "Edit", (Obj) =>
            {
                db.EditNote((NoteModel)Obj[0]);
                (BindingContext as NoteViewModel).AllNotes[(int)Obj[1]] = (NoteModel)Obj[0];
            });
        }

        public async void NoteSelected(object sender, EventArgs e)
        {
            var noteframe = (Frame)sender;
            var notemodel = (NoteModel)(noteframe).BindingContext;
            await noteframe.ScaleTo(0.9, 70);
            await noteframe.ScaleTo(1, 70);

            int index = NotesList.TabIndex;
            Console.WriteLine($"You clicked on {index}");

            await Navigation.PushModalAsync(new NotePage(false, notemodel, 0), true);
        }

        public async void AddClicked(object sender, EventArgs e)
        {
            await addbtn.ScaleTo(0.75, 60);
            await addbtn.ScaleTo(1, 60);
            await Navigation.PushModalAsync(new NotePage(true, null, 0), true);
        }

        public async void SortClicked(object sender, EventArgs e)
        {
            await sortbtn.ScaleTo(0.75, 60);
            await sortbtn.ScaleTo(1, 60);

            Device.BeginInvokeOnMainThread(() =>
            {
                if (!sort_picker.IsFocused)
                    sort_picker.Focus();
            });
        }

        public void PickerSelected(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            if(selectedIndex == 0)
            {
                var temp = new ObservableCollection<NoteModel>((BindingContext as NoteViewModel).AllNotes.OrderBy(x => x.title).ToList());
                (BindingContext as NoteViewModel).AllNotes.Clear();
                foreach (NoteModel i in temp) (BindingContext as NoteViewModel).AllNotes.Add(i);

            }
            else
            {
                var temp = new ObservableCollection<NoteModel>((BindingContext as NoteViewModel).AllNotes.OrderBy(x => x.timeunix).ToList());
                (BindingContext as NoteViewModel).AllNotes.Clear();
                foreach (NoteModel i in temp) (BindingContext as NoteViewModel).AllNotes.Add(i);
            }
        }
    }
}
