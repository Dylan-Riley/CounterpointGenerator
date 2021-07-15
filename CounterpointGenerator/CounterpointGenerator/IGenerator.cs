using System;
using System.Threading.Tasks;

namespace CounterpointGenerator
{
    public interface IGenerator
    {
        Task<IOutput> Generate(IInput input);
    }
}
