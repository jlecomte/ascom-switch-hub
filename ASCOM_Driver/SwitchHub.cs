/*
 * SwitchHub.cs
 * Copyright (C) 2023 - Present, Julien Lecomte - All Rights Reserved
 * Licensed under the MIT License. See the accompanying LICENSE file for terms.
 */

using ASCOM.DeviceInterface;
using ASCOM.DriverAccess;
using ASCOM.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;

namespace ASCOM.DarkSkyGeek
{
    //
    // Your driver's DeviceID is ASCOM.DarkSkyGeek.SwitchHub
    //
    // The Guid attribute sets the CLSID for ASCOM.DarkSkyGeek.SwitchHub
    // The ClassInterface/None attribute prevents an empty interface called
    // _DarkSkyGeek from being created and used as the [default] interface
    //

    /// <summary>
    /// DarkSkyGeek’s ASCOM Switch Hub
    /// </summary>
    [Guid("d0be89bb-fd96-49d7-a496-a99452becc50")]
    [ClassInterface(ClassInterfaceType.None)]
    public class SwitchHub : ISwitchV2
    {
        /// <summary>
        /// ASCOM DeviceID (COM ProgID) for this driver.
        /// The DeviceID is used by ASCOM applications to load the driver at runtime.
        /// </summary>
        internal static string driverID = "ASCOM.DarkSkyGeek.SwitchHub";

        /// <summary>
        /// Driver description that displays in the ASCOM Chooser.
        /// </summary>
        private static string driverDescription = "DarkSkyGeek’s ASCOM Switch Hub";

        // Constants used for Profile persistence
        internal static string traceStateProfileName = "Trace Level";
        internal static string traceStateDefault = "false";

        internal static string prependDeviceNameProfileName = "Prepend Device Name";
        internal static string prependDeviceNameDefault = "false";

        internal static string devicesProfileName = "Switch Devices";
        internal static string devicesDefault = string.Empty;

        // Variables to hold the current device configuration
        internal bool prependDeviceName = false;
        internal List<SwitchDevice> devices = new List<SwitchDevice>();

        /// <summary>
        /// Private variable to hold the connected state
        /// </summary>
        private bool connectedState;

        /// <summary>
        /// Variable to hold the trace logger object (creates a diagnostic log file with information that you specify)
        /// </summary>
        internal TraceLogger tl;

        /// <summary>
        /// Initializes a new instance of the <see cref="DarkSkyGeek"/> class.
        /// Must be public for COM registration.
        /// </summary>
        public SwitchHub()
        {
            tl = new TraceLogger("", "DarkSkyGeek");
            ReadProfile();
            tl.LogMessage("SwitchHub", "Starting initialization");
            connectedState = false;
            tl.LogMessage("SwitchHub", "Completed initialization");
        }

        //
        // PUBLIC COM INTERFACE ISwitchV2 IMPLEMENTATION
        //

        #region Common properties and methods.

        /// <summary>
        /// Displays the Setup Dialog form.
        /// If the user clicks the OK button to dismiss the form, then
        /// the new settings are saved, otherwise the old values are reloaded.
        /// THIS IS THE ONLY PLACE WHERE SHOWING USER INTERFACE IS ALLOWED!
        /// </summary>
        public void SetupDialog()
        {
            if (IsConnected)
                System.Windows.Forms.MessageBox.Show("Already connected, just press OK");

            using (SetupDialogForm F = new SetupDialogForm(this))
            {
                var result = F.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    WriteProfile(); // Persist device configuration values to the ASCOM Profile store
                }
            }
        }

        /// <summary>Returns the list of custom action names supported by this driver.</summary>
        /// <value>An ArrayList of strings (SafeArray collection) containing the names of supported actions.</value>
        public ArrayList SupportedActions
        {
            get
            {
                tl.LogMessage("SupportedActions Get", "Returning empty arraylist");
                return new ArrayList();
            }
        }

