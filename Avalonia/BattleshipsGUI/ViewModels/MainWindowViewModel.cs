using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using System.Text.RegularExpressions;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using BattleshipsGUI.Views;
using ReactiveUI;

namespace BattleshipsGUI.ViewModels;

public class MainWindowViewModel : ViewModelBase {
    
    private bool _right;
    public bool Right {
        get => _right;
        set => this.RaiseAndSetIfChanged(ref _right, value);
    }

    private int _carriers;
    public int Carriers {
        get => _carriers;
        set {
            this.RaiseAndSetIfChanged(ref _carriers, value);
            File.WriteAllText("./Dependencies/Ships/Carrier.txt",_carriers.ToString());
        }
    }

    private int _destroyers;
    public int Destroyers {
        get => _destroyers;
        set { 
            this.RaiseAndSetIfChanged(ref _destroyers, value);
            File.WriteAllText("./Dependencies/Ships/Destroyer.txt",_destroyers.ToString());
        }
    }

    private int _ships;
    public int Ships {
        get => _ships;
        set {
            this.RaiseAndSetIfChanged(ref _ships, value);
            File.WriteAllText("./Dependencies/Ships/Ship.txt",_ships.ToString());
        }
    }

    private int _patrols;
    public int Patrols {
        get => _patrols;
        set {
            this.RaiseAndSetIfChanged(ref _patrols, value);
            File.WriteAllText("./Dependencies/Ships/Patrol.txt",_patrols.ToString());
        }
    }

    private int[] _count = new int[4];

    public void BoardValues() {
        int carrier = int.Parse(File.ReadAllText("./Dependencies/Ships/Carrier.txt"));
        int destroyer = int.Parse(File.ReadAllText("./Dependencies/Ships/Destroyer.txt"));
        int ship = int.Parse(File.ReadAllText("./Dependencies/Ships/Ship.txt"));
        int patrol = int.Parse(File.ReadAllText("./Dependencies/Ships/Patrol.txt"));
        int[] count = { carrier, destroyer, ship, patrol };
        for (int i = 0; i < 4; i++) {
            if (count[i] == -1) {
                Random r = new Random();
                switch (i) {
                    case 0: count[i] = r.Next(3); break;
                    case 1: count[i] = r.Next(4); break;
                    case 2: count[i] = r.Next(6); break;
                    case 3: count[i] = r.Next(-1,9); break;
                }
            }
        }

        count[3] = count[3] == 9 ? -1 : count[3];
        _count = new int[] { count[0]+1, count[1]+1, count[2]+1, count[3]+1  };
    }

    private int _boardCount;
    public void MakeTheBoard() {
        _boardCount = Battleships.MakeBoard(BotBoard, _count);
        BB = BotBoard;
    }
    
    private int _botCount;
    public void MakePlayerBoard() {
        _botCount = Battleships.MakeBoard(PlayerBoard, _count);
        PB = PlayerBoard;
    }
    
    private void StartTheGame() {
        BoardValues();
        MakeTheBoard();
        _shipsleft = (_count[0] * 5) + (_count[1] * 4) + (_count[2] * 3) + (_count[3] * 2);
        HowManyShips = "Ships left: " + _shipsleft.ToString();
    }

    private void UpdateCounter() {
        _shipsleft -= 1;
        HowManyShips = "Ships left: " + _shipsleft.ToString();
    }

    public void SinkShip() {
        int row; //= new Random().Next(10);
        int col; //= new Random().Next(10);
        row = 0;
        col = 0;
        

        PB[row][col] = "Haha";
    }
    
    public string[][] PB {
        get => PlayerBoard;
        set {
            this.RaiseAndSetIfChanged(ref PlayerBoard, value);
            for (int row = 0; row < 10; row++) {
                for (int col = 0; col < 10; col++) {
                    File.WriteAllText("./Dependencies/Board/P" + row.ToString() + col.ToString(),PlayerBoard[row][col]);
                }
            }
        }
    }
    
    public string[][] BB {
        get => BotBoard;
        set {
            this.RaiseAndSetIfChanged(ref PlayerBoard, value);
            for (int row = 0; row < 10; row++) {
                for (int col = 0; col < 10; col++) {
                    File.WriteAllText("./Dependencies/Board/B" + row.ToString() + col.ToString(),BotBoard[row][col]);
                }
            }
        }
    }

    public void Clicked(string name) {
        (int, int) coords = (int.Parse(name[1].ToString()), int.Parse(name[2].ToString()));
        string plot = BB[coords.Item1][coords.Item2];
        if (plot != "O") {
            UpdateCounter();
        }
    }

    private int _shipsleft = 0;
    private string _howmanyships;
    public string HowManyShips {
        get => _howmanyships;
        set => this.RaiseAndSetIfChanged(ref _howmanyships, value);
    }

