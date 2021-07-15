using System;
namespace CounterpointGenerator
{
    /**
     * An interface for the input given to IGenerator.
     * It should include user the cantus firmus and any user preferences that need 
     * to be passed to the generator.
     */
    public interface IInput
    {
        /**
         * The cantus firmus.
         */
        System.Collections.Generic.List<int> Cantus { get; set; }

        string ToString();
    }
}
