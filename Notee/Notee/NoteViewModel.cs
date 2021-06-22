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
    public partial class NoteViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<NoteModel> AllNotes { get; set; }

        public NoteViewModel()
        {
            var db = new DatabaseHandler();
            AllNotes = db.GetNotes();

            //AllNotes = new ObservableCollection<NoteModel>();
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
            if(IsNew.isit == true)
            {
                var time = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                var newnote = new NoteModel(title, note, Convert.ToInt32(time));
                AllNotes.Add(newnote);
                MessagingCenter.Send<NoteModel>(newnote, "Add");

                title = string.Empty;
                note = string.Empty;

                await (Application.Current as App).MainPage.Navigation.PopModalAsync(true);

            }

        }
    }
}
