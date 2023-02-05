using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using System.Text.RegularExpressions;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ReactiveUI;

namespace BattleshipsGUI.ViewModels;

public class MainWindowViewModel : ViewModelBase {

    private string? _text;
    public string? Text {
        get => _text;
        set => this.RaiseAndSetIfChanged(ref _text, value);
    }

    
    
    public string GetName(string name) {
        int row = int.Parse(name[1].ToString());
        int col = int.Parse(name[2].ToString());

        string? plot = Battleships.Bot[row, col];
        return plot == "" ? "X" : plot![0].ToString();
    }

    public bool MakeTheBoard() {
        if (Regex.IsMatch(_text, "^[1-3],[1-4],[1-6],[0-8]$")) {
            int[] count = new int[] {
                int.Parse(_text[0].ToString()), int.Parse(_text[0].ToString()), int.Parse(_text[0].ToString()),
                int.Parse(_text[0].ToString())
            };
            Battleships.MakeBoard(Battleships.Bot, count);
            return true;
        }

        return false;
    }

    public bool MakePlayerBoard() {
        if (Regex.IsMatch(_text, "^[1-3],[1-4],[1-6],[0-8]$")) {
            int[] count = new int[] {
                int.Parse(_text[0].ToString()), int.Parse(_text[0].ToString()), int.Parse(_text[0].ToString()),
                int.Parse(_text[0].ToString())
            };
            Battleships.MakeBoard(Battleships.Player, count);
            return true;
        }

        return false;
    }
    
    public string GetPlayerName(string name) {
        int row = int.Parse(name[1].ToString());
        int col = int.Parse(name[2].ToString());

        string? plot = Battleships.Player[row, col];
        return plot == "" ? "X" : plot![0].ToString();
    }
}

class Battleships {
    private static Random _rand = new();
    
    public static string?[,] Bot = { 
        { "", "", "", "", "", "", "", "", "", "" }, 
        { "", "", "", "", "", "", "", "", "", "" },
        { "", "", "", "", "", "", "", "", "", "" },
        { "", "", "", "", "", "", "", "", "", "" },
        { "", "", "", "", "", "", "", "", "", "" },
        { "", "", "", "", "", "", "", "", "", "" },
        { "", "", "", "", "", "", "", "", "", "" },
        { "", "", "", "", "", "", "", "", "", "" },
        { "", "", "", "", "", "", "", "", "", "" },
        { "", "", "", "", "", "", "", "", "", "" } 
    };
    
    public static string?[,] Player = { 
        { "", "", "", "", "", "", "", "", "", "" }, 
        { "", "", "", "", "", "", "", "", "", "" },
        { "", "", "", "", "", "", "", "", "", "" },
        { "", "", "", "", "", "", "", "", "", "" },
        { "", "", "", "", "", "", "", "", "", "" },
        { "", "", "", "", "", "", "", "", "", "" },
        { "", "", "", "", "", "", "", "", "", "" },
        { "", "", "", "", "", "", "", "", "", "" },
        { "", "", "", "", "", "", "", "", "", "" },
        { "", "", "", "", "", "", "", "", "", "" } 
    };

