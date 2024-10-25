namespace Grace.View;

partial class References
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(References));
        monsterDataGrid = new DataGridView();
        cMonsterId = new DataGridViewTextBoxColumn();
        cMonsterName = new DataGridViewTextBoxColumn();
        cLocation = new DataGridViewTextBoxColumn();
        cLv = new DataGridViewTextBoxColumn();
        ((System.ComponentModel.ISupportInitialize)monsterDataGrid).BeginInit();
        SuspendLayout();
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
        monsterDataGrid.Location = new Point(12, 12);
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
        monsterDataGrid.Size = new Size(776, 426);
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
        // References
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(monsterDataGrid);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Name = "References";
        Text = "References";
        ((System.ComponentModel.ISupportInitialize)monsterDataGrid).EndInit();
        ResumeLayout(false);
    }

    #endregion
    private DataGridView monsterDataGrid;
    private DataGridViewTextBoxColumn cMonsterId;
    private DataGridViewTextBoxColumn cMonsterName;
    private DataGridViewTextBoxColumn cLocation;
    private DataGridViewTextBoxColumn cLv;
}