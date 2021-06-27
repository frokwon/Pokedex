using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonServices
{
    public interface ITranslationService
    {
        string Translate(string text, string type);
    }
}