    public static int MakeBoard(string?[,] board,int[] count) {
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
                            if (row < 4 || board[row,col] != "" || board[row - 1,col] != "" ||
                                board[row - 2,col] != "" || board[row - 3,col] != "" || board[row - 4,col] != "") {
                                break;
                            }

                            validplace = true;
                            board[row,col] = "Carrier";
                            board[row - 1,col] = "Carrier";
                            board[row - 2,col] = "Carrier";
                            board[row - 3,col] = "Carrier";
                            board[row - 4,col] = "Carrier";
                            break;
                        case 1: // Right
                            if (col > 5 || board[row,col] != "" || board[row,col + 1] != "" ||
                                board[row,col + 2] != "" || board[row,col + 3] != "" || board[row,col + 4] != "") {
                                break;
                            }

                            validplace = true;
                            board[row,col] = "Carrier";
                            board[row,col + 1] = "Carrier";
                            board[row,col + 2] = "Carrier";
                            board[row,col + 3] = "Carrier";
                            board[row,col + 4] = "Carrier";
                            break;
                        case 2: // Down
                            if (row > 5 || board[row,col] != "" || board[row + 1,col] != "" ||
                                board[row + 2,col] != "" || board[row + 3,col] != "" || board[row + 4,col] != "") {
                                break;
                            }

                            validplace = true;
                            board[row,col] = "Carrier";
                            board[row + 1,col] = "Carrier";
                            board[row + 2,col] = "Carrier";
                            board[row + 3,col] = "Carrier";
                            board[row + 4,col] = "Carrier";
                            break;
                        case 3: // Left
                            if (col < 4 || board[row,col] != "" || board[row,col - 1] != "" ||
                                board[row,col - 2] != "" || board[row,col - 3] != "" || board[row,col - 4] != "") {
                                break;
                            }

                            validplace = true;
                            board[row,col] = "Carrier";
                            board[row,col - 1] = "Carrier";
                            board[row,col - 2] = "Carrier";
                            board[row,col - 3] = "Carrier";
                            board[row,col - 4] = "Carrier";
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
                            if (row < 3 || board[row,col] != "" || board[row - 1,col] != "" ||
                                board[row - 2,col] != "" || board[row - 3,col] != "") {
                                break;
                            }

                            validplace = true;
                            board[row,col] = "Destroyer";
                            board[row - 1,col] = "Destroyer";
                            board[row - 2,col] = "Destroyer";
                            board[row - 3,col] = "Destroyer";
                            break;
                        case 1: // Right
                            if (col > 6 || board[row,col] != "" || board[row,col + 1] != "" ||
                                board[row,col + 2] != "" || board[row,col + 3] != "") {
                                break;
                            }

                            validplace = true;
                            board[row,col] = "Destroyer";
                            board[row,col + 1] = "Destroyer";
                            board[row,col + 2] = "Destroyer";
                            board[row,col + 3] = "Destroyer";
                            break;
                        case 2: // Down
                            if (row > 6 || board[row,col] != "" || board[row + 1,col] != "" ||
                                board[row + 2,col] != "" || board[row + 3,col] != "") {
                                break;
                            }

                            validplace = true;
                            board[row,col] = "Destroyer";
                            board[row + 1,col] = "Destroyer";
                            board[row + 2,col] = "Destroyer";
                            board[row + 3,col] = "Destroyer";
                            break;
                        case 3: // Left
                            if (col < 3 || board[row,col] != "" || board[row,col - 1] != "" ||
                                board[row,col - 2] != "" || board[row,col - 3] != "") {
                                break;
                            }

                            validplace = true;
                            board[row,col] = "Destroyer";
                            board[row,col - 1] = "Destroyer";
                            board[row,col - 2] = "Destroyer";
                            board[row,col - 3] = "Destroyer";
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
                            if (row < 2 || board[row,col] != "" || board[row - 1,col] != "" ||
                                board[row - 2,col] != "") {
                                break;
                            }

                            validplace = true;
                            board[row,col] = "Ship";
                            board[row - 1,col] = "Ship";
                            board[row - 2,col] = "Ship";
                            break;
                        case 1: // Right
                            if (col > 7 || board[row,col] != "" || board[row,col + 1] != "" ||
                                board[row,col + 2] != "") {
                                break;
                            }

                            validplace = true;
                            board[row,col] = "Ship";
                            board[row,col + 1] = "Ship";
                            board[row,col + 2] = "Ship";
                            break;
                        case 2: // Down
                            if (row > 7 || board[row,col] != "" || board[row + 1,col] != "" ||
                                board[row + 2,col] != "") {
                                break;
                            }

                            validplace = true;
                            board[row,col] = "Ship";
                            board[row + 1,col] = "Ship";
                            board[row + 2,col] = "Ship";
                            break;
                        case 3: // Left
                            if (col < 2 || board[row,col] != "" || board[row,col - 1] != "" ||
                                board[row,col - 2] != "") {
                                break;
                            }

                            validplace = true;
                            board[row,col] = "Ship";
                            board[row,col - 1] = "Ship";
                            board[row,col - 2] = "Ship";
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
                            if (row < 1 || board[row,col] != "" || board[row - 1,col] != "") {
                                break;
                            }

                            validplace = true;
                            board[row,col] = "Patrol";
                            board[row - 1,col] = "Patrol";
                            break;
                        case 1: // Right
                            if (col > 8 || board[row,col] != "" || board[row,col + 1] != "") {
                                break;
                            }

                            validplace = true;
                            board[row,col] = "Patrol";
                            board[row,col + 1] = "Patrol";
                            break;
                        case 2: // Down
                            if (row > 8 || board[row,col] != "" || board[row + 1,col] != "") {
                                break;
                            }

                            validplace = true;
                            board[row,col] = "Patrol";
                            board[row + 1,col] = "Patrol";
                            break;
                        case 3: // Left
                            if (col < 1 || board[row,col] != "" || board[row,col - 1] != "") {
                                break;
                            }

                            validplace = true;
                            board[row,col] = "Patrol";
                            board[row,col - 1] = "Patrol";
                            break;
                    }
                }
                plots += 2;
            }
            
            return plots;
    }
}