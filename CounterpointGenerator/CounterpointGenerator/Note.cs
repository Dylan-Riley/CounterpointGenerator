using System;
namespace CounterpointGenerator
{
    public class Note
    {
        int NoteUnit { get; set; }

        public Note(string item)
        {
            NoteUnit = Int32.Parse(item);
        }

    }
}
