using System;
namespace CounterpointGenerator
{
    public class Note
    {
        public int Pitch { get; set; }
        //Default to whole notes
        public double Length { get; set; } = Constants.WHOLE_NOTE_LENGTH;

        public Note(int pitch)
        {
            this.Pitch = pitch;
        }

        public Note(int pitch, int length)
        {
            this.Pitch = pitch;
            this.Length = length;
        }

        public Boolean Equals(Note n)
        {
            return (this.Pitch == n.Pitch && this.Length.Equals(n.Length));
        }

        public override string ToString()
        {
            return $"(pitch: {Pitch}, length: {Length})";
        }
    }
}
