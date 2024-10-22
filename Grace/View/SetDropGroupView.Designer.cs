namespace Grace.View
{
    partial class SetDropGroupView
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            dropGroupDataGrid = new DataGridView();
            cItemId = new DataGridViewTextBoxColumn();
            cItemName = new DataGridViewTextBoxColumn();
            btn_Cancel = new Button();
            btn_Confirm = new Button();
            btn_Filter = new Button();
            label_Filter = new Label();
            radioButton_Id = new RadioButton();
            radioButton_Name = new RadioButton();
            textBox_Filter = new TextBox();
            btn_Reset = new Button();
            ((System.ComponentModel.ISupportInitialize)dropGroupDataGrid).BeginInit();
            SuspendLayout();
            // 
            // itemDataGrid
            // 
            dropGroupDataGrid.AllowUserToAddRows = false;
            dropGroupDataGrid.AllowUserToDeleteRows = false;
            dropGroupDataGrid.AllowUserToOrderColumns = true;
            dropGroupDataGrid.AllowUserToResizeRows = false;
            dropGroupDataGrid.BackgroundColor = SystemColors.Window;
            dropGroupDataGrid.BorderStyle = BorderStyle.Fixed3D;
            dropGroupDataGrid.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dropGroupDataGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dropGroupDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dropGroupDataGrid.Columns.AddRange(new DataGridViewColumn[] { cItemId, cItemName });
            dropGroupDataGrid.Location = new Point(12, 12);
            dropGroupDataGrid.MultiSelect = false;
            dropGroupDataGrid.Name = "itemDataGrid";
            dropGroupDataGrid.ReadOnly = true;
            dropGroupDataGrid.RowHeadersVisible = false;
            dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dropGroupDataGrid.RowsDefaultCellStyle = dataGridViewCellStyle1;
            dropGroupDataGrid.RowTemplate.Height = 15;
            dropGroupDataGrid.RowTemplate.ReadOnly = true;
            dropGroupDataGrid.RowTemplate.Resizable = DataGridViewTriState.False;
            dropGroupDataGrid.Size = new Size(380, 426);
            dropGroupDataGrid.TabIndex = 3;
            // 
            // cItemId
            // 
            cItemId.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            cItemId.DataPropertyName = "Key";
            cItemId.FillWeight = 30F;
            cItemId.HeaderText = "ID";
            cItemId.Name = "cItemId";
            cItemId.ReadOnly = true;
            // 
            // cItemName
            // 
            cItemName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            cItemName.DataPropertyName = "Value";
            cItemName.FillWeight = 70F;
            cItemName.HeaderText = "Name";
            cItemName.Name = "cItemName";
            cItemName.ReadOnly = true;
            // 
            // btn_Cancel
            // 
            btn_Cancel.Location = new Point(398, 415);
            btn_Cancel.Name = "btn_Cancel";
            btn_Cancel.Size = new Size(75, 23);
            btn_Cancel.TabIndex = 4;
            btn_Cancel.Text = "Cancel";
            btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // btn_Confirm
            // 
            btn_Confirm.DialogResult = DialogResult.OK;
            btn_Confirm.Location = new Point(479, 415);
            btn_Confirm.Name = "btn_Confirm";
            btn_Confirm.Size = new Size(75, 23);
            btn_Confirm.TabIndex = 5;
            btn_Confirm.Text = "Confirm";
            btn_Confirm.UseVisualStyleBackColor = true;
            // 
            // btn_Filter
            // 
            btn_Filter.Location = new Point(398, 84);
            btn_Filter.Name = "btn_Filter";
            btn_Filter.Size = new Size(75, 23);
            btn_Filter.TabIndex = 6;
            btn_Filter.Text = "Filter";
            btn_Filter.UseVisualStyleBackColor = true;
            // 
            // label_Filter
            // 
            label_Filter.AutoSize = true;
            label_Filter.Location = new Point(398, 12);
            label_Filter.Name = "label_Filter";
            label_Filter.Size = new Size(49, 15);
            label_Filter.TabIndex = 7;
            label_Filter.Text = "Filter by";
            // 
            // radioButton_Id
            // 
            radioButton_Id.AutoSize = true;
            radioButton_Id.Checked = true;
            radioButton_Id.Location = new Point(398, 30);
            radioButton_Id.Name = "radioButton_Id";
            radioButton_Id.Size = new Size(36, 19);
            radioButton_Id.TabIndex = 8;
            radioButton_Id.TabStop = true;
            radioButton_Id.Text = "ID";
            radioButton_Id.UseVisualStyleBackColor = true;
            // 
            // radioButton_Name
            // 
            radioButton_Name.AutoSize = true;
            radioButton_Name.Location = new Point(440, 30);
            radioButton_Name.Name = "radioButton_Name";
            radioButton_Name.Size = new Size(57, 19);
            radioButton_Name.TabIndex = 9;
            radioButton_Name.Text = "Name";
            radioButton_Name.UseVisualStyleBackColor = true;
            // 
            // textBox_Filter
            // 
            textBox_Filter.Location = new Point(398, 55);
            textBox_Filter.Name = "textBox_Filter";
            textBox_Filter.Size = new Size(156, 23);
            textBox_Filter.TabIndex = 10;
            // 
            // btn_Reset
            // 
            btn_Reset.Location = new Point(479, 84);
            btn_Reset.Name = "btn_Reset";
            btn_Reset.Size = new Size(75, 23);
            btn_Reset.TabIndex = 11;
            btn_Reset.Text = "Reset";
            btn_Reset.UseVisualStyleBackColor = true;
            // 
            // SetItemView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btn_Cancel;
            ClientSize = new Size(566, 450);
            Controls.Add(btn_Reset);
            Controls.Add(textBox_Filter);
            Controls.Add(radioButton_Name);
            Controls.Add(radioButton_Id);
            Controls.Add(label_Filter);
            Controls.Add(btn_Filter);
            Controls.Add(btn_Confirm);
            Controls.Add(btn_Cancel);
            Controls.Add(dropGroupDataGrid);
            Name = "SetItemView";
            Text = "Set Item";
            ((System.ComponentModel.ISupportInitialize)dropGroupDataGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dropGroupDataGrid;
        private Button btn_Cancel;
        private Button btn_Confirm;
        private Button btn_Filter;
        private Label label_Filter;
        private RadioButton radioButton_Id;
        private RadioButton radioButton_Name;
        private TextBox textBox_Filter;
        private DataGridViewTextBoxColumn cItemId;
        private DataGridViewTextBoxColumn cItemName;
        private Button btn_Reset;
    }
}