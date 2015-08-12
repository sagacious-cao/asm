namespace AsmKurs
{
    partial class FormMain
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.tbSourseFileName = new System.Windows.Forms.TextBox();
            this.btnKlick = new System.Windows.Forms.Button();
            this.dgTable = new System.Windows.Forms.DataGridView();
            this.tbLineNumber = new System.Windows.Forms.TextBox();
            this.btnAsmWords = new System.Windows.Forms.Button();
            this.btnLexemes = new System.Windows.Forms.Button();
            this.rtbCopyFile = new System.Windows.Forms.RichTextBox();
            this.btnSentence = new System.Windows.Forms.Button();
            this.lblAllert = new System.Windows.Forms.Label();
            this.lblTableName = new System.Windows.Forms.Label();
            this.btnSegments = new System.Windows.Forms.Button();
            this.btnAssume = new System.Windows.Forms.Button();
            this.btnInstruct = new System.Windows.Forms.Button();
            this.btnOperand1 = new System.Windows.Forms.Button();
            this.btnOperand2 = new System.Windows.Forms.Button();
            this.btnUser = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnResult = new System.Windows.Forms.Button();
            this.tbResultFileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnErrors = new System.Windows.Forms.Button();
            this.btnSwitchMode = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnErrorsList = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgTable)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(606, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ім\'я вихідного файлу *.asm:";
            // 
            // tbSourseFileName
            // 
            this.tbSourseFileName.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbSourseFileName.Location = new System.Drawing.Point(609, 32);
            this.tbSourseFileName.Name = "tbSourseFileName";
            this.tbSourseFileName.Size = new System.Drawing.Size(148, 20);
            this.tbSourseFileName.TabIndex = 2;
            this.tbSourseFileName.Text = "kurs";
            // 
            // btnKlick
            // 
            this.btnKlick.BackColor = System.Drawing.Color.MediumAquamarine;
            this.btnKlick.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKlick.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnKlick.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnKlick.Location = new System.Drawing.Point(609, 105);
            this.btnKlick.Name = "btnKlick";
            this.btnKlick.Size = new System.Drawing.Size(148, 43);
            this.btnKlick.TabIndex = 1;
            this.btnKlick.Text = "Обробити";
            this.btnKlick.UseVisualStyleBackColor = false;
            this.btnKlick.Click += new System.EventHandler(this.btnKlick_Click);
            // 
            // dgTable
            // 
            this.dgTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgTable.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTable.Location = new System.Drawing.Point(5, 16);
            this.dgTable.Name = "dgTable";
            this.dgTable.Size = new System.Drawing.Size(595, 238);
            this.dgTable.TabIndex = 5;
            // 
            // tbLineNumber
            // 
            this.tbLineNumber.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbLineNumber.Location = new System.Drawing.Point(525, 261);
            this.tbLineNumber.Name = "tbLineNumber";
            this.tbLineNumber.Size = new System.Drawing.Size(36, 20);
            this.tbLineNumber.TabIndex = 6;
            this.tbLineNumber.Text = "-1";
            this.tbLineNumber.Visible = false;
            // 
            // btnAsmWords
            // 
            this.btnAsmWords.BackColor = System.Drawing.Color.Honeydew;
            this.btnAsmWords.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAsmWords.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAsmWords.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnAsmWords.Location = new System.Drawing.Point(639, 156);
            this.btnAsmWords.Name = "btnAsmWords";
            this.btnAsmWords.Size = new System.Drawing.Size(94, 23);
            this.btnAsmWords.TabIndex = 7;
            this.btnAsmWords.Text = "Зарезервовані";
            this.btnAsmWords.UseVisualStyleBackColor = false;
            this.btnAsmWords.Click += new System.EventHandler(this.btnAsmWords_Click);
            // 
            // btnLexemes
            // 
            this.btnLexemes.BackColor = System.Drawing.Color.Honeydew;
            this.btnLexemes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLexemes.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnLexemes.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnLexemes.Location = new System.Drawing.Point(639, 417);
            this.btnLexemes.Name = "btnLexemes";
            this.btnLexemes.Size = new System.Drawing.Size(94, 23);
            this.btnLexemes.TabIndex = 8;
            this.btnLexemes.Text = "Лексеми";
            this.btnLexemes.UseVisualStyleBackColor = false;
            this.btnLexemes.Visible = false;
            this.btnLexemes.Click += new System.EventHandler(this.btnLexemes_Click);
            // 
            // rtbCopyFile
            // 
            this.rtbCopyFile.BackColor = System.Drawing.SystemColors.Window;
            this.rtbCopyFile.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rtbCopyFile.Location = new System.Drawing.Point(5, 289);
            this.rtbCopyFile.Name = "rtbCopyFile";
            this.rtbCopyFile.Size = new System.Drawing.Size(595, 238);
            this.rtbCopyFile.TabIndex = 4;
            this.rtbCopyFile.Text = "";
            this.rtbCopyFile.WordWrap = false;
            // 
            // btnSentence
            // 
            this.btnSentence.BackColor = System.Drawing.Color.Honeydew;
            this.btnSentence.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSentence.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSentence.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnSentence.Location = new System.Drawing.Point(639, 446);
            this.btnSentence.Name = "btnSentence";
            this.btnSentence.Size = new System.Drawing.Size(94, 23);
            this.btnSentence.TabIndex = 9;
            this.btnSentence.Text = "Речення";
            this.btnSentence.UseVisualStyleBackColor = false;
            this.btnSentence.Visible = false;
            this.btnSentence.Click += new System.EventHandler(this.btnSentence_Click);
            // 
            // lblAllert
            // 
            this.lblAllert.AutoSize = true;
            this.lblAllert.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblAllert.Location = new System.Drawing.Point(2, 264);
            this.lblAllert.Name = "lblAllert";
            this.lblAllert.Size = new System.Drawing.Size(0, 14);
            this.lblAllert.TabIndex = 10;
            // 
            // lblTableName
            // 
            this.lblTableName.AutoSize = true;
            this.lblTableName.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTableName.Location = new System.Drawing.Point(2, 0);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(0, 14);
            this.lblTableName.TabIndex = 11;
            // 
            // btnSegments
            // 
            this.btnSegments.BackColor = System.Drawing.Color.Honeydew;
            this.btnSegments.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSegments.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSegments.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnSegments.Location = new System.Drawing.Point(639, 185);
            this.btnSegments.Name = "btnSegments";
            this.btnSegments.Size = new System.Drawing.Size(94, 23);
            this.btnSegments.TabIndex = 12;
            this.btnSegments.Text = "Сегменти";
            this.btnSegments.UseVisualStyleBackColor = false;
            this.btnSegments.Click += new System.EventHandler(this.btnSegments_Click);
            // 
            // btnAssume
            // 
            this.btnAssume.BackColor = System.Drawing.Color.Honeydew;
            this.btnAssume.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAssume.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAssume.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnAssume.Location = new System.Drawing.Point(639, 214);
            this.btnAssume.Name = "btnAssume";
            this.btnAssume.Size = new System.Drawing.Size(94, 23);
            this.btnAssume.TabIndex = 13;
            this.btnAssume.Text = "Assume";
            this.btnAssume.UseVisualStyleBackColor = false;
            this.btnAssume.Click += new System.EventHandler(this.btnAssume_Click);
            // 
            // btnInstruct
            // 
            this.btnInstruct.BackColor = System.Drawing.Color.Honeydew;
            this.btnInstruct.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInstruct.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnInstruct.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnInstruct.Location = new System.Drawing.Point(639, 243);
            this.btnInstruct.Name = "btnInstruct";
            this.btnInstruct.Size = new System.Drawing.Size(94, 23);
            this.btnInstruct.TabIndex = 14;
            this.btnInstruct.Text = "Інструкції";
            this.btnInstruct.UseVisualStyleBackColor = false;
            this.btnInstruct.Click += new System.EventHandler(this.btnInstruct_Click);
            // 
            // btnOperand1
            // 
            this.btnOperand1.BackColor = System.Drawing.Color.Honeydew;
            this.btnOperand1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOperand1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnOperand1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnOperand1.Location = new System.Drawing.Point(639, 475);
            this.btnOperand1.Name = "btnOperand1";
            this.btnOperand1.Size = new System.Drawing.Size(94, 23);
            this.btnOperand1.TabIndex = 15;
            this.btnOperand1.Text = "Операнд 1";
            this.btnOperand1.UseVisualStyleBackColor = false;
            this.btnOperand1.Visible = false;
            this.btnOperand1.Click += new System.EventHandler(this.btnOperand1_Click);
            // 
            // btnOperand2
            // 
            this.btnOperand2.BackColor = System.Drawing.Color.Honeydew;
            this.btnOperand2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOperand2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnOperand2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnOperand2.Location = new System.Drawing.Point(639, 504);
            this.btnOperand2.Name = "btnOperand2";
            this.btnOperand2.Size = new System.Drawing.Size(94, 23);
            this.btnOperand2.TabIndex = 16;
            this.btnOperand2.Text = "Операнд 2";
            this.btnOperand2.UseVisualStyleBackColor = false;
            this.btnOperand2.Visible = false;
            this.btnOperand2.Click += new System.EventHandler(this.btnOperand2_Click);
            // 
            // btnUser
            // 
            this.btnUser.BackColor = System.Drawing.Color.Honeydew;
            this.btnUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUser.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnUser.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnUser.Location = new System.Drawing.Point(639, 272);
            this.btnUser.Name = "btnUser";
            this.btnUser.Size = new System.Drawing.Size(94, 23);
            this.btnUser.TabIndex = 17;
            this.btnUser.Text = "Користувацькі";
            this.btnUser.UseVisualStyleBackColor = false;
            this.btnUser.Click += new System.EventHandler(this.btnUser_Click);
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.Honeydew;
            this.btnRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegister.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRegister.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnRegister.Location = new System.Drawing.Point(639, 301);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(94, 23);
            this.btnRegister.TabIndex = 18;
            this.btnRegister.Text = "Регістри";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnResult
            // 
            this.btnResult.BackColor = System.Drawing.Color.Honeydew;
            this.btnResult.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnResult.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnResult.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnResult.Location = new System.Drawing.Point(639, 330);
            this.btnResult.Name = "btnResult";
            this.btnResult.Size = new System.Drawing.Size(94, 23);
            this.btnResult.TabIndex = 19;
            this.btnResult.Text = "Результат";
            this.btnResult.UseVisualStyleBackColor = false;
            this.btnResult.Click += new System.EventHandler(this.btnResult_Click);
            // 
            // tbResultFileName
            // 
            this.tbResultFileName.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbResultFileName.Location = new System.Drawing.Point(609, 75);
            this.tbResultFileName.Name = "tbResultFileName";
            this.tbResultFileName.Size = new System.Drawing.Size(148, 20);
            this.tbResultFileName.TabIndex = 21;
            this.tbResultFileName.Text = "kurs";
            this.tbResultFileName.Enter += new System.EventHandler(this.tbResultFileName_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(606, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 14);
            this.label2.TabIndex = 20;
            this.label2.Text = "Ім\'я файлу лістингу *.lst:";
            // 
            // btnErrors
            // 
            this.btnErrors.BackColor = System.Drawing.Color.Honeydew;
            this.btnErrors.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnErrors.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnErrors.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnErrors.Location = new System.Drawing.Point(639, 359);
            this.btnErrors.Name = "btnErrors";
            this.btnErrors.Size = new System.Drawing.Size(94, 23);
            this.btnErrors.TabIndex = 22;
            this.btnErrors.Text = "Повідомлення";
            this.btnErrors.UseVisualStyleBackColor = false;
            this.btnErrors.Click += new System.EventHandler(this.btnErrors_Click);
            // 
            // btnSwitchMode
            // 
            this.btnSwitchMode.BackColor = System.Drawing.Color.Honeydew;
            this.btnSwitchMode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSwitchMode.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSwitchMode.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnSwitchMode.Location = new System.Drawing.Point(567, 260);
            this.btnSwitchMode.Name = "btnSwitchMode";
            this.btnSwitchMode.Size = new System.Drawing.Size(33, 23);
            this.btnSwitchMode.TabIndex = 23;
            this.btnSwitchMode.UseVisualStyleBackColor = false;
            this.btnSwitchMode.Click += new System.EventHandler(this.btnSwitchMode_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.Controls.Add(this.btnErrorsList);
            this.panel1.Controls.Add(this.btnSwitchMode);
            this.panel1.Controls.Add(this.btnErrors);
            this.panel1.Controls.Add(this.tbResultFileName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnResult);
            this.panel1.Controls.Add(this.btnRegister);
            this.panel1.Controls.Add(this.btnUser);
            this.panel1.Controls.Add(this.btnOperand2);
            this.panel1.Controls.Add(this.btnOperand1);
            this.panel1.Controls.Add(this.btnInstruct);
            this.panel1.Controls.Add(this.btnAssume);
            this.panel1.Controls.Add(this.btnSegments);
            this.panel1.Controls.Add(this.lblTableName);
            this.panel1.Controls.Add(this.lblAllert);
            this.panel1.Controls.Add(this.btnSentence);
            this.panel1.Controls.Add(this.btnLexemes);
            this.panel1.Controls.Add(this.btnAsmWords);
            this.panel1.Controls.Add(this.tbLineNumber);
            this.panel1.Controls.Add(this.dgTable);
            this.panel1.Controls.Add(this.rtbCopyFile);
            this.panel1.Controls.Add(this.btnKlick);
            this.panel1.Controls.Add(this.tbSourseFileName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(763, 537);
            this.panel1.TabIndex = 24;
            // 
            // btnErrorsList
            // 
            this.btnErrorsList.BackColor = System.Drawing.Color.Honeydew;
            this.btnErrorsList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnErrorsList.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnErrorsList.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnErrorsList.Location = new System.Drawing.Point(639, 388);
            this.btnErrorsList.Name = "btnErrorsList";
            this.btnErrorsList.Size = new System.Drawing.Size(94, 23);
            this.btnErrorsList.TabIndex = 24;
            this.btnErrorsList.Text = "Помилки";
            this.btnErrorsList.UseVisualStyleBackColor = false;
            this.btnErrorsList.Click += new System.EventHandler(this.btnErrorsList_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Ivory;
            this.ClientSize = new System.Drawing.Size(787, 561);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(795, 565);
            this.Name = "FormMain";
            this.Text = "Курсова робота: транслятор. Гоменюк Ніна, ЗКСМ-11";
            ((System.ComponentModel.ISupportInitialize)(this.dgTable)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSourseFileName;
        private System.Windows.Forms.Button btnKlick;
        private System.Windows.Forms.DataGridView dgTable;
        private System.Windows.Forms.TextBox tbLineNumber;
        private System.Windows.Forms.Button btnAsmWords;
        private System.Windows.Forms.Button btnLexemes;
        private System.Windows.Forms.RichTextBox rtbCopyFile;
        private System.Windows.Forms.Button btnSentence;
        private System.Windows.Forms.Label lblAllert;
        private System.Windows.Forms.Label lblTableName;
        private System.Windows.Forms.Button btnSegments;
        private System.Windows.Forms.Button btnAssume;
        private System.Windows.Forms.Button btnInstruct;
        private System.Windows.Forms.Button btnOperand1;
        private System.Windows.Forms.Button btnOperand2;
        private System.Windows.Forms.Button btnUser;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnResult;
        private System.Windows.Forms.TextBox tbResultFileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnErrors;
        private System.Windows.Forms.Button btnSwitchMode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnErrorsList;
    }
}

