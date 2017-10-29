using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClipboardDemo
{
    public partial class Form1 : Form
    {
        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern IntPtr SetClipboardViewer(IntPtr hwnd);
        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern IntPtr ChangeClipboardChain(IntPtr hwnd, IntPtr hWndNext);
        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        private const int WM_DRAWCLIPBOARD = 0x308;
        private const int WM_CHANGECBCHAIN = 0x30D;
        private IntPtr NextClipHwnd;

        public Form1()
        {
            InitializeComponent();
            this.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NextClipHwnd = SetClipboardViewer(this.Handle);
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case WM_DRAWCLIPBOARD:
                    SendMessage(NextClipHwnd, m.Msg, m.WParam, m.LParam);

                    string type = ClipboardProcesser.GetDataTypeFromClipboard();
                    Object data = ClipboardProcesser.GetDataFromClipboardByType(type);

                    this.Text = "Clipboard contains: "+type;
                    if (type == ClipboardDataFormat.TEXT)
                    {
                        this.textBox1.Text = (string)data;
                    }
                    if (type == ClipboardDataFormat.FILEDROP)
                    {
                        this.textBox1.Text = ((StringCollection)data)[0].ToString();
                    }
                    if (type == ClipboardDataFormat.IMAGE)
                    {
                        this.pictureBox1.Image = (Image)data;
                    }
                    if (type == ClipboardDataFormat.AUDIO)
                    {
                        this.textBox1.Text = (string)data;
                    }
                    if (type == ClipboardDataFormat.USERDEFINED)
                    {
                        this.textBox1.Text = ((MyItem)data).ItemName;
                    }  

                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        private void Form_Closed(object sender, System.EventArgs e)
        {
            ChangeClipboardChain(this.Handle, NextClipHwnd);
            SendMessage(NextClipHwnd, WM_CHANGECBCHAIN, this.Handle, NextClipHwnd);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyItem item = new MyItem();
            item.ItemName = "My Item!";

            ClipboardProcesser.SetDataToClipboard(item, ClipboardDataFormat.USERDEFINED);
        }
    }
}
