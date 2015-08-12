using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AsmKurs
{
    class TableCurrInstruct
    {
        public int LineNumber;
        public string Name;//имя инструкции
        public int ByteCount;// количество байт инструкции
        public bool IsPrefix;
        public bool IsModRm;
        public bool IsSib;
        public bool IsOffset;//смещение в команде
        public bool IsImmediate;

        public static List<TableCurrInstruct> Items = new List<TableCurrInstruct>();

        public TableCurrInstruct(string name)
        {
            LineNumber = Result.Current().LineNumber;
            Name = name;
            ByteCount = 0;
            IsPrefix = false;
            IsModRm = false;
            IsSib = false;
            IsOffset = false;
            IsImmediate = false;

            Items.Add(this);
        }

        public enum Types { префікс, модрм, сіб, зміщення, дані, пусто }

        public void IncByteCount(int count, Types codePart)
        {
            ByteCount += count;
            switch (codePart)
            {
                case Types.префікс:
                    IsPrefix = true;
                    break;
                case Types.модрм:
                    IsModRm = true;
                    break;
                case Types.сіб:
                    IsSib = true;
                    break;
                case Types.зміщення:
                    IsOffset = true;
                    break;
                case Types.дані:
                    IsImmediate = true;
                    break;
                default:
                    break;
            }
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
}
