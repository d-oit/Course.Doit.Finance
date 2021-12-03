using Blazored.FluentValidation;
using BlazorHero.CleanArchitecture.Application.Features.Finance.Transactions.Commands.AddEdit;
using BlazorHero.CleanArchitecture.Client.Extensions;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Finance.Transaction;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Finance.FinanceAccount;
using BlazorHero.CleanArchitecture.Application.Requests;

namespace BlazorHero.CleanArchitecture.Client.Pages.Finance
{
    public partial class AddEditTransactionModal
    {
        [Inject] private ITransactionManager TransactionManager { get; set; }
        [Inject] private IFinanceAccountManager FinanceAccountManager { get; set; }

        [Parameter] public AddEditTransactionCommand AddEditTransactionModel { get; set; } = new();
        [CascadingParameter] private HubConnection HubConnection { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private IEnumerable<NameIntValueResponse> _financeAccounts = new List<NameIntValueResponse>();

        public void Cancel()
        {
            MudDialog.Cancel();
        }

        private async Task SaveAsync()
        {
            var response = await TransactionManager.SaveAsync(AddEditTransactionModel);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
                await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
                MudDialog.Close();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadFinanceAccountsAsync();
            HubConnection = HubConnection.TryInitialize(_navigationManager, _localStorage);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task LoadFinanceAccountsAsync()
        {
            _financeAccounts = await FinanceAccountManager.GetFinanceAccountNamesAsync();
        }

        private async Task<IEnumerable<int>> SearchFinanceAccounts(string value)
        {
            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
            {
                var singleList = _financeAccounts.Select(x => x.Id);
                return await Task.Run(() => singleList);
            }

            var list = _financeAccounts
                .Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.Id);
            return await Task.Run(() => list);
        }
    }
}