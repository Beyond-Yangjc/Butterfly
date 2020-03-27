using ByYoung.Butterfly;
using ByYoung.Butterfly.AutoGenerate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Butterfly
{
    public partial class ButterflyWindow : Form
    { 
        private Dictionary<string, TabPage> btfPageDic = new Dictionary<string, TabPage>();

        private string Default_Rule_Path = Application.StartupPath + "/Rule.json";
        public ButterflyWindow()
        {
            InitializeComponent();

            this.Sel_BtfToolStripMenuItem.Enabled = false;
             
            if(File.Exists(Default_Rule_Path))
            {
                Init(Default_Rule_Path, null); 
            }
        }
        private static BattleREC battleRec;
        private void BtnSelectRule_Click(object sender, EventArgs e)
        {
            ofd_Rule.ShowDialog();
        }

        private async void OFDRule_FileOk(object sender, CancelEventArgs e)
        {
            if (ofd_Rule.CheckFileExists)
            { 
                var _stream = ofd_Rule.OpenFile(); 
                Init(ofd_Rule.FileName, _stream);
            }
        }

        async void Init(string _path, Stream _stream)
        {
            Lbl_RulePath.Text = $"Rule文件路径：{_path}";
            battleRec = new BattleREC(_path, _stream);
            await Task.Delay(1000);
            this.Sel_BtfToolStripMenuItem.Enabled = true;
            RTB_RulePage.Text = BattleREC.rule.content;
        }

        private void BtnSelectBtf_Click(object sender, EventArgs e)
        {
            ofd_Btf.ShowDialog();
        }

        private void OFDBtf_FileOk(object sender, CancelEventArgs e)
        {
            if (ofd_Btf.CheckFileExists)
            {
                var _btfPage = new System.Windows.Forms.TabPage();
                _btfPage.Location = new System.Drawing.Point(4, 22);
                _btfPage.Name = ofd_Btf.FileName;
                _btfPage.Padding = new System.Windows.Forms.Padding(3);
                _btfPage.Size = new System.Drawing.Size(792, 373);
                _btfPage.Text = ofd_Btf.SafeFileName;
                _btfPage.UseVisualStyleBackColor = true;
                _btfPage.ContextMenuStrip = new ContextMenuStrip();
                _btfPage.ToolTipText = _btfPage.Name;

                var _closeToolStripItem = new ToolStripMenuItem();
                _closeToolStripItem.Name = "_closeToolStripItem";
                _closeToolStripItem.Size = new System.Drawing.Size(180, 22);
                _closeToolStripItem.Text = "关闭";
                _closeToolStripItem.Click += new EventHandler(OnBtfCloseClick);

                _btfPage.ContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { _closeToolStripItem });
                _btfPage.ContextMenuStrip.Name = "_btfPageContextMenu";
                _btfPage.ContextMenuStrip.Size = new System.Drawing.Size(181, 48);

                var _RTB_BtfPage = new System.Windows.Forms.RichTextBox();
                _RTB_BtfPage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
                _RTB_BtfPage.Location = new System.Drawing.Point(0, 0);
                _RTB_BtfPage.Name = "RTB_RulePage";
                _RTB_BtfPage.Size = new System.Drawing.Size(792, 373);
                _RTB_BtfPage.TabIndex = 0;
                _RTB_BtfPage.Text = "";
                _RTB_BtfPage.ReadOnly = true;
                _RTB_BtfPage.BackColor = Color.White;
                _RTB_BtfPage.ContextMenuStrip = _btfPage.ContextMenuStrip;

                _btfPage.Controls.Add(_RTB_BtfPage);
                if (btfPageDic.TryGetValue(ofd_Btf.FileName, out var _page))
                {
                    tableCtrl.Controls.Remove(_page);
                    btfPageDic[ofd_Btf.FileName] = _btfPage;
                }
                else
                {
                    btfPageDic.Add(_btfPage.Name, _btfPage);
                }
                tableCtrl.Controls.Add(_btfPage);
                _btfPage.Show();

                var _stream = ofd_Btf.OpenFile();
                var _btf = Loader.LoadBtfFile(_stream);
                this.Sel_BtfToolStripMenuItem.Enabled = true;
                _RTB_BtfPage.Text = BattleREC.ShowRec(_btf);
            }
        }

        private void OnBtfCloseClick(object sender, EventArgs e)
        {
            if (tableCtrl.SelectedTab.Text != "RulePage")
                if (btfPageDic.ContainsKey(tableCtrl.SelectedTab.Name))
                {
                    tableCtrl.Controls.Remove(tableCtrl.SelectedTab);
                    btfPageDic.Remove(tableCtrl.SelectedTab.Name);
                }
        }
    }
}
