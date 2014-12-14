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
			this.timerParticleEmitter = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// timerRedraw
			// 
			this.timerRedraw.Enabled = true;
			this.timerRedraw.Interval = 40;
			this.timerRedraw.Tick += new System.EventHandler(this.TimerRedrawTick);
			// 
			// timerParticleEmitter
			// 
			this.timerParticleEmitter.Enabled = true;
			this.timerParticleEmitter.Interval = 1000;
			this.timerParticleEmitter.Tick += new System.EventHandler(this.TimerParticleEmitterTick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 562);
			this.Name = "MainForm";
			this.Text = "mfp2";
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Timer timerParticleEmitter;
		private System.Windows.Forms.Timer timerRedraw;
	}
}
