using System;
using System.IO;
using ReactiveUI;

namespace BattleshipsGUI.ViewModels;

public class MainWindowViewModel : ViewModelBase {
    private Random _rand = new();

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
            File.WriteAllText("./Dependencies/Ships/Carrier.txt", _carriers.ToString());
        }
    }

    private int _destroyers;

    public int Destroyers {
        get => _destroyers;
        set {
            this.RaiseAndSetIfChanged(ref _destroyers, value);
            File.WriteAllText("./Dependencies/Ships/Destroyer.txt", _destroyers.ToString());
        }
    }

    private int _ships;

    public int Ships {
        get => _ships;
        set {
            this.RaiseAndSetIfChanged(ref _ships, value);
            File.WriteAllText("./Dependencies/Ships/Ship.txt", _ships.ToString());
        }
    }

    private int _patrols;

    public int Patrols {
        get => _patrols;
        set {
            this.RaiseAndSetIfChanged(ref _patrols, value);
            File.WriteAllText("./Dependencies/Ships/Patrol.txt", _patrols.ToString());
        }
    }

    private void StartTheGame() {
        BoardValues();
        MakeBotBoard();
        MakePlayerBoard();
        EnemyShipsLeft = _count[0] * 5 + _count[1] * 4 + _count[2] * 3 + _count[3] * 2;
        HowManyShips = "Ships left: " + EnemyShipsLeft.ToString();
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
                    case 0:
                        count[i] = r.Next(3);
                        break;
                    case 1:
                        count[i] = r.Next(4);
                        break;
                    case 2:
                        count[i] = r.Next(6);
                        break;
                    case 3:
                        count[i] = r.Next(-1, 9);
                        break;
                }
            }
        }

        count[3] = count[3] == 9 ? -1 : count[3];
        _count = new[] { count[0] + 1, count[1] + 1, count[2] + 1, count[3] + 1 };
    }

    private int _boardCount;
    public void MakeBotBoard() {
        bool validplace;
        for (var x = 0; x < _count[0]; x++) {
            validplace = false;
            while (!validplace) {
                var row = _rand.Next(10);
                var col = _rand.Next(10);
                var direction = _rand.Next(4);

                switch (direction) {
                    case 0: // Up
                        if (row < 4 || BotBoard[row][col] != "O" || BotBoard[row - 1][col] != "O" ||
                            BotBoard[row - 2][col] != "O" || BotBoard[row - 3][col] != "O" ||
                            BotBoard[row - 4][col] != "O")
                            break;

                        validplace = true;
                        BotBoard[row][col] = "Carrier" + x.ToString();
                        BotBoard[row - 1][col] = "Carrier" + x.ToString();
                        BotBoard[row - 2][col] = "Carrier" + x.ToString();
                        BotBoard[row - 3][col] = "Carrier" + x.ToString();
                        BotBoard[row - 4][col] = "Carrier" + x.ToString();
                        break;
                    case 1: // Right
                        if (col > 5 || BotBoard[row][col] != "O" || BotBoard[row][col + 1] != "O" ||
                            BotBoard[row][col + 2] != "O" || BotBoard[row][col + 3] != "O" ||
                            BotBoard[row][col + 4] != "O")
                            break;

                        validplace = true;
                        BotBoard[row][col] = "Carrier" + x.ToString();
                        BotBoard[row][col + 1] = "Carrier" + x.ToString();
                        BotBoard[row][col + 2] = "Carrier" + x.ToString();
                        BotBoard[row][col + 3] = "Carrier" + x.ToString();
                        BotBoard[row][col + 4] = "Carrier" + x.ToString();
                        break;
                    case 2: // Down
                        if (row > 5 || BotBoard[row][col] != "O" || BotBoard[row + 1][col] != "O" ||
                            BotBoard[row + 2][col] != "O" || BotBoard[row + 3][col] != "O" ||
                            BotBoard[row + 4][col] != "O")
                            break;

                        validplace = true;
                        BotBoard[row][col] = "Carrier" + x.ToString();
                        BotBoard[row + 1][col] = "Carrier" + x.ToString();
                        BotBoard[row + 2][col] = "Carrier" + x.ToString();
                        BotBoard[row + 3][col] = "Carrier" + x.ToString();
                        BotBoard[row + 4][col] = "Carrier" + x.ToString();
                        break;
                    case 3: // Left
                        if (col < 4 || BotBoard[row][col] != "O" || BotBoard[row][col - 1] != "O" ||
                            BotBoard[row][col - 2] != "O" || BotBoard[row][col - 3] != "O" ||
                            BotBoard[row][col - 4] != "O")
                            break;

                        validplace = true;
                        BotBoard[row][col] = "Carrier" + x.ToString();
                        BotBoard[row][col - 1] = "Carrier" + x.ToString();
                        BotBoard[row][col - 2] = "Carrier" + x.ToString();
                        BotBoard[row][col - 3] = "Carrier" + x.ToString();
                        BotBoard[row][col - 4] = "Carrier" + x.ToString();
                        break;
                }
            }
            _botCount += 5;
        }
        for (var x = 0; x < _count[1]; x++) {
            validplace = false;
            while (!validplace) {
                var row = _rand.Next(10);
                var col = _rand.Next(10);
                var direction = _rand.Next(4);

                switch (direction) {
                    case 0: // Up
                        if (row < 3 || BotBoard[row][col] != "O" || BotBoard[row - 1][col] != "O" ||
                            BotBoard[row - 2][col] != "O" || BotBoard[row - 3][col] != "O")
                            break;

                        validplace = true;
                        BotBoard[row][col] = "Destroyer" + x.ToString();
                        BotBoard[row - 1][col] = "Destroyer" + x.ToString();
                        BotBoard[row - 2][col] = "Destroyer" + x.ToString();
                        BotBoard[row - 3][col] = "Destroyer" + x.ToString();
                        break;
                    case 1: // Right
                        if (col > 6 || BotBoard[row][col] != "O" || BotBoard[row][col + 1] != "O" ||
                            BotBoard[row][col + 2] != "O" || BotBoard[row][col + 3] != "O")
                            break;

                        validplace = true;
                        BotBoard[row][col] = "Destroyer" + x.ToString();
                        BotBoard[row][col + 1] = "Destroyer" + x.ToString();
                        BotBoard[row][col + 2] = "Destroyer" + x.ToString();
                        BotBoard[row][col + 3] = "Destroyer" + x.ToString();
                        break;
                    case 2: // Down
                        if (row > 6 || BotBoard[row][col] != "O" || BotBoard[row + 1][col] != "O" ||
                            BotBoard[row + 2][col] != "O" || BotBoard[row + 3][col] != "O")
                            break;

                        validplace = true;
                        BotBoard[row][col] = "Destroyer" + x.ToString();
                        BotBoard[row + 1][col] = "Destroyer" + x.ToString();
                        BotBoard[row + 2][col] = "Destroyer" + x.ToString();
                        BotBoard[row + 3][col] = "Destroyer" + x.ToString();
                        break;
                    case 3: // Left
                        if (col < 3 || BotBoard[row][col] != "O" || BotBoard[row][col - 1] != "O" ||
                            BotBoard[row][col - 2] != "O" || BotBoard[row][col - 3] != "O")
                            break;

                        validplace = true;
                        BotBoard[row][col] = "Destroyer" + x.ToString();
                        BotBoard[row][col - 1] = "Destroyer" + x.ToString();
                        BotBoard[row][col - 2] = "Destroyer" + x.ToString();
                        BotBoard[row][col - 3] = "Destroyer" + x.ToString();
                        break;
                }
            }
            _botCount += 4;
        }
        for (var x = 0; x < _count[2]; x++) {
            validplace = false;
            while (!validplace) {
                var row = _rand.Next(10);
                var col = _rand.Next(10);
                var direction = _rand.Next(4);

                switch (direction) {
                    case 0: // Up
                        if (row < 2 || BotBoard[row][col] != "O" || BotBoard[row - 1][col] != "O" ||
                            BotBoard[row - 2][col] != "O")
                            break;

                        validplace = true;
                        BotBoard[row][col] = "Ship" + x.ToString();
                        BotBoard[row - 1][col] = "Ship" + x.ToString();
                        BotBoard[row - 2][col] = "Ship" + x.ToString();
                        break;
                    case 1: // Right
                        if (col > 7 || BotBoard[row][col] != "O" || BotBoard[row][col + 1] != "O" ||
                            BotBoard[row][col + 2] != "O")
                            break;

                        validplace = true;
                        BotBoard[row][col] = "Ship" + x.ToString();
                        BotBoard[row][col + 1] = "Ship" + x.ToString();
                        BotBoard[row][col + 2] = "Ship" + x.ToString();
                        break;
                    case 2: // Down
                        if (row > 7 || BotBoard[row][col] != "O" || BotBoard[row + 1][col] != "O" ||
                            BotBoard[row + 2][col] != "O")
                            break;

                        validplace = true;
                        BotBoard[row][col] = "Ship" + x.ToString();
                        BotBoard[row + 1][col] = "Ship" + x.ToString();
                        BotBoard[row + 2][col] = "Ship" + x.ToString();
                        break;
                    case 3: // Left
                        if (col < 2 || BotBoard[row][col] != "O" || BotBoard[row][col - 1] != "O" ||
                            BotBoard[row][col - 2] != "O")
                            break;

                        validplace = true;
                        BotBoard[row][col] = "Ship" + x.ToString();
                        BotBoard[row][col - 1] = "Ship" + x.ToString();
                        BotBoard[row][col - 2] = "Ship" + x.ToString();
                        break;
                }
            }
            _botCount += 3;
        }

        for (var x = 0; x < _count[3]; x++) {
            validplace = false;
            while (!validplace) {
                var row = _rand.Next(10);
                var col = _rand.Next(10);
                var direction = _rand.Next(4);

                switch (direction) {
                    case 0: // Up
                        if (row < 1 || BotBoard[row][col] != "O" || BotBoard[row - 1][col] != "O") break;

                        validplace = true;
                        BotBoard[row][col] = "Patrol" + x.ToString();
                        BotBoard[row - 1][col] = "Patrol" + x.ToString();
                        break;
                    case 1: // Right
                        if (col > 8 || BotBoard[row][col] != "O" || BotBoard[row][col + 1] != "O") break;

                        validplace = true;
                        BotBoard[row][col] = "Patrol" + x.ToString();
                        BotBoard[row][col + 1] = "Patrol" + x.ToString();
                        break;
                    case 2: // Down
                        if (row > 8 || BotBoard[row][col] != "O" || BotBoard[row + 1][col] != "O") break;

                        validplace = true;
                        BotBoard[row][col] = "Patrol" + x.ToString();
                        BotBoard[row + 1][col] = "Patrol" + x.ToString();
                        break;
                    case 3: // Left
                        if (col < 1 || BotBoard[row][col] != "O" || BotBoard[row][col - 1] != "O") break;

                        validplace = true;
                        BotBoard[row][col] = "Patrol" + x.ToString();
                        BotBoard[row][col - 1] = "Patrol" + x.ToString();
                        break;
                }
            }
            _botCount += 2;
        }
        BB = BotBoard;
    }

    private int _botCount;
    public void MakePlayerBoard() {
        bool validplace;
        for (var x = 0; x < _count[0]; x++) {
            validplace = false;
            while (!validplace) {
                var row = _rand.Next(10);
                var col = _rand.Next(10);
                var direction = _rand.Next(4);

                switch (direction) {
                    case 0: // Up
                        if (row < 4 || PlayerBoard[row][col] != "O" || PlayerBoard[row - 1][col] != "O" ||
                            PlayerBoard[row - 2][col] != "O" || PlayerBoard[row - 3][col] != "O" ||
                            PlayerBoard[row - 4][col] != "O")
                            break;

                        validplace = true;
                        PlayerBoard[row][col] = "Carrier" + x.ToString();
                        PlayerBoard[row - 1][col] = "Carrier" + x.ToString();
                        PlayerBoard[row - 2][col] = "Carrier" + x.ToString();
                        PlayerBoard[row - 3][col] = "Carrier" + x.ToString();
                        PlayerBoard[row - 4][col] = "Carrier" + x.ToString();
                        break;
                    case 1: // Right
                        if (col > 5 || PlayerBoard[row][col] != "O" || PlayerBoard[row][col + 1] != "O" ||
                            PlayerBoard[row][col + 2] != "O" || PlayerBoard[row][col + 3] != "O" ||
                            PlayerBoard[row][col + 4] != "O")
                            break;

                        validplace = true;
                        PlayerBoard[row][col] = "Carrier" + x.ToString();
                        PlayerBoard[row][col + 1] = "Carrier" + x.ToString();
                        PlayerBoard[row][col + 2] = "Carrier" + x.ToString();
                        PlayerBoard[row][col + 3] = "Carrier" + x.ToString();
                        PlayerBoard[row][col + 4] = "Carrier" + x.ToString();
                        break;
                    case 2: // Down
                        if (row > 5 || PlayerBoard[row][col] != "O" || PlayerBoard[row + 1][col] != "O" ||
                            PlayerBoard[row + 2][col] != "O" || PlayerBoard[row + 3][col] != "O" ||
                            PlayerBoard[row + 4][col] != "O")
                            break;

                        validplace = true;
                        PlayerBoard[row][col] = "Carrier" + x.ToString();
                        PlayerBoard[row + 1][col] = "Carrier" + x.ToString();
                        PlayerBoard[row + 2][col] = "Carrier" + x.ToString();
                        PlayerBoard[row + 3][col] = "Carrier" + x.ToString();
                        PlayerBoard[row + 4][col] = "Carrier" + x.ToString();
                        break;
                    case 3: // Left
                        if (col < 4 || PlayerBoard[row][col] != "O" || PlayerBoard[row][col - 1] != "O" ||
                            PlayerBoard[row][col - 2] != "O" || PlayerBoard[row][col - 3] != "O" ||
                            PlayerBoard[row][col - 4] != "O")
                            break;

                        validplace = true;
                        PlayerBoard[row][col] = "Carrier" + x.ToString();
                        PlayerBoard[row][col - 1] = "Carrier" + x.ToString();
                        PlayerBoard[row][col - 2] = "Carrier" + x.ToString();
                        PlayerBoard[row][col - 3] = "Carrier" + x.ToString();
                        PlayerBoard[row][col - 4] = "Carrier" + x.ToString();
                        break;
                }
            }
            _botCount += 5;
        }
        for (var x = 0; x < _count[1]; x++) {
            validplace = false;
            while (!validplace) {
                var row = _rand.Next(10);
                var col = _rand.Next(10);
                var direction = _rand.Next(4);

                switch (direction) {
                    case 0: // Up
                        if (row < 3 || PlayerBoard[row][col] != "O" || PlayerBoard[row - 1][col] != "O" ||
                            PlayerBoard[row - 2][col] != "O" || PlayerBoard[row - 3][col] != "O")
                            break;

                        validplace = true;
                        PlayerBoard[row][col] = "Destroyer" + x.ToString();
                        PlayerBoard[row - 1][col] = "Destroyer" + x.ToString();
                        PlayerBoard[row - 2][col] = "Destroyer" + x.ToString();
                        PlayerBoard[row - 3][col] = "Destroyer" + x.ToString();
                        break;
                    case 1: // Right
                        if (col > 6 || PlayerBoard[row][col] != "O" || PlayerBoard[row][col + 1] != "O" ||
                            PlayerBoard[row][col + 2] != "O" || PlayerBoard[row][col + 3] != "O")
                            break;

                        validplace = true;
                        PlayerBoard[row][col] = "Destroyer" + x.ToString();
                        PlayerBoard[row][col + 1] = "Destroyer" + x.ToString();
                        PlayerBoard[row][col + 2] = "Destroyer" + x.ToString();
                        PlayerBoard[row][col + 3] = "Destroyer" + x.ToString();
                        break;
                    case 2: // Down
                        if (row > 6 || PlayerBoard[row][col] != "O" || PlayerBoard[row + 1][col] != "O" ||
                            PlayerBoard[row + 2][col] != "O" || PlayerBoard[row + 3][col] != "O")
                            break;

                        validplace = true;
                        PlayerBoard[row][col] = "Destroyer" + x.ToString();
                        PlayerBoard[row + 1][col] = "Destroyer" + x.ToString();
                        PlayerBoard[row + 2][col] = "Destroyer" + x.ToString();
                        PlayerBoard[row + 3][col] = "Destroyer" + x.ToString();
                        break;
                    case 3: // Left
                        if (col < 3 || PlayerBoard[row][col] != "O" || PlayerBoard[row][col - 1] != "O" ||
                            PlayerBoard[row][col - 2] != "O" || PlayerBoard[row][col - 3] != "O")
                            break;

                        validplace = true;
                        PlayerBoard[row][col] = "Destroyer" + x.ToString();
                        PlayerBoard[row][col - 1] = "Destroyer" + x.ToString();
                        PlayerBoard[row][col - 2] = "Destroyer" + x.ToString();
                        PlayerBoard[row][col - 3] = "Destroyer" + x.ToString();
                        break;
                }
            }
            _botCount += 4;
        }
        for (var x = 0; x < _count[2]; x++) {
            validplace = false;
            while (!validplace) {
                var row = _rand.Next(10);
                var col = _rand.Next(10);
                var direction = _rand.Next(4);

                switch (direction) {
                    case 0: // Up
                        if (row < 2 || PlayerBoard[row][col] != "O" || PlayerBoard[row - 1][col] != "O" ||
                            PlayerBoard[row - 2][col] != "O")
                            break;

                        validplace = true;
                        PlayerBoard[row][col] = "Ship" + x.ToString();
                        PlayerBoard[row - 1][col] = "Ship" + x.ToString();
                        PlayerBoard[row - 2][col] = "Ship" + x.ToString();
                        break;
                    case 1: // Right
                        if (col > 7 || PlayerBoard[row][col] != "O" || PlayerBoard[row][col + 1] != "O" ||
                            PlayerBoard[row][col + 2] != "O")
                            break;

                        validplace = true;
                        PlayerBoard[row][col] = "Ship" + x.ToString();
                        PlayerBoard[row][col + 1] = "Ship" + x.ToString();
                        PlayerBoard[row][col + 2] = "Ship" + x.ToString();
                        break;
                    case 2: // Down
                        if (row > 7 || PlayerBoard[row][col] != "O" || PlayerBoard[row + 1][col] != "O" ||
                            PlayerBoard[row + 2][col] != "O")
                            break;

                        validplace = true;
                        PlayerBoard[row][col] = "Ship" + x.ToString();
                        PlayerBoard[row + 1][col] = "Ship" + x.ToString();
                        PlayerBoard[row + 2][col] = "Ship" + x.ToString();
                        break;
                    case 3: // Left
                        if (col < 2 || PlayerBoard[row][col] != "O" || PlayerBoard[row][col - 1] != "O" ||
                            PlayerBoard[row][col - 2] != "O")
                            break;

                        validplace = true;
                        PlayerBoard[row][col] = "Ship" + x.ToString();
                        PlayerBoard[row][col - 1] = "Ship" + x.ToString();
                        PlayerBoard[row][col - 2] = "Ship" + x.ToString();
                        break;
                }
            }
            _botCount += 3;
        }

        for (var x = 0; x < _count[3]; x++) {
            validplace = false;
            while (!validplace) {
                var row = _rand.Next(10);
                var col = _rand.Next(10);
                var direction = _rand.Next(4);

                switch (direction) {
                    case 0: // Up
                        if (row < 1 || PlayerBoard[row][col] != "O" || PlayerBoard[row - 1][col] != "O") break;

                        validplace = true;
                        PlayerBoard[row][col] = "Patrol" + x.ToString();
                        PlayerBoard[row - 1][col] = "Patrol" + x.ToString();
                        break;
                    case 1: // Right
                        if (col > 8 || PlayerBoard[row][col] != "O" || PlayerBoard[row][col + 1] != "O") break;

                        validplace = true;
                        PlayerBoard[row][col] = "Patrol" + x.ToString();
                        PlayerBoard[row][col + 1] = "Patrol" + x.ToString();
                        break;
                    case 2: // Down
                        if (row > 8 || PlayerBoard[row][col] != "O" || PlayerBoard[row + 1][col] != "O") break;

                        validplace = true;
                        PlayerBoard[row][col] = "Patrol" + x.ToString();
                        PlayerBoard[row + 1][col] = "Patrol" + x.ToString();
                        break;
                    case 3: // Left
                        if (col < 1 || PlayerBoard[row][col] != "O" || PlayerBoard[row][col - 1] != "O") break;

                        validplace = true;
                        PlayerBoard[row][col] = "Patrol" + x.ToString();
                        PlayerBoard[row][col - 1] = "Patrol" + x.ToString();
                        break;
                }
            }
            _botCount += 2;
        }
        PB = PlayerBoard;
    }
    
    public int EnemyShipsLeft = -1;
    private string _howManyShips = "";

    public string HowManyShips {
        get => _howManyShips;
        set => this.RaiseAndSetIfChanged(ref _howManyShips, value);
    }

    public void Clicked(string name) {
        (int, int) coords = (int.Parse(name[1].ToString()), int.Parse(name[2].ToString()));
        string plot = BB[coords.Item1][coords.Item2];
        if (plot != "O") {
            UpdateCounter();
        }
        SinkShip();
    }

    private void UpdateCounter() {
        EnemyShipsLeft -= 1;
        HowManyShips = "Ships left: " + EnemyShipsLeft.ToString();
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
                    File.WriteAllText("./Dependencies/Board/P" + row.ToString() + col.ToString() + ".txt",
                        PlayerBoard[row][col]);
                }
            }
        }
    }

    public string[][] BB {
        get => BotBoard;
        set {
            this.RaiseAndSetIfChanged(ref BotBoard, value);
            for (int row = 0; row < 10; row++) {
                for (int col = 0; col < 10; col++) {
                    File.WriteAllText("./Dependencies/Board/B" + row.ToString() + col.ToString() + ".txt", BotBoard[row][col]);
                }
            }
        }
    }

    private string[][] PlayerBoard = {
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

    private string[][] BotBoard = {
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