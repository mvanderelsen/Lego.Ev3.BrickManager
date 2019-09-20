using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lego.Ev3.BrickManager
{
    public partial class StatusBar : UserControl
    {
        public StatusBar()
        {
            InitializeComponent();
        }

        public void SetLoading()
        {
            toolStripLabel.Text = "Loading...";
        }

        public void Set(DirectoryInfo info)
        {
            if (info.TotalByteSize == 0) toolStripLabel.Text = $"{info.ItemCount} items";
            else toolStripLabel.Text = $"{info.ItemCount} items ({DirectoryContent.ToFileSize(info.TotalByteSize)})";
        }
    }
}
