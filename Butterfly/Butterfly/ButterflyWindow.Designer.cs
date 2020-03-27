using System.Drawing;

namespace Butterfly
{
    partial class ButterflyWindow
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ofd_Rule = new System.Windows.Forms.OpenFileDialog();
            this.Lbl_RulePath = new System.Windows.Forms.Label();
            this.ofd_Btf = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.Menu_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Sel_RuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Sel_BtfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RulePage = new System.Windows.Forms.TabPage();
            this.RTB_RulePage = new System.Windows.Forms.RichTextBox();
            this.tableCtrl = new System.Windows.Forms.TabControl();
            this.menuStrip1.SuspendLayout();
            this.RulePage.SuspendLayout();
            this.tableCtrl.SuspendLayout();
            this.SuspendLayout();
            // 
            // ofd_Rule
            // 
            this.ofd_Rule.FileName = "ofd_Rule";
            this.ofd_Rule.Filter = "Json文件(*.json)|*.json";
            this.ofd_Rule.FileOk += new System.ComponentModel.CancelEventHandler(this.OFDRule_FileOk);
            // 
            // Lbl_RulePath
            // 
            this.Lbl_RulePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Lbl_RulePath.Location = new System.Drawing.Point(12, 25);
            this.Lbl_RulePath.Name = "Lbl_RulePath";
            this.Lbl_RulePath.Size = new System.Drawing.Size(776, 23);
            this.Lbl_RulePath.TabIndex = 2;
            this.Lbl_RulePath.Text = "Rule Path:";
            this.Lbl_RulePath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ofd_Btf
            // 
            this.ofd_Btf.FileName = "ofd_Rule";
            this.ofd_Btf.Filter = "Btf文件(*.btf)|*.btf";
            this.ofd_Btf.FileOk += new System.ComponentModel.CancelEventHandler(this.OFDBtf_FileOk);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 25);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // Menu_ToolStripMenuItem
            // 
            this.Menu_ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Sel_RuleToolStripMenuItem,
            this.Sel_BtfToolStripMenuItem});
            this.Menu_ToolStripMenuItem.Name = "Menu_ToolStripMenuItem";
            this.Menu_ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.Menu_ToolStripMenuItem.Text = "菜单";
            // 
            // Sel_RuleToolStripMenuItem
            // 
            this.Sel_RuleToolStripMenuItem.Name = "Sel_RuleToolStripMenuItem";
            this.Sel_RuleToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.Sel_RuleToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.Sel_RuleToolStripMenuItem.Text = "选择 Rule";
            this.Sel_RuleToolStripMenuItem.Click += new System.EventHandler(this.BtnSelectRule_Click);
            // 
            // Sel_BtfToolStripMenuItem
            // 
            this.Sel_BtfToolStripMenuItem.Name = "Sel_BtfToolStripMenuItem";
            this.Sel_BtfToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.Sel_BtfToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.Sel_BtfToolStripMenuItem.Text = "选择 Btf";
            this.Sel_BtfToolStripMenuItem.Click += new System.EventHandler(this.BtnSelectBtf_Click);
            // 
            // RulePage
            // 
            this.RulePage.Controls.Add(this.RTB_RulePage);
            this.RulePage.Location = new System.Drawing.Point(4, 22);
            this.RulePage.Name = "RulePage";
            this.RulePage.Padding = new System.Windows.Forms.Padding(3);
            this.RulePage.Size = new System.Drawing.Size(792, 373);
            this.RulePage.TabIndex = 0;
            this.RulePage.Text = "RulePage";
            this.RulePage.UseVisualStyleBackColor = true;
            // 
            // RTB_RulePage
            // 
            this.RTB_RulePage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RTB_RulePage.BackColor = System.Drawing.Color.White;
            this.RTB_RulePage.Location = new System.Drawing.Point(0, 0);
            this.RTB_RulePage.Name = "RTB_RulePage";
            this.RTB_RulePage.ReadOnly = true;
            this.RTB_RulePage.Size = new System.Drawing.Size(792, 373);
            this.RTB_RulePage.TabIndex = 0;
            this.RTB_RulePage.Text = "";
            // 
            // tableCtrl
            // 
            this.tableCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableCtrl.Controls.Add(this.RulePage);
            this.tableCtrl.ImeMode = System.Windows.Forms.ImeMode.On;
            this.tableCtrl.Location = new System.Drawing.Point(0, 51);
            this.tableCtrl.Name = "tableCtrl";
            this.tableCtrl.SelectedIndex = 0;
            this.tableCtrl.Size = new System.Drawing.Size(800, 399);
            this.tableCtrl.TabIndex = 8;
            // 
            // ButterflyWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableCtrl);
            this.Controls.Add(this.Lbl_RulePath);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ButterflyWindow";
            this.Text = "Butterfly";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.RulePage.ResumeLayout(false);
            this.tableCtrl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog ofd_Rule;
        private System.Windows.Forms.Label Lbl_RulePath;
        private System.Windows.Forms.OpenFileDialog ofd_Btf;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Menu_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Sel_RuleToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem Sel_BtfToolStripMenuItem;
        private System.Windows.Forms.TabPage RulePage;
        private System.Windows.Forms.TabControl tableCtrl;
        private System.Windows.Forms.RichTextBox RTB_RulePage;
    }
}

