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
namespace APlayer
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFolderIncludeSubFoldersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.openListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.recordieConvertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.nextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previousToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toggleMuteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveRecentPlaylistOnExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.audioSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frequencyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hzToolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.hzToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.hzToolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.hzToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.hzToolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.hzToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hzToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.hzToolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.hzToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.bitsPerSampleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bitsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bitsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.bitsToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.bitsToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.channelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stereoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.playbackQualityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.highToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.audioRendererToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button_record = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button_prev = new System.Windows.Forms.Button();
            this.button_next = new System.Windows.Forms.Button();
            this.trackBar_volume = new System.Windows.Forms.TrackBar();
            this.button_toggle_mute = new System.Windows.Forms.Button();
            this.button_pause = new System.Windows.Forms.Button();
            this.button_play_pause = new System.Windows.Forms.Button();
            this.button_stop = new System.Windows.Forms.Button();
            this.label_time = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton_levels_mode_rnd = new System.Windows.Forms.RadioButton();
            this.radioButton_level_mode_src = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton_32bits = new System.Windows.Forms.RadioButton();
            this.radioButton_24bits = new System.Windows.Forms.RadioButton();
            this.radioButton_16bit = new System.Windows.Forms.RadioButton();
            this.radioButton_8bits = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton_32000hz = new System.Windows.Forms.RadioButton();
            this.radioButton_freq_96000 = new System.Windows.Forms.RadioButton();
            this.radioButton_freq_88200 = new System.Windows.Forms.RadioButton();
            this.radioButton_freq_48000 = new System.Windows.Forms.RadioButton();
            this.radioButton_freq_44100 = new System.Windows.Forms.RadioButton();
            this.radioButton_freq_22050 = new System.Windows.Forms.RadioButton();
            this.radioButton_freq_16000 = new System.Windows.Forms.RadioButton();
            this.radioButton_freq_11025 = new System.Windows.Forms.RadioButton();
            this.radioButton_freq_8000 = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButton_stereo = new System.Windows.Forms.RadioButton();
            this.radioButton_mono = new System.Windows.Forms.RadioButton();
            this.groupBox_main = new System.Windows.Forms.GroupBox();
            this.panel_media_bar = new System.Windows.Forms.Panel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label_playing = new System.Windows.Forms.Label();
            this.label_bits_converting = new System.Windows.Forms.Label();
            this.label_downsampling = new System.Windows.Forms.Label();
            this.label_Upsampling = new System.Windows.Forms.Label();
            this.label_Normal = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_song_name = new System.Windows.Forms.Label();
            this.label_channels = new System.Windows.Forms.Label();
            this.label_freq = new System.Windows.Forms.Label();
            this.label_bits = new System.Windows.Forms.Label();
            this.groupBox_meter = new System.Windows.Forms.GroupBox();
            this.groupBox_meter_container = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.textBox_rendered_rate = new System.Windows.Forms.TextBox();
            this.textBox_src_rate = new System.Windows.Forms.TextBox();
            this.textBox_clocks_max = new System.Windows.Forms.TextBox();
            this.textBox_cps = new System.Windows.Forms.TextBox();
            this.timer_per_ind = new System.Windows.Forms.Timer(this.components);
            this.timer_meter = new System.Windows.Forms.Timer(this.components);
            this.websiteOnlineRepositoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.donateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_volume)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox_main.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox_meter.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.controlsToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.audioSettingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1018, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.openFolderToolStripMenuItem,
            this.openFolderIncludeSubFoldersToolStripMenuItem,
            this.toolStripSeparator1,
            this.openListToolStripMenuItem,
            this.saveListToolStripMenuItem,
            this.toolStripSeparator5,
            this.recordieConvertToolStripMenuItem,
            this.toolStripSeparator8,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
            this.fileToolStripMenuItem.Text = "&File";
            this.fileToolStripMenuItem.DropDownOpening += new System.EventHandler(this.fileToolStripMenuItem_DropDownOpening);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = global::APlayer.Properties.Resources.folder;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(446, 34);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // openFolderToolStripMenuItem
            // 
            this.openFolderToolStripMenuItem.Image = global::APlayer.Properties.Resources.folder_explore;
            this.openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            this.openFolderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.openFolderToolStripMenuItem.Size = new System.Drawing.Size(446, 34);
            this.openFolderToolStripMenuItem.Text = "Open &Folder";
            this.openFolderToolStripMenuItem.Click += new System.EventHandler(this.openFolderToolStripMenuItem_Click);
            // 
            // openFolderIncludeSubFoldersToolStripMenuItem
            // 
            this.openFolderIncludeSubFoldersToolStripMenuItem.Name = "openFolderIncludeSubFoldersToolStripMenuItem";
            this.openFolderIncludeSubFoldersToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.openFolderIncludeSubFoldersToolStripMenuItem.Size = new System.Drawing.Size(446, 34);
            this.openFolderIncludeSubFoldersToolStripMenuItem.Text = "Open Folder (Include S&ub Folders)";
            this.openFolderIncludeSubFoldersToolStripMenuItem.Click += new System.EventHandler(this.openFolderIncludeSubFoldersToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(443, 6);
            // 
            // openListToolStripMenuItem
            // 
            this.openListToolStripMenuItem.Image = global::APlayer.Properties.Resources.folder_table;
            this.openListToolStripMenuItem.Name = "openListToolStripMenuItem";
            this.openListToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
            this.openListToolStripMenuItem.Size = new System.Drawing.Size(446, 34);
            this.openListToolStripMenuItem.Text = "Open &List";
            this.openListToolStripMenuItem.Click += new System.EventHandler(this.openListToolStripMenuItem_Click);
            // 
            // saveListToolStripMenuItem
            // 
            this.saveListToolStripMenuItem.Image = global::APlayer.Properties.Resources.table_save;
            this.saveListToolStripMenuItem.Name = "saveListToolStripMenuItem";
            this.saveListToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveListToolStripMenuItem.Size = new System.Drawing.Size(446, 34);
            this.saveListToolStripMenuItem.Text = "&Save List";
            this.saveListToolStripMenuItem.Click += new System.EventHandler(this.saveListToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(443, 6);
            // 
            // recordieConvertToolStripMenuItem
            // 
            this.recordieConvertToolStripMenuItem.Image = global::APlayer.Properties.Resources.bullet_red;
            this.recordieConvertToolStripMenuItem.Name = "recordieConvertToolStripMenuItem";
            this.recordieConvertToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.recordieConvertToolStripMenuItem.Size = new System.Drawing.Size(446, 34);
            this.recordieConvertToolStripMenuItem.Text = "Record (i.e. Convert)";
            this.recordieConvertToolStripMenuItem.Click += new System.EventHandler(this.button_record_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(443, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::APlayer.Properties.Resources.door_in;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(446, 34);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // controlsToolStripMenuItem
            // 
            this.controlsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playToolStripMenuItem,
            this.pauseToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.toolStripSeparator6,
            this.nextToolStripMenuItem,
            this.previousToolStripMenuItem,
            this.toolStripSeparator7,
            this.toggleMuteToolStripMenuItem});
            this.controlsToolStripMenuItem.Name = "controlsToolStripMenuItem";
            this.controlsToolStripMenuItem.Size = new System.Drawing.Size(95, 29);
            this.controlsToolStripMenuItem.Text = "&Controls";
            // 
            // playToolStripMenuItem
            // 
            this.playToolStripMenuItem.Image = global::APlayer.Properties.Resources.control_play;
            this.playToolStripMenuItem.Name = "playToolStripMenuItem";
            this.playToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.playToolStripMenuItem.Size = new System.Drawing.Size(244, 34);
            this.playToolStripMenuItem.Text = "&Play";
            this.playToolStripMenuItem.Click += new System.EventHandler(this.button_play_pause_Click);
            // 
            // pauseToolStripMenuItem
            // 
            this.pauseToolStripMenuItem.Image = global::APlayer.Properties.Resources.control_pause;
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.pauseToolStripMenuItem.Size = new System.Drawing.Size(244, 34);
            this.pauseToolStripMenuItem.Text = "Pau&se";
            this.pauseToolStripMenuItem.Click += new System.EventHandler(this.button_pause_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Image = global::APlayer.Properties.Resources.control_stop;
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(244, 34);
            this.stopToolStripMenuItem.Text = "&Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(241, 6);
            // 
            // nextToolStripMenuItem
            // 
            this.nextToolStripMenuItem.Image = global::APlayer.Properties.Resources.control_end;
            this.nextToolStripMenuItem.Name = "nextToolStripMenuItem";
            this.nextToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.nextToolStripMenuItem.Size = new System.Drawing.Size(244, 34);
            this.nextToolStripMenuItem.Text = "&Next";
            this.nextToolStripMenuItem.Click += new System.EventHandler(this.button_next_Click);
            // 
            // previousToolStripMenuItem
            // 
            this.previousToolStripMenuItem.Image = global::APlayer.Properties.Resources.control_start;
            this.previousToolStripMenuItem.Name = "previousToolStripMenuItem";
            this.previousToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.previousToolStripMenuItem.Size = new System.Drawing.Size(244, 34);
            this.previousToolStripMenuItem.Text = "Pre&vious";
            this.previousToolStripMenuItem.Click += new System.EventHandler(this.button_prev_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(241, 6);
            // 
            // toggleMuteToolStripMenuItem
            // 
            this.toggleMuteToolStripMenuItem.Image = global::APlayer.Properties.Resources.sound_mute;
            this.toggleMuteToolStripMenuItem.Name = "toggleMuteToolStripMenuItem";
            this.toggleMuteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.toggleMuteToolStripMenuItem.Size = new System.Drawing.Size(244, 34);
            this.toggleMuteToolStripMenuItem.Text = "Toggle &Mute";
            this.toggleMuteToolStripMenuItem.Click += new System.EventHandler(this.button_toggle_mute_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveRecentPlaylistOnExitToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(92, 29);
            this.settingsToolStripMenuItem.Text = "&Settings";
            this.settingsToolStripMenuItem.DropDownOpening += new System.EventHandler(this.settingsToolStripMenuItem_DropDownOpening_1);
            // 
            // saveRecentPlaylistOnExitToolStripMenuItem
            // 
            this.saveRecentPlaylistOnExitToolStripMenuItem.Name = "saveRecentPlaylistOnExitToolStripMenuItem";
            this.saveRecentPlaylistOnExitToolStripMenuItem.Size = new System.Drawing.Size(300, 34);
            this.saveRecentPlaylistOnExitToolStripMenuItem.Text = "&Save Recent List On Exit";
            this.saveRecentPlaylistOnExitToolStripMenuItem.Click += new System.EventHandler(this.saveRecentPlaylistOnExitToolStripMenuItem_Click);
            // 
            // audioSettingsToolStripMenuItem
            // 
            this.audioSettingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.frequencyToolStripMenuItem,
            this.bitsPerSampleToolStripMenuItem,
            this.channelsToolStripMenuItem,
            this.toolStripSeparator4,
            this.playbackQualityToolStripMenuItem,
            this.toolStripSeparator3,
            this.audioRendererToolStripMenuItem});
            this.audioSettingsToolStripMenuItem.Name = "audioSettingsToolStripMenuItem";
            this.audioSettingsToolStripMenuItem.Size = new System.Drawing.Size(145, 29);
            this.audioSettingsToolStripMenuItem.Text = "&Audio Settings";
            this.audioSettingsToolStripMenuItem.DropDownOpening += new System.EventHandler(this.settingsToolStripMenuItem_DropDownOpening);
            // 
            // frequencyToolStripMenuItem
            // 
            this.frequencyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hzToolStripMenuItem7,
            this.hzToolStripMenuItem2,
            this.hzToolStripMenuItem10,
            this.hzToolStripMenuItem1,
            this.hzToolStripMenuItem6,
            this.hzToolStripMenuItem,
            this.hzToolStripMenuItem3,
            this.hzToolStripMenuItem9,
            this.hzToolStripMenuItem4});
            this.frequencyToolStripMenuItem.Name = "frequencyToolStripMenuItem";
            this.frequencyToolStripMenuItem.Size = new System.Drawing.Size(237, 34);
            this.frequencyToolStripMenuItem.Text = "&Frequency";
            this.frequencyToolStripMenuItem.DropDownOpening += new System.EventHandler(this.frequencyToolStripMenuItem_DropDownOpening);
            this.frequencyToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.frequencyToolStripMenuItem_DropDownItemClicked);
            // 
            // hzToolStripMenuItem7
            // 
            this.hzToolStripMenuItem7.Name = "hzToolStripMenuItem7";
            this.hzToolStripMenuItem7.Size = new System.Drawing.Size(409, 34);
            this.hzToolStripMenuItem7.Tag = "8000";
            this.hzToolStripMenuItem7.Text = "8000 Hz";
            // 
            // hzToolStripMenuItem2
            // 
            this.hzToolStripMenuItem2.Name = "hzToolStripMenuItem2";
            this.hzToolStripMenuItem2.Size = new System.Drawing.Size(409, 34);
            this.hzToolStripMenuItem2.Tag = "11025";
            this.hzToolStripMenuItem2.Text = "11025 Hz";
            // 
            // hzToolStripMenuItem10
            // 
            this.hzToolStripMenuItem10.Name = "hzToolStripMenuItem10";
            this.hzToolStripMenuItem10.Size = new System.Drawing.Size(409, 34);
            this.hzToolStripMenuItem10.Tag = "16000";
            this.hzToolStripMenuItem10.Text = "16000 Hz";
            // 
            // hzToolStripMenuItem1
            // 
            this.hzToolStripMenuItem1.Name = "hzToolStripMenuItem1";
            this.hzToolStripMenuItem1.Size = new System.Drawing.Size(409, 34);
            this.hzToolStripMenuItem1.Tag = "22050";
            this.hzToolStripMenuItem1.Text = "22050 Hz";
            // 
            // hzToolStripMenuItem6
            // 
            this.hzToolStripMenuItem6.Name = "hzToolStripMenuItem6";
            this.hzToolStripMenuItem6.Size = new System.Drawing.Size(409, 34);
            this.hzToolStripMenuItem6.Tag = "32000";
            this.hzToolStripMenuItem6.Text = "32000 Hz";
            // 
            // hzToolStripMenuItem
            // 
            this.hzToolStripMenuItem.Name = "hzToolStripMenuItem";
            this.hzToolStripMenuItem.Size = new System.Drawing.Size(409, 34);
            this.hzToolStripMenuItem.Tag = "44100";
            this.hzToolStripMenuItem.Text = "44100 Hz (Audio DC Quality, Default)";
            // 
            // hzToolStripMenuItem3
            // 
            this.hzToolStripMenuItem3.Name = "hzToolStripMenuItem3";
            this.hzToolStripMenuItem3.Size = new System.Drawing.Size(409, 34);
            this.hzToolStripMenuItem3.Tag = "48000";
            this.hzToolStripMenuItem3.Text = "48000 Hz (DVD Qualtiy)";
            // 
            // hzToolStripMenuItem9
            // 
            this.hzToolStripMenuItem9.Name = "hzToolStripMenuItem9";
            this.hzToolStripMenuItem9.Size = new System.Drawing.Size(409, 34);
            this.hzToolStripMenuItem9.Tag = "88200";
            this.hzToolStripMenuItem9.Text = "88200 Hz";
            // 
            // hzToolStripMenuItem4
            // 
            this.hzToolStripMenuItem4.Name = "hzToolStripMenuItem4";
            this.hzToolStripMenuItem4.Size = new System.Drawing.Size(409, 34);
            this.hzToolStripMenuItem4.Tag = "96000";
            this.hzToolStripMenuItem4.Text = "96000 Hz";
            // 
            // bitsPerSampleToolStripMenuItem
            // 
            this.bitsPerSampleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bitsToolStripMenuItem,
            this.bitsToolStripMenuItem1,
            this.bitsToolStripMenuItem2,
            this.bitsToolStripMenuItem3});
            this.bitsPerSampleToolStripMenuItem.Name = "bitsPerSampleToolStripMenuItem";
            this.bitsPerSampleToolStripMenuItem.Size = new System.Drawing.Size(237, 34);
            this.bitsPerSampleToolStripMenuItem.Text = "&Bits Per Sample";
            this.bitsPerSampleToolStripMenuItem.DropDownOpening += new System.EventHandler(this.bitsPerSampleToolStripMenuItem_DropDownOpening);
            this.bitsPerSampleToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.bitsPerSampleToolStripMenuItem_DropDownItemClicked);
            // 
            // bitsToolStripMenuItem
            // 
            this.bitsToolStripMenuItem.Name = "bitsToolStripMenuItem";
            this.bitsToolStripMenuItem.Size = new System.Drawing.Size(320, 34);
            this.bitsToolStripMenuItem.Tag = "8";
            this.bitsToolStripMenuItem.Text = "8 Bits";
            // 
            // bitsToolStripMenuItem1
            // 
            this.bitsToolStripMenuItem1.Name = "bitsToolStripMenuItem1";
            this.bitsToolStripMenuItem1.Size = new System.Drawing.Size(320, 34);
            this.bitsToolStripMenuItem1.Tag = "16";
            this.bitsToolStripMenuItem1.Text = "16 Bits (Audio CD Quality)";
            // 
            // bitsToolStripMenuItem2
            // 
            this.bitsToolStripMenuItem2.Name = "bitsToolStripMenuItem2";
            this.bitsToolStripMenuItem2.Size = new System.Drawing.Size(320, 34);
            this.bitsToolStripMenuItem2.Tag = "24";
            this.bitsToolStripMenuItem2.Text = "24 Bits";
            // 
            // bitsToolStripMenuItem3
            // 
            this.bitsToolStripMenuItem3.Name = "bitsToolStripMenuItem3";
            this.bitsToolStripMenuItem3.Size = new System.Drawing.Size(320, 34);
            this.bitsToolStripMenuItem3.Tag = "32";
            this.bitsToolStripMenuItem3.Text = "32 Bits";
            // 
            // channelsToolStripMenuItem
            // 
            this.channelsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.monoToolStripMenuItem,
            this.stereoToolStripMenuItem});
            this.channelsToolStripMenuItem.Name = "channelsToolStripMenuItem";
            this.channelsToolStripMenuItem.Size = new System.Drawing.Size(237, 34);
            this.channelsToolStripMenuItem.Text = "&Channels";
            this.channelsToolStripMenuItem.DropDownOpening += new System.EventHandler(this.channelsToolStripMenuItem_DropDownOpening);
            this.channelsToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.channelsToolStripMenuItem_DropDownItemClicked);
            // 
            // monoToolStripMenuItem
            // 
            this.monoToolStripMenuItem.Name = "monoToolStripMenuItem";
            this.monoToolStripMenuItem.Size = new System.Drawing.Size(189, 34);
            this.monoToolStripMenuItem.Tag = "1";
            this.monoToolStripMenuItem.Text = "1 (Mono)";
            // 
            // stereoToolStripMenuItem
            // 
            this.stereoToolStripMenuItem.Name = "stereoToolStripMenuItem";
            this.stereoToolStripMenuItem.Size = new System.Drawing.Size(189, 34);
            this.stereoToolStripMenuItem.Tag = "2";
            this.stereoToolStripMenuItem.Text = "2 (Stereo)";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(234, 6);
            this.toolStripSeparator4.Visible = false;
            // 
            // playbackQualityToolStripMenuItem
            // 
            this.playbackQualityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lowToolStripMenuItem,
            this.normalToolStripMenuItem,
            this.highToolStripMenuItem});
            this.playbackQualityToolStripMenuItem.Name = "playbackQualityToolStripMenuItem";
            this.playbackQualityToolStripMenuItem.Size = new System.Drawing.Size(237, 34);
            this.playbackQualityToolStripMenuItem.Text = "Playback &Load";
            this.playbackQualityToolStripMenuItem.Visible = false;
            this.playbackQualityToolStripMenuItem.DropDownOpening += new System.EventHandler(this.playbackQualityToolStripMenuItem_DropDownOpening);
            // 
            // lowToolStripMenuItem
            // 
            this.lowToolStripMenuItem.Name = "lowToolStripMenuItem";
            this.lowToolStripMenuItem.Size = new System.Drawing.Size(374, 34);
            this.lowToolStripMenuItem.Text = "&Low (Performance)";
            this.lowToolStripMenuItem.Click += new System.EventHandler(this.lowToolStripMenuItem_Click);
            // 
            // normalToolStripMenuItem
            // 
            this.normalToolStripMenuItem.Name = "normalToolStripMenuItem";
            this.normalToolStripMenuItem.Size = new System.Drawing.Size(374, 34);
            this.normalToolStripMenuItem.Text = "&Normal (Recommended, Default)";
            this.normalToolStripMenuItem.Click += new System.EventHandler(this.normalToolStripMenuItem_Click);
            // 
            // highToolStripMenuItem
            // 
            this.highToolStripMenuItem.Name = "highToolStripMenuItem";
            this.highToolStripMenuItem.Size = new System.Drawing.Size(374, 34);
            this.highToolStripMenuItem.Text = "&High (Quality)";
            this.highToolStripMenuItem.Click += new System.EventHandler(this.highToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(234, 6);
            this.toolStripSeparator3.Visible = false;
            // 
            // audioRendererToolStripMenuItem
            // 
            this.audioRendererToolStripMenuItem.Name = "audioRendererToolStripMenuItem";
            this.audioRendererToolStripMenuItem.Size = new System.Drawing.Size(237, 34);
            this.audioRendererToolStripMenuItem.Text = "Audio &Renderer";
            this.audioRendererToolStripMenuItem.Visible = false;
            this.audioRendererToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.audioRendererToolStripMenuItem_DropDownItemClicked);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1,
            this.toolStripSeparator2,
            this.websiteOnlineRepositoryToolStripMenuItem,
            this.toolStripSeparator10,
            this.donateToolStripMenuItem,
            this.toolStripSeparator9,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(65, 29);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Image = global::APlayer.Properties.Resources.help;
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(333, 34);
            this.helpToolStripMenuItem1.Text = "&Help";
            this.helpToolStripMenuItem1.Click += new System.EventHandler(this.helpToolStripMenuItem1_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(330, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(333, 34);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // button_record
            // 
            this.button_record.Enabled = false;
            this.button_record.Image = global::APlayer.Properties.Resources.bullet_red;
            this.button_record.Location = new System.Drawing.Point(710, 25);
            this.button_record.Name = "button_record";
            this.button_record.Size = new System.Drawing.Size(56, 36);
            this.button_record.TabIndex = 10;
            this.toolTip1.SetToolTip(this.button_record, "Record\r\n (i.e. convert into target settings)");
            this.button_record.UseVisualStyleBackColor = true;
            this.button_record.Click += new System.EventHandler(this.button_record_Click);
            // 
            // button1
            // 
            this.button1.Image = global::APlayer.Properties.Resources.control_eject;
            this.button1.Location = new System.Drawing.Point(6, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 36);
            this.button1.TabIndex = 9;
            this.toolTip1.SetToolTip(this.button1, "Open File(s) (CTRL + O)");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // button_prev
            // 
            this.button_prev.Image = global::APlayer.Properties.Resources.control_start;
            this.button_prev.Location = new System.Drawing.Point(454, 25);
            this.button_prev.Name = "button_prev";
            this.button_prev.Size = new System.Drawing.Size(56, 36);
            this.button_prev.TabIndex = 8;
            this.toolTip1.SetToolTip(this.button_prev, "Previous (F2)");
            this.button_prev.UseVisualStyleBackColor = true;
            this.button_prev.Click += new System.EventHandler(this.button_prev_Click);
            // 
            // button_next
            // 
            this.button_next.Image = global::APlayer.Properties.Resources.control_end;
            this.button_next.Location = new System.Drawing.Point(516, 25);
            this.button_next.Name = "button_next";
            this.button_next.Size = new System.Drawing.Size(56, 36);
            this.button_next.TabIndex = 7;
            this.toolTip1.SetToolTip(this.button_next, "Next (F3)");
            this.button_next.UseVisualStyleBackColor = true;
            this.button_next.Click += new System.EventHandler(this.button_next_Click);
            // 
            // trackBar_volume
            // 
            this.trackBar_volume.AutoSize = false;
            this.trackBar_volume.BackColor = System.Drawing.SystemColors.Control;
            this.trackBar_volume.Location = new System.Drawing.Point(834, 25);
            this.trackBar_volume.Maximum = 100;
            this.trackBar_volume.Name = "trackBar_volume";
            this.trackBar_volume.Size = new System.Drawing.Size(154, 36);
            this.trackBar_volume.TabIndex = 5;
            this.trackBar_volume.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_volume.Value = 100;
            this.trackBar_volume.Scroll += new System.EventHandler(this.trackBar_volume_Scroll);
            // 
            // button_toggle_mute
            // 
            this.button_toggle_mute.Image = global::APlayer.Properties.Resources.sound;
            this.button_toggle_mute.Location = new System.Drawing.Point(772, 25);
            this.button_toggle_mute.Name = "button_toggle_mute";
            this.button_toggle_mute.Size = new System.Drawing.Size(56, 36);
            this.button_toggle_mute.TabIndex = 6;
            this.button_toggle_mute.UseVisualStyleBackColor = true;
            this.button_toggle_mute.Click += new System.EventHandler(this.button_toggle_mute_Click);
            // 
            // button_pause
            // 
            this.button_pause.Image = global::APlayer.Properties.Resources.control_pause;
            this.button_pause.Location = new System.Drawing.Point(369, 25);
            this.button_pause.Name = "button_pause";
            this.button_pause.Size = new System.Drawing.Size(56, 36);
            this.button_pause.TabIndex = 3;
            this.toolTip1.SetToolTip(this.button_pause, "Pause (F6)");
            this.button_pause.UseVisualStyleBackColor = true;
            this.button_pause.Click += new System.EventHandler(this.button_pause_Click);
            // 
            // button_play_pause
            // 
            this.button_play_pause.Image = global::APlayer.Properties.Resources.control_play;
            this.button_play_pause.Location = new System.Drawing.Point(228, 25);
            this.button_play_pause.Name = "button_play_pause";
            this.button_play_pause.Size = new System.Drawing.Size(135, 36);
            this.button_play_pause.TabIndex = 1;
            this.toolTip1.SetToolTip(this.button_play_pause, "Play (F5)");
            this.button_play_pause.UseVisualStyleBackColor = true;
            this.button_play_pause.Click += new System.EventHandler(this.button_play_pause_Click);
            // 
            // button_stop
            // 
            this.button_stop.Image = global::APlayer.Properties.Resources.control_stop;
            this.button_stop.Location = new System.Drawing.Point(166, 25);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(56, 36);
            this.button_stop.TabIndex = 0;
            this.toolTip1.SetToolTip(this.button_stop, "Stop (F7)");
            this.button_stop.UseVisualStyleBackColor = true;
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // label_time
            // 
            this.label_time.BackColor = System.Drawing.SystemColors.Control;
            this.label_time.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_time.Font = new System.Drawing.Font("Franklin Gothic Medium", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_time.ForeColor = System.Drawing.Color.Black;
            this.label_time.Location = new System.Drawing.Point(3, 115);
            this.label_time.Name = "label_time";
            this.label_time.Size = new System.Drawing.Size(611, 37);
            this.label_time.TabIndex = 4;
            this.label_time.Text = "00:00:00 - 00:00:00";
            this.label_time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "SPD:";
            this.toolTip1.SetToolTip(this.label2, "How many clocks the player can make in second");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "CLK:";
            this.toolTip1.SetToolTip(this.label1, "Clocks per seconds");
            // 
            // radioButton_levels_mode_rnd
            // 
            this.radioButton_levels_mode_rnd.AutoSize = true;
            this.radioButton_levels_mode_rnd.Checked = true;
            this.radioButton_levels_mode_rnd.Location = new System.Drawing.Point(6, 52);
            this.radioButton_levels_mode_rnd.Name = "radioButton_levels_mode_rnd";
            this.radioButton_levels_mode_rnd.Size = new System.Drawing.Size(78, 22);
            this.radioButton_levels_mode_rnd.TabIndex = 8;
            this.radioButton_levels_mode_rnd.TabStop = true;
            this.radioButton_levels_mode_rnd.Text = "Output";
            this.toolTip1.SetToolTip(this.radioButton_levels_mode_rnd, "Display db meter from the target audio after processig");
            this.radioButton_levels_mode_rnd.UseVisualStyleBackColor = true;
            // 
            // radioButton_level_mode_src
            // 
            this.radioButton_level_mode_src.AutoSize = true;
            this.radioButton_level_mode_src.Location = new System.Drawing.Point(6, 22);
            this.radioButton_level_mode_src.Name = "radioButton_level_mode_src";
            this.radioButton_level_mode_src.Size = new System.Drawing.Size(65, 22);
            this.radioButton_level_mode_src.TabIndex = 7;
            this.radioButton_level_mode_src.Text = "Input";
            this.toolTip1.SetToolTip(this.radioButton_level_mode_src, "Display db meter from the source song/track");
            this.radioButton_level_mode_src.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "RND:";
            this.toolTip1.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "SRC:";
            this.toolTip1.SetToolTip(this.label3, "How many KBytes that read from the source in second.\r\n\r\nThis depends and the sour" +
        "ce specifications.");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_32bits);
            this.groupBox1.Controls.Add(this.radioButton_24bits);
            this.groupBox1.Controls.Add(this.radioButton_16bit);
            this.groupBox1.Controls.Add(this.radioButton_8bits);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 473);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(160, 155);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bits Per Sample";
            // 
            // radioButton_32bits
            // 
            this.radioButton_32bits.AutoSize = true;
            this.radioButton_32bits.Location = new System.Drawing.Point(6, 115);
            this.radioButton_32bits.Name = "radioButton_32bits";
            this.radioButton_32bits.Size = new System.Drawing.Size(82, 22);
            this.radioButton_32bits.TabIndex = 3;
            this.radioButton_32bits.Text = "32 Bits";
            this.radioButton_32bits.UseVisualStyleBackColor = true;
            this.radioButton_32bits.Click += new System.EventHandler(this.radioButton_32bits_Click);
            // 
            // radioButton_24bits
            // 
            this.radioButton_24bits.AutoSize = true;
            this.radioButton_24bits.Location = new System.Drawing.Point(6, 85);
            this.radioButton_24bits.Name = "radioButton_24bits";
            this.radioButton_24bits.Size = new System.Drawing.Size(82, 22);
            this.radioButton_24bits.TabIndex = 2;
            this.radioButton_24bits.Text = "24 Bits";
            this.radioButton_24bits.UseVisualStyleBackColor = true;
            this.radioButton_24bits.Click += new System.EventHandler(this.radioButton_24bits_Click);
            // 
            // radioButton_16bit
            // 
            this.radioButton_16bit.AutoSize = true;
            this.radioButton_16bit.Checked = true;
            this.radioButton_16bit.Location = new System.Drawing.Point(6, 55);
            this.radioButton_16bit.Name = "radioButton_16bit";
            this.radioButton_16bit.Size = new System.Drawing.Size(82, 22);
            this.radioButton_16bit.TabIndex = 1;
            this.radioButton_16bit.TabStop = true;
            this.radioButton_16bit.Text = "16 Bits";
            this.radioButton_16bit.UseVisualStyleBackColor = true;
            this.radioButton_16bit.Click += new System.EventHandler(this.radioButton_16bit_Click);
            // 
            // radioButton_8bits
            // 
            this.radioButton_8bits.AutoSize = true;
            this.radioButton_8bits.Location = new System.Drawing.Point(6, 25);
            this.radioButton_8bits.Name = "radioButton_8bits";
            this.radioButton_8bits.Size = new System.Drawing.Size(73, 22);
            this.radioButton_8bits.TabIndex = 0;
            this.radioButton_8bits.Text = "8 Bits";
            this.radioButton_8bits.UseVisualStyleBackColor = true;
            this.radioButton_8bits.Click += new System.EventHandler(this.radioButton_8bits_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton_32000hz);
            this.groupBox2.Controls.Add(this.radioButton_freq_96000);
            this.groupBox2.Controls.Add(this.radioButton_freq_88200);
            this.groupBox2.Controls.Add(this.radioButton_freq_48000);
            this.groupBox2.Controls.Add(this.radioButton_freq_44100);
            this.groupBox2.Controls.Add(this.radioButton_freq_22050);
            this.groupBox2.Controls.Add(this.radioButton_freq_16000);
            this.groupBox2.Controls.Add(this.radioButton_freq_11025);
            this.groupBox2.Controls.Add(this.radioButton_freq_8000);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 162);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(160, 305);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Frequency";
            // 
            // radioButton_32000hz
            // 
            this.radioButton_32000hz.AutoSize = true;
            this.radioButton_32000hz.Location = new System.Drawing.Point(6, 141);
            this.radioButton_32000hz.Name = "radioButton_32000hz";
            this.radioButton_32000hz.Size = new System.Drawing.Size(100, 22);
            this.radioButton_32000hz.TabIndex = 8;
            this.radioButton_32000hz.Text = "32000 Hz";
            this.radioButton_32000hz.UseVisualStyleBackColor = true;
            this.radioButton_32000hz.Click += new System.EventHandler(this.radioButton_32000hz_Click);
            // 
            // radioButton_freq_96000
            // 
            this.radioButton_freq_96000.AutoSize = true;
            this.radioButton_freq_96000.Location = new System.Drawing.Point(6, 257);
            this.radioButton_freq_96000.Name = "radioButton_freq_96000";
            this.radioButton_freq_96000.Size = new System.Drawing.Size(100, 22);
            this.radioButton_freq_96000.TabIndex = 7;
            this.radioButton_freq_96000.Text = "96000 Hz";
            this.radioButton_freq_96000.UseVisualStyleBackColor = true;
            this.radioButton_freq_96000.Click += new System.EventHandler(this.radioButton_freq_96000_Click);
            // 
            // radioButton_freq_88200
            // 
            this.radioButton_freq_88200.AutoSize = true;
            this.radioButton_freq_88200.Location = new System.Drawing.Point(6, 227);
            this.radioButton_freq_88200.Name = "radioButton_freq_88200";
            this.radioButton_freq_88200.Size = new System.Drawing.Size(100, 22);
            this.radioButton_freq_88200.TabIndex = 6;
            this.radioButton_freq_88200.Text = "88200 Hz";
            this.radioButton_freq_88200.UseVisualStyleBackColor = true;
            this.radioButton_freq_88200.Click += new System.EventHandler(this.radioButton_freq_88200_Click);
            // 
            // radioButton_freq_48000
            // 
            this.radioButton_freq_48000.AutoSize = true;
            this.radioButton_freq_48000.Location = new System.Drawing.Point(6, 197);
            this.radioButton_freq_48000.Name = "radioButton_freq_48000";
            this.radioButton_freq_48000.Size = new System.Drawing.Size(100, 22);
            this.radioButton_freq_48000.TabIndex = 5;
            this.radioButton_freq_48000.Text = "48000 Hz";
            this.radioButton_freq_48000.UseVisualStyleBackColor = true;
            this.radioButton_freq_48000.Click += new System.EventHandler(this.radioButton_freq_48000_Click);
            // 
            // radioButton_freq_44100
            // 
            this.radioButton_freq_44100.AutoSize = true;
            this.radioButton_freq_44100.Checked = true;
            this.radioButton_freq_44100.Location = new System.Drawing.Point(6, 169);
            this.radioButton_freq_44100.Name = "radioButton_freq_44100";
            this.radioButton_freq_44100.Size = new System.Drawing.Size(100, 22);
            this.radioButton_freq_44100.TabIndex = 4;
            this.radioButton_freq_44100.TabStop = true;
            this.radioButton_freq_44100.Text = "44100 Hz";
            this.radioButton_freq_44100.UseVisualStyleBackColor = true;
            this.radioButton_freq_44100.Click += new System.EventHandler(this.radioButton_freq_44100_Click);
            // 
            // radioButton_freq_22050
            // 
            this.radioButton_freq_22050.AutoSize = true;
            this.radioButton_freq_22050.Location = new System.Drawing.Point(6, 113);
            this.radioButton_freq_22050.Name = "radioButton_freq_22050";
            this.radioButton_freq_22050.Size = new System.Drawing.Size(100, 22);
            this.radioButton_freq_22050.TabIndex = 3;
            this.radioButton_freq_22050.Text = "22050 Hz";
            this.radioButton_freq_22050.UseVisualStyleBackColor = true;
            this.radioButton_freq_22050.Click += new System.EventHandler(this.radioButton_freq_22050_Click);
            // 
            // radioButton_freq_16000
            // 
            this.radioButton_freq_16000.AutoSize = true;
            this.radioButton_freq_16000.Location = new System.Drawing.Point(6, 85);
            this.radioButton_freq_16000.Name = "radioButton_freq_16000";
            this.radioButton_freq_16000.Size = new System.Drawing.Size(100, 22);
            this.radioButton_freq_16000.TabIndex = 2;
            this.radioButton_freq_16000.Text = "16000 Hz";
            this.radioButton_freq_16000.UseVisualStyleBackColor = true;
            this.radioButton_freq_16000.Click += new System.EventHandler(this.radioButton_freq_16000_Click);
            // 
            // radioButton_freq_11025
            // 
            this.radioButton_freq_11025.AutoSize = true;
            this.radioButton_freq_11025.Location = new System.Drawing.Point(6, 55);
            this.radioButton_freq_11025.Name = "radioButton_freq_11025";
            this.radioButton_freq_11025.Size = new System.Drawing.Size(99, 22);
            this.radioButton_freq_11025.TabIndex = 1;
            this.radioButton_freq_11025.Text = "11025 Hz";
            this.radioButton_freq_11025.UseVisualStyleBackColor = true;
            this.radioButton_freq_11025.Click += new System.EventHandler(this.radioButton_freq_11025_Click);
            // 
            // radioButton_freq_8000
            // 
            this.radioButton_freq_8000.AutoSize = true;
            this.radioButton_freq_8000.Location = new System.Drawing.Point(6, 25);
            this.radioButton_freq_8000.Name = "radioButton_freq_8000";
            this.radioButton_freq_8000.Size = new System.Drawing.Size(91, 22);
            this.radioButton_freq_8000.TabIndex = 0;
            this.radioButton_freq_8000.Text = "8000 Hz";
            this.radioButton_freq_8000.UseVisualStyleBackColor = true;
            this.radioButton_freq_8000.Click += new System.EventHandler(this.radioButton_freq_8000_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButton_levels_mode_rnd);
            this.groupBox3.Controls.Add(this.radioButton_level_mode_src);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(3, 22);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(154, 89);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButton_stereo);
            this.groupBox4.Controls.Add(this.radioButton_mono);
            this.groupBox4.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(12, 61);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(160, 95);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Channels";
            // 
            // radioButton_stereo
            // 
            this.radioButton_stereo.AutoSize = true;
            this.radioButton_stereo.Checked = true;
            this.radioButton_stereo.Location = new System.Drawing.Point(6, 55);
            this.radioButton_stereo.Name = "radioButton_stereo";
            this.radioButton_stereo.Size = new System.Drawing.Size(93, 22);
            this.radioButton_stereo.TabIndex = 1;
            this.radioButton_stereo.TabStop = true;
            this.radioButton_stereo.Text = "2 Stereo";
            this.radioButton_stereo.UseVisualStyleBackColor = true;
            this.radioButton_stereo.Click += new System.EventHandler(this.radioButton_stereo_Click);
            // 
            // radioButton_mono
            // 
            this.radioButton_mono.AutoSize = true;
            this.radioButton_mono.Location = new System.Drawing.Point(6, 25);
            this.radioButton_mono.Name = "radioButton_mono";
            this.radioButton_mono.Size = new System.Drawing.Size(85, 22);
            this.radioButton_mono.TabIndex = 0;
            this.radioButton_mono.Text = "1 Mono";
            this.radioButton_mono.UseVisualStyleBackColor = true;
            this.radioButton_mono.Click += new System.EventHandler(this.radioButton_mono_Click);
            // 
            // groupBox_main
            // 
            this.groupBox_main.Controls.Add(this.panel_media_bar);
            this.groupBox_main.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_main.Location = new System.Drawing.Point(178, 61);
            this.groupBox_main.Name = "groupBox_main";
            this.groupBox_main.Size = new System.Drawing.Size(662, 406);
            this.groupBox_main.TabIndex = 7;
            this.groupBox_main.TabStop = false;
            // 
            // panel_media_bar
            // 
            this.panel_media_bar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_media_bar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_media_bar.Location = new System.Drawing.Point(3, 366);
            this.panel_media_bar.Name = "panel_media_bar";
            this.panel_media_bar.Size = new System.Drawing.Size(656, 37);
            this.panel_media_bar.TabIndex = 0;
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox6.Controls.Add(this.label_time);
            this.groupBox6.Controls.Add(this.panel2);
            this.groupBox6.Controls.Add(this.panel1);
            this.groupBox6.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(178, 473);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(662, 155);
            this.groupBox6.TabIndex = 8;
            this.groupBox6.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label_playing);
            this.panel2.Controls.Add(this.label_bits_converting);
            this.panel2.Controls.Add(this.label_downsampling);
            this.panel2.Controls.Add(this.label_Upsampling);
            this.panel2.Controls.Add(this.label_Normal);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.ForeColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(3, 59);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(656, 37);
            this.panel2.TabIndex = 6;
            // 
            // label_playing
            // 
            this.label_playing.BackColor = System.Drawing.SystemColors.Control;
            this.label_playing.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_playing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_playing.ForeColor = System.Drawing.Color.Black;
            this.label_playing.Location = new System.Drawing.Point(491, 0);
            this.label_playing.Name = "label_playing";
            this.label_playing.Size = new System.Drawing.Size(161, 33);
            this.label_playing.TabIndex = 4;
            this.label_playing.Text = "Playing";
            this.label_playing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_bits_converting
            // 
            this.label_bits_converting.BackColor = System.Drawing.SystemColors.Control;
            this.label_bits_converting.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_bits_converting.Dock = System.Windows.Forms.DockStyle.Left;
            this.label_bits_converting.ForeColor = System.Drawing.Color.Black;
            this.label_bits_converting.Location = new System.Drawing.Point(344, 0);
            this.label_bits_converting.Name = "label_bits_converting";
            this.label_bits_converting.Size = new System.Drawing.Size(147, 33);
            this.label_bits_converting.TabIndex = 3;
            this.label_bits_converting.Text = "Bits-Converting";
            this.label_bits_converting.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_downsampling
            // 
            this.label_downsampling.BackColor = System.Drawing.SystemColors.Control;
            this.label_downsampling.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_downsampling.Dock = System.Windows.Forms.DockStyle.Left;
            this.label_downsampling.ForeColor = System.Drawing.Color.Black;
            this.label_downsampling.Location = new System.Drawing.Point(203, 0);
            this.label_downsampling.Name = "label_downsampling";
            this.label_downsampling.Size = new System.Drawing.Size(141, 33);
            this.label_downsampling.TabIndex = 2;
            this.label_downsampling.Text = "Downsampling";
            this.label_downsampling.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_Upsampling
            // 
            this.label_Upsampling.BackColor = System.Drawing.SystemColors.Control;
            this.label_Upsampling.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_Upsampling.Dock = System.Windows.Forms.DockStyle.Left;
            this.label_Upsampling.ForeColor = System.Drawing.Color.Black;
            this.label_Upsampling.Location = new System.Drawing.Point(84, 0);
            this.label_Upsampling.Name = "label_Upsampling";
            this.label_Upsampling.Size = new System.Drawing.Size(119, 33);
            this.label_Upsampling.TabIndex = 1;
            this.label_Upsampling.Text = "Upsampling";
            this.label_Upsampling.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_Normal
            // 
            this.label_Normal.BackColor = System.Drawing.SystemColors.Control;
            this.label_Normal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_Normal.Dock = System.Windows.Forms.DockStyle.Left;
            this.label_Normal.ForeColor = System.Drawing.Color.Black;
            this.label_Normal.Location = new System.Drawing.Point(0, 0);
            this.label_Normal.Name = "label_Normal";
            this.label_Normal.Size = new System.Drawing.Size(84, 33);
            this.label_Normal.TabIndex = 0;
            this.label_Normal.Text = "Normal";
            this.label_Normal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label_song_name);
            this.panel1.Controls.Add(this.label_channels);
            this.panel1.Controls.Add(this.label_freq);
            this.panel1.Controls.Add(this.label_bits);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(3, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(656, 37);
            this.panel1.TabIndex = 7;
            // 
            // label_song_name
            // 
            this.label_song_name.BackColor = System.Drawing.SystemColors.Control;
            this.label_song_name.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_song_name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_song_name.ForeColor = System.Drawing.Color.Black;
            this.label_song_name.Location = new System.Drawing.Point(344, 0);
            this.label_song_name.Name = "label_song_name";
            this.label_song_name.Size = new System.Drawing.Size(308, 33);
            this.label_song_name.TabIndex = 3;
            this.label_song_name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_channels
            // 
            this.label_channels.BackColor = System.Drawing.SystemColors.Control;
            this.label_channels.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_channels.Dock = System.Windows.Forms.DockStyle.Left;
            this.label_channels.ForeColor = System.Drawing.Color.Black;
            this.label_channels.Location = new System.Drawing.Point(203, 0);
            this.label_channels.Name = "label_channels";
            this.label_channels.Size = new System.Drawing.Size(141, 33);
            this.label_channels.TabIndex = 2;
            this.label_channels.Text = "0 Channels";
            this.label_channels.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_freq
            // 
            this.label_freq.BackColor = System.Drawing.SystemColors.Control;
            this.label_freq.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_freq.Dock = System.Windows.Forms.DockStyle.Left;
            this.label_freq.ForeColor = System.Drawing.Color.Black;
            this.label_freq.Location = new System.Drawing.Point(84, 0);
            this.label_freq.Name = "label_freq";
            this.label_freq.Size = new System.Drawing.Size(119, 33);
            this.label_freq.TabIndex = 1;
            this.label_freq.Text = "0 Hz";
            this.label_freq.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_bits
            // 
            this.label_bits.BackColor = System.Drawing.SystemColors.Control;
            this.label_bits.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_bits.Dock = System.Windows.Forms.DockStyle.Left;
            this.label_bits.ForeColor = System.Drawing.Color.Black;
            this.label_bits.Location = new System.Drawing.Point(0, 0);
            this.label_bits.Name = "label_bits";
            this.label_bits.Size = new System.Drawing.Size(84, 33);
            this.label_bits.TabIndex = 0;
            this.label_bits.Text = "0 Bits";
            this.label_bits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox_meter
            // 
            this.groupBox_meter.Controls.Add(this.groupBox_meter_container);
            this.groupBox_meter.Controls.Add(this.groupBox3);
            this.groupBox_meter.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_meter.Location = new System.Drawing.Point(846, 61);
            this.groupBox_meter.Name = "groupBox_meter";
            this.groupBox_meter.Size = new System.Drawing.Size(160, 406);
            this.groupBox_meter.TabIndex = 9;
            this.groupBox_meter.TabStop = false;
            this.groupBox_meter.Text = "DB Meter";
            // 
            // groupBox_meter_container
            // 
            this.groupBox_meter_container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_meter_container.Location = new System.Drawing.Point(3, 111);
            this.groupBox_meter_container.Name = "groupBox_meter_container";
            this.groupBox_meter_container.Size = new System.Drawing.Size(154, 292);
            this.groupBox_meter_container.TabIndex = 6;
            this.groupBox_meter_container.TabStop = false;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.button_record);
            this.groupBox7.Controls.Add(this.button1);
            this.groupBox7.Controls.Add(this.button_stop);
            this.groupBox7.Controls.Add(this.button_prev);
            this.groupBox7.Controls.Add(this.button_play_pause);
            this.groupBox7.Controls.Add(this.button_next);
            this.groupBox7.Controls.Add(this.button_pause);
            this.groupBox7.Controls.Add(this.trackBar_volume);
            this.groupBox7.Controls.Add(this.button_toggle_mute);
            this.groupBox7.Location = new System.Drawing.Point(12, 634);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(994, 77);
            this.groupBox7.TabIndex = 10;
            this.groupBox7.TabStop = false;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.textBox_rendered_rate);
            this.groupBox8.Controls.Add(this.label4);
            this.groupBox8.Controls.Add(this.textBox_src_rate);
            this.groupBox8.Controls.Add(this.label3);
            this.groupBox8.Controls.Add(this.textBox_clocks_max);
            this.groupBox8.Controls.Add(this.label2);
            this.groupBox8.Controls.Add(this.textBox_cps);
            this.groupBox8.Controls.Add(this.label1);
            this.groupBox8.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.Location = new System.Drawing.Point(846, 473);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(160, 155);
            this.groupBox8.TabIndex = 11;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Performance";
            // 
            // textBox_rendered_rate
            // 
            this.textBox_rendered_rate.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_rendered_rate.Location = new System.Drawing.Point(48, 118);
            this.textBox_rendered_rate.Name = "textBox_rendered_rate";
            this.textBox_rendered_rate.ReadOnly = true;
            this.textBox_rendered_rate.Size = new System.Drawing.Size(106, 26);
            this.textBox_rendered_rate.TabIndex = 7;
            // 
            // textBox_src_rate
            // 
            this.textBox_src_rate.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_src_rate.Location = new System.Drawing.Point(48, 86);
            this.textBox_src_rate.Name = "textBox_src_rate";
            this.textBox_src_rate.ReadOnly = true;
            this.textBox_src_rate.Size = new System.Drawing.Size(106, 26);
            this.textBox_src_rate.TabIndex = 5;
            // 
            // textBox_clocks_max
            // 
            this.textBox_clocks_max.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_clocks_max.Location = new System.Drawing.Point(48, 54);
            this.textBox_clocks_max.Name = "textBox_clocks_max";
            this.textBox_clocks_max.ReadOnly = true;
            this.textBox_clocks_max.Size = new System.Drawing.Size(106, 26);
            this.textBox_clocks_max.TabIndex = 3;
            // 
            // textBox_cps
            // 
            this.textBox_cps.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_cps.Location = new System.Drawing.Point(48, 22);
            this.textBox_cps.Name = "textBox_cps";
            this.textBox_cps.ReadOnly = true;
            this.textBox_cps.Size = new System.Drawing.Size(106, 26);
            this.textBox_cps.TabIndex = 1;
            // 
            // timer_per_ind
            // 
            this.timer_per_ind.Enabled = true;
            this.timer_per_ind.Interval = 700;
            this.timer_per_ind.Tick += new System.EventHandler(this.timer_per_ind_Tick);
            // 
            // timer_meter
            // 
            this.timer_meter.Enabled = true;
            this.timer_meter.Interval = 44;
            this.timer_meter.Tick += new System.EventHandler(this.timer_meter_Tick);
            // 
            // websiteOnlineRepositoryToolStripMenuItem
            // 
            this.websiteOnlineRepositoryToolStripMenuItem.Name = "websiteOnlineRepositoryToolStripMenuItem";
            this.websiteOnlineRepositoryToolStripMenuItem.Size = new System.Drawing.Size(333, 34);
            this.websiteOnlineRepositoryToolStripMenuItem.Text = "&Website (Online Repository)";
            this.websiteOnlineRepositoryToolStripMenuItem.Click += new System.EventHandler(this.websiteOnlineRepositoryToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(330, 6);
            // 
            // donateToolStripMenuItem
            // 
            this.donateToolStripMenuItem.Name = "donateToolStripMenuItem";
            this.donateToolStripMenuItem.Size = new System.Drawing.Size(333, 34);
            this.donateToolStripMenuItem.Text = "&Donate";
            this.donateToolStripMenuItem.Click += new System.EventHandler(this.donateToolStripMenuItem_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(330, 6);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 723);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox_meter);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox_main);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Agile Player";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_volume)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox_main.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox_meter.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button button_play_pause;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.Button button_pause;
        private System.Windows.Forms.Label label_time;
        private System.Windows.Forms.TrackBar trackBar_volume;
        private System.Windows.Forms.Button button_toggle_mute;
        private System.Windows.Forms.ToolStripMenuItem audioSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem audioRendererToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem frequencyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hzToolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem hzToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem hzToolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem hzToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem hzToolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem hzToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hzToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem hzToolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem hzToolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem bitsPerSampleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bitsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bitsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem bitsToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem bitsToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem channelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem monoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stereoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem playbackQualityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem normalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem highToolStripMenuItem;
        private System.Windows.Forms.Button button_next;
        private System.Windows.Forms.Button button_prev;
        private System.Windows.Forms.ToolStripMenuItem openFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFolderIncludeSubFoldersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveListToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem controlsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem nextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previousToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveRecentPlaylistOnExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem toggleMuteToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton_32bits;
        private System.Windows.Forms.RadioButton radioButton_24bits;
        private System.Windows.Forms.RadioButton radioButton_16bit;
        private System.Windows.Forms.RadioButton radioButton_8bits;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton_freq_22050;
        private System.Windows.Forms.RadioButton radioButton_freq_16000;
        private System.Windows.Forms.RadioButton radioButton_freq_11025;
        private System.Windows.Forms.RadioButton radioButton_freq_8000;
        private System.Windows.Forms.RadioButton radioButton_freq_96000;
        private System.Windows.Forms.RadioButton radioButton_freq_88200;
        private System.Windows.Forms.RadioButton radioButton_freq_48000;
        private System.Windows.Forms.RadioButton radioButton_freq_44100;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radioButton_stereo;
        private System.Windows.Forms.RadioButton radioButton_mono;
        private System.Windows.Forms.GroupBox groupBox_main;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label_playing;
        private System.Windows.Forms.Label label_bits_converting;
        private System.Windows.Forms.Label label_downsampling;
        private System.Windows.Forms.Label label_Upsampling;
        private System.Windows.Forms.Label label_Normal;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton radioButton_levels_mode_rnd;
        private System.Windows.Forms.RadioButton radioButton_level_mode_src;
        private System.Windows.Forms.GroupBox groupBox_meter;
        private System.Windows.Forms.Panel panel_media_bar;
        private System.Windows.Forms.Button button_record;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TextBox textBox_rendered_rate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_src_rate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_clocks_max;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_cps;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_song_name;
        private System.Windows.Forms.Label label_channels;
        private System.Windows.Forms.Label label_freq;
        private System.Windows.Forms.Label label_bits;
        private System.Windows.Forms.Timer timer_per_ind;
        private System.Windows.Forms.Timer timer_meter;
        private System.Windows.Forms.ToolStripMenuItem recordieConvertToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.GroupBox groupBox_meter_container;
        private System.Windows.Forms.RadioButton radioButton_32000hz;
        private System.Windows.Forms.ToolStripMenuItem websiteOnlineRepositoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem donateToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
    }
}