    public string[][] PlayerBoard = { 
        new[] { "O", "O", "O", "O", "O", "O", "O", "O", "O", "O" }, 
        new[] { "O", "O", "O", "O", "O", "O", "O", "O", "O", "O" },
        new[] { "O", "O", "O", "O", "O", "O", "O", "O", "O", "O" },
        new[] { "O", "O", "O", "O", "O", "O", "O", "O", "O", "O" },
        new[] { "O", "O", "O", "O", "O", "O", "O", "O", "O", "O" },
        new[] { "O", "O", "O", "O", "O", "O", "O", "O", "O", "O" },
        new[] { "O", "O", "O", "O", "O", "O", "O", "O", "O", "O" },
        new[] { "O", "O", "O", "O", "O", "O", "O", "O", "O", "O" },
        new[] { "O", "O", "O", "O", "O", "O", "O", "O", "O", "O" },
        new[] { "O", "O", "O", "O", "O", "O", "O", "O", "O", "O" } 
    };
    
    public string[][] BotBoard = { 
        new[] { "O", "O", "O", "O", "O", "O", "O", "O", "O", "O" }, 
        new[] { "O", "O", "O", "O", "O", "O", "O", "O", "O", "O" },
        new[] { "O", "O", "O", "O", "O", "O", "O", "O", "O", "O" },
        new[] { "O", "O", "O", "O", "O", "O", "O", "O", "O", "O" },
        new[] { "O", "O", "O", "O", "O", "O", "O", "O", "O", "O" },
        new[] { "O", "O", "O", "O", "O", "O", "O", "O", "O", "O" },
        new[] { "O", "O", "O", "O", "O", "O", "O", "O", "O", "O" },
        new[] { "O", "O", "O", "O", "O", "O", "O", "O", "O", "O" },
        new[] { "O", "O", "O", "O", "O", "O", "O", "O", "O", "O" },
        new[] { "O", "O", "O", "O", "O", "O", "O", "O", "O", "O" } 
    };
}

