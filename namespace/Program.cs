/*
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




#define NAUGHTSANDCROSSESWITHBOT // просто раскомментируйте/закомментируйте
//#define NAUGHTSANDCROSSESWITHFRIEND // просто раскомментируйте/закомментируйте
//#define TOMORSECODE // просто раскомментируйте/закомментируйте
//#define FROMMORSECODE // просто раскомментируйте/закомментируйте




namespace namespaces 
{
    internal class Program
    {
        static void Main()
        {

#if NAUGHTSANDCROSSESWITHBOT
            /* КРЕСТИКИ-НОЛИКИ C БОТОМ */

            GameWithBot.NaughtsAndCrosses naughtsAndCrossesWithBot = new GameWithBot.NaughtsAndCrosses(); // игра с ботом (рандомом)
            naughtsAndCrossesWithBot.Playing();
#endif





#if NAUGHTSANDCROSSESWITHFRIEND
            /* КРЕСТИКИ-НОЛИКИ C ДРУГОМ */

            GameWithFriend.NaughtsAndCrosses naughtsAndCrossesWithFriend = new GameWithFriend.NaughtsAndCrosses(); // игра с другом
            naughtsAndCrossesWithFriend.Playing();
#endif





#if TOMORSECODE
            /* АЗБУКА МОРЗЕ */

            // ВАЖНО!!!! Я ИСПОЛЬЗОВАЛ ЭТОТ САЙТ С ОНЛАЙН ПЕРЕВОДЧИКОМ ЭТОЙ АЗБУКИ: https://morsedecoder.com/ru/


            /* ЗАГОТОВЛЕННЫЕ СТРОКИ */

            string text = "Съешь же ещё этих мягких французских булок да выпей чаю 0123456789 !\"$&\'()+,-./:;=?@";
            //string text = "      \t\n\n\t     Съешь же ещё этих мягких французских булок да выпей чаю 0123456789 !\"$&\'()+,-./:;=?@    \t\t\n\n    ";
            string invalidText = "~Съешь ж{е ещё этих мягких* французских бул]ок да выпей _чаю 0123456789 !\"#$%&'()*+,-./:;<=>?@";

            string rus = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            string eng = "abcdefghijklmnopqrstuvwxyz";

            string digit = "0123456789";
            string invalidPunct = "!\"#$%&'()*+,-./:;<=>?@";
            string validPunct = "!\"$&\'()+,-./:;=?@";


            /* ТУТ ПРОСТО ВЫВОД ASCII-КОДОВ СИМВОЛОВ, МОЖЕТ ДЛЯ ПРОВЕРКИ ПОНАДОБИТСЯ */

            //Console.WriteLine("ASCII букв из кириллицы: ");
            //foreach (char c in rus) { Console.WriteLine($"{(char)c} = {(int)c}"); }
            //Console.WriteLine($"{(char)1104}"); // загадочный 1104 символ: у кириллицы символы имеют код от 1072 до 1105 (ё), а тут на 1104 место фигню какую-то добавили
            //Console.WriteLine("\n\n");

            //Console.WriteLine("ASCII букв из латиницы: ");
            //foreach (char c in eng) { Console.WriteLine($"{(char)c} = {(int)c}"); }
            //Console.WriteLine("\n\n");

            //Console.WriteLine("ASCII чисел: ");
            //foreach (char c in digit) { Console.WriteLine($"{(char)c} = {(int)c}"); }
            //Console.WriteLine("\n\n");

            //Console.WriteLine("ASCII пунктуации: ");
            //foreach (char c in invalidPunct) { Console.WriteLine($"{(char)c} = {(int)c}"); }
            //Console.WriteLine("\n\n");


            /* ДЕМОНСТРАЦИЯ */

            //Console.WriteLine($"Изначальная строка: {validPunct}");
            //Console.WriteLine($"Строка в морзе: {ConvertTo.MorseCode.FromString(validPunct)}\n\n");

            //Console.WriteLine($"Изначальная строка: {invalidPunct}");
            //Console.WriteLine($"Строка в морзе: {ConvertTo.MorseCode.FromString(invalidPunct)}\n\n");

            //Console.WriteLine($"Изначальная строка: {digit}");
            //Console.WriteLine($"Строка в морзе: {ConvertTo.MorseCode.FromString(digit)}\n\n");

            //Console.WriteLine($"Изначальная строка: {rus}");
            //Console.WriteLine($"Строка в морзе: {ConvertTo.MorseCode.FromString(rus)}\n\n");

            //Console.WriteLine($"Изначальная строка: {eng}");
            //Console.WriteLine($"Строка в морзе: {ConvertTo.MorseCode.FromString(eng)}\n\n");

            Console.WriteLine($"Изначальная строка: {text}");
            Console.WriteLine($"Строка в морзе без опций: {ConvertTo.MorseCode.FromString(text)}\n\n"); // без использования опций

            Console.WriteLine($"Строка в морзе c опцией, что нужно удалить все знаки препинания: {ConvertTo.MorseCode.FromString(text, ConvertTo.MorseCode.MorseCodeOptions.DeletePunctuations)}\n\n"); // с использованием одной опции
            Console.WriteLine($"Строка в морзе c опцией, что нужно удалить все числа: {ConvertTo.MorseCode.FromString(text, ConvertTo.MorseCode.MorseCodeOptions.DeleteDigits)}\n\n"); // с использованием одной опции
            Console.WriteLine($"Строка в морзе c опцией, что нужно удалить все буквы: {ConvertTo.MorseCode.FromString(text, ConvertTo.MorseCode.MorseCodeOptions.DeleteLetters)}\n\n"); // с использованием одной опции

            Console.WriteLine($"Некорректная строка в морзе: {ConvertTo.MorseCode.FromString(invalidText)}\n\n"); // некорректная строка без опций
            Console.WriteLine($"Некорректная строка в морзе c опцией, что нужно игнорировать все некорректные символы: {ConvertTo.MorseCode.FromString(invalidText, ConvertTo.MorseCode.MorseCodeOptions.IgnoreInvalidSymbols)}\n\n"); // некорректная строка c опцией

            Console.WriteLine($"Строка в морзе c опциями, что нужно удалить все буквы и цифры: {ConvertTo.MorseCode.FromString(text, ConvertTo.MorseCode.MorseCodeOptions.DeleteDigits, ConvertTo.MorseCode.MorseCodeOptions.DeleteLetters)}\n\n"); // с использованием двух опций
            Console.WriteLine($"Некорректная строка в морзе c опциями, что нужно удалить все цифры и проигнорировать некорректные символы: {ConvertTo.MorseCode.FromString(invalidText, ConvertTo.MorseCode.MorseCodeOptions.DeleteDigits, ConvertTo.MorseCode.MorseCodeOptions.IgnoreInvalidSymbols)}\n\n"); // с использованием двух опций
