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

namespace mfp2
{
	/// <summary>
	/// Description of Particle.
	/// </summary>
	public class Particle
	{
		public double mass = 10000;
		public double mass_base = 1e12; 
		public Vector4 velocity = new Vector4(0	,0,0,0);
		public Vector4 position = new Vector4(400,80,1,1);
		public Vector4 acceleration = new Vector4(0,0,0,0);
		public Vector4 q; // pozicia pocas medzivypoctov
		
		Brush brush = Brushes.Blue;
		
		public Particle( Brush in_brush, int seed = 0)
		{
			Random rnd = new Random(seed);
			velocity = new Vector4(3-(rnd.NextDouble()*6),-3,0,0);
			position = new Vector4(400+rnd.Next(-10,10),80+rnd.Next(-10,10),1,1);
			mass = mass_base;
			//mass = (rnd.NextDouble()*100+1)*mass_base;
			brush = in_brush;
		}
		
		public void CheckPosition()
		{
			if (position.X > 2000 && position.X < -1000 && position.Y > 2000 && position.Y < -1000)
			{
				throw new ArgumentOutOfRangeException();
			}
			
		}
		
		public void Draw(Graphics g)
		{
			CheckPosition();
			float r = (float)(2+((mass/mass_base)/10));
			g.FillEllipse(brush, (float)(position.X-(r/2)), (float)(position.Y-(r/2)), r, r);
		}
		
		public double w
		{
			get { return 1.0/mass; }
		}
		
	}
}
