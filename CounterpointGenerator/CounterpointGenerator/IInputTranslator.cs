using System;
using System.Threading.Tasks;

namespace CounterpointGenerator
{
    public interface IInputTranslator
    {
        Task<IInput> GetInput();
    }
}
