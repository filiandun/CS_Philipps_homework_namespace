namespace GameWithBot
{
    public class NaughtsAndCrosses
    {
        private ushort fieldSize;
        private char[,] field;

        public NaughtsAndCrosses() 
        {
            this.fieldSize = 3;
            this.field = new char[fieldSize, fieldSize];

            for (int i = 0; i < fieldSize; ++i)
            {
                for (int j = 0; j < fieldSize; ++j)
                {
                    this.field[i, j] = ' ';
                }
            }
        }

        public void Playing()
        {
            Console.WriteLine("X ИГРА КРЕСТИКИ-НОЛИКИ 0");
            Console.WriteLine("Выберите сторону, за которую будете играть: ");
            Console.WriteLine("За крестики (X)");
            Console.WriteLine("За нолики (O)\n");

            char player = 'X';
            char bot = 'O';
            int temp = 1;

            while (!FindWinner())
            {
                //Console.Clear();
                this.ShowField();

                Console.Write("\nВаш ход: ");
                temp = Convert.ToInt32(Console.ReadLine());

                while (!this.EditField(temp, player))
                {
                    Console.Write("Эта клетка занята, ваш ход: ");
                    temp = Convert.ToInt32(Console.ReadLine());
                }

                Random random = new Random();
                temp = random.Next(0, 8);
                while (!this.EditField(temp, bot))
                {
                    temp = random.Next(0, 8);
                }
                Console.Write($"Ход противника: {temp}\n");
            }
        }

        private bool FindWinner()
        {
            if ((this.field[0, 0] == 'X' && this.field[0, 1] == 'X' && this.field[0, 2] == 'X') ||
                (this.field[1, 0] == 'X' && this.field[1, 1] == 'X' && this.field[1, 2] == 'X') ||
                (this.field[2, 0] == 'X' && this.field[2, 1] == 'X' && this.field[2, 2] == 'X') ||

                (this.field[0, 0] == 'X' && this.field[1, 0] == 'X' && this.field[2, 0] == 'X') ||
                (this.field[0, 1] == 'X' && this.field[1, 1] == 'X' && this.field[2, 1] == 'X') ||
                (this.field[0, 2] == 'X' && this.field[1, 2] == 'X' && this.field[2, 2] == 'X') ||

                (this.field[0, 0] == 'X' && this.field[1, 1] == 'X' && this.field[2, 2] == 'X') ||
                (this.field[0, 2] == 'X' && this.field[1, 1] == 'X' && this.field[2, 0] == 'X'))
            {
                this.ShowField();
                Console.Write("\nВы выиграли!");
                return true;
            }

            if ((this.field[0, 0] == 'O' && this.field[0, 1] == 'O' && this.field[0, 2] == 'O') ||
                (this.field[1, 0] == 'O' && this.field[1, 1] == 'O' && this.field[1, 2] == 'O') ||
                (this.field[2, 0] == 'O' && this.field[2, 1] == 'O' && this.field[2, 2] == 'O') ||

                (this.field[0, 0] == 'O' && this.field[1, 0] == 'O' && this.field[2, 0] == 'O') ||
                (this.field[0, 1] == 'O' && this.field[1, 1] == 'O' && this.field[2, 1] == 'O') ||
                (this.field[0, 2] == 'O' && this.field[1, 2] == 'O' && this.field[2, 2] == 'O') ||

                (this.field[0, 0] == 'O' && this.field[1, 1] == 'O' && this.field[2, 2] == 'O') ||
                (this.field[0, 2] == 'O' && this.field[1, 1] == 'O' && this.field[2, 0] == 'O'))
            {
                this.ShowField();
                Console.Write("\nВы проиграли!");
                return true;
            }

            return false;
        }

        private bool EditField(int position, char who)
        {
            switch (position)
            {
                case 1: if (this.field[0, 0] == ' ') { this.field[0, 0] = who; return true; } return false;
                case 2: if (this.field[0, 1] == ' ') { this.field[0, 1] = who; return true; } return false;
                case 3: if (this.field[0, 2] == ' ') { this.field[0, 2] = who; return true; } return false;

                case 4: if (this.field[1, 0] == ' ') { this.field[1, 0] = who; return true; } return false;
                case 5: if (this.field[1, 1] == ' ') { this.field[1, 1] = who; return true; } return false;
                case 6: if (this.field[1, 2] == ' ') { this.field[1, 2] = who; return true; } return false;

                case 7: if (this.field[2, 0] == ' ') { this.field[2, 0] = who; return true; } return false;
                case 8: if (this.field[2, 1] == ' ') { this.field[2, 1] = who; return true; } return false;
                case 9: if (this.field[2, 2] == ' ') { this.field[2, 2] = who; return true; } return false;

                default: return false;
            }  
        }

        private void ShowField()
        {
            Console.WriteLine();
            for (int i = 0; i < this.fieldSize; ++i) 
            {
                for (int j = 0; j < this.fieldSize; ++j)
                {
                    Console.Write($" {this.field[i, j]}");

                    if (j < this.fieldSize - 1)
                    {
                        Console.Write(" |");
                    }
                }

                if (i < this.fieldSize - 1) 
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
  0 | 0 | 0
 ---|---|---
  0 | 0 | 0
 ---|---|---
  0 | 0 | 0

*/
