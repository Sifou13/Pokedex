using System.Threading.Tasks;

namespace Pokedex.Services.Resources.DataAccess.Pokemon.Contract
{
    public interface IPokemonDA
    {
        Task<PokemonRoot> SelectByName(string name);

        Task<PokemonSpecies> SelectSpeciesByUrl(string url);
    }

}
