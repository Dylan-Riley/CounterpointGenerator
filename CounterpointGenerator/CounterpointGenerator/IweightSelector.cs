using System;
using System.Collections.Generic;

namespace CounterpointGenerator
{
    public interface IWeightSelect
    {
        public List<Note> SelectPossibilities(List<Note> selectFrom);
    }
}