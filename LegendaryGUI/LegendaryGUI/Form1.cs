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
using IWshRuntimeLibrary;
using LegendaryGUI.GameList;

namespace LegendaryGUI
{
    public partial class Form1 : Form
    {
        GamesLister allGamesLister;
        GamesLister installedGamesLister;
        NotInstalledGamesLister notInstalledGamesLister;

        public Form1()
        {
            InitializeComponent();
            FormConsole.Console = rtb_console;
            HideAllPanels();
            Config.Read();

            LaunchProcess proc = new LaunchProcess("-V");
            proc.WaitOnExit = true;
            proc.Run();
            if (proc.Output.Count > 0)
                if (proc.Output[0].StartsWith("modded legendary"))
                    mi_forceLaunch.Visible = true;

            allGamesLister = new AllGamesLister(new ListViewSafeWriter(lv_allGames));
            installedGamesLister = new InstalledGamesLister(new ListViewSafeWriter(installed_lv));
            notInstalledGamesLister = new NotInstalledGamesLister(allGamesLister, installedGamesLister, new ListViewSafeWriter(lv_notInstalled));

            tb_home_installLoc.Text = Config.config.InstallFolder;
        }

        private void HideAllPanels()
        {
            // Hide panels
            pnl_installed.Hide();
            pnl_home.Hide();
            pnl_forceLaunch.Hide();
            pnl_allgames.Hide();
            pnl_notInstalled.Hide();

            // Deselect all menu objects
            mi_home.BackColor = Color.FromArgb(64,64,64);
            mi_Installed.BackColor = Color.FromArgb(64, 64, 64);
            mi_forceLaunch.BackColor = Color.FromArgb(64, 64, 64);
            mi_notInstalled.BackColor = Color.FromArgb(64, 64, 64);
            mi_allgames.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void mi_home_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            pnl_home.Show();
            mi_home.BackColor = Color.FromKnownColor(KnownColor.Highlight);
        }

        private void rtb_console_TextChanged(object sender, EventArgs e)
        {
            rtb_console.SelectionStart = rtb_console.Text.Length;
            rtb_console.ScrollToCaret();
        }

        private void mi_Installed_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            pnl_installed.Show();
            mi_Installed.BackColor = Color.FromKnownColor(KnownColor.Highlight);
            btn_installed_launch.Enabled = false;

            installedGamesLister.RefreshListing(false);
        }

