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
            this.impedanceGridView = new System.Windows.Forms.DataGridView();
            this.frequency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.impedance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.circuitsListView = new System.Windows.Forms.ListView();
            this.circuitViewGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.circuitView)).BeginInit();
            this.circuitsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.impedanceGridView)).BeginInit();
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
            this.circuitViewGroupBox.Size = new System.Drawing.Size(519, 378);
            this.circuitViewGroupBox.TabIndex = 0;
            this.circuitViewGroupBox.TabStop = false;
            this.circuitViewGroupBox.Text = "Circuit View";
            // 
            // circuitView
            // 
            this.circuitView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.circuitView.Location = new System.Drawing.Point(3, 16);
            this.circuitView.Name = "circuitView";
            this.circuitView.Size = new System.Drawing.Size(513, 359);
            this.circuitView.TabIndex = 0;
            this.circuitView.TabStop = false;
            // 
            // circuitsGroupBox
            // 
            this.circuitsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.circuitsGroupBox.Controls.Add(this.circuitsListView);
            this.circuitsGroupBox.Controls.Add(this.impedanceGridView);
            this.circuitsGroupBox.Location = new System.Drawing.Point(534, 12);
            this.circuitsGroupBox.Name = "circuitsGroupBox";
            this.circuitsGroupBox.Size = new System.Drawing.Size(324, 378);
            this.circuitsGroupBox.TabIndex = 1;
            this.circuitsGroupBox.TabStop = false;
            this.circuitsGroupBox.Text = "Circuits";
            // 
            // impedanceGridView
            // 
            this.impedanceGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.impedanceGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.impedanceGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.impedanceGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.frequency,
            this.impedance});
            this.impedanceGridView.Location = new System.Drawing.Point(6, 222);
            this.impedanceGridView.Name = "impedanceGridView";
            this.impedanceGridView.RowHeadersVisible = false;
            this.impedanceGridView.Size = new System.Drawing.Size(312, 150);
            this.impedanceGridView.TabIndex = 0;
            // 
            // frequency
            // 
            this.frequency.HeaderText = "Частота";
            this.frequency.Name = "frequency";
            this.frequency.ReadOnly = true;
            // 
            // impedance
            // 
            this.impedance.HeaderText = "Импеданс цепи";
            this.impedance.Name = "impedance";
            this.impedance.ReadOnly = true;
            // 
            // circuitsListView
            // 
            this.circuitsListView.Location = new System.Drawing.Point(6, 16);
            this.circuitsListView.Name = "circuitsListView";
            this.circuitsListView.Size = new System.Drawing.Size(312, 200);
            this.circuitsListView.TabIndex = 1;
            this.circuitsListView.UseCompatibleStateImageBehavior = false;
            // 
            // CircuitViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(870, 402);
            this.Controls.Add(this.circuitsGroupBox);
            this.Controls.Add(this.circuitViewGroupBox);
            this.MinimumSize = new System.Drawing.Size(720, 440);
            this.Name = "CircuitViewForm";
            this.Text = "Impedance Calculator";
            this.circuitViewGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.circuitView)).EndInit();
            this.circuitsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.impedanceGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox circuitViewGroupBox;
        private System.Windows.Forms.PictureBox circuitView;
        private System.Windows.Forms.GroupBox circuitsGroupBox;
        private System.Windows.Forms.ListView circuitsListView;
        private System.Windows.Forms.DataGridView impedanceGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn frequency;
        private System.Windows.Forms.DataGridViewTextBoxColumn impedance;
    }
}

