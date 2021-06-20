﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Notee
{
    class NoteModel
    {
        public string title { get; set; }
        public string description { get; set; }
        public string timestamp { get; set; }

        public NoteModel(string title, string description, int timestamp)
        {
            this.title = title;
            this.description = description;
            var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(timestamp).ToLocalTime();
            this.timestamp = dt.ToString("d MMM yyyy");
        }
    }
}
