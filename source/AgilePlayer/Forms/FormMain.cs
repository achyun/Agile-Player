// This file is part of Agile Player
// An Audio player with downsampler, upsampler and bit-converter
// written in C#.
// 
// Copyright © Alaa Ibrahim Hadid 2022
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
// 
// Author email: mailto:alaahadidfreeware@gmail.com
//
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using APlayer.Core;

namespace APlayer
{
    public partial class FormMain : Form
    {
        public FormMain(string[] args)
        {
            InitializeComponent();
            InitializeControls();

            LoadSettings();
            UpdateSwitches();
            LoadRenderers();
            FillSourceInfo();
            LoadShortcuts();

            // Force direct sound if nothing is selected
            if (APMain.CoreSettings.Audio_RendererID == "")
                APMain.CoreSettings.Audio_RendererID = "slimdx.directsound";
            APMain.SetupAudioRenderer(this.Handle);

            // Open from args
            bool args_opened = OpenDragedArgs(args);

            // Open latest list if no args 
            if (Program.AppSettings.SaveListOnExit & !args_opened)
            {
                string list_path = Path.Combine(APMain.WorkingFolder, "recent.m3u");
                if (File.Exists(list_path))
                {
                    files_browser.OpenMU3List(list_path, false);
                }
            }
        }

        private MediaBar mediaBar1;
        private DBMeterControl dbmeter;
        private UserControlFilesBrowser files_browser;
        private double old_vol = 0;
        private Color indicater_active = Color.LightSteelBlue;
        private ShortcutsManager shortcuts_manager;

        private void InitializeControls()
        {
            // Media bar
            mediaBar1 = new MediaBar();
            mediaBar1.Size = new Size(10, 23);
            mediaBar1.BackColor = Color.LightSteelBlue;
            mediaBar1.MouseClick += MediaBar1_MouseClick;
            panel_media_bar.Controls.Add(mediaBar1);

            mediaBar1.Dock = DockStyle.Top;
            mediaBar1.TimeChangeRequest += MediaBar1_TimeChangeRequest;

            // DB meter
            dbmeter = new DBMeterControl();
            dbmeter.BackColor = Color.LightSlateGray;
            panel_db_meter.Controls.Add(dbmeter);
            dbmeter.Dock = DockStyle.Fill;
            dbmeter.BringToFront();

            toolTip1.SetToolTip(dbmeter, "Left-Mouse-Click: Toggle surrounding \n\nRight-Mouse-Click: Toggle texts\n\nMiddle-Mouse-Click: Toggle lines");
            // Info mtc
            /*mtc_info = new ManagedTabControl();
           mtc_info.DrawStyle = MTCDrawStyle.Flat;
           mtc_info.BorderStyle = BorderStyle.Fixed3D;
           mtc_info.CloseBoxAlwaysVisible = false;
           mtc_info.CloseBoxOnEachPageVisible = false;
           mtc_info.AllowTabPagesReorder = false;
           mtc_info.AllowTabPageDragAndDrop = false;

           mtc_info.BackColor = SystemColors.Control;
           mtc_info.TabPageSelectedColor = Color.LightSteelBlue;
           mtc_info.TabPageColor = SystemColors.Control;
           mtc_info.TabPageSplitColor = Color.Black;
           mtc_info.ForeColor = Color.Black;

           groupBox_main.Controls.Add(mtc_info);
           mtc_info.Dock = DockStyle.Fill;

           // Add controls to the mtc 
           // 1 files browser (list)
          MTCTabPage tab = new MTCTabPage();
           tab.Text = "List";
           tab.ID = "list";
           files_browser = new UserControlFilesBrowser();
           tab.Panel = new Panel();
           tab.Panel.Controls.Add(files_browser);

           files_browser.Dock = DockStyle.Fill;

           mtc_info.TabPages.Add(tab);

           tab = new MTCTabPage();
           tab.Text = "File Details";
           tab.ID = "details";

           mtc_info.TabPages.Add(tab);

           tab = new MTCTabPage();
           tab.Text = "Visualization";
           tab.ID = "visualization";

           mtc_info.TabPages.Add(tab);*/
            files_browser = new UserControlFilesBrowser();
            groupBox_main.Controls.Add(files_browser);
            files_browser.Dock = DockStyle.Fill;
            files_browser.BringToFront();
            groupBox_main.Text = "List";
        }

