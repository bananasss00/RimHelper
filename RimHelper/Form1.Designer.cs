namespace RimHelper
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.tbFilter1 = new System.Windows.Forms.TextBox();
            this.tbFilter2 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.отображениеКолонокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.скрытьПустыеСтолбцыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAllTabsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.корзинаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьВКорзинуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьИзКорзиныToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.очиститьКорзинуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.инцидентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pawnHediffsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.дампМатериаловИХарактеристикToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.дампОружияИОдеждыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.injectDllAndRunToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gCCollectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.языкToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cbTabs = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnResetCache = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Offline";
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeColumns = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv.Location = new System.Drawing.Point(0, 53);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.ShowEditingIcon = false;
            this.dgv.Size = new System.Drawing.Size(988, 483);
            this.dgv.TabIndex = 24;
            this.dgv.SelectionChanged += new System.EventHandler(this.dgv_SelectionChanged);
            // 
            // tbFilter1
            // 
            this.tbFilter1.Location = new System.Drawing.Point(241, 2);
            this.tbFilter1.Name = "tbFilter1";
            this.tbFilter1.Size = new System.Drawing.Size(55, 20);
            this.tbFilter1.TabIndex = 10;
            this.tbFilter1.Text = "0";
            // 
            // tbFilter2
            // 
            this.tbFilter2.Location = new System.Drawing.Point(302, 2);
            this.tbFilter2.Name = "tbFilter2";
            this.tbFilter2.Size = new System.Drawing.Size(42, 20);
            this.tbFilter2.TabIndex = 11;
            this.tbFilter2.Text = "99999";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(71, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(165, 21);
            this.comboBox1.TabIndex = 13;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Location = new System.Drawing.Point(12, 505);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.отображениеКолонокToolStripMenuItem,
            this.скрытьПустыеСтолбцыToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.корзинаToolStripMenuItem,
            this.debugToolStripMenuItem,
            this.языкToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(988, 24);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // отображениеКолонокToolStripMenuItem
            // 
            this.отображениеКолонокToolStripMenuItem.Name = "отображениеКолонокToolStripMenuItem";
            this.отображениеКолонокToolStripMenuItem.Size = new System.Drawing.Size(145, 20);
            this.отображениеКолонокToolStripMenuItem.Text = "Отображение колонок";
            this.отображениеКолонокToolStripMenuItem.Click += new System.EventHandler(this.отображениеКолонокToolStripMenuItem_Click);
            // 
            // скрытьПустыеСтолбцыToolStripMenuItem
            // 
            this.скрытьПустыеСтолбцыToolStripMenuItem.Name = "скрытьПустыеСтолбцыToolStripMenuItem";
            this.скрытьПустыеСтолбцыToolStripMenuItem.Size = new System.Drawing.Size(153, 20);
            this.скрытьПустыеСтолбцыToolStripMenuItem.Text = "Скрыть пустые столбцы";
            this.скрытьПустыеСтолбцыToolStripMenuItem.Click += new System.EventHandler(this.скрытьПустыеСтолбцыToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportTabToolStripMenuItem,
            this.exportAllTabsToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // exportTabToolStripMenuItem
            // 
            this.exportTabToolStripMenuItem.Name = "exportTabToolStripMenuItem";
            this.exportTabToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.exportTabToolStripMenuItem.Text = "ExportTab";
            this.exportTabToolStripMenuItem.Click += new System.EventHandler(this.exportTabToolStripMenuItem_Click);
            // 
            // exportAllTabsToolStripMenuItem
            // 
            this.exportAllTabsToolStripMenuItem.Name = "exportAllTabsToolStripMenuItem";
            this.exportAllTabsToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.exportAllTabsToolStripMenuItem.Text = "ExportAllTabs";
            this.exportAllTabsToolStripMenuItem.Click += new System.EventHandler(this.exportAllTabsToolStripMenuItem_Click);
            // 
            // корзинаToolStripMenuItem
            // 
            this.корзинаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.добавитьВКорзинуToolStripMenuItem,
            this.удалитьИзКорзиныToolStripMenuItem,
            this.очиститьКорзинуToolStripMenuItem});
            this.корзинаToolStripMenuItem.Name = "корзинаToolStripMenuItem";
            this.корзинаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.корзинаToolStripMenuItem.Text = "Корзина";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.CheckOnClick = true;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(184, 22);
            this.toolStripMenuItem1.Text = "Корзина";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // добавитьВКорзинуToolStripMenuItem
            // 
            this.добавитьВКорзинуToolStripMenuItem.Name = "добавитьВКорзинуToolStripMenuItem";
            this.добавитьВКорзинуToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.добавитьВКорзинуToolStripMenuItem.Text = "Добавить в корзину";
            this.добавитьВКорзинуToolStripMenuItem.Click += new System.EventHandler(this.добавитьВКорзинуToolStripMenuItem_Click);
            // 
            // удалитьИзКорзиныToolStripMenuItem
            // 
            this.удалитьИзКорзиныToolStripMenuItem.Name = "удалитьИзКорзиныToolStripMenuItem";
            this.удалитьИзКорзиныToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.удалитьИзКорзиныToolStripMenuItem.Text = "Удалить из корзины";
            this.удалитьИзКорзиныToolStripMenuItem.Visible = false;
            this.удалитьИзКорзиныToolStripMenuItem.Click += new System.EventHandler(this.удалитьИзКорзиныToolStripMenuItem_Click);
            // 
            // очиститьКорзинуToolStripMenuItem
            // 
            this.очиститьКорзинуToolStripMenuItem.Name = "очиститьКорзинуToolStripMenuItem";
            this.очиститьКорзинуToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.очиститьКорзинуToolStripMenuItem.Text = "Очистить корзину";
            this.очиститьКорзинуToolStripMenuItem.Click += new System.EventHandler(this.очиститьКорзинуToolStripMenuItem_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.инцидентыToolStripMenuItem,
            this.pawnHediffsToolStripMenuItem,
            this.дампМатериаловИХарактеристикToolStripMenuItem,
            this.дампОружияИОдеждыToolStripMenuItem,
            this.injectDllAndRunToolStripMenuItem,
            this.gCCollectToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.debugToolStripMenuItem.Text = "Debug";
            this.debugToolStripMenuItem.Click += new System.EventHandler(this.debugToolStripMenuItem_Click);
            // 
            // инцидентыToolStripMenuItem
            // 
            this.инцидентыToolStripMenuItem.Enabled = false;
            this.инцидентыToolStripMenuItem.Name = "инцидентыToolStripMenuItem";
            this.инцидентыToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.инцидентыToolStripMenuItem.Text = "Инциденты";
            this.инцидентыToolStripMenuItem.Click += new System.EventHandler(this.инцидентыToolStripMenuItem_Click_1);
            // 
            // pawnHediffsToolStripMenuItem
            // 
            this.pawnHediffsToolStripMenuItem.Enabled = false;
            this.pawnHediffsToolStripMenuItem.Name = "pawnHediffsToolStripMenuItem";
            this.pawnHediffsToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.pawnHediffsToolStripMenuItem.Text = "PawnHeddifs";
            this.pawnHediffsToolStripMenuItem.Click += new System.EventHandler(this.pawnHediffsToolStripMenuItem_Click_1);
            // 
            // дампМатериаловИХарактеристикToolStripMenuItem
            // 
            this.дампМатериаловИХарактеристикToolStripMenuItem.Enabled = false;
            this.дампМатериаловИХарактеристикToolStripMenuItem.Name = "дампМатериаловИХарактеристикToolStripMenuItem";
            this.дампМатериаловИХарактеристикToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.дампМатериаловИХарактеристикToolStripMenuItem.Text = "Дамп материалов и характеристик";
            this.дампМатериаловИХарактеристикToolStripMenuItem.Visible = false;
            this.дампМатериаловИХарактеристикToolStripMenuItem.Click += new System.EventHandler(this.дампМатериаловИХарактеристикToolStripMenuItem_Click);
            // 
            // дампОружияИОдеждыToolStripMenuItem
            // 
            this.дампОружияИОдеждыToolStripMenuItem.Enabled = false;
            this.дампОружияИОдеждыToolStripMenuItem.Name = "дампОружияИОдеждыToolStripMenuItem";
            this.дампОружияИОдеждыToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.дампОружияИОдеждыToolStripMenuItem.Text = "Дамп оружия и одежды";
            this.дампОружияИОдеждыToolStripMenuItem.Visible = false;
            this.дампОружияИОдеждыToolStripMenuItem.Click += new System.EventHandler(this.дампОружияИОдеждыToolStripMenuItem_Click);
            // 
            // injectDllAndRunToolStripMenuItem
            // 
            this.injectDllAndRunToolStripMenuItem.Enabled = false;
            this.injectDllAndRunToolStripMenuItem.Name = "injectDllAndRunToolStripMenuItem";
            this.injectDllAndRunToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.injectDllAndRunToolStripMenuItem.Text = "InjectDll and Run";
            this.injectDllAndRunToolStripMenuItem.Click += new System.EventHandler(this.injectDllAndRunToolStripMenuItem_Click);
            // 
            // gCCollectToolStripMenuItem
            // 
            this.gCCollectToolStripMenuItem.Enabled = false;
            this.gCCollectToolStripMenuItem.Name = "gCCollectToolStripMenuItem";
            this.gCCollectToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.gCCollectToolStripMenuItem.Text = "GC.Collect()";
            this.gCCollectToolStripMenuItem.Click += new System.EventHandler(this.gCCollectToolStripMenuItem_Click);
            // 
            // языкToolStripMenuItem
            // 
            this.языкToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.языкToolStripMenuItem.Name = "языкToolStripMenuItem";
            this.языкToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.языкToolStripMenuItem.Text = "Язык";
            // 
            // cbTabs
            // 
            this.cbTabs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTabs.Enabled = false;
            this.cbTabs.FormattingEnabled = true;
            this.cbTabs.Location = new System.Drawing.Point(449, 4);
            this.cbTabs.Name = "cbTabs";
            this.cbTabs.Size = new System.Drawing.Size(165, 21);
            this.cbTabs.TabIndex = 20;
            this.cbTabs.SelectedIndexChanged += new System.EventHandler(this.cbTabs_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(350, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(31, 20);
            this.button1.TabIndex = 21;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnResetCache
            // 
            this.btnResetCache.Location = new System.Drawing.Point(620, 4);
            this.btnResetCache.Name = "btnResetCache";
            this.btnResetCache.Size = new System.Drawing.Size(21, 21);
            this.btnResetCache.TabIndex = 22;
            this.btnResetCache.Text = "X";
            this.btnResetCache.UseVisualStyleBackColor = true;
            this.btnResetCache.Click += new System.EventHandler(this.btnResetCache_Click);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 536);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(988, 10);
            this.splitter1.TabIndex = 23;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tbFilter1);
            this.panel1.Controls.Add(this.btnResetCache);
            this.panel1.Controls.Add(this.tbFilter2);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.cbTabs);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(988, 29);
            this.panel1.TabIndex = 9;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(664, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(117, 23);
            this.button4.TabIndex = 25;
            this.button4.Text = "Выбор материала";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(387, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(21, 21);
            this.button3.TabIndex = 24;
            this.button3.Text = "X";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(787, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 23);
            this.button2.TabIndex = 23;
            this.button2.Text = "Подбор одежды";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox4
            // 
            this.textBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox4.Location = new System.Drawing.Point(0, 546);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox4.Size = new System.Drawing.Size(988, 75);
            this.textBox4.TabIndex = 26;
            // 
            // Form1
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 621);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "RimHelper v1.0";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.TextBox tbFilter1;
        private System.Windows.Forms.TextBox tbFilter2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ComboBox cbTabs;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem отображениеКолонокToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem корзинаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem добавитьВКорзинуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьИзКорзиныToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem очиститьКорзинуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem дампМатериаловИХарактеристикToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem скрытьПустыеСтолбцыToolStripMenuItem;
        private System.Windows.Forms.Button btnResetCache;
        private System.Windows.Forms.ToolStripMenuItem языкToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem дампОружияИОдеждыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem injectDllAndRunToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem инцидентыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pawnHediffsToolStripMenuItem;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.ToolStripMenuItem exportTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportAllTabsToolStripMenuItem;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem gCCollectToolStripMenuItem;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

