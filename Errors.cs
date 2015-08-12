using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace AsmKurs
{
    class Errors
    {
        public int LineNumber;//номер строки проги, к которой создаётся ошибка
        public static int Count;//количество ошибок в коде
        public string Descript;//описание
        public string Number;//отображаемый номер ошибки
        public int TempNumber;//номер ошибки по порядку
        private static int TempCount;//количество ошибок в списке

        public static List<Errors> Templates = new List<Errors>();
        public static DataTable TempTable = CreateTable();

        public static List<Errors> Items = new List<Errors>();
        public static DataTable Table = CreateTable();

        public Errors(string number, string descript)
        {
            LineNumber = -1;
            TempNumber = ++TempCount;
            Number = number;
            Descript = descript;

            Templates.Add(this);
            TempTable.Rows.Add(LineNumber, Number, Descript);
        }

        public Errors(int lineNumber, string number, string descript)
        {
            LineNumber = lineNumber;
            Number = number;
            Descript = descript;

            Items.Add(this);
            Table.Rows.Add(LineNumber, Number, Descript);
            Count++;
        }

        private static DataTable CreateTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Номер рядка", typeof(int));
            table.Columns.Add("Номер помилки", typeof(string));
            table.Columns.Add("Опис помилки", typeof(string));

            return table;
        }

        public static void Add(int num)
        {
            int line;
            if (Result.Current() == null)//строки нет
                line = -1;
            else
                if (Result.Current().Error != null)//в строке уже есть ошибка
                    return;
                else
                    line = Result.Current().LineNumber;

            Errors tempError = null;
            foreach (var templ in Templates)
                if (templ.TempNumber == num)
                    tempError = templ;
            Errors error;

            if (tempError == null)
            {
                MessageBox.Show("Error not found!");
                return;
            }
            else
                error = new Errors(line, tempError.Number, tempError.Descript);

            if (Result.Current() != null)//если строка есть, добавляем ей ошибку
            {
                Result.Current().Error = error;
                Result.Table.Rows[Result.Count - 1][7] = error;
            }
        }

        public static void CreateTempl()
        {
            new Errors("Лекс 1", "Недопустимі символи");
            new Errors("Лекс 2", "Завелике число");
            new Errors("Лекс 3", "Ідентифікатор довший за 8 символів");

            new Errors("Синт 4", "Дані або команда поза сегментом");
            new Errors("Синт 5", "Невизначена синтаксична конструкція");
            new Errors("Синт 6", "Невірна кількість операндів");
            new Errors("Синт 7", "Невірне використання регістру");
            new Errors("Синт 8", "Очікується директива визначення типу");
            new Errors("Синт 9", "Помилковий множник регістру");
            new Errors("Синт 10", "Помилковий операнд оператору Seg");
            new Errors("Синт 11", "Невірний абсолютний вираз");
            
            new Errors("Грам 12", "Невірний або відсутній тип мітки");
            new Errors("Грам 13", "Спроба повторного визначення ідентифікатора користувача");
            new Errors("Грам 14", "Недопустимий аргумент директиви");
            new Errors("Грам 15", "Помилкові операнди");
            
            new Errors("Крит 16", "Порожній вихідний файл");
            new Errors("Крит 17", "Відсутня директива END");
            new Errors("Крит 18", "Не вдалося відкрити файл");
            
            new Errors("Грам 19", "Невідповідність зміщень першого та другого переглядів");
            new Errors("Синт 20", "Забагато операндів у директиві");
            new Errors("Лекс 21", "Невірна лексема");
        }

        public override string ToString()
        {
            return this.Number + ": " + this.Descript;
        }

    }
}
