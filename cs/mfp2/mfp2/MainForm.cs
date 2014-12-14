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
		
		PBDSystem pdb = new PBDSystem();
		
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
        	pdb.Draw(g);

		}
		
		void TimerRedrawTick(object sender, EventArgs e)
		{
			this.Invalidate();
			pdb.Update(); //ked chces zabavu tak to daj do onpaint a resizuj
		}
		
		void TimerParticleEmitterTick(object sender, EventArgs e)
		{
			pdb.Spawn();
		}
		
		void ButtonStartClick(object sender, EventArgs e)
		{
			if (timerRedraw.Enabled)
			{
				timerParticleEmitter.Enabled = false;
				timerRedraw.Enabled = false;
				buttonStart.Text = "Start";
			}
			else
			{
				timerParticleEmitter.Enabled = true;
				timerRedraw.Enabled = true;
				buttonStart.Text = "Stop";
			}

		}
		
		void ButtonRestartClick(object sender, EventArgs e)
		{
			pdb = new PBDSystem();
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			
		}
	}
}
