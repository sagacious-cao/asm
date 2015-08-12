using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AsmKurs
{
    class TableLexeme
    {
        public int Number;//номер лексемы
        public int Length;//количество символов
        public string Name;//лексема
        public string Description;//полное описание
        public TableAsmWords.Types Type;//короткое описание
        public static List<TableLexeme> Items = new List<TableLexeme>();
        public static DataTable Table = CreateTable();
        public static int Count//количество лексем
        {
            get
            {
                int result = 0;
                foreach (var item in Items)
                    result++;
                return result;
            }
        }

        public static string MnemName()
        {
            if (TableSentence.Item.MnemNumber != 0)
                return GetByNumber(TableSentence.Item.MnemNumber).Name;
            return "";
        }

        public static TableLexeme Label()
        {
            if (TableSentence.Item.NameOrLabelNumber != 0)
                return GetByNumber(TableSentence.Item.NameOrLabelNumber);
            return null;
        }

        public TableLexeme(string name, string desc, TableAsmWords.Types type)
        {
            Number = Count + 1;
            Name = name;
            Length = name.Length;
            Description = desc;
            Type = type;
            Items.Add(this);
            Table.Rows.Add(Count, name, name.Length, desc, type);
        }

        public static DataTable CreateTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Номер", typeof(int));
            table.Columns.Add("Лексема", typeof(string));
            table.Columns.Add("Довжина лексеми", typeof(int));
            table.Columns.Add("Опис", typeof(string));
            table.Columns.Add("Тип", typeof(string));

            return table;
        }

        public int NumberDex()//десятковий вид константи
        {
            string Lexeme = this.Name;
            if (char.IsLetter(Lexeme, Lexeme.Length - 1))
                Lexeme = Lexeme.Remove(Lexeme.Length - 1, 1);
            char First = Lexeme[0];

            if (this.Type == TableAsmWords.Types.число)
                try
                {
                    switch (this.Description)
                    {
                        case "Десяткова константа":
                            return Convert.ToInt32(Lexeme);
                        case "Шістнадцяткова константа":
                            if (First == '0')
                                Lexeme = Lexeme.Remove(0, 1);
                            return Convert.ToInt32(Lexeme, 16);
                        case "Двійкова константа":
                            return Convert.ToInt32(Lexeme, 2);
                        default:
                            break;
                    }
                }
                catch
                {
                    Errors.Add(2);//@error too big number
                }
            else
                Errors.Add(21);//@error wrong lexeme
            return -1;
        }

        public TableAsmWords AsmWord()
        {
            foreach (var word in TableAsmWords.Items)
                if (this.Name == word.Name)
                    return word;
            return null;
        }

        public static TableLexeme GetByNumber(int num)
        {
            foreach (TableLexeme lextable in TableLexeme.Items)
                if (num == lextable.Number)
                    return lextable;
            return null;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
