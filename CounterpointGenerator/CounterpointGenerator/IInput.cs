using System;
namespace CounterpointGenerator
{
    public interface IInput
    {
        System.Collections.Generic.List<int> Cantus { get; set; }

        string ToString();
    }
}