        private void LoadRenderers()
        {
            //audioRendererToolStripMenuItem.DropDownItems.Clear();
            foreach (IAudioRenderer pro in APMain.AudioRenderers)
            {
                ToolStripMenuItem it = new ToolStripMenuItem();
                it.Text = pro.Name;
                it.Tag = pro.ID;
                //  audioRendererToolStripMenuItem.DropDownItems.Add(it);
            }

            FormatsManager.MediaLoaded += FormatsManager_MediaLoaded;
        }
        private void ResetAudioRenderer()
        {
            if (APCore.ON)
                APCore.PAUSED = true;

            System.Threading.Thread.Sleep(200);

            if (APMain.AudioRenderer != null)
            {
                APMain.AudioRenderer.TogglePause(true);
                APMain.AudioRenderer.Reset();
            }
            if (APCore.ON)
            {
                APCore.InitiailizePlayer();
                APCore.PAUSED = false;
            }
        }
        private void UpdateDBMeter()
        {
            if (APCore.ON && !APCore.PAUSED)
            {
                if (dbmeter.IsStereo)
                {
                    // From target
                    dbmeter.SetValues(APCore.audio_last_target_sample[0], APCore.audio_last_target_sample[1], APMain.CoreSettings.Audio_TargetBitsPerSample);
                }
                else
                {
                    // From target
                    dbmeter.SetValues(APCore.audio_last_target_sample[0], APCore.audio_last_target_sample[0], APMain.CoreSettings.Audio_TargetBitsPerSample);
                }
            }
            else
            {
                dbmeter.SetValues(0, 0, 16);
            }
        }
        private bool OpenDragedArgs(string[] args)
        {
            // Open from args
            bool args_opened = false;
            if (args != null)
            {
                if (args.Length > 0)
                {
                    List<string> args_files = new List<string>();
                    string m3u_list = "";

                    for (int i = 0; i < args.Length; i++)
                    {
                        if (args[i] != "")
                        {
                            if (FormatsManager.IsFileSupportedFormat(args[i]))
                            {
                                args_files.Add(args[i]);
                            }
                            else if (Path.GetExtension(args[i]).ToLower() == ".m3u")
                            {
                                m3u_list = args[i];
                            }
                        }
                    }

                    if (args_files.Count > 0)
                    {
                        files_browser.OpenFiles(args_files.ToArray(), true);
                        args_opened = true;
                    }
                    else if (m3u_list != "")
                    {
                        files_browser.OpenMU3List(m3u_list, true);
                        args_opened = true;
                    }
                }
            }
            return args_opened;
        }

