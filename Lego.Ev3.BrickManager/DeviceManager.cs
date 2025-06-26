using Lego.Ev3.Framework;
using Lego.Ev3.Framework.Configuration;
using Lego.Ev3.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lego.Ev3.BrickManager
{
    public partial class DeviceManager : Form
    {

        private List<ConfigEntry> Entries;
        public DeviceManager()
        {
            InitializeComponent();
            Width = 494;
            Height = 355;
            buttonSave.Visible = false; //TODO enable saving brick config json
            checkBoxFull.Visible = false;
        }


        public async Task Initialize()
        {
            Entries = new List<ConfigEntry>();

            IEnumerable<PortInfo> ports = await Manager.Brick.Console.PortScan();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                sb.AppendLine($"Brick {(ChainLayer)i}");

                int start = i * 4;
                int end = start + 4;

                IEnumerable<PortInfo> inputPorts = ports.Where(p => p.Number >= start && p.Number < end);
                foreach (PortInfo portInfo in inputPorts)
                {
                    InputPortName name = (InputPortName)portInfo.Number - start;
                    if (portInfo.Status == PortStatus.OK)
                    {
                        ConfigEntry entry = new ConfigEntry
                        {
                            Layer = i,
                            PortName = name.ToString(),
                            Type = portInfo.Device.Value,
                        };
                        Entries.Add(entry);
                        sb.AppendLine($"\tPort {name}  - {portInfo.Status}  - {portInfo.Device}");
                    }
                    else sb.AppendLine($"\tPort {name}  - {portInfo.Status}");
                }

                start += 16;
                end = start + 4;

                IEnumerable<PortInfo> outputPorts = ports.Where(p => p.Number >= start && p.Number < end);
                foreach (PortInfo portInfo in outputPorts)
                {
                    OutputPortName name = (OutputPortName)portInfo.Number - start;
                    if (portInfo.Status == PortStatus.OK)
                    {
                        ConfigEntry entry = new ConfigEntry
                        {
                            Layer = i,
                            PortName = name.ToString(),
                            Type = portInfo.Device.Value,
                        };
                        Entries.Add(entry);
                        sb.AppendLine($"\tPort {name}  - {portInfo.Status}  - {portInfo.Device}");
                    }
                    else sb.AppendLine($"\tPort {name}  - {portInfo.Status}");
                }
            }
            textBoxDeviceList.Text = sb.ToString();
        }

        private async void ButtonRefresh_Click(object sender, EventArgs e)
        {
            await Initialize();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
        }


        private class ConfigEntry
        {
            public int Layer { get; set; }

            public DeviceType Type { get; set; }

            public string PortName { get; set; }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            BrickOptions brickOptions = new BrickOptions();
        }
    }
}
