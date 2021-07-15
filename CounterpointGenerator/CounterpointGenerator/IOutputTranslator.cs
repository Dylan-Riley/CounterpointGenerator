using System.Threading.Tasks;

namespace CounterpointGenerator
{
    public interface IOutputTranslator
    {
        Task TranslateOutput(IOutput output);
    }
}
