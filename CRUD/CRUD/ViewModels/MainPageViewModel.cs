using CRUD.Models;
using CRUD.Services.Interfaces;
using CRUD.Util;
using CRUD.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace CRUD.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ICompanyService CompanyService { get; set; }
        public IPageDialogService PageDialogService { get; set; }


        public MainPageViewModel(
            INavigationService navigationService,
            ICompanyService companyService, 
            IPageDialogService pageDialogService
            )
            : base(navigationService)
        {

            Title = "Delivery Pizzas";

            CompanyService = companyService;
            PageDialogService = pageDialogService;

            CompanyTapCommand = new DelegateCommand<Company>(OpenCompanyOptions);
            AddNewCompanyCommand = new DelegateCommand(AddNewCompany);
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            GetCompanies();

            await GetCurrentAddress();
        }

        private async Task GetCurrentAddress()
        {
            if (CurrentAddress != null)
                return;

            CurrentAddress = new Placemark
            {
                Thoroughfare = "Endereço não encontrado"
            };

            var hasPermission = await Util.PermissionHelper.RequestLocationPermission();

            if (!hasPermission)
                return;

            var location = await Geolocation.GetLastKnownLocationAsync();

            if (location == null)
                return;

            var address = await Geocoding.GetPlacemarksAsync(location);

            if (address == null)
                return;

            CurrentAddress = address.FirstOrDefault();

        }

        private void GetCompanies()
        {
            var list = CompanyService.GetAll();

            CompanyList = new ObservableCollection<Company>(list.OrderBy(x => x.Distance).ThenBy(x => x.Name));
        }

        private async void OpenCompanyOptions(Company company)
        {
            try
            {
                var action = await PageDialogService.DisplayActionSheetAsync(company.Name, Constants.Cancel, null, Constants.Edit, Constants.Remove);

                switch (action)
                {
                    case Constants.Edit:

                        await NavigationService.NavigateAsync(nameof(CompanyEditPage), new NavigationParameters
                        {
                            {Constants.Company, company }
                        });

                        break;

                    case Constants.Remove:
                        await DeleteCompany(company);
                        break;

                }
            }

            catch(Exception e)
            {

            }
        }

        private async void AddNewCompany()
        {
            try
            {
                await NavigationService.NavigateAsync(nameof(CompanyEditPage));
            }

            catch (Exception e)
            {

            }
        }


        private async Task DeleteCompany(Company company)
        {
            try
            {
                var confirm = await PageDialogService.DisplayAlertAsync(company.Name, "Deseja realmente remover?", Constants.Confirm, Constants.Cancel);

                if (!confirm)
                    return;

                var removed = CompanyService.Delete(company.Id);

                if (!removed)
                {
                    await PageDialogService.DisplayAlertAsync("Falha", "Não foi possível remover, por favor tente novamente mais tarde", "Ok", null);

                    return;
                }

                CompanyList.Remove(company);
            }

            catch (Exception e)
            {

            }
        }


        #region Commands

        public ICommand CompanyTapCommand{ get; set;}
        public ICommand AddNewCompanyCommand { get; set; }

        #endregion

        #region Props

        Placemark _currentAddress;
        public Placemark CurrentAddress
        {
            get => _currentAddress;
            set => SetProperty(ref _currentAddress, value);
        }

        ObservableCollection<Company> _companyList;
        public ObservableCollection<Company> CompanyList
        {
            get => _companyList;
            set => SetProperty(ref _companyList, value);
        }

        #endregion
    }
}
