namespace Grace.View;

partial class MainView
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
        tabControl = new TabControl();
        tabPage_Monsters = new TabPage();
        tabPage_DropGroups = new TabPage();
        tabControl.SuspendLayout();
        SuspendLayout();
        // 
        // tabControl
        // 
        tabControl.Controls.Add(tabPage_Monsters);
        tabControl.Controls.Add(tabPage_DropGroups);
        tabControl.ItemSize = new Size(61, 20);
        tabControl.Location = new Point(12, 11);
        tabControl.Name = "tabControl";
        tabControl.SelectedIndex = 0;
        tabControl.Size = new Size(1281, 609);
        tabControl.TabIndex = 14;
        // 
        // tabPage_Monsters
        // 
        tabPage_Monsters.Location = new Point(4, 24);
        tabPage_Monsters.Name = "tabPage_Monsters";
        tabPage_Monsters.Padding = new Padding(3);
        tabPage_Monsters.Size = new Size(1273, 581);
        tabPage_Monsters.TabIndex = 0;
        tabPage_Monsters.Text = "Monsters";
        tabPage_Monsters.UseVisualStyleBackColor = true;
        // 
        // tabPage_DropGroups
        // 
        tabPage_DropGroups.BackColor = Color.Transparent;
        tabPage_DropGroups.Location = new Point(4, 24);
        tabPage_DropGroups.Name = "tabPage_DropGroups";
        tabPage_DropGroups.Size = new Size(1273, 581);
        tabPage_DropGroups.TabIndex = 1;
        tabPage_DropGroups.Text = "DropGroups";
        tabPage_DropGroups.UseVisualStyleBackColor = true;
        // 
        // MainView
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1304, 631);
        Controls.Add(tabControl);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Name = "MainView";
        Text = "Grace";
        tabControl.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private TabControl tabControl;
    private TabPage tabPage_Monsters;
    private TabPage tabPage_DropGroups;
}