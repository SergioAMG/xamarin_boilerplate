﻿using DataManagers.Entities;
using DataManagers.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamarinBoilerplate.UnitTesting.MockData
{
    public class MockBrands : IBrands
    {
        public async Task<List<Brands>> GetBrands()
        {
            List<Brands> ListItems = new List<Brands>();
            ListItems.Add(new Brands { ItemTitle = "Brand 1", Text = "Sample Text One", Image = "sampleOne", IsFavorite = false });
            ListItems.Add(new Brands { ItemTitle = "Brand 2", Text = "Sample Text Two", Image = "sampleTwo", IsFavorite = false });
            ListItems.Add(new Brands { ItemTitle = "Brand 3", Text = "Sample Text Three", Image = "sampleThree", IsFavorite = false });
            ListItems.Add(new Brands { ItemTitle = "Brand 4", Text = "Sample Text Four", Image = "sampleFour", IsFavorite = false });
            ListItems.Add(new Brands { ItemTitle = "Brand 5", Text = "Sample Text Five", Image = "sampleFive", IsFavorite = false });
            ListItems.Add(new Brands { ItemTitle = "Brand 6", Text = "Sample Text Six", Image = "sampleSix", IsFavorite = false });
            ListItems.Add(new Brands { ItemTitle = "Brand 7", Text = "Sample Text Seven", Image = "sampleOne", IsFavorite = false });
            ListItems.Add(new Brands { ItemTitle = "Brand 8", Text = "Sample Text Eight", Image = "sampleTwo", IsFavorite = false });
            ListItems.Add(new Brands { ItemTitle = "Brand 9", Text = "Sample Text Nine", Image = "sampleThree", IsFavorite = false });
            ListItems.Add(new Brands { ItemTitle = "Brand 10", Text = "Sample Text Ten", Image = "sampleFour", IsFavorite = false });
            ListItems.Add(new Brands { ItemTitle = "Brand 11", Text = "Sample Text Eleven", Image = "sampleFive", IsFavorite = false });
            ListItems.Add(new Brands { ItemTitle = "Brand 12", Text = "Sample Text Twelve", Image = "sampleSix", IsFavorite = false });

            return await Task.FromResult<List<Brands>>(ListItems);
        }
    }
}