        /// <summary>Invokes the specified device-specific custom action.</summary>
        /// <param name="ActionName">A well known name agreed by interested parties that represents the action to be carried out.</param>
        /// <param name="ActionParameters">List of required parameters or an <see cref="String.Empty">Empty String</see> if none are required.</param>
        /// <returns>A string response. The meaning of returned strings is set by the driver author.
        /// <para>Suppose filter wheels start to appear with automatic wheel changers; new actions could be <c>QueryWheels</c> and <c>SelectWheel</c>. The former returning a formatted list
        /// of wheel names and the second taking a wheel name and making the change, returning appropriate values to indicate success or failure.</para>
        /// </returns>
        public string Action(string actionName, string actionParameters)
        {
            LogMessage("", "Action {0}, parameters {1} not implemented", actionName, actionParameters);
            throw new ASCOM.ActionNotImplementedException("Action " + actionName + " is not implemented by this driver");
        }

        /// <summary>
        /// Transmits an arbitrary string to the device and does not wait for a response.
        /// Optionally, protocol framing characters may be added to the string before transmission.
        /// </summary>
        /// <param name="Command">The literal command string to be transmitted.</param>
        /// <param name="Raw">
        /// if set to <c>true</c> the string is transmitted 'as-is'.
        /// If set to <c>false</c> then protocol framing characters may be added prior to transmission.
        /// </param>
        public void CommandBlind(string command, bool raw)
        {
            CheckConnected("CommandBlind");
            throw new ASCOM.MethodNotImplementedException("CommandBlind");
        }

        /// <summary>
        /// Transmits an arbitrary string to the device and waits for a boolean response.
        /// Optionally, protocol framing characters may be added to the string before transmission.
        /// </summary>
        /// <param name="Command">The literal command string to be transmitted.</param>
        /// <param name="Raw">
        /// if set to <c>true</c> the string is transmitted 'as-is'.
        /// If set to <c>false</c> then protocol framing characters may be added prior to transmission.
        /// </param>
        /// <returns>
        /// Returns the interpreted boolean response received from the device.
        /// </returns>
        public bool CommandBool(string command, bool raw)
        {
            CheckConnected("CommandBool");
            throw new ASCOM.MethodNotImplementedException("CommandBool");
        }

        /// <summary>
        /// Transmits an arbitrary string to the device and waits for a string response.
        /// Optionally, protocol framing characters may be added to the string before transmission.
        /// </summary>
        /// <param name="Command">The literal command string to be transmitted.</param>
        /// <param name="Raw">
        /// if set to <c>true</c> the string is transmitted 'as-is'.
        /// If set to <c>false</c> then protocol framing characters may be added prior to transmission.
        /// </param>
        /// <returns>
        /// Returns the string response received from the device.
        /// </returns>
        public string CommandString(string command, bool raw)
        {
            CheckConnected("CommandString");
            throw new ASCOM.MethodNotImplementedException("CommandString");
        }

        /// <summary>
        /// Dispose the late-bound interface, if needed. Will release it via COM
        /// if it is a COM object, else if native .NET will just dereference it
        /// for GC.
        /// </summary>
        public void Dispose()
        {
            tl.Enabled = false;
            tl.Dispose();
            tl = null;
        }

        /// <summary>
        /// Set True to connect to the device hardware. Set False to disconnect from the device hardware.
        /// You can also read the property to check whether it is connected. This reports the current hardware state.
        /// </summary>
        /// <value><c>true</c> if connected to the hardware; otherwise, <c>false</c>.</value>
        public bool Connected
        {
            get
            {
                LogMessage("Connected", "Get {0}", IsConnected);
                return IsConnected;
            }
            set
            {
                tl.LogMessage("Connected", "Set {0}", value);
                if (value == IsConnected)
                    return;

                if (value)
                {
                    ConnectAll();
                    connectedState = true;
                }
                else
                {
                    connectedState = false;
                    DisconnectAll();
                }
            }
        }

        /// <summary>
        /// Returns a description of the device, such as manufacturer and modelnumber. Any ASCII characters may be used.
        /// </summary>
        /// <value>The description.</value>
        public string Description
        {
            get
            {
                tl.LogMessage("Description Get", driverDescription);
                return driverDescription;
            }
        }

