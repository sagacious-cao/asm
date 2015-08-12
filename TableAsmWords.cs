using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AsmKurs
{
    class TableAsmWords
    {
        public string Name;//зарезервированное слово
        public int Length;//количество символов
        public string Description;//полное описание
        public Types Type;//короткое описание
        public static List<TableAsmWords> Items = new List<TableAsmWords>();
        public static DataTable Table = CreateTable();

        public enum Types
        {
            пусто = 0, символ, інструкція, тип, директива, регістр,
            сегментний_регістр, число, текст, користувач, помилка, мітка, seg
        }

        public TableAsmWords(string name, string descr, Types type)
        {
            Name = name;
            Length = name.Length;
            Description = descr;
            Type = type;
            Items.Add(this);
            Table.Rows.Add(name, name.Length, type, descr);
        }

        public static DataTable CreateTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Лексема", typeof(string));
            table.Columns.Add("Довжина лексеми", typeof(int));
            table.Columns.Add("Тип лексеми", typeof(string));
            table.Columns.Add("Опис", typeof(string));

            return table;
        }

        public static TableAsmWords GetByName(string name)
        {
            foreach (TableAsmWords asmword in TableAsmWords.Items)
                if (name == asmword.Name)
                    return asmword;
            return null;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public static void Create()
        {
            new TableAsmWords(",", "Односимвольна", Types.символ);
            new TableAsmWords("[", "Односимвольна", Types.символ);
            new TableAsmWords("]", "Односимвольна", Types.символ);
            new TableAsmWords("*", "Односимвольна", Types.символ);
            new TableAsmWords("-", "Односимвольна", Types.символ);
            new TableAsmWords("+", "Односимвольна", Types.символ);
            new TableAsmWords(";", "Односимвольна", Types.символ);
            new TableAsmWords(":", "Односимвольна", Types.символ);
            new TableAsmWords("(", "Односимвольна", Types.символ);
            new TableAsmWords(")", "Односимвольна", Types.символ);

            new TableAsmWords("CLD", "Ідентифікатор мнемокоду машинної інструкції", Types.інструкція);
            new TableAsmWords("CMP", "Ідентифікатор мнемокоду машинної інструкції", Types.інструкція);
            new TableAsmWords("JAE", "Ідентифікатор мнемокоду машинної інструкції", Types.інструкція);
            new TableAsmWords("JMP", "Ідентифікатор мнемокоду машинної інструкції", Types.інструкція);
            new TableAsmWords("MOV", "Ідентифікатор мнемокоду машинної інструкції", Types.інструкція);
            new TableAsmWords("NEG", "Ідентифікатор мнемокоду машинної інструкції", Types.інструкція);
            new TableAsmWords("PUSH", "Ідентифікатор мнемокоду машинної інструкції", Types.інструкція);

            new TableAsmWords("SEG", "Ідентифікатор виділення сегментної частини адреси", Types.seg);
            new TableAsmWords("PTR", "Ідентифікатор оператора визначення типу", Types.тип);
            new TableAsmWords("DWORD", "Ідентифікатор типу - подвійне слово", Types.тип);
            new TableAsmWords("FWORD", "Ідентифікатор типу - 32-розрядна дальня мітка", Types.тип);
            new TableAsmWords("FAR", "Ідентифікатор дальньої мітки", Types.тип);
            new TableAsmWords("LABEL", "Ідентифікатор мітки", Types.директива);
            new TableAsmWords(".386", "Ідентифікатор директиви розрядності даних та адрес", Types.директива);

            new TableAsmWords("DB", "Ідентифікатор директиви даних типу байт", Types.директива);
            new TableAsmWords("DW", "Ідентифікатор директиви даних типу слово", Types.директива);
            new TableAsmWords("DD", "Ідентифікатор директиви даних типу подвійне слово", Types.директива);
            new TableAsmWords("END", "Ідентифікатор директиви кінця програми", Types.директива);
            new TableAsmWords("SEGMENT", "Ідентифікатор директиви початку сегменту", Types.директива);
            new TableAsmWords("ASSUME", "Ідентифікатор директиви призначення сегментних регістрів", Types.директива);
            new TableAsmWords("ENDS", "Ідентифікатор директиви кінця сегменту", Types.директива);

            new TableAsmWords("EAX", "Ідентифікатор 32 розрядного регістру даних", Types.регістр);
            new TableAsmWords("EBX", "Ідентифікатор 32 розрядного регістру адрес", Types.регістр);
            new TableAsmWords("ECX", "Ідентифікатор 32 розрядного регістру даних - лічильник", Types.регістр);
            new TableAsmWords("EDX", "Ідентифікатор 32 розрядного регістру даних", Types.регістр);
            new TableAsmWords("ESP", "Ідентифікатор вказівника стеку", Types.регістр);
            new TableAsmWords("EBP", "Ідентифікатор 32 розрядного регістру адрес", Types.регістр);
            new TableAsmWords("ESI", "Ідентифікатор 32 розрядного регістру адрес", Types.регістр);
            new TableAsmWords("EDI", "Ідентифікатор 32 розрядного регістру адрес", Types.регістр);

            new TableAsmWords("CS", "Ідентифікатор сегментного регістру кодів", Types.сегментний_регістр);
            new TableAsmWords("DS", "Ідентифікатор сегментного регістру даних", Types.сегментний_регістр);
            new TableAsmWords("SS", "Ідентифікатор сегментного регістру стеку", Types.сегментний_регістр);
            new TableAsmWords("ES", "Ідентифікатор додаткового сегментного регістру даних", Types.сегментний_регістр);
            new TableAsmWords("FS", "Ідентифікатор додаткового сегментного регістру даних", Types.сегментний_регістр);
            new TableAsmWords("GS", "Ідентифікатор додаткового сегментного регістру даних", Types.сегментний_регістр);
        }
    }
}
