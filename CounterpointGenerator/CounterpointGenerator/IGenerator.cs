using System;
using System.Threading.Tasks;

namespace CounterpointGenerator
{
    /**
     * An inpterface for the component of the system that generates polyphonic counterpoint
     * given an input cantus firmus in intermediate representation.
     */
    public interface IGenerator
    {
        /**
         * Given user input IInput compose possible counterpoints and return as IOutput.
         */
        Task<IOutput> Generate(IInput input);
    }
}
