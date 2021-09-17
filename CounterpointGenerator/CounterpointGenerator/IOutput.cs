using System;
using System.Collections.Generic;

namespace CounterpointGenerator
{
    /**
     * The interface for the part of the system that takes in generated music and exposes it to the user.
     */
    public interface IOutput
    {
        /**
         * Cantus firmus.
         */
        List<MelodyLine> Cantus { get; set; }

        // Add something for the generated counterpoint.

        string ToString();

        // Gather up entries in Cantus based on like features
        void Collect();
        string CompleteToString();
        string CloseToToString();
    }
}
