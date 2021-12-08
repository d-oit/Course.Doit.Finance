namespace BlazorHero.CleanArchitecture.Shared.Constants.Localization
{
    public static class LocalizationConstants
    {
        public static readonly LanguageCode[] SupportedLanguages = {
            new LanguageCode
            {
                Code = "de-DE",
                DisplayName = "Deutsch"
            },
            new LanguageCode
            {
                Code = "en-US",
                DisplayName = "English"
            }
        };
    }
}
