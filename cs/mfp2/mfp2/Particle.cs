/*
 * Created by SharpDevelop.
 * User: pom
 * Date: 12/14/2014
 * Time: 12:19 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Runtime.Remoting.Channels;
using CG1.Ex02.Mathematics;

namespace mfp2
{
	/// <summary>
	/// Description of Particle.
	/// </summary>
	public class Particle
	{
		double mass = 0.1;
		Vector4 velocity;
		public Vector4 position;
		Vector4 acceleration = new Vector4(0,0.981,0,0);
		bool active = true;
		
		public Particle()
		{
			
			Random rnd = new Random();
			velocity = new Vector4(3-(rnd.NextDouble()*6),-3,0,0);
			position = new Vector4(400,80,0,0);
			mass = rnd.NextDouble()*10;
		}
		
		public void Draw(Graphics g)
		{
			g.FillEllipse(Brushes.Blue, (float)position.X, (float)position.Y, 3, 3);
			Update();
		}
		
		public void Update()
		{
			if(active){
				if (position.X < 760 && position.Y < 560)
				{
					position += velocity;
					velocity += mass * acceleration;
				}
				else
				{
					position = new Vector4(Math.Min(position.X,760),Math.Min(position.Y,560), 0,0);
					velocity = new Vector4(0,0,0,0);
					active = false;
				}
			}
		}
		
	}
}
