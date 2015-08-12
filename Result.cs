using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace AsmKurs
{
    class Result
    {
        public int LineNumber;//номер рядка проги
        public int Number;//номер рядка лістингу
        public int LineSize;//размер рядка
        public int OffsetDex;//десятичное смещение
        public string Offset;//смещение рядка
        public string FullCode;//готовый код рядка
        public string ProgLine;//рядок проги
        public Errors Error;//помилки в рядку
        public bool isOffset;//нужно ли выводить смещенние в листинге
        public static int Count;
        public static string FilePath;
        public static int FileLength;

        public static List<Result> Items = new List<Result>();
        public static DataTable Table = CreateTable();

        public Result(int num, string line)
        {
            Number = Items.Count + 1;
            LineNumber = num;
            FullCode = "";
            ProgLine = line;
            Error = null;
            OffsetDex = 0;
            Offset = "";
            isOffset = true;

            Items.Add(this);
            Table.Rows.Add(Number, LineNumber, LineSize, OffsetDex, Offset, FullCode, ProgLine);
        }

        private void SetOffsetDex()
        {
            OffsetDex = 0;

            if (LineSize == 0)
            {
                if (TableSegment.ActiveSegmentNumber != 0)
                    OffsetDex = TableSegment.ActiveSegment().Offset;
                else
                    if (TableSentence.Item.Mnem == "ENDS")
                        OffsetDex = Items[Items.Count - 2].OffsetDex + Items[Items.Count - 2].LineSize;
            }
            else
                if (Items.Count > 0)
                    OffsetDex = Items[Items.Count - 2].OffsetDex + Items[Items.Count - 2].LineSize;
        }

        private static string OffsetToHex(int offsetDex)
        {
            string s = Convert.ToString(offsetDex, 16).ToUpper();
            int d = 4 - s.Length;
            if (d > 0)
                for (int i = 0; i < d; i++)
                    s = string.Concat("0", s);
            return s;
        }

        public void ModifyOffset(int offset)
        {
            if (!isOffset)
                return;
            LineSize = offset;
            SetOffsetDex();
            Offset = OffsetToHex(OffsetDex);

            Table.Rows[Number - 1][2] = LineSize;
            Table.Rows[Number - 1][3] = OffsetDex;
            Table.Rows[Number - 1][4] = Offset;
        }

        private static DataTable CreateTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("№", typeof(int));
            table.Columns.Add("№ рядка", typeof(int));
            table.Columns.Add("Розмір '10", typeof(int));
            table.Columns.Add("Зміщення '10", typeof(int));
            table.Columns.Add("Зміщення '16", typeof(string));
            table.Columns.Add("Код", typeof(string));
            table.Columns.Add("Рядок програми", typeof(string));
            table.Columns.Add("Помилка", typeof(string));
            return table;
        }

        public static Result Current()
        {
            foreach (var item in Items)
                if (item.Number == Count)
                    return item;
            return null;
        }

        public void ModifyCode(string code)
        {
            FullCode = code;
            Table.Rows[Number - 1][5] = code;
        }

        public override string ToString()
        {
            return this.ProgLine;
        }

        public static void OpenFile(string FileName)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            FilePath = Path.Combine(basePath, FileName);
            try
            {
                FileLength = File.ReadAllLines(FilePath).Length;
            }
            catch
            {
                MessageBox.Show("Не вдалося відкрити файл");
                Errors.Add(18);//@error Не вдалося відкрити файл
            }
        }

        public static void CreateListing(string NewFileName)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string ListingPath = Path.Combine(basePath, NewFileName);
            string FileName = Path.GetFileName(FilePath);
            string time = DateTime.Now.ToString();
            string Line = "";

            using (StreamWriter sw = File.CreateText(ListingPath))
            {
                Line = " Гоменюк Ніна, ЗКСМ-11";
                Line = CheckSpaceCount(Line, 60) + time + "\n";
                sw.WriteLine(Line); //первая строка

                foreach (var result in Result.Items)
                    if (result.Error == null)
                        WriteProgramLine(result, sw);
                    else
                        sw.WriteLine(FormErrorLine(result, FileName));

                Line = "\n\n Таблиця сегментів:\n\n Сегмент        Зміщення  Розрядність\n";
                sw.WriteLine(Line);
                foreach (var segment in TableSegment.Items)
                    sw.WriteLine(FormSegmentLine(segment));

                Line = "\n\n Таблиця ідентифікаторів:\n\n Ідентифікатор  Зміщення  Сегмент    Тип\n";
                sw.WriteLine(Line);
                foreach (var user in TableUser.Items)
                    sw.WriteLine(FormUserLine(user));

                Line = "\n Кількість помилок:  " + Errors.Count.ToString();
                sw.Write(Line);
            }
        }

        private static void WriteProgramLine(Result result, StreamWriter sw)
        {
            string Line = " " + result.Offset;
            Line = CheckSpaceCount(Line, 7);
            Line += result.FullCode;

            string helpLine = "";
            if (Line.Length > 28)
            {
                helpLine = Line.Remove(0, 28);
                Line = Line.Remove(28);
            }

            Line = CheckSpaceCount(Line, 30);
            Line += result.ProgLine;
            if ((result.Offset != "0000") && (result.Offset != "")
                && (Result.Items[result.Number].Offset == "0000"))
                Line += "\n\n";
            sw.WriteLine(Line);

            while (helpLine.Length > 0)
            {
                int index = helpLine.Length > 20 ? 20 : helpLine.Length - 1;
                string s = CheckSpaceCount("", 7) + helpLine.Remove(index);
                sw.WriteLine(s);
                helpLine = helpLine.Remove(0, index + 1);
            }
        }

        private static string FormErrorLine(Result result, string FileName)
        {
            string Line = "";
            Line = CheckSpaceCount(Line, 30);
            Line += result.ProgLine + "\n" + FileName + "(" + result.LineNumber.ToString()
                + "): помилка " + result.Error.ToString();
            return Line;
        }

        private static string FormSegmentLine(TableSegment segment)
        {
            string Line = "  " + segment.Name;
            Line = CheckSpaceCount(Line, 18);
            Line += OffsetToHex(segment.Offset);
            Line = CheckSpaceCount(Line, 27);
            Line += segment.Capacity + " Bit";
            return Line;
        }

        private static string FormUserLine(TableUser user)
        {
            string Line = "  " + user.Name;
            Line = CheckSpaceCount(Line, 18);
            Line += OffsetToHex(user.Offset);
            Line = CheckSpaceCount(Line, 27);
            Line += user.ActiveSeg;
            Line = CheckSpaceCount(Line, 36);
            Line += user.Type.ToString().ToUpper();
            TableUser user1 = TableUser.GetByNumber(user.Number + 1);
            if (user1 != null && user.ActiveSeg != user1.ActiveSeg)
                Line += "\n";
            return Line;
        }

        public static string CheckSpaceCount(string StrToCheck, int CountToBe)
        {
            string result = StrToCheck;
            int d = CountToBe - StrToCheck.Length;
            if (d > 0)
                for (int i = 0; i < d; i++)
                    result = string.Concat(result, " ");
            return result;
        }
    }
}
