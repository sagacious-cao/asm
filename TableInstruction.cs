using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AsmKurs
{
    class TableInstruction
    {
        public string Name;//имя команды
        public int ByteCount;//количество байт, занимаемое командой
        public bool ModRM;//наличие сабжа
        public int OperandCount;//количество операндов
        public string OpCommand;//команда, если она не зависит от операндов

        public static List<TableInstruction> Items = new List<TableInstruction>();
        public static DataTable Table = CreateTable();

        public TableInstruction(string name, int byteCount, bool modrm, int opCount, string opCode)
        {
            Name = name;
            ByteCount = byteCount;
            ModRM = modrm;
            OperandCount = opCount;
            OpCommand = opCode;

            Items.Add(this);
            Table.Rows.Add(name, byteCount, modrm, opCount, OpCommand);
        }

        private static DataTable CreateTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Інструкція", typeof(string));
            table.Columns.Add("Кількість байтів", typeof(int));
            table.Columns.Add("Наявність байту Modr/m", typeof(bool));
            table.Columns.Add("Кількість операндів", typeof(int));
            table.Columns.Add("Код команди", typeof(string));
            return table;
        }

        public static TableInstruction GetByName(string name)
        {
            foreach (TableInstruction instruct in TableInstruction.Items)
                if (name == instruct.Name)
                    return instruct;
            return null;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public static void Create()
        {
            new TableInstruction("CLD", 1, false, 0, "FC");
            new TableInstruction("CMP", 1, true, 2, "39");
            new TableInstruction("JAE", 2, false, 1, "0F83");
            new TableInstruction("JMP", 1, false, 1, "opt");
            new TableInstruction("MOV", 1, true, 2, "opt");
            new TableInstruction("NEG", 1, true, 1, "F7");
            new TableInstruction("PUSH", 1, true, 1, "FF");

            new TableInstruction("DB", 1, false, 1, "");
            new TableInstruction("DW", 2, false, 1, "");
            new TableInstruction("DD", 4, false, 1, "");
            new TableInstruction("LABEL", 0, false, 0, "");
            new TableInstruction("END", 0, false, 1, "");
        }

    }
}
