using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using Renci.SshNet;
using Renci.SshNet.Common;
using clib;

namespace interface_parse
{
    public partial class Form1 : Form
    {
        Stream myStream = null;
        Stream myStream1 = null;
        int ProgressMultiplier = 1;
        int ComboBoxSelectedIndex = 0;

        struct ServiceProviderInfo
        {
            public string BranchName;
            public string ServiceProviderName;
            public string IP;
            public string Role;
        }
        struct TunnelInfo
        {
            public string Number;
            public string BranchName;
            public string ServiceProviderName;
            public string Delay;
            public string Destination;
        }


        public Form1()
        {
            InitializeComponent();
            lblVersion.Text = Application.ProductVersion;
            cboxSelectJobs.SelectedIndex = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {


            try
            {   // Open the text file using a stream reader.
                string path1 = "C:\\Users\\y.tretyakov\\Desktop\\interface-parse\\interfaces.txt";
                string path2 = "C:\\Users\\y.tretyakov\\Desktop\\interface-parse\\description.txt";
                string path3 = "C:\\Users\\y.tretyakov\\Desktop\\interface-parse\\bandwidth.txt";
                string path4 = "C:\\Users\\y.tretyakov\\Desktop\\interface-parse\\qos-bandwidth.txt";
                string path_out = "C:\\Users\\y.tretyakov\\Desktop\\interface-parse\\output.txt";
                StreamReader str_1 = new StreamReader(path1);
                StreamReader str_2 = new StreamReader(path2);
                StreamReader str_3 = new StreamReader(path3);
                StreamReader str_4 = new StreamReader(path4);
                string[] lines = new string[2000];
                int i = 0;
                while (str_1.Peek() >= 0 || str_2.Peek() >= 0 || str_3.Peek() >= 0 || str_4.Peek() >= 0)
                {
                    if (str_1.Peek() >= 0)
                    {
                        lines[i] = str_1.ReadLine();
                        i++;
                    }
                    if (str_2.Peek() >= 0)
                    {
                        lines[i] = str_2.ReadLine();
                        i++;
                    }
                    if (str_3.Peek() >= 0)
                    {
                        lines[i] = str_3.ReadLine();
                        i++;
                        if (str_4.Peek() >= 0)
                        {
                            lines[i] = str_4.ReadLine();
                            i++;
                        }


                    }

                }
                StreamWriter str_output = new StreamWriter(path_out);
                foreach (string line in lines)
                {
                    str_output.WriteLine(line);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ex.Message);
            }
        }

        public void ThreadJob()
        {
            using (myStream)
            {
                int IndexCount = 0;
                StreamReader IndexReader = new StreamReader(myStream);
                while (IndexReader.ReadLine() != null)
                {
                    IndexCount++;
                }
                IndexReader.Close();

                Invoke(new Action(() =>
                {
                    WorkProgressBar.Minimum = 0;
                    WorkProgressBar.Maximum = IndexCount;
                    WorkProgressBar.Step = 1;
                    WorkProgressBar.Value = 0;
                }));

                UpdateProgress(0);

                StreamReader reader = new StreamReader(myStream1);
                string[] lines = new string[IndexCount];
                string[] buffer = new string[IndexCount];
                int i = 0;
                int k = 0;
                while (reader.Peek() >= 0)
                {
                    buffer[i] = reader.ReadLine();

                    if (buffer[i].Contains("interface") || buffer[i].Contains("service-policy"))
                    {
                        if (buffer[i].Contains("service-policy"))
                        {
                            buffer[i] = buffer[i].Insert(0, "no");
                        }
                        lines[k] = buffer[i];
                        k++;
                    }
                    i++;
                    //WorkProgressBar.Value = i;
                    //                   UpdateProgress(i);
                    Invoke(new Action(() =>
                    {
                        WorkProgressBar.Value = i;
                    }));

                }
                reader.Close();
                StreamWriter str_output1 = new StreamWriter("C:\\Users\\y.tretyakov\\Desktop\\interface-parse\\service-pl-output.txt");
                foreach (string line in lines)
                {
                    if (line != null)
                        str_output1.WriteLine(line);
                }
                str_output1.Close();
            }
        }


        private void StartButton_Click(object sender, EventArgs e)
        {

            Thread WorkParseThread = new Thread(new ThreadStart(ThreadJob));


            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\Users\\y.tretyakov\\desktop\\interface-parse";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        myStream1 = openFileDialog1.OpenFile();
                        WorkParseThread.Start();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
        public void UpdateProgress(int percentComplete)
        {
            if (!InvokeRequired)
            {
                WorkProgressBar.Value = percentComplete;
            }
            else
            {
                Invoke(new Action<int>(UpdateProgress), percentComplete);
            }
        }

        private void StartSSHButton_Click(object sender, EventArgs e)
        {
            ComboBoxSelectedIndex = cboxSelectJobs.SelectedIndex;

            Thread ChangeTunnelsThread = new Thread(new ThreadStart(ThreadChangeTunnelsJob));
            ChangeTunnelsThread.Start();

        }

        public void ThreadChangeTunnelsJob()
        {
            try
            {
                Router.Username = Username.Text;
                Router.Password = Password.Text;
                
                //                int a = (3 * 9) / 7;
                UpdateProgressMaxValueDelegate UPMVDeleg = new UpdateProgressMaxValueDelegate(UpdateProgressMaxValue);
                UpdateProgressValueDelegate UPVDeleg = new UpdateProgressValueDelegate(UpdateProgressValue);

                Invoke(new Action(() =>
                {
                    OverallProgress.Maximum = (int)RouterAddressEnd.Value - (int)RouterAddressStart.Value + 1;
                }));



                for (int i = (int)RouterAddressStart.Value; i <= (int)RouterAddressEnd.Value; i++)
                {
                    Invoke(new Action(() =>
                    {
                        OverallProgressInfo.Text = string.Concat("Processing router 10.99.0.", i.ToString());
                        RouterProgress.Maximum = 4;
                    }));


                    Router router = new Router(string.Concat("10.99.0.", i.ToString()), UPMVDeleg, UPVDeleg);
                    Invoke(new Action(() =>
                    {
                        RouterProgressInfo.Text = string.Concat("Connecting to 10.99.0.", i.ToString(), "...");
                        RouterProgress.Maximum = 4;
                    }));
                    try
                    {
                        router.Connect();
                    }
                    catch (Exception ex)
                    {
                        router.WriteLog(ex.ToString());
                    }
                    if (router.client.IsConnected)
                    {


                        Invoke(new Action(() =>
                        {
                            RouterProgressInfo.Text = "Separating Tunnel Numbers";
                        }));
                        router.SeparateTunnelNumbers(router.GetLocalTunnelNumberInfo());
                        Invoke(new Action(() =>
                            {
                                RouterProgress.Value = 1;
                            }));
                        if (router.TunnelsQuantity > 0)
                        {



                            switch (ComboBoxSelectedIndex)
                            {
                                case 0:
                                    router.Connect("10.99.0.1", Router.Username, Router.Password);
                                    router.GetEdgeTunnelInfo();
                                    router.OpenShellStreamSession();
                                    router.SyncTunnelInfo();
                                    router.RemoveUnnecessaryCryptoKeys();
                                    router.Disconnect("edge");
                                    break;
                                case 1:
                                    router.OpenShellStreamSession();
                                    router.SetNetflowLines();
                                    break;
                                default:
                                    MessageBox.Show("Job is not selected");
                                    break;

                            }
                            
                        }

                        router.Disconnect();
                    }


                    Invoke(new Action(() =>
                    {
                        OverallProgress.Value += 1;
                    }));

                }
                MessageBox.Show("Job is complete");


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + " Stack: " + ex.StackTrace + " Source: " + ex.Source + " Hres: " + ex.HResult + " InnerEx: " + ex.InnerException);
            }
        }
        public delegate int UpdateProgressMaxValueDelegate(int NewMaxValue);

        public int UpdateProgressMaxValue(int NewMaxValue)
        {
            int NewActiveProgress = 0;
            if (NewMaxValue > (RouterProgress.Maximum / 4))
            {
                NewActiveProgress = (RouterProgress.Value * (NewMaxValue * 4)) / RouterProgress.Maximum;
                ProgressMultiplier = 1;
            }
            else if (NewMaxValue < (RouterProgress.Maximum / 4))
            {
                ProgressMultiplier = (RouterProgress.Maximum / 4) / NewMaxValue;
                return RouterProgress.Value;
            }
            else
            {
                ProgressMultiplier = 1;
                return RouterProgress.Value;
            }

            Invoke(new Action(() =>
            {
                RouterProgress.Maximum = NewMaxValue * 4;
                RouterProgress.Value = NewActiveProgress;
            }));
            return NewActiveProgress;
        }

        public delegate void UpdateProgressValueDelegate(object NewValue, int type);

        public void UpdateProgressValue(object NewValue, int type)
        {
            Invoke(new Action(() =>
            {
                if (type == 1)
                {
                    RouterProgress.Value = (int)NewValue - 1 + (ProgressMultiplier);
                }
                else
                {
                    RouterProgressInfo.Text = (string)NewValue;
                }
            }));
        }




        public class Router
        {
            string RouterIP;// { get; set; }
            public int TunnelsQuantity;
            public SshClient client;
            static SshClient clientEdge;

            ShellStream shell;
            StreamWriter WriteStream;
            StreamReader ReadStream;

            public static string[] CryptoPeers = new string[] { "80.91.186.242", "77.52.192.90", "172.16.1.254" };

            public UpdateProgressMaxValueDelegate UpdateProgressMaxValueDelegateObj;
            public UpdateProgressValueDelegate UpdateProgressValueDelegateObj;

            static string WriteLogPath = "C:\\Users\\y.tretyakov\\Desktop\\interface-parse\\SyncLog.txt";
            StreamWriter str_output;

            struct RouterCommands
            {
                public static string GetLocalTunnelNumberInfo = "sh ip int br | inc Tunnel";
                public static string GetEdgeTunnelInfo = "sh run int tun ";
                public static string GetBranchTunnelInfo = "do sh run int tun ";
                public static string EnterConfigureMode = "conf t";
                public static string EnterTunnelMode = "interface Tunnel ";
                public static string RemoveTunnel = "no interface Tunnel ";
                public static string NoCommand = "no ";
                public static string WriteCommand = "do wr";
                public static string GetCryptoIsakmpKey = "do sh run | inc isakmp key";
                public static string GetCryptoIsakmpSA = "do sh crypto isakmp sa";
                public static string GetRoutes = "do sh run | inc ip route";
                public static string SetNetflowInput = "ip flow monitor NTAmon input";
                public static string SetNetflowOutput = "ip flow monitor NTAmon output";
                public static string RemoveOldNetflowIngress = "no ip flow ingress";
                public static string RemoveOldNetflowEgress = "no ip flow egress";



            }
            public struct TunnelInfo
            {
                public string TunnelNumber;
                public string TunnelDescription;
                public string TunnelBandwidth;
                public bool IsTunnelUp;
                public string TunnelDelay;
            }

            TunnelInfo[] TunnelInfoStruct;

            #region Credentials

            static public string Username { get; set; }
            static public string Password { get; set; }
            #endregion

            public Router(string ip, UpdateProgressMaxValueDelegate UPMVDel, UpdateProgressValueDelegate UPVDel )
            {
                RouterIP = ip;
                TunnelsQuantity = 0;
                TunnelInfoStruct = new TunnelInfo[20];
                client = new SshClient(ip, Router.Username, Router.Password);
                UpdateProgressMaxValueDelegateObj = UPMVDel;
                UpdateProgressValueDelegateObj = UPVDel;

            }


            public void Connect()
            {
                //using (client = new SshClient(this.RouterIP, Router.Username, Router.Password))
                //{
                client.Connect();
                //}
            }
            public void Connect(string RouterIP, string Username, string Password)
            {
                //using (client = new SshClient(this.RouterIP, Router.Username, Router.Password))
                //{
                clientEdge = new SshClient(RouterIP, Username, Password);
                clientEdge.Connect();
                //}
            }
            public void Disconnect()
            {
                client.Disconnect();
            }
            public void Disconnect(string EdgeMark)
            {
                clientEdge.Disconnect();
            }

            public void WriteLog(string Message)
            {
                string CurrentDate = DateTime.Now.ToLongDateString();
                string CurrentTime = DateTime.Now.ToLongTimeString();
                string CurrentDateTime = string.Concat(CurrentDate, " ", CurrentTime);

                using (str_output = new StreamWriter(WriteLogPath, true))
                {
                    str_output.WriteLine(string.Concat("\r\n", CurrentDateTime, "\r\nNode ", this.RouterIP.ToString(), "\r\n   ", Message));
                }
            }

            public string GetLocalTunnelNumberInfo()
            {
                using (var cmd = client.CreateCommand(RouterCommands.GetLocalTunnelNumberInfo))
                {
                    cmd.Execute();
                    return cmd.Result;
                }
            }
            public void GetEdgeTunnelInfo()
            {
                int i = 0;

                int CurrentProgressValue = UpdateProgressMaxValueDelegateObj.Invoke(TunnelsQuantity);

                while (i < TunnelsQuantity)
                {

                    UpdateProgressValueDelegateObj.Invoke(string.Concat("Gathering tunnel", TunnelInfoStruct[i].TunnelNumber, " info"), 2);

                    if (!clientEdge.IsConnected)
                        clientEdge.Connect();
                    using (var cmd = clientEdge.CreateCommand(string.Concat(RouterCommands.GetEdgeTunnelInfo, TunnelInfoStruct[i].TunnelNumber)))
                    {

                        string result = cmd.Execute();

                        int FirstIndex = result.IndexOf("description");
                        if (FirstIndex != -1)
                        {
                            string description = "";
                            int LastIndex = result.IndexOf("\r\n", FirstIndex);
                            if (FirstIndex >= 0 && LastIndex > FirstIndex)
                                description = result.Substring(FirstIndex, (LastIndex - FirstIndex));
                            string ResultDescription = "";
                            int SecondIndex = description.IndexOf(" ", 0);
                            if (SecondIndex > 0)
                                ResultDescription = string.Concat(ResultDescription, description.Substring(0, SecondIndex));
                            int ThirdIndex = description.IndexOf(" ", SecondIndex + 1);
                            if (ThirdIndex >= 0)
                                ResultDescription = string.Concat(ResultDescription, description.Substring(ThirdIndex));
                            TunnelInfoStruct[i].TunnelDescription = ResultDescription;

                            int FirstIndexB = result.IndexOf("bandwidth");
                            if (FirstIndexB != -1)
                            {
                                string bandwidth = "";
                                int LastIndexB = result.IndexOf("\r\n", FirstIndexB);
                                if (FirstIndexB >= 0 && LastIndexB > FirstIndexB)
                                    bandwidth = result.Substring(FirstIndexB, (LastIndexB - FirstIndexB));
                                TunnelInfoStruct[i].TunnelBandwidth = bandwidth;

                            }

                            int FirstIndexD = result.IndexOf("delay");
                            if (FirstIndexD != -1)
                            {
                                string delay = "";
                                int LastIndexD = result.IndexOf("\r\n", FirstIndexD);
                                if (FirstIndexD >= 0 && LastIndexD > FirstIndexD)
                                    delay = result.Substring(FirstIndexD, (LastIndexD - FirstIndexD));
                                TunnelInfoStruct[i].TunnelDelay = delay;

                            }
                        }
                    }
                    UpdateProgressValueDelegateObj.Invoke(CurrentProgressValue + i + 1, 1);
                    WriteLog(string.Concat("GetEdgeTunnelInfo Tunnel", TunnelInfoStruct[i].TunnelNumber, " -> OK"));


                    i++;
                }
            }
            public void SeparateTunnelNumbers(string TunnelList)
            {

                int i = 0;
                int index = 0;
                int EndOfTheLineIndex = 0;

                string substring = "Tunnel";
                string substringState = "down";

                while ((index = TunnelList.IndexOf(substring, index)) >= 0)
                {
                    if (index >= 0)
                        TunnelInfoStruct[i].TunnelNumber = TunnelList.Substring(index + substring.Length, 4);

                    EndOfTheLineIndex = TunnelList.IndexOf("\r\n", index);

                    if (EndOfTheLineIndex != -1)
                    {
                        if (TunnelList.LastIndexOf(substringState, EndOfTheLineIndex) == -1)
                        {
                            TunnelInfoStruct[i].IsTunnelUp = true;
                        }
                    }
                    else
                    {
                        if (TunnelList.LastIndexOf(substringState) == -1)
                        {
                            TunnelInfoStruct[i].IsTunnelUp = true;
                        }
                        i++;
                        break;
                    }
                    i++;
                    index = EndOfTheLineIndex;
                }
                TunnelsQuantity = i;
                WriteLog("SeparateTunnelNumbers -> OK");

            }

            public void SetNetflowLines ()
            {
                int i = 0;

                string[] CommandList = new string[] { RouterCommands.EnterConfigureMode };
                string result = ExecuteShellCommand(CommandList, "EndOfConft");

                int CurrentProgressValue = UpdateProgressMaxValueDelegateObj.Invoke(TunnelsQuantity);

                while (i < TunnelsQuantity)
                {
                    UpdateProgressValueDelegateObj.Invoke(string.Concat("Setting Netflow Lines in tunnel", TunnelInfoStruct[i].TunnelNumber), 2);
                    CommandList = new string[] { string.Concat(RouterCommands.EnterTunnelMode, TunnelInfoStruct[i].TunnelNumber), RouterCommands.SetNetflowInput, RouterCommands.SetNetflowOutput, RouterCommands.RemoveOldNetflowIngress, RouterCommands.RemoveOldNetflowEgress , "exit" };
                    result = ExecuteShellCommand(CommandList, "EndOfSyncTunnel");
                    WriteLog(string.Concat("Set Netflow Lines", TunnelInfoStruct[i].TunnelNumber, " ->OK"));
                    UpdateProgressValueDelegateObj.Invoke(CurrentProgressValue + i + 1, 1);
                    i++;
                }
                CommandList = new string[] { RouterCommands.WriteCommand };
                result = ExecuteShellCommand(CommandList, "EndOfWrite");

            }

            public void SyncTunnelInfo()
            {
                int i = 0;

                string[] CommandList = new string[] { RouterCommands.EnterConfigureMode };
                string result = ExecuteShellCommand(CommandList, "EndOfConft");

                string First2DigitsOfTunnel = "";

                int CurrentProgressValue = UpdateProgressMaxValueDelegateObj.Invoke(TunnelsQuantity);

                while (i < TunnelsQuantity)
                {
                    UpdateProgressValueDelegateObj.Invoke(string.Concat("Syncing tunnel", TunnelInfoStruct[i].TunnelNumber), 2);
                    First2DigitsOfTunnel = TunnelInfoStruct[i].TunnelNumber.Substring(0, 2);

                    if (First2DigitsOfTunnel == "20" || First2DigitsOfTunnel == "21" || First2DigitsOfTunnel == "35" || First2DigitsOfTunnel == "36" || First2DigitsOfTunnel == "40")
                    {
                        if (TunnelInfoStruct[i].TunnelDescription == null || TunnelInfoStruct[i].TunnelBandwidth == null || TunnelInfoStruct[i].TunnelDelay == null)
                        {
                            result = RemoveUnnecessaryTunnels(TunnelInfoStruct[i].TunnelNumber);
                            WriteLog(string.Concat("Remove Tunnel", TunnelInfoStruct[i].TunnelNumber, " ->OK"));
                        }
                        else
                        { 
                            CommandList = new string[] { string.Concat(RouterCommands.EnterTunnelMode, TunnelInfoStruct[i].TunnelNumber), TunnelInfoStruct[i].TunnelDescription, TunnelInfoStruct[i].TunnelBandwidth, TunnelInfoStruct[i].TunnelDelay, string.Concat(RouterCommands.NoCommand, GetServicePolicyName(TunnelInfoStruct[i].TunnelNumber)), "exit" };
                            result = ExecuteShellCommand(CommandList, "EndOfSyncTunnel");
                            WriteLog(string.Concat("Sync Tunnel", TunnelInfoStruct[i].TunnelNumber, " ->OK"));
                        }
                    }
                    else
                    {
                        if (!TunnelInfoStruct[i].IsTunnelUp)
                        {
                            result = RemoveUnnecessaryTunnels(TunnelInfoStruct[i].TunnelNumber);
                            //                          CommandList = new string[] { string.Concat(RouterCommands.EnterTunnelMode, TunnelInfoStruct[i].TunnelNumber) };
                            //                          result = ExecuteShellCommand(CommandList);
                            WriteLog(string.Concat("Remove Tunnel", TunnelInfoStruct[i].TunnelNumber, " ->OK"));
                        }
                    }
                    UpdateProgressValueDelegateObj.Invoke(CurrentProgressValue + i + 1, 1);
                    i++;
                }
                CommandList = new string[] { RouterCommands.WriteCommand };
                result = ExecuteShellCommand(CommandList, "EndOfWrite");

            }

            string GetServicePolicyName(string TunnelNumber)
            {
                string[] CommandList = new string[] { string.Concat(RouterCommands.GetBranchTunnelInfo, TunnelNumber) };
                string result = ExecuteShellCommand(CommandList, "EndOfGetBranchTunnelInfo");
                string ServicePolicyText = "service-policy output ";
                int TunnelPosition = result.IndexOf(string.Concat("interface Tunnel", TunnelNumber));
                int ServicePolicyPosition = result.IndexOf(ServicePolicyText, TunnelPosition);
                int EndServicePolicyPosition = result.IndexOf("\r\n", ServicePolicyPosition + ServicePolicyText.Length);
                string resultCmd = "";
                if (ServicePolicyPosition >= 0 && EndServicePolicyPosition > ServicePolicyPosition)
                    resultCmd = result.Substring(ServicePolicyPosition, EndServicePolicyPosition - ServicePolicyPosition);
                return resultCmd;
            }
            string RemoveUnnecessaryTunnels(string TunnelNumber)
            {
                string[] CommandList = new string[] { string.Concat(RouterCommands.RemoveTunnel, TunnelNumber) };
                return ExecuteShellCommand(CommandList, "EndOfRemoveTunnel");
            }

            public void RemoveUnnecessaryCryptoKeys()
            {

                int CurrentProgressValue = UpdateProgressMaxValueDelegateObj.Invoke(1);
                UpdateProgressValueDelegateObj.Invoke(string.Concat("Gathering Crypto Data..."), 2);
                string[] CommandList = new string[] { RouterCommands.GetCryptoIsakmpKey };
                string CryptoIsakmpData = ExecuteShellCommand(CommandList, "EndOfGetCryptoIsakmpKey");
                CommandList = new string[] { RouterCommands.GetCryptoIsakmpSA };
                string CryptoIsakmpSA = ExecuteShellCommand(CommandList, "EndOfGetCryptoIsakmpSA");
                CommandList = new string[] { RouterCommands.GetRoutes };
                string StaticRoutes = ExecuteShellCommand(CommandList, "EndOfGetRoutes");
                //               string result = "";
                //                CommandList = new string[] { RouterCommands.EnterConfigureMode };
                //               result = ExecuteShellCommand(CommandList, "EndOfConft");

                string CryptoIsakmpLine = "";
                int FirstIndex = 0;
                int LastIndex = 0;
                int FirstIndexInLine = 0;
                //               int LastIndexInLine = 0;
                string CryptoIsakmpAddress = "";

                while (FirstIndex >= 0)
                {
  //                  if (CryptoIsakmpData == null)
    //   /                 MessageBox.Show("CryptoIsakmpData is NULL");
                    FirstIndex = CryptoIsakmpData.IndexOf("crypto isakmp key", FirstIndex);
                    if (FirstIndex != -1)
                    {
                        LastIndex = CryptoIsakmpData.IndexOf("\r\n", FirstIndex);
                        if (FirstIndex >= 0 && LastIndex > FirstIndex)
                            CryptoIsakmpLine = CryptoIsakmpData.Substring(FirstIndex, LastIndex - FirstIndex);

                        FirstIndexInLine = CryptoIsakmpLine.LastIndexOf("address") + 8;
                        if (FirstIndexInLine >= 0)
                            CryptoIsakmpAddress = CryptoIsakmpLine.Substring(FirstIndexInLine);
                        if ((CryptoIsakmpAddress.IndexOf(" ")) != -1)
                            CryptoIsakmpAddress = CryptoIsakmpAddress.Substring(0, CryptoIsakmpAddress.IndexOf(" ")); //Crop Whitespases at the end of the line

                        if (!CheckCryptoPeers(CryptoIsakmpAddress))
                        {
                            if (CryptoIsakmpSA.IndexOf(CryptoIsakmpAddress) != -1)
                            {
                                if (!CheckCryptoIsakmpSA(CryptoIsakmpAddress, CryptoIsakmpSA))
                                {
                                    RemoveCryptoKey(CryptoIsakmpLine);
                                    RemoveUnnecessaryRoutes(CryptoIsakmpAddress, StaticRoutes);
                                }
                            }
                            else
                            {
                                RemoveCryptoKey(CryptoIsakmpLine);
                                RemoveUnnecessaryRoutes(CryptoIsakmpAddress, StaticRoutes);
                            }
                        }
                        FirstIndex = LastIndex;
                    }
                }

                UpdateProgressValueDelegateObj.Invoke(CurrentProgressValue + 1, 1);
                CommandList = new string[] { RouterCommands.WriteCommand };
                string result = ExecuteShellCommand(CommandList, "EndOfWrite");


            }

            public bool CheckCryptoIsakmpSA(string CryptoIsakmpAddress, string CryptoIsakmpSA)
            {

                //               int FirstIndex = CryptoIsakmpSA.IndexOf(CryptoIsakmpAddress);
                 //              int LastIndex = CryptoIsakmpSA.IndexOf("\r\n", CryptoIsakmpSA.IndexOf(CryptoIsakmpAddress));

                if (CryptoIsakmpSA.IndexOf("QM_IDLE", CryptoIsakmpSA.IndexOf(CryptoIsakmpAddress), CryptoIsakmpSA.IndexOf("\r\n", CryptoIsakmpSA.IndexOf(CryptoIsakmpAddress)) - CryptoIsakmpSA.IndexOf(CryptoIsakmpAddress)) != -1)
                    return true;

                return false;
            }

            public string RemoveCryptoKey(string CryptoIsakmpLine)
            {
                UpdateProgressValueDelegateObj.Invoke(string.Concat("Removing key: ", CryptoIsakmpLine), 2);
                string[] CommandList = new string[] { string.Concat(RouterCommands.NoCommand, CryptoIsakmpLine) };
                string result = ExecuteShellCommand(CommandList, "EndOfNoCryptoIsakmp");
                WriteLog(string.Concat("RemoveCryptoKey: no ", CryptoIsakmpLine, " ->OK"));
                return result;
            }

            public bool CheckCryptoPeers(string CryptoIsakmpAddress)
            {
                int i = 0;
                while (i < CryptoPeers.Length)
                {
                    if (CryptoPeers[i] == CryptoIsakmpAddress)
                        return true;
                    i++;
                }
                return false;
            }

            public string RemoveUnnecessaryRoutes(string CryptoIsakmpAddress, string StaticRoutes)
            {
                int FirstIndex = StaticRoutes.IndexOf(CryptoIsakmpAddress);
                if (FirstIndex != -1)
                {
                    UpdateProgressValueDelegateObj.Invoke(string.Concat("Removing route to ", CryptoIsakmpAddress), 2);
                    string RouteToRemove = "";
                    if (StaticRoutes.LastIndexOf("\r\n", FirstIndex) > 0 && (StaticRoutes.IndexOf("\r\n", FirstIndex) - (StaticRoutes.LastIndexOf("\r\n", FirstIndex) + 2) > 0))
                        RouteToRemove = StaticRoutes.Substring(StaticRoutes.LastIndexOf("\r\n", FirstIndex) + 2, StaticRoutes.IndexOf("\r\n", FirstIndex) - (StaticRoutes.LastIndexOf("\r\n", FirstIndex) + 2));
                    string[] CommandList = new string[] { string.Concat(RouterCommands.NoCommand, RouteToRemove) };
                    string result = ExecuteShellCommand(CommandList, "EndOfGetCryptoIsakmpKey");
                    WriteLog(string.Concat("Remove Route to ", CryptoIsakmpAddress, " ->OK"));
                    return result;

                }

                return "NoRouteToRemove";

            }

            public void OpenShellStreamSession()
            {
                if (!client.IsConnected)
                    client.Connect();
                shell = client.CreateShellStream("Tail", 0, 0, 0, 0, 1024);
                WriteStream = new StreamWriter(shell);
                ReadStream = new StreamReader(shell);
                WriteStream.AutoFlush = true;
                WriteLog("OpenShellStreamSession -> OK");
            }


            public string ExecuteShellCommand(string[] CommandList, string EndOfCommand)
            {
                int i = 0;
                while (i < CommandList.Length)
                {
                    WriteStream.WriteLine(CommandList[i]);
                    i++;
                }
                WriteStream.WriteLine(EndOfCommand);
                return shell.Expect(EndOfCommand, new TimeSpan(0, 0, 10));

            }

        }

        private void ShellTestButton_Click(object sender, EventArgs e)
        {
            /*         string ip = "10.99.0.30";
                     #region creds
                     string username = "";
                     string password = "";
                     #endregion
                     SshClient cl = new SshClient(ip, username, password);

                     cl.Connect();

                     ShellStream shell = cl.CreateShellStream("Tail", 0, 0, 0, 0, 1024);

                     StreamWriter wr = new StreamWriter(shell);
                     StreamReader rd = new StreamReader(shell);

                     wr.AutoFlush = true;

         //            wr.WriteLine("conf t");
         //            wr.WriteLine("interface Tunnel 2023");
         //            wr.WriteLine("description ---DUDU---");

                     wr.WriteLine("sh run int tun 2023");
                     wr.WriteLine("exit1");

                     //           Thread.Sleep(1000);
                     //Application.DoEvents();

                     string rep = shell.Expect("exit1", new TimeSpan(0, 0, 3));
          //             string rep = rd.ReadToEnd();
                     MessageBox.Show(rep, "Output");
                     wr.WriteLine("sh run int tun 3523");
                     wr.WriteLine("exit1");
                     rep = shell.Expect("exit1", new TimeSpan(0, 0, 3));
                     MessageBox.Show(rep, "Output");
             */
        }

        private void ParseDelay_Click(object sender, EventArgs e)
        {

            StreamReader strSP = new StreamReader("C:\\Users\\y.tretyakov\\Desktop\\interface-parse\\SP.txt");
            StreamReader strSPIP = new StreamReader("C:\\Users\\y.tretyakov\\Desktop\\interface-parse\\SP-IP.txt");
            StreamReader strTUN = new StreamReader("C:\\Users\\y.tretyakov\\Desktop\\interface-parse\\TunAll.txt");

            /*            StreamWriter wrIP = new StreamWriter("C:\\Users\\y.tretyakov\\Desktop\\interface-parse\\incoIP.txt", true);
                        StreamWriter wrPR = new StreamWriter("C:\\Users\\y.tretyakov\\Desktop\\interface-parse\\incoPR.txt", true);
                        StreamWriter wrNA = new StreamWriter("C:\\Users\\y.tretyakov\\Desktop\\interface-parse\\incoNA.txt", true);
            */
            StreamWriter wrIP;
            StreamWriter wrPR;
            StreamWriter wrNA;
            StreamWriter wrNONE;
            StreamWriter wrTUN;
            StreamWriter wrResult;
            string wrTUNPath = "C:\\Users\\y.tretyakov\\Desktop\\interface-parse\\TunnelReplacement.txt";
            string wrResultPath = "C:\\Users\\y.tretyakov\\Desktop\\interface-parse\\RESULT-TunnelReplacement.txt";


            ServiceProviderInfo[] SPI = new ServiceProviderInfo[200];
            TunnelInfo[] TI = new TunnelInfo[500];

            string buffer = "";
            string bufferTUN = "";
            int i = 0;
            int k = 0;
            // load strSP
            while (strSP.Peek() >= 0)
            {
                buffer = strSP.ReadLine();
                SPI[i].IP = strSPIP.ReadLine();

                int FirstDot = buffer.IndexOf(".");
                int SecondDot = buffer.IndexOf(".", FirstDot + 1);
                int ThirdDot = buffer.IndexOf(".", SecondDot + 1);

                SPI[i].BranchName = buffer.Substring(FirstDot + 1, SecondDot - FirstDot - 1);
                SPI[i].ServiceProviderName = buffer.Substring(SecondDot + 1, ThirdDot - SecondDot - 1);
                SPI[i].Role = buffer.Substring(ThirdDot + 1);

                i++;
            }
            strSP.Close();
            strSPIP.Close();
            i = 0;
            while (strTUN.Peek() >= 0)
            {
                bufferTUN = strTUN.ReadLine();
                
                if (bufferTUN.Contains("interface Tunnel"))
                {
                    string ConstLine = "interface Tunnel";
                    TI[i].Number = bufferTUN.Substring(ConstLine.Length);
                    using (wrTUN = new StreamWriter(wrTUNPath, true))
                    {
                        wrTUN.WriteLine(bufferTUN);
                    }
                }
                else if (bufferTUN.Contains("description"))
                {
                    if (bufferTUN.Contains("El"))
                    {
                        using (wrTUN = new StreamWriter(wrTUNPath, true))
                        {
                            wrTUN.WriteLine(bufferTUN);
                        }
                    }

                    int FirstWS = bufferTUN.IndexOf(" ", 2);
                    int SecondWS = bufferTUN.IndexOf(" ", FirstWS + 1);
                    int ThirdWS = bufferTUN.IndexOf(" ", SecondWS + 1);
                    if ( ThirdWS > 0)
                    {
                        TI[i].BranchName = bufferTUN.Substring(SecondWS + 1, ThirdWS - SecondWS - 1);
                        TI[i].ServiceProviderName = bufferTUN.Substring(ThirdWS + 1);
                    }
                    else
                    {
                        TI[i].BranchName = bufferTUN.Substring(SecondWS + 1);
                    }
                }
                else if (bufferTUN.Contains("delay"))
                {
//                    string ConstLine = "delay";
//                    TI[i].Delay = bufferTUN.Substring(1 + ConstLine.Length + 1);
                }
                else if (bufferTUN.Contains("tunnel destination"))
                {
                    string ConstLine = "tunnel destination";
                    TI[i].Destination = bufferTUN.Substring(1 + ConstLine.Length + 1);
                    i++;
                }

            }
            strTUN.Close();

            i = 0;
            int CountSuccessDelay = 0;
            int CountIncomIp = 0;
            int CountIncomPr = 0;
            int CountIncomName = 0;



            while (TI[i].Number != null)
            {
                k = 0;
                bool flag = false;
                while (SPI[k].ServiceProviderName != null)
                {

                    if (TI[i].BranchName == SPI[k].BranchName && TI[i].ServiceProviderName == SPI[k].ServiceProviderName && TI[i].Destination == SPI[k].IP )
                    {
                        flag = true;
                        CountSuccessDelay++;
                        string TunnelPrefix = TI[i].Number.Substring(0, 2);

                        if (TunnelPrefix == "35" || TunnelPrefix == "36" || TunnelPrefix == "40")
                        {
                            switch (SPI[k].Role)
                            {
                                case "M":
                                    TI[i].Delay = "1";
                                    break;
                                case "B":
                                    TI[i].Delay = "50000";
                                    break;
                                case "B2":
                                    TI[i].Delay = "100000";
                                    break;
                                default:
                                    MessageBox.Show("Error while parsing delays 35, 36, 40");
                                    break;

                            }
                        }
                        else
                        {
                            switch (SPI[k].Role)
                            {
                                case "M":
                                    TI[i].Delay = "20000";
                                    break;
                                case "B":
                                    TI[i].Delay = "70000";
                                    break;
                                case "B2":
                                    TI[i].Delay = "120000";
                                    break;
                                default:
                                    MessageBox.Show("Error while parsing delays 20, 21");
                                    break;

                            }
                        }


                    }
                    else
                    {
                        if (TI[i].BranchName == SPI[k].BranchName && TI[i].ServiceProviderName == SPI[k].ServiceProviderName && TI[i].Destination != SPI[k].IP)
                        {
                            flag = true;
                            using (wrIP = new StreamWriter("C:\\Users\\y.tretyakov\\Desktop\\interface-parse\\incoIP.txt", true))
                            {
                                wrIP.WriteLine(string.Concat("Tunnel", TI[i].Number, "\r\n ", TI[i].BranchName, " ", TI[i].ServiceProviderName, " -> ", SPI[k].BranchName, ".", SPI[k].ServiceProviderName, ".", SPI[k].Role, "\r\n ", TI[i].Destination, " -> ", SPI[k].IP));
                            }

                            string TunnelPrefix = TI[i].Number.Substring(0, 2);
                            if (TunnelPrefix == "35" || TunnelPrefix == "36" || TunnelPrefix == "40")
                            {
                                switch (SPI[k].Role)
                                {
                                    case "M":
                                        TI[i].Delay = "1";
                                        break;
                                    case "B":
                                        TI[i].Delay = "50000";
                                        break;
                                    case "B2":
                                        TI[i].Delay = "100000";
                                        break;
                                    default:
                                        MessageBox.Show("Error while parsing delays 35, 36, 40");
                                        break;

                                }
                            }
                            else
                            {
                                switch (SPI[k].Role)
                                {
                                    case "M":
                                        TI[i].Delay = "20000";
                                        break;
                                    case "B":
                                        TI[i].Delay = "70000";
                                        break;
                                    case "B2":
                                        TI[i].Delay = "120000";
                                        break;
                                    default:
                                        MessageBox.Show("Error while parsing delays 20, 21");
                                        break;

                                }
                            }


                        }
                            
                        if (TI[i].BranchName == SPI[k].BranchName && TI[i].ServiceProviderName != SPI[k].ServiceProviderName && TI[i].Destination == SPI[k].IP)
                        {
                            flag = true;
                            using (wrPR = new StreamWriter("C:\\Users\\y.tretyakov\\Desktop\\interface-parse\\incoPR.txt", true))
                            {
                                wrPR.WriteLine(string.Concat("Tunnel", TI[i].Number, "\r\n ", TI[i].BranchName, " ", TI[i].ServiceProviderName, " -> ", SPI[k].BranchName, ".", SPI[k].ServiceProviderName, ".", SPI[k].Role, "\r\n ", TI[i].Destination, " -> ", SPI[k].IP));
                            }
                        }
                            
                        if (TI[i].BranchName != SPI[k].BranchName && TI[i].ServiceProviderName == SPI[k].ServiceProviderName && TI[i].Destination == SPI[k].IP)
                        {
                            flag = true;
                            using (wrNA = new StreamWriter("C:\\Users\\y.tretyakov\\Desktop\\interface-parse\\incoNA.txt", true))
                            {
                                wrNA.WriteLine(string.Concat("Tunnel", TI[i].Number, "\r\n ", TI[i].BranchName, " ", TI[i].ServiceProviderName, " -> ", SPI[k].BranchName, ".", SPI[k].ServiceProviderName, ".", SPI[k].Role, "\r\n ", TI[i].Destination, " -> ", SPI[k].IP));
                            }
                        }
                            

                    }

                    k++;
                }

                if (!flag)
                {
                    
                    using (wrNONE = new StreamWriter("C:\\Users\\y.tretyakov\\Desktop\\interface-parse\\incoNONE.txt", true))
                    {
                        wrNONE.WriteLine(string.Concat("Tunnel", TI[i].Number, "\r\n ", TI[i].BranchName, " ", TI[i].ServiceProviderName, " -> ???"));
                    }

                }
                /*
                if (TI[i].Number == "2052" || TI[i].Number == "3552")
                {
                    switch (TI[i].Number)
                    {
                        case "2052":
                            TI[i].Delay = "70000";
                            break;
                        case "3552":
                            TI[i].Delay = "50000";
                            break;
                        default:
                            MessageBox.Show("Error while set delays 2052 - 3552");
                            break;

                    }
                }*/

                i++;
            }
            i = 0;
            using (wrResult = new StreamWriter(wrResultPath, true))
            {
                
                while (TI[i].Number != null)
                {
                    wrResult.WriteLine(string.Concat("interface Tunnel", TI[i].Number));
                    wrResult.WriteLine(string.Concat("delay ", TI[i].Delay));
                    i++;
                }

            }

            MessageBox.Show("THE END");

        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            //            txtCrypt.Text =  Encrypt(txtPlain.Text, 17);
            txtCrypt.Text = txtPlain.Text;
            //            txtCrypt.Text = CryptoService.EncryptString(txtPlain.Text, "secret");

            txtPlain.Text = CryptoService.EncryptKey(txtCrypt.Text);


        }


        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            //txtCrypt.Text = CryptoService.DecryptString(txtPlain.Text, "secret");
            txtPlain.Text = CryptoService.DecryptKey(txtPlain.Text);
        }
    }
}
