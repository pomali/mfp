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
using CG1.Ex02.Mathematics;

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
				g.DrawLine(Pens.SlateGray, (float)a.position.X, (float)a.position.Y, (float)b.position.X, (float)b.position.Y);
			}
			
			public void Update()
			{
				double ks = 0.5;
				double kd = 0.5;
				double L = 0;
				Vector4 c = (a.position - b.position);
				double distance = c.Length;
				Vector4 f = - ( ks * (distance - L) )* ( (1/distance) * c );
				               // + kd/distance * (a.velocity.Length - b.velocity.Length)
				double total_mass = a.mass + b.mass;
				a.acceleration += (total_mass/(total_mass-a.mass))*f; //inverse mass * f
				b.acceleration += (-total_mass/(total_mass-b.mass))*f;

			}
		}
		
		List<ParticlePair> particle_pairs = new List<ParticlePair>();
		List<Particle> particles = new List<Particle>();
		
		public ParticleGroup()
		{
			Random rnd = new Random();
			Particle a = new Particle(Brushes.Blue, rnd.Next() + 1);
			Particle b = new Particle(Brushes.Red, rnd.Next() + 2);
			Particle c = new Particle(Brushes.Green, rnd.Next() + 3);
			
			particles.Add(a);
			particles.Add(b);
			particles.Add(c);
			
			particle_pairs.Add(new ParticlePair(a,b));
			particle_pairs.Add(new ParticlePair(b,c));
			particle_pairs.Add(new ParticlePair(a,c));
		}
		
		
		public void Draw(Graphics g)
		{
			foreach(Particle p in particles)
			{
				p.Draw(g);
			}
			
			foreach (ParticlePair p in particle_pairs)
			{
				p.Draw(g);
			}
			
		}
		
		public void Update()
		{
			foreach(Particle p in particles)
			{
				p.Update();
			} 
			
			foreach(ParticlePair p in particle_pairs)
			{
				p.Update();
			}
		}
	}
}
