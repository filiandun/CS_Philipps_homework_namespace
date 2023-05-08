namespace GameWithFriend
{
    public class NaughtsAndCrosses
    {
        Random random; // просто рандом

        private byte fieldSize; // размер игрового поля
        private char[,]? field; // игровое поле, в виде двухмерного массива

        private byte currentRow; // текущая строка, где находится "курсор"
        private byte currentColumn; // текущий стобец, где находится "курсор"
        private byte maxPosition; // максимальная позиция, где может находится "курсор"

        private byte count; // счётчик того, сколько была вызвана функция findWinner();, конкретно он используется для выдачи ничьи

        private char firstPlayer; // за кого будет играть первый игрок
        private char secondPlayer; // за кого будет играть второй игрок


        //////


        public NaughtsAndCrosses()
        {
            this.random = new Random();

            this.fieldSize = 0;
            this.field = null;

            this.currentRow = 0;
            this.currentColumn = 0;
            this.maxPosition = 0;

            this.count = 0;

            this.firstPlayer = 'X';
            this.secondPlayer = 'O';
        }


        //////


        public void Playing()
        {
            Console.Clear();
            Console.WriteLine("КРЕСТИКИ-НОЛИКИ\n");

            Console.Write("* введите, за кого будет играть первый игрок, за крестики (X) или нолики (O): "); this.ChoicingSide();
            Console.WriteLine($"* второй игрок играет за {(this.secondPlayer == 'O' ? "нолики (O)" : "крестики (X)")}\n");

            Console.Write($"\n* введите желаемый размер поля игры (диапазон от 2 до 12, рекомендуется 3): "); this.ChoicingFieldSize();

            byte whoFirst = (byte)this.random.Next(0, 2);
            Console.WriteLine($"\n* методом рандома было решено, что {(whoFirst == 0 ? "первый" : "второй")} игрок ходит первый");
            Console.WriteLine("* управление реализовано стрелочками и клавишей enter\n");

            Console.Write("Для продолженния нажмите любую клавишу.."); Console.ReadKey();


            if (whoFirst == 0) // если первым ходит первый
            {
                while (true)
                {
                    this.PlayersMove(this.firstPlayer);
                    if (this.FindWinner() == true) { return; }

                    this.PlayersMove(this.secondPlayer);
                    if (this.FindWinner() == true) { return; }
                }
            }


            if (whoFirst == 1) // если первым ходит второй игрок
            {
                while (true)
                {
                    this.PlayersMove(this.secondPlayer);
                    if (this.FindWinner() == true) { return; }

                    this.PlayersMove(this.firstPlayer);
                    if (this.FindWinner() == true) { return; }

                }
            }
        }


        //////


        private void ChoicingSide()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("КРЕСТИКИ-НОЛИКИ\n");
                Console.Write("* введите, за кого будет играть первый игрок, за крестики (X) или нолики (O): ");

                try
                {
                    this.firstPlayer = Convert.ToChar(Console.ReadLine().Trim(' ', '\t')); // Trim убирает пробелы и табуляции

                    if (this.firstPlayer != 'X' && this.firstPlayer != 'O')
                    {
                        throw new FormatException();
                    }

                    break;
                }

                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"\nОШИБКА: вы ввели что-то не то!"); Console.ResetColor();
                    Console.Write("Нажите любую клавишу для продолжения.."); Console.ReadKey();
                }
                catch (OverflowException oe)
                {
                    Console.WriteLine($"ОШИБКА: {oe.Message}");
                    Console.Write("Нажите любую клавишу для продолжения.."); Console.ReadKey();
                }
            }
            while (true);

            if (this.firstPlayer == 'O') { this.secondPlayer = 'X'; }
        }


        //////


        private void ChoicingFieldSize()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("КРЕСТИКИ-НОЛИКИ\n");

                Console.WriteLine($"* введите, за кого будет играть первый игрок, за крестики (X) или нолики (O): {this.firstPlayer}");
                Console.WriteLine($"* второй игрок играет за {(this.secondPlayer == 'O' ? "нолики (O)" : "крестики (X)")}\n");

                Console.Write($"* введите желаемый размер поля игры (диапазон от 2 до 12, рекомендуется 3): ");

                try
                {
                    this.fieldSize = Convert.ToByte(Console.ReadLine().Trim(' ', '\t')); // Trim убирает пробелы и табуляции

                    if (this.fieldSize < 2 || this.fieldSize > 12)
                    {
                        throw new OverflowException();
                    }

                    break;
                }

                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"\nОШИБКА: вы ввели что-то не то!"); Console.ResetColor();
                    Console.Write("Нажите любую клавишу для продолжения.."); Console.ReadKey();
                }
                catch (OverflowException)
                {
                    Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"\nОШИБКА: вы вышли за дипазон!"); Console.ResetColor();
                    Console.Write("Нажите любую клавишу для продолжения.."); Console.ReadKey();
                }
            }
            while (true);


            this.field = new char[this.fieldSize, this.fieldSize];
            for (int i = 0; i < this.fieldSize; ++i)
            {
                for (int j = 0; j < this.fieldSize; ++j)
                {
                    this.field[i, j] = ' ';
                }
            }
            this.maxPosition = this.fieldSize;
        }


        //////


        private void PlayersMove(char who)
        {
            ConsoleKeyInfo key;

            do
            {
                Console.Clear(); Console.WriteLine($"КРЕСТИКИ-НОЛИКИ ({(who == this.firstPlayer ? "ХОД ПЕРВОГО ИГРОКА" : "ХОД ВТОРОГО ИГРОКА")})");
                this.ShowField(this.currentRow, this.currentColumn, who);

                key = Console.ReadKey(); // считывание значения нажатой клавиши

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow: if (this.currentRow - 1 >= 0) { --this.currentRow; }; break; // нажата клавиша вверх
                    case ConsoleKey.DownArrow: if (this.currentRow + 1 < this.maxPosition) { ++this.currentRow; }; break; // нажата клавиша вниз
                    case ConsoleKey.RightArrow: if (this.currentColumn + 1 < this.maxPosition) { ++this.currentColumn; }; break; // нажата клавиша вправо
                    case ConsoleKey.LeftArrow: if (this.currentColumn - 1 >= 0) { --this.currentColumn; }; break; // нажата клавиша влево

                    case ConsoleKey.Enter: if (this.EditField(this.currentRow, this.currentColumn, who)) { return; } Console.WriteLine("\nКлетка занята!"); Console.Write("Для продолжения нажмите любую клавишу.."); Console.ReadKey(); break; // нажата клавиша enter

                    default: this.ShowField(this.currentRow, this.currentColumn, who); break;
                }
            }
            while (true);
        }


        //////


        private bool FindWinner()
        {
            ++this.count;

            byte countForWin = 0;

            // ПРОВЕРКА НА ПОБЕДУ ПО ГОРИЗОНТАЛИ
            for (int i = 0; i < this.fieldSize; ++i)
            {
                for (int j = 0; j < this.fieldSize - 1; ++j)
                {
                    if (this.field[i, j] == this.field[i, j + 1] && this.field[i, j] != ' ')
                    {
                        ++countForWin;
                    }
                    else
                    {
                        break;
                    }
                }

                if (countForWin == this.fieldSize - 1)
                {
                    Console.Clear();
                    Console.WriteLine("КРЕСТИКИ-НОЛИКИ");

                    this.ShowField();

                    Console.Write($"\nВЫИГРАЛ {(this.field[i, i] == this.firstPlayer ? "ПЕРВЫЙ ИГРОК" : "ВТОРОЙ ИГРОК")}!");

                    return true;
                }

                countForWin = 0;
            }



            // ПРОВЕРКА НА ПОБЕДУ ПО ВЕРТИКАЛИ
            for (int j = 0; j < this.fieldSize; ++j)
            {
                for (int i = 0; i < this.fieldSize - 1; ++i)
                {
                    if (this.field[i, j] == this.field[i + 1, j] && this.field[i, j] != ' ')
                    {
                        ++countForWin;
                    }
                    else
                    {
                        break;
                    }
                }

                if (countForWin == this.fieldSize - 1)
                {
                    Console.Clear();
                    Console.WriteLine("КРЕСТИКИ-НОЛИКИ");

                    this.ShowField();

                    Console.Write($"\nВЫИГРАЛ {(this.field[j, j] == this.firstPlayer ? "ПЕРВЫЙ ИГРОК" : "ВТОРОЙ ИГРОК")}!");

                    return true;
                }
                countForWin = 0;
            }



            // ПРОВЕРКА НА ПОБЕДУ ПО ОДНОЙ ДИАГОНАЛИ
            for (int i = 0; i < this.fieldSize - 1; ++i)
            {
                if (this.field[i, i] == this.field[i + 1, i + 1] && this.field[i, i] != ' ')
                {
                    ++countForWin;
                }
                else
                {
                    break;
                }
            }

            if (countForWin == this.fieldSize - 1)
            {
                Console.Clear();
                Console.WriteLine("КРЕСТИКИ-НОЛИКИ");

                this.ShowField();

                Console.Write($"\nВЫИГРАЛ {(this.field[this.fieldSize - 1, this.fieldSize - 1] == this.firstPlayer ? "ПЕРВЫЙ ИГРОК" : "ВТОРОЙ ИГРОК")}!");

                return true;
            }

            countForWin = 0;



            // ПРОВЕРКА НА ПОБЕДУ ПО ВТОРОЙ ДИАГОНАЛИ
            for (int i = 0; i < this.fieldSize - 1; ++i)
            {
                if (this.field[i, this.fieldSize - i - 1] == this.field[i + 1, this.fieldSize - i - 2] && this.field[i, this.fieldSize - i - 1] != ' ')
                {
                    ++countForWin;
                }
                else
                {
                    break;
                }
            }

            if (countForWin == this.fieldSize - 1)
            {
                Console.Clear();
                Console.WriteLine("КРЕСТИКИ-НОЛИКИ");
                this.ShowField();
                Console.Write($"\nВЫИГРАЛ {(this.field[this.fieldSize - 1, 0] == this.firstPlayer ? "ПЕРВЫЙ ИГРОК" : "ВТОРОЙ ИГРОК")}!");

                return true;
            }



            // ПРОВЕРКА НА НИЧЬЮ
            if (this.count == this.fieldSize * this.fieldSize)
            {
                Console.Clear();
                Console.Write("КРЕСТИКИ-НОЛИКИ\n");
                this.ShowField();
                Console.Write("\nНичья!");

                return true;
            }

            return false;
        }


        //////


        private bool EditField(byte row, byte column, char who)
        {
            if (this.field[row, column] == ' ')
            {
                this.field[row, column] = who;
                return true;
            }
            return false;
        }


        //////


        private void ShowField(byte row = 255, byte column = 255, char who = 'X')
        {
            Console.WriteLine();
            for (int i = 0; i < this.fieldSize; ++i)
            {
                for (int j = 0; j < this.fieldSize; ++j)
                {
                    if (i == row && j == column)
                    {
                        if (this.field[i, j] == ' ') // цвет текущей ячейки меняет, в зависимости, занята ли она 
                        {
                            Console.ForegroundColor = ConsoleColor.Green; Console.Write($" {who}"); Console.ResetColor(); // если свободна, то цвет зелёный
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red; Console.Write($" {this.field[i, j]}"); Console.ResetColor(); // если занята, то цвет красный
                        }
                    }
                    else
                    {
                        Console.Write($" {this.field[i, j]}");
                    }

                    if (j < this.fieldSize - 1) // вывод между колонами
                    {
                        Console.Write(" |");
                    }
                }

                if (i < this.fieldSize - 1) // вывод между строками
                {
                    Console.WriteLine();
                    for (int l = 0; l < this.fieldSize; ++l)
                    {
                        Console.Write("---");
                        if (l < this.fieldSize - 1)
                        {
                            Console.Write("|");
                        }
                    }
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }


        ///////
    }
}
