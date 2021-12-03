using BlazorHero.CleanArchitecture.Application.Features.Finance.Investments.Queries.GetAllPaged;
using BlazorHero.CleanArchitecture.Application.Requests;
using BlazorHero.CleanArchitecture.Application.Requests.Finance;
using BlazorHero.CleanArchitecture.Client.Extensions;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Finance.FinanceAccount;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Finance.Investment;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using BlazorHero.CleanArchitecture.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Client.Pages.Finance
{
    public partial class Investments
    {
        [Inject] private IInvestmentManager InvestmentManager { get; set; }

        [Inject] private IFinanceAccountManager FinanceAccountManager { get; set; }

        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private IEnumerable<GetAllPagedInvestmentsResponse> _pagedData;
        private MudTable<GetAllPagedInvestmentsResponse> _table;
        private int _totalItems;
        private int _currentPage;
        private string _searchString = "";
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;

        private ClaimsPrincipal _currentUser;
        private bool _canCreateInvestments;
        private bool _canEditInvestments;
        private bool _canDeleteInvestments;
        private bool _canSearchInvestments;
        private bool _loaded;
        private List<NameIntValueResponse> _financeAccountNames;

        private bool FilterByFinanceAccount { get; set; }

        private NameIntValueResponse FilterFinanceAccount { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateInvestments = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Investments.Create)).Succeeded;
            _canEditInvestments = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Investments.Edit)).Succeeded;
            _canDeleteInvestments = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Investments.Delete)).Succeeded;
            _canSearchInvestments = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Investments.Search)).Succeeded;

            _loaded = true;
            HubConnection = HubConnection.TryInitialize(_navigationManager, _localStorage);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task<IEnumerable<NameIntValueResponse>> SearchFinanceAccount(string value)
        {
            if (_financeAccountNames == null)
            {
                _financeAccountNames = await FinanceAccountManager.GetFinanceAccountNamesAsync();
            }
            FilterByFinanceAccount = false;
            if (FilterFinanceAccount != null && FilterFinanceAccount.Id > 0)
            {
                FilterByFinanceAccount = true;
            }
            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return _financeAccountNames;

            return _financeAccountNames.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }

        private async Task<TableData<GetAllPagedInvestmentsResponse>> ServerReload(TableState state)
        {
            if (!string.IsNullOrWhiteSpace(_searchString))
            {
                state.Page = 0;
            }
            await LoadData(state.Page, state.PageSize, state);
            return new TableData<GetAllPagedInvestmentsResponse> { TotalItems = _totalItems, Items = _pagedData };
        }

        private async Task LoadData(int pageNumber, int pageSize, TableState state)
        {

            string[] orderings = new[] { "Name" };
            if (!string.IsNullOrEmpty(state.SortLabel))
            {
                orderings = state.SortDirection != SortDirection.None ? new[] { $"{state.SortLabel} {state.SortDirection}" } : new[] { $"{state.SortLabel}" };
            }

            var request = new GetAllPagedInvestmentsRequest { PageSize = pageSize, PageNumber = pageNumber + 1, SearchString = _searchString, Orderby = orderings };
            var response = await InvestmentManager.GetInvestmentsAsync(request);
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

        private async Task InvokeModal(long id = 0)
        {
            var parameters = new DialogParameters();
            if (id != 0)
            {
                var investment = await InvestmentManager.GetByIdAsync(id);
                if (investment != null)
                {
                    parameters.Add(nameof(AddEditInvestmentModal.AddEditInvestmentModel), investment);
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditInvestmentModal>(id == 0 ? _localizer["Create"] : _localizer["Edit"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                OnSearch("");
            }
        }

        private async Task Delete(long id)
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
                var response = await InvestmentManager.DeleteAsync(id);
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