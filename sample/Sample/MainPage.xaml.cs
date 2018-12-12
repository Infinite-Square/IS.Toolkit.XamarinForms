using IS.Toolkit.XamarinForms.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sample
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<Data> _data;
        public MainPage()
        {
            ItemSelectedCommand = new Command(FabMenuClicked);
            InitializeComponent();
            _data = new ObservableCollection<Data>()
             {
                new Data() { Text = "C#" },
                new Data() { Text = "VB" },
                new Data() { Text = "JavaScript" },
                new Data() { Text = "C++" },
             };

            _itemsControl.ItemsSource = _data;
            _itemsWrappedControl.ItemsSource = _data;

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
            foreach (var item in fab.Items)
            {
                item.Command = ItemSelectedCommand;
            }

            _btnPicker.SelectedItem = "Salut";
            _btnPicker.Command = new Command(() =>
            {
                DisplayAlert("Title", "Do whatever you want", "Ok");
            });
        }

        public ICommand ItemSelectedCommand
        {
            get;
        }

        private void FabMenuClicked()
        {
            Console.WriteLine("FAB Menu Clicked");
            _data.Add(new Data() { Text = "Added" });
        }

        private void Picker_SelectedItemChanged(object sender, object e)
        {
            var newValue = e as string;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            SnackB.Open("Hello Man");
        }
    }

    public class Data
    {
        public string Text { get; set; }
    }
}
