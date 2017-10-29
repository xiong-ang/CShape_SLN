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

namespace DragDropDemo
{
    public partial class Form1 : Form
    {
        //用GUID标识应用程序
        private readonly Guid id = Guid.NewGuid();
 
        public Form1()
        {
            InitializeComponent();
        }

        //开启DragDrop
        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0)
            {
                MyListItem item = new MyListItem(id);
                item.ItemData = listBox1.Items[listBox1.SelectedIndex];
                item.ToClipboard();
            }
            DoDragDrop(Clipboard.GetDataObject(), DragDropEffects.Move);
        }


        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            IDataObject iData= Clipboard.GetDataObject();
            if (iData.GetDataPresent(MyListItem.UserFormat))
            {
                //判断是否是不同的应用程序
                if (id != ((MyListItem)iData.GetData(MyListItem.UserFormat)).AppID)
                    e.Effect = DragDropEffects.Move;
                return;
            }
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
             
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            IDataObject iData = Clipboard.GetDataObject();
            if (iData.GetDataPresent(MyListItem.UserFormat))
            {
                //判断是否是不同的应用程序
                MyListItem item = (MyListItem)iData.GetData(MyListItem.UserFormat);
                if (id != item.AppID)
                    this.listBox1.Items.Add((string)item.ItemData);
                return;
            }
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                this.listBox1.Items.Add(((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString());
            }  
        }

        //清理Clipboard
        protected override void OnClosing(CancelEventArgs e)
        {
            Clipboard.Clear();
            base.OnClosing(e);
        } 
    }

    [Serializable]
    public class MyListItem
    {
        public MyListItem(Guid appID)
        {
            AppID = appID;
        }
        public object ItemData { get; set; }
        public Guid AppID { get; set; }
        public static readonly string UserFormat = "USER_FORMAT"; 

        public void ToClipboard()
        {
            DataFormats.Format format = DataFormats.GetFormat(UserFormat);

            IDataObject dataObj = new DataObject();
            dataObj.SetData(format.Name, false, this);
            Clipboard.SetDataObject(dataObj, false);
        }
    }
}
