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
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(References));
        monsterDataGrid = new DataGridView();
        cMonsterId = new DataGridViewTextBoxColumn();
        cMonsterName = new DataGridViewTextBoxColumn();
        cLocation = new DataGridViewTextBoxColumn();
        cLv = new DataGridViewTextBoxColumn();
        itemDataGrid = new DataGridView();
        dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
        dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
        label1 = new Label();
        label2 = new Label();
        ((System.ComponentModel.ISupportInitialize)monsterDataGrid).BeginInit();
        ((System.ComponentModel.ISupportInitialize)itemDataGrid).BeginInit();
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
        monsterDataGrid.Location = new Point(12, 27);
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
        monsterDataGrid.Size = new Size(776, 240);
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
        // itemDataGrid
        // 
        itemDataGrid.AllowUserToAddRows = false;
        itemDataGrid.AllowUserToDeleteRows = false;
        itemDataGrid.AllowUserToOrderColumns = true;
        itemDataGrid.AllowUserToResizeRows = false;
        itemDataGrid.BackgroundColor = SystemColors.Window;
        itemDataGrid.BorderStyle = BorderStyle.Fixed3D;
        itemDataGrid.CellBorderStyle = DataGridViewCellBorderStyle.None;
        itemDataGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        itemDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        itemDataGrid.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2 });
        itemDataGrid.Location = new Point(12, 288);
        itemDataGrid.MultiSelect = false;
        itemDataGrid.Name = "itemDataGrid";
        itemDataGrid.ReadOnly = true;
        itemDataGrid.RowHeadersVisible = false;
        dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
        itemDataGrid.RowsDefaultCellStyle = dataGridViewCellStyle2;
        itemDataGrid.RowTemplate.Height = 15;
        itemDataGrid.RowTemplate.ReadOnly = true;
        itemDataGrid.RowTemplate.Resizable = DataGridViewTriState.False;
        itemDataGrid.Size = new Size(776, 240);
        itemDataGrid.TabIndex = 3;
        // 
        // dataGridViewTextBoxColumn1
        // 
        dataGridViewTextBoxColumn1.DataPropertyName = "Id";
        dataGridViewTextBoxColumn1.HeaderText = "ID";
        dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
        dataGridViewTextBoxColumn1.ReadOnly = true;
        dataGridViewTextBoxColumn1.Width = 88;
        // 
        // dataGridViewTextBoxColumn2
        // 
        dataGridViewTextBoxColumn2.DataPropertyName = "Name";
        dataGridViewTextBoxColumn2.HeaderText = "Name";
        dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
        dataGridViewTextBoxColumn2.ReadOnly = true;
        dataGridViewTextBoxColumn2.Width = 240;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(12, 9);
        label1.Name = "label1";
        label1.Size = new Size(80, 15);
        label1.TabIndex = 4;
        label1.Text = "Monster Drop";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(12, 270);
        label2.Name = "label2";
        label2.Size = new Size(192, 15);
        label2.TabIndex = 5;
        label2.Text = "Item opt_type 94 (GENERATE_ITEM)";
        // 
        // References
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 536);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(itemDataGrid);
        Controls.Add(monsterDataGrid);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Name = "References";
        Text = "References";
        ((System.ComponentModel.ISupportInitialize)monsterDataGrid).EndInit();
        ((System.ComponentModel.ISupportInitialize)itemDataGrid).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private DataGridView monsterDataGrid;
    private DataGridViewTextBoxColumn cMonsterId;
    private DataGridViewTextBoxColumn cMonsterName;
    private DataGridViewTextBoxColumn cLocation;
    private DataGridViewTextBoxColumn cLv;
    private DataGridView itemDataGrid;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private Label label1;
    private Label label2;
}