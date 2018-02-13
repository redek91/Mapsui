﻿using Mapsui.Samples.Common;
using Mapsui.UI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mapsui.Sample.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageLarge : ContentPage
    {
        Dictionary<string, Func<Map>> allSamples;
        int markerNum = 1;
        Random rnd = new Random();

        public MainPageLarge()
        {
            InitializeComponent();

            allSamples = AllSamples.CreateList();

            listView.ItemsSource = allSamples.Select(k => k.Key).ToList();

            mapView.AllowPinchRotation = true;
            mapView.UnSnapRotationDegrees = 30;
            mapView.ReSnapRotationDegrees = 5;

            mapView.PinClicked += OnPinClicked;
            mapView.MapClicked += OnMapClicked;
        }

        private void OnMapClicked(object sender, MapClickedEventArgs e)
        {
            //var assembly = typeof(MainPageLarge).GetTypeInfo().Assembly;
            //var image = assembly.GetManifestResourceStream("").ToBytes();
            mapView.Pins.Add(new Pin { Label = $"Marker {markerNum++}", Position = e.Point, Type = PinType.Pin, Color = new Color(rnd.Next(0, 255) / 255.0, rnd.Next(0, 255) / 255.0, rnd.Next(0, 255) / 255.0) });
        }

        void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            var sample = e.SelectedItem.ToString();
            var call = allSamples[sample];

            mapView.Map = call();
        }

        private void OnPinClicked(object sender, PinClickedEventArgs e)
        {
            if (e.Pin != null)
            {
                if (e.NumOfTaps == 2)
                {
                    // Hide Pin when double click
                    //DisplayAlert($"Pin {e.Pin.Label}", $"Is at position {e.Pin.Position}", "Ok");
                    e.Pin.IsVisible = false;
                }
            }

            e.Handled = true;
        }
    }
}