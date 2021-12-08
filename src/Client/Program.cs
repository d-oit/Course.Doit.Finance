using BlazorHero.CleanArchitecture.Client.Extensions;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Preferences;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Settings;
using BlazorHero.CleanArchitecture.Shared.Constants.Localization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Client
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder
                          .CreateDefault(args)
                          .AddRootComponents()
                          .AddClientServices();

            var host = builder.Build();
            var storageService = host.Services.GetRequiredService<ClientPreferenceManager>();
            CultureInfo culture = culture = new CultureInfo(LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "de-DE");

            if (storageService != null)
            {
                var preference = await storageService.GetPreference() as ClientPreference;
                if (preference != null)
                {
                    culture = new CultureInfo(preference.LanguageCode);
                    var js = host.Services.GetRequiredService<IJSRuntime>();
                    var result = await js.InvokeAsync<string>("blazorCulture.get");
                    await js.InvokeVoidAsync("blazorCulture.set", preference.LanguageCode);
                }
            }
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;


            await builder.Build().RunAsync();
        }
    }
}