using Moq;
using Pokedex.Services.Resources.Contract;

namespace Pokedex.Services.UnitTesting
{
    public static class MockHelper
    {
        public static void ExpectSelectByName(this Mock<IPokemonResource> pokemonResource, string name, Pokedex.Services.Resources.Contract.PokemonBasic pokemonToReturn)
        {
            pokemonResource
                .Setup(x => x.SelectByName(name))
                .Returns(pokemonToReturn)
                .Verifiable();
        }

        public static void VerifySelectByName(this Mock<IPokemonResource> pokemonResource, string name, Times? callCount = null)
        {
            pokemonResource.Verify(x => x.SelectByName(name), callCount ?? Times.AtMostOnce());
        }
    }
}
