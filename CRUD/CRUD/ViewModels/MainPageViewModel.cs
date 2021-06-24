using CRUD.Models;
using CRUD.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
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



        public MainPageViewModel(INavigationService navigationService, ICompanyService companyService)
            : base(navigationService)
        {
            Util.Mock.DatabaseMock.InitializeMockDataBase();

            Title = "DeliveryPizzas";

            CompanyService = companyService;

            CompanyTapCommand = new DelegateCommand<Company>(OpenCompanyOptions);
            AddNewCompanyCommand = new DelegateCommand(AddNewCompany);
            DeleteCompanyCommand = new DelegateCommand<Company>(DeleteCompany);
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            await GetCurrentAddress();

            GetCompanies();
        }

        private async Task GetCurrentAddress()
        {
            CurrentAddress = new Placemark
            {
                Thoroughfare = "Não encontrado"
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

            }

            catch(Exception e)
            {

            }
        }

        private async void AddNewCompany()
        {
            try

            {

            }

            catch (Exception e)
            {

            }
        }


        private async void DeleteCompany(Company company)
        {
            try

            {

            }

            catch (Exception e)
            {

            }
        }


        #region Commands

        public ICommand CompanyTapCommand{ get; set;}
        public ICommand AddNewCompanyCommand { get; set; }
        public ICommand DeleteCompanyCommand { get; set; }

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
