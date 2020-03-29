namespace CocosCodeCreater
{
    partial class SolutionExplorer
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
            this.components = new System.ComponentModel.Container();
            this.mainMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newSceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newComponentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newResourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSoundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newPlistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPlistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.添加APIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newSceneToolStripMenuItem,
            this.newComponentToolStripMenuItem,
            this.newResourceToolStripMenuItem,
            this.添加APIToolStripMenuItem});
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(181, 114);
            // 
            // newSceneToolStripMenuItem
            // 
            this.newSceneToolStripMenuItem.Name = "newSceneToolStripMenuItem";
            this.newSceneToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newSceneToolStripMenuItem.Text = "添加Scene";
            this.newSceneToolStripMenuItem.Click += new System.EventHandler(this.newSceneToolStripMenuItem_Click);
            // 
            // newComponentToolStripMenuItem
            // 
            this.newComponentToolStripMenuItem.Name = "newComponentToolStripMenuItem";
            this.newComponentToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newComponentToolStripMenuItem.Text = "添加Component";
            this.newComponentToolStripMenuItem.Click += new System.EventHandler(this.newComponentToolStripMenuItem_Click);
            // 
            // newResourceToolStripMenuItem
            // 
            this.newResourceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addImageToolStripMenuItem,
            this.addFontToolStripMenuItem,
            this.addSoundToolStripMenuItem,
            this.newPlistToolStripMenuItem,
            this.addPlistToolStripMenuItem});
            this.newResourceToolStripMenuItem.Name = "newResourceToolStripMenuItem";
            this.newResourceToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newResourceToolStripMenuItem.Text = "添加Resource";
            this.newResourceToolStripMenuItem.Click += new System.EventHandler(this.newResourceToolStripMenuItem_Click);
            // 
            // addImageToolStripMenuItem
            // 
            this.addImageToolStripMenuItem.Name = "addImageToolStripMenuItem";
            this.addImageToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.addImageToolStripMenuItem.Text = "添加Image";
            this.addImageToolStripMenuItem.Click += new System.EventHandler(this.addImageToolStripMenuItem_Click);
            // 
            // addFontToolStripMenuItem
            // 
            this.addFontToolStripMenuItem.Name = "addFontToolStripMenuItem";
            this.addFontToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.addFontToolStripMenuItem.Text = "添加Font";
            this.addFontToolStripMenuItem.Click += new System.EventHandler(this.addFontToolStripMenuItem_Click);
            // 
            // addSoundToolStripMenuItem
            // 
            this.addSoundToolStripMenuItem.Name = "addSoundToolStripMenuItem";
            this.addSoundToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.addSoundToolStripMenuItem.Text = "添加Sound";
            this.addSoundToolStripMenuItem.Click += new System.EventHandler(this.addSoundToolStripMenuItem_Click);
            // 
            // newPlistToolStripMenuItem
            // 
            this.newPlistToolStripMenuItem.Name = "newPlistToolStripMenuItem";
            this.newPlistToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.newPlistToolStripMenuItem.Text = "新建Plist";
            this.newPlistToolStripMenuItem.Click += new System.EventHandler(this.newPlistToolStripMenuItem_Click);
            // 
            // addPlistToolStripMenuItem
            // 
            this.addPlistToolStripMenuItem.Name = "addPlistToolStripMenuItem";
            this.addPlistToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.addPlistToolStripMenuItem.Text = "添加Plist";
            this.addPlistToolStripMenuItem.Click += new System.EventHandler(this.addPlistToolStripMenuItem_Click);
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(429, 450);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeView1_KeyDown);
            // 
            // 添加APIToolStripMenuItem
            // 
            this.添加APIToolStripMenuItem.Name = "添加APIToolStripMenuItem";
            this.添加APIToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.添加APIToolStripMenuItem.Text = "添加API";
            this.添加APIToolStripMenuItem.Click += new System.EventHandler(this.添加APIToolStripMenuItem_Click);
            // 
            // SolutionExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 450);
            this.Controls.Add(this.treeView1);
            this.Name = "SolutionExplorer";
            this.Text = "解决方案管理器";
            this.mainMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ContextMenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem newSceneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newComponentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newResourceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFontToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSoundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addPlistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newPlistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加APIToolStripMenuItem;
    }
}