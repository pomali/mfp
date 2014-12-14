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
	/// 
	
	public class ParticlePair
	{
		public Particle a;
		public Particle b;
		public ParticlePair(Particle in1, Particle in2)
		{
			this.a = in1;
			this.b = in2;
			
		}
		
		public void Draw(Graphics g)
		{
			g.DrawLine(Pens.SlateGray, (float)a.position.X, (float)a.position.Y, (float)b.position.X, (float)b.position.Y);
		}
		
		public void ProjectDistanceConstraints()
		{
			double L = 15;
			double s = ((distance - L)/(w_total))/distance;
			a.q += (-a.w)*s*vect;
			a.q += (b.w)*s*vect;
		}
		
		public void ProjectFloorConstraints(double limit)
		{
			// chcem aby bola Y suradnica < limit
			// teda chcem p1.Y - limit < 0

			double ca = a.q.Y - limit;
			double cb = b.q.Y - limit;
			if (ca >= 0)
			{
				a.q.Y = limit;
			}
			
			if (cb >= 0)
			{
				b.q.Y = limit;
			}
		}
		
		double w_total { get { return a.w + b.w;}}
		Vector4 vect { get { return a.q - b.q;}}
		double distance { get { return vect.Length;}}
	}

	public class ParticleGroup
	{
		
		
		public List<ParticlePair> particle_pairs = new List<ParticlePair>();
		public List<Particle> particles = new List<Particle>();
		public DateTime born;
		
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
			born = DateTime.Now;
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
		
		public void ProjectDistanceConstraints()
		{
			foreach(ParticlePair p in particle_pairs)
			{
				p.ProjectDistanceConstraints();
			}
		}
		
		public void ProjectFloorConstraints(double limit)
		{
			foreach(ParticlePair p in particle_pairs)
			{
				p.ProjectFloorConstraints(limit);
			}
		}
		
		public Vector4 velocity
		{
			get {
				double total_mass = 0;
				Vector4 velocity = new Vector4(0,0,0,0);
				foreach (Particle p in particles)
				{
					velocity += p.mass * p.velocity;
					total_mass += p.mass;
				}
				return (1/total_mass) * velocity;
			}
		}
			
		public Vector4 position
		{
			get { 
				double total_mass = 0;
				Vector4 center = new Vector4(0,0,0,0);
				foreach (Particle p in particles)
				{
					center += p.mass * p.position;
					total_mass += p.mass;
				}
				return (1/total_mass) * center;
			}
		}
	}
}
