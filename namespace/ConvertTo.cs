using System.Text.RegularExpressions;


namespace ConvertTo
{
    public static class MorseCode
    {
        public enum MorseCodeOptions
        { 
            None = 0,
            DeletePunctuations = 1,
            DeleteDigits = 2,
            DeleteLetters = 3,
            IgnoreInvalidSymbols = 4,
        }


        //////


        private static string[] codeMorseRusLetters = new string[] 
        { 
            /*Аа*/ ".-", 
            /*Бб*/ "-...", 
            /*Вв*/ ".--", 
            /*Гг*/ "--.", 
            /*Дд*/ "-..", 
            /*Ее и Ёё*/ ".", 
            /*Жж*/ "...-", 
                                                                    
            /*Зз*/ "--..", 
            /*Ии*/ "..", 
            /*Йй*/ ".---", 
            /*Кк*/ "-.-", 
            /*Лл*/ ".-..", 
            /*Мм*/ "--",
                                                                    
            /*Нн*/ "-.", 
            /*Оо*/ "---", 
            /*Пп*/ ".--.", 
            /*Рр*/ ".-.", 
            /*Сс*/ "...",
            /*Тт*/ "-", 
            /*Уу*/ "..-", 
                                                                    
            /*Фф*/ "..-.", 
            /*Хх*/ "....",
            /*Цц*/  "-.-.", 
            /*Чч*/ "---.", 
            /*Шш*/ "----", 
            /*Щщ*/ "--.-", 
                                                                    
            /*Ъъ*/ "--.--", 
            /*Ыы*/ "-.--", 
            /*Ьь*/ "-..-", 
            /*Ээ*/ "..-..", 
            /*Юю*/ "..--", 
            /*Яя*/ ".-.-"
        };
        private static string[] codeMorseEngLetters = new string[]
        { 
            /*Aa*/ ".-", 
            /*Bb*/ "-...", 
            /*Cc*/ "-.-.", 
            /*Dd*/ "-..", 
            /*Ee*/ ".", 

            /*Ff*/ "..-.", 
            /*Gg*/ "--.", 
            /*Hh*/ "....", 
            /*Ii*/ "..", 
            /*Jj*/ ".---", 

            /*Kk*/ "-.-", 
            /*Ll*/ ".-..", 
            /*Mm*/ "--", 
            /*Nn*/ "-.", 
            /*Oo*/ "---", 
            /*Pp*/ ".--.", 

            /*Qq*/ "--.-", 
            /*Rr*/ ".-.", 
            /*Ss*/ "...", 
            /*Tt*/ "-", 
            /*Uu*/ "..-", 

            /*Vv*/ "...-", 
            /*Ww*/ ".--", 
            /*Xx*/ "-..-", 
            /*Yy*/ "-.--", 
            /*Zz*/ "--.."
        };
        private static string[] codeMorseDigit = new string[] 
        { 
            /*0*/ "-----", 
            /*1*/ ".----", 
            /*2*/ "..---", 
            /*3*/ "...--", 
            /*4*/ "....-",
            /*5*/ ".....", 
            /*6*/ "-....", 
            /*7*/ "--...",
            /*8*/ "---..", 
            /*9*/ "----." 
        };
        private static string[] codeMorsePunctuation = new string[] 
        { 
            /*!*/ "-.-.--", 
            /*"*/ ".-..-.", 
            /*#*/ "\0", 
            /*$*/ "...-..-", 
            /*%*/ "\0",
            /*&*/ ".-...", 
            /*'*/ ".----.", 

            /*(*/ "-.--.", 
            /*)*/ "-.--.-", 
            /***/ "\0", 
            /*+*/ ".-.-.", 
            /*,*/ "--..--",
            /*-*/ "-....-", 
            /*.*/ ".-.-.-", 

            /*/*/ "-..-.", 
            /*Числа от 0 до 9*/ "\0", "\0", "\0", "\0", "\0", "\0", "\0", "\0", "\0", "\0",
            /*:*/ "---...", 
            /*;*/ "-.-.-.", 
            /*<*/ "\0", 
            /*=*/ "-...-", 
            /*>*/ "\0", 
            /*?*/ "..--..", 
            /*@*/ ".--.-."
        };


        //////