class Battleships {
    private static Random _rand = new();
    public static int MakeBoard(string[][] board, int[] count) {
            bool validplace;
            int plots = 0;

            for (int x = 0; x < count[0]; x++) {
                validplace = false;
                while (!validplace) {
                    int row = _rand.Next(10);
                    int col = _rand.Next(10);
                    int direction = _rand.Next(4);

                    switch (direction) {
                        case 0: // Up
                            if (row < 4 || board[row][col] != "O" || board[row - 1][col] != "O" ||
                                board[row - 2][col] != "O" || board[row - 3][col] != "O" || board[row - 4][col] != "O") {
                                break;
                            }

                            validplace = true;
                            board[row][col] = "Carrier" + x.ToString();
                            board[row - 1][col] = "Carrier" + x.ToString();
                            board[row - 2][col] = "Carrier" + x.ToString();
                            board[row - 3][col] = "Carrier" + x.ToString();
                            board[row - 4][col] = "Carrier" + x.ToString();
                            break;
                        case 1: // Right
                            if (col > 5 || board[row][col] != "O" || board[row][col + 1] != "O" ||
                                board[row][col + 2] != "O" || board[row][col + 3] != "O" || board[row][col + 4] != "O") {
                                break;
                            }

                            validplace = true;
                            board[row][col] = "Carrier" + x.ToString();
                            board[row][col + 1] = "Carrier" + x.ToString();
                            board[row][col + 2] = "Carrier" + x.ToString();
                            board[row][col + 3] = "Carrier" + x.ToString();
                            board[row][col + 4] = "Carrier" + x.ToString();
                            break;
                        case 2: // Down
                            if (row > 5 || board[row][col] != "O" || board[row + 1][col] != "O" ||
                                board[row + 2][col] != "O" || board[row + 3][col] != "O" || board[row + 4][col] != "O") {
                                break;
                            }

                            validplace = true;
                            board[row][col] = "Carrier" + x.ToString();
                            board[row + 1][col] = "Carrier" + x.ToString();
                            board[row + 2][col] = "Carrier" + x.ToString();
                            board[row + 3][col] = "Carrier" + x.ToString();
                            board[row + 4][col] = "Carrier" + x.ToString();
                            break;
                        case 3: // Left
                            if (col < 4 || board[row][col] != "O" || board[row][col - 1] != "O" ||
                                board[row][col - 2] != "O" || board[row][col - 3] != "O" || board[row][col - 4] != "O") {
                                break;
                            }

                            validplace = true;
                            board[row][col] = "Carrier" + x.ToString();
                            board[row][col - 1] = "Carrier" + x.ToString();
                            board[row][col - 2] = "Carrier" + x.ToString();
                            board[row][col - 3] = "Carrier" + x.ToString();
                            board[row][col - 4] = "Carrier" + x.ToString();
                            break;
                    }
                }
                plots += 5;
            }

            for (int x = 0; x < count[1]; x++) {
                validplace = false;
                while (!validplace) {
                    int row = _rand.Next(10);
                    int col = _rand.Next(10);
                    int direction = _rand.Next(4);

                    switch (direction) {
                        case 0: // Up
                            if (row < 3 || board[row][col] != "O" || board[row - 1][col] != "O" ||
                                board[row - 2][col] != "O" || board[row - 3][col] != "O") {
                                break;
                            }

                            validplace = true;
                            board[row][col] = "Destroyer" + x.ToString();
                            board[row - 1][col] = "Destroyer" + x.ToString();
                            board[row - 2][col] = "Destroyer" + x.ToString();
                            board[row - 3][col] = "Destroyer" + x.ToString();
                            break;
                        case 1: // Right
                            if (col > 6 || board[row][col] != "O" || board[row][col + 1] != "O" ||
                                board[row][col + 2] != "O" || board[row][col + 3] != "O") {
                                break;
                            }

                            validplace = true;
                            board[row][col] = "Destroyer" + x.ToString();
                            board[row][col + 1] = "Destroyer" + x.ToString();
                            board[row][col + 2] = "Destroyer" + x.ToString();
                            board[row][col + 3] = "Destroyer" + x.ToString();
                            break;
                        case 2: // Down
                            if (row > 6 || board[row][col] != "O" || board[row + 1][col] != "O" ||
                                board[row + 2][col] != "O" || board[row + 3][col] != "O") {
                                break;
                            }

                            validplace = true;
                            board[row][col] = "Destroyer" + x.ToString();
                            board[row + 1][col] = "Destroyer" + x.ToString();
                            board[row + 2][col] = "Destroyer" + x.ToString();
                            board[row + 3][col] = "Destroyer" + x.ToString();
                            break;
                        case 3: // Left
                            if (col < 3 || board[row][col] != "O" || board[row][col - 1] != "O" ||
                                board[row][col - 2] != "O" || board[row][col - 3] != "O") {
                                break;
                            }

                            validplace = true;
                            board[row][col] = "Destroyer" + x.ToString();
                            board[row][col - 1] = "Destroyer" + x.ToString();
                            board[row][col - 2] = "Destroyer" + x.ToString();
                            board[row][col - 3] = "Destroyer" + x.ToString();
                            break;
                    }
                }
                plots += 4;
            }

            for (int x = 0; x < count[2]; x++) {
                validplace = false;
                while (!validplace) {
                    int row = _rand.Next(10);
                    int col = _rand.Next(10);
                    int direction = _rand.Next(4);

                    switch (direction) {
                        case 0: // Up
                            if (row < 2 || board[row][col] != "O" || board[row - 1][col] != "O" ||
                                board[row - 2][col] != "O") {
                                break;
                            }

                            validplace = true;
                            board[row][col] = "Ship" + x.ToString();
                            board[row - 1][col] = "Ship" + x.ToString();
                            board[row - 2][col] = "Ship" + x.ToString();
                            break;
                        case 1: // Right
                            if (col > 7 || board[row][col] != "O" || board[row][col + 1] != "O" ||
                                board[row][col + 2] != "O") {
                                break;
                            }

                            validplace = true;
                            board[row][col] = "Ship" + x.ToString();
                            board[row][col + 1] = "Ship" + x.ToString();
                            board[row][col + 2] = "Ship" + x.ToString();
                            break;
                        case 2: // Down
                            if (row > 7 || board[row][col] != "O" || board[row + 1][col] != "O" ||
                                board[row + 2][col] != "O") {
                                break;
                            }

                            validplace = true;
                            board[row][col] = "Ship" + x.ToString();
                            board[row + 1][col] = "Ship" + x.ToString();
                            board[row + 2][col] = "Ship" + x.ToString();
                            break;
                        case 3: // Left
                            if (col < 2 || board[row][col] != "O" || board[row][col - 1] != "O" ||
                                board[row][col - 2] != "O") {
                                break;
                            }

                            validplace = true;
                            board[row][col] = "Ship" + x.ToString();
                            board[row][col - 1] = "Ship" + x.ToString();
                            board[row][col - 2] = "Ship" + x.ToString();
                            break;
                    }
                }
                plots += 3;
            }

            for (int x = 0; x < count[3]; x++) {
                validplace = false;
                while (!validplace) {
                    int row = _rand.Next(10);
                    int col = _rand.Next(10);
                    int direction = _rand.Next(4);

                    switch (direction) {
                        case 0: // Up
                            if (row < 1 || board[row][col] != "O" || board[row - 1][col] != "O") {
                                break;
                            }

                            validplace = true;
                            board[row][col] = "Patrol" + x.ToString();
                            board[row - 1][col] = "Patrol" + x.ToString();
                            break;
                        case 1: // Right
                            if (col > 8 || board[row][col] != "O" || board[row][col + 1] != "O") {
                                break;
                            }

                            validplace = true;
                            board[row][col] = "Patrol" + x.ToString();
                            board[row][col + 1] = "Patrol" + x.ToString();
                            break;
                        case 2: // Down
                            if (row > 8 || board[row][col] != "O" || board[row + 1][col] != "O") {
                                break;
                            }

                            validplace = true;
                            board[row][col] = "Patrol" + x.ToString();
                            board[row + 1][col] = "Patrol" + x.ToString();
                            break;
                        case 3: // Left
                            if (col < 1 || board[row][col] != "O" || board[row][col - 1] != "O") {
                                break;
                            }

                            validplace = true;
                            board[row][col] = "Patrol" + x.ToString();
                            board[row][col - 1] = "Patrol" + x.ToString();
                            break;
                    }
                }
                plots += 2;
            }
            
            return plots;
    }
}