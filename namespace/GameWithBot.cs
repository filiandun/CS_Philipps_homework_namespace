using System.Globalization;

namespace GameWithBot
{
    public class NaughtsAndCrosses
    {
        Random random; // просто рандом

        private byte fieldSize; // размер игрового поля
        private char[,] field; // иговое поле, в виде двухмерного массива

        private byte currentRow; // текущая строка, где находится "курсор"
        private byte currentColumn; // текущий стобец, где находится "курсор"
        private byte maxPosition; // максимальная позиция, где может находится "курсор"

        private byte count; // счётчик того, сколько была вызвана функция findWinner();, конкретно он используется для выдачи ничьи


        //////


        public NaughtsAndCrosses() 
        {
            this.random = new Random();

            this.fieldSize = 5;
            this.field = new char[this.fieldSize, this.fieldSize];
            for (int i = 0; i < this.fieldSize; ++i)
            {
                for (int j = 0; j < this.fieldSize; ++j)
                {
                    this.field[i, j] = ' ';
                }
            }

            this.currentRow = 0;
            this.currentColumn = 0;
            this.maxPosition = this.fieldSize;

            this.count = 0;
        }


        //////


        public void Playing()
        {
            Console.WriteLine("КРЕСТИКИ-НОЛИКИ\n");

            Console.WriteLine("* вы играете за крестики (X)");
            Console.WriteLine("* ваш компьюетрный соперник за нолики (O)\n");

            byte whoFirst = 0; //(byte)this.random.Next(0, 2);
            Console.WriteLine($"* методом рандома было решено, что {(whoFirst == 0 ? "вы ходите первым" : "бот ходит первым")}");
            Console.WriteLine("* упрвление реализовано стрелочками и клавишей enter\n");

            Console.Write("Для продолженния нажмите любую клавишу.."); Console.ReadKey();


            if (whoFirst == 0) // если первым ходит игрок
            {
                while (true)
                {

                    //this.field[0, 0] = this.field[0, 1] = this.field[0, 2] = this.field[0, 3] = this.field[0, 4] = 'X'; this.ShowField();
                    //this.field[1, 0] = this.field[1, 1] = this.field[1, 2] = this.field[1, 3] = this.field[1, 4] = 'X'; this.ShowField();
                    //this.field[2, 0] = this.field[2, 1] = 'X'; this.ShowField();

                    //this.field[0, 0] = this.field[1, 0] = this.field[2, 0] = this.field[3, 0] = this.field[4, 0] = 'X'; this.ShowField();
                    //this.field[0, 1] = this.field[1, 1] = this.field[2, 1] = 'X'; this.ShowField();
                    //this.field[0, 2] = this.field[1, 2] = this.field[2, 2] = 'X'; this.ShowField();

                    //this.field[0, 0] = this.field[1, 1] = this.field[2, 2] = this.field[3, 3] = this.field[4, 4] = 'X';
                    //this.field[4, 0] = this.field[3, 1] = this.field[2, 2] = this.field[1, 3] = this.field[0, 4] = 'X';
                    if (this.FindWinner() == true) { return; }




                    // this.PlayerMove();
                    //if (this.FindWinner() == true) { return; }

                    //this.BotMove();
                    //if (this.FindWinner() == true) { return; }
                }
            }


            if (whoFirst == 1) // если первым ходит бот
            {
                while (true)
                {
                    this.BotMove();
                    if (this.FindWinner() == true) { return; }

                    this.PlayerMove();
                    if (this.FindWinner() == true) { return; }
                }
            }
        }


        //////


        private void PlayerMove()
        {
            ConsoleKeyInfo key;

            do
            {
                Console.Clear(); Console.WriteLine("КРЕСТИКИ-НОЛИКИ (ВАШ ХОД)");
                this.ShowField(this.currentRow, this.currentColumn, 'X');

                key = Console.ReadKey(); // считывание значения нажатой клавиши

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow: if (this.currentRow - 1 >= 0) { --this.currentRow; }; break; // нажата клавиша вверх
                    case ConsoleKey.DownArrow: if (this.currentRow + 1 < this.maxPosition) { ++this.currentRow; }; break; // нажата клавиша вниз
                    case ConsoleKey.RightArrow: if (this.currentColumn + 1 < this.maxPosition) { ++this.currentColumn; }; break; // нажата клавиша вправо
                    case ConsoleKey.LeftArrow: if (this.currentColumn - 1 >= 0) { --this.currentColumn; }; break; // нажата клавиша влево

                    case ConsoleKey.Enter: if (this.EditField(this.currentRow, this.currentColumn, 'X')) { return; } Console.WriteLine("\nКлетка занята!"); Console.Write("Для продолжения нажмите любую клавишу.."); Console.ReadKey(); break; // нажата клавиша enter

                    default: this.ShowField(this.currentRow, this.currentColumn, 'X'); break;
                }
            }
            while (true);
        }


        //////


        private void BotMove()
        {
            do
            {
                Console.Clear(); Console.WriteLine("КРЕСТИКИ-НОЛИКИ (ХОД БОТА)");

                this.currentRow = (byte) this.random.Next(0, this.maxPosition);
                this.currentColumn = (byte) this.random.Next(0, this.maxPosition);
            }
            while (!this.EditField(this.currentRow, this.currentColumn, 'O'));

            this.ShowField(this.currentRow, this.currentColumn, 'O');
            Console.Write("\nДля продолжения нажмите любую стрелочку..");
        }


        //////


