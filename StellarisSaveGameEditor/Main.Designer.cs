namespace StellarisSaveGameEditor
{
    partial class Main
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
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageMain = new System.Windows.Forms.TabPage();
            this.labelLastBombardment = new System.Windows.Forms.Label();
            this.labelFortificationHealth = new System.Windows.Forms.Label();
            this.textBoxFortificationHealth = new System.Windows.Forms.TextBox();
            this.labelPlanetSize = new System.Windows.Forms.Label();
            this.labelPlanetOrbit = new System.Windows.Forms.Label();
            this.textBoxPlanetSize = new System.Windows.Forms.TextBox();
            this.textBoxOrbit = new System.Windows.Forms.TextBox();
            this.labelPlanetClass = new System.Windows.Forms.Label();
            this.textBoxPlanetClass = new System.Windows.Forms.TextBox();
            this.textBoxPlanetName = new System.Windows.Forms.TextBox();
            this.labelPlanetName = new System.Windows.Forms.Label();
            this.tabPageTiles = new System.Windows.Forms.TabPage();
            this.labelChoosePlanetTitle = new System.Windows.Forms.Label();
            this.comboBoxChoosePlanet = new System.Windows.Forms.ComboBox();
            this.labelEntity = new System.Windows.Forms.Label();
            this.textBoxPlanetId = new System.Windows.Forms.TextBox();
            this.labelPlanetId = new System.Windows.Forms.Label();
            this.textBoxLastBombardment = new System.Windows.Forms.TextBox();
            this.textBoxEntity = new System.Windows.Forms.TextBox();
            this.menuStripMain.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(1384, 24);
            this.menuStripMain.TabIndex = 1;
            this.menuStripMain.Text = "menuStripMain";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripMenuItem1,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(100, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 589);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1384, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageMain);
            this.tabControl.Controls.Add(this.tabPageTiles);
            this.tabControl.Location = new System.Drawing.Point(0, 59);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1378, 527);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl.TabIndex = 3;
            // 
            // tabPageMain
            // 
            this.tabPageMain.Controls.Add(this.textBoxEntity);
            this.tabPageMain.Controls.Add(this.textBoxLastBombardment);
            this.tabPageMain.Controls.Add(this.labelPlanetId);
            this.tabPageMain.Controls.Add(this.textBoxPlanetId);
            this.tabPageMain.Controls.Add(this.labelEntity);
            this.tabPageMain.Controls.Add(this.labelLastBombardment);
            this.tabPageMain.Controls.Add(this.labelFortificationHealth);
            this.tabPageMain.Controls.Add(this.textBoxFortificationHealth);
            this.tabPageMain.Controls.Add(this.labelPlanetSize);
            this.tabPageMain.Controls.Add(this.labelPlanetOrbit);
            this.tabPageMain.Controls.Add(this.textBoxPlanetSize);
            this.tabPageMain.Controls.Add(this.textBoxOrbit);
            this.tabPageMain.Controls.Add(this.labelPlanetClass);
            this.tabPageMain.Controls.Add(this.textBoxPlanetClass);
            this.tabPageMain.Controls.Add(this.textBoxPlanetName);
            this.tabPageMain.Controls.Add(this.labelPlanetName);
            this.tabPageMain.Location = new System.Drawing.Point(4, 22);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMain.Size = new System.Drawing.Size(1370, 501);
            this.tabPageMain.TabIndex = 0;
            this.tabPageMain.Text = "Main";
            this.tabPageMain.UseVisualStyleBackColor = true;
            // 
            // labelLastBombardment
            // 
            this.labelLastBombardment.AutoSize = true;
            this.labelLastBombardment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLastBombardment.Location = new System.Drawing.Point(24, 165);
            this.labelLastBombardment.Name = "labelLastBombardment";
            this.labelLastBombardment.Size = new System.Drawing.Size(111, 13);
            this.labelLastBombardment.TabIndex = 12;
            this.labelLastBombardment.Text = "Last Bombardment";
            // 
            // labelFortificationHealth
            // 
            this.labelFortificationHealth.AutoSize = true;
            this.labelFortificationHealth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFortificationHealth.Location = new System.Drawing.Point(20, 139);
            this.labelFortificationHealth.Name = "labelFortificationHealth";
            this.labelFortificationHealth.Size = new System.Drawing.Size(115, 13);
            this.labelFortificationHealth.TabIndex = 11;
            this.labelFortificationHealth.Text = "Fortification Health";
            // 
            // textBoxFortificationHealth
            // 
            this.textBoxFortificationHealth.Location = new System.Drawing.Point(141, 136);
            this.textBoxFortificationHealth.Name = "textBoxFortificationHealth";
            this.textBoxFortificationHealth.Size = new System.Drawing.Size(148, 20);
            this.textBoxFortificationHealth.TabIndex = 10;
            // 
            // labelPlanetSize
            // 
            this.labelPlanetSize.AutoSize = true;
            this.labelPlanetSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlanetSize.Location = new System.Drawing.Point(104, 113);
            this.labelPlanetSize.Name = "labelPlanetSize";
            this.labelPlanetSize.Size = new System.Drawing.Size(31, 13);
            this.labelPlanetSize.TabIndex = 9;
            this.labelPlanetSize.Text = "Size";
            // 
            // labelPlanetOrbit
            // 
            this.labelPlanetOrbit.AutoSize = true;
            this.labelPlanetOrbit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlanetOrbit.Location = new System.Drawing.Point(101, 87);
            this.labelPlanetOrbit.Name = "labelPlanetOrbit";
            this.labelPlanetOrbit.Size = new System.Drawing.Size(34, 13);
            this.labelPlanetOrbit.TabIndex = 8;
            this.labelPlanetOrbit.Text = "Orbit";
            // 
            // textBoxPlanetSize
            // 
            this.textBoxPlanetSize.Location = new System.Drawing.Point(141, 110);
            this.textBoxPlanetSize.Name = "textBoxPlanetSize";
            this.textBoxPlanetSize.Size = new System.Drawing.Size(148, 20);
            this.textBoxPlanetSize.TabIndex = 7;
            // 
            // textBoxOrbit
            // 
            this.textBoxOrbit.Location = new System.Drawing.Point(141, 84);
            this.textBoxOrbit.Name = "textBoxOrbit";
            this.textBoxOrbit.Size = new System.Drawing.Size(148, 20);
            this.textBoxOrbit.TabIndex = 6;
            // 
            // labelPlanetClass
            // 
            this.labelPlanetClass.AutoSize = true;
            this.labelPlanetClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlanetClass.Location = new System.Drawing.Point(98, 61);
            this.labelPlanetClass.Name = "labelPlanetClass";
            this.labelPlanetClass.Size = new System.Drawing.Size(37, 13);
            this.labelPlanetClass.TabIndex = 3;
            this.labelPlanetClass.Text = "Class";
            // 
            // textBoxPlanetClass
            // 
            this.textBoxPlanetClass.Location = new System.Drawing.Point(141, 58);
            this.textBoxPlanetClass.Name = "textBoxPlanetClass";
            this.textBoxPlanetClass.Size = new System.Drawing.Size(148, 20);
            this.textBoxPlanetClass.TabIndex = 2;
            // 
            // textBoxPlanetName
            // 
            this.textBoxPlanetName.Location = new System.Drawing.Point(141, 32);
            this.textBoxPlanetName.Name = "textBoxPlanetName";
            this.textBoxPlanetName.Size = new System.Drawing.Size(148, 20);
            this.textBoxPlanetName.TabIndex = 1;
            // 
            // labelPlanetName
            // 
            this.labelPlanetName.AutoSize = true;
            this.labelPlanetName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlanetName.Location = new System.Drawing.Point(96, 35);
            this.labelPlanetName.Name = "labelPlanetName";
            this.labelPlanetName.Size = new System.Drawing.Size(39, 13);
            this.labelPlanetName.TabIndex = 0;
            this.labelPlanetName.Text = "Name";
            // 
            // tabPageTiles
            // 
            this.tabPageTiles.Location = new System.Drawing.Point(4, 22);
            this.tabPageTiles.Name = "tabPageTiles";
            this.tabPageTiles.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTiles.Size = new System.Drawing.Size(1370, 501);
            this.tabPageTiles.TabIndex = 1;
            this.tabPageTiles.Text = "Tiles";
            this.tabPageTiles.UseVisualStyleBackColor = true;
            // 
            // labelChoosePlanetTitle
            // 
            this.labelChoosePlanetTitle.AutoSize = true;
            this.labelChoosePlanetTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelChoosePlanetTitle.Location = new System.Drawing.Point(12, 35);
            this.labelChoosePlanetTitle.Name = "labelChoosePlanetTitle";
            this.labelChoosePlanetTitle.Size = new System.Drawing.Size(100, 13);
            this.labelChoosePlanetTitle.TabIndex = 4;
            this.labelChoosePlanetTitle.Text = "Choose a Planet";
            // 
            // comboBoxChoosePlanet
            // 
            this.comboBoxChoosePlanet.FormattingEnabled = true;
            this.comboBoxChoosePlanet.Location = new System.Drawing.Point(118, 32);
            this.comboBoxChoosePlanet.Name = "comboBoxChoosePlanet";
            this.comboBoxChoosePlanet.Size = new System.Drawing.Size(200, 21);
            this.comboBoxChoosePlanet.TabIndex = 5;
            this.comboBoxChoosePlanet.SelectedIndexChanged += new System.EventHandler(this.comboBoxChoosePlanet_SelectedIndexChanged);
            // 
            // labelEntity
            // 
            this.labelEntity.AutoSize = true;
            this.labelEntity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEntity.Location = new System.Drawing.Point(96, 191);
            this.labelEntity.Name = "labelEntity";
            this.labelEntity.Size = new System.Drawing.Size(39, 13);
            this.labelEntity.TabIndex = 13;
            this.labelEntity.Text = "Entity";
            // 
            // textBoxPlanetId
            // 
            this.textBoxPlanetId.Enabled = false;
            this.textBoxPlanetId.Location = new System.Drawing.Point(141, 6);
            this.textBoxPlanetId.Name = "textBoxPlanetId";
            this.textBoxPlanetId.Size = new System.Drawing.Size(148, 20);
            this.textBoxPlanetId.TabIndex = 14;
            // 
            // labelPlanetId
            // 
            this.labelPlanetId.AutoSize = true;
            this.labelPlanetId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlanetId.Location = new System.Drawing.Point(115, 9);
            this.labelPlanetId.Name = "labelPlanetId";
            this.labelPlanetId.Size = new System.Drawing.Size(20, 13);
            this.labelPlanetId.TabIndex = 15;
            this.labelPlanetId.Text = "ID";
            // 
            // textBoxLastBombardment
            // 
            this.textBoxLastBombardment.Location = new System.Drawing.Point(141, 162);
            this.textBoxLastBombardment.Name = "textBoxLastBombardment";
            this.textBoxLastBombardment.Size = new System.Drawing.Size(148, 20);
            this.textBoxLastBombardment.TabIndex = 16;
            // 
            // textBoxEntity
            // 
            this.textBoxEntity.Location = new System.Drawing.Point(141, 188);
            this.textBoxEntity.Name = "textBoxEntity";
            this.textBoxEntity.Size = new System.Drawing.Size(148, 20);
            this.textBoxEntity.TabIndex = 17;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 611);
            this.Controls.Add(this.comboBoxChoosePlanet);
            this.Controls.Add(this.labelChoosePlanetTitle);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStripMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStripMain;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Stellaris Savegame Editor";
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPageMain.ResumeLayout(false);
            this.tabPageMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageMain;
        private System.Windows.Forms.TabPage tabPageTiles;
        private System.Windows.Forms.Label labelPlanetName;
        private System.Windows.Forms.Label labelChoosePlanetTitle;
        private System.Windows.Forms.ComboBox comboBoxChoosePlanet;
        private System.Windows.Forms.TextBox textBoxOrbit;
        private System.Windows.Forms.Label labelPlanetClass;
        private System.Windows.Forms.TextBox textBoxPlanetClass;
        private System.Windows.Forms.TextBox textBoxPlanetName;
        private System.Windows.Forms.Label labelPlanetSize;
        private System.Windows.Forms.Label labelPlanetOrbit;
        private System.Windows.Forms.TextBox textBoxPlanetSize;
        private System.Windows.Forms.Label labelLastBombardment;
        private System.Windows.Forms.Label labelFortificationHealth;
        private System.Windows.Forms.TextBox textBoxFortificationHealth;
        private System.Windows.Forms.Label labelEntity;
        private System.Windows.Forms.Label labelPlanetId;
        private System.Windows.Forms.TextBox textBoxPlanetId;
        private System.Windows.Forms.TextBox textBoxEntity;
        private System.Windows.Forms.TextBox textBoxLastBombardment;
    }
}

