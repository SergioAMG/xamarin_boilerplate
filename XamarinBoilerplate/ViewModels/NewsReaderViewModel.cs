using DataManagers.Interfaces;
using System.Threading.Tasks;
using System.Windows.Input;
using XamarinBoilerplate.Utils;
using XamarinBoilerplate.ViewModels.DataObjects;

namespace XamarinBoilerplate.ViewModels
{
    public class NewsReaderViewModel : BaseViewModel
    {
        private double _textsize;
        private double _titleTextSize;
        private NewsViewModel _newsViewModel;
        private ICommand _increaseTextSizeCommand;
        private ICommand _decreaseTextSizeCommand;

        public NewsReaderViewModel(IDataService dataManager = null) : base(dataManager)
        {
            if (dataManager != null)
            {
                DataManager = dataManager;
            };

            TextSize = Constants.InitialTextSizeForNewsItem;
            TitleTextSize = Constants.InitialTitleSizeForNewsItem;
        }

        public double TextSize
        {
            get
            {
                return _textsize;
            }
            set
            {
                if (_textsize != value)
                {
                    _textsize = value;
                    OnPropertyChanged(nameof(TextSize));
                }
            }
        }

        public double TitleTextSize
        {
            get
            {
                return _titleTextSize;
            }
            set
            {
                if (_titleTextSize != value)
                {
                    _titleTextSize = value;
                    OnPropertyChanged(nameof(TitleTextSize));
                }
            }
        }

        public NewsViewModel NewsViewModel
        {
            get
            {
                return _newsViewModel;
            }
            set
            {
                if (_newsViewModel != value)
                {
                    _newsViewModel = value;
                    OnPropertyChanged(nameof(NewsViewModel));
                }
            }
        }

        public ICommand IncreaseTextSizeCommand
        {
            get
            {
                return _increaseTextSizeCommand ?? (_increaseTextSizeCommand = new CommandExtended(ExecuteIncreaseTextSizeCommandAsync));

            }
        }

        public ICommand DecreaseTextSizeCommand
        {
            get
            {
                return _decreaseTextSizeCommand ?? (_decreaseTextSizeCommand = new CommandExtended(ExecuteDecreaseTextSizeCommandAsync));

            }
        }

        public void Init(NewsViewModel newsViewModel)
        {
            NewsViewModel = newsViewModel;
        }

        public async Task ExecuteIncreaseTextSizeCommandAsync()
        {
            TitleTextSize *= Constants.TextIncreaseFactor;
            TextSize *= Constants.TextIncreaseFactor;
        }

        public async Task ExecuteDecreaseTextSizeCommandAsync()
        {
            TitleTextSize *= Constants.TextDecreaseFactor;
            TextSize *= Constants.TextDecreaseFactor;
        }
    }
}
