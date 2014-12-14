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
		
		public void ProjectDistanceConstraints()
		{
			double L = 25;
			double s = ((distance - L)/(w_total))/distance;
			a.q += (-a.w)*s*vect;
			a.q += (b.w)*s*vect;
		}
		
		public void ProjectFloorConstraints()
		{
			// chcem aby bola Y suradnica < 600
			// teda chcem p1.Y - 600 < 0
			double limit = 600;
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
		Vector4 vect { get { return a.position - b.position;}}
		double distance { get { return vect.Length;}}
	}

	public class ParticleGroup
	{
		
		
		public List<ParticlePair> particle_pairs = new List<ParticlePair>();
		public List<Particle> particles = new List<Particle>();
		
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
		
		public void ProjectDistanceConstraints()
		{
			foreach(ParticlePair p in particle_pairs)
			{
				p.ProjectDistanceConstraints();
			}
		}
		
		public void ProjectFloorConstraints()
		{
			foreach(ParticlePair p in particle_pairs)
			{
				p.ProjectFloorConstraints();
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
