/*
 * Created by SharpDevelop.
 * User: karci
 * Date: 12/14/2014
 * Time: 12:18 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace mfp2
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.timerRedraw = new System.Windows.Forms.Timer(this.components);
			this.buttonStart = new System.Windows.Forms.Button();
			this.buttonRestart = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.tb_time_speed = new System.Windows.Forms.TrackBar();
			this.panel4 = new System.Windows.Forms.Panel();
			this.tB_dt = new System.Windows.Forms.TrackBar();
			this.lbl_dt = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.panel5 = new System.Windows.Forms.Panel();
			this.num_ttl = new System.Windows.Forms.NumericUpDown();
			this.lbl_ttl = new System.Windows.Forms.Label();
			this.panel6 = new System.Windows.Forms.Panel();
			this.num_solit = new System.Windows.Forms.NumericUpDown();
			this.lbl_solit = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.num_size = new System.Windows.Forms.NumericUpDown();
			this.num_kc = new System.Windows.Forms.NumericUpDown();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.cp_compute_collisions = new System.Windows.Forms.CheckBox();
			this.cb_autospawn = new System.Windows.Forms.CheckBox();
			this.lbl_system_step = new System.Windows.Forms.Label();
			this.btnStep = new System.Windows.Forms.Button();
			this.cb_aabb = new System.Windows.Forms.CheckBox();
			this.num_kd = new System.Windows.Forms.NumericUpDown();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tb_time_speed)).BeginInit();
			this.panel4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tB_dt)).BeginInit();
			this.panel5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.num_ttl)).BeginInit();
			this.panel6.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.num_solit)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.num_size)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.num_kc)).BeginInit();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.num_kd)).BeginInit();
			this.SuspendLayout();
			// 
			// timerRedraw
			// 
			this.timerRedraw.Interval = 40;
			this.timerRedraw.Tick += new System.EventHandler(this.TimerRedrawTick);
			// 
			// buttonStart
			// 
			this.buttonStart.Location = new System.Drawing.Point(28, 19);
			this.buttonStart.Name = "buttonStart";
			this.buttonStart.Size = new System.Drawing.Size(39, 23);
			this.buttonStart.TabIndex = 0;
			this.buttonStart.Text = "Start";
			this.buttonStart.UseVisualStyleBackColor = true;
			this.buttonStart.Click += new System.EventHandler(this.ButtonStartClick);
			// 
			// buttonRestart
			// 
			this.buttonRestart.Location = new System.Drawing.Point(6, 19);
			this.buttonRestart.Name = "buttonRestart";
			this.buttonRestart.Size = new System.Drawing.Size(21, 23);
			this.buttonRestart.TabIndex = 1;
			this.buttonRestart.Text = "R";
			this.buttonRestart.UseVisualStyleBackColor = true;
			this.buttonRestart.Click += new System.EventHandler(this.ButtonRestartClick);
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.Controls.Add(this.tb_time_speed);
			this.panel2.Controls.Add(this.panel4);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Controls.Add(this.panel5);
			this.panel2.Controls.Add(this.panel6);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.groupBox1);
			this.panel2.Controls.Add(this.num_kc);
			this.panel2.Controls.Add(this.groupBox2);
			this.panel2.Controls.Add(this.num_kd);
			this.panel2.Location = new System.Drawing.Point(656, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(127, 442);
			this.panel2.TabIndex = 7;
			this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel2Paint);
			// 
			// tb_time_speed
			// 
			this.tb_time_speed.LargeChange = 20;
			this.tb_time_speed.Location = new System.Drawing.Point(7, 406);
			this.tb_time_speed.Maximum = 220;
			this.tb_time_speed.Minimum = 30;
			this.tb_time_speed.Name = "tb_time_speed";
			this.tb_time_speed.Size = new System.Drawing.Size(104, 45);
			this.tb_time_speed.SmallChange = 5;
			this.tb_time_speed.TabIndex = 14;
			this.tb_time_speed.Value = 40;
			this.tb_time_speed.Scroll += new System.EventHandler(this.Tb_time_speedScroll);
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.tB_dt);
			this.panel4.Controls.Add(this.lbl_dt);
			this.panel4.Location = new System.Drawing.Point(3, 69);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(121, 55);
			this.panel4.TabIndex = 8;
			// 
			// tB_dt
			// 
			this.tB_dt.Location = new System.Drawing.Point(3, 16);
			this.tB_dt.Maximum = 1000;
			this.tB_dt.Name = "tB_dt";
			this.tB_dt.Size = new System.Drawing.Size(101, 45);
			this.tB_dt.TabIndex = 10;
			this.tB_dt.Scroll += new System.EventHandler(this.TB_dtScroll);
			// 
			// lbl_dt
			// 
			this.lbl_dt.AutoSize = true;
			this.lbl_dt.Location = new System.Drawing.Point(0, 0);
			this.lbl_dt.Name = "lbl_dt";
			this.lbl_dt.Size = new System.Drawing.Size(16, 13);
			this.lbl_dt.TabIndex = 9;
			this.lbl_dt.Tag = "dt";
			this.lbl_dt.Text = "dt";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(21, 23);
			this.label2.TabIndex = 17;
			this.label2.Text = "kc";
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.num_ttl);
			this.panel5.Controls.Add(this.lbl_ttl);
			this.panel5.Location = new System.Drawing.Point(7, 130);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(107, 29);
			this.panel5.TabIndex = 8;
			// 
			// num_ttl
			// 
			this.num_ttl.Increment = new decimal(new int[] {
									10,
									0,
									0,
									0});
			this.num_ttl.Location = new System.Drawing.Point(25, 6);
			this.num_ttl.Maximum = new decimal(new int[] {
									1000,
									0,
									0,
									0});
			this.num_ttl.Minimum = new decimal(new int[] {
									10,
									0,
									0,
									0});
			this.num_ttl.Name = "num_ttl";
			this.num_ttl.Size = new System.Drawing.Size(82, 20);
			this.num_ttl.TabIndex = 9;
			this.num_ttl.Value = new decimal(new int[] {
									10,
									0,
									0,
									0});
			this.num_ttl.ValueChanged += new System.EventHandler(this.Num_ttlValueChanged);
			// 
			// lbl_ttl
			// 
			this.lbl_ttl.AutoSize = true;
			this.lbl_ttl.Location = new System.Drawing.Point(0, 0);
			this.lbl_ttl.Name = "lbl_ttl";
			this.lbl_ttl.Size = new System.Drawing.Size(27, 13);
			this.lbl_ttl.TabIndex = 10;
			this.lbl_ttl.Text = "TTL";
			// 
			// panel6
			// 
			this.panel6.Controls.Add(this.num_solit);
			this.panel6.Controls.Add(this.lbl_solit);
			this.panel6.Location = new System.Drawing.Point(7, 165);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(108, 40);
			this.panel6.TabIndex = 8;
			// 
			// num_solit
			// 
			this.num_solit.Location = new System.Drawing.Point(25, 16);
			this.num_solit.Name = "num_solit";
			this.num_solit.Size = new System.Drawing.Size(80, 20);
			this.num_solit.TabIndex = 1;
			this.num_solit.ValueChanged += new System.EventHandler(this.Num_solitValueChanged);
			// 
			// lbl_solit
			// 
			this.lbl_solit.AutoSize = true;
			this.lbl_solit.Location = new System.Drawing.Point(0, 0);
			this.lbl_solit.Name = "lbl_solit";
			this.lbl_solit.Size = new System.Drawing.Size(83, 13);
			this.lbl_solit.TabIndex = 0;
			this.lbl_solit.Text = "Solver Iterations";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(21, 18);
			this.label1.TabIndex = 16;
			this.label1.Text = "kd";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.num_size);
			this.groupBox1.Location = new System.Drawing.Point(8, 211);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(107, 46);
			this.groupBox1.TabIndex = 12;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Spring Length";
			// 
			// num_size
			// 
			this.num_size.Location = new System.Drawing.Point(17, 19);
			this.num_size.Maximum = new decimal(new int[] {
									300,
									0,
									0,
									0});
			this.num_size.Name = "num_size";
			this.num_size.Size = new System.Drawing.Size(61, 20);
			this.num_size.TabIndex = 9;
			this.num_size.Value = new decimal(new int[] {
									30,
									0,
									0,
									0});
			this.num_size.ValueChanged += new System.EventHandler(this.Num_sizeValueChanged);
			// 
			// num_kc
			// 
			this.num_kc.DecimalPlaces = 10;
			this.num_kc.Increment = new decimal(new int[] {
									1,
									0,
									0,
									262144});
			this.num_kc.Location = new System.Drawing.Point(34, 43);
			this.num_kc.Maximum = new decimal(new int[] {
									2,
									0,
									0,
									0});
			this.num_kc.Name = "num_kc";
			this.num_kc.Size = new System.Drawing.Size(90, 20);
			this.num_kc.TabIndex = 15;
			this.num_kc.ValueChanged += new System.EventHandler(this.Num_kcValueChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.cp_compute_collisions);
			this.groupBox2.Controls.Add(this.cb_autospawn);
			this.groupBox2.Controls.Add(this.buttonRestart);
			this.groupBox2.Controls.Add(this.lbl_system_step);
			this.groupBox2.Controls.Add(this.btnStep);
			this.groupBox2.Controls.Add(this.buttonStart);
			this.groupBox2.Controls.Add(this.cb_aabb);
			this.groupBox2.Location = new System.Drawing.Point(7, 256);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(118, 186);
			this.groupBox2.TabIndex = 8;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "controls";
			// 
			// cp_compute_collisions
			// 
			this.cp_compute_collisions.Location = new System.Drawing.Point(5, 101);
			this.cp_compute_collisions.Name = "cp_compute_collisions";
			this.cp_compute_collisions.Size = new System.Drawing.Size(102, 16);
			this.cp_compute_collisions.TabIndex = 16;
			this.cp_compute_collisions.Text = "self-collisions";
			this.cp_compute_collisions.UseVisualStyleBackColor = true;
			this.cp_compute_collisions.CheckedChanged += new System.EventHandler(this.Cp_compute_collisionsCheckedChanged);
			// 
			// cb_autospawn
			// 
			this.cb_autospawn.Location = new System.Drawing.Point(5, 82);
			this.cb_autospawn.Name = "cb_autospawn";
			this.cb_autospawn.Size = new System.Drawing.Size(104, 20);
			this.cb_autospawn.TabIndex = 15;
			this.cb_autospawn.Text = "autospawn";
			this.cb_autospawn.UseVisualStyleBackColor = true;
			this.cb_autospawn.CheckedChanged += new System.EventHandler(this.Cb_autospawnCheckedChanged);
			// 
			// lbl_system_step
			// 
			this.lbl_system_step.Location = new System.Drawing.Point(18, 45);
			this.lbl_system_step.Name = "lbl_system_step";
			this.lbl_system_step.Size = new System.Drawing.Size(94, 20);
			this.lbl_system_step.TabIndex = 11;
			this.lbl_system_step.Text = "99999999";
			this.lbl_system_step.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnStep
			// 
			this.btnStep.Location = new System.Drawing.Point(70, 19);
			this.btnStep.Name = "btnStep";
			this.btnStep.Size = new System.Drawing.Size(42, 23);
			this.btnStep.TabIndex = 10;
			this.btnStep.Text = "Step";
			this.btnStep.UseVisualStyleBackColor = true;
			this.btnStep.Click += new System.EventHandler(this.BtnStepClick);
			// 
			// cb_aabb
			// 
			this.cb_aabb.Location = new System.Drawing.Point(5, 68);
			this.cb_aabb.Name = "cb_aabb";
			this.cb_aabb.Size = new System.Drawing.Size(104, 17);
			this.cb_aabb.TabIndex = 13;
			this.cb_aabb.Text = "draw AABB";
			this.cb_aabb.UseVisualStyleBackColor = true;
			this.cb_aabb.CheckedChanged += new System.EventHandler(this.Cb_aabbCheckedChanged);
			// 
			// num_kd
			// 
			this.num_kd.DecimalPlaces = 10;
			this.num_kd.Increment = new decimal(new int[] {
									5,
									0,
									0,
									131072});
			this.num_kd.Location = new System.Drawing.Point(34, 17);
			this.num_kd.Maximum = new decimal(new int[] {
									2,
									0,
									0,
									0});
			this.num_kd.Name = "num_kd";
			this.num_kd.Size = new System.Drawing.Size(92, 20);
			this.num_kd.TabIndex = 14;
			this.num_kd.ValueChanged += new System.EventHandler(this.Num_kdValueChanged);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 562);
			this.Controls.Add(this.panel2);
			this.Name = "MainForm";
			this.Tag = "";
			this.Text = "mfp2";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.ResizeEnd += new System.EventHandler(this.MainFormResizeEnd);
			this.SizeChanged += new System.EventHandler(this.MainFormSizeChanged);
			this.Click += new System.EventHandler(this.MainFormClick);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.tb_time_speed)).EndInit();
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.tB_dt)).EndInit();
			this.panel5.ResumeLayout(false);
			this.panel5.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.num_ttl)).EndInit();
			this.panel6.ResumeLayout(false);
			this.panel6.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.num_solit)).EndInit();
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.num_size)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.num_kc)).EndInit();
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.num_kd)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.CheckBox cp_compute_collisions;
		private System.Windows.Forms.CheckBox cb_autospawn;
		private System.Windows.Forms.TrackBar tb_time_speed;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown num_kc;
		private System.Windows.Forms.NumericUpDown num_kd;
		private System.Windows.Forms.CheckBox cb_aabb;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label lbl_system_step;
		private System.Windows.Forms.Button btnStep;
		private System.Windows.Forms.NumericUpDown num_size;
		private System.Windows.Forms.Label lbl_solit;
		private System.Windows.Forms.NumericUpDown num_solit;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.NumericUpDown num_ttl;
		private System.Windows.Forms.Label lbl_ttl;
		private System.Windows.Forms.Label lbl_dt;
		private System.Windows.Forms.TrackBar tB_dt;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button buttonRestart;
		private System.Windows.Forms.Button buttonStart;
		private System.Windows.Forms.Timer timerRedraw;
		
	}
}
