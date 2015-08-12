using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AsmKurs
{
    class TableSegment
    {
        public int Number;//№ п/п
        public string Name;//имя сегмента
        public int Capacity;//разрядность (32)
        public int Offset;//смещение (размер)
        public string Placing = "PARA";
        public string Union = "PRIVATE";
        public string Class = "NONE";
        public static int ActiveSegmentNumber;//номер активного сегмента

        public static List<TableSegment> Items = new List<TableSegment>();
        public static DataTable Table = CreateTable();

        public TableSegment(int num, string name, int cap, int off)
        {
            Number = num;
            Name = name;
            Capacity = cap;
            Offset = off;

            Items.Add(this);
            Table.Rows.Add(num, name, cap, off, Placing, Union, Class);
        }

        private static DataTable CreateTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("№", typeof(int));
            table.Columns.Add("Ім'я", typeof(string));
            table.Columns.Add("Розрядність", typeof(int));
            table.Columns.Add("Зміщення", typeof(int));
            table.Columns.Add("Розміщення", typeof(string));
            table.Columns.Add("Об'єднання", typeof(string));
            table.Columns.Add("Клас", typeof(string));

            return table;
        }

        public static TableSegment GetByName(string name)
        {
            foreach (TableSegment seg in TableSegment.Items)
                if (name == seg.Name)
                    return seg;
            return null;
        }

        public static TableSegment ActiveSegment()
        {
            if (ActiveSegmentNumber != 0)
                foreach (TableSegment seg in TableSegment.Items)
                    if (ActiveSegmentNumber == seg.Number)
                        return seg;
            return null;
        }

        public static void IncOffset(int num)
        {
            if (ActiveSegmentNumber != 0)
            {
                ActiveSegment().Offset += num;
                if (num > 0)
                    Table.Rows[ActiveSegmentNumber - 1][3] = ActiveSegment().Offset;
            }
            else
                if (!(TableLexeme.MnemName() == ".386") && !(TableLexeme.MnemName() == "ENDS") && !(TableLexeme.MnemName() == "END"))
                    Errors.Add(4);//@error данные/команда вне сегмента
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
