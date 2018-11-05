﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Sample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            _itemsControl.ItemsSource = new List<Data>()
             {
                new Data() { Text = "C#" },
                new Data() { Text = "VB" },
                new Data() { Text = "JavaScript" },
                new Data() { Text = "test" },
             };

            segmentedControl.Items = new List<string>
            {
                "Item 1",
                "Item 2",
                "Item 3"
            };
        }

        private void FloatingActionButton_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("FAB Clicked");
        }
    }

    public class Data
    {
        public string Text { get; set; }
    }
}