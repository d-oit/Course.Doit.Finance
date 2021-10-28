using BlazorHero.CleanArchitecture.Application.Features.Finance.FinanceAccounts.Queries.GetAllPaged;
using BlazorHero.CleanArchitecture.Application.Requests.Finance;
using BlazorHero.CleanArchitecture.Client.Extensions;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazorHero.CleanArchitecture.Application.Features.Finance.FinanceAccounts.Commands.AddEdit;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Finance.FinanceAccount;
using BlazorHero.CleanArchitecture.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;

namespace BlazorHero.CleanArchitecture.Client.Pages.Finance
{
    public partial class FinanceAccounts
    {
        [Inject] private IFinanceAccountManager FinanceAccountManager { get; set; }

        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private IEnumerable<GetAllPagedFinanceAccountsResponse> _pagedData;
        private MudTable<GetAllPagedFinanceAccountsResponse> _table;
        private int _totalItems;
        private int _currentPage;
        private string _searchString = "";
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;

        private ClaimsPrincipal _currentUser;
        private bool _canCreateFinanceAccounts;
        private bool _canEditFinanceAccounts;
        private bool _canDeleteFinanceAccounts;
        private bool _canSearchFinanceAccounts;
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateFinanceAccounts = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.FinanceAccounts.Create)).Succeeded;
            _canEditFinanceAccounts = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.FinanceAccounts.Edit)).Succeeded;
            _canDeleteFinanceAccounts = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.FinanceAccounts.Delete)).Succeeded;
            _canSearchFinanceAccounts = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.FinanceAccounts.Search)).Succeeded;

            _loaded = true;
            HubConnection = HubConnection.TryInitialize(_navigationManager, _localStorage);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task<TableData<GetAllPagedFinanceAccountsResponse>> ServerReload(TableState state)
        {
            if (!string.IsNullOrWhiteSpace(_searchString))
            {
                state.Page = 0;
            }
            await LoadData(state.Page, state.PageSize, state);
            return new TableData<GetAllPagedFinanceAccountsResponse> { TotalItems = _totalItems, Items = _pagedData };
        }

        private async Task LoadData(int pageNumber, int pageSize, TableState state)
        {
            string[] orderings = null;
            if (!string.IsNullOrEmpty(state.SortLabel))
            {
                orderings = state.SortDirection != SortDirection.None ? new[] {$"{state.SortLabel} {state.SortDirection}"} : new[] {$"{state.SortLabel}"};
            }

            var request = new GetAllPagedFinanceAccountsRequest { PageSize = pageSize, PageNumber = pageNumber + 1, SearchString = _searchString, Orderby = orderings };
            var response = await FinanceAccountManager.GetFinanceAccountsAsync(request);
            if (response.Succeeded)
            {
                _totalItems = response.TotalCount;
                _currentPage = response.CurrentPage;
                _pagedData = response.Data;
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private void OnSearch(string text)
        {
            _searchString = text;
            _table.ReloadServerData();
        }

        private async Task InvokeModal(int id = 0)
        {
            var parameters = new DialogParameters();
            if (id != 0)
            {
                var financeaccount = _pagedData.FirstOrDefault(c => c.Id == id);
                if (financeaccount != null)
                {
                    parameters.Add(nameof(AddEditFinanceAccountModal.AddEditFinanceAccountModel), new AddEditFinanceAccountCommand
                    {
                        Id = financeaccount.Id,
                        Name = financeaccount.Name,
                        Code = financeaccount.Code,
                        Owner = financeaccount.Owner,
                        Type = financeaccount.Type,
                        CountryCode = financeaccount.CountryCode,
                        SortOrder = financeaccount.SortOrder,
                        IsActive = financeaccount.IsActive,
                        InactiveDate = financeaccount.InactiveDate,
                        InitAmount = financeaccount.InitAmount,
                        IsBankAccount = financeaccount.IsBankAccount,
                        IsInvestmentAccount = financeaccount.IsInvestmentAccount
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditFinanceAccountModal>(id == 0 ? _localizer["Create"] : _localizer["Edit"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                OnSearch("");
            }
        }

        private async Task Delete(int id)
        {
            string deleteContent = _localizer["Delete Content"];
            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent, id)}
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>(_localizer["Delete"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await FinanceAccountManager.DeleteAsync(id);
                if (response.Succeeded)
                {
                    OnSearch("");
                    await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
                    _snackBar.Add(response.Messages[0], Severity.Success);
                }
                else
                {
                    OnSearch("");
                    foreach (var message in response.Messages)
                    {
                        _snackBar.Add(message, Severity.Error);
                    }
                }
            }
        }
    }
}