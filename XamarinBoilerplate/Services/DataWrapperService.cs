using DataManagers.Interfaces;

namespace XamarinBoilerplate.Services
{
    public class DataWrapperService : IDataService
    {
        public INews News
        {
            get { return DataManagers.DataManager.Instance.News; }
        }
    }
}