        public static string FromString(string text)
        {
            text = text.Trim(' ', '\n', '\t').ToLower(); // Trim - чтобы убрать лишние (символы) в начале и в конце строки, а ToLower() - чтобы перенести все буквы в нижний регистр
            string morseText = new string('\0', 6 * text.Length); // выделение максимума места

            bool space = false; // чтобы не было повторяющихся пробелов, а также, чтобы не было пробелов в начале строки


            for (int i = 0; i < text.Length; ++i)
            {
                if (text[i] >= 1072 && text[i] <= 1105 && text[i] != 1104) // если символ из кириллицы (1104 игнорируется, так как он не буква)
                {
                    if (text[i] == 'ё')
                    {
                        morseText += ". ";
                        space = true;

                        continue;
                    }

                    if (text[i] == 'ъ')
                    {
                        morseText += "-..- ";
                        space = true;

                        continue;
                    }

                    morseText += codeMorseRusLetters[text[i] - 1072] + ' ';
                    space = true;

                    continue;
                }

                else if (text[i] >= 97 && text[i] <= 122) // если символ из латиницы
                {
                    morseText += codeMorseEngLetters[text[i] - 97] + ' ';
                    space = true;

                    continue;
                }

                else if (text[i] >= 48 && text[i] <= 57) // если символ - число
                {
                    morseText += codeMorseDigit[text[i] - 48] + ' ';
                    space = true;

                    continue;
                }

                else if ((text[i] >= 33 && text[i] < 48 || text[i] > 57 && text[i] <= 64) && (text[i] != 35 && text[i] != 37 && text[i] != 42 && text[i] != 60 && text[i] != 62)) // если символ - пунктуация
                {
                    morseText += codeMorsePunctuation[text[i] - 33] + ' ';
                    space = true;

                    continue;
                }

                else if (text[i] == ' ') // если символ - пробел
                {
                    if (space == true)
                    {
                        morseText += "/ ";
                        space = false;
                    }

                    continue;
                }

                else
                {
                    return $"ошибка, ваша строка имеет недопустимые символы, первый попавшийся: {text[i]}, чтобы проигнорировать их используйте опцию IgnoreInvalidSymbols";
                }
            }

            return morseText;
        }


        //////