        private bool FindWinner()
        {
            ++this.count;

            byte countForWin = 0;

            for (int i = 0; i < this.fieldSize; ++i)
            {
                for (int j = 0; j < this.fieldSize - 1; ++j)
                {
                    if (this.field[i, j] == this.field[i, j + 1] && this.field[i, j] != ' ')
                    {
                        ++countForWin;
                    }
                }

                if (countForWin == this.fieldSize - 1)
                {
                    Console.Clear();
                    Console.WriteLine("КРЕСТИКИ-НОЛИКИ");
                    this.ShowField();
                    Console.Write("\nВЫИГРАЛО ЧТО-ТО ПО ГОРИЗОНТАЛИ");

                    return true;
                }
                countForWin = 0;
            }


            for (int j = 0; j < this.fieldSize; ++j)
            {
                for (int i = 0; i < this.fieldSize - 1; ++i)
                {
                    if (this.field[i, j] == this.field[i + 1, j] && this.field[i, j] != ' ')
                    {
                        ++countForWin;
                    }
                }

                if (countForWin == this.fieldSize - 1)
                {
                    Console.Clear();
                    Console.WriteLine("КРЕСТИКИ-НОЛИКИ");
                    this.ShowField();
                    Console.Write("\nВЫИГРАЛО ЧТО-ТО ПО ВЕРТИКАЛИ");

                    return true;
                }
                countForWin = 0;
            }



            for (int i = 0; i < this.fieldSize - 1; ++i)
            {
                if (this.field[i, i] == this.field[i + 1, i + 1] && this.field[i, i] != ' ')
                {
                    ++countForWin;
                }
            }

            if (countForWin == this.fieldSize - 1)
            {
                Console.Clear();
                Console.WriteLine("КРЕСТИКИ-НОЛИКИ");
                this.ShowField();
                Console.Write("\nВЫИГРАЛО ЧТО-ТО КОСОЕ 1");

                return true;
            }
            countForWin = 0;



            for (int i = 0; i < this.fieldSize - 1; ++i)
            {
                if (this.field[i, this.fieldSize - i - 1] == this.field[i + 1, this.fieldSize - i - 2] && this.field[i, this.fieldSize - i - 1] != ' ')
                {
                    ++countForWin;
                }
            }


            if (countForWin == this.fieldSize - 1)
            {
                Console.Clear();
                Console.WriteLine("КРЕСТИКИ-НОЛИКИ");
                this.ShowField();
                Console.Write("\nВЫИГРАЛО ЧТО-ТО КОСОЕ 2");

                return true;
            }


            //if ((this.field[0, 0] == 'X' && this.field[0, 1] == 'X' && this.field[0, 2] == 'X') || // если выиграл игрок
            //    (this.field[1, 0] == 'X' && this.field[1, 1] == 'X' && this.field[1, 2] == 'X') ||
            //    (this.field[2, 0] == 'X' && this.field[2, 1] == 'X' && this.field[2, 2] == 'X') ||

            //    (this.field[0, 0] == 'X' && this.field[1, 0] == 'X' && this.field[2, 0] == 'X') ||
            //    (this.field[0, 1] == 'X' && this.field[1, 1] == 'X' && this.field[2, 1] == 'X') ||
            //    (this.field[0, 2] == 'X' && this.field[1, 2] == 'X' && this.field[2, 2] == 'X') ||

            //    (this.field[0, 0] == 'X' && this.field[1, 1] == 'X' && this.field[2, 2] == 'X') ||
            //    (this.field[2, 0] == 'X' && this.field[1, 1] == 'X' && this.field[0, 2] == 'X'))
            //{
            //    Console.Clear();
            //    Console.WriteLine("КРЕСТИКИ-НОЛИКИ");
            //    this.ShowField();
            //    Console.Write("\nИгрок выиграл!");

            //    return true;
            //}


            //if ((this.field[0, 0] == 'O' && this.field[0, 1] == 'O' && this.field[0, 2] == 'O') || // если выиграл бот
            //    (this.field[1, 0] == 'O' && this.field[1, 1] == 'O' && this.field[1, 2] == 'O') ||
            //    (this.field[2, 0] == 'O' && this.field[2, 1] == 'O' && this.field[2, 2] == 'O') ||

            //    (this.field[0, 0] == 'O' && this.field[1, 0] == 'O' && this.field[2, 0] == 'O') ||
            //    (this.field[0, 1] == 'O' && this.field[1, 1] == 'O' && this.field[2, 1] == 'O') ||
            //    (this.field[0, 2] == 'O' && this.field[1, 2] == 'O' && this.field[2, 2] == 'O') ||

            //    (this.field[0, 0] == 'O' && this.field[1, 1] == 'O' && this.field[2, 2] == 'O') ||
            //    (this.field[2, 0] == 'O' && this.field[1, 1] == 'O' && this.field[0, 2] == 'O'))
            //{
            //    Console.Clear();
            //    Console.WriteLine("КРЕСТИКИ-НОЛИКИ");
            //    this.ShowField();
            //    Console.Write("\nБот выиграл!");

            //    return true;
            //}


            //if (this.count == this.fieldSize * this.fieldSize) // если ничья
            //{
            //    Console.Clear();
            //    Console.Write("КРЕСТИКИ-НОЛИКИ\n");
            //    this.ShowField();
            //    Console.Write("\nНичья!");

            //    return true;
            //}

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
    }
}

/*
 * УНИВЕРСАЛЬНОСТЬ В FindWinner() РАБОТАЕТ, ТОЛЬКО ЕЁ НУЖНО ГРАМОТНЕЕ ОФОРМИТЬ
 * ТАКЖЕ СТОИТ ДОБАИТЬ ВЫБОР ЗА КОГО ИГРАТЬ
 * 
 * 
0 0  0 1  0 2  0 3  0 4

1 0  1 1  1 2  1 3  1 4

2 0  2 1  2 2  2 3  2 4

3 0  3 1  3 2  3 3  3 4

4 0  4 1  4 2  4 3  4 4
*/

