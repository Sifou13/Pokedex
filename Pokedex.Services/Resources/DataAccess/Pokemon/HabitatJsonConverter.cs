using Pokedex.Services.Resources.DataAccess.Pokemon.Contract;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pokedex.Services.Resources.DataAccess.Pokemon
{
    public class PokemonHabitatEnumJsonConverter : JsonConverter<PokemonHabitat>
    {
        public override PokemonHabitat Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var enumString = (string)reader.GetString();
                        
            switch(enumString)
            {
                case "rough-terrain":
                    return PokemonHabitat.RoughTerrain;

                case "waters-edge":
                    return PokemonHabitat.WatersEdge;

                default:
                    return (PokemonHabitat)Enum.Parse(typeof(PokemonHabitat), enumString, true);
            }               
        }

        public override void Write(Utf8JsonWriter writer, PokemonHabitat value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
