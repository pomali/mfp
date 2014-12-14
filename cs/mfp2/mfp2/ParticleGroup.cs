/*
 * Created by SharpDevelop.
 * User: karci
 * Date: 12/14/2014
 * Time: 1:49 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;

namespace mfp2
{
	/// <summary>
	/// Description of ParticleGroup.
	/// </summary>
	public class ParticleGroup
	{
		public class ParticlePair
		{
			Particle a;
			Particle b;
			public ParticlePair(Particle in1, Particle in2)
			{
				this.a = in1;
				this.b = in2;
			}
			
			public void Draw(Graphics g)
			{
				a.Draw(g);
				b.Draw(g);
				g.DrawLine(Pens.Black, a.position.X, a.position.Y, b.position.X, b.position.Y);
			}
		}
		
		List<ParticlePair> particle_pairs = new List<ParticlePair>();
		
		public ParticleGroup()
		{
			Particle a = new Particle();
			Particle b = new Particle();
			Particle c = new Particle();
			
			particle_pairs.Add();
		}
		
		
		public void Draw(Graphics g)
		{
			foreach (ParticlePair p in particle_pairs)
			{
				p.Draw(g);
			}
		}
	}
}
