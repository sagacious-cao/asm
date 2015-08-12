using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AsmKurs
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void btnKlick_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbResultFileName.Text))
            {
                MessageBox.Show("Введіть назву файлу лістингу");
                return;
            }

            lblAllert.Text = "";
            string FileName = tbSourseFileName.Text.ToString() + ".asm";
            string ListingName = tbResultFileName.Text.ToString() + ".lst";
            int StopNumber = Convert.ToInt32(tbLineNumber.Text) - 1;
            tbLineNumber.Text = (StopNumber + 2).ToString();

            Result.OpenFile(FileName);
            LookOver.GoFirst(StopNumber);
            LookOver.GoSecond(StopNumber, ListingName);

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string ListingPath = Path.Combine(basePath, ListingName);
            rtbCopyFile.Text = File.ReadAllText(ListingPath);

            dgTable.DataSource = Errors.Table;
            lblTableName.Text = "Таблиця помилок:";

        }

        private void btnAsmWords_Click(object sender, EventArgs e)
        {
            lblAllert.Text = "";
            lblTableName.Text = "Таблиця зарезервованих ідентифікаторів:";
            dgTable.DataSource = TableAsmWords.Table;
        }

        private void btnLexemes_Click(object sender, EventArgs e)
        {
            lblAllert.Text = "";
            if (TableLexeme.Count > 0)
            {
                lblTableName.Text = "Таблиця лексем:";
                dgTable.DataSource = TableLexeme.Table;
            }
            else
                lblAllert.Text = "Немає лексем";
        }

        private void btnSentence_Click(object sender, EventArgs e)
        {
            lblAllert.Text = "";
            if (TableLexeme.Count == 0)
                lblAllert.Text = "Немає лексем";
            else
            {
                lblTableName.Text = "Таблиця структури речення:";
                dgTable.DataSource = TableSentence.Table;
            }
        }

        private void btnSegments_Click(object sender, EventArgs e)
        {
            lblAllert.Text = "";
            lblTableName.Text = "Таблиця сегментів:";
            dgTable.DataSource = TableSegment.Table;
        }

        private void btnAssume_Click(object sender, EventArgs e)
        {
            lblAllert.Text = "";
            lblTableName.Text = "Таблиця Assume:";
            dgTable.DataSource = TableAssume.Table;
        }

        private void btnInstruct_Click(object sender, EventArgs e)
        {
            lblAllert.Text = "";
            lblTableName.Text = "Таблиця інструкцій:";
            dgTable.DataSource = TableInstruction.Table;
        }

        private void btnOperand1_Click(object sender, EventArgs e)
        {
            lblAllert.Text = "";
            lblTableName.Text = "Таблиця першого операнду:";
            dgTable.DataSource = TableOperand.TableFirst;
        }

        private void btnOperand2_Click(object sender, EventArgs e)
        {
            lblAllert.Text = "";
            lblTableName.Text = "Таблиця другого операнду:";
            dgTable.DataSource = TableOperand.TableSecond;
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            lblAllert.Text = "";
            lblTableName.Text = "Таблиця ідентифікаторів користувача:";
            dgTable.DataSource = TableUser.Table;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            lblAllert.Text = "";
            lblTableName.Text = "Таблиця регістрів:";
            dgTable.DataSource = TableRegister.Table;
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            lblAllert.Text = "";
            lblTableName.Text = "Таблиця результату:";
            dgTable.DataSource = Result.Table;
        }

        private void tbResultFileName_Enter(object sender, EventArgs e)
        {
            tbResultFileName.Text = tbSourseFileName.Text;
        }

        private void btnErrors_Click(object sender, EventArgs e)
        {
            lblAllert.Text = "";
            lblTableName.Text = "Таблиця помилок:";
            dgTable.DataSource = Errors.Table;
        }

        private void btnSwitchMode_Click(object sender, EventArgs e)
        {
            bool isVis = !btnLexemes.Visible;
            btnLexemes.Visible = isVis;
            btnSentence.Visible = isVis;
            btnOperand1.Visible = isVis;
            btnOperand2.Visible = isVis;
            tbLineNumber.Visible = isVis;
            btnSwitchMode.Text = isVis ? "Ввімкнути режим користування" : "Ввімкнути режим розробки";
        }

        private void btnErrorsList_Click(object sender, EventArgs e)
        {
            lblAllert.Text = "";
            lblTableName.Text = "Таблиця шаблонів помилок:";
            dgTable.DataSource = Errors.TempTable;
        }
    }
}