        /// <summary>
        /// Descriptive and version information about this ASCOM driver.
        /// </summary>
        public string DriverInfo
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string driverInfo = driverDescription + " Version " + String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                tl.LogMessage("DriverInfo Get", driverInfo);
                return driverInfo;
            }
        }

        /// <summary>
        /// A string containing only the major and minor version of the driver formatted as 'm.n'.
        /// </summary>
        public string DriverVersion
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string driverVersion = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                tl.LogMessage("DriverVersion Get", driverVersion);
                return driverVersion;
            }
        }

        /// <summary>
        /// The interface version number that this device supports. 
        /// </summary>
        public short InterfaceVersion
        {
            get
            {
                LogMessage("InterfaceVersion Get", "2");
                return Convert.ToInt16("2");
            }
        }

        /// <summary>
        /// The short name of the driver, for display purposes
        /// </summary>
        public string Name
        {
            get
            {
                tl.LogMessage("Name Get", driverDescription);
                return driverDescription;
            }
        }

        #endregion

        #region ISwitchV2 Implementation

        private short numSwitch = 0;

        /// <summary>
        /// The number of switches managed by this driver
        /// </summary>
        /// <returns>The number of devices managed by this driver.</returns>
        public short MaxSwitch
        {
            get
            {
                tl.LogMessage("MaxSwitch Get", numSwitch.ToString());
                return this.numSwitch;
            }
        }

        /// <summary>
        /// Return the name of switch device n.
        /// </summary>
        /// <param name="id">The device number (0 to <see cref="MaxSwitch"/> - 1)</param>
        /// <returns>The name of the device</returns>
        public string GetSwitchName(short id)
        {
            CheckConnected("GetSwitchName");
            Validate("GetSwitchName", id);
            var (driver, switchId) = getDriverAndSwitchId(id);
            var switchName = driver.GetSwitchName(switchId);
            return prependDeviceName ? driver.Name + " - " + switchName : switchName;
        }

        /// <summary>
        /// Set a switch device name to a specified value.
        /// </summary>
        /// <param name="id">The device number (0 to <see cref="MaxSwitch"/> - 1)</param>
        /// <param name="name">The name of the device</param>
        public void SetSwitchName(short id, string name)
        {
            CheckConnected("SetSwitchName");
            Validate("SetSwitchName", id);
            var (driver, switchId) = getDriverAndSwitchId(id);
            driver.SetSwitchName(switchId, name);
        }

        /// <summary>
        /// Gets the description of the specified switch device. This is to allow a fuller description of
        /// the device to be returned, for example for a tool tip.
        /// </summary>
        /// <param name="id">The device number (0 to <see cref="MaxSwitch"/> - 1)</param>
        /// <returns>
        /// String giving the device description.
        /// </returns>
        public string GetSwitchDescription(short id)
        {
            CheckConnected("GetSwitchDescription");
            Validate("GetSwitchDescription", id);
            var (driver, switchId) = getDriverAndSwitchId(id);
            return driver.GetSwitchDescription(switchId);
        }

        /// <summary>
        /// Reports if the specified switch device can be written to, default true.
        /// This is false if the device cannot be written to, for example a limit switch or a sensor.
        /// </summary>
        /// <param name="id">The device number (0 to <see cref="MaxSwitch"/> - 1)</param>
        /// <returns>
        /// <c>true</c> if the device can be written to, otherwise <c>false</c>.
        /// </returns>
        public bool CanWrite(short id)
        {
            CheckConnected("CanWrite");
            Validate("CanWrite", id);
            var (driver, switchId) = getDriverAndSwitchId(id);
            return driver.CanWrite(switchId);
        }

        #region Boolean switch members

        /// <summary>
        /// Return the state of switch device id as a boolean
        /// </summary>
        /// <param name="id">The device number (0 to <see cref="MaxSwitch"/> - 1)</param>
        /// <returns>True or false</returns>
        public bool GetSwitch(short id)
        {
            CheckConnected("GetSwitch");
            Validate("GetSwitch", id);
            var (driver, switchId) = getDriverAndSwitchId(id);
            return driver.GetSwitch(switchId);
        }

        /// <summary>
        /// Sets a switch controller device to the specified state, true or false.
        /// </summary>
        /// <param name="id">The device number (0 to <see cref="MaxSwitch"/> - 1)</param>
        /// <param name="state">The required control state</param>
        public void SetSwitch(short id, bool state)
        {
            CheckConnected("SetSwitch");
            Validate("SetSwitch", id);

            if (!CanWrite(id))
            {
                var str = $"SetSwitch({id}) - Cannot Write";
                tl.LogMessage("SetSwitch", str);
                throw new MethodNotImplementedException(str);
            }

            var (driver, switchId) = getDriverAndSwitchId(id);
            driver.SetSwitch(switchId, state);
        }

        #endregion

        #region Analogue members

        /// <summary>
        /// Returns the maximum value for this switch device, this must be greater than <see cref="MinSwitchValue"/>.
        /// </summary>
        /// <param name="id">The device number (0 to <see cref="MaxSwitch"/> - 1)</param>
        /// <returns>The maximum value to which this device can be set or which a read only sensor will return.</returns>
        public double MaxSwitchValue(short id)
        {
            CheckConnected("MaxSwitchValue");
            Validate("MaxSwitchValue", id);
            var (driver, switchId) = getDriverAndSwitchId(id);
            return driver.MaxSwitchValue(switchId);
        }

        /// <summary>
        /// Returns the minimum value for this switch device, this must be less than <see cref="MaxSwitchValue"/>
        /// </summary>
        /// <param name="id">The device number (0 to <see cref="MaxSwitch"/> - 1)</param>
        /// <returns>The minimum value to which this device can be set or which a read only sensor will return.</returns>
        public double MinSwitchValue(short id)
        {
            CheckConnected("MinSwitchValue");
            Validate("MinSwitchValue", id);
            var (driver, switchId) = getDriverAndSwitchId(id);
            return driver.MinSwitchValue(switchId);
        }

        /// <summary>
        /// Returns the step size that this device supports (the difference between successive values of the device).
        /// </summary>
        /// <param name="id">The device number (0 to <see cref="MaxSwitch"/> - 1)</param>
        /// <returns>The step size for this device.</returns>
        public double SwitchStep(short id)
        {
            CheckConnected("SwitchStep");
            Validate("SwitchStep", id);
            var (driver, switchId) = getDriverAndSwitchId(id);
            return driver.SwitchStep(switchId);
        }

        /// <summary>
        /// Returns the value for switch device id as a double
        /// </summary>
        /// <param name="id">The device number (0 to <see cref="MaxSwitch"/> - 1)</param>
        /// <returns>The value for this switch, this is expected to be between <see cref="MinSwitchValue"/> and
        /// <see cref="MaxSwitchValue"/>.</returns>
        public double GetSwitchValue(short id)
        {
            CheckConnected("GetSwitchValue");
            Validate("GetSwitchValue", id);
            var (driver, switchId) = getDriverAndSwitchId(id);
            return driver.GetSwitchValue(switchId);
        }

        /// <summary>
        /// Set the value for this device as a double.
        /// </summary>
        /// <param name="id">The device number (0 to <see cref="MaxSwitch"/> - 1)</param>
        /// <param name="value">The value to be set, between <see cref="MinSwitchValue"/> and <see cref="MaxSwitchValue"/></param>
        public void SetSwitchValue(short id, double value)
        {
            CheckConnected("SetSwitchValue");
            Validate("SetSwitchValue", id, value);

            if (!CanWrite(id))
            {
                tl.LogMessage("SetSwitchValue", $"SetSwitchValue({id}) - Cannot write");
                throw new ASCOM.MethodNotImplementedException($"SetSwitchValue({id}) - Cannot write");
            }

            var (driver, switchId) = getDriverAndSwitchId(id);
            driver.SetSwitchValue(switchId, value);
        }

        #endregion

        #endregion

        #region Private methods

        /// <summary>
        /// Checks that the switch id is in range and throws an InvalidValueException if it isn't
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="id">The id.</param>
        private void Validate(string message, short id)
        {
            if (id < 0 || id >= numSwitch)
            {
                tl.LogMessage(message, string.Format("Switch {0} not available, range is 0 to {1}", id, numSwitch - 1));
                throw new InvalidValueException(message, id.ToString(), string.Format("0 to {0}", numSwitch - 1));
            }
        }

        /// <summary>
        /// Checks that the switch id and value are in range and throws an
        /// InvalidValueException if they are not.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="id">The id.</param>
        /// <param name="value">The value.</param>
        private void Validate(string message, short id, double value)
        {
            Validate(message, id);
            var min = MinSwitchValue(id);
            var max = MaxSwitchValue(id);
            if (value < min || value > max)
            {
                tl.LogMessage(message, string.Format("Value {1} for Switch {0} is out of the allowed range {2} to {3}", id, value, min, max));
                throw new InvalidValueException(message, value.ToString(), string.Format("Switch({0}) range {1} to {2}", id, min, max));
            }
        }

        #endregion

        #region Private properties and methods
        // here are some useful properties and methods that can be used as required
        // to help with driver development

        #region ASCOM Registration

        // Register or unregister driver for ASCOM. This is harmless if already
        // registered or unregistered. 
        //
        /// <summary>
        /// Register or unregister the driver with the ASCOM Platform.
        /// This is harmless if the driver is already registered/unregistered.
        /// </summary>
        /// <param name="bRegister">If <c>true</c>, registers the driver, otherwise unregisters it.</param>
        private static void RegUnregASCOM(bool bRegister)
        {
            using (var P = new ASCOM.Utilities.Profile())
            {
                P.DeviceType = "Switch";
                if (bRegister)
                {
                    P.Register(driverID, driverDescription);
                }
                else
                {
                    P.Unregister(driverID);
                }
            }
        }

        /// <summary>
        /// This function registers the driver with the ASCOM Chooser and
        /// is called automatically whenever this class is registered for COM Interop.
        /// </summary>
        /// <param name="t">Type of the class being registered, not used.</param>
        /// <remarks>
        /// This method typically runs in two distinct situations:
        /// <list type="numbered">
        /// <item>
        /// In Visual Studio, when the project is successfully built.
        /// For this to work correctly, the option <c>Register for COM Interop</c>
        /// must be enabled in the project settings.
        /// </item>
        /// <item>During setup, when the installer registers the assembly for COM Interop.</item>
        /// </list>
        /// This technique should mean that it is never necessary to manually register a driver with ASCOM.
        /// </remarks>
        [ComRegisterFunction]
        public static void RegisterASCOM(Type t)
        {
            RegUnregASCOM(true);
        }

        /// <summary>
        /// This function unregisters the driver from the ASCOM Chooser and
        /// is called automatically whenever this class is unregistered from COM Interop.
        /// </summary>
        /// <param name="t">Type of the class being registered, not used.</param>
        /// <remarks>
        /// This method typically runs in two distinct situations:
        /// <list type="numbered">
        /// <item>
        /// In Visual Studio, when the project is cleaned or prior to rebuilding.
        /// For this to work correctly, the option <c>Register for COM Interop</c>
        /// must be enabled in the project settings.
        /// </item>
        /// <item>During uninstall, when the installer unregisters the assembly from COM Interop.</item>
        /// </list>
        /// This technique should mean that it is never necessary to manually unregister a driver from ASCOM.
        /// </remarks>
        [ComUnregisterFunction]
        public static void UnregisterASCOM(Type t)
        {
            RegUnregASCOM(false);
        }

        #endregion

        /// <summary>
        /// Returns true if there is a valid connection to the driver hardware
        /// </summary>
        private bool IsConnected
        {
            get
            {
                return connectedState;
            }
        }

        /// <summary>
        /// Use this function to throw an exception if we aren't connected to the hardware
        /// </summary>
        /// <param name="message"></param>
        private void CheckConnected(string message)
        {
            if (!IsConnected)
            {
                throw new ASCOM.NotConnectedException(message);
            }
        }

        /// <summary>
        /// Returns the name of the switch device with the specified id.
        /// Returns null if the device cannot be found / is not registered.
        /// </summary>
        /// <param name="deviceId"></param>
        private string getSwitchDeviceName(string deviceId)
        {
            Profile profile = new Profile();
            ArrayList registeredSwitchDevices = profile.RegisteredDevices("Switch");

            foreach (KeyValuePair kv in registeredSwitchDevices)
            {
                if (kv.Key == deviceId)
                {
                    return kv.Value;
                }
            }

            return null;
        }

        /// <summary>
        /// Read the device configuration from the ASCOM Profile store
        /// </summary>
        internal void ReadProfile()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "Switch";

                tl.Enabled = Convert.ToBoolean(driverProfile.GetValue(driverID, traceStateProfileName, string.Empty, traceStateDefault));

                prependDeviceName = Convert.ToBoolean(driverProfile.GetValue(driverID, prependDeviceNameProfileName, string.Empty, prependDeviceNameDefault));

                // Read device ids from ASCOM profile. Also filter out devices that
                // are no longer registered on the system (which can happen...)
                devices.Clear();
                string switchDevicesProfileValue = driverProfile.GetValue(driverID, devicesProfileName, string.Empty, string.Empty);
                foreach (string deviceId in switchDevicesProfileValue.Split(','))
                {
                    string deviceName = getSwitchDeviceName(deviceId);
                    if (deviceName != null)
                    {
                        devices.Add(new SwitchDevice(deviceId, deviceName));
                    }
                }
            }
        }

        /// <summary>
        /// Write the device configuration to the  ASCOM  Profile store
        /// </summary>
        internal void WriteProfile()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "Switch";
                driverProfile.WriteValue(driverID, traceStateProfileName, tl.Enabled.ToString());
                driverProfile.WriteValue(driverID, devicesProfileName, string.Join(",", devices.Select(device => device.Id)));
                driverProfile.WriteValue(driverID, prependDeviceNameProfileName, prependDeviceName.ToString());
            }
        }

        /// <summary>
        /// Log helper function that takes formatted strings and arguments
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        internal void LogMessage(string identifier, string message, params object[] args)
        {
            var msg = string.Format(message, args);
            tl.LogMessage(identifier, msg);
        }

        /// <summary>
        /// Connects all devices configured in driver settings dialog.
        /// </summary>
        private void ConnectAll()
        {
            numSwitch = 0;
            foreach (var device in devices)
            {
                try
                {
                    device.driver = new ASCOM.DriverAccess.Switch(device.Id);
                    device.driver.Connected = true;
                    numSwitch += device.driver.MaxSwitch;
                }
                catch (Exception)
                {
                    DisconnectAll(); // end in a clean state!
                    throw new ASCOM.DriverException("Cannot connect to \"" + device.Name + "\"");
                }
            }
        }

        /// <summary>
        /// Disconnects all currently connected devices.
        /// </summary>
        private void DisconnectAll()
        {
            numSwitch = 0;
            foreach (var device in devices)
            {
                if (device.driver != null)
                {
                    try
                    {
                        device.driver.Connected = false;
                        device.driver.Dispose();
                    }
                    catch (Exception)
                    {
                        // Ignore...
                    }
                    finally
                    {
                        device.driver = null;
                    }
                }
            }
        }

        /// <summary>
        /// Returns the driver corresponding to the specified switch id,
        /// and return the corresponding switch id for that driver.
        /// </summary>
        private Tuple<ASCOM.DriverAccess.Switch, short> getDriverAndSwitchId(short id)
        {
            if (id < 0)
            {
                throw new ASCOM.InvalidValueException("Invalid switch number, must be a positive integer!");
            }

            foreach (var device in devices)
            {
                if (id < device.driver.MaxSwitch)
                {
                    return Tuple.Create(device.driver, id);
                }

                id -= device.driver.MaxSwitch;
            }

            return null;
        }

        #endregion
    }

    class SwitchDevice
    {
        readonly public string Id;
        readonly public string Name;
        public ASCOM.DriverAccess.Switch driver;

        public SwitchDevice(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
