using Moq;
using Pokedex.Services.Contract;
using System.Threading.Tasks;

namespace Pokedex.Api.UnitTesting
{
    public static class MockHelper
    {
        public static void ExpectGetPokemonDetailsAsync(this Mock<IPokemonInformationOrchestrator> pokemonInformationOrchestrator, string name, Pokedex.Services.Contract.PokemonBasic pokemonToReturn)
        {
            pokemonInformationOrchestrator
                .Setup(x => x.GetPokemonDetailsAsync(name))
                .Returns(Task.FromResult(pokemonToReturn))
                .Verifiable();
        }

        public static void VerifyGetPokemonDetailsAsync(this Mock<IPokemonInformationOrchestrator> pokemonInformationOrchestrator, string name, Times? callCount = null)
        {
            pokemonInformationOrchestrator.Verify(x => x.GetPokemonDetailsAsync(name), callCount ?? Times.AtMostOnce());
        }
    }
}
