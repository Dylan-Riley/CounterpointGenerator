using System.Threading.Tasks;

namespace CounterpointGenerator
{
    /**
     * An interface for the component of the system that takes in generated polyphonic music
     * and exposes it to the user.
     */
    public interface IOutputTranslator
    {
        /**
         * Expose composed polyphonic music in intermediate representation
         * to the user.
         */
        Task TranslateOutput(IOutput output);
    }
}
