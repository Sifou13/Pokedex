using AutoMapper;
using Pokedex.Clients;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pokedex.Api.Clients
{
    public interface IPokemonInformationServiceClient
    {
        Task<Models.PokemonBasic> GetPokemonDetailsAsync(string pokemonName);
        
        Task<Models.PokemonBasic> GetTranslatedPokemonDetailsAsync(string pokemonName);
    }

    public class PokemonInformationServiceClient : IPokemonInformationServiceClient
    {
        private readonly IPokemonInformationService.IPokemonInformationServiceClient pokemonInformationServiceClient;
        private readonly IMapper mapper;

        public PokemonInformationServiceClient(IMapper mapper)
        {
            this.mapper = mapper;

            HttpClientHandler httpHandler = new HttpClientHandler();

            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            Grpc.Net.Client.GrpcChannel channel = Grpc.Net.Client.GrpcChannel.ForAddress(new System.Uri("https://localhost:5001/"), new Grpc.Net.Client.GrpcChannelOptions { HttpHandler = httpHandler });

            pokemonInformationServiceClient = new IPokemonInformationService.IPokemonInformationServiceClient(channel);

        }

        public async Task<Models.PokemonBasic> GetPokemonDetailsAsync(string pokemonName)
        {
            return mapper.Map<Models.PokemonBasic>(await pokemonInformationServiceClient.GetPokemonDetailsAsync(new PokemonRequest { PokemonName = pokemonName }));
        }

        public async Task<Models.PokemonBasic> GetTranslatedPokemonDetailsAsync(string pokemonName)
        {
            return mapper.Map<Models.PokemonBasic>(await pokemonInformationServiceClient.GetTranslatedPokemonDetailsAsync(new PokemonRequest { PokemonName = pokemonName }));
        }
    }
}
