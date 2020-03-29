namespace CocosCodeCreater
{
    partial class ActionExplorer
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addActionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveByToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scaleToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scaleByToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateByToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fadeInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fadeOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delayTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.callFuncToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sequenceToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.spawnToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.repeatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.repeatForeverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.effectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.easeBounceInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.easeBounceOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.easeBounceInOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.easeBackInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.easeBackOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.easeBackInOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.easeElasticInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.easeElasticOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.easeElasticInOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.easeExponentialInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.easeExponentialOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.easeExponentialInOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.easeRateActionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.easeSineInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.easeSineOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.easeSineInOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sequenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spawnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addActionToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 26);
            // 
            // addActionToolStripMenuItem
            // 
            this.addActionToolStripMenuItem.Name = "addActionToolStripMenuItem";
            this.addActionToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.addActionToolStripMenuItem.Text = "添加Action";
            this.addActionToolStripMenuItem.Click += new System.EventHandler(this.addActionToolStripMenuItem_Click);
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(292, 266);
            this.treeView1.TabIndex = 1;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeView1_KeyDown);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actionsToolStripMenuItem,
            this.actionGroupToolStripMenuItem,
            this.effectsToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(154, 70);
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveToToolStripMenuItem,
            this.moveByToolStripMenuItem,
            this.scaleToToolStripMenuItem,
            this.scaleByToolStripMenuItem,
            this.rotateToToolStripMenuItem,
            this.rotateByToolStripMenuItem,
            this.fadeInToolStripMenuItem,
            this.fadeOutToolStripMenuItem,
            this.delayTimeToolStripMenuItem,
            this.callFuncToolStripMenuItem});
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.actionsToolStripMenuItem.Text = "Actions";
            // 
            // moveToToolStripMenuItem
            // 
            this.moveToToolStripMenuItem.Name = "moveToToolStripMenuItem";
            this.moveToToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.moveToToolStripMenuItem.Text = "MoveTo";
            this.moveToToolStripMenuItem.Click += new System.EventHandler(this.moveToToolStripMenuItem_Click);
            // 
            // moveByToolStripMenuItem
            // 
            this.moveByToolStripMenuItem.Name = "moveByToolStripMenuItem";
            this.moveByToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.moveByToolStripMenuItem.Text = "MoveBy";
            this.moveByToolStripMenuItem.Click += new System.EventHandler(this.moveByToolStripMenuItem_Click);
            // 
            // scaleToToolStripMenuItem
            // 
            this.scaleToToolStripMenuItem.Name = "scaleToToolStripMenuItem";
            this.scaleToToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.scaleToToolStripMenuItem.Text = "ScaleTo";
            this.scaleToToolStripMenuItem.Click += new System.EventHandler(this.scaleToToolStripMenuItem_Click);
            // 
            // scaleByToolStripMenuItem
            // 
            this.scaleByToolStripMenuItem.Name = "scaleByToolStripMenuItem";
            this.scaleByToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.scaleByToolStripMenuItem.Text = "ScaleBy";
            this.scaleByToolStripMenuItem.Click += new System.EventHandler(this.scaleByToolStripMenuItem_Click);
            // 
            // rotateToToolStripMenuItem
            // 
            this.rotateToToolStripMenuItem.Name = "rotateToToolStripMenuItem";
            this.rotateToToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.rotateToToolStripMenuItem.Text = "RotateTo";
            this.rotateToToolStripMenuItem.Click += new System.EventHandler(this.rotateToToolStripMenuItem_Click);
            // 
            // rotateByToolStripMenuItem
            // 
            this.rotateByToolStripMenuItem.Name = "rotateByToolStripMenuItem";
            this.rotateByToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.rotateByToolStripMenuItem.Text = "RotateBy";
            this.rotateByToolStripMenuItem.Click += new System.EventHandler(this.rotateByToolStripMenuItem_Click);
            // 
            // fadeInToolStripMenuItem
            // 
            this.fadeInToolStripMenuItem.Name = "fadeInToolStripMenuItem";
            this.fadeInToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.fadeInToolStripMenuItem.Text = "FadeIn ";
            this.fadeInToolStripMenuItem.Click += new System.EventHandler(this.fadeInToolStripMenuItem_Click);
            // 
            // fadeOutToolStripMenuItem
            // 
            this.fadeOutToolStripMenuItem.Name = "fadeOutToolStripMenuItem";
            this.fadeOutToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.fadeOutToolStripMenuItem.Text = "FadeOut  ";
            this.fadeOutToolStripMenuItem.Click += new System.EventHandler(this.fadeOutToolStripMenuItem_Click);
            // 
            // delayTimeToolStripMenuItem
            // 
            this.delayTimeToolStripMenuItem.Name = "delayTimeToolStripMenuItem";
            this.delayTimeToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.delayTimeToolStripMenuItem.Text = "DelayTime  ";
            this.delayTimeToolStripMenuItem.Click += new System.EventHandler(this.delayTimeToolStripMenuItem_Click);
            // 
            // callFuncToolStripMenuItem
            // 
            this.callFuncToolStripMenuItem.Name = "callFuncToolStripMenuItem";
            this.callFuncToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.callFuncToolStripMenuItem.Text = "CallFunc";
            this.callFuncToolStripMenuItem.Click += new System.EventHandler(this.callFuncToolStripMenuItem_Click);
            // 
            // actionGroupToolStripMenuItem
            // 
            this.actionGroupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sequenceToolStripMenuItem1,
            this.spawnToolStripMenuItem1,
            this.repeatToolStripMenuItem,
            this.repeatForeverToolStripMenuItem});
            this.actionGroupToolStripMenuItem.Name = "actionGroupToolStripMenuItem";
            this.actionGroupToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.actionGroupToolStripMenuItem.Text = "Action Group";
            // 
            // sequenceToolStripMenuItem1
            // 
            this.sequenceToolStripMenuItem1.Name = "sequenceToolStripMenuItem1";
            this.sequenceToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.sequenceToolStripMenuItem1.Text = "Sequence";
            this.sequenceToolStripMenuItem1.Click += new System.EventHandler(this.sequenceToolStripMenuItem1_Click);
            // 
            // spawnToolStripMenuItem1
            // 
            this.spawnToolStripMenuItem1.Name = "spawnToolStripMenuItem1";
            this.spawnToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.spawnToolStripMenuItem1.Text = "Spawn";
            this.spawnToolStripMenuItem1.Click += new System.EventHandler(this.spawnToolStripMenuItem1_Click);
            // 
            // repeatToolStripMenuItem
            // 
            this.repeatToolStripMenuItem.Name = "repeatToolStripMenuItem";
            this.repeatToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.repeatToolStripMenuItem.Text = "Repeat  ";
            this.repeatToolStripMenuItem.Click += new System.EventHandler(this.repeatToolStripMenuItem_Click);
            // 
            // repeatForeverToolStripMenuItem
            // 
            this.repeatForeverToolStripMenuItem.Name = "repeatForeverToolStripMenuItem";
            this.repeatForeverToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.repeatForeverToolStripMenuItem.Text = "RepeatForever";
            this.repeatForeverToolStripMenuItem.Click += new System.EventHandler(this.repeatForeverToolStripMenuItem_Click);
            // 
            // effectsToolStripMenuItem
            // 
            this.effectsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.easeBounceInToolStripMenuItem,
            this.easeBounceOutToolStripMenuItem,
            this.easeBounceInOutToolStripMenuItem,
            this.easeBackInToolStripMenuItem,
            this.easeBackOutToolStripMenuItem,
            this.easeBackInOutToolStripMenuItem,
            this.easeElasticInToolStripMenuItem,
            this.easeElasticOutToolStripMenuItem,
            this.easeElasticInOutToolStripMenuItem,
            this.easeExponentialInToolStripMenuItem,
            this.easeExponentialOutToolStripMenuItem,
            this.easeExponentialInOutToolStripMenuItem,
            this.easeRateActionToolStripMenuItem,
            this.easeSineInToolStripMenuItem,
            this.easeSineOutToolStripMenuItem,
            this.easeSineInOutToolStripMenuItem,
            this.speedToolStripMenuItem});
            this.effectsToolStripMenuItem.Name = "effectsToolStripMenuItem";
            this.effectsToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.effectsToolStripMenuItem.Text = "Effects";
            // 
            // easeBounceInToolStripMenuItem
            // 
            this.easeBounceInToolStripMenuItem.Name = "easeBounceInToolStripMenuItem";
            this.easeBounceInToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.easeBounceInToolStripMenuItem.Text = "EaseBounceIn";
            // 
            // easeBounceOutToolStripMenuItem
            // 
            this.easeBounceOutToolStripMenuItem.Name = "easeBounceOutToolStripMenuItem";
            this.easeBounceOutToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.easeBounceOutToolStripMenuItem.Text = "EaseBounceOut";
            // 
            // easeBounceInOutToolStripMenuItem
            // 
            this.easeBounceInOutToolStripMenuItem.Name = "easeBounceInOutToolStripMenuItem";
            this.easeBounceInOutToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.easeBounceInOutToolStripMenuItem.Text = "EaseBounceInOut";
            // 
            // easeBackInToolStripMenuItem
            // 
            this.easeBackInToolStripMenuItem.Name = "easeBackInToolStripMenuItem";
            this.easeBackInToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.easeBackInToolStripMenuItem.Text = "EaseBackIn";
            // 
            // easeBackOutToolStripMenuItem
            // 
            this.easeBackOutToolStripMenuItem.Name = "easeBackOutToolStripMenuItem";
            this.easeBackOutToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.easeBackOutToolStripMenuItem.Text = "EaseBackOut";
            // 
            // easeBackInOutToolStripMenuItem
            // 
            this.easeBackInOutToolStripMenuItem.Name = "easeBackInOutToolStripMenuItem";
            this.easeBackInOutToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.easeBackInOutToolStripMenuItem.Text = "EaseBackInOut";
            // 
            // easeElasticInToolStripMenuItem
            // 
            this.easeElasticInToolStripMenuItem.Name = "easeElasticInToolStripMenuItem";
            this.easeElasticInToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.easeElasticInToolStripMenuItem.Text = "EaseElasticIn";
            // 
            // easeElasticOutToolStripMenuItem
            // 
            this.easeElasticOutToolStripMenuItem.Name = "easeElasticOutToolStripMenuItem";
            this.easeElasticOutToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.easeElasticOutToolStripMenuItem.Text = "EaseElasticOut";
            // 
            // easeElasticInOutToolStripMenuItem
            // 
            this.easeElasticInOutToolStripMenuItem.Name = "easeElasticInOutToolStripMenuItem";
            this.easeElasticInOutToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.easeElasticInOutToolStripMenuItem.Text = "EaseElasticInOut";
            // 
            // easeExponentialInToolStripMenuItem
            // 
            this.easeExponentialInToolStripMenuItem.Name = "easeExponentialInToolStripMenuItem";
            this.easeExponentialInToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.easeExponentialInToolStripMenuItem.Text = "EaseExponentialIn";
            // 
            // easeExponentialOutToolStripMenuItem
            // 
            this.easeExponentialOutToolStripMenuItem.Name = "easeExponentialOutToolStripMenuItem";
            this.easeExponentialOutToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.easeExponentialOutToolStripMenuItem.Text = "EaseExponentialOut";
            // 
            // easeExponentialInOutToolStripMenuItem
            // 
            this.easeExponentialInOutToolStripMenuItem.Name = "easeExponentialInOutToolStripMenuItem";
            this.easeExponentialInOutToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.easeExponentialInOutToolStripMenuItem.Text = "EaseExponentialInOut";
            // 
            // easeRateActionToolStripMenuItem
            // 
            this.easeRateActionToolStripMenuItem.Name = "easeRateActionToolStripMenuItem";
            this.easeRateActionToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.easeRateActionToolStripMenuItem.Text = "EaseRateAction";
            // 
            // easeSineInToolStripMenuItem
            // 
            this.easeSineInToolStripMenuItem.Name = "easeSineInToolStripMenuItem";
            this.easeSineInToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.easeSineInToolStripMenuItem.Text = "EaseSineIn";
            // 
            // easeSineOutToolStripMenuItem
            // 
            this.easeSineOutToolStripMenuItem.Name = "easeSineOutToolStripMenuItem";
            this.easeSineOutToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.easeSineOutToolStripMenuItem.Text = "EaseSineOut";
            // 
            // easeSineInOutToolStripMenuItem
            // 
            this.easeSineInOutToolStripMenuItem.Name = "easeSineInOutToolStripMenuItem";
            this.easeSineInOutToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.easeSineInOutToolStripMenuItem.Text = "EaseSineInOut";
            // 
            // speedToolStripMenuItem
            // 
            this.speedToolStripMenuItem.Name = "speedToolStripMenuItem";
            this.speedToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.speedToolStripMenuItem.Text = "Speed";
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sequenceToolStripMenuItem,
            this.spawnToolStripMenuItem});
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(181, 70);
            // 
            // sequenceToolStripMenuItem
            // 
            this.sequenceToolStripMenuItem.Name = "sequenceToolStripMenuItem";
            this.sequenceToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sequenceToolStripMenuItem.Text = "Sequence";
            this.sequenceToolStripMenuItem.Click += new System.EventHandler(this.sequenceToolStripMenuItem_Click);
            // 
            // spawnToolStripMenuItem
            // 
            this.spawnToolStripMenuItem.Name = "spawnToolStripMenuItem";
            this.spawnToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.spawnToolStripMenuItem.Text = "Spawn";
            this.spawnToolStripMenuItem.Click += new System.EventHandler(this.spawnToolStripMenuItem_Click);
            // 
            // ActionExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.treeView1);
            this.Name = "ActionExplorer";
            this.Text = "事件编辑器";
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStripMenuItem addActionToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scaleToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scaleByToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotateToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fadeInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fadeOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delayTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem callFuncToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sequenceToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem spawnToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem repeatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem repeatForeverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem effectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveByToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotateByToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem easeBounceInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem easeBounceOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem easeBounceInOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem easeBackInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem easeBackOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem easeBackInOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem easeElasticInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem easeElasticOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem easeElasticInOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem easeExponentialInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem easeExponentialOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem easeExponentialInOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem easeRateActionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem easeSineInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem easeSineOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem easeSineInOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem speedToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem sequenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spawnToolStripMenuItem;
    }
}
