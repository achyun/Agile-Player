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
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton_db_fix_on = new System.Windows.Forms.RadioButton();
            this.radioButton_wave_fix_shift = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button_save_list = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.groupBox_meter = new System.Windows.Forms.GroupBox();
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
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radioButton_wave_fix_off = new System.Windows.Forms.RadioButton();
            this.timer_per_ind = new System.Windows.Forms.Timer(this.components);
            this.timer_meter = new System.Windows.Forms.Timer(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.panel_db_meter = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_volume)).BeginInit();
            this.groupBox_meter.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox_main.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_record
            // 
            this.button_record.Image = global::APlayer.Properties.Resources.bullet_red;
            this.button_record.Location = new System.Drawing.Point(710, 22);
            this.button_record.Name = "button_record";
            this.button_record.Size = new System.Drawing.Size(56, 46);
            this.button_record.TabIndex = 10;
            this.toolTip1.SetToolTip(this.button_record, "Record\r\n (i.e. convert into target settings)");
            this.button_record.UseVisualStyleBackColor = true;
            this.button_record.Click += new System.EventHandler(this.button_record_Click);
            // 
            // button1
            // 
            this.button1.Image = global::APlayer.Properties.Resources.control_eject;
            this.button1.Location = new System.Drawing.Point(6, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 46);
            this.button1.TabIndex = 9;
            this.toolTip1.SetToolTip(this.button1, "Open File(s) (CTRL + O)");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // button_prev
            // 
            this.button_prev.Image = global::APlayer.Properties.Resources.control_start;
            this.button_prev.Location = new System.Drawing.Point(454, 22);
            this.button_prev.Name = "button_prev";
            this.button_prev.Size = new System.Drawing.Size(56, 46);
            this.button_prev.TabIndex = 8;
            this.toolTip1.SetToolTip(this.button_prev, "Previous (F2)");
            this.button_prev.UseVisualStyleBackColor = true;
            this.button_prev.Click += new System.EventHandler(this.button_prev_Click);
            // 
            // button_next
            // 
            this.button_next.Image = global::APlayer.Properties.Resources.control_end;
            this.button_next.Location = new System.Drawing.Point(516, 22);
            this.button_next.Name = "button_next";
            this.button_next.Size = new System.Drawing.Size(56, 46);
            this.button_next.TabIndex = 7;
            this.toolTip1.SetToolTip(this.button_next, "Next (F3)");
            this.button_next.UseVisualStyleBackColor = true;
            this.button_next.Click += new System.EventHandler(this.button_next_Click);
            // 
            // trackBar_volume
            // 
            this.trackBar_volume.AutoSize = false;
            this.trackBar_volume.BackColor = System.Drawing.SystemColors.Control;
            this.trackBar_volume.Location = new System.Drawing.Point(834, 29);
            this.trackBar_volume.Maximum = 100;
            this.trackBar_volume.Name = "trackBar_volume";
            this.trackBar_volume.Size = new System.Drawing.Size(154, 34);
            this.trackBar_volume.TabIndex = 5;
            this.trackBar_volume.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_volume.Value = 100;
            this.trackBar_volume.Scroll += new System.EventHandler(this.trackBar_volume_Scroll);
            // 
            // button_toggle_mute
            // 
            this.button_toggle_mute.Image = global::APlayer.Properties.Resources.sound;
            this.button_toggle_mute.Location = new System.Drawing.Point(772, 22);
            this.button_toggle_mute.Name = "button_toggle_mute";
            this.button_toggle_mute.Size = new System.Drawing.Size(56, 46);
            this.button_toggle_mute.TabIndex = 6;
            this.button_toggle_mute.UseVisualStyleBackColor = true;
            this.button_toggle_mute.Click += new System.EventHandler(this.button_toggle_mute_Click);
            // 
            // button_pause
            // 
            this.button_pause.Image = global::APlayer.Properties.Resources.control_pause;
            this.button_pause.Location = new System.Drawing.Point(369, 22);
            this.button_pause.Name = "button_pause";
            this.button_pause.Size = new System.Drawing.Size(56, 46);
            this.button_pause.TabIndex = 3;
            this.toolTip1.SetToolTip(this.button_pause, "Pause (F6)");
            this.button_pause.UseVisualStyleBackColor = true;
            this.button_pause.Click += new System.EventHandler(this.button_pause_Click);
            // 
            // button_play_pause
            // 
            this.button_play_pause.Image = global::APlayer.Properties.Resources.control_play;
            this.button_play_pause.Location = new System.Drawing.Point(228, 22);
            this.button_play_pause.Name = "button_play_pause";
            this.button_play_pause.Size = new System.Drawing.Size(135, 46);
            this.button_play_pause.TabIndex = 1;
            this.toolTip1.SetToolTip(this.button_play_pause, "Play (F5)");
            this.button_play_pause.UseVisualStyleBackColor = true;
            this.button_play_pause.Click += new System.EventHandler(this.button_play_pause_Click);
            // 
            // button_stop
            // 
            this.button_stop.Image = global::APlayer.Properties.Resources.control_stop;
            this.button_stop.Location = new System.Drawing.Point(166, 22);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(56, 46);
            this.button_stop.TabIndex = 0;
            this.toolTip1.SetToolTip(this.button_stop, "Stop (F7)");
            this.button_stop.UseVisualStyleBackColor = true;
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // label_time
            // 
            this.label_time.BackColor = System.Drawing.SystemColors.Control;
            this.label_time.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_time.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_time.Font = new System.Drawing.Font("Franklin Gothic Medium", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_time.ForeColor = System.Drawing.Color.Black;
            this.label_time.Location = new System.Drawing.Point(3, 96);
            this.label_time.Name = "label_time";
            this.label_time.Size = new System.Drawing.Size(656, 56);
            this.label_time.TabIndex = 4;
            this.label_time.Text = "00:00:00 - 00:00:00";
            this.label_time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 25);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(65, 22);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "OFF";
            this.toolTip1.SetToolTip(this.radioButton1, "Disable the db fix.");
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // radioButton_db_fix_on
            // 
            this.radioButton_db_fix_on.AutoSize = true;
            this.radioButton_db_fix_on.Location = new System.Drawing.Point(6, 53);
            this.radioButton_db_fix_on.Name = "radioButton_db_fix_on";
            this.radioButton_db_fix_on.Size = new System.Drawing.Size(56, 22);
            this.radioButton_db_fix_on.TabIndex = 0;
            this.radioButton_db_fix_on.Text = "ON";
            this.toolTip1.SetToolTip(this.radioButton_db_fix_on, resources.GetString("radioButton_db_fix_on.ToolTip"));
            this.radioButton_db_fix_on.UseVisualStyleBackColor = true;
            this.radioButton_db_fix_on.Click += new System.EventHandler(this.radioButton_db_fix_on_Click);
            // 
            // radioButton_wave_fix_shift
            // 
            this.radioButton_wave_fix_shift.AutoSize = true;
            this.radioButton_wave_fix_shift.Checked = true;
            this.radioButton_wave_fix_shift.Location = new System.Drawing.Point(6, 53);
            this.radioButton_wave_fix_shift.Name = "radioButton_wave_fix_shift";
            this.radioButton_wave_fix_shift.Size = new System.Drawing.Size(56, 22);
            this.radioButton_wave_fix_shift.TabIndex = 3;
            this.radioButton_wave_fix_shift.TabStop = true;
            this.radioButton_wave_fix_shift.Text = "ON";
            this.toolTip1.SetToolTip(this.radioButton_wave_fix_shift, resources.GetString("radioButton_wave_fix_shift.ToolTip"));
            this.radioButton_wave_fix_shift.UseVisualStyleBackColor = true;
            this.radioButton_wave_fix_shift.Click += new System.EventHandler(this.radioButton_wave_fix_shift_Click);
            // 
            // button2
            // 
            this.button2.Image = global::APlayer.Properties.Resources.folder;
            this.button2.Location = new System.Drawing.Point(6, 20);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(79, 46);
            this.button2.TabIndex = 1;
            this.toolTip1.SetToolTip(this.button2, "Open folder \r\n(scan a folder for audio files with supported formats)");
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.openFolderToolStripMenuItem_Click);
            // 
            // button3
            // 
            this.button3.Image = global::APlayer.Properties.Resources.folder_explore;
            this.button3.Location = new System.Drawing.Point(166, 20);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(56, 46);
            this.button3.TabIndex = 2;
            this.toolTip1.SetToolTip(this.button3, "Open folder including sub folders\r\n(scan folder(s) for audio files with supported" +
        " formats)\r\n");
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.openFolderIncludeSubFoldersToolStripMenuItem_Click);
            // 
            // button4
            // 
            this.button4.Image = global::APlayer.Properties.Resources.folder_table;
            this.button4.Location = new System.Drawing.Point(228, 20);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(135, 46);
            this.button4.TabIndex = 3;
            this.toolTip1.SetToolTip(this.button4, "Open Playlist");
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.openListToolStripMenuItem_Click);
            // 
            // button_save_list
            // 
            this.button_save_list.Image = global::APlayer.Properties.Resources.table_save;
            this.button_save_list.Location = new System.Drawing.Point(369, 20);
            this.button_save_list.Name = "button_save_list";
            this.button_save_list.Size = new System.Drawing.Size(56, 46);
            this.button_save_list.TabIndex = 4;
            this.toolTip1.SetToolTip(this.button_save_list, "Save Playlist");
            this.button_save_list.UseVisualStyleBackColor = true;
            this.button_save_list.Click += new System.EventHandler(this.saveListToolStripMenuItem_Click);
            // 
            // button6
            // 
            this.button6.Image = global::APlayer.Properties.Resources.door_in;
            this.button6.Location = new System.Drawing.Point(932, 20);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(56, 46);
            this.button6.TabIndex = 7;
            this.toolTip1.SetToolTip(this.button6, "Exit");
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // button7
            // 
            this.button7.Image = global::APlayer.Properties.Resources.help;
            this.button7.Location = new System.Drawing.Point(772, 20);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(56, 46);
            this.button7.TabIndex = 8;
            this.toolTip1.SetToolTip(this.button7, "Help (Online Wiki)");
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.helpToolStripMenuItem1_Click);
            // 
            // button8
            // 
            this.button8.ForeColor = System.Drawing.Color.DimGray;
            this.button8.Location = new System.Drawing.Point(710, 20);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(56, 46);
            this.button8.TabIndex = 9;
            this.button8.Text = "A";
            this.toolTip1.SetToolTip(this.button8, "About Agile Player");
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // button9
            // 
            this.button9.Image = global::APlayer.Properties.Resources.paypal__1_;
            this.button9.Location = new System.Drawing.Point(454, 20);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(56, 46);
            this.button9.TabIndex = 10;
            this.toolTip1.SetToolTip(this.button9, "Donate\r\n( Thanks in advance :) )");
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.donateToolStripMenuItem_Click);
            // 
            // button10
            // 
            this.button10.Image = global::APlayer.Properties.Resources.world;
            this.button10.Location = new System.Drawing.Point(516, 20);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(56, 46);
            this.button10.TabIndex = 11;
            this.toolTip1.SetToolTip(this.button10, "Website (Online Repository)");
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.websiteOnlineRepositoryToolStripMenuItem_Click);
            // 
            // groupBox_meter
            // 
            this.groupBox_meter.Controls.Add(this.panel_db_meter);
            this.groupBox_meter.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_meter.Location = new System.Drawing.Point(846, 196);
            this.groupBox_meter.Name = "groupBox_meter";
            this.groupBox_meter.Size = new System.Drawing.Size(160, 305);
            this.groupBox_meter.TabIndex = 9;
            this.groupBox_meter.TabStop = false;
            this.groupBox_meter.Text = "DB Meter";
            this.toolTip1.SetToolTip(this.groupBox_meter, "db meter works only with 16 bits. ");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_32bits);
            this.groupBox1.Controls.Add(this.radioButton_24bits);
            this.groupBox1.Controls.Add(this.radioButton_16bit);
            this.groupBox1.Controls.Add(this.radioButton_8bits);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 507);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(160, 155);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bits Per Sample";
            // 
            // radioButton_32bits
            // 
            this.radioButton_32bits.AutoSize = true;
            this.radioButton_32bits.Location = new System.Drawing.Point(6, 109);
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
            this.radioButton_24bits.Location = new System.Drawing.Point(6, 81);
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
            this.radioButton_16bit.Location = new System.Drawing.Point(6, 53);
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
            this.groupBox2.Location = new System.Drawing.Point(12, 196);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(160, 305);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Frequency";
            // 
            // radioButton_32000hz
            // 
            this.radioButton_32000hz.AutoSize = true;
            this.radioButton_32000hz.Location = new System.Drawing.Point(6, 137);
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
            this.radioButton_freq_96000.Location = new System.Drawing.Point(6, 249);
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
            this.radioButton_freq_88200.Location = new System.Drawing.Point(6, 221);
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
            this.radioButton_freq_48000.Location = new System.Drawing.Point(6, 193);
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
            this.radioButton_freq_44100.Location = new System.Drawing.Point(6, 165);
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
            this.radioButton_freq_22050.Location = new System.Drawing.Point(6, 109);
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
            this.radioButton_freq_16000.Location = new System.Drawing.Point(6, 81);
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
            this.radioButton_freq_11025.Location = new System.Drawing.Point(6, 53);
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
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButton_stereo);
            this.groupBox4.Controls.Add(this.radioButton_mono);
            this.groupBox4.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(12, 95);
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
            this.radioButton_stereo.Location = new System.Drawing.Point(6, 53);
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
            this.groupBox_main.Location = new System.Drawing.Point(178, 95);
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
            this.groupBox6.Location = new System.Drawing.Point(178, 507);
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
            this.groupBox7.Location = new System.Drawing.Point(12, 668);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(994, 77);
            this.groupBox7.TabIndex = 10;
            this.groupBox7.TabStop = false;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.pictureBox1);
            this.groupBox8.Controls.Add(this.radioButton_wave_fix_shift);
            this.groupBox8.Controls.Add(this.radioButton_wave_fix_off);
            this.groupBox8.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.Location = new System.Drawing.Point(846, 507);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(160, 155);
            this.groupBox8.TabIndex = 11;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Wave Shift";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Image = global::APlayer.Properties.Resources.Screenshot_2022_02_21_030033;
            this.pictureBox1.Location = new System.Drawing.Point(6, 81);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(148, 68);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // radioButton_wave_fix_off
            // 
            this.radioButton_wave_fix_off.AutoSize = true;
            this.radioButton_wave_fix_off.Location = new System.Drawing.Point(6, 25);
            this.radioButton_wave_fix_off.Name = "radioButton_wave_fix_off";
            this.radioButton_wave_fix_off.Size = new System.Drawing.Size(65, 22);
            this.radioButton_wave_fix_off.TabIndex = 2;
            this.radioButton_wave_fix_off.Text = "OFF";
            this.radioButton_wave_fix_off.UseVisualStyleBackColor = true;
            this.radioButton_wave_fix_off.Click += new System.EventHandler(this.radioButton_wave_fix_off_Click);
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButton1);
            this.groupBox3.Controls.Add(this.radioButton_db_fix_on);
            this.groupBox3.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(846, 95);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(160, 95);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "DB Fix";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button10);
            this.groupBox5.Controls.Add(this.button9);
            this.groupBox5.Controls.Add(this.button8);
            this.groupBox5.Controls.Add(this.button7);
            this.groupBox5.Controls.Add(this.button6);
            this.groupBox5.Controls.Add(this.button_save_list);
            this.groupBox5.Controls.Add(this.button4);
            this.groupBox5.Controls.Add(this.button3);
            this.groupBox5.Controls.Add(this.button2);
            this.groupBox5.Location = new System.Drawing.Point(12, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(994, 77);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            // 
            // panel_db_meter
            // 
            this.panel_db_meter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_db_meter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_db_meter.Location = new System.Drawing.Point(3, 22);
            this.panel_db_meter.Name = "panel_db_meter";
            this.panel_db_meter.Size = new System.Drawing.Size(154, 280);
            this.panel_db_meter.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 759);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox_meter);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox_main);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Agile Player";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_volume)).EndInit();
            this.groupBox_meter.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox_main.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button_play_pause;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.Button button_pause;
        private System.Windows.Forms.Label label_time;
        private System.Windows.Forms.TrackBar trackBar_volume;
        private System.Windows.Forms.Button button_toggle_mute;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button_next;
        private System.Windows.Forms.Button button_prev;
        private System.Windows.Forms.ToolTip toolTip1;
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
        private System.Windows.Forms.GroupBox groupBox_meter;
        private System.Windows.Forms.Panel panel_media_bar;
        private System.Windows.Forms.Button button_record;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_song_name;
        private System.Windows.Forms.Label label_channels;
        private System.Windows.Forms.Label label_freq;
        private System.Windows.Forms.Label label_bits;
        private System.Windows.Forms.Timer timer_per_ind;
        private System.Windows.Forms.Timer timer_meter;
        private System.Windows.Forms.RadioButton radioButton_32000hz;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton_db_fix_on;
        private System.Windows.Forms.RadioButton radioButton_wave_fix_shift;
        private System.Windows.Forms.RadioButton radioButton_wave_fix_off;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button_save_list;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Panel panel_db_meter;
    }
}

