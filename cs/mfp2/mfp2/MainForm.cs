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
		
		List<Particle> particles = new List<Particle>();
		
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
			base.OnPaint(e);
			Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = PixelOffsetMode.Half;
            foreach (Particle x in particles)
            {
            	x.Draw(g);
            }
		}
		
		void TimerRedrawTick(object sender, EventArgs e)
		{
			this.Invalidate();
		}
		
		void TimerParticleEmitterTick(object sender, EventArgs e)
		{
			particles.Add(new Particle());
		}
	}
}