#endif





#if FROMMORSECODE
            /* ЗАГОТОВЛЕННЫЕ СТРОКИ */

            string text = "... --.-- . ---- -..- / ...- . / . --.- . / ..-.. - .. .... / -- .-.- --. -.- .. .... / ..-. .-. .- -. -.-. ..- --.. ... -.- .. .... / -... ..- .-.. --- -.- / -.. .- / .-- -.-- .--. . .--- / ---. .- ..-- / ----- .---- ..--- ...-- ....- ..... -.... --... ---.. ----. / -.-.-- .-..-. ...-..- .-... .----. -.--. -.--.- .-.-. --..-- -....- .-.-.- -..-. ---... -.-.-. -...- ..--.. .--.-.";
            //string text = "  //  \t\n/\n\t   //  ... --.-- . ---- -..- / ...- . / . --.- . / ..-.. - .. .... / -- .-.- --. -.- .. .... / ..-. .-. .- -. -.-. ..- --.. ... -.- .. .... / -... ..- .-.. --- -.- / -.. .- / .-- -.-- .--. . .--- / ---. .- ..-- / ----- .---- ..--- ...-- ....- ..... -.... --... ---.. ----. / -.-.-- .-..-. ...-..- .-... .----. -.--. -.--.- .-.-. --..-- -....- .-.-.- -..-. ---... -.-.-. -...- ..--.. .--.-.  //  \t\t/\n\n  /  ";
            string invalidText = "~... --.-*- . ---- -..- /fdhdfhd ...- . / . --.- . / ..44-.. - .. .... / -- .-.- --. -sdgfdsgsd.- .. .... / ..-. .-. .- -. -.-. ..- --.. ... esa-.- .. .... / -... ..#- .-.. --- -.- / -.. .- / .-- -.-- .--. . .--- / ---. .- ..-- / ---58-56-- .---- ..--- ...-- ...8gfd.- ..///... -.... --... ---...---.. ----. / -.-.-- .-22..-. ...-..- .-... .----. -.--. -.--.- .-.-. --..-- -..13..- .-.-.- -..-. ---... -.-.-. -...- ..--.. .--.-.";

            string rus = ".- -... .-- --. -.. . ...- --.. .. .--- -.- .-.. -- -. --- .--. .-. ... - ..- ..-. .... -.-. ---. ---- --.- --.-- -.-- -..- ..-.. ..-- .-.-";
            string eng = ".- -... -.-. -.. . ..-. --. .... .. .--- -.- .-.. -- -. --- .--. --.- .-. ... - ..- ...- .-- -..- -.-- --..";

            string digit = "----- .---- ..--- ...-- ....- ..... -.... --... ---.. ----.";
            string punct = "-.-.-- .-..-. ...-..- .-... .----. -.--. -.--.- .-.-. --..-- -....- .-.-.- -..-. ---... -.-.-. -...- ..--.. .--.-.";


            /* ДЕМОНСТРАЦИЯ */

            Console.WriteLine($"Строка (text) из морзе: {ConvertFrom.MorseCode.ToRusString(text)}");
            Console.WriteLine($"Строка (invalidText) из морзе: {ConvertFrom.MorseCode.ToRusString(invalidText)}");
            Console.WriteLine($"Строка (invalidText) из морзе c опцией: {ConvertFrom.MorseCode.ToRusString(invalidText, ConvertFrom.MorseCode.MorseCodeOptions.IgnoreInvalidSymbols)}");

            Console.WriteLine($"Строка (rus) из морзе: {ConvertFrom.MorseCode.ToRusString(rus)}");
            Console.WriteLine($"Строка (eng) из морзе: {ConvertFrom.MorseCode.ToEngString(eng)}");

            Console.WriteLine($"Строка (digit) из морзе: {ConvertFrom.MorseCode.ToRusString(digit)}");
            Console.WriteLine($"Строка (punct) из морзе: {ConvertFrom.MorseCode.ToEngString(punct)}");
#endif
        }
    }
}