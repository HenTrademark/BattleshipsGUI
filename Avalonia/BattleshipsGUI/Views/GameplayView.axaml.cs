using System;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using BattleshipsGUI.ViewModels;
using ReactiveUI;

namespace BattleshipsGUI.Views; 

public partial class GameplayView : UserControl {
    private MainWindowViewModel _mwvm = new MainWindowViewModel();
    public GameplayView() {
        InitializeComponent();
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }
    
    private void ButtonOnClick(object? o, RoutedEventArgs e) {
        ((Button)o).Content = _mwvm.GetName(((Button)o).Name);
        _mwvm.SinkShip();
        ((Button)o).IsEnabled = false;
    }
    private void StartTheGame(object? o, RoutedEventArgs e) {
        _mwvm.BoardValues();
        _mwvm.MakeTheBoard();
        _mwvm.MakePlayerBoard();
        ((Button)o!).IsEnabled = false;
    }

    private void ButtonChecked(object? o, RoutedEventArgs e) {
        ((Button)o).Content = _mwvm.GetPlayerName(((Button)o).Name);
    }

    private void BeingInitialised(object? o, EventArgs e) {
        string[] ships = { "Carrier", "Destroyer", "Ship", "Patrol" };
        Directory.CreateDirectory("./Ships");
        foreach (string ship in ships) {
            File.WriteAllText("./Ships/" + ship + ".txt","-1");
        }
    }
}