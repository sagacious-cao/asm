using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsmKurs
{
    class AnalysisSyntaxy
    {
        private static string Mnem;//інструкція або директива
        private static int NameOrLabelNumber;//номер позиції мітки, якщо вона є
        private static int MnemNumber;//номер позиції інструкції або директиви
        private static int[,] Operands = new int[6, 2];
        private static TableAsmWords.Types Type;//тип речення
        private static bool IsComa;

        public static void FormSentenceTable()
        {
            Mnem = "";
            NameOrLabelNumber = 0;
            MnemNumber = 0;
            Type = TableAsmWords.Types.пусто;

            for (int k = 0; k < 6; k++)
                for (int l = 0; l < 2; l++)
                    Operands[k, l] = 0;

            TableSentence.Item = null;//Обнуляем таблицу предложения в начале строки
            TableSentence.Table.Clear();
            TableOperand.ReCreate();//Обнуляем таблицы операндов в начале строки

            foreach (var lexem in TableLexeme.Items)
                if (lexem.AsmWord() != null)
                    if ((lexem.Type == TableAsmWords.Types.інструкція)
                        || (lexem.Type == TableAsmWords.Types.директива))
                    {
                        MnemNumber = lexem.Number;
                        Mnem = lexem.Name;
                        if (MnemNumber > 1)//mnem not first
                            NameOrLabelNumber = 1;
                        Type = lexem.Type;
                    }
            switch (MnemNumber)
            {
                case 0://label only possible
                    if (TableLexeme.Items[0].Type == TableAsmWords.Types.користувач
                        && TableLexeme.Items[1].Name == ":")//it IS label
                    {
                        NameOrLabelNumber = 1;
                        Mnem = TableLexeme.Items[0].Name;
                        Type = TableAsmWords.Types.мітка;
                    }
                    else
                        Errors.Add(5);//@error хз шо не удалось определить конструкцию
                    IsComa = false;
                    break;
                case 1://команда
                    IsComa = true;
                    break;
                case 2://директива
                    if (TableLexeme.Items[0].Type == TableAsmWords.Types.користувач)//имя на своём месте
                        IsComa = true;
                    else
                        Errors.Add(5);//@error хз шо не удалось определить конструкцию
                    break;
                case 3://команда с меткой в одной строке
                    if (TableLexeme.Items[0].Type == TableAsmWords.Types.користувач
                        && TableLexeme.Items[1].Name == ":")//there IS label
                        IsComa = true;
                    else
                        Errors.Add(5);//@error хз шо не удалось определить конструкцию
                    break;
                default:
                    Errors.Add(5);//@error хз шо не удалось определить конструкцию
                    break;
            }

            if (MnemNumber < TableLexeme.Count && Mnem != "LABEL" && IsComa)//mnem has operands
                ReadOperands();

            new TableSentence(Mnem, NameOrLabelNumber, MnemNumber, Operands, Type);
        }

        private static void ReadOperands()//записуєм номери і довжину операндів у таблицю
        {
            int iter = MnemNumber > 0 ? MnemNumber + 1 : NameOrLabelNumber + 1;
            int i = 0;

            while ((iter <= TableLexeme.Count) && (i < 6))
            {
                Operands[i, 0] = iter;
                for (int j = iter; j <= TableLexeme.Count; j++)
                    if (TableLexeme.GetByNumber(j).Name == ",")
                    {
                        Operands[i, 1] = j - Operands[i, 0];
                        iter = j + 1;
                        IsComa = true;
                        break;
                    }
                    else
                        IsComa = false;
                if (!IsComa)//last operand
                {
                    Operands[i, 1] = TableLexeme.Count - Operands[i, 0] + 1;
                    break;
                }
                i++;
                if ((iter <= TableLexeme.Count) && (i >= 6))
                    Errors.Add(20);//@error слишком много операндов
            }
        }

        /******************************************************************************************************/

        private static bool isAdress = false;
        private static bool isFirst = true;

        public static void DoOperands()//обробка операндів
        {
            string mnemName = TableSentence.Item.Mnem;
            int opCount;
            if (TableInstruction.GetByName(mnemName) != null)//у директиви або інструкції є операнди
                opCount = TableInstruction.GetByName(mnemName).OperandCount;
            else//інші директиви
                opCount = 0;

            if (mnemName != "ASSUME" && opCount != TableSentence.Item.OpCount)
                Errors.Add(6);//@error Невірна кількість операндів
            else
                for (int i = 0; i < TableSentence.Item.OpCount; i++)//для каждого операнда
                {
                    isFirst = (i == 0) ? true : false;
                    int j = 0;
                    while (j < TableSentence.Item.Operands[i, 1])
                    {
                        TableLexeme Lexeme = TableLexeme.GetByNumber(TableSentence.Item.Operands[i, 0] + j);
                        switch (Lexeme.Type)
                        {
                            case TableAsmWords.Types.регістр:
                                DoOpRegister(Lexeme);
                                break;
                            case TableAsmWords.Types.сегментний_регістр:
                                DoOpSegRegister(Lexeme);
                                break;
                            case TableAsmWords.Types.тип:
                                DoOpType(Lexeme);
                                break;
                            case TableAsmWords.Types.користувач:
                                DoOpName(Lexeme);
                                break;
                            case TableAsmWords.Types.символ:
                                j += DoOpSymbols(Lexeme);
                                break;
                            case TableAsmWords.Types.seg:
                                DoOpSeg(Lexeme);
                                break;
                            case TableAsmWords.Types.число:
                                j += DoOpConst();
                                break;
                            case TableAsmWords.Types.текст:
                                DoOpText();
                                break;
                            default:
                                break;
                        }
                        j++;
                    }
                    TableOperand.IdentifyOperands(isFirst);
                }
            bool result = opCount > 0 ? TableOperand.VerifyOperands(mnemName) : true;
            if (!result)
                Errors.Add(15);//@error Помилкові операнди
        }

        private static void DoOpText()
        {
            TableLexeme lex = TableLexeme.GetByNumber(TableSentence.Item.Operands[0, 0]);//первая лексема первого операнда
            int byteCount = TableInstruction.GetByName(TableLexeme.MnemName()).ByteCount;

            if (byteCount == 1)
                byteCount *= lex.Length;
            else
                Errors.Add(14);//@error недопустимий аргумент директиви
            TableOperand.GetByNumbers(11, isFirst).Modify(byteCount, -1);
            AnalysisGrammar.AddTableUser(1);
        }

        private static int DoOpConst()
        {//количество байт, выделенных под данные
            int byteCount = TableInstruction.GetByName(TableLexeme.MnemName()).ByteCount;
            int result = byteCount;
            int value;
            if (TableSentence.Item.Operands[1, 0] != 0)// есть второй операнд
                Errors.Add(20);//@error слишком много операндов
            if (TableSentence.Item.Operands[0, 1] == 1)//константа
            {
                TableLexeme lex = TableLexeme.GetByNumber(TableSentence.Item.Operands[0, 0]);//первая лексема первого операнда
                value = lex.NumberDex();
                double f = Math.Pow(2, 8 * byteCount) - 1;
                uint j = Convert.ToUInt32(f);
                if (value > j)
                    Errors.Add(2);//@error too big number
                TableOperand.GetByNumbers(10, isFirst).Modify(value, byteCount);
            }
            else//выражение
            {
                int FirstLex = TableSentence.Item.Operands[0, 0];
                int LastLex = FirstLex + TableSentence.Item.Operands[0, 1] - 1;
                int Const = Calc.DoCalc(FirstLex, LastLex);
                result = LastLex - FirstLex;
                TableOperand.GetByNumbers(10, isFirst).Modify(Const, byteCount);
            }

            AnalysisGrammar.AddTableUser(byteCount);
            return result;
        }

        private static void DoOpSeg(TableLexeme Lexeme)
        {
            TableLexeme LexemeNext = TableLexeme.GetByNumber(Lexeme.Number + 1);
            if (LexemeNext.Type != TableAsmWords.Types.мітка && LexemeNext.Type != TableAsmWords.Types.користувач)
                Errors.Add(10);//@error неправильный агрумент у Seg
            TableOperand.GetByNumbers(9, isFirst).Modify(LexemeNext.Number, -1);
        }

        private static void DoOpRegister(TableLexeme Lexeme)
        {
            int RegNum = TableRegister.GetByName(Lexeme.Name).Number;
            if (isAdress)
                if (!TableOperand.GetByNumbers(6, isFirst).IsLexemePresent)
                    TableOperand.GetByNumbers(6, isFirst).Modify(RegNum, 32);
                else
                    TableOperand.GetByNumbers(7, isFirst).Modify(RegNum, 32);
            else
                TableOperand.GetByNumbers(1, isFirst).Modify(RegNum, 32);
        }

        private static void DoOpSegRegister(TableLexeme Lexeme)
        {
            int RegNum = TableRegister.GetByName(Lexeme.Name).Number;
            TableOperand.GetByNumbers(3, isFirst).Modify(RegNum, -1);
        }

        private static void DoOpType(TableLexeme Lexeme)
        {
            switch (Lexeme.Name)
            {
                case "DWORD":
                    if (!TableOperand.GetByNumbers(2, isFirst).IsLexemePresent)
                        TableOperand.GetByNumbers(2, isFirst).Modify(4, -1);
                    break;
                case "FWORD":
                    if (!TableOperand.GetByNumbers(2, isFirst).IsLexemePresent)
                        TableOperand.GetByNumbers(2, isFirst).Modify(6, -1);
                    break;
                case "FAR":
                    if (!TableOperand.GetByNumbers(2, isFirst).IsLexemePresent)
                        TableOperand.GetByNumbers(2, isFirst).Modify(5, -1);
                    break;
                case "PTR":
                    if (!TableOperand.GetByNumbers(2, isFirst).IsLexemePresent)
                        Errors.Add(8);//@error type is missing
                    break;
                default:
                    Errors.Add(5);//@error какая-то хрень
                    break;
            }
        }

        private static void DoOpName(TableLexeme Lexeme)
        {
            TableUser user = TableUser.GetByName(Lexeme.Name);
            if (user != null)
                TableOperand.GetByNumbers(4, isFirst).Modify(user.Number, -1);
            else
                TableOperand.GetByNumbers(5, isFirst).Modify(-1, -1);
            if (TableLexeme.MnemName() == "DD")
                AnalysisGrammar.AddTableUser(4);
            else
                AnalysisGrammar.AddTableUser(-1);
        }

        private static int DoOpSymbols(TableLexeme Lexeme)
        {
            switch (Lexeme.Name)
            {
                case "[":
                    isAdress = true;
                    break;
                case "]":
                    isAdress = false;
                    break;
                case "*":
                    int EndOfConst = Lexeme.Number;
                    while (TableLexeme.GetByNumber(EndOfConst + 1).Name != "]")
                        EndOfConst++;

                    int RegNumber;
                    if (TableOperand.GetByNumbers(7, isFirst).IsLexemePresent)
                        RegNumber = TableOperand.GetByNumbers(7, isFirst).MainAttr;
                    else
                        RegNumber = TableOperand.GetByNumbers(6, isFirst).MainAttr;
                    int ConstValue = Calc.DoCalc(Lexeme.Number + 1, EndOfConst);
                    if (ConstValue == 1 || ConstValue == 2 || ConstValue == 4 || ConstValue == 8)
                        TableOperand.GetByNumbers(8, isFirst).Modify(RegNumber, ConstValue);
                    else
                        Errors.Add(9);//@error wrong multiplier

                    return EndOfConst - Lexeme.Number;//прыгаем в конец конст. выражения
                default:
                    break;
            }
            return 0;
        }
    }
}
