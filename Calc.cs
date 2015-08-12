using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsmKurs
{
    class Calc
    {
        private static List<int> DataStek = new List<int>();
        private static List<string> OpStek = new List<string>();

        private static int Prior(string oper)
        {
            switch (oper)
            {
                case ")":
                case "(":
                    return 1;
                case "*":
                    return 3;
                default:
                    return 2;
            }
        }

        public static int DoCalc(int FirstLex, int LastLex)
        {
            DataStek.Clear();
            int i = FirstLex;
            while (i <= LastLex)
            {
                TableLexeme CurrLex = TableLexeme.Items[i - 1];
                if (CurrLex.Type == TableAsmWords.Types.число)
                {
                    DataStek.Add(CurrLex.NumberDex());
                    i++;
                }
                else
                    if (CurrLex.Type == TableAsmWords.Types.символ)
                        if (OpStek.Count == 0 || CurrLex.Name == "(" || Prior(CurrLex.Name) > Prior(OpStek[OpStek.Count - 1]))
                        {
                            OpStek.Add(CurrLex.Name);
                            i++;
                        }
                        else
                            if (CurrLex.Name == ")")
                            {
                                while (DoCount() != "(")
                                    ;
                                i++;
                            }
                            else
                                DoCount();
                    else
                        i++;
            }
            while (OpStek.Count > 0)
                DoCount();
            if (DataStek.Count > 1)
                Errors.Add(11);//@error невірний абсолютний вираз
            return DataStek[0];
        }

        private static string DoCount()
        {
            string CurrOper = "";
            int FirstNum;
            int SecondNum;
            int Length;

            try
            {
                Length = OpStek.Count - 1;
                CurrOper = OpStek[Length];
                OpStek.RemoveAt(Length);
                if (CurrOper == "(")
                    return CurrOper;

                Length = DataStek.Count - 1;
                SecondNum = DataStek[Length];
                DataStek.RemoveAt(Length);

                Length--;
                FirstNum = DataStek[Length];
                DataStek.RemoveAt(Length);

                switch (CurrOper)
                {
                    case "+":
                        FirstNum += SecondNum;
                        break;
                    case "-":
                        FirstNum -= SecondNum;
                        break;
                    case "*":
                        FirstNum *= SecondNum;
                        break;
                    default:
                        Errors.Add(11);//@error невірний абсолютний вираз
                        break;
                }
                DataStek.Add(FirstNum);
            }
            catch
            {
                Errors.Add(11);//@error невірний абсолютний вираз
            }
            return CurrOper;
        }
    }
}
