using Pokedex.Api.Models;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pokedex.Api.Framework.JsonConverters
{
    public class PokemonHabitatEnumJsonConverter : JsonConverter<PokemonHabitat>
    {
        public override PokemonHabitat Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        //This will require some integration testing
        public override void Write(Utf8JsonWriter writer, PokemonHabitat value, JsonSerializerOptions options)
        {
            switch(value)
            {
                case PokemonHabitat.RoughTerrain:
                    writer.WriteStringValue("rough-terrain");
                    return;

                case PokemonHabitat.WatersEdge:
                    writer.WriteStringValue("waters-edge");
                    return;

                default:
                    writer.WriteStringValue((Enum.GetName(typeof(PokemonHabitat), (int)value).ToLower()));
                    return;
            }
        }
    }
}
