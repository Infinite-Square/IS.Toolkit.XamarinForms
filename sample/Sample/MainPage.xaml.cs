using IS.Toolkit.XamarinForms.Controls;
using System;
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

            var list = new List<AvailableValue>()
            {
                new AvailableValue() { Label = "Text 1", Value = "Text 1" },
                new AvailableValue() { Label = "Text 2", Value = "Text 2" },
                new AvailableValue() { Label = "Text 3", Value = "Text 3" },
                new AvailableValue() { Label = "Text 4", Value = "Text 4" }
            };

            // _picker.ItemsSource = list;
            // _picker.SelectedItem = list[2];
            // _picker.SelectedItem = DateTime.Now.AddDays(-15);
        }

        private void FloatingActionButton_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("FAB Clicked");
        }

        private void Picker_SelectedItemChanged(object sender, object e)
        {
            var newValue = e as string;
        }
    }

    public class Data
    {
        public string Text { get; set; }
    }
}
