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
		
		int tb_dt_const = 1000000000;
		
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
		
		void TimerRedrawTick(object sender, EventArgs e)
		{
						pbd.Update(); //ked chces zabavu tak to daj do onpaint a resizuj
			this.Invalidate();

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
			tB_dt.Value = (int)(pbd.dt*tb_dt_const);
			num_ttl.Value = pbd.lifetime;
			num_solit.Value = (decimal) pbd.ns;
			cb_aabb.Checked = pbd.draw_aabb;
			num_size.Value = (decimal) pbd.spring_size;
			pbd.limit_X = Size.Width;
			pbd.limit_Y = Size.Height-60;
			cb_autospawn.Checked = pbd.autospawn;
			
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
			double x = ((double)tB_dt.Value+1)/tb_dt_const;
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
			this.Invalidate();
			pbd.Update(); //ked chces zabavu tak to daj do onpaint a resizuj
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
	}
}
