using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AsmKurs
{
    class TableSentence
    {
        public TableAsmWords.Types Type = TableAsmWords.Types.пусто;//тип предложения
        public string Mnem = "";//инструкция или директива. если нету - имя или лейбл
        public int NameOrLabelNumber = 0;//имя пользователя
        public int MnemNumber = 0;//место инструкции в предложении
        public int[,] Operands = new int[6, 2];//операнды
        public static TableSentence Item;
        public static DataTable Table = CreateTable();
        public int OpCount//количество операндов
        {
            get
            {
                int result = 0;
                for (int i = 0; i < 6; i++)
                    if (Operands[i, 0] != 0)
                        result++;
                return result;
            }
        }

        public TableSentence(string mnem, int nameNum, int mnemNum, int[,] opers, TableAsmWords.Types type)
        {
            Type = type;
            Mnem = mnem;
            NameOrLabelNumber = nameNum;
            MnemNumber = mnemNum;
            for (int k = 0; k < 6; k++)
                for (int l = 0; l < 2; l++)
                    Operands[k, l] = opers[k, l];

            Item = this;

            if (Table.Rows.Count <= 0)
                Table.Rows.Add(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Table.Rows[0][0] = NameOrLabelNumber;
            Table.Rows[0][1] = MnemNumber;
            for (int p = 0; p < 6; p++)
            {
                Table.Rows[0][p * 2 + 2] = Operands[p, 0];
                Table.Rows[0][p * 2 + 3] = Operands[p, 1];
            }
        }

        public static DataTable CreateTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Номер лексеми імені або міток", typeof(int));
            table.Columns.Add("Номер лексеми мнемокоду", typeof(int));
            table.Columns.Add("Номер першої лексеми першого операнду", typeof(int));
            table.Columns.Add("Кількість лексем першого операнду", typeof(int));
            table.Columns.Add("Номер першої лексеми другого операнду", typeof(int));
            table.Columns.Add("Кількість лексем другого операнду", typeof(int));
            table.Columns.Add("Номер першої лексеми третього операнду", typeof(int));
            table.Columns.Add("Кількість лексем третього операнду", typeof(int));
            table.Columns.Add("Номер першої лексеми четвертого операнду", typeof(int));
            table.Columns.Add("Кількість лексем четвертого операнду", typeof(int));
            table.Columns.Add("Номер першої лексеми п'ятого операнду", typeof(int));
            table.Columns.Add("Кількість лексем п'ятого операнду", typeof(int));
            table.Columns.Add("Номер першої лексеми шостого операнду", typeof(int));
            table.Columns.Add("Кількість лексем шостого операнду", typeof(int));

            return table;
        }

        public override string ToString()
        {
            return this.Mnem;
        }
    }
}
