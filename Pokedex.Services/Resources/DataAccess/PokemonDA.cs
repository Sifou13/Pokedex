namespace Pokedex.Services.Resources.DataAccess
{
    public interface IPokemonDA
    {
        PokemonBasic SelectByName(string name);
    }

    public class PokemonDA : IPokemonDA
    {
        public PokemonBasic SelectByName(string name)
        {
            return new PokemonBasic
            {
                Name = name,
                Description = $"We don't yet have a description for {name}, but we promise, it's coming soon",
                PokemonHabitat = PokemonHabitat.Cave,
                IsLegendary = true
            };
        }
    }
}
