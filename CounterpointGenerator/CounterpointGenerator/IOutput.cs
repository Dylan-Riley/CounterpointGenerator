using System;
using System.Collections.Generic;

namespace CounterpointGenerator
{
    public interface IOutput
    {
        List<int> Cantus { get; set; }

        // Add something for the generated counterpoint.

        string ToString();
    }
}
