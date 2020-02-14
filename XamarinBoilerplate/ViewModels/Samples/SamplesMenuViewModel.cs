using DataManagers.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.ViewModels.DataObjects;
using XamarinBoilerplate.Views;

namespace XamarinBoilerplate.ViewModels.Samples
{
    public class SamplesMenuViewModel : BaseViewModel
    {
        private int _selectedTabIndex;
        private ICommand _backFromDetailsCommand;
        private SampleMenuItemViewModel _sampleMenuItemSelected;
        private ObservableCollection<SampleMenuItemViewModel> _sampleMenu;

        public SamplesMenuViewModel(IDataService dataManager = null) : base(dataManager)
        {
            if (dataManager != null)
            {
                DataManager = dataManager;
            }
            GenerateSamplesMenu();
        }

        public SampleMenuItemViewModel SampleMenuItemSelected
        {
            get
            {
                return _sampleMenuItemSelected;
            }
            set
            {
                if (_sampleMenuItemSelected != value)
                {
                    _sampleMenuItemSelected = value;
                    OnPropertyChanged(nameof(SampleMenuItemSelected));
                    NavigateToSample(_sampleMenuItemSelected);
                }
            }
        }

        public ObservableCollection<SampleMenuItemViewModel> SampleMenu
        {
            get
            {
                return _sampleMenu;
            }
            set
            {
                if (_sampleMenu != value)
                {
                    _sampleMenu = value;
                    OnPropertyChanged(nameof(SampleMenu));
                }
            }
        }

        public int SelectedTabIndex
        {
            get
            {
                return _selectedTabIndex;
            }
            set
            {
                if (_selectedTabIndex != value)
                {
                    _selectedTabIndex = value;
                    OnPropertyChanged(nameof(SelectedTabIndex));
                }
            }
        }

        public TextAlignment CustomTextAlignment
        {
            get
            {
                return (DeviceManager.IsIOS) ? TextAlignment.Center : TextAlignment.Start; 
            }
        }

        public LayoutOptions CustomTextLayout
        {
            get
            {
                return (DeviceManager.IsIOS) ? LayoutOptions.Center : LayoutOptions.Start;
            }
        }

        public GridLength FirstColumnWidth
        {
            get
            {
                return (DeviceManager.IsLandscape) ? new GridLength(9, GridUnitType.Star) : new GridLength(16, GridUnitType.Star);
            }
        }

        public GridLength SecondColumnWidth
        {
            get
            {
                return (DeviceManager.IsLandscape) ? new GridLength(80, GridUnitType.Star) : new GridLength(70, GridUnitType.Star);
            }
        }

        public GridLength ThirdColumnWidth
        {
            get
            {
                return (DeviceManager.IsLandscape) ? new GridLength(10, GridUnitType.Star) : new GridLength(20, GridUnitType.Star);
            }
        }
        
        public ICommand BackFromDetailsCommand
        {
            get
            {
                return _backFromDetailsCommand ?? (_backFromDetailsCommand = new CommandExtended(ExecuteBackFromDetailsCommandAsync));

            }
        }

        public void Init(int selectedTabIndex)
        {
            SelectedTabIndex = selectedTabIndex;
        }

        public async Task ExecuteBackFromDetailsCommandAsync()
        {
            NavigationService.NavigateDetails(nameof(CustomTabbedPage), SelectedTabIndex);
        }

        public void GenerateSamplesMenu()
        {
            SampleMenu = new ObservableCollection<SampleMenuItemViewModel>();
            SampleMenu.Add(new SampleMenuItemViewModel() { SampleMenuImage = "baseline_view_carousel_black_24", SampleMenuItem = Constants.CarouselViewMenu });
        }

        public async Task NavigateToSample(SampleMenuItemViewModel samplePage)
        {
            await NavigationService.NavigateAsync(samplePage.SampleMenuItem, null, true);
        }

        public void SetOrientationValues()
        {
            OnPropertyChanged(nameof(FirstColumnWidth));
            OnPropertyChanged(nameof(SecondColumnWidth));
            OnPropertyChanged(nameof(ThirdColumnWidth));
        }
    }
}
