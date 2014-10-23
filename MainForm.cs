using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing.Imaging;
using Helper.Utility_Classes;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Helper
{
    public partial class MainForm : Form
    {
        private static KeysHandler objKeysHandler;
        public static bool isAdmin=false;
        public static string priorFtp=null;
        public static string backupFtp=null;
        public MainForm()
        {
            InitializeComponent();
            objKeysHandler = new KeysHandler();
            objKeysHandler.mainForm = this;
        }
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            //_hookID = SetHook(_proc);
            rdBtnAdmin.Checked = false;
            rdBtnClient.Checked = true;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnhookWindowsHookEx(_hookID);
        }

        /*Handling the global hook*/
        #region Global KeyHook

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(
            int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                /*Sending the key to KeyHandler*/
                objKeysHandler.HandleKey((Keys)vkCode);
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        #endregion

        private void rdBtnAdmin_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnAdmin.Checked)
            {
                txtPassword.Visible = true;
                btnValid.Visible = true;
            }
        }

        private void rdBtnClient_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnClient.Checked)
            {
                label1.Text = "Prior FTP";
                btnBrowse.Visible = false;
                txtPassword.Visible = false;
                btnValid.Visible = false;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (txtFtp1.Text == "" || txtFtp2.Text == "")
            {
                MessageBox.Show("Abe Kuch Likh toh sahi textbox mein..!!!!");
            }
            else
            {
                priorFtp = txtFtp1.Text;
                backupFtp = txtFtp2.Text;
                _hookID = SetHook(_proc);
                this.Hide();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (rdBtnAdmin.Checked)
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                DialogResult result = fbd.ShowDialog();
                txtFtp1.Text = fbd.SelectedPath;
            }
        }

        private void btnValid_Click(object sender, EventArgs e)
        {
            if (rdBtnAdmin.Checked)
            {
                if (txtPassword.Text == "TitanShinigami")
                {
                    isAdmin = true;
                    btnBrowse.Visible = true;
                    label1.Text = "FTP Path";
                }
                else
                {
                    MessageBox.Show("You are not Admin. Please be happy being client only....!!  Happy Diwali!!");
                }
            }
        }
    }
}
