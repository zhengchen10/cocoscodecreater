namespace CocosCodeCreater
{
    partial class SceneExplorer
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addComponentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSpriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSpriteOnlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSpriteScale9ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addLabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addSpriteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addSpriteOnlineToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addSpriteScale9ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addLabelToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addButtonToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(432, 450);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeView1_KeyDown);
            this.treeView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addComponentToolStripMenuItem,
            this.addSpriteToolStripMenuItem,
            this.addSpriteOnlineToolStripMenuItem,
            this.addSpriteScale9ToolStripMenuItem,
            this.addLabelToolStripMenuItem,
            this.addButtonToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(181, 158);
            // 
            // addComponentToolStripMenuItem
            // 
            this.addComponentToolStripMenuItem.Name = "addComponentToolStripMenuItem";
            this.addComponentToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addComponentToolStripMenuItem.Text = "添加Component";
            // 
            // addSpriteToolStripMenuItem
            // 
            this.addSpriteToolStripMenuItem.Name = "addSpriteToolStripMenuItem";
            this.addSpriteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addSpriteToolStripMenuItem.Text = "添加Sprite";
            this.addSpriteToolStripMenuItem.Click += new System.EventHandler(this.addSpriteToolStripMenuItem_Click);
            // 
            // addSpriteOnlineToolStripMenuItem
            // 
            this.addSpriteOnlineToolStripMenuItem.Name = "addSpriteOnlineToolStripMenuItem";
            this.addSpriteOnlineToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addSpriteOnlineToolStripMenuItem.Text = "添加Sprite(Online)";
            this.addSpriteOnlineToolStripMenuItem.Click += new System.EventHandler(this.addSpriteOnlineToolStripMenuItem_Click);
            // 
            // addSpriteScale9ToolStripMenuItem
            // 
            this.addSpriteScale9ToolStripMenuItem.Name = "addSpriteScale9ToolStripMenuItem";
            this.addSpriteScale9ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addSpriteScale9ToolStripMenuItem.Text = "添加Sprite(Scale9)";
            this.addSpriteScale9ToolStripMenuItem.Click += new System.EventHandler(this.addSpriteScale9ToolStripMenuItem_Click);
            // 
            // addLabelToolStripMenuItem
            // 
            this.addLabelToolStripMenuItem.Name = "addLabelToolStripMenuItem";
            this.addLabelToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addLabelToolStripMenuItem.Text = "添加Label";
            this.addLabelToolStripMenuItem.Click += new System.EventHandler(this.addLabelToolStripMenuItem_Click);
            // 
            // addButtonToolStripMenuItem
            // 
            this.addButtonToolStripMenuItem.Name = "addButtonToolStripMenuItem";
            this.addButtonToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addButtonToolStripMenuItem.Text = "添加Button";
            this.addButtonToolStripMenuItem.Click += new System.EventHandler(this.addButtonToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSpriteToolStripMenuItem1,
            this.addSpriteOnlineToolStripMenuItem1,
            this.addSpriteScale9ToolStripMenuItem1,
            this.addLabelToolStripMenuItem1,
            this.addButtonToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(180, 114);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // addSpriteToolStripMenuItem1
            // 
            this.addSpriteToolStripMenuItem1.Name = "addSpriteToolStripMenuItem1";
            this.addSpriteToolStripMenuItem1.Size = new System.Drawing.Size(179, 22);
            this.addSpriteToolStripMenuItem1.Text = "添加Sprite";
            this.addSpriteToolStripMenuItem1.Click += new System.EventHandler(this.addSpriteToolStripMenuItem1_Click);
            // 
            // addSpriteOnlineToolStripMenuItem1
            // 
            this.addSpriteOnlineToolStripMenuItem1.Name = "addSpriteOnlineToolStripMenuItem1";
            this.addSpriteOnlineToolStripMenuItem1.Size = new System.Drawing.Size(179, 22);
            this.addSpriteOnlineToolStripMenuItem1.Text = "添加Sprite(Online)";
            // 
            // addSpriteScale9ToolStripMenuItem1
            // 
            this.addSpriteScale9ToolStripMenuItem1.Name = "addSpriteScale9ToolStripMenuItem1";
            this.addSpriteScale9ToolStripMenuItem1.Size = new System.Drawing.Size(179, 22);
            this.addSpriteScale9ToolStripMenuItem1.Text = "添加Sprite(Scale9)";
            // 
            // addLabelToolStripMenuItem1
            // 
            this.addLabelToolStripMenuItem1.Name = "addLabelToolStripMenuItem1";
            this.addLabelToolStripMenuItem1.Size = new System.Drawing.Size(179, 22);
            this.addLabelToolStripMenuItem1.Text = "添加Label";
            this.addLabelToolStripMenuItem1.Click += new System.EventHandler(this.addLabelToolStripMenuItem1_Click);
            // 
            // addButtonToolStripMenuItem1
            // 
            this.addButtonToolStripMenuItem1.Name = "addButtonToolStripMenuItem1";
            this.addButtonToolStripMenuItem1.Size = new System.Drawing.Size(179, 22);
            this.addButtonToolStripMenuItem1.Text = "添加Button";
            this.addButtonToolStripMenuItem1.Click += new System.EventHandler(this.addButtonToolStripMenuItem1_Click);
            // 
            // SceneExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 450);
            this.Controls.Add(this.treeView1);
            this.Name = "SceneExplorer";
            this.Text = "场景编辑器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SceneExplorer_FormClosing);
            this.contextMenuStrip.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addSpriteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSpriteOnlineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSpriteScale9ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addLabelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addButtonToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addSpriteToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addSpriteOnlineToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addSpriteScale9ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addLabelToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addButtonToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addComponentToolStripMenuItem;
    }
}