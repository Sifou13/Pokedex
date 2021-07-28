using Moq;
using Pokedex.Services.Resources.Contract;
using Pokedex.Services.Resources.DataAccess.Pokemon;
using System.Threading.Tasks;

namespace Pokedex.Services.UnitTesting.UnitTestingHelpers
{
    public static class MockHelper
    {
        #region Pokemon Resource
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
        #endregion

        #region PokemonDA
        public static void ExpectSelectByName(this Mock<IPokemonDA> pokemonDA, string name, Pokedex.Services.Resources.DataAccess.Pokemon.PokemonRoot pokemonRootToReturn)
        {
            pokemonDA
                .Setup(x => x.SelectByName(name))
                .Returns(Task.FromResult(pokemonRootToReturn))
                .Verifiable();
        }

        public static void VerifySelectByName(this Mock<IPokemonDA> pokemonResource, string name, Times? callCount = null)
        {
            pokemonResource.Verify(x => x.SelectByName(name), callCount ?? Times.AtMostOnce());
        }

        public static void ExpectSelectSpeciesByUrl(this Mock<IPokemonDA> pokemonDA, string speciesURL, Pokedex.Services.Resources.DataAccess.Pokemon.PokemonSpecies pokemonSpeciesToReturn)
        {
            pokemonDA
                .Setup(x => x.SelectSpeciesByUrl(speciesURL))
                .Returns(Task.FromResult(pokemonSpeciesToReturn))
                .Verifiable();
        }

        public static void VerifySelectSpeciesByUrl(this Mock<IPokemonDA> pokemonResource, string speciesURL, Times? callCount = null)
        {
            pokemonResource.Verify(x => x.SelectSpeciesByUrl(speciesURL), callCount ?? Times.AtMostOnce());
        }
        #endregion
    }
}
