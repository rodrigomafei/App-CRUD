using CRUD.Models;
using CRUD.Services.Interfaces;
using CRUD.Util;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CRUD.ViewModels
{
    public class CompanyEditViewModel : ViewModelBase
    {
        public ICompanyService CompanyService { get; set; }
        public IPageDialogService PageDialogService { get; set; }
        public CompanyEditViewModel(
            INavigationService navigationService, 
            ICompanyService companyService,
            IPageDialogService pageDialogService
            ) : base(navigationService)
        {
            CompanyService = companyService;
            PageDialogService = pageDialogService;

            SaveCommand = new DelegateCommand(Save);
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            SelectedCompany = parameters.ContainsKey(Constants.Company) ? parameters[Constants.Company] as Company : new Company();

            await LoadData();
        }

        private async Task LoadData()
        {
            Title = string.IsNullOrWhiteSpace(SelectedCompany.Name) ? "Nova Pizzaria" : SelectedCompany.Name;
        }

        private async void Save()
        {
            try
            {
                var company = string.IsNullOrWhiteSpace(SelectedCompany.Id) ?  await CreateCompany() : await UpdateCompany();

                if (company != null)
                    await NavigationService.GoBackAsync();
            }

            catch(Exception e)
            {

            }
        }

        private async Task<Company> CreateCompany()
        {
            var cmd = SelectedCompany.ToCreateCommand();

            if (!cmd.IsValid())
            {
                await PageDialogService.DisplayAlertAsync("Validação", cmd.ErrorMsg, "Ok");

                return null;
            }

            return CompanyService.Create(cmd);
        }

        private async Task<Company> UpdateCompany()
        {
            var cmd = SelectedCompany.ToUpdateCommand();

            if (!cmd.IsValid())
            {
                await PageDialogService.DisplayAlertAsync("Validação", cmd.ErrorMsg, "Ok");

                return null;
            }

            return CompanyService.Update(cmd);
        }

        #region Commands

        public ICommand SaveCommand { get; set; }

        #endregion

        #region Props

        Company _selectedCompany;
        public Company SelectedCompany
        {
            get => _selectedCompany;
            set => SetProperty(ref _selectedCompany, value);
        }

        #endregion
    }
}
