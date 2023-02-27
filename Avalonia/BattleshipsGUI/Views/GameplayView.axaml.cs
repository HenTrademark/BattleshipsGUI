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
    public GameplayView()
    {
        Directory.CreateDirectory("./Dependencies");
        Directory.CreateDirectory("./Dependencies/Ships");
        File.WriteAllText("./Dependencies/Ships/" + "Carrier" + ".txt","-1");
        File.WriteAllText("./Dependencies/Ships/" + "Destroyer" + ".txt","-1");
        File.WriteAllText("./Dependencies/Ships/" + "Ship" + ".txt","-1");
        File.WriteAllText("./Dependencies/Ships/" + "Patrol" + ".txt","-1");
        Directory.CreateDirectory("./Dependencies/Board");
        for (int a = 0; a < 10; a++) {
            for (int b = 0; b < 10; b++) {
                File.WriteAllText("./Dependencies/Board/P" + a.ToString() + b.ToString() + ".txt","O");
            }
        }
        for (int a = 0; a < 10; a++) {
            for (int b = 0; b < 10; b++) {
                File.WriteAllText("./Dependencies/Board/B" + a.ToString() + b.ToString() + ".txt","O");
            }
        }
        InitializeComponent();
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }
    
    private void StartTheGame(object? o, RoutedEventArgs e) {
        ((Button)o!).IsEnabled = false;
    }
    
    private void ButtonOnClick(object? o, RoutedEventArgs e) {
        string name = File.ReadAllText("./Dependencies/Board/B" + ((Button)o).Name[1] + ((Button)o).Name[2]);
        name = name == "O" ? "X" : name;
        ((Button)o).Content = name[0];
        ((Button)o).IsEnabled = false;
    }

    private void ButtonChecked(object? o, RoutedEventArgs e) {
        
    }

    private void BeingInitialised(object? o, EventArgs e) {
    }
}