//using GameWithBot;

//namespace Menu
//{
//    internal class Menu
//    {
//        private ushort currentPosition;
//        private ushort maxPosition;

//        public NaughtsAndCrosses naughtsAndCrosses;

//        public Menu()
//        {
//	        this.currentPosition = 0;
//	        this.maxPosition = 9;

//            this.naughtsAndCrosses = new NaughtsAndCrosses();
//        }

//        public void DoIt(ushort currentPosition)
//        {
//            switch (currentPosition)
//            {
//                case 0: this.naughtsAndCrosses.EditField(currentPosition, 'X'); this.ShowMenu(); break;
//                case 2: return; //exit(0);
//            }
//        }

//        public void Choice()
//        {
//            ConsoleKeyInfo key;
//            while (true)
//            {
//                key = Console.ReadKey(); // считывание значения нажатой клавиши

//                switch (key.Key)
//                {
//                    case ConsoleKey.UpArrow: if (this.currentPosition - 1 >= 0) { --this.currentPosition; }; this.ShowMenu(); return; // нажата клавиша вверх
//                    case ConsoleKey.DownArrow: if (this.currentPosition + 1 < this.currentPosition) { ++this.currentPosition; }; this.ShowMenu(); return; // нажата клавиша вниз
//                    case ConsoleKey.RightArrow: if (this.currentPosition + 1 < this.currentPosition) { ++this.currentPosition; }; this.ShowMenu(); return; // нажата клавиша вправо
//                    case ConsoleKey.LeftArrow: if (this.currentPosition + 1 < this.currentPosition) { ++this.currentPosition; }; this.ShowMenu(); return; // нажата клавиша влево
//                    case ConsoleKey.Enter: this.DoIt(this.currentPosition); return; // нажата клавиша enter
        
//                    default: break;
//                }
//            }
//        }

//        public void ShowMenu()
//        {
//            if (this.currentPosition == 0)
//            {
//                Console.Clear();
//                //std::cout << "ЗДРАВСТВУЙТЕ, " << user->fio << ", ВЫБЕРИТЕ, ЧТО ВЫ ХОТИТЕ СДЕЛАТЬ: " << std::endl;
//                //std::cout << "> пройти тестирование" << std::endl; this->set_color(15, 0);
//                //std::cout << "  посмотреть результаты прошлых тестирований" << std::endl << std::endl;

//                //std::cout << "  выйти" << std::endl;

//                this.Choice();
//            }
//        }
//    }
//}
