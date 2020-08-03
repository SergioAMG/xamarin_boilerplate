namespace XamarinBoilerplate.ViewModels.DataObjects
{
    public class PopularBrandsViewModel : BaseViewModel
    {
        private string _itemTitle;
        private string _text;
        private string _image;
        private bool _isFavorite;

        public string ItemTitle
        {
            get
            {
                return Utils.Strings.TruncateAtWord(_itemTitle, Utils.Constants.MaxCharForListItemsTitleDisplay);
            }
            set
            {
                if (_itemTitle != value)
                {
                    _itemTitle = Utils.Strings.TruncateAtWord(value, Utils.Constants.MaxCharForListItemsTitleDisplay);
                    OnPropertyChanged(nameof(ItemTitle));
                }
            }
        }
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged(nameof(Text));
                }
            }
        }

        public string Image
        {
            get
            {
                return _image;
            }
            set
            {
                if (_image != value)
                {
                    _image = value;
                    OnPropertyChanged(nameof(Image));
                }
            }
        }

        public bool IsFavorite
        {
            get
            {
                return _isFavorite;
            }
            set
            {
                if (_isFavorite != value)
                {
                    _isFavorite = value;
                    OnPropertyChanged(nameof(IsFavorite));
                }
            }
        }
    }
}
