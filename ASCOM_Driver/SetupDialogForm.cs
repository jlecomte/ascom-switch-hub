using ASCOM.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ASCOM.DarkSkyGeek
{
    // Form not registered for COM!
    [ComVisible(false)]
    public partial class SetupDialogForm : Form
    {
        // Holder for a reference to the driver's trace logger
        private readonly SwitchHub hub;

        // Data source for grid view
        private readonly List<DataGridViewSwitchDevice> dgvSwitchDeviceList = new List<DataGridViewSwitchDevice>();

        public SetupDialogForm(SwitchHub hub)
        {
            InitializeComponent();

            // Save the provided trace logger for use within the setup dialogue
            this.hub = hub;
        }

        private void SetupDialogForm_Load(object sender, EventArgs e)
        {
            Profile profile = new Profile();

            // Populate switch device list...
            ArrayList switchDevices = profile.RegisteredDevices("Switch");
            foreach (KeyValuePair kv in switchDevices)
            {
                string deviceId = kv.Key;
                string deviceName = kv.Value;

                // Don't include the switch hub device in the list, for obvious reasons...
                if (deviceId != SwitchHub.driverID)
                {
                    DataGridViewSwitchDevice dgvSwitchDevice = new DataGridViewSwitchDevice(deviceId, deviceName)
                    {
                        Selected = hub.devices.Exists(x => x.Id == deviceId)
                    };
                    dgvSwitchDeviceList.Add(dgvSwitchDevice);
                }
            }

            var source = new BindingSource
            {
                DataSource = dgvSwitchDeviceList
            };

            switchDevicesDataGridView.DataSource = source;

            chkTrace.Checked = hub.tl.Enabled;
            prependDeviceNameChk.Checked = hub.prependDeviceName;

            if (hub.Connected)
            {
                switchDevicesDataGridView.Enabled = false;
                chkTrace.Enabled = false;
                prependDeviceNameChk.Enabled = false;
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            hub.devices.Clear();

            foreach (DataGridViewSwitchDevice dgvSwitchDevice in dgvSwitchDeviceList)
            {
                if (dgvSwitchDevice.Selected)
                {
                    hub.devices.Add(new SwitchDevice(dgvSwitchDevice.Id, dgvSwitchDevice.Name));
                }
            }

            hub.tl.Enabled = chkTrace.Checked;
            hub.prependDeviceName = prependDeviceNameChk.Checked;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BrowseToHomepage(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://github.com/jlecomte/ascom-switch-hub");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        // Deal with DataGridView quirks...
        // See http://www.codingeverything.com/2013/01/firing-datagridview-cellvaluechanged.html
        private void switchDevicesDataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (switchDevicesDataGridView.IsCurrentCellDirty)
            {
                switchDevicesDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
    }

    class DataGridViewSwitchDevice
    {
        readonly public string Id;
        public string Name { get; }
        public bool Selected { get; set; }

        public DataGridViewSwitchDevice(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