        private void installed_lv_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_installed_launch.Enabled = (installed_lv.SelectedItems.Count >= 1);
            btn_addToDesktop.Enabled = (installed_lv.SelectedItems.Count >= 1);
        }

        private void btn_installed_launch_Click(object sender, EventArgs e)
        {
            string toLaunch = installed_lv.SelectedItems[0].SubItems[1].Text;
            LaunchProcess proc = new LaunchProcess($"launch {toLaunch}");
            proc.Run();
        }

        private void showVersion(LaunchProcess proc)
        {
            MessageBox.Show(proc.Output[0], "Version info");
        }

        private void btn_home_verInfo_Click(object sender, EventArgs e)
        {
            LaunchProcess proc = new LaunchProcess($"-V");
            proc.ReturnFunc = showVersion;
            proc.Run(); 
        }

        private void RefreshForceLaunch()
        {
            lv_forceLaunch.Items.Clear();
            foreach (var item in Config.config.ForceLaunchGames)
            {
                ListViewItem li = new ListViewItem(item.Name);
                li.SubItems.Add(item.AppName);
                li.SubItems.Add(item.Version);
                li.SubItems.Add(item.Size);
                li.SubItems.Add(item.GamePath);
                lv_forceLaunch.Items.Add(li);
            }
        }

        private void mi_forceLaunch_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            pnl_forceLaunch.Show();
            mi_forceLaunch.BackColor = Color.FromKnownColor(KnownColor.Highlight);
            btn_forceLaunch_launch.Enabled = false;
            btn_forceLaunch_remove.Enabled = false;

            RefreshForceLaunch();
        }

        private void mi_notInstalled_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            pnl_notInstalled.Show();
            mi_notInstalled.BackColor = Color.FromKnownColor(KnownColor.Highlight);

            btn_notInstalledInstall.Enabled = false;
            notInstalledGamesLister.RefreshListing();
        }

        private void btn_forceLaunch_Add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_name.Text) || string.IsNullOrEmpty(tb_appname.Text) || string.IsNullOrEmpty(tb_version.Text) || string.IsNullOrEmpty(tb_size.Text) || string.IsNullOrEmpty(tb_path.Text))
            {
                lbl_forceLaunch_status.Text = "Status: Not all fields are filled!";
                return;
            }

            if (!System.IO.File.Exists(tb_path.Text))
            {
                lbl_forceLaunch_status.Text = "Status: Path does not exist!";
                return;
            }

            foreach (var item in Config.config.ForceLaunchGames)
            {
                if (item.Name == tb_name.Text)
                {
                    lbl_forceLaunch_status.Text = "Status: App already registered!";
                    return;
                }
            }

            ForceGameInfo info = new ForceGameInfo(tb_name.Text, tb_appname.Text, tb_version.Text, tb_size.Text, tb_path.Text);
            Config.config.ForceLaunchGames.Add(info);
            Config.Write();
            RefreshForceLaunch();
            lbl_forceLaunch_status.Text = "Status: Added";
        }

        private void mi_allgames_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            pnl_allgames.Show();
            mi_allgames.BackColor = Color.FromKnownColor(KnownColor.Highlight);

            allGamesLister.RefreshListing(false);
        }

        private void btn_forceLaunch_remove_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Config.config.ForceLaunchGames.Count; i++)
            {
                if (Config.config.ForceLaunchGames[i].Name == lv_forceLaunch.SelectedItems[0].Text)
                {
                    Config.config.ForceLaunchGames.RemoveAt(i);
                }
            }

            RefreshForceLaunch();
        }

        private void lv_forceLaunch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_forceLaunch.SelectedItems.Count >= 1)
            {
                btn_forceLaunch_remove.Enabled = true;
                btn_forceLaunch_launch.Enabled = true;
            }
            else
            {
                btn_forceLaunch_remove.Enabled = false;
                btn_forceLaunch_launch.Enabled = false;
            }
        }

        private void btn_forceLaunch_launch_Click(object sender, EventArgs e)
        {
            string toLaunch = lv_forceLaunch.SelectedItems[0].SubItems[1].Text;
            string gamePath = lv_forceLaunch.SelectedItems[0].SubItems[4].Text;
            LaunchProcess proc = new LaunchProcess($"force-launch {toLaunch} \"{gamePath}\"");
            proc.Run();
        }

        private void file_chooseForceLaunchTarget_FileOk(object sender, CancelEventArgs e)
        {
            tb_path.Text = file_chooseForceLaunchTarget.FileName;
        }

        private void btn_forceLaunch_browse_Click(object sender, EventArgs e)
        {
            file_chooseForceLaunchTarget.ShowDialog();
        }

        private void btn_homeBrowse_Click(object sender, EventArgs e)
        {
            folder_chooseInstallLoc.ShowDialog();
            if (!string.IsNullOrEmpty(folder_chooseInstallLoc.SelectedPath))
            {
                tb_home_installLoc.Text = folder_chooseInstallLoc.SelectedPath;
            }
        }

        private void btn_home_save_Click(object sender, EventArgs e)
        {
            Config.config.InstallFolder = tb_home_installLoc.Text;
            Config.Write();
            FormConsole.WriteLine("<Configuration saved!>");
        }

        bool installing = false;

        private void GameInstallFinished(LaunchProcess proc)
        {
            installedGamesLister.RefreshListing(true);
            notInstalledGamesLister.RefreshListing();

            installing = false;
            MessageBox.Show("Install finished", "Game installer");
        }

        private void btn_notInstalledInstall_Click(object sender, EventArgs e)
        {
            if (installing)
            {
                MessageBox.Show("There is already a program being installed!", "Warning: Game installer");
                return;
            }

            if (string.IsNullOrEmpty(tb_home_installLoc.Text))
            {
                MessageBox.Show("No default install path set. Set one in home");
                return;
            }

            installing = true;

            string game = lv_notInstalled.SelectedItems[0].SubItems[1].Text;

            LaunchProcessMT launchProcessMT = new LaunchProcessMT($"-y install {game} --base-path \"{tb_home_installLoc.Text}\"");
            launchProcessMT.ReturnFunc = GameInstallFinished;
            launchProcessMT.Run();
        }

        private void lv_notInstalled_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_notInstalled.SelectedItems.Count >= 1)
                btn_notInstalledInstall.Enabled = true;
            else
                btn_notInstalledInstall.Enabled = false;
        }

        private void btn_addToDesktop_Click(object sender, EventArgs e)
        {
            //WshShell shell = new WshShell();
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), installed_lv.SelectedItems[0].SubItems[0].Text + ".bat");
            if (System.IO.File.Exists(path))
                return;

            //IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(path);

            //shortcut.TargetPath = $"legendary launch {installed_lv.SelectedItems[0].SubItems[1].Text}";
            //shortcut.Save();

            System.IO.File.WriteAllText(path, $"legendary launch {installed_lv.SelectedItems[0].SubItems[1].Text}");

            MessageBox.Show("Shortcut created on the desktop");
        }
    }
}
