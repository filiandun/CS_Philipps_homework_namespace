﻿/*
* Задание 1
Создайте приложение «Крестики-Нолики». Пользователь играет с компьютером. 
При старте игры случайным образом выбирается, кто ходит первым. 
Игроки ходят по очереди. Игра может закончиться победой одного из игроков или ничьей. 
Используйте механизмы пространств имён.

* Задание 2
Добавьте к первому заданию возможность игры с другим пользователем.

* Задание 3
Создайте приложение для перевода обычного текста в азбуку Морзе. 
Пользователь вводит текст. Приложение отображает введенный текст азбукой Морзе. 
Используйте механизмы пространств имён.

* Задание 4
Добавьте к предыдущему заданию механизм перевода текста из азбуки Морзе в обычный текст.
*/


namespace namespaces 
{
    internal class Program
    {
        static void Main()
        {
            GameWithBot.NaughtsAndCrosses naughtsAndCrosses = new GameWithBot.NaughtsAndCrosses();
            naughtsAndCrosses.Playing();
            //naughtsAndCrosses.ShowField(1, 0);
        }
    }
}