using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Notee
{
    partial class NoteViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public INavigation Navigation { get; set; }
        public ObservableCollection<NoteModel> AllNotes { get; set; }

        public NoteViewModel(INavigation navitation)
        {
            AllNotes = new ObservableCollection<NoteModel>();
            AllNotes.Add(new NoteModel("Maths Assignment", "this is test", 1624160304));
            this.Navigation = navitation;
        }
        

        public ICommand AddNoteCommand => new Command(AddNote);

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string _title { get; set; }
        public string _note { get; set; }

        public string title
        {
            get
            {
                return _title;
            }
            set
            {
                if (value != this._title)
                {
                    _title = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string note
        {
            get
            {
                return _note;
            }
            set
            {
                if (value != this._note)
                {
                    _note = value;
                    NotifyPropertyChanged();
                }
            }
        }

        async void AddNote()
        {
            var time = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            AllNotes.Add(new NoteModel(title, note, Convert.ToInt32(time)));

            title = string.Empty;
            note = string.Empty;

            await Navigation.PushModalAsync(new MainPage(), true);
        }

        public ICommand DelNoteCommand => new Command(DelNote);
        async void DelNote(object o)
        {
            var notemodel = (NoteModel)o;
            AllNotes.Remove(notemodel);

            await Navigation.PushModalAsync(new MainPage(), true);
        }

    }
}
