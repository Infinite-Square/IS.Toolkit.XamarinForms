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

            segmentedControl.Items = new List<string>
            {
                "Item 1",
                "Item 2",
                "Item 3",
            };

            fab.Items = new List<IS.Toolkit.XamarinForms.Controls.FloatingMenu.Item>()
            {
                new IS.Toolkit.XamarinForms.Controls.FloatingMenu.Item()
                {
                    BackgroundColor = Color.FromHex("#B3ffffff"),
                    Color = Color.CadetBlue,
                    Label = "Text",
                    Icon = "Icon.png",
                    Action = new Command(() => { System.Diagnostics.Debug.WriteLine("Item Clicked"); })
                },
                new IS.Toolkit.XamarinForms.Controls.FloatingMenu.Item()
                {
                    BackgroundColor = Color.DarkGray,
                    Color = Color.LightPink,
                    Label = "Text 1",
                    Icon = "Icon.png",
                    Action = new Command(() => { System.Diagnostics.Debug.WriteLine("Item 1 Clicked"); })
                },
                new IS.Toolkit.XamarinForms.Controls.FloatingMenu.Item()
                {
                    Color = Color.PaleVioletRed,
                    Label = "Text 2",
                    Icon = "Icon.png",
                    Action = new Command(() => { System.Diagnostics.Debug.WriteLine("Item 2 Clicked"); })
                }
            };

            var list = new List<AvailableValue>()
            {
                new AvailableValue() { Label = "Text 1", Value = "Text 1" },
                new AvailableValue() { Label = "Text 2", Value = "Text 2" },
                new AvailableValue() { Label = "Text 3", Value = "Text 3" },
                new AvailableValue() { Label = "Text 4", Value = "Text 4" }
            };
            _picker.ItemsSource = list;
            _picker.SelectedItem = list[2];
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
