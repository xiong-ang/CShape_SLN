using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClipboardDemo
{
    class ClipboardDataFormat
    {
        public static readonly string TEXT="Text";
        public static readonly string IMAGE="Image";
        public static readonly string FILEDROP="FileDrop";
        public static readonly string AUDIO="Audio";
        public static readonly string USERDEFINED="UserDefined";
    }

    class ClipboardProcesser
    {
        public static void SetDataToClipboard(object o, string type)
        {
            try
            {
                if (o != null)
                {
                    if (type == ClipboardDataFormat.TEXT)
                    {
                        Clipboard.SetText((string)o);
                    }
                    if (type == ClipboardDataFormat.FILEDROP)
                    {
                        Clipboard.SetFileDropList((System.Collections.Specialized.StringCollection)o);
                    }
                    if (type == ClipboardDataFormat.IMAGE)
                    {
                        Clipboard.SetImage((System.Drawing.Image)o);
                    }
                    if (type == ClipboardDataFormat.AUDIO)
                    {
                        Clipboard.SetAudio((Stream)o);
                    }
                    if (type == ClipboardDataFormat.USERDEFINED)
                    {
                        MyItem item = (MyItem)o;
                        item.ToClipboard();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public static object GetDataFromClipboardByType(string type)
        {
            object retObj = null;
            try
            {
                if (type == ClipboardDataFormat.TEXT)
                {
                    retObj = Clipboard.GetText();
                }
                if (type == ClipboardDataFormat.FILEDROP)
                {
                    retObj = Clipboard.GetFileDropList();
                }
                if (type == ClipboardDataFormat.IMAGE)
                {
                    retObj = Clipboard.GetImage();
                }
                if (type == ClipboardDataFormat.AUDIO)
                {
                    retObj = Clipboard.GetAudioStream();
                }
                if (type == ClipboardDataFormat.USERDEFINED)
                {
                    IDataObject iData = Clipboard.GetDataObject();
                    retObj = (MyItem)iData.GetData(type);
                }                
            }
            catch (Exception)
            {
                retObj = null;
            }
            return retObj;
        }

        public static string GetDataTypeFromClipboard()
        {
            string type = string.Empty;

            try
            {
                if (Clipboard.ContainsText())
                {
                    type = ClipboardDataFormat.TEXT;
                }
                else if (Clipboard.ContainsFileDropList())
                {
                    type = ClipboardDataFormat.FILEDROP;
                }
                else if (Clipboard.ContainsImage())
                {
                    type = ClipboardDataFormat.IMAGE;
                }
                else if (Clipboard.ContainsAudio())
                {
                    type = ClipboardDataFormat.AUDIO;
                }
                else
                {
                    IDataObject iData = Clipboard.GetDataObject();
                    if (iData.GetDataPresent(ClipboardDataFormat.USERDEFINED))
                    {
                        type = ClipboardDataFormat.USERDEFINED;
                    } 
                }
            }
            catch (Exception)
            {
            }
            return type;
        }
    }

    [Serializable]
    public class MyItem
    {
        public string ItemName { get; set; }
        public object ItemData { get; set; }
        
        public void ToClipboard()
        {
            DataFormats.Format format = DataFormats.GetFormat(ClipboardDataFormat.USERDEFINED);

            IDataObject dataObj = new DataObject();
            dataObj.SetData(format.Name, false, this);
            Clipboard.SetDataObject(dataObj, false);
        }
    }
}
