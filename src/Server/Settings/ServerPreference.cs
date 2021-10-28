using BlazorHero.CleanArchitecture.Shared.Constants.Localization;
using BlazorHero.CleanArchitecture.Shared.Settings;
using System.Linq;

namespace BlazorHero.CleanArchitecture.Server.Settings
{
    public record ServerPreference : IPreference
    {
        public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "de-DE";

        //TODO - add server preferences
    }
}