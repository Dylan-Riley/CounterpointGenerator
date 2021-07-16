using System;

namespace GenerateCounterpoint
{
    public interface IWeightSelect
    {
        public Set<Note> selectPossibilities(Set<Note> posibilities);
    }
}