        private void LoadSettings()
        {
            dbmeter.LoadSettings();
            this.Location = new Point(Program.AppSettings.Win_Location_X, Program.AppSettings.Win_Location_Y);
            // this.Size = new Size(Program.AppSettings.Win_Size_W, Program.AppSettings.Win_Size_H);

            mediaBar1.TimingFormat = Program.AppSettings.MediaBarTimingFormat;
            // Colors
            mediaBar1.BackColor = Color.FromArgb(Program.AppSettings.MediaBarBackgroundcolor);
            mediaBar1.MediaRecColor = Color.FromArgb(Program.AppSettings.MediaRecColor);
            mediaBar1.TickColor = Color.FromArgb(Program.AppSettings.MediaTickColor);
            mediaBar1.TickTextColor = Color.FromArgb(Program.AppSettings.MediaTickTextColor);
            mediaBar1.MediaLineColor = Color.FromArgb(Program.AppSettings.MediaMediaLineColor);
            mediaBar1.ToolTipColor = Color.FromArgb(Program.AppSettings.MediaToolTipColor);
            mediaBar1.ToolTipTextColor = Color.FromArgb(Program.AppSettings.MediaToolTipTextColor);

            trackBar_volume.Value = APMain.CoreSettings.Audio_Volume;

            toolTip1.SetToolTip(trackBar_volume, "Volume = " + trackBar_volume.Value.ToString() + " %" + "\n(F11 Volume Up, F10 Volume Down)");
        }
        private void SaveSettings()
        {
            dbmeter.SaveSettings();
            Program.AppSettings.Win_Location_X = this.Location.X;
            Program.AppSettings.Win_Location_Y = this.Location.Y;
            Program.AppSettings.Win_Size_W = this.Size.Width;
            Program.AppSettings.Win_Size_H = this.Size.Height;

            if (Program.AppSettings.SaveListOnExit)
            {
                string list_path = Path.Combine(APMain.WorkingFolder, "recent.m3u");

                files_browser.SaveMU3List(list_path);
            }
        }
        private void LoadShortcuts()
        {
            shortcuts_manager = new ShortcutsManager();

            // WE MUST FILL SHORTCUTS BEFORE INITIALIZE
            // Main
            shortcuts_manager.Shortcuts.Add(new ShortcutItem("F1", SC_Help));
            shortcuts_manager.Shortcuts.Add(new ShortcutItem("F2", SC_Website));
            shortcuts_manager.Shortcuts.Add(new ShortcutItem("F3", SC_About));
            shortcuts_manager.Shortcuts.Add(new ShortcutItem("F4", SC_Donate));
            shortcuts_manager.Shortcuts.Add(new ShortcutItem("LeftAlt+F4", SC_Exit));

            // Player Controls
            shortcuts_manager.Shortcuts.Add(new ShortcutItem("LeftControl+Space", SC_Stop));
            shortcuts_manager.Shortcuts.Add(new ShortcutItem("Space", SC_PlayPause));
            shortcuts_manager.Shortcuts.Add(new ShortcutItem("F5", SC_Prev));
            shortcuts_manager.Shortcuts.Add(new ShortcutItem("F6", SC_Next));
            shortcuts_manager.Shortcuts.Add(new ShortcutItem("F8", SC_Record));
            shortcuts_manager.Shortcuts.Add(new ShortcutItem("F9", SC_ToggleMute));
            shortcuts_manager.Shortcuts.Add(new ShortcutItem("F10", SC_VolumeDown));
            shortcuts_manager.Shortcuts.Add(new ShortcutItem("F11", SC_VolumeUp));

            // Open - Save
            shortcuts_manager.Shortcuts.Add(new ShortcutItem("LeftControl+O", SC_OpenFile));
            shortcuts_manager.Shortcuts.Add(new ShortcutItem("LeftControl+F", SC_OpenFolder));
            shortcuts_manager.Shortcuts.Add(new ShortcutItem("LeftControl+LeftShift+F", SC_OpenFolderWithSub));
            shortcuts_manager.Shortcuts.Add(new ShortcutItem("LeftControl+L", SC_OpenList));
            shortcuts_manager.Shortcuts.Add(new ShortcutItem("LeftControl+S", SC_SaveList));

            // Settings
            shortcuts_manager.Shortcuts.Add(new ShortcutItem("LeftControl+D", SC_ToggleDBFix));
            shortcuts_manager.Shortcuts.Add(new ShortcutItem("LeftControl+W", SC_ToggleWaveShift));
            shortcuts_manager.Shortcuts.Add(new ShortcutItem("LeftControl+C", SC_ToggleChannels));
            shortcuts_manager.Shortcuts.Add(new ShortcutItem("LeftControl+B", SC_ToggleBits));
            shortcuts_manager.Shortcuts.Add(new ShortcutItem("LeftControl+Q", SC_ToggleFreq));

            // Edit
            shortcuts_manager.Shortcuts.Add(new ShortcutItem("LeftControl+A", SC_SelectAll));
            shortcuts_manager.Shortcuts.Add(new ShortcutItem("Delete", SC_Delete));

            shortcuts_manager.Initialize(this.Handle);
        }

