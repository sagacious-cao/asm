using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsmKurs
{
    class AnalysisLexy
    {
        public static void FormLexyTable(string MainString)
        {
            TableLexeme.Items.Clear();//Обнуляем таблицу лексем в начале строки
            TableLexeme.Table.Clear();
            //TableLexeme.Count = 0;

            int Count = MainString.IndexOf(";");
            if (Count >= 0)
                MainString = MainString.Remove(Count);
            Count = 0;

            MainString = MainString.Trim(new Char[] { ' ', '\t' });
            while (!string.IsNullOrWhiteSpace(MainString))
            {
                MainString = MainString.Trim(new Char[] { ' ', '\t' });

                int SpaceIndex = MainString.IndexOfAny(new char[] { ' ', '\t', '\n' });
                int SingleIndex = MainString.IndexOfAny(new char[] { ':', ',', '[', ']', '*', '+', '(', ')', '-' });
                bool IsSingleLexeme = false;

                if (SpaceIndex < 0)
                    if (SingleIndex < 0)
                        Count = MainString.Length;
                    else //singleIndex >= 0
                    {
                        IsSingleLexeme = true;
                        Count = SingleIndex;
                    }
                else //spaceIndex >= 0
                    if (SingleIndex < 0)
                        Count = SpaceIndex;
                    else //singleIndex >= 0
                        if (SpaceIndex < SingleIndex)
                            Count = SpaceIndex;
                        else
                        {
                            IsSingleLexeme = true;
                            Count = SingleIndex;
                        }
                int iter = IsSingleLexeme ? 2 : 1;

                MainString = CreateLexemeTable(MainString, Count, iter);
            }
        }

        private static string CreateLexemeTable(string Lexeme, int count, int iter)
        {
            char[] Chars = new char[225];

            for (int i = 0; i < iter; i++)
            {
                if (i == 1)
                    count = 1;

                Lexeme.CopyTo(0, Chars, 0, count);
                Lexeme = Lexeme.Remove(0, count);
                string CurrentLexeme = new string(Chars);
                CurrentLexeme = CurrentLexeme.Trim(new Char[] { '\0' });
                Array.Clear(Chars, 0, Chars.Length);

                if (CurrentLexeme == "")
                    continue;//проверка на две односимвольные подряд

                TableAsmWords Word = TableAsmWords.GetByName(CurrentLexeme.ToUpper());
                if (Word != null)//мы знаем эту лексему
                    new TableLexeme(Word.Name, Word.Description, Word.Type);
                else
                {
                    string ConstDescript = ConstOrUser(CurrentLexeme);
                    TableAsmWords.Types ConstType = TableAsmWords.Types.пусто;
                    switch (ConstDescript)
                    {
                        case "Помилка":
                            ConstType = TableAsmWords.Types.помилка;
                            Errors.Add(1);//@error недопустимые символы в лексеме
                            break;
                        case "Текстова константа":
                            CurrentLexeme = CurrentLexeme.Remove(0, 1);
                            CurrentLexeme = CurrentLexeme.Remove(CurrentLexeme.Length - 1, 1);
                            ConstType = TableAsmWords.Types.текст;
                            break;
                        case "Ідентифікатор користувача або невизначений":
                            CurrentLexeme = CurrentLexeme.ToUpper();
                            ConstType = TableAsmWords.Types.користувач;
                            if (CurrentLexeme.Length > 8)
                                Errors.Add(3);//@error Ідентифікатор довший за 8 символів
                            break;
                        default:
                            CurrentLexeme = CurrentLexeme.ToUpper();
                            ConstType = TableAsmWords.Types.число;
                            break;
                    }
                    new TableLexeme(CurrentLexeme, ConstDescript, ConstType);
                }
            }
            return Lexeme;
        }

        private static string ConstOrUser(string Lexeme)
        {
            string Alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ_@?$";
            string Binar = "01";
            string Dex = Binar + "23456789";
            string Hex = Dex + "ABCDEF";
            string Quote = "\'\"";
            string AlphaDex = Alpha + Dex;
            char First = Lexeme.ToUpper()[0];
            char Last = Lexeme.ToUpper()[Lexeme.Length - 1];

            if (Quote.Contains(First))
            {
                if (Quote.Contains(Last))
                    return "Текстова константа";
            }
            else
            {
                Lexeme = Lexeme.ToUpper();
                if (Alpha.Contains(First))
                {//проверяем первый на буквы
                    if (CharCheck(Lexeme, AlphaDex) && AlphaDex.Contains(Last))
                        return "Ідентифікатор користувача або невизначений";
                }
                else//остались цифры и левые символы в первом
                {
                    if (char.IsDigit(First) && char.IsDigit(Last))
                        return "Десяткова константа"; //первый и последний символы - цифры. 

                    switch (Last)//в последнем остались буквы и левые проверяем на допустимые
                    {
                        case 'B':
                            if (CharCheck(Lexeme, Binar))//проверяем все остальные на допустимость
                                return "Двійкова константа";
                            break;
                        case 'D':
                            if (CharCheck(Lexeme, Dex))
                                return "Десяткова константа";
                            break;
                        case 'H':
                            if (CharCheck(Lexeme, Hex))
                                return "Шістнадцяткова константа";
                            break;
                        default:
                            break;
                    }
                }
            }
            return "Помилка";
        }

        private static bool CharCheck(string lexeme, string pattern)
        {
            for (int i = 0; i < lexeme.Length - 1; i++)
                if (!pattern.Contains(lexeme[i]))
                    return false;
            return true;
        }

        public static string NumberHex(int Number)
        {
            string result;
            if (Number == 0)
                return "";

            result = Convert.ToString(Number, 16);
            if (result.Length > 1 && result[1] == 'x')//проверка на 0х
                result = result.Remove(0, 2);

            if (char.IsLetter(result[0]))
                result = string.Concat("0", result);

            return result.ToUpper();
        }
    }
}
