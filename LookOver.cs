using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;


namespace AsmKurs
{
    class LookOver
    {
        public static bool IsGoFirst;
        private static bool IsEnd;
        public static void GoFirst(int StopNumber)
        {
            IsGoFirst = true;
            IsEnd = false;

            ClearTables();

            string line = "";

            Result.Count = 0;
            if (Result.FileLength > 0)
                for (int LineNumber = 0; LineNumber < Result.FileLength; LineNumber++)
                {
                    line = File.ReadAllLines(Result.FilePath)[LineNumber];
                    Result result = new Result(LineNumber + 1, line);
                    Result.Count++;

                    AnalysisLexy.FormLexyTable(line);
                    if (TableLexeme.Count > 0)
                    {
                        AnalysisSyntaxy.FormSentenceTable();
                        if (TableSentence.Item.Type != TableAsmWords.Types.пусто)
                        {
                            int CurrentOffset = AnalysisGrammar.CalculateOffset();
                            TableSegment.IncOffset(CurrentOffset);
                            result.ModifyOffset(CurrentOffset);
                        }
                        if (TableLexeme.MnemName() == "END")
                        {
                            IsEnd = true;
                            break;
                        }
                    }
                    else//пустая строка
                    {
                        Result.Table.Rows.RemoveAt(Result.Items.Count - 1);
                        Result.Items.RemoveAt(Result.Items.Count - 1);
                        result = null;
                        Result.Count--;
                    }
                    if (LineNumber == StopNumber)
                        LineNumber += 0;//@error: doesn't do anything
                }
            if (Result.Count == 0)
                Errors.Add(16);//@error пустой исходный файл
            else if (!IsEnd)
                Errors.Add(17);//@error end is missing
        }

        private static void ClearTables()
        {
            if (TableAsmWords.Items.Count == 0)//создаем раз и навсегда
                TableAsmWords.Create();
            if (TableRegister.Items.Count == 0)
                TableRegister.Create();
            if (TableInstruction.Items.Count == 0)
                TableInstruction.Create();
            if (TableOperand.ItemsFirst.Count == 0 && TableOperand.ItemsSecond.Count == 0)
                TableOperand.Create();
            if (TableAssume.Item.Count == 0)
                new TableAssume();
            if (Errors.Templates.Count == 0)
                Errors.CreateTempl();
            Errors.Table.Clear();
            Errors.Items.Clear();
            Result.Items.Clear();
            Result.Table.Clear();
            TableSegment.Items.Clear();
            TableSegment.Table.Clear();
            TableUser.Items.Clear();
            TableUser.Table.Clear();
        }

        public static void GoSecond(int StopNumber, string ListingName)
        {
            IsGoFirst = false;
            TableAssume.Clear();

            string line = "";

            Result.Count = 0;
            if (Result.FileLength > 0)
                for (int LineNumber = 0; LineNumber < Result.FileLength; LineNumber++)
                {
                    line = File.ReadAllLines(Result.FilePath)[LineNumber];
                    Result.Count++;//доступ к текущей строке результата

                    AnalysisLexy.FormLexyTable(line);
                    if (TableLexeme.Count > 0)
                    {
                        AnalysisSyntaxy.FormSentenceTable();
                        if (TableSentence.Item.Type != TableAsmWords.Types.пусто)
                        {
                            int CurrentOffset = AnalysisGrammar.CalculateOffset();

                            if (CurrentOffset != Result.Current().LineSize)
                                Errors.Add(19);//@error невідповідність зміщень першого та другого переглядів

                            if (Result.Current().Error == null)//ошибок нет
                            {
                                string CurrentCommand = AnalysisGrammar.CommandCreate();
                                Result.Items[Result.Count - 1].ModifyCode(CurrentCommand);//формирование команды
                            }
                        }
                    }
                    else//пустая строка
                        Result.Count--;

                    if (LineNumber == StopNumber)
                        LineNumber += 0;//@error: doesn't do anything
                }
            //формирование файла листинга
            try
            {
                Result.CreateListing(ListingName);
            }
            catch
            {
                MessageBox.Show("Не вдалося сформувати файл лістингу");
            }
            Result.FileLength = 0;
        }
    }
}
