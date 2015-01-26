/*
 * Created by SharpDevelop.
 * User: pom
 * Date: 12/14/2014
 * Time: 12:18 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace mfp2
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		
		PBDSystem pbd;
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			
			DoubleBuffered = true;
			InitializeComponent();
			
		}
		
		protected override void OnPaint(PaintEventArgs e)
		{
			//base.OnPaint(e);
			Graphics g = e.Graphics;
//            g.SmoothingMode = SmoothingMode.AntiAlias;
//            g.InterpolationMode = InterpolationMode.NearestNeighbor;
//            g.PixelOffsetMode = PixelOffsetMode.Half;
        	pbd.Draw(g);
        	lbl_system_step.Text = pbd.System_step.ToString();

		}
		
		void pbd_Step()
		{
			pbd.Update(); //ked chces zabavu tak to daj do onpaint a resizuj
			this.Invalidate();
		}
		
		void TimerRedrawTick(object sender, EventArgs e)
		{
			pbd_Step();
		}
		
		void ButtonStartClick(object sender, EventArgs e)
		{
			if (timerRedraw.Enabled)
			{
				timerRedraw.Enabled = false;
				buttonStart.Text = "Start";
				btnStep.Enabled = true;
			}
			else
			{
				timerRedraw.Enabled = true;
				buttonStart.Text = "Stop";
				btnStep.Enabled = false;
			}

		}
		
		void RestartSystem()
		{
			pbd = new PBDSystem();
			num_kd.Value = (decimal)(pbd.kd);
			num_kc.Value = (decimal)(pbd.kc);
			double k = Math.Floor(Math.Log10(pbd.dt));
			tB_dt.Value = -(int)k;
			num_ttl.Value = pbd.lifetime;
			num_solit.Value = (decimal) pbd.ns;
			cb_aabb.Checked = pbd.draw_aabb;
			num_size.Value = (decimal) pbd.spring_size;
			
			cb_autospawn.Checked = pbd.autospawn;
			cp_compute_collisions.Checked = pbd.compute_collisions;
			
			pbd.limit_X = Size.Width;
			pbd.limit_Y = Size.Height-60;
			pbd.presentation_interval = tb_time_speed.Value;
			timerRedraw.Interval = tb_time_speed.Value;
			
			lbl_dt.Text = lbl_dt.Tag + ": " + pbd.dt.ToString();
		}
		
		void ButtonRestartClick(object sender, EventArgs e)
		{
			RestartSystem();
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			RestartSystem();
		}
		
		void TB_dtScroll(object sender, EventArgs e)
		{
			double x = Math.Pow(10,-tB_dt.Value);
			pbd.dt = x;
			lbl_dt.Text = lbl_dt.Tag + ": " + x.ToString();
		}
		
		void Num_ttlValueChanged(object sender, EventArgs e)
		{
			pbd.lifetime = (int) num_ttl.Value;
		}
		
		void Num_solitValueChanged(object sender, EventArgs e)
		{
			pbd.ns = (int)num_solit.Value;
		}
		
		void BtnStepClick(object sender, EventArgs e)
		{
			pbd_Step();
		}
		
		void Num_sizeValueChanged(object sender, EventArgs e)
		{
			pbd.spring_size = (int)num_size.Value;
		}
		
		void Cb_aabbCheckedChanged(object sender, EventArgs e)
		{
			pbd.draw_aabb = cb_aabb.Checked;			
		}
		
		
		void Num_kdValueChanged(object sender, System.EventArgs e)
		{
			pbd.kd = (double) num_kd.Value;
		}
		
		void Num_kcValueChanged(object sender, EventArgs e)
		{
			pbd.kc = (double) num_kc.Value;
		}
		
		void Tb_time_speedScroll(object sender, EventArgs e)
		{
			timerRedraw.Interval = tb_time_speed.Value;			
			pbd.presentation_interval = tb_time_speed.Value;
		}
		
		void MainFormClick(object sender, EventArgs e)
		{
			pbd.Spawn();
		}
		
		void MainFormResizeEnd(object sender, EventArgs e)
		{
			pbd.limit_X = Size.Width;
			pbd.limit_Y = Size.Height-60;
		}
		
		void MainFormSizeChanged(object sender, EventArgs e)
		{
			pbd.limit_X = Size.Width;
			pbd.limit_Y = Size.Height-60;
		}
		
		void Cb_autospawnCheckedChanged(object sender, EventArgs e)
		{
			pbd.autospawn = cb_autospawn.Checked;
		}
		
		void Panel2Paint(object sender, PaintEventArgs e)
		{
			
		}
		
		void Cp_compute_collisionsCheckedChanged(object sender, EventArgs e)
		{
			pbd.compute_collisions = cp_compute_collisions.Checked;			
		}
		
		void Cb_normal_timeCheckedChanged(object sender, EventArgs e)
		{
			
		}
	}
}
