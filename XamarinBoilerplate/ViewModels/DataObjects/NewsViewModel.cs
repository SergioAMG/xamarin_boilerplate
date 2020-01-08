namespace XamarinBoilerplate.ViewModels.DataObjects
{
    public class NewsViewModel : BaseViewModel
    {
        private string _itemTitle;
        private string _text;
        private string _textClipped;
        private string _image;

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

        public string TextClipped
        {
            get
            {
                return Utils.Strings.TruncateAtWord(Text, Utils.Constants.MaxCharForListItemsDisplay);
            }
            set
            {
                if (_textClipped != value)
                {
                    _textClipped = value;
                    OnPropertyChanged(nameof(TextClipped));
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
    }
}
