﻿/*
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
		public double mass = 0.9;
		public Vector4 velocity = new Vector4(0,0,0,0);
		public Vector4 position = new Vector4(400,80,0,0);
		public Vector4 acceleration = new Vector4(0,0,0,0);
		public Vector4 q;
		
		Brush brush = Brushes.Blue;
		
		public Particle( Brush in_brush, int seed = 0)
		{
			Random rnd = new Random(seed);
			velocity = new Vector4(3-(rnd.NextDouble()*10),-(rnd.NextDouble()*3),0,0);
			position = new Vector4(400+rnd.Next(-10,10),80+rnd.Next(-10,10),0,0);
			//mass = rnd.NextDouble()*0.1;
			brush = in_brush;
		}
		
		public void Draw(Graphics g)
		{
			g.FillEllipse(brush, (float)(position.X-1.5), (float)(position.Y-1.5), 3, 3);
		}
		
		
		public double w
		{
			get { return 1/mass; }
		}
		
	}
}
