using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AsmKurs
{
    class TableUser
    {
        public string Name;//метка или имя
        public int Offset;//смещение
        public string ActiveSeg;//сегмент, в котором определена
        public Types Type;//количество выделенных байт
        public int Number;//№ п/п
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
        public enum Types
        { Byte = 1, Word = 2, DWord = 4, Far = 5, Near = -1 }

        public static List<TableUser> Items = new List<TableUser>();
        public static DataTable Table = CreateTable();

        public TableUser(string name, int off, int seg, int typeNumber)
        {
            Number = Count + 1;
            Name = name;
            Offset = off;
            ActiveSeg = TableSegment.ActiveSegment().Name;
            Type = (Types)typeNumber;
            Items.Add(this);
            Table.Rows.Add(Number, name, off, ActiveSeg, Type);
        }

        private static DataTable CreateTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Номер", typeof(int));
            table.Columns.Add("Ім'я", typeof(string));
            table.Columns.Add("Зміщення", typeof(int));
            table.Columns.Add("Сегмент", typeof(string));
            table.Columns.Add("Тип", typeof(Types));

            return table;
        }

        public static TableUser GetByName(string name)
        {
            foreach (var user in Items)
                if (user.Name == name)
                    return user;
            return null;
        }

        public static TableUser GetByNumber(int number)
        {
            foreach (var user in Items)
                if (user.Number == number)
                    return user;
            return null;
        }

        public override string ToString()
        {
            return this.Name;
        }

    }
}