        public static string FromString(string text, MorseCodeOptions option)
        {
            text = text.Trim(' ', '\n', '\t').ToLower(); // Trim - чтобы убрать лишние (символы) в начале и в конце строки, а ToLower() - чтобы перенести все буквы в нижний регистр
            string morseText = new string('\0', 6 * text.Length); // выделение максимума места

            bool space = false; // чтобы не было повторяющихся пробелов, а также, чтобы не было пробелов в начале строки


            if (option == MorseCodeOptions.DeletePunctuations)
            {
                for (int i = 0; i < text.Length; ++i)
                {
                    if ((text[i] >= 1072 && text[i] <= 1105 && text[i] != 1104)) // если символ из кириллицы (1104 игнорируется, так как он не буква)
                    {
                        if (text[i] == 'ё')
                        {
                            morseText += ". ";
                            space = true;

                            continue;
                        }

                        if (text[i] == 'ъ')
                        {
                            morseText += "-..- ";
                            space = true;

                            continue;
                        }

                        morseText += codeMorseRusLetters[text[i] - 1072] + ' ';
                        space = true;

                        continue;
                    }

                    else if (text[i] >= 97 && text[i] <= 122) // если символ из латиницы
                    {
                        morseText += codeMorseEngLetters[text[i] - 97] + ' ';
                        space = true;

                        continue;
                    }

                    else if (text[i] >= 48 && text[i] <= 57) // если символ - число
                    {
                        morseText += codeMorseDigit[text[i] - 48] + ' ';
                        space = true;

                        continue;
                    }

                    else if ((text[i] >= 33 && text[i] < 48 || text[i] > 57 && text[i] <= 64) && (text[i] != 35 && text[i] != 37 && text[i] != 42 && text[i] != 60 && text[i] != 62)) // если символ - пунктуация
                    {
                        continue;
                    }

                    else if (text[i] == ' ') // если символ - пробел
                    {
                        if (space == true)
                        {
                            morseText += "/ ";
                            space = false;
                        }

                        continue;
                    }

                    else
                    {
                        return $"ошибка, ваша строка имеет недопустимые символы, первый попавшийся: {text[i]}, чтобы проигнорировать их используйте опцию IgnoreInvalidSymbols";
                    }
                }
            }


            //////


            if (option == MorseCodeOptions.DeleteDigits)
            {
                for (int i = 0; i < text.Length; ++i)
                {
                    if ((text[i] >= 1072 && text[i] <= 1105 && text[i] != 1104)) // если символ из кириллицы (1104 игнорируется, так как он не буква)
                    {
                        if (text[i] == 'ё')
                        {
                            morseText += ". ";
                            space = true;

                            continue;
                        }

                        if (text[i] == 'ъ')
                        {
                            morseText += "-..- ";
                            space = true;

                            continue;
                        }

                        morseText += codeMorseRusLetters[text[i] - 1072] + ' ';
                        space = true;

                        continue;
                    }

                    else if (text[i] >= 97 && text[i] <= 122) // если символ из латиницы
                    {
                        morseText += codeMorseEngLetters[text[i] - 97] + ' ';
                        space = true;

                        continue;
                    }

                    else if (text[i] >= 48 && text[i] <= 57) // если символ - число
                    {
                        continue;
                    }

                    else if ((text[i] >= 33 && text[i] < 48 || text[i] > 57 && text[i] <= 64) && (text[i] != 35 && text[i] != 37 && text[i] != 42 && text[i] != 60 && text[i] != 62)) // если символ - пунктуация
                    {
                        morseText += codeMorsePunctuation[text[i] - 33] + ' ';
                        space = true;

                        continue;
                    }

                    else if (text[i] == ' ') // если символ - пробел
                    {
                        if (space == true)
                        {
                            morseText += "/ ";
                            space = false;
                        }

                        continue;
                    }

                    else
                    {
                        return $"ошибка, ваша строка имеет недопустимые символы, первый попавшийся: {text[i]}, чтобы проигнорировать их используйте опцию IgnoreInvalidSymbols";
                    }
                }
            }


            //////


            if (option == MorseCodeOptions.DeleteLetters)
            {
                for (int i = 0; i < text.Length; ++i)
                {
                    if ((text[i] >= 1072 && text[i] <= 1105 && text[i] != 1104)) // если символ из кириллицы (1104 игнорируется, так как он не буква)
                    {
                        continue;
                    }

                    else if (text[i] >= 97 && text[i] <= 122) // если символ из латиницы
                    {
                        continue;
                    }

                    else if (text[i] >= 48 && text[i] <= 57) // если символ - число
                    {
                        morseText += codeMorseDigit[text[i] - 48] + ' ';
                        space = true;

                        continue;
                    }

                    else if ((text[i] >= 33 && text[i] < 48 || text[i] > 57 && text[i] <= 64) && (text[i] != 35 && text[i] != 37 && text[i] != 42 && text[i] != 60 && text[i] != 62)) // если символ - пунктуация
                    {
                        morseText += codeMorsePunctuation[text[i] - 33] + ' ';
                        space = true;

                        continue;
                    }

                    else if (text[i] == ' ') // если символ - пробел
                    {
                        if (space == true)
                        {
                            morseText += "/ ";
                            space = false;
                        }

                        continue;
                    }

                    else
                    {
                        return $"ошибка, ваша строка имеет недопустимые символы, первый попавшийся: {text[i]}, чтобы проигнорировать их используйте опцию IgnoreInvalidSymbols";
                    }
                }
            }


            //////


            if (option == MorseCodeOptions.IgnoreInvalidSymbols)
            {
                for (int i = 0; i < text.Length; ++i)
                {
                    if (text[i] >= 1072 && text[i] <= 1105 && text[i] != 1104) // если символ из кириллицы (1104 игнорируется, так как он не буква)
                    {
                        if (text[i] == 'ё')
                        {
                            morseText += ". ";
                            space = true;

                            continue;
                        }

                        if (text[i] == 'ъ')
                        {
                            morseText += "-..- ";
                            space = true;

                            continue;
                        }

                        morseText += codeMorseRusLetters[text[i] - 1072] + ' ';
                        space = true;

                        continue;
                    }

                    else if (text[i] >= 97 && text[i] <= 122) // если символ из латиницы
                    {
                        morseText += codeMorseEngLetters[text[i] - 97] + ' ';
                        space = true;

                        continue;
                    }

                    else if (text[i] >= 48 && text[i] <= 57) // если символ - число
                    {
                        morseText += codeMorseDigit[text[i] - 48] + ' ';
                        space = true;

                        continue;
                    }

                    else if ((text[i] >= 33 && text[i] < 48 || text[i] > 57 && text[i] <= 64) && (text[i] != 35 && text[i] != 37 && text[i] != 42 && text[i] != 60 && text[i] != 62)) // если символ - пунктуация
                    {
                        morseText += codeMorsePunctuation[text[i] - 33] + ' ';
                        space = true;

                        continue;
                    }

                    else if (text[i] == ' ') // если символ - пробел
                    {
                        if (space == true)
                        {
                            morseText += "/ ";
                            space = false;
                        }

                        continue;
                    }
                }
            }

            return morseText;
        }


