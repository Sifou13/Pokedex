namespace Pokedex.Services.Resources.DataAccess.Translators.Contract
{
    public class TranslationResult
    {
        public Success Success { get; set; }

        public Contents Contents{ get; set; }
    }

    public class Contents
    {
        public string Translated { get; set; }
    }

    public class Success
    {
        public int Total { get; set; }
    }
}
