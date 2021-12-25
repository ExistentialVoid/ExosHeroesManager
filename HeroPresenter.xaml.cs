global using System;
global using System.Collections.Generic;
global using System.ComponentModel;
global using System.Data;
global using System.Windows;
global using System.Windows.Controls;
global using System.Windows.Media;

namespace ExosHeroesManager;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class HeroPresenter : Window
{
    public HeroPresenter()
    {
        InitializeComponent();
    }


    private void HeroImage_MouseDown(object sender, MouseButtonEventArgs e)
    {
        HeroImageDisplay displayWindow = new(((HeroViewModel)DataContext).SelectedHero);
        displayWindow.Show();
    }
    private void HeroStatTextBox_GotFocus(object sender, RoutedEventArgs e)
    {
        ((TextBox)sender).SelectAll();
    }
    private void ExclusiveWeaponButton_Click(object sender, RoutedEventArgs e)
    {
        if (MessageBox.Show("Assign Exclusive Weapon?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        {
            ((HeroViewModel)DataContext).SelectedHero.HasExclusive = true;
        }
    }
}
