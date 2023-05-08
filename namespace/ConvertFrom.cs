using System.Runtime.CompilerServices;

namespace ConvertFrom
{
    public static class MorseCode
    {
        public enum MorseCodeOptions
        {
            None = 0,
            IgnoreInvalidSymbols = 1
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
        }; // ЛУЧШЕ ЛИШНИЙ РАЗ НЕ ТРОГАТЬ ЭТИ МАССИВЫ, ТАК КАК, ЛИЧНО У МЕНЯ, ЛОМАЛИСЬ ЧЁРТОЧКИ: ОНИ ВЫГЛЯДЕЛИ ТАКЖЕ, НО ИМЕЛИ ДРУГОЙ ASCII-КОД, ПОЭТОМУ ЛОГИКА ЛОМАЛАСЬ
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


        public static string ToRusString(string morseCode, MorseCodeOptions option = MorseCodeOptions.None)
        {
            morseCode = morseCode.Trim(' ', '/', '\n', '\t');

            string text = new string('\0', morseCode.Length / 2); // выделение возможного максимума места

            bool upper = true;
            bool space = false;
            int i;


            foreach (string str in morseCode.Split(' '))
            {
                for (i = 0; i < 32; ++i)
                {
                    if (codeMorseRusLetters[i] == str)
                    {
                        text += upper == true ? (char) (i + 1040) : (char) (i + 1072);
                        upper = false;
                        space = true;

                        break;
                    }

                    else if (codeMorsePunctuation[i] == str)
                    {
                        text += (char) (i + 33);
                        space = true;

                        if ((char)(i + 33) == '.' || (char)(i + 33) == '!' || (char)(i + 33) == '?')
                        {
                            upper = true;
                        }

                        break;
                    }

                    else if (i < 10 && codeMorseDigit[i] == str)
                    {
                        text += (char) (i + 48);
                        space = true;

                        break;

                    }

                    else if (str == "/")
                    {
                        if (space == true)
                        {
                            text += " ";
                        }
                        space = false;

                        break;
                    }
                }

                if (i == 32 && option != MorseCodeOptions.IgnoreInvalidSymbols)
                {
                    return $"ошибка, ваш код морзе некорректен, первый попавшийся некорректный символ: {str}";
                }
            }

            return text;
        }


        //////


        public static string ToEngString(string morseCode, MorseCodeOptions option = MorseCodeOptions.None)
        {
            morseCode = morseCode.Trim(' ', '/', '\n', '\t');

            string text = new string('\0', morseCode.Length / 2); // выделение возможного максимума места

            bool upper = true;
            bool space = false;
            int i;


            foreach (string str in morseCode.Split(' '))
            {
                for (i = 0; i < 32; ++i)
                {
                    if (i < 26 && codeMorseEngLetters[i] == str)
                    {
                        text += upper == true ? (char)(i + 65) : (char)(i + 97);
                        upper = false;
                        space = true;

                        break;
                    }

                    else if (codeMorsePunctuation[i] == str)
                    {
                        text += (char)(i + 33);
                        space = true;

                        if ((char)(i + 33) == '.' || (char)(i + 33) == '!' || (char)(i + 33) == '?')
                        {
                            upper = true;
                        }

                        break;
                    }

                    else if (i < 10 && codeMorseDigit[i] == str)
                    {
                        text += (char)(i + 48);
                        space = true;

                        break;

                    }

                    else if (str == "/")
                    {
                        if (space == true)
                        {
                            text += " ";
                        }
                        space = false;

                        break;
                    }
                }

                if (i == 32 && option != MorseCodeOptions.IgnoreInvalidSymbols)
                {
                    return $"ошибка, ваш код морзе некорректен, первый попавшийся некорректный символ: {str}";
                }
            }

            return text;
        }


        //////
    }
}
