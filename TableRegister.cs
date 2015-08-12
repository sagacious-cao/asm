using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AsmKurs
{
    class TableRegister
    {
        public string Name;//имя регистра
        public int Number;//номер
        public int Prefix;//десятичное представление
        public string PrefixString;//шестнадцатеричное представление
        public static List<TableRegister> Items = new List<TableRegister>();
        public static DataTable Table = CreateTable();

        public TableRegister(string name, int num, int pref)
        {
            Name = name;
            Number = num;
            Prefix = pref;
            PrefixString = AnalysisLexy.NumberHex(Prefix);

            Items.Add(this);
            Table.Rows.Add(Name, Number, Prefix, PrefixString);
        }

        private static DataTable CreateTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Ім'я регістра", typeof(string));
            table.Columns.Add("Номер", typeof(int));
            table.Columns.Add("Префікс число", typeof(int));
            table.Columns.Add("Префікс строка", typeof(string));
            return table;
        }

        public static TableRegister GetByName(string name)
        {
            foreach (TableRegister register in TableRegister.Items)
                if (name == register.Name)
                    return register;
            return null;
        }

        public static TableRegister GetByNumbers(int number, int charCount)
        {
            foreach (TableRegister register in TableRegister.Items)
                if (number == register.Number && register.Name.Length == charCount)
                    return register;
            return null;
        }

        public static void Create()
        {
            new TableRegister("EAX", 0, 0);
            new TableRegister("ECX", 1, 0);
            new TableRegister("EDX", 2, 0);
            new TableRegister("EBX", 3, 0);
            new TableRegister("ESP", 4, 0);
            new TableRegister("EBP", 5, 0);
            new TableRegister("ESI", 6, 0);
            new TableRegister("EDI", 7, 0);
            new TableRegister("ES", 0, 38);
            new TableRegister("CS", 1, 46);
            new TableRegister("SS", 2, 54);
            new TableRegister("DS", 3, 62);
            new TableRegister("FS", 4, 100);
            new TableRegister("GS", 5, 101);
        }

        public override string ToString()
        {
            return this.Name;
        }

    }
}
