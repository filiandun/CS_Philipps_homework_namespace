using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace GameWithBot
{
    public class NaughtsAndCrosses
    {
        private ushort currentRow;
        private ushort currentColumn;
        private ushort maxPosition;

        int tempCount = 0;

        private bool Player()
        {
            ConsoleKeyInfo key;
            ++this.tempCount;

            while (true)
            {
                key = Console.ReadKey(); // считывание значения нажатой клавиши

                Console.Clear(); Console.WriteLine("КРЕСТИКИ-НОЛИКИ (ВАШ ХОД)");
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow: if (this.currentRow - 1 >= 0) { --this.currentRow; }; this.ShowField((short) this.currentRow, (short) this.currentColumn, 'X'); break; // нажата клавиша вверх
                    case ConsoleKey.DownArrow: if (this.currentRow + 1 < this.maxPosition) { ++this.currentRow; }; this.ShowField((short)this.currentRow, (short)this.currentColumn, 'X'); break; // нажата клавиша вниз
                    case ConsoleKey.RightArrow: if (this.currentColumn + 1 < this.maxPosition) { ++this.currentColumn; }; this.ShowField((short)this.currentRow, (short)this.currentColumn, 'X'); break; // нажата клавиша вправо
                    case ConsoleKey.LeftArrow: if (this.currentColumn - 1 >= 0) { --this.currentColumn; }; this.ShowField((short)this.currentRow, (short)this.currentColumn, 'X'); break; // нажата клавиша влево
                    case ConsoleKey.Enter: if (this.EditField(this.currentRow, this.currentColumn, 'X')) { return true; } return false; // нажата клавиша enter

                    default: this.ShowField((short)this.currentRow, (short)this.currentColumn, 'X'); break;
                }
            }
        }


        private bool Bot()
        {
            do
            {
                Console.Clear(); Console.WriteLine("КРЕСТИКИ-НОЛИКИ (ХОД БОТА)");

                this.currentRow = (ushort) this.random.Next(0, this.maxPosition);
                this.currentColumn = (ushort) this.random.Next(0, this.maxPosition);
            }
            while (!this.EditField(this.currentRow, this.currentColumn, 'O'));

            this.ShowField((short) this.currentRow, (short) this.currentColumn, 'O');
            Console.Write("\nДля продолжения нажмите любую стрелочку..");

            return true;
        }


        Random random;

        private ushort fieldSize;
        private char[,] field;

        private byte count;

        public NaughtsAndCrosses() 
        {
            this.random = new Random();
            this.fieldSize = 3;
            this.currentRow = 0;
            this.currentColumn = 0;
            this.maxPosition = this.fieldSize;
            this.count = 0;

            this.field = new char[this.fieldSize, this.fieldSize];
            for (int i = 0; i < this.fieldSize; ++i)
            {
                for (int j = 0; j < this.fieldSize; ++j)
                {
                    this.field[i, j] = ' ';
                }
            }
        }


        public void Playing()
        {
            Console.WriteLine("КРЕСТИКИ-НОЛИКИ\n");

            Console.WriteLine("* вы играете за крестики (X)");
            Console.WriteLine("* ваш компьюетрный соперник за нолики (O)\n");

            byte whoFirst = 1;//(byte) this.random.Next(0, 2);
            Console.WriteLine($"* методом рандома было решено, что {(whoFirst == 0 ? "вы ходите первым" : "бот ходит первым")}");
            Console.WriteLine("* упрвление реализовано стрелочками и клавишей enter\n");

            Console.Write("Для продолженния нажмите любую стрелочку.."); Console.ReadKey();

            if (whoFirst == 0) 
            {       
                for (int i = 0; i < 9; ++i)
                {
                    while (!this.Player()) { this.ShowField((short)this.currentRow, (short)this.currentColumn, 'X'); Console.WriteLine("\nКлетка занята!"); Console.Write("Для продолжения нажмите любую стрелочку.."); }
                    if (this.FindWinner() == true) { return; }

                    while (!this.Bot()) { }
                    if (this.FindWinner() == true) { return; }
                }
            }

            if (whoFirst == 1)
            {
                for (int i = 0; i < 9; ++i)
                {
                    while (!this.Bot()) { }
                    if (this.FindWinner() == true) { return; }

                    while (!this.Player()) { this.ShowField((short) this.currentRow, (short) this.currentColumn, 'O');  Console.WriteLine("\nКлетка занята!"); Console.Write("Для продолжения нажмите любую стрелочку.."); }
                    if (this.FindWinner() == true) { return; }
                }
            }

            Console.WriteLine("\nНичья!");
            return;
        }


        private bool FindWinner()
        {
            ++this.count;

            if ((this.field[0, 0] == 'X' && this.field[0, 1] == 'X' && this.field[0, 2] == 'X') ||
                (this.field[1, 0] == 'X' && this.field[1, 1] == 'X' && this.field[1, 2] == 'X') ||
                (this.field[2, 0] == 'X' && this.field[2, 1] == 'X' && this.field[2, 2] == 'X') ||

                (this.field[0, 0] == 'X' && this.field[1, 0] == 'X' && this.field[2, 0] == 'X') ||
                (this.field[0, 1] == 'X' && this.field[1, 1] == 'X' && this.field[2, 1] == 'X') ||
                (this.field[0, 2] == 'X' && this.field[1, 2] == 'X' && this.field[2, 2] == 'X') ||

                (this.field[0, 0] == 'X' && this.field[1, 1] == 'X' && this.field[2, 2] == 'X') ||
                (this.field[2, 0] == 'X' && this.field[1, 1] == 'X' && this.field[0, 2] == 'X'))
            {
                Console.Clear();
                Console.WriteLine("КРЕСТИКИ-НОЛИКИ");
                this.ShowField();
                Console.Write("\nИгрок выиграл!");

                Console.Write($"\n\n{tempCount}\n\n");

                return true;
            }


            if ((this.field[0, 0] == 'O' && this.field[0, 1] == 'O' && this.field[0, 2] == 'O') ||
                (this.field[1, 0] == 'O' && this.field[1, 1] == 'O' && this.field[1, 2] == 'O') ||
                (this.field[2, 0] == 'O' && this.field[2, 1] == 'O' && this.field[2, 2] == 'O') ||

                (this.field[0, 0] == 'O' && this.field[1, 0] == 'O' && this.field[2, 0] == 'O') ||
                (this.field[0, 1] == 'O' && this.field[1, 1] == 'O' && this.field[2, 1] == 'O') ||
                (this.field[0, 2] == 'O' && this.field[1, 2] == 'O' && this.field[2, 2] == 'O') ||

                (this.field[0, 0] == 'O' && this.field[1, 1] == 'O' && this.field[2, 2] == 'O') ||
                (this.field[2, 0] == 'O' && this.field[1, 1] == 'O' && this.field[0, 2] == 'O'))
            {
                Console.Clear();
                Console.WriteLine("КРЕСТИКИ-НОЛИКИ");
                this.ShowField();
                Console.Write("\nБот выиграл!");

                Console.Write($"\n\n{tempCount}\n\n");

                return true;
            }

            if (this.count == this.fieldSize * this.fieldSize)
            {
                Console.Clear();
                Console.Write("КРЕСТИКИ-НОЛИКИ\n");
                this.ShowField();
                Console.Write("\nНичья!");

                Console.Write($"\n\n{tempCount}\n\n");

                return true;
            }

            return false;
        }


        public bool EditField(ushort row, ushort column, char who)
        {
            if (this.field[row, column] == ' ')
            {
                this.field[row, column] = who;
                return true;
            }
            return false;
        }


        public void ShowField(short row = -1, short column = -1, char who = 'X')
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
  0 0 | 0 1 | 0 2
  --- | --- | ---
  1 0 | 1 1 | 1 2
  --- | --- |---
  2 0 | 2 1 | 2 2

*/
