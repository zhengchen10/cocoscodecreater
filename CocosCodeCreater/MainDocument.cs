using kernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace CocosCodeCreater
{
    public partial class MainDocument : DockContent
    {
        private Bitmap bufferImage = null;
        private Graphics bufferGraph = null;
        public IEditer Editer { get; set; }
        private Global global;
        public MainDocument(Global global)
        {
            InitializeComponent();
            this.global = global;
            AutoScaleMode = AutoScaleMode.Dpi;
            DockAreas = DockAreas.Document;
            Editer = null;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            this.SizeChanged += MainDocument_SizeChanged;
        }

        private void MainDocument_SizeChanged(object sender, EventArgs e)
        {
            /*if(this.Width >0 && this.Height > 0)
            {
                bufferImage = new Bitmap(this.Width, this.Height);
                bufferGraph = Graphics.FromImage(bufferImage);
                bufferGraph.Clear(this.BackColor);
            }*/
        }
        public void DoKeyDown(KeyEventArgs e)
        {
            OnKeyDown(e);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (Editer != null)
            {
                Editer.OnKeyDown(e);
            }
                
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (Editer != null)
            {
                Editer.OnKeyUp(e);
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (e.Graphics == null)
                return;
            if(Editer != null)
            {
                bufferImage = new Bitmap(this.Width, this.Height);
                bufferGraph = Graphics.FromImage(bufferImage);
                bufferGraph.Clear(this.BackColor);

                Editer.OnPaint(bufferGraph, new Rectangle(0, 0, this.Width, this.Height));
                //bufferImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
                e.Graphics.DrawImage((Image)bufferImage, 0, 0);
                
            }
                
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (Editer != null)
                Editer.OnMouseClick(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (Editer != null)
            {
                if (Editer.OnMouseDown(e))
                {
                    this.Refresh();
                }
            }
                
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (Editer != null)
            {
                if (Editer.OnMouseMove(e)){
                    this.Refresh();
                }
            }
                
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (Editer != null)
                Editer.OnMouseUp(e);
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (Editer != null)
                if (Editer.OnMouseWheel(e))
                {
                    this.Refresh();
                }
        }

        private void MainDocument_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                RedoManager.Undo();
                this.Refresh();
            } else if (e.Control && e.KeyCode == Keys.R)
            {
                RedoManager.Redo();
                this.Refresh();
            } else if(e.Control && e.KeyCode == Keys.S)
            {
                if (global.SolutionExplorer.Project != null)
                {
                    ProjectTools.saveProejct(global.SolutionExplorer.FilePath, global.SolutionExplorer.FileName, global.SolutionExplorer.Project);
                }
            }
        }
    }
}
