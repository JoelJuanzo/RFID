namespace RFID
{
    partial class DisplayAttendance
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayAttendance));
            this.panel1 = new System.Windows.Forms.Panel();
            this.printButton = new System.Windows.Forms.Button();
            this.sectionComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.attendanceMonthCalendar = new System.Windows.Forms.MonthCalendar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.attendanceDataGridView = new System.Windows.Forms.DataGridView();
            this.DateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LRNColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fullNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeInAMColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeOutAMColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeInPMColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeOutPMColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.closeButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.attendanceDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.closeButton);
            this.panel1.Controls.Add(this.printButton);
            this.panel1.Controls.Add(this.sectionComboBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.attendanceMonthCalendar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(243, 438);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // printButton
            // 
            this.printButton.Image = ((System.Drawing.Image)(resources.GetObject("printButton.Image")));
            this.printButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.printButton.Location = new System.Drawing.Point(48, 287);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(104, 23);
            this.printButton.TabIndex = 10;
            this.printButton.Text = "Print";
            this.printButton.UseVisualStyleBackColor = true;
            this.printButton.Click += new System.EventHandler(this.printButton_Click);
            // 
            // sectionComboBox
            // 
            this.sectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sectionComboBox.FormattingEnabled = true;
            this.sectionComboBox.Location = new System.Drawing.Point(64, 183);
            this.sectionComboBox.Name = "sectionComboBox";
            this.sectionComboBox.Size = new System.Drawing.Size(172, 23);
            this.sectionComboBox.TabIndex = 9;
            this.sectionComboBox.SelectedIndexChanged += new System.EventHandler(this.sectionComboBox_SelectedIndexChanged);
            this.sectionComboBox.TextChanged += new System.EventHandler(this.sectionComboBox_TextChanged);
            this.sectionComboBox.Click += new System.EventHandler(this.sectionComboBox_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 186);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Section:";
            // 
            // attendanceMonthCalendar
            // 
            this.attendanceMonthCalendar.Location = new System.Drawing.Point(9, 9);
            this.attendanceMonthCalendar.Name = "attendanceMonthCalendar";
            this.attendanceMonthCalendar.TabIndex = 6;
            this.attendanceMonthCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.attendanceMonthCalendar_DateChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.attendanceDataGridView);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(243, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1127, 438);
            this.panel2.TabIndex = 3;
            // 
            // attendanceDataGridView
            // 
            this.attendanceDataGridView.AllowUserToAddRows = false;
            this.attendanceDataGridView.AllowUserToDeleteRows = false;
            this.attendanceDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.attendanceDataGridView.ColumnHeadersHeight = 40;
            this.attendanceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.attendanceDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DateColumn,
            this.LRNColumn,
            this.fullNameColumn,
            this.timeInAMColumn,
            this.timeOutAMColumn,
            this.timeInPMColumn,
            this.timeOutPMColumn});
            this.attendanceDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.attendanceDataGridView.Location = new System.Drawing.Point(0, 0);
            this.attendanceDataGridView.MultiSelect = false;
            this.attendanceDataGridView.Name = "attendanceDataGridView";
            this.attendanceDataGridView.ReadOnly = true;
            this.attendanceDataGridView.RowTemplate.Height = 25;
            this.attendanceDataGridView.Size = new System.Drawing.Size(1127, 438);
            this.attendanceDataGridView.TabIndex = 2;
            // 
            // DateColumn
            // 
            this.DateColumn.HeaderText = "Date";
            this.DateColumn.Name = "DateColumn";
            this.DateColumn.ReadOnly = true;
            // 
            // LRNColumn
            // 
            this.LRNColumn.HeaderText = "LRN";
            this.LRNColumn.Name = "LRNColumn";
            this.LRNColumn.ReadOnly = true;
            // 
            // fullNameColumn
            // 
            this.fullNameColumn.HeaderText = "Last Name";
            this.fullNameColumn.Name = "fullNameColumn";
            this.fullNameColumn.ReadOnly = true;
            this.fullNameColumn.Width = 150;
            // 
            // timeInAMColumn
            // 
            this.timeInAMColumn.HeaderText = "Time In - AM";
            this.timeInAMColumn.Name = "timeInAMColumn";
            this.timeInAMColumn.ReadOnly = true;
            this.timeInAMColumn.Width = 125;
            // 
            // timeOutAMColumn
            // 
            this.timeOutAMColumn.HeaderText = "Time Out - AM";
            this.timeOutAMColumn.Name = "timeOutAMColumn";
            this.timeOutAMColumn.ReadOnly = true;
            this.timeOutAMColumn.Width = 125;
            // 
            // timeInPMColumn
            // 
            this.timeInPMColumn.HeaderText = "Time In - PM";
            this.timeInPMColumn.Name = "timeInPMColumn";
            this.timeInPMColumn.ReadOnly = true;
            this.timeInPMColumn.Width = 125;
            // 
            // timeOutPMColumn
            // 
            this.timeOutPMColumn.HeaderText = "Time Out - PM";
            this.timeOutPMColumn.Name = "timeOutPMColumn";
            this.timeOutPMColumn.ReadOnly = true;
            this.timeOutPMColumn.Width = 125;
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(158, 287);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 11;
            this.closeButton.Text = "&Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // DisplayAttendance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 438);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "DisplayAttendance";
            this.Text = "DisplayAttendance";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DisplayAttendance_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.attendanceDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Panel panel1;
        private ComboBox sectionComboBox;
        private Label label1;
        private MonthCalendar attendanceMonthCalendar;
        private Panel panel2;
        private DataGridView attendanceDataGridView;
        private DataGridViewTextBoxColumn DateColumn;
        private DataGridViewTextBoxColumn LRNColumn;
        private DataGridViewTextBoxColumn fullNameColumn;
        private DataGridViewTextBoxColumn timeInAMColumn;
        private DataGridViewTextBoxColumn timeOutAMColumn;
        private DataGridViewTextBoxColumn timeInPMColumn;
        private DataGridViewTextBoxColumn timeOutPMColumn;
        private Button printButton;
        private Button closeButton;
    }
}