        private void CheckMute()
        {
            double val = 0;
            APCore.GetVolume(out val);
            button_toggle_mute.Image = val == 0 ? Properties.Resources.sound_mute : Properties.Resources.sound;
        }
        private void UpdateIndicaters()
        {
            label_bits_converting.BackColor = APCore.audio_bp_bits_per_sample_converting_needed ? indicater_active : SystemColors.Control;
            label_downsampling.BackColor = (APCore.audio_sampling_mode == SamplingMode.Downsampling) ? indicater_active : SystemColors.Control;
            label_Normal.BackColor = (APCore.audio_sampling_mode == SamplingMode.None) ? indicater_active : SystemColors.Control;
            label_playing.BackColor = APCore.PAUSED ? SystemColors.Control : indicater_active;
            label_Upsampling.BackColor = (APCore.audio_sampling_mode == SamplingMode.Upsampling) ? indicater_active : SystemColors.Control;

            //recordieConvertToolStripMenuItem.Enabled = button_record.Enabled = APCore.audio_bp_bits_per_sample_converting_needed || (APCore.audio_sampling_mode != SamplingMode.None);
        }
        private void UpdateSwitches()
        {
            radioButton_8bits.Checked = APMain.CoreSettings.Audio_TargetBitsPerSample == 8;
            radioButton_16bit.Checked = APMain.CoreSettings.Audio_TargetBitsPerSample == 16;
            radioButton_24bits.Checked = APMain.CoreSettings.Audio_TargetBitsPerSample == 24;
            radioButton_32bits.Checked = APMain.CoreSettings.Audio_TargetBitsPerSample == 32;

            radioButton_mono.Checked = APMain.CoreSettings.Audio_TargetAudioChannels == 1;
            radioButton_stereo.Checked = APMain.CoreSettings.Audio_TargetAudioChannels == 2;

            radioButton_freq_8000.Checked = APMain.CoreSettings.Audio_TargetFrequency == 8000;
            radioButton_freq_11025.Checked = APMain.CoreSettings.Audio_TargetFrequency == 11025;
            radioButton_freq_16000.Checked = APMain.CoreSettings.Audio_TargetFrequency == 16000;
            radioButton_freq_22050.Checked = APMain.CoreSettings.Audio_TargetFrequency == 22050;
            radioButton_32000hz.Checked = APMain.CoreSettings.Audio_TargetFrequency == 32000;
            radioButton_freq_44100.Checked = APMain.CoreSettings.Audio_TargetFrequency == 44100;
            radioButton_freq_48000.Checked = APMain.CoreSettings.Audio_TargetFrequency == 48000;
            radioButton_freq_88200.Checked = APMain.CoreSettings.Audio_TargetFrequency == 88200;
            radioButton_freq_96000.Checked = APMain.CoreSettings.Audio_TargetFrequency == 96000;

            dbmeter.IsStereo = APMain.CoreSettings.Audio_TargetAudioChannels == 2;

            radioButton_db_fix_on.Checked = APMain.CoreSettings.Audio_DB_Fix_Enabled;
            radioButton1.Checked = !APMain.CoreSettings.Audio_DB_Fix_Enabled;

            radioButton_wave_fix_off.Checked = APMain.CoreSettings.Audio_Wave_Fix_Mode == 0;
            radioButton_wave_fix_shift.Checked = APMain.CoreSettings.Audio_Wave_Fix_Mode == 1;

            button_save_list.Enabled = files_browser.CanSaveList;
        }
        private void FillSourceInfo()
        {
            label_bits.Text = "0 Bits";
            label_channels.Text = "0 Channels";
            label_freq.Text = "0 Hz";
            label_song_name.Text = "";
            if (APCore.CurrentMediaFormat != null)
            {
                if (APCore.CurrentMediaFormat.FileLoaded)
                {
                    //label_now_playing.Text = Path.GetFileName(APCore.CurrentMediaFormat.CurrentFilePath);
                    string chann = "";
                    if (APCore.CurrentMediaFormat.ChannelsNumber == 1)
                    {
                        chann = "1 (Mono)";
                    }
                    else if (APCore.CurrentMediaFormat.ChannelsNumber == 2)
                    {
                        chann = "2 (Stereo)";
                    }
                    else
                    {
                        chann = APCore.CurrentMediaFormat.ChannelsNumber.ToString() + " Channels";
                    }
                    label_channels.Text = chann;
                    label_bits.Text = APCore.CurrentMediaFormat.BitsPerSample + " Bits";
                    label_freq.Text = APCore.CurrentMediaFormat.Frequency + "Hz";
                    label_song_name.Text = Path.GetFileName(APCore.CurrentMediaFormat.CurrentFilePath);
                    dbmeter.IsStereo = APCore.CurrentMediaFormat.ChannelsNumber == 2;
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            SaveSettings();
        }
        /* private void settingsToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
         {
             // Check renderer
             foreach (ToolStripMenuItem it in audioRendererToolStripMenuItem.DropDownItems)
             {
                 it.Checked = it.Tag.ToString() == APMain.CoreSettings.Audio_RendererID;
             }
             dBFixToolStripMenuItem.Checked = APMain.CoreSettings.Audio_DB_Fix_Enabled;
         }*/
        private void audioRendererToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            APMain.CoreSettings.Audio_RendererID = e.ClickedItem.Tag.ToString();
            MessageBox.Show("The application need to be restarted in order to apply this change.");
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Open Media File";
            op.Filter = FormatsManager.GetFilter();
            op.Multiselect = true;
            if (op.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                //  FormatsManager.LoadMediaFile(op.FileName);
                files_browser.OpenFiles(op.FileNames, true);
        }
        private void FormatsManager_MediaLoaded(object sender, EventArgs e)
        {
            timer1.Start();
            mediaBar1.MediaDuration = APCore.CurrentMediaFormat.Length;
            label_time.Text = TimingFormatter.SecondsToTime(APCore.CurrentMediaFormat.Length, Program.AppSettings.TimeTextTimingFormat);
            APCore.SetVolume(trackBar_volume.Value);

            FillSourceInfo();
            double tg = 0;
            APCore.GetTargetCPS(out tg);
            timer_meter.Interval = (int)(tg);
        }
        /*TIMERS*/
        // playback timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            label_time.Text = TimingFormatter.SecondsToTime(APCore.CurrentMediaFormat.Position, Program.AppSettings.TimeTextTimingFormat) + " - " + TimingFormatter.SecondsToTime(APCore.CurrentMediaFormat.Length, Program.AppSettings.TimeTextTimingFormat);

            mediaBar1.Tick(APCore.CurrentMediaFormat.Position);
        }
        // performance update timer
        private void timer_per_ind_Tick(object sender, EventArgs e)
        {
            UpdateIndicaters();
        }
        // Meter timer
        private void timer_meter_Tick(object sender, EventArgs e)
        {
            UpdateDBMeter();

            if (shortcuts_manager != null)
                shortcuts_manager.CheckShortcuts();
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            APCore.Stop();
        }
        private void button_play_pause_Click(object sender, EventArgs e)
        {
            APCore.Play();
        }
        private void button_pause_Click(object sender, EventArgs e)
        {
            APCore.Pause();
        }
        private void trackBar_volume_Scroll(object sender, EventArgs e)
        {
            APCore.SetVolume(trackBar_volume.Value);

            toolTip1.SetToolTip(trackBar_volume, "Volume = " + trackBar_volume.Value.ToString() + " %" + "\n(F11 Volume Up, F10 Volume Down)");
            CheckMute();
        }
        private void MediaBar1_TimeChangeRequest(object sender, TimeChangeArgs e)
        {
            if (APCore.CurrentMediaFormat != null)
                APCore.CurrentMediaFormat.Position = e.NewTime;
        }
        // Play next
        private void button_next_Click(object sender, EventArgs e)
        {
            files_browser.PlayNext();
        }
        // Play previous
        private void button_prev_Click(object sender, EventArgs e)
        {
            files_browser.PlayPrevious();
        }
        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = "Open folder";

            if (folder.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                string[] files = (Directory.GetFiles(folder.SelectedPath, "*", SearchOption.TopDirectoryOnly));
                List<string> f = new List<string>();
                foreach (string file in files)
                {
                    foreach (IMediaFormat format in APMain.MediaFormats)
                    {
                        bool found = false;
                        foreach (string ex in format.Extensions)
                        {
                            if (Path.GetExtension(file) == ex)
                            {
                                found = true;
                                f.Add(file);
                                break;
                            }
                        }
                        if (found)
                            break;
                    }
                }

                files_browser.OpenFiles(f.ToArray(), true);
            }
        }
        private void openFolderIncludeSubFoldersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = "Open folder including sub folders";

