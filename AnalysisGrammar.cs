using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsmKurs
{
    class AnalysisGrammar
    {
        // СЧИТАЕМ КОЛИЧЕСТВО БАЙТ СТРОКИ ******************************************************

        public static int CalculateOffset()
        {
            int result = 0;

            if (TableSentence.Item.Type == TableAsmWords.Types.директива)
                result = CountDirective();

            if (TableSentence.Item.Type == TableAsmWords.Types.інструкція)
                result = CountInstruction();

            if (TableSentence.Item.Type == TableAsmWords.Types.мітка)
                AddTableUser(-1);

            return result;//количество байт строки
        }

        // СЧИТАЕМ КОЛИЧЕСТВО БАЙТ В ДИРЕКТИВЕ ******************************************************

        public static int CountDirective()
        {
            int result = 0;
            AnalysisSyntaxy.DoOperands();

            if (Result.Current().Error != null)
                return 0;

            switch (TableSentence.Item.Mnem)
            {
                case "SEGMENT":
                    DoSegment(TableLexeme.Label().Name);
                    break;
                case "ASSUME":
                    DoAssume();
                    break;
                case "ENDS":
                    TableSegment.ActiveSegmentNumber = 0;
                    break;
                case "DB":
                    result += CountData();
                    break;
                case "DW":
                    result += 2;
                    break;
                case "DD":
                    result += 4;
                    break;
                case "LABEL":
                    DoLabel();
                    break;
                default:
                    Result.Current().isOffset = false;
                    break;
            }
            return result;
        }

        private static void DoLabel()
        {
            TableLexeme LabelLex = TableLexeme.Label();
            // int i = TableSentence.Item.Operands[0, 0];
            TableLexeme LabelType = TableLexeme.GetByNumber(TableLexeme.Label().Number + 2);
            if (LabelType != null && LabelType.Name == "FAR")
                AddTableUser(5);
            else
                Errors.Add(12);//@error Label type is wrong or missing
        }

        private static int CountData()
        {
            switch (TableOperand.TypeFirst)
            {
                case TableOperand.Types.Константа:
                    return TableOperand.GetByNumbers(10, true).ExtraAttr;
                case TableOperand.Types.Текст:
                    return TableOperand.GetByNumbers(11, true).MainAttr;
                default:
                    return 1;
            }
        }

        public static void AddTableUser(int byteCount)
        {//if username present
            if (TableSentence.Item.NameOrLabelNumber != 0 && LookOver.IsGoFirst 
                && TableSegment.ActiveSegment() != null)
            {
                string userName = TableLexeme.Label().Name;
                int userOffset = TableSegment.ActiveSegment().Offset;

                if (TableUser.GetByName(userName) != null)
                    Errors.Add(13);//@error user name already exists in the table
                else
                    new TableUser(userName, userOffset, TableSegment.ActiveSegmentNumber, byteCount);
            }
        }

        private static void DoAssume()
        {
            int i = 0;
            Result.Current().isOffset = false;
            while (TableSentence.Item.Operands[i, 0] != 0)
            {
                string reg = TableLexeme.GetByNumber(TableSentence.Item.Operands[i, 0]).Name;
                string name = TableLexeme.GetByNumber(TableSentence.Item.Operands[i, 0] + 2).Name;
                TableAssume.Modify(reg, name);
                i++;
            }
        }

        private static void DoSegment(string dirname)
        {
            if (TableSegment.Items.Count > 0 && TableSegment.GetByName(dirname) != null)
                TableSegment.ActiveSegmentNumber = TableSegment.GetByName(dirname).Number;
            else
                new TableSegment(TableSegment.Items.Count + 1, dirname, 32, 0);
            TableSegment.ActiveSegmentNumber = TableSegment.GetByName(dirname).Number;
        }

        // СЧИТАЕМ КОЛИЧЕСТВО БАЙТ В ИНСТРУКЦИИ ******************************************************

        public static int CountInstruction()
        {
            string MnemName = TableSentence.Item.Mnem;
            AnalysisSyntaxy.DoOperands();

            if (Result.Current().Error != null)
                return 0;

            //if (TableSegment.ActiveSegmentNumber == 0 && !(MnemName == ".386") && !(MnemName == "ENDS") && !(MnemName == "END"))
            //    Errors.Add(2);//@error команда вне сегмента

            TableCurrInstruct CurrInstr = new TableCurrInstruct(MnemName);

            CurrInstr.IncByteCount(TableInstruction.GetByName(MnemName).ByteCount, TableCurrInstruct.Types.пусто);//размер кода операции
            CountPrefix(CurrInstr);
            CountModRm(CurrInstr);
            CountSib(CurrInstr);
            CountOffset(CurrInstr);

            return CurrInstr.ByteCount;
        }

        private static void CountPrefix(TableCurrInstruct CurrInstr)
        {
            if (CurrInstr.Name != "PUSH" && CurrInstr.Name != "CMP" && CurrInstr.Name != "MOV" && CurrInstr.Name != "JMP")
                return;//префикса быть не может

            bool isFirstOp = CurrInstr.Name == "MOV" ? false : true;
            int DefaultRegisterNum = 3;//ds

            int MultipliedRegister = TableOperand.GetByNumbers(8, isFirstOp).MainAttr;
            int FirstAdressRegister = TableOperand.GetByNumbers(6, isFirstOp).MainAttr;
            int SecondAdressRegister = TableOperand.GetByNumbers(7, isFirstOp).MainAttr;

            if (MultipliedRegister != 4 && MultipliedRegister != 5)//множитель не при ebp esp
                if (FirstAdressRegister == 4 || FirstAdressRegister == 5 || //ebp или esp есть
                 SecondAdressRegister == 4 || SecondAdressRegister == 5)
                    DefaultRegisterNum = 2;//ss

            if (TableOperand.GetByNumbers(3, isFirstOp).IsLexemePresent)
            {
                int PrefReg = TableOperand.GetByNumbers(3, isFirstOp).MainAttr;
                if (PrefReg != DefaultRegisterNum)
                    CurrInstr.IncByteCount(1, TableCurrInstruct.Types.префікс); //префикс задан явно и не совпадает по умолчанию
            }
            else //префикс не задан явно
                if (TableOperand.GetByNumbers(4, isFirstOp).IsLexemePresent)//є ідентиф користувача
                {
                    int userNumber = TableOperand.GetByNumbers(4, isFirstOp).MainAttr;//номер рядка в таблиці користувача
                    string logicSegment = TableUser.GetByNumber(userNumber).ActiveSeg;//ім'я логічного сегменту
                    string userRegister = TableAssume.GetByName(logicSegment);//имя регистра логического сегмента
                    var Register = TableRegister.GetByName(userRegister);//сам регистр

                    if (Register != null && Register.Number != DefaultRegisterNum)
                        CurrInstr.IncByteCount(1, TableCurrInstruct.Types.префікс);//нужно генерировать незаданный префикс
                }
        }

        private static void CountModRm(TableCurrInstruct CurrInstr)
        {
            if (CurrInstr.Name == "MOV")
            {
                if (TableOperand.GetByNumbers(1, true).MainAttr != 0)
                    CurrInstr.IncByteCount(1, TableCurrInstruct.Types.модрм);
                else if (TableOperand.TypeSecond == TableOperand.Types.Адресний_вираз)
                    CurrInstr.IncByteCount(1, TableCurrInstruct.Types.модрм);
                return;
            }

            if (CurrInstr.Name == "JMP")
            {
                if (TableOperand.GetByNumbers(4, true).IsLexemePresent)//визначений ідентифікатор
                {
                    TableUser.Types userType = TableUser.GetByNumber(TableOperand.GetByNumbers(4, true).MainAttr).Type;
                    if (userType != TableUser.Types.Far)
                        CurrInstr.IncByteCount(1, TableCurrInstruct.Types.модрм);
                }
                return;
            }

            if (TableInstruction.GetByName(CurrInstr.Name).ModRM)
                CurrInstr.IncByteCount(1, TableCurrInstruct.Types.модрм);
        }

        private static void CountSib(TableCurrInstruct CurrInstr)
        {
            if (CurrInstr.IsModRm && (TableOperand.TypeFirst == TableOperand.Types.Адресний_вираз
                || TableOperand.TypeSecond == TableOperand.Types.Адресний_вираз))
                CurrInstr.IncByteCount(1, TableCurrInstruct.Types.сіб);
        }

        private static void CountOffset(TableCurrInstruct CurrInstr)
        {
            if ((TableOperand.TypeFirst >= TableOperand.Types.Мітка
                && TableOperand.TypeFirst <= TableOperand.Types.Адресний_вираз)
                || (TableOperand.TypeSecond >= TableOperand.Types.Мітка
                && TableOperand.TypeSecond <= TableOperand.Types.Адресний_вираз))
                CurrInstr.IncByteCount(4, TableCurrInstruct.Types.зміщення);
            if (CurrInstr.Name == "JMP" && !CurrInstr.IsModRm)
                CurrInstr.IncByteCount(2, TableCurrInstruct.Types.зміщення);
        }

        // ФОРМИРУЕМ ЗНАЧЕНИЕ СТРОКИ ******************************************************************

        internal static string CommandCreate()
        {
            if (TableSentence.Item.Type == TableAsmWords.Types.директива)
                return CreateData();

            if (TableSentence.Item.Type == TableAsmWords.Types.інструкція)
                return CreateInstruction();

            return "";
        }

        // ФОРМИРУЕМ ДАННЫЕ ****************************************************************************

        private static string CreateData()
        {
            switch (TableSentence.Item.Mnem)
            {
                case "DB":
                    return CreateBytes(2);
                case "DW":
                    return CreateBytes(4);
                case "DD":
                    return CreateBytes(8);
                default://ASSUME, SEGMENT, ENDS, END, .386, LABEL
                    return "";
            }
        }

        private static string CreateBytes(int ByteCount)
        {
            int FirstOpNum = TableSentence.Item.Operands[0, 0];
            TableLexeme Lexeme = TableLexeme.GetByNumber(FirstOpNum);
            string result = "";

            switch (TableOperand.TypeFirst)
            {
                case TableOperand.Types.Мітка:
                    result = Lexeme.Type == TableAsmWords.Types.seg ? "----" : CreateUser(Lexeme, ByteCount);
                    break;
                case TableOperand.Types.Константа:
                    result = CreateNumber(ByteCount);
                    break;
                case TableOperand.Types.Текст:
                    result = CreateText(Lexeme);
                    break;
                default:
                    break;
            }
            return result;
        }

        private static string CreateText(TableLexeme Lexeme)
        {
            byte[] resBytes;
            string result = "";

            resBytes = Encoding.GetEncoding(1251).GetBytes(Lexeme.Name);//@error неправильные коды
            for (int i = 0; i < resBytes.Length; i++)
                result += Convert.ToString(resBytes[i], 16).ToUpper() + " ";
            return result;
        }

        private static string CreateUser(TableLexeme Lexeme, int ByteCount)
        {
            TableUser User = TableUser.GetByName(Lexeme.Name);
            string result = Convert.ToString(Convert.ToInt32(User.Offset), 16).ToUpper();
            result = CheckCharCount(result, ByteCount);
            return result;
        }

        private static string CreateNumber(int ByteCount)
        {
            string result = "";
            long Number = TableOperand.GetByNumbers(10, true).MainAttr;
            try
            {
                switch (ByteCount)
                {
                    case 2:
                        result = Convert.ToString(Convert.ToByte(Number), 16).ToUpper();
                        break;
                    case 4:
                        result = Convert.ToString(Convert.ToInt16(Number), 16).ToUpper();
                        break;
                    case 8:
                        result = Convert.ToString(Convert.ToInt32(Number), 16).ToUpper();
                        break;
                    default:
                        break;
                }
                result = CheckCharCount(result, ByteCount);
            }
            catch
            {
                Errors.Add(2);//@error number too big
            }
            return result;
        }

        // ФОРМИРУЕМ КОМАНДУ ****************************************************************************

        private static string CreateInstruction()
        {
            TableCurrInstruct CurrInstruct = TableCurrInstruct.Items[TableCurrInstruct.Items.Count - 1];

            string result = "";
            if (CurrInstruct.IsPrefix)
                result += CreatePrefix(CurrInstruct) + ":";

            result = Result.CheckSpaceCount(result, 4);
            result += CreateOpCode(CurrInstruct) + " ";

            if (CurrInstruct.IsModRm)
            {
                result += CreateModRm();
                if (CurrInstruct.IsSib)
                    result += CreateSib();
                result += " ";
            }

            if (CurrInstruct.IsOffset)
                result += CreateOffset(CurrInstruct) + " ";

            if (CurrInstruct.IsImmediate)
                result += CreateImmediate(CurrInstruct);

            return result;
        }

        private static string CreatePrefix(TableCurrInstruct CurrInstr)
        {
            bool isFirstOp = CurrInstr.Name == "MOV" ? false : true;
            int DefaultRegisterNum = 3;//ds

            int MultipliedRegister = TableOperand.GetByNumbers(8, isFirstOp).MainAttr;
            int FirstAdressRegister = TableOperand.GetByNumbers(6, isFirstOp).MainAttr;
            int SecondAdressRegister = TableOperand.GetByNumbers(7, isFirstOp).MainAttr;

            if (MultipliedRegister != 4 && MultipliedRegister != 5)//множитель не при ebp esp
                if (FirstAdressRegister == 4 || FirstAdressRegister == 5 || //ebp или esp есть
                 SecondAdressRegister == 4 || SecondAdressRegister == 5)
                    DefaultRegisterNum = 2;//ss

            string Prefix = "";
            TableOperand OperPref = TableOperand.GetByNumbers(3, isFirstOp);
            if (OperPref.IsLexemePresent)//задан явно == не совпадает по умолчанию
            {
                Prefix = TableRegister.GetByNumbers(OperPref.MainAttr, 2).PrefixString;//нужный префикс
                // CurrInstr.Prefix = Prefix;
            }
            else //префикс не задан явно
            {
                int userNumber = TableOperand.GetByNumbers(4, isFirstOp).MainAttr;//номер рядка в таблиці користувача
                string logicSegment = TableUser.GetByNumber(userNumber).ActiveSeg;//ім'я логічного сегменту
                string userRegister = TableAssume.GetByName(logicSegment);//имя регистра логического сегмента
                var Register = TableRegister.GetByName(userRegister);//сам регистр

                if (Register != null && Register.Number != DefaultRegisterNum)//нужно генерировать незаданный префикс
                {
                    Prefix = Register.PrefixString;
                    // CurrInstr.Prefix = Prefix;
                }
            }
            return Prefix;
        }

        private static string CreateOpCode(TableCurrInstruct CurrInstr)
        {
            string Code = TableInstruction.GetByName(CurrInstr.Name).OpCommand;
            if (Code != "opt")//команда завжди однакова
                return Code;
            if (CurrInstr.Name == "JMP")
                if (CurrInstr.IsModRm)
                    Code = "FF";
                else
                    Code = "EA";
            if (CurrInstr.Name == "MOV")
                if (CurrInstr.IsModRm)
                    Code = "8B";
                else
                    Code = "A1";
            return Code;
        }

        private static string CreateModRm()
        {
            string Mod = "";
            string Reg = "";
            string Rm = "";

            switch (TableSentence.Item.Mnem)
            {
                case "CMP":
                    Reg = Convert.ToString(TableOperand.GetByNumbers(1, false).MainAttr, 2);//второй регистр
                    Reg = CheckCharCount(Reg, 3);
                    //есть сиб и смещение : нет сиба, есть смещение
                    Mod = TableOperand.TypeFirst == TableOperand.Types.Адресний_вираз ? "10" : "00";
                    Rm = TableOperand.TypeFirst == TableOperand.Types.Адресний_вираз ? "100" : "101";
                    break;
                case "JMP":
                    Reg = "101";//кусок команды
                    //есть сиб и смещение : нет сиба, есть смещение
                    Mod = TableOperand.TypeFirst == TableOperand.Types.Адресний_вираз ? "10" : "00";
                    Rm = TableOperand.TypeFirst == TableOperand.Types.Адресний_вираз ? "100" : "101";
                    break;
                case "MOV":
                    Reg = Convert.ToString(TableOperand.GetByNumbers(1, true).MainAttr, 2);//первый регистр
                    Reg = CheckCharCount(Reg, 3);
                    //есть сиб и смещение : нет сиба, есть смещение
                    Mod = TableOperand.TypeSecond == TableOperand.Types.Адресний_вираз ? "10" : "00";
                    Rm = TableOperand.TypeSecond == TableOperand.Types.Адресний_вираз ? "100" : "101";
                    break;
                case "NEG":
                    Reg = "011";//кусок команды
                    Mod = "11";//операнд - всегда регистр
                    Rm = Convert.ToString(TableOperand.GetByNumbers(1, true).MainAttr, 2);//первый регистр
                    Rm = CheckCharCount(Rm, 3);
                    break;
                case "PUSH":
                    Reg = "110";//кусок команды
                    Mod = TableOperand.TypeFirst == TableOperand.Types.Адресний_вираз ? "10" : "00";
                    Rm = TableOperand.TypeFirst == TableOperand.Types.Адресний_вираз ? "100" : "101";
                    break;
                default:
                    break;
            }
            int ModRmDex = Convert.ToInt32(Mod + Reg + Rm, 2);
            string ModRm = Convert.ToString(ModRmDex, 16).ToUpper();
            ModRm = CheckCharCount(ModRm, 2);
            return ModRm;
        }

        private static string CreateSib()
        {
            string Sib = "";//строка результата
            bool isFirst = TableOperand.TypeFirst == TableOperand.Types.Адресний_вираз ? true : false;//первый ли операнд 

            switch (TableOperand.GetByNumbers(8, isFirst).ExtraAttr)
            {//кусочек ss
                case 1:
                    Sib += "00";
                    break;
                case 2:
                    Sib += "01";
                    break;
                case 4:
                    Sib += "10";
                    break;
                case 8:
                    Sib += "11";
                    break;
                default:
                    break;
            }

            int scaledReg = TableOperand.GetByNumbers(8, isFirst).MainAttr;
            string Index = Convert.ToString(scaledReg, 2);
            Index = CheckCharCount(Index, 3);
            Sib += Index;

            int unScaledReg;
            if (TableOperand.GetByNumbers(6, isFirst).MainAttr == scaledReg)
                unScaledReg = TableOperand.GetByNumbers(7, isFirst).MainAttr;
            else
                unScaledReg = TableOperand.GetByNumbers(6, isFirst).MainAttr;

            string Base = Convert.ToString(unScaledReg, 2);
            Base = CheckCharCount(Base, 3);
            Sib += Base;//двоичный код
            int SibDex = Convert.ToInt16(Sib, 2);//десятичный код
            Sib = Convert.ToString(SibDex, 16);//16 код
            return Sib.ToUpper();
        }

        private static string CreateOffset(TableCurrInstruct CurrInstr)
        {
            string Offset = "";
            int UserNumber = 0;
            if (TableOperand.TypeFirst >= TableOperand.Types.Мітка
                && TableOperand.TypeFirst <= TableOperand.Types.Адресний_вираз)
                UserNumber = TableOperand.GetByNumbers(4, true).MainAttr;
            if (TableOperand.TypeSecond >= TableOperand.Types.Мітка
                && TableOperand.TypeSecond <= TableOperand.Types.Адресний_вираз)
                UserNumber = TableOperand.GetByNumbers(4, false).MainAttr;
            if (UserNumber > 0)
            {
                Offset = Convert.ToString(TableUser.GetByNumber(UserNumber).Offset, 16);
                Offset = CheckCharCount(Offset, 8);

                if (CurrInstr.Name == "JMP" && !CurrInstr.IsModRm)
                    Offset += " ----";
            }
            return Offset.ToUpper();
        }

        private static string CreateImmediate(TableCurrInstruct CurrInstr)
        {
            return "";
        }
        //*******************************************************************************************
        private static string CheckCharCount(string StrToCheck, int CountToBe)
        {
            string result = StrToCheck;
            int d = CountToBe - StrToCheck.Length;
            if (d > 0)
                for (int i = 0; i < d; i++)
                    result = string.Concat("0", result);
            return result;
        }
    }
}
