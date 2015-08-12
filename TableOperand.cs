using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AsmKurs
{
    class TableOperand
    {
        public bool IsFirstOperand; //номер операнда
        public int LineNumber;//номер рядка
        public string LexemeName;//тип рядка
        public bool IsLexemePresent;//есть ли сабж в операнде
        public int MainAttr;//основной аттр
        public int ExtraAttr;//дополнительный аттр
        public static Types TypeFirst = 0;//тип первого операнда
        public static Types TypeSecond = 0;//тип первого операнда

        public static List<TableOperand> ItemsFirst = new List<TableOperand>();
        public static List<TableOperand> ItemsSecond = new List<TableOperand>();
        public static DataTable TableFirst = CreateTable();
        public static DataTable TableSecond = CreateTable();

        public TableOperand(bool isFirst, int num, string name)
        {
            IsFirstOperand = isFirst;
            LineNumber = num;
            LexemeName = name;
            IsLexemePresent = false;
            MainAttr = -1;
            ExtraAttr = -1;

            if (isFirst)
            {
                TableFirst.Rows.Add(num, name, false, MainAttr, ExtraAttr);
                ItemsFirst.Add(this);
            }
            else
            {
                TableSecond.Rows.Add(num, name, false, MainAttr, ExtraAttr);
                ItemsSecond.Add(this);
            }
        }
        public override string ToString()
        {
            return this.LexemeName;
        }

        private static DataTable CreateTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("№", typeof(int));
            table.Columns.Add("Можлива лексема", typeof(string));
            table.Columns.Add("Наявність лексеми", typeof(bool));
            table.Columns.Add("Основні властивості", typeof(int));
            table.Columns.Add("Допоміжні властивості", typeof(int));

            return table;
        }

        public static void Create()
        {
            //первый операнд
            new TableOperand(true, 1, "Регістр даних");
            new TableOperand(true, 2, "Оператор ptr");
            new TableOperand(true, 3, "Префікс заміни сегменту");
            new TableOperand(true, 4, "Ідентифікатор мітки або імені");
            new TableOperand(true, 5, "Невизначений ідентифікатор");
            new TableOperand(true, 6, "Адресний регістр 1");
            new TableOperand(true, 7, "Адресний регістр 2");
            new TableOperand(true, 8, "Множник");
            new TableOperand(true, 9, "Оператор Seg");
            new TableOperand(true, 10, "Константа або абсолютний вираз");
            new TableOperand(true, 11, "Текстова константа");

            //второй операнд
            new TableOperand(false, 1, "Регістр даних");
            new TableOperand(false, 2, "Оператор ptr");
            new TableOperand(false, 3, "Префікс заміни сегменту");
            new TableOperand(false, 4, "Ідентифікатор мітки або імені");
            new TableOperand(false, 5, "Невизначений ідентифікатор");
            new TableOperand(false, 6, "Адресний регістр 1");
            new TableOperand(false, 7, "Адресний регістр 2");
            new TableOperand(false, 8, "Множник");
            new TableOperand(false, 9, "Оператор Seg");
            new TableOperand(false, 10, "Константа або абсолютний вираз");
            new TableOperand(false, 11, "Текстова константа");
        }

        public static void ReCreate()
        {
            //первый операнд
            foreach (var item in TableOperand.ItemsFirst)
            {
                item.Modify(-1, -1);
                item.IsLexemePresent = false;
                TableFirst.Rows[item.LineNumber - 1][2] = false;
            }

            //второй операнд
            foreach (var item in TableOperand.ItemsSecond)
            {
                item.Modify(-1, -1);
                item.IsLexemePresent = false;
                TableSecond.Rows[item.LineNumber - 1][2] = false;
            }
            TypeFirst = 0;
            TypeSecond = 0;
        }

        public static TableOperand GetByNumbers(int number, bool isFirst)
        {
            if (isFirst)
            {
                foreach (TableOperand value in TableOperand.ItemsFirst)
                    if (number == value.LineNumber)
                        return value;
            }
            else
                foreach (TableOperand value in TableOperand.ItemsSecond)
                    if (number == value.LineNumber)
                        return value;
            return null;
        }

        public void Modify(int mainAttr, int extraAttr)
        {
            IsLexemePresent = true;
            MainAttr = mainAttr;
            ExtraAttr = extraAttr;

            if (IsFirstOperand)
            {
                TableFirst.Rows[LineNumber - 1][2] = IsLexemePresent;
                TableFirst.Rows[LineNumber - 1][3] = mainAttr;
                TableFirst.Rows[LineNumber - 1][4] = extraAttr;
            }
            else
            {
                TableSecond.Rows[LineNumber - 1][2] = IsLexemePresent;
                TableSecond.Rows[LineNumber - 1][3] = mainAttr;
                TableSecond.Rows[LineNumber - 1][4] = extraAttr;
            }
        }

        public enum Types
        {
            Операнд_відсутній = 0,
            Регістр_даних = 1,
            Мітка = 2,
            Ідентифікатор_або_мітка = 3,
            Невизначена_мітка = 4,
            Ідентифікатор = 5,
            Адресний_вираз = 6,
            Константа = 7,
            Помилковий_операнд = 8,
            Текст = 9
        }

        public static void IdentifyOperands(bool IsFirst)
        {
            bool One = GetByNumbers(1, IsFirst).IsLexemePresent;
            bool Two = GetByNumbers(2, IsFirst).IsLexemePresent;
            bool Three = GetByNumbers(3, IsFirst).IsLexemePresent;
            bool Four = GetByNumbers(4, IsFirst).IsLexemePresent;
            bool Five = GetByNumbers(5, IsFirst).IsLexemePresent;
            bool Six = GetByNumbers(6, IsFirst).IsLexemePresent;
            bool Seven = GetByNumbers(7, IsFirst).IsLexemePresent;
            bool Eight = GetByNumbers(8, IsFirst).IsLexemePresent;
            bool Nine = GetByNumbers(9, IsFirst).IsLexemePresent;
            bool Ten = GetByNumbers(10, IsFirst).IsLexemePresent;
            bool Eleven = GetByNumbers(11, IsFirst).IsLexemePresent;

            Types Results;
            if (Eight)
            {
                int scaledReg = GetByNumbers(8, IsFirst).MainAttr;
                if (scaledReg == 4)
                    Errors.Add(7);//@error esp с индексом

                int unScaledReg;
                if (GetByNumbers(6, IsFirst).MainAttr == scaledReg)
                    unScaledReg = GetByNumbers(7, IsFirst).MainAttr;
                else
                    unScaledReg = GetByNumbers(6, IsFirst).MainAttr;
                if (unScaledReg == 5)
                    Errors.Add(7);//@error ebp база
            }

            if (!One && !Two && !Three && !Four && !Five && !Six && !Seven && !Eight && !Nine && !Ten && !Eleven)
                Results = Types.Операнд_відсутній;
            else if (One && !Two && !Three && !Four && !Five && !Six && !Seven && !Eight && !Nine && !Ten && !Eleven)
                Results = Types.Регістр_даних;
            else if (Four && !One&& !Three && !Five && !Six && !Seven && !Eight && !Ten && !Eleven)
                Results = Types.Мітка;
            else if (Four && !One && !Two && !Three && !Five && !Six && !Seven && !Eight && !Nine && !Ten && !Eleven)
                Results = Types.Ідентифікатор_або_мітка;
            else if (Five && !One && !Three && !Four && !Six && !Seven && !Eight && !Ten && !Eleven)
                Results = Types.Невизначена_мітка;
            else if (Four && !One && !Five && !Six && !Seven && !Eight && !Nine && !Ten && !Eleven)
                Results = Types.Ідентифікатор;
            else if (Four && Six && Seven && Eight && !One && !Five && !Nine && !Ten && !Eleven)
                Results = Types.Адресний_вираз;
            else if (Ten && !One && !Two && !Three && !Four && !Five && !Six && !Seven && !Eight && !Nine && !Eleven)
                Results = Types.Константа;
            else if (Eleven && !One && !Two && !Three && !Four && !Five && !Six && !Seven && !Eight && !Nine && !Ten)
                Results = Types.Текст;
            else
                Results = Types.Помилковий_операнд;//@error операнд неопределён

            if (IsFirst)
                TypeFirst = Results;
            else
                TypeSecond = Results;
        }

        public static bool VerifyOperands(string MnenName)
        {
            bool result = false;
            switch (MnenName)
            {
                case "CLD":
                    if (TypeFirst == 0 && TypeSecond == 0)
                        result = true;
                    break;
                case "NEG":
                    if (TypeFirst == Types.Регістр_даних && TypeSecond == 0)
                        result = true;
                    break;
                case "JAE":
                    if ((TypeFirst == Types.Ідентифікатор_або_мітка || TypeFirst == Types.Невизначена_мітка
                        || TypeFirst == Types.Мітка) && TypeSecond == 0)
                        result = true;
                    break;
                case "JMP":
                    if ((TypeFirst == Types.Ідентифікатор_або_мітка || TypeFirst == Types.Невизначена_мітка
                        || TypeFirst == Types.Адресний_вираз || TypeFirst == Types.Ідентифікатор || TypeFirst == Types.Мітка)
                        && TypeSecond == 0)
                        result = true;
                    break;
                case "PUSH":
                    if ((TypeFirst == Types.Ідентифікатор_або_мітка || TypeFirst == Types.Ідентифікатор
                        || TypeFirst == Types.Адресний_вираз || TypeFirst == Types.Мітка) && TypeSecond == 0)
                        result = true;
                    break;
                case "CMP":
                    if ((TypeFirst == Types.Ідентифікатор_або_мітка || TypeFirst == Types.Ідентифікатор
                        || TypeFirst == Types.Адресний_вираз || TypeFirst == Types.Мітка) && TypeSecond == Types.Регістр_даних)
                        result = true;
                    break;
                case "MOV":
                    if (TypeFirst == Types.Регістр_даних && (TypeSecond == Types.Ідентифікатор_або_мітка
                        || TypeSecond == Types.Ідентифікатор || TypeSecond == Types.Адресний_вираз || TypeSecond == Types.Мітка))
                        result = true;
                    break;
                case "DB":
                    if ((TypeFirst == Types.Константа || TypeFirst == Types.Текст) && TypeSecond == Types.Операнд_відсутній)
                        result = true;
                    break;
                case "DW":
                case "DD":
                    if ((TypeFirst == Types.Константа || TypeFirst == Types.Невизначена_мітка
                        || TypeFirst == Types.Мітка || TypeFirst == Types.Ідентифікатор_або_мітка)
                        && TypeSecond == Types.Операнд_відсутній)
                        result = true;
                    break;
                case "END":
                    if ((TypeFirst == Types.Мітка || TypeFirst == Types.Ідентифікатор_або_мітка)
                        && TypeSecond == Types.Операнд_відсутній)
                        result = true;
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