            if (folder.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                string[] files = (Directory.GetFiles(folder.SelectedPath, "*", SearchOption.AllDirectories));
                List<string> f = new List<string>();
                foreach (string file in files)
                {
                    foreach (IMediaFormat format in APMain.MediaFormats)
                    {
                        bool found = false;
                        foreach (string ex in format.Extensions)
                        {
                            if (Path.GetExtension(file) == ex)
                            {
                                found = true;
                                f.Add(file);
                                break;
                            }
                        }
                        if (found)
                            break;
                    }
                }

                files_browser.OpenFiles(f.ToArray(), true);
            }
        }
        private void openListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Open List";
            op.Filter = "M3U list (*.m3u)|*.m3u";
            op.Multiselect = false;
            if (op.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                files_browser.OpenMU3List(op.FileName, true);
        }
        private void saveListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sav = new SaveFileDialog();
            sav.Title = "Save List";
            sav.Filter = "M3U list (*.m3u)|*.m3u";
            if (sav.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                files_browser.SaveMU3List(sav.FileName);
        }
        private void saveRecentPlaylistOnExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.AppSettings.SaveListOnExit = !Program.AppSettings.SaveListOnExit;
        }
        private void button_toggle_mute_Click(object sender, EventArgs e)
        {
            double val = 0;
            APCore.GetVolume(out val);

            if (val == 0)
            {
                APCore.SetVolume(old_vol);
            }
            else
            {
                APCore.SetVolume(0);
                old_vol = val;
            }
            CheckMute();
        }

        private void radioButton_mono_Click(object sender, EventArgs e)
        {
            APMain.CoreSettings.Audio_TargetAudioChannels = 1;
            UpdateSwitches();
            ResetAudioRenderer();
        }
        private void radioButton_stereo_Click(object sender, EventArgs e)
        {
            APMain.CoreSettings.Audio_TargetAudioChannels = 2;
            UpdateSwitches();
            ResetAudioRenderer();
        }
        private void radioButton_8bits_Click(object sender, EventArgs e)
        {
            APMain.CoreSettings.Audio_TargetBitsPerSample = 8;
            UpdateSwitches();
            ResetAudioRenderer();
        }
        private void radioButton_16bit_Click(object sender, EventArgs e)
        {
            APMain.CoreSettings.Audio_TargetBitsPerSample = 16;
            UpdateSwitches();
            ResetAudioRenderer();
        }
        private void radioButton_24bits_Click(object sender, EventArgs e)
        {
            APMain.CoreSettings.Audio_TargetBitsPerSample = 24;
            UpdateSwitches();
            ResetAudioRenderer();
        }
        private void radioButton_32bits_Click(object sender, EventArgs e)
        {
            APMain.CoreSettings.Audio_TargetBitsPerSample = 32;
            UpdateSwitches();
            ResetAudioRenderer();
        }
        private void radioButton_freq_8000_Click(object sender, EventArgs e)
        {
            APMain.CoreSettings.Audio_TargetFrequency = 8000;
            UpdateSwitches();
            ResetAudioRenderer();
        }
        private void radioButton_freq_11025_Click(object sender, EventArgs e)
        {
            APMain.CoreSettings.Audio_TargetFrequency = 11025;
            UpdateSwitches();
            ResetAudioRenderer();
        }
        private void radioButton_freq_16000_Click(object sender, EventArgs e)
        {
            APMain.CoreSettings.Audio_TargetFrequency = 16000;
            UpdateSwitches();
            ResetAudioRenderer();
        }
        private void radioButton_freq_22050_Click(object sender, EventArgs e)
        {
            APMain.CoreSettings.Audio_TargetFrequency = 22050;
            UpdateSwitches();
            ResetAudioRenderer();
        }
        private void radioButton_32000hz_Click(object sender, EventArgs e)
        {
            APMain.CoreSettings.Audio_TargetFrequency = 32000;
            UpdateSwitches();
            ResetAudioRenderer();
        }
        private void radioButton_freq_44100_Click(object sender, EventArgs e)
        {
            APMain.CoreSettings.Audio_TargetFrequency = 44100;
            UpdateSwitches();
            ResetAudioRenderer();
        }
        private void radioButton_freq_48000_Click(object sender, EventArgs e)
        {
            APMain.CoreSettings.Audio_TargetFrequency = 48000;
            UpdateSwitches();
            ResetAudioRenderer();
        }
        private void radioButton_freq_88200_Click(object sender, EventArgs e)
        {
            APMain.CoreSettings.Audio_TargetFrequency = 88200;
            UpdateSwitches();
            ResetAudioRenderer();
        }
        private void radioButton_freq_96000_Click(object sender, EventArgs e)
        {
            APMain.CoreSettings.Audio_TargetFrequency = 96000;
            UpdateSwitches();
            ResetAudioRenderer();
        }
        private void button_record_Click(object sender, EventArgs e)
        {
            // Save as wave
            if (APCore.ON)
            {
                if (APCore.CurrentMediaFormat != null)
                {
                    if (APCore.CurrentMediaFormat.FileLoaded)
                    {
                        SaveFileDialog sv = new SaveFileDialog();
                        sv.Title = "Save and convert into wav file with current settings";
                        sv.Filter = "Wave file |*.wav;.WAV";
                        if (sv.ShowDialog() == DialogResult.OK)
                        {
                            APCore.RecordToWav(sv.FileName);
                            try
                            {
                                System.Diagnostics.Process.Start("explorer.exe", @"/select, " + sv.FileName);
                            }
                            catch { }
                        }
                    }
                }
            }
        }
        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/alaahadid/Agile-Player/wiki");
        }
        private void websiteOnlineRepositoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/alaahadid/Agile-Player/");
        }
        private void donateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.paypal.com/donate?hosted_button_id=KV25VFRMVKLM2");
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout ab = new FormAbout();
            ab.ShowDialog();
        }
        private void radioButton1_Click(object sender, EventArgs e)
        {
            APMain.CoreSettings.Audio_DB_Fix_Enabled = false;
            UpdateSwitches();
            ResetAudioRenderer();
        }
        private void radioButton_db_fix_on_Click(object sender, EventArgs e)
        {
            APMain.CoreSettings.Audio_DB_Fix_Enabled = true;
            UpdateSwitches();
            ResetAudioRenderer();
        }
        private void dBFixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            APMain.CoreSettings.Audio_DB_Fix_Enabled = !APMain.CoreSettings.Audio_DB_Fix_Enabled;
            UpdateSwitches();
            ResetAudioRenderer();
        }
        private void offToolStripMenuItem_Click(object sender, EventArgs e)
        {
            APMain.CoreSettings.Audio_Wave_Fix_Mode = 0;
            UpdateSwitches();
            ResetAudioRenderer();
        }
        private void shiftModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            APMain.CoreSettings.Audio_Wave_Fix_Mode = 1;
            UpdateSwitches();
            ResetAudioRenderer();
        }
        private void radioButton_wave_fix_off_Click(object sender, EventArgs e)
        {
            APMain.CoreSettings.Audio_Wave_Fix_Mode = 0;
            UpdateSwitches();
            ResetAudioRenderer();
        }
        private void radioButton_wave_fix_shift_Click(object sender, EventArgs e)
        {
            APMain.CoreSettings.Audio_Wave_Fix_Mode = 1;
            UpdateSwitches();
            ResetAudioRenderer();
        }
        private void CheckDrop(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void FormMain_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] droppedfiles = (string[])e.Data.GetData(DataFormats.FileDrop);
                OpenDragedArgs(droppedfiles);
            }
        }
        private void FormMain_DragEnter(object sender, DragEventArgs e)
        {
            CheckDrop(e);
        }
        private void FormMain_DragLeave(object sender, EventArgs e)
        {
        }
        private void FormMain_DragOver(object sender, DragEventArgs e)
        {
            CheckDrop(e);
        }
        private void label_time_Click(object sender, EventArgs e)
        {
            if (Program.AppSettings.TimeTextTimingFormat == "hh:mm:ss")
            {
                Program.AppSettings.TimeTextTimingFormat = "hh:mm:ss.i";
            }
            else if (Program.AppSettings.TimeTextTimingFormat == "hh:mm:ss.i")
            {
                Program.AppSettings.TimeTextTimingFormat = "nn";
            }
            else if (Program.AppSettings.TimeTextTimingFormat == "nn")
            {
                Program.AppSettings.TimeTextTimingFormat = "hh:mm:ss";
            }
            else
            {
                Program.AppSettings.TimeTextTimingFormat = "hh:mm:ss.i";
            }
        }
        private void MediaBar1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (Program.AppSettings.MediaBarTimingFormat == "hh:mm:ss")
                {
                    Program.AppSettings.MediaBarTimingFormat = "hh:mm:ss.i";
                }
                else if (Program.AppSettings.MediaBarTimingFormat == "hh:mm:ss.i")
                {
                    Program.AppSettings.MediaBarTimingFormat = "nn";
                }
                else if (Program.AppSettings.MediaBarTimingFormat == "nn")
                {
                    Program.AppSettings.MediaBarTimingFormat = "hh:mm:ss";
                }
                else
                {
                    Program.AppSettings.MediaBarTimingFormat = "hh:mm:ss.i";
                }
                mediaBar1.TimingFormat = Program.AppSettings.MediaBarTimingFormat;
                mediaBar1.CalculateTicksBuffer();
            }
        }
        #region Shortcuts Method
        private void SC_PlayPause()
        {
            if (APCore.PAUSED)
                APCore.Play();
            else
                APCore.Pause();
        }
        private void SC_Stop()
        {
            APCore.Stop();
        }
        private void SC_Prev()
        {
            files_browser.PlayPrevious();
        }
        private void SC_Next()
        {
            files_browser.PlayNext();
        }
        private void SC_Record()
        {
            button_record_Click(this, null);
        }
        private void SC_ToggleMute()
        {
            button_toggle_mute_Click(this, null);
        }
        private void SC_VolumeUp()
        {
            if (trackBar_volume.Value + 10 <= 100)
                trackBar_volume.Value += 10;
            else
                trackBar_volume.Value = 100;
            trackBar_volume_Scroll(this, null);
        }
        private void SC_VolumeDown()
        {
            if (trackBar_volume.Value - 10 >= 0)
                trackBar_volume.Value -= 10;
            else
                trackBar_volume.Value = 0;
            trackBar_volume_Scroll(this, null);
        }
        private void SC_OpenFile()
        {
            openToolStripMenuItem_Click(this, null);
        }
        private void SC_OpenFolder()
        {
            openFolderToolStripMenuItem_Click(this, null);
        }
        private void SC_OpenFolderWithSub()
        {
            openFolderIncludeSubFoldersToolStripMenuItem_Click(this, null);
        }
        private void SC_OpenList()
        {
            openListToolStripMenuItem_Click(this, null);
        }
        private void SC_SaveList()
        {
            saveListToolStripMenuItem_Click(this, null);
        }
        private void SC_Help()
        {
            helpToolStripMenuItem1_Click(this, null);
        }
        private void SC_Website()
        {
            websiteOnlineRepositoryToolStripMenuItem_Click(this, null);
        }
        private void SC_About()
        {
            aboutToolStripMenuItem_Click(this, null);
        }
        private void SC_Donate()
        {
            donateToolStripMenuItem_Click(this, null);
        }
        private void SC_Exit()
        {
            exitToolStripMenuItem_Click(this, null);
        }
        private void SC_ToggleDBFix()
        {
            dBFixToolStripMenuItem_Click(this, null);
        }
        private void SC_ToggleWaveShift()
        {
            if (APMain.CoreSettings.Audio_Wave_Fix_Mode == 1)
                APMain.CoreSettings.Audio_Wave_Fix_Mode = 0;
            else
                APMain.CoreSettings.Audio_Wave_Fix_Mode = 1;
            UpdateSwitches();
            ResetAudioRenderer();
        }
        private void SC_ToggleChannels()
        {
            if (APMain.CoreSettings.Audio_TargetAudioChannels == 2)
                APMain.CoreSettings.Audio_TargetAudioChannels = 1;
            else
                APMain.CoreSettings.Audio_TargetAudioChannels = 2;
            UpdateSwitches();
            ResetAudioRenderer();
        }
        private void SC_ToggleBits()
        {
            if (APMain.CoreSettings.Audio_TargetBitsPerSample == 32)
                APMain.CoreSettings.Audio_TargetBitsPerSample = 8;
            else if (APMain.CoreSettings.Audio_TargetBitsPerSample == 8)
                APMain.CoreSettings.Audio_TargetBitsPerSample = 16;
            else if (APMain.CoreSettings.Audio_TargetBitsPerSample == 16)
                APMain.CoreSettings.Audio_TargetBitsPerSample = 24;
            else if (APMain.CoreSettings.Audio_TargetBitsPerSample == 24)
                APMain.CoreSettings.Audio_TargetBitsPerSample = 32;

            UpdateSwitches();
            ResetAudioRenderer();
        }
        private void SC_ToggleFreq()
        {
            if (APMain.CoreSettings.Audio_TargetFrequency == 96000)
                APMain.CoreSettings.Audio_TargetFrequency = 8000;
            else if (APMain.CoreSettings.Audio_TargetFrequency == 8000)
                APMain.CoreSettings.Audio_TargetFrequency = 11025;
            else if (APMain.CoreSettings.Audio_TargetFrequency == 11025)
                APMain.CoreSettings.Audio_TargetFrequency = 16000;
            else if (APMain.CoreSettings.Audio_TargetFrequency == 16000)
                APMain.CoreSettings.Audio_TargetFrequency = 22050;
            else if (APMain.CoreSettings.Audio_TargetFrequency == 22050)
                APMain.CoreSettings.Audio_TargetFrequency = 32000;
            else if (APMain.CoreSettings.Audio_TargetFrequency == 32000)
                APMain.CoreSettings.Audio_TargetFrequency = 44100;
            else if (APMain.CoreSettings.Audio_TargetFrequency == 44100)
                APMain.CoreSettings.Audio_TargetFrequency = 48000;
            else if (APMain.CoreSettings.Audio_TargetFrequency == 48000)
                APMain.CoreSettings.Audio_TargetFrequency = 88200;
            else if (APMain.CoreSettings.Audio_TargetFrequency == 88200)
                APMain.CoreSettings.Audio_TargetFrequency = 96000;
            UpdateSwitches();
            ResetAudioRenderer();
        }
        private void SC_SelectAll()
        {
            files_browser.SelectAll();
        }
        private void SC_Delete()
        {
            files_browser.DeleteSelected();
        }
        #endregion
    }
}
