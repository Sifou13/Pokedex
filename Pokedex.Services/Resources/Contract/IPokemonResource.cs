namespace Pokedex.Services.Resources.Contract
{
    public interface IPokemonResource
    {
        PokemonBasic SelectByName(string name);
    }
}
