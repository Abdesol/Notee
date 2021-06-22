using SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Notee
{
    [SQLite.Table("Notes")]
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        [SQLite.Column("id")]
        public int Id { get; set; }

        [SQLite.Column("title")]
        public string title { get; set; }

        [SQLite.Column("description")]
        public string description { get; set; }

        [SQLite.Column("timestamp")]
        public long timestamp { get; set; }
    }

    public class DatabaseHandler
    {
        private SQLiteConnection _db;
        public DatabaseHandler()
        {
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DataBase.db");
            _db = new SQLiteConnection(path);
            _db.CreateTable<Note>();
        }

        public int AddNote(NoteModel model)
        {
            var note = new Note
            {
                title = model.title,
                description = model.description,
                timestamp = model.timeunix
            };
            _db.Insert(note);

            int last_id = (int)SQLite3.LastInsertRowid(_db.Handle);
            return last_id;
        }

        public void DelNote(int id)
        {
            _db.Delete<Note>(id);
        }

        public ObservableCollection<NoteModel> GetNotes()
        {
            var notes = _db.Query<Note>("SELECT * FROM Notes");

            var all_notes = new ObservableCollection<NoteModel>();
            foreach(var i in notes)
            {
                var model = new NoteModel(i.title, i.description, i.timestamp);
                model.id = i.Id;
                all_notes.Add(model);
            }
            return all_notes;
        }

        public void EditNote(NoteModel model)
        {
            var ex_row = _db.Query<Note>("select * from Notes where Id = ?", model.id).FirstOrDefault();
            if(ex_row != null)
            {
                ex_row.title = model.title;
                ex_row.description = model.description;
                _db.RunInTransaction(() =>
                {
                    _db.Update(ex_row);
                });
            }
        }
    }

}
