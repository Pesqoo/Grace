namespace Grace.View
{
    partial class RenameView
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
            label_Filter = new Label();
            textBox_Filter = new TextBox();
            btn_Cancel = new Button();
            btn_Filter = new Button();
            SuspendLayout();
            // 
            // label_Filter
            // 
            label_Filter.AutoSize = true;
            label_Filter.Location = new Point(12, 15);
            label_Filter.Name = "label_Filter";
            label_Filter.Size = new Size(32, 15);
            label_Filter.TabIndex = 0;
            label_Filter.Text = "Alias";
            // 
            // textBox_Filter
            // 
            textBox_Filter.Location = new Point(83, 12);
            textBox_Filter.Name = "textBox_Filter";
            textBox_Filter.Size = new Size(112, 23);
            textBox_Filter.TabIndex = 1;
            // 
            // btn_Cancel
            // 
            btn_Cancel.DialogResult = DialogResult.Cancel;
            btn_Cancel.Location = new Point(12, 41);
            btn_Cancel.Name = "btn_Cancel";
            btn_Cancel.Size = new Size(85, 23);
            btn_Cancel.TabIndex = 2;
            btn_Cancel.Text = "Cancel";
            btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // btn_Filter
            // 
            btn_Filter.DialogResult = DialogResult.OK;
            btn_Filter.Location = new Point(110, 41);
            btn_Filter.Name = "btn_Filter";
            btn_Filter.Size = new Size(85, 23);
            btn_Filter.TabIndex = 3;
            btn_Filter.Text = "Confirm";
            btn_Filter.UseVisualStyleBackColor = true;
            // 
            // RenameView
            // 
            AcceptButton = btn_Filter;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btn_Cancel;
            ClientSize = new Size(209, 76);
            Controls.Add(btn_Filter);
            Controls.Add(btn_Cancel);
            Controls.Add(textBox_Filter);
            Controls.Add(label_Filter);
            Name = "RenameView";
            Text = "Rename";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label_Filter;
        private TextBox textBox_Filter;
        private Button btn_Cancel;
        private Button btn_Filter;
    }
}