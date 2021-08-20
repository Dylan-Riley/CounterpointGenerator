using System;
using System.Threading.Tasks;
using System.Threading;

namespace CounterpointGenerator
{
    /**
     * An interface for the component of the system that takes in user input and translates it to the intermediate representation
     * that IGenerator can consume.
     */
    public interface IInputTranslator
    {
        /**
         * Get input frtom user and convert to intermediate representation form IInput.
         */
        Task<IInput> GetInput();
    }
}
