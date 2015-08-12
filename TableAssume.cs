using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AsmKurs
{
    class TableAssume
    {
        public string CS;
        public string DS;
        public string SS;
        public string ES;
        public string GS;
        public string FS;

        public static List<TableAssume> Item = new List<TableAssume>();
        public static DataTable Table = CreateTable();

        public TableAssume()
        {
            CS = "NOTHING";
            DS = "NOTHING";
            SS = "NOTHING";
            ES = "NOTHING";
            GS = "NOTHING";
            FS = "NOTHING";

            Item.Add(this);
            Table.Rows.Add(CS, DS, SS, ES, GS, FS);
        }

        private static DataTable CreateTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("CS", typeof(string));
            table.Columns.Add("DS", typeof(string));
            table.Columns.Add("SS", typeof(string));
            table.Columns.Add("ES", typeof(string));
            table.Columns.Add("GS", typeof(string));
            table.Columns.Add("FS", typeof(string));
            return table;
        }

        public static void Clear()
        {
            Item[0].CS = "NOTHING";
            Item[0].DS = "NOTHING";
            Item[0].SS = "NOTHING";
            Item[0].ES = "NOTHING";
            Item[0].GS = "NOTHING";
            Item[0].FS = "NOTHING";
            for (int i = 0; i < 6; i++)
            {
                Table.Rows[0][i] = "NOTHING";
            }
        }

        public static void Modify(string reg, string name)
        {
            int i = 0;
            switch (reg)
            {
                case "CS":
                    Item[0].CS = name;
                    break;
                case "DS":
                    Item[0].DS = name;
                    i = 1;
                    break;
                case "SS":
                    Item[0].SS = name;
                    i = 2;
                    break;
                case "ES":
                    Item[0].ES = name;
                    i = 3;
                    break;
                case "GS":
                    Item[0].GS = name;
                    i = 4;
                    break;
                case "FS":
                    Item[0].FS = name;
                    i = 5;
                    break;
                default:
                    break;
            }
            Table.Rows[0][i] = name;
        }

        public static string GetByName(string name)
        {
            if (Item[0].CS == name)
                return "CS";
            if (Item[0].DS == name)
                return "DS";
            if (Item[0].SS == name)
                return "SS";
            if (Item[0].ES == name)
                return "ES";
            if (Item[0].GS == name)
                return "GS";
            if (Item[0].FS == name)
                return "FS";
            return "";
        }
    }
}
