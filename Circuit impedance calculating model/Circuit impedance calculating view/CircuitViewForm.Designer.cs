namespace Circuit_impedance_calculating_view
{
    partial class CircuitViewForm
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
            this.circuitViewGroupBox = new System.Windows.Forms.GroupBox();
            this.circuitView = new System.Windows.Forms.PictureBox();
            this.circuitsGroupBox = new System.Windows.Forms.GroupBox();
            this.circuitsListBox = new System.Windows.Forms.ListBox();
            this.changeElementsValueButton = new System.Windows.Forms.Button();
            this.calculateImpedanceButton = new System.Windows.Forms.Button();
            this.impedanceGridView = new System.Windows.Forms.DataGridView();
            this.frequency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.impedance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.circuitListTab = new System.Windows.Forms.TabPage();
            this.circuitElementsValues = new System.Windows.Forms.TabPage();
            this.circuitViewGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.circuitView)).BeginInit();
            this.circuitsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.impedanceGridView)).BeginInit();
            this.tabControl.SuspendLayout();
            this.circuitListTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // circuitViewGroupBox
            // 
            this.circuitViewGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.circuitViewGroupBox.Controls.Add(this.circuitView);
            this.circuitViewGroupBox.Location = new System.Drawing.Point(12, 12);
            this.circuitViewGroupBox.Name = "circuitViewGroupBox";
            this.circuitViewGroupBox.Size = new System.Drawing.Size(564, 446);
            this.circuitViewGroupBox.TabIndex = 0;
            this.circuitViewGroupBox.TabStop = false;
            this.circuitViewGroupBox.Text = "Отрисовка схемы";
            // 
            // circuitView
            // 
            this.circuitView.BackColor = System.Drawing.Color.White;
            this.circuitView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.circuitView.Location = new System.Drawing.Point(3, 16);
            this.circuitView.Name = "circuitView";
            this.circuitView.Size = new System.Drawing.Size(558, 427);
            this.circuitView.TabIndex = 0;
            this.circuitView.TabStop = false;
            // 
            // circuitsGroupBox
            // 
            this.circuitsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.circuitsGroupBox.Controls.Add(this.tabControl);
            this.circuitsGroupBox.Controls.Add(this.changeElementsValueButton);
            this.circuitsGroupBox.Controls.Add(this.calculateImpedanceButton);
            this.circuitsGroupBox.Controls.Add(this.impedanceGridView);
            this.circuitsGroupBox.Location = new System.Drawing.Point(579, 12);
            this.circuitsGroupBox.Name = "circuitsGroupBox";
            this.circuitsGroupBox.Size = new System.Drawing.Size(324, 446);
            this.circuitsGroupBox.TabIndex = 1;
            this.circuitsGroupBox.TabStop = false;
            this.circuitsGroupBox.Text = "Схемы";
            // 
            // circuitsListBox
            // 
            this.circuitsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.circuitsListBox.FormattingEnabled = true;
            this.circuitsListBox.Location = new System.Drawing.Point(3, 3);
            this.circuitsListBox.Name = "circuitsListBox";
            this.circuitsListBox.Size = new System.Drawing.Size(298, 204);
            this.circuitsListBox.TabIndex = 4;
            this.circuitsListBox.SelectedIndexChanged += new System.EventHandler(this.circuitsListBox_SelectedIndexChanged);
            // 
            // changeElementsValueButton
            // 
            this.changeElementsValueButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.changeElementsValueButton.Location = new System.Drawing.Point(179, 417);
            this.changeElementsValueButton.Name = "changeElementsValueButton";
            this.changeElementsValueButton.Size = new System.Drawing.Size(139, 23);
            this.changeElementsValueButton.TabIndex = 3;
            this.changeElementsValueButton.Text = "Изменить значения";
            this.changeElementsValueButton.UseVisualStyleBackColor = true;
            // 
            // calculateImpedanceButton
            // 
            this.calculateImpedanceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.calculateImpedanceButton.Location = new System.Drawing.Point(36, 417);
            this.calculateImpedanceButton.Name = "calculateImpedanceButton";
            this.calculateImpedanceButton.Size = new System.Drawing.Size(137, 23);
            this.calculateImpedanceButton.TabIndex = 2;
            this.calculateImpedanceButton.Text = "Рассчитать импеданс";
            this.calculateImpedanceButton.UseVisualStyleBackColor = true;
            this.calculateImpedanceButton.Click += new System.EventHandler(this.calculateImpedanceButton_Click);
            // 
            // impedanceGridView
            // 
            this.impedanceGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.impedanceGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.impedanceGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.impedanceGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.frequency,
            this.impedance});
            this.impedanceGridView.Location = new System.Drawing.Point(6, 261);
            this.impedanceGridView.Name = "impedanceGridView";
            this.impedanceGridView.RowHeadersVisible = false;
            this.impedanceGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.impedanceGridView.Size = new System.Drawing.Size(312, 150);
            this.impedanceGridView.TabIndex = 0;
            // 
            // frequency
            // 
            this.frequency.HeaderText = "Частота";
            this.frequency.Name = "frequency";
            // 
            // impedance
            // 
            this.impedance.HeaderText = "Импеданс цепи";
            this.impedance.Name = "impedance";
            this.impedance.ReadOnly = true;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.circuitListTab);
            this.tabControl.Controls.Add(this.circuitElementsValues);
            this.tabControl.Location = new System.Drawing.Point(6, 19);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(312, 236);
            this.tabControl.TabIndex = 5;
            // 
            // circuitListTab
            // 
            this.circuitListTab.Controls.Add(this.circuitsListBox);
            this.circuitListTab.Location = new System.Drawing.Point(4, 22);
            this.circuitListTab.Name = "circuitListTab";
            this.circuitListTab.Padding = new System.Windows.Forms.Padding(3);
            this.circuitListTab.Size = new System.Drawing.Size(304, 210);
            this.circuitListTab.TabIndex = 0;
            this.circuitListTab.Text = "Список схем";
            this.circuitListTab.UseVisualStyleBackColor = true;
            // 
            // circuitElementsValues
            // 
            this.circuitElementsValues.Location = new System.Drawing.Point(4, 22);
            this.circuitElementsValues.Name = "circuitElementsValues";
            this.circuitElementsValues.Padding = new System.Windows.Forms.Padding(3);
            this.circuitElementsValues.Size = new System.Drawing.Size(304, 210);
            this.circuitElementsValues.TabIndex = 1;
            this.circuitElementsValues.Text = "Значения элементов схемы";
            this.circuitElementsValues.UseVisualStyleBackColor = true;
            // 
            // CircuitViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(915, 470);
            this.Controls.Add(this.circuitsGroupBox);
            this.Controls.Add(this.circuitViewGroupBox);
            this.MinimumSize = new System.Drawing.Size(720, 440);
            this.Name = "CircuitViewForm";
            this.Text = "Impedance Calculator";
            this.circuitViewGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.circuitView)).EndInit();
            this.circuitsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.impedanceGridView)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.circuitListTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox circuitViewGroupBox;
        private System.Windows.Forms.PictureBox circuitView;
        private System.Windows.Forms.GroupBox circuitsGroupBox;
        private System.Windows.Forms.DataGridView impedanceGridView;
        private System.Windows.Forms.Button changeElementsValueButton;
        private System.Windows.Forms.Button calculateImpedanceButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn frequency;
        private System.Windows.Forms.DataGridViewTextBoxColumn impedance;
        private System.Windows.Forms.ListBox circuitsListBox;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage circuitListTab;
        private System.Windows.Forms.TabPage circuitElementsValues;
    }
}

