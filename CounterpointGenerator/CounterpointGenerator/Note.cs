using System;
namespace CounterpointGenerator
{
    public class Note
    {
        public int Pitch { get; set; }
        public int Length { get; set; } = 4; //Measure in beats, default whole note

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
            return (this.Pitch == n.Pitch && this.Length == n.Length);
        }
    }
}
