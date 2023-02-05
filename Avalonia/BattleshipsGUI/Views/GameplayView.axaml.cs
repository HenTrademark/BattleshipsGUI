using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using BattleshipsGUI.ViewModels;
using ReactiveUI;

namespace BattleshipsGUI.Views; 

public partial class GameplayView : UserControl {
    private MainWindowViewModel _mwvm = new MainWindowViewModel();
    private bool _gameStarted = false;
    public GameplayView() {
        InitializeComponent();
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }
    
    private void ButtonOnClick(object? o, RoutedEventArgs e) {
        ((Button)o).Content = _mwvm.GetName(((Button)o).Name);
        ((Button)o).IsEnabled = false;
    }
    private void StartTheGame(object? o, RoutedEventArgs e) {
        bool b = _mwvm.MakeTheBoard();
        bool p = _mwvm.MakePlayerBoard();
        
        ((Button)o!).IsEnabled = false;
    }

    private void ButtonChecked(object? o, RoutedEventArgs e) {
        ((Button)o).Content = _mwvm.GetPlayerName(((Button)o).Name);
    }
}