namespace Grace
{
    partial class MonsterView
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            TreeNode treeNode10 = new TreeNode("Sub ID #1");
            TreeNode treeNode11 = new TreeNode("Sub ID #2");
            TreeNode treeNode12 = new TreeNode("Sub ID #3");
            TreeNode treeNode13 = new TreeNode("Sub ID #4");
            TreeNode treeNode14 = new TreeNode("Sub ID #5");
            TreeNode treeNode15 = new TreeNode("Sub ID #6");
            TreeNode treeNode16 = new TreeNode("Sub ID #7");
            TreeNode treeNode17 = new TreeNode("Sub ID #8");
            TreeNode treeNode18 = new TreeNode("Sub ID #9");
            btn_ReloadData = new Button();
            monsterDataGrid = new DataGridView();
            cMonsterName = new DataGridViewTextBoxColumn();
            cLocation = new DataGridViewTextBoxColumn();
            cMonsterId = new DataGridViewTextBoxColumn();
            groupBox_MonsterDetails = new GroupBox();
            contextMenuStrip_DropInfo = new ContextMenuStrip(components);
            toolStripMenuItem_InfoRemove = new ToolStripMenuItem();
            toolStripMenuItem_InfoSetItem = new ToolStripMenuItem();
            toolStripMenuItem_InfoSetDropGroup = new ToolStripMenuItem();
            toolStripMenuItem_InfoRename = new ToolStripMenuItem();
            toolStripMenuItem_InfoViewReferences = new ToolStripMenuItem();
            dropTreeView = new TreeView();
            contextMenuStrip_TreeView = new ContextMenuStrip(components);
            toolStripMenuItem_AddSubId = new ToolStripMenuItem();
            groupBox_DropTableDetails = new GroupBox();
            progressBar1 = new ProgressBar();
            radioButton_FilterAll = new RadioButton();
            groupBox_Filter = new GroupBox();
            radioButton_FilterItemId = new RadioButton();
            radioButton_FilterDropGroupId = new RadioButton();
            radioButton_FilterDrop = new RadioButton();
            radioButton_FilterLocation = new RadioButton();
            radioButton_FilterName = new RadioButton();
            radioButton_FilterId = new RadioButton();
            btn_Filter = new Button();
            checkBox_NoTable = new CheckBox();
            contextMenuStrip_SubId = new ContextMenuStrip(components);
            toolStripMenuItem_DeleteSubId = new ToolStripMenuItem();
            contextMenuStrip_TreeViewItem = new ContextMenuStrip(components);
            toolStripMenuItem_TreeViewRename = new ToolStripMenuItem();
            toolStripMenuItem_TreeViewViewReferences = new ToolStripMenuItem();
            DropInfo = new DropInfo();
            ((System.ComponentModel.ISupportInitialize)monsterDataGrid).BeginInit();
            groupBox_MonsterDetails.SuspendLayout();
            contextMenuStrip_DropInfo.SuspendLayout();
            contextMenuStrip_TreeView.SuspendLayout();
            groupBox_DropTableDetails.SuspendLayout();
            groupBox_Filter.SuspendLayout();
            contextMenuStrip_SubId.SuspendLayout();
            contextMenuStrip_TreeViewItem.SuspendLayout();
            SuspendLayout();
            // 
            // btn_ReloadData
            // 
            btn_ReloadData.Location = new Point(6, 6);
            btn_ReloadData.Name = "btn_ReloadData";
            btn_ReloadData.Size = new Size(128, 23);
            btn_ReloadData.TabIndex = 1;
            btn_ReloadData.Text = "Reload Data";
            btn_ReloadData.UseVisualStyleBackColor = true;
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
            monsterDataGrid.Columns.AddRange(new DataGridViewColumn[] { cMonsterName, cLocation, cMonsterId });
            monsterDataGrid.Location = new Point(6, 22);
            monsterDataGrid.MultiSelect = false;
            monsterDataGrid.Name = "monsterDataGrid";
            monsterDataGrid.ReadOnly = true;
            monsterDataGrid.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            monsterDataGrid.RowsDefaultCellStyle = dataGridViewCellStyle2;
            monsterDataGrid.RowTemplate.Height = 15;
            monsterDataGrid.RowTemplate.ReadOnly = true;
            monsterDataGrid.RowTemplate.Resizable = DataGridViewTriState.False;
            monsterDataGrid.Size = new Size(380, 481);
            monsterDataGrid.TabIndex = 2;
            // 
            // cMonsterName
            // 
            cMonsterName.DataPropertyName = "Name";
            cMonsterName.HeaderText = "Monster Name";
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
            // cMonsterId
            // 
            cMonsterId.DataPropertyName = "Id";
            cMonsterId.HeaderText = "Monster ID";
            cMonsterId.Name = "cMonsterId";
            cMonsterId.ReadOnly = true;
            cMonsterId.Width = 88;
            // 
            // groupBox_MonsterDetails
            // 
            groupBox_MonsterDetails.Controls.Add(monsterDataGrid);
            groupBox_MonsterDetails.Location = new Point(6, 64);
            groupBox_MonsterDetails.Name = "groupBox_MonsterDetails";
            groupBox_MonsterDetails.Size = new Size(392, 510);
            groupBox_MonsterDetails.TabIndex = 5;
            groupBox_MonsterDetails.TabStop = false;
            groupBox_MonsterDetails.Tag = "monsterDetails";
            groupBox_MonsterDetails.Text = "Monster Details";
            // 
            // contextMenuStrip_DropInfo
            // 
            contextMenuStrip_DropInfo.Items.AddRange(new ToolStripItem[] { toolStripMenuItem_InfoRemove, toolStripMenuItem_InfoSetItem, toolStripMenuItem_InfoSetDropGroup, toolStripMenuItem_InfoRename, toolStripMenuItem_InfoViewReferences });
            contextMenuStrip_DropInfo.Name = "contextMenuStrip_EditDrop";
            contextMenuStrip_DropInfo.Size = new Size(160, 114);
            // 
            // toolStripMenuItem_InfoRemove
            // 
            toolStripMenuItem_InfoRemove.Enabled = false;
            toolStripMenuItem_InfoRemove.Name = "toolStripMenuItem_InfoRemove";
            toolStripMenuItem_InfoRemove.Size = new Size(159, 22);
            toolStripMenuItem_InfoRemove.Text = "Remove";
            // 
            // toolStripMenuItem_InfoSetItem
            // 
            toolStripMenuItem_InfoSetItem.Enabled = false;
            toolStripMenuItem_InfoSetItem.Name = "toolStripMenuItem_InfoSetItem";
            toolStripMenuItem_InfoSetItem.Size = new Size(159, 22);
            toolStripMenuItem_InfoSetItem.Text = "Set Item";
            // 
            // toolStripMenuItem_InfoSetDropGroup
            // 
            toolStripMenuItem_InfoSetDropGroup.Enabled = false;
            toolStripMenuItem_InfoSetDropGroup.Name = "toolStripMenuItem_InfoSetDropGroup";
            toolStripMenuItem_InfoSetDropGroup.Size = new Size(159, 22);
            toolStripMenuItem_InfoSetDropGroup.Text = "Set DropGroup";
            // 
            // toolStripMenuItem_InfoRename
            // 
            toolStripMenuItem_InfoRename.Enabled = false;
            toolStripMenuItem_InfoRename.Name = "toolStripMenuItem_InfoRename";
            toolStripMenuItem_InfoRename.Size = new Size(159, 22);
            toolStripMenuItem_InfoRename.Text = "Rename";
            // 
            // toolStripMenuItem_InfoViewReferences
            // 
            toolStripMenuItem_InfoViewReferences.Enabled = false;
            toolStripMenuItem_InfoViewReferences.Name = "toolStripMenuItem_InfoViewReferences";
            toolStripMenuItem_InfoViewReferences.Size = new Size(159, 22);
            toolStripMenuItem_InfoViewReferences.Text = "View References";
            // 
            // dropTreeView
            // 
            dropTreeView.ContextMenuStrip = contextMenuStrip_TreeView;
            dropTreeView.Location = new Point(6, 22);
            dropTreeView.Name = "dropTreeView";
            treeNode10.Name = "SubId1";
            treeNode10.Text = "Sub ID #1";
            treeNode11.Name = "SubId2";
            treeNode11.Text = "Sub ID #2";
            treeNode12.Name = "SubId3";
            treeNode12.Text = "Sub ID #3";
            treeNode13.Name = "SubId4";
            treeNode13.Text = "Sub ID #4";
            treeNode14.Name = "SubId5";
            treeNode14.Text = "Sub ID #5";
            treeNode15.Name = "SubId6";
            treeNode15.Text = "Sub ID #6";
            treeNode16.Name = "SubId7";
            treeNode16.Text = "Sub ID #7";
            treeNode17.Name = "SubId8";
            treeNode17.Text = "Sub ID #8";
            treeNode18.Name = "SubId9";
            treeNode18.Text = "Sub ID #9";
            dropTreeView.Nodes.AddRange(new TreeNode[] { treeNode10, treeNode11, treeNode12, treeNode13, treeNode14, treeNode15, treeNode16, treeNode17, treeNode18 });
            dropTreeView.Size = new Size(380, 481);
            dropTreeView.TabIndex = 3;
            // 
            // contextMenuStrip_TreeView
            // 
            contextMenuStrip_TreeView.Items.AddRange(new ToolStripItem[] { toolStripMenuItem_AddSubId });
            contextMenuStrip_TreeView.Name = "contextMenuStrip1";
            contextMenuStrip_TreeView.Size = new Size(134, 26);
            // 
            // toolStripMenuItem_AddSubId
            // 
            toolStripMenuItem_AddSubId.Name = "toolStripMenuItem_AddSubId";
            toolStripMenuItem_AddSubId.Size = new Size(133, 22);
            toolStripMenuItem_AddSubId.Text = "Add Sub ID";
            // 
            // groupBox_DropTableDetails
            // 
            groupBox_DropTableDetails.Controls.Add(dropTreeView);
            groupBox_DropTableDetails.Location = new Point(397, 64);
            groupBox_DropTableDetails.Name = "groupBox_DropTableDetails";
            groupBox_DropTableDetails.Size = new Size(392, 510);
            groupBox_DropTableDetails.TabIndex = 6;
            groupBox_DropTableDetails.TabStop = false;
            groupBox_DropTableDetails.Tag = "dropTableDetails";
            groupBox_DropTableDetails.Text = "Drop Table Details";
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(1164, 6);
            progressBar1.MarqueeAnimationSpeed = 30;
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(100, 23);
            progressBar1.Step = 1;
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.TabIndex = 9;
            progressBar1.Visible = false;
            // 
            // radioButton_FilterAll
            // 
            radioButton_FilterAll.AutoSize = true;
            radioButton_FilterAll.Checked = true;
            radioButton_FilterAll.Location = new Point(6, 22);
            radioButton_FilterAll.Name = "radioButton_FilterAll";
            radioButton_FilterAll.Size = new Size(39, 19);
            radioButton_FilterAll.TabIndex = 10;
            radioButton_FilterAll.TabStop = true;
            radioButton_FilterAll.Text = "All";
            radioButton_FilterAll.UseVisualStyleBackColor = true;
            // 
            // groupBox_Filter
            // 
            groupBox_Filter.Controls.Add(radioButton_FilterItemId);
            groupBox_Filter.Controls.Add(radioButton_FilterDropGroupId);
            groupBox_Filter.Controls.Add(radioButton_FilterDrop);
            groupBox_Filter.Controls.Add(radioButton_FilterLocation);
            groupBox_Filter.Controls.Add(radioButton_FilterName);
            groupBox_Filter.Controls.Add(radioButton_FilterId);
            groupBox_Filter.Controls.Add(radioButton_FilterAll);
            groupBox_Filter.Location = new Point(140, 6);
            groupBox_Filter.Name = "groupBox_Filter";
            groupBox_Filter.Size = new Size(504, 52);
            groupBox_Filter.TabIndex = 11;
            groupBox_Filter.TabStop = false;
            groupBox_Filter.Text = "Filter";
            // 
            // radioButton_FilterItemId
            // 
            radioButton_FilterItemId.AutoSize = true;
            radioButton_FilterItemId.Location = new Point(435, 22);
            radioButton_FilterItemId.Name = "radioButton_FilterItemId";
            radioButton_FilterItemId.Size = new Size(63, 19);
            radioButton_FilterItemId.TabIndex = 16;
            radioButton_FilterItemId.Text = "Item ID";
            radioButton_FilterItemId.UseVisualStyleBackColor = true;
            // 
            // radioButton_FilterDropGroupId
            // 
            radioButton_FilterDropGroupId.AutoSize = true;
            radioButton_FilterDropGroupId.Location = new Point(331, 22);
            radioButton_FilterDropGroupId.Name = "radioButton_FilterDropGroupId";
            radioButton_FilterDropGroupId.Size = new Size(98, 19);
            radioButton_FilterDropGroupId.TabIndex = 15;
            radioButton_FilterDropGroupId.Text = "DropGroup ID";
            radioButton_FilterDropGroupId.UseVisualStyleBackColor = true;
            // 
            // radioButton_FilterDrop
            // 
            radioButton_FilterDrop.AutoSize = true;
            radioButton_FilterDrop.Location = new Point(233, 22);
            radioButton_FilterDrop.Name = "radioButton_FilterDrop";
            radioButton_FilterDrop.Size = new Size(92, 19);
            radioButton_FilterDrop.TabIndex = 14;
            radioButton_FilterDrop.Text = "DropTable ID";
            radioButton_FilterDrop.UseVisualStyleBackColor = true;
            // 
            // radioButton_FilterLocation
            // 
            radioButton_FilterLocation.AutoSize = true;
            radioButton_FilterLocation.Location = new Point(156, 22);
            radioButton_FilterLocation.Name = "radioButton_FilterLocation";
            radioButton_FilterLocation.Size = new Size(71, 19);
            radioButton_FilterLocation.TabIndex = 13;
            radioButton_FilterLocation.Text = "Location";
            radioButton_FilterLocation.UseVisualStyleBackColor = true;
            // 
            // radioButton_FilterName
            // 
            radioButton_FilterName.AutoSize = true;
            radioButton_FilterName.Location = new Point(93, 22);
            radioButton_FilterName.Name = "radioButton_FilterName";
            radioButton_FilterName.Size = new Size(57, 19);
            radioButton_FilterName.TabIndex = 12;
            radioButton_FilterName.Text = "Name";
            radioButton_FilterName.UseVisualStyleBackColor = true;
            // 
            // radioButton_FilterId
            // 
            radioButton_FilterId.AutoSize = true;
            radioButton_FilterId.Location = new Point(51, 22);
            radioButton_FilterId.Name = "radioButton_FilterId";
            radioButton_FilterId.Size = new Size(36, 19);
            radioButton_FilterId.TabIndex = 11;
            radioButton_FilterId.Text = "ID";
            radioButton_FilterId.UseVisualStyleBackColor = true;
            // 
            // btn_Filter
            // 
            btn_Filter.Location = new Point(6, 35);
            btn_Filter.Name = "btn_Filter";
            btn_Filter.Size = new Size(128, 23);
            btn_Filter.TabIndex = 12;
            btn_Filter.Text = "Filter";
            btn_Filter.UseVisualStyleBackColor = true;
            // 
            // checkBox_NoTable
            // 
            checkBox_NoTable.AutoSize = true;
            checkBox_NoTable.Location = new Point(650, 28);
            checkBox_NoTable.Name = "checkBox_NoTable";
            checkBox_NoTable.Size = new Size(203, 19);
            checkBox_NoTable.TabIndex = 13;
            checkBox_NoTable.Text = "Hide Monsters without DropTable";
            checkBox_NoTable.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip_SubId
            // 
            contextMenuStrip_SubId.Items.AddRange(new ToolStripItem[] { toolStripMenuItem_DeleteSubId });
            contextMenuStrip_SubId.Name = "contextMenuStrip_SubId";
            contextMenuStrip_SubId.Size = new Size(145, 26);
            // 
            // toolStripMenuItem_DeleteSubId
            // 
            toolStripMenuItem_DeleteSubId.Name = "toolStripMenuItem_DeleteSubId";
            toolStripMenuItem_DeleteSubId.Size = new Size(144, 22);
            toolStripMenuItem_DeleteSubId.Text = "Delete Sub ID";
            // 
            // contextMenuStrip_TreeViewItem
            // 
            contextMenuStrip_TreeViewItem.Items.AddRange(new ToolStripItem[] { toolStripMenuItem_TreeViewRename, toolStripMenuItem_TreeViewViewReferences });
            contextMenuStrip_TreeViewItem.Name = "contextMenuStrip_Item";
            contextMenuStrip_TreeViewItem.Size = new Size(160, 48);
            // 
            // toolStripMenuItem_TreeViewRename
            // 
            toolStripMenuItem_TreeViewRename.Name = "toolStripMenuItem_TreeViewRename";
            toolStripMenuItem_TreeViewRename.Size = new Size(159, 22);
            toolStripMenuItem_TreeViewRename.Text = "Rename";
            // 
            // toolStripMenuItem_TreeViewViewReferences
            // 
            toolStripMenuItem_TreeViewViewReferences.Name = "toolStripMenuItem_TreeViewViewReferences";
            toolStripMenuItem_TreeViewViewReferences.Size = new Size(159, 22);
            toolStripMenuItem_TreeViewViewReferences.Text = "View References";
            // 
            // DropInfo
            // 
            DropInfo.Location = new Point(788, 64);
            DropInfo.Name = "DropInfo";
            DropInfo.Size = new Size(476, 510);
            DropInfo.TabIndex = 14;
            // 
            // MonsterView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(249, 249, 249);
            ClientSize = new Size(1274, 582);
            Controls.Add(DropInfo);
            Controls.Add(checkBox_NoTable);
            Controls.Add(btn_ReloadData);
            Controls.Add(groupBox_DropTableDetails);
            Controls.Add(btn_Filter);
            Controls.Add(groupBox_Filter);
            Controls.Add(groupBox_MonsterDetails);
            Controls.Add(progressBar1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MonsterView";
            Text = "Grace";
            ((System.ComponentModel.ISupportInitialize)monsterDataGrid).EndInit();
            groupBox_MonsterDetails.ResumeLayout(false);
            contextMenuStrip_DropInfo.ResumeLayout(false);
            contextMenuStrip_TreeView.ResumeLayout(false);
            groupBox_DropTableDetails.ResumeLayout(false);
            groupBox_Filter.ResumeLayout(false);
            groupBox_Filter.PerformLayout();
            contextMenuStrip_SubId.ResumeLayout(false);
            contextMenuStrip_TreeViewItem.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btn_ReloadData;
        private GroupBox groupBox_MonsterDetails;
        private DataGridView monsterDataGrid;
        private TreeView dropTreeView;
        private GroupBox groupBox_DropTableDetails;
        private DataGridViewTextBoxColumn cMonsterName;
        private DataGridViewTextBoxColumn cLocation;
        private DataGridViewTextBoxColumn cMonsterId;
        private ProgressBar progressBar1;
        private RadioButton radioButton_FilterAll;
        private GroupBox groupBox_Filter;
        private RadioButton radioButton_FilterId;
        private RadioButton radioButton_FilterName;
        private RadioButton radioButton_FilterDrop;
        private RadioButton radioButton_FilterLocation;
        private Button btn_Filter;
        private ToolStripMenuItem toolStripMenuItem_InfoRemove;
        private ToolStripMenuItem toolStripMenuItem_InfoSetItem;
        private ToolStripMenuItem toolStripMenuItem_InfoRename;
        private ToolStripMenuItem toolStripMenuItem_InfoSetDropGroup;
        private RadioButton radioButton_FilterDropGroupId;
        private RadioButton radioButton_FilterItemId;
        private CheckBox checkBox_NoTable;
        public ContextMenuStrip contextMenuStrip_DropInfo;
        private ToolStripMenuItem toolStripMenuItem_InfoViewReferences;
        private ToolStripMenuItem toolStripMenuItem_AddSubId;
        public ContextMenuStrip contextMenuStrip_TreeView;
        public ToolStripMenuItem toolStripMenuItem_DeleteSubId;
        public ContextMenuStrip contextMenuStrip_SubId;
        private ToolStripMenuItem toolStripMenuItem_TreeViewRename;
        private ToolStripMenuItem toolStripMenuItem_TreeViewViewReferences;
        public ContextMenuStrip contextMenuStrip_TreeViewItem;
        public DropInfo DropInfo;
    }
}
