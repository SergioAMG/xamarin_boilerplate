﻿using DataManagers.Interfaces;
using XamarinBoilerplate.UnitTesting.MockData;

namespace XamarinBoilerplate.UnitTesting.Services
{
    public class MockDataWrapperService : IDataService
    {
        public INews News
        {
            get { return new MockNews(); }
        }
    }
}