        //////


        public static string FromString(string text, MorseCodeOptions firstOption, MorseCodeOptions secondOption)
        {
            text = text.Trim(' ', '\n', '\t').ToLower(); // Trim - чтобы убрать лишние (символы) в начале и в конце строки, а ToLower() - чтобы перенести все буквы в нижний регистр
            string morseText = new string('\0', 6 * text.Length); // выделение максимума места

            bool space = false; // чтобы не было повторяющихся пробелов, а также, чтобы не было пробелов в начале строки


            for (int i = 0; i < text.Length; ++i)
            {
                if (text[i] >= 1072 && text[i] <= 1105 && text[i] != 1104) // если символ из кириллицы (1104 игнорируется, так как он не буква)
                {
                    if (firstOption != MorseCodeOptions.DeleteLetters && secondOption != MorseCodeOptions.DeleteLetters) // тут решил сделать так, НО:
                                                                                                                        // как мне кажется, реализация в разы хуже (чем в том, где одна опция принимается) в плане оптимизации,
                                                                                                                        // так как миллиард условий нужно проверять при каждой букве взятой со string,
                                                                                                                        // а в том всё проверяется вне циклов.
                                                                                                                        //
                                                                                                                        // Или разница незначительная?
                    {
                        if (text[i] == 'ё')
                        {
                            morseText += ". ";
                            space = true;

                            continue;
                        }

                        if (text[i] == 'ъ')
                        {
                            morseText += "-..- ";
                            space = true;

                            continue;
                        }

                        morseText += codeMorseRusLetters[text[i] - 1072] + ' ';
                        space = true;
                    }

                    continue;
                }

                else if (text[i] >= 97 && text[i] <= 122) // если символ из латиницы
                {
                    if (firstOption != MorseCodeOptions.DeleteLetters && secondOption != MorseCodeOptions.DeleteLetters)
                    {
                        morseText += codeMorseEngLetters[text[i] - 97] + ' ';
                        space = true;
                    }

                    continue;
                }

                else if (text[i] >= 48 && text[i] <= 57) // если символ - число
                {
                    if (firstOption != MorseCodeOptions.DeleteDigits && secondOption != MorseCodeOptions.DeleteDigits)
                    {
                        morseText += codeMorseDigit[text[i] - 48] + ' ';
                        space = true;
                    }

                    continue;
                }

                else if ((text[i] >= 33 && text[i] < 48 || text[i] > 57 && text[i] <= 64) && (text[i] != 35 && text[i] != 37 && text[i] != 42 && text[i] != 60 && text[i] != 62)) // если символ - пунктуация
                {
                    if (firstOption != MorseCodeOptions.DeletePunctuations && secondOption != MorseCodeOptions.DeletePunctuations)
                    {
                        morseText += codeMorsePunctuation[text[i] - 33] + ' ';
                        space = true;
                    }
                    continue;
                }

                else if (text[i] == ' ') // если символ - пробел
                {
                    if (space == true)
                    {
                        morseText += "/ ";
                        space = false;
                    }

                    continue;
                }

                else if (firstOption != MorseCodeOptions.IgnoreInvalidSymbols && secondOption != MorseCodeOptions.IgnoreInvalidSymbols)
                {
                    return $"ошибка, ваша строка имеет недопустимые символы, первый попавшийся: {text[i]}, чтобы проигнорировать их используйте опцию IgnoreInvalidSymbols";
                }
            }

            return morseText;
        }


        //////
    }
}
