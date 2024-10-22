namespace Grace.View;

partial class UpdateWarningView
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
        groupBox_MonsterDetails = new GroupBox();
        monsterDataGrid = new DataGridView();
        cMonsterId = new DataGridViewTextBoxColumn();
        cMonsterName = new DataGridViewTextBoxColumn();
        cLocation = new DataGridViewTextBoxColumn();
        cLv = new DataGridViewTextBoxColumn();
        warningIcon = new PictureBox();
        warningLabel = new Label();
        btnCancel = new Button();
        btnConfirm = new Button();
        groupBox_MonsterDetails.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)monsterDataGrid).BeginInit();
        ((System.ComponentModel.ISupportInitialize)warningIcon).BeginInit();
        SuspendLayout();
        // 
        // groupBox_MonsterDetails
        // 
        groupBox_MonsterDetails.Controls.Add(monsterDataGrid);
        groupBox_MonsterDetails.Location = new Point(12, 58);
        groupBox_MonsterDetails.Name = "groupBox_MonsterDetails";
        groupBox_MonsterDetails.Size = new Size(776, 343);
        groupBox_MonsterDetails.TabIndex = 6;
        groupBox_MonsterDetails.TabStop = false;
        groupBox_MonsterDetails.Tag = "monsterDetails";
        groupBox_MonsterDetails.Text = "Affected Monsters";
        // 
        // monsterDataGrid
        // 
        monsterDataGrid.AllowUserToAddRows = false;
        monsterDataGrid.AllowUserToDeleteRows = false;
        monsterDataGrid.AllowUserToOrderColumns = true;
        monsterDataGrid.AllowUserToResizeRows = false;
        monsterDataGrid.BackgroundColor = SystemColors.Window;
        monsterDataGrid.BorderStyle = BorderStyle.Fixed3D;
        monsterDataGrid.CellBorderStyle = DataGridViewCellBorderStyle.None;
        monsterDataGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        monsterDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        monsterDataGrid.Columns.AddRange(new DataGridViewColumn[] { cMonsterId, cMonsterName, cLocation, cLv });
        monsterDataGrid.Location = new Point(6, 22);
        monsterDataGrid.MultiSelect = false;
        monsterDataGrid.Name = "monsterDataGrid";
        monsterDataGrid.ReadOnly = true;
        monsterDataGrid.RowHeadersVisible = false;
        dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
        dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
        monsterDataGrid.RowsDefaultCellStyle = dataGridViewCellStyle1;
        monsterDataGrid.RowTemplate.Height = 15;
        monsterDataGrid.RowTemplate.ReadOnly = true;
        monsterDataGrid.RowTemplate.Resizable = DataGridViewTriState.False;
        monsterDataGrid.Size = new Size(764, 315);
        monsterDataGrid.TabIndex = 2;
        // 
        // cMonsterId
        // 
        cMonsterId.DataPropertyName = "Id";
        cMonsterId.HeaderText = "ID";
        cMonsterId.Name = "cMonsterId";
        cMonsterId.ReadOnly = true;
        cMonsterId.Width = 88;
        // 
        // cMonsterName
        // 
        cMonsterName.DataPropertyName = "Name";
        cMonsterName.HeaderText = "Name";
        cMonsterName.Name = "cMonsterName";
        cMonsterName.ReadOnly = true;
        cMonsterName.Width = 240;
        // 
        // cLocation
        // 
        cLocation.DataPropertyName = "Location";
        cLocation.HeaderText = "Location";
        cLocation.Name = "cLocation";
        cLocation.ReadOnly = true;
        cLocation.Width = 240;
        // 
        // cLv
        // 
        cLv.DataPropertyName = "Level";
        cLv.HeaderText = "Lv";
        cLv.Name = "cLv";
        cLv.ReadOnly = true;
        // 
        // warningIcon
        // 
        warningIcon.Location = new Point(12, 12);
        warningIcon.Name = "warningIcon";
        warningIcon.Size = new Size(100, 50);
        warningIcon.SizeMode = PictureBoxSizeMode.AutoSize;
        warningIcon.TabIndex = 7;
        warningIcon.TabStop = false;
        // 
        // warningLabel
        // 
        warningLabel.AutoSize = true;
        warningLabel.Location = new Point(56, 23);
        warningLabel.Name = "warningLabel";
        warningLabel.Size = new Size(409, 15);
        warningLabel.TabIndex = 8;
        warningLabel.Text = "Updating this DropGroup will affect the following Monsters and DropGroups";
        // 
        // btnCancel
        // 
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.Location = new Point(632, 407);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(75, 23);
        btnCancel.TabIndex = 9;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = true;
        // 
        // btnConfirm
        // 
        btnConfirm.DialogResult = DialogResult.OK;
        btnConfirm.Location = new Point(713, 407);
        btnConfirm.Name = "btnConfirm";
        btnConfirm.Size = new Size(75, 23);
        btnConfirm.TabIndex = 10;
        btnConfirm.Text = "Confirm";
        btnConfirm.UseVisualStyleBackColor = true;
        // 
        // UpdateWarningView
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 441);
        Controls.Add(btnConfirm);
        Controls.Add(btnCancel);
        Controls.Add(warningLabel);
        Controls.Add(warningIcon);
        Controls.Add(groupBox_MonsterDetails);
        Name = "UpdateWarningView";
        Text = "UpdateWarningView";
        groupBox_MonsterDetails.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)monsterDataGrid).EndInit();
        ((System.ComponentModel.ISupportInitialize)warningIcon).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private GroupBox groupBox_MonsterDetails;
    private DataGridView monsterDataGrid;
    private DataGridViewTextBoxColumn cMonsterId;
    private DataGridViewTextBoxColumn cMonsterName;
    private DataGridViewTextBoxColumn cLocation;
    private DataGridViewTextBoxColumn cLv;
    private PictureBox warningIcon;
    private Label warningLabel;
    private Button btnCancel;
    private Button btnConfirm;
}