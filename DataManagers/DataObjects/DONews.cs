﻿using DataManagers.Entities;
using DataManagers.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataManagers.DataObjects
{
    public class DONews : INews
    {
        public async Task<List<News>> GetNews()
        {       
            List<News> ListItems = new List<News>();
            var connection = DataManager.Instance.SqliteDatabase;
            var news = await connection.Table<News>().ToListAsync();
            
            foreach (News item in news)
            {
                ListItems.Add(item);
            }

            return await Task.FromResult<List<News>>(ListItems);
        }
        
        /*
        public async Task<List<News>> GetNews()
        {
            List<News> ListItems = new List<News>();
            ListItems.Add(new News() { ItemTitle = "Lorem ipsum dolor sit amet", Image = "sampleOne", Date = DateTime.Now, Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque tempus lobortis lacinia. Nunc condimentum nisi convallis, finibus metus non, facilisis leo. Phasellus interdum purus a mauris posuere, eget varius justo lobortis. Phasellus felis felis, finibus eu viverra quis, dignissim in ligula. Ut dapibus ligula a nunc lacinia ullamcorper. Phasellus venenatis sagittis bibendum. Aliquam augue nisl, vulputate non porta nec, efficitur sed diam. In auctor, ipsum eget facilisis malesuada, turpis nisl ultricies nibh, in fermentum quam lectus et nibh. Proin eu ultricies lacus. Mauris vel odio non ex tincidunt tempor nec id risus." });
            ListItems.Add(new News() { ItemTitle = "Nam placerat commodo vestibulum", Image = "sampleTwo", Date = DateTime.Now, Text = "Aenean lorem nisl, aliquet in scelerisque quis, mattis quis velit. Mauris rhoncus vitae enim eu suscipit. Duis at luctus lorem. Proin aliquam sagittis lorem a pulvinar. Nunc vel ullamcorper orci. Phasellus eget leo nec metus egestas consequat blandit eu neque. Phasellus aliquet elit sit amet lectus consequat, sit amet posuere purus vulputate. Sed a est arcu." });
            ListItems.Add(new News() { ItemTitle = "Curabitur condimentum vestibulum", Image = "sampleThree", Date = DateTime.Now, Text = "Aliquam tempor ante nec sem accumsan fermentum. Suspendisse interdum, odio et facilisis hendrerit, ante neque tincidunt justo, id posuere urna mi at massa. Curabitur bibendum ligula in bibendum laoreet. Vestibulum eget sem eget erat feugiat hendrerit. Suspendisse blandit metus sed nisi vestibulum maximus. Cras feugiat hendrerit nulla suscipit pretium. Nullam nec lacus malesuada, ultricies tortor quis, sagittis eros. Aliquam vehicula justo vitae mollis ultricies. Morbi finibus lorem ultrices, luctus sapien ut, lobortis metus. Aenean varius erat eget quam placerat elementum. Nulla vitae nunc sodales, auctor arcu quis, tristique felis." });
            ListItems.Add(new News() { ItemTitle = "Curabitur scelerisqu", Image = "sampleFour", Date = DateTime.Now, Text = "In posuere sed tortor at viverra. Donec sed sodales tortor. Nam nisl elit, consequat a euismod hendrerit, sollicitudin ut diam. Sed nibh elit, mollis non facilisis eu, aliquet sed ligula. Interdum et malesuada fames ac ante ipsum primis in faucibus. Maecenas sagittis luctus libero vel ullamcorper. Nullam lectus sapien, lobortis sed dui et, facilisis facilisis lacus." });
            ListItems.Add(new News() { ItemTitle = "Quisque gravida porta laoreet", Image = "sampleFive", Date = DateTime.Now, Text = "Aliquam arcu massa, commodo sit amet ligula sed, fringilla scelerisque eros. Nulla scelerisque vestibulum ultricies. Nullam eget velit iaculis, porta est et, laoreet lorem. Mauris aliquam ligula nisl, ac porta lacus imperdiet sit amet. Aenean faucibus felis nec tortor accumsan accumsan. Phasellus mollis mi est, in pulvinar sem volutpat placerat. Fusce nisi dui, porttitor et convallis id, placerat at justo. Mauris in mi eros. Mauris volutpat mattis fringilla. Donec at tempor lacus, sed sodales lorem. Mauris efficitur lobortis nunc, in sodales erat lobortis fringilla. Aliquam tortor lacus, tincidunt vel sollicitudin et, vehicula sit amet ipsum. Mauris eu rutrum nisi, ullamcorper suscipit odio. Nulla consectetur tempor eros, sit amet molestie lorem ultricies et." });
            ListItems.Add(new News() { ItemTitle = "Mauris eu eros pellentesque", Image = "sampleSix", Date = DateTime.Now, Text = "Nullam vitae tortor magna. Pellentesque pellentesque sapien vitae risus finibus bibendum at at ligula. Ut et ultrices lorem. Praesent rutrum dolor a malesuada facilisis. Morbi maximus metus eu diam lobortis cursus. Nulla imperdiet, lacus id dictum rhoncus, est lacus lacinia mi, vel tempus lectus ante faucibus lectus. Donec scelerisque sit amet nunc nec viverra." });
            ListItems.Add(new News() { ItemTitle = "Etiam id mattis lorem", Image = "sampleOne", Date = DateTime.Now, Text = "Suspendisse lectus libero, varius quis metus at, pharetra varius leo. Maecenas iaculis urna id lacinia aliquam. Pellentesque et ante malesuada, fringilla ex vel, aliquet orci. In hendrerit efficitur maximus. Phasellus in justo pulvinar, ultricies elit ut, bibendum nisi. Sed tincidunt tincidunt urna, eu tincidunt ex ultrices vel. Suspendisse varius nunc ut ligula vestibulum mattis. Donec scelerisque rhoncus lacus et volutpat. Donec non consectetur risus." });
            return await Task.FromResult<List<News>>(ListItems);
        }
        */
    }
}
