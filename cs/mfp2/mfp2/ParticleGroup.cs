/*
 * Created by SharpDevelop.
 * User: pom
 * Date: 12/14/2014
 * Time: 1:49 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace mfp2
{
	/// <summary>
	/// Description of ParticleGroup.
	/// </summary>
	/// 
	
	public class AABBox
	{
		public double x1, x2, y1, y2;
		
		public AABBox(double in_x1,double in_x2,double in_y1,double in_y2)
		{
			x1 = in_x1;
			x2 = in_x2;
			y1 = in_y1;
			y2 = in_y2;
		}
		
		public bool is_inside(Vector4 p)
		{
			return x1 < p.X && p.X < x2 && y1 < p.Y && p.Y < y2;
		}
	}
	
	public class ParticlePair
	{
		public Particle a;
		public Particle b;
		public double L;
		public ParticlePair(Particle in1, Particle in2, double inL)
		{
			this.a = in1;
			this.b = in2;
			this.L = inL;
		}
		
		public void Draw(Graphics g)
		{
			g.DrawLine(Pens.SlateGray, (float)a.position.X, (float)a.position.Y, (float)b.position.X, (float)b.position.Y);
		}
		
		public void ProjectDistanceConstraints(double in_k)
		{
			double k = 1.4;
			double s = (distance - L)/distance;
			a.q += (((-a.w)/(w_total))*s*vect) * in_k * k;
			b.q += (((b.w)/(w_total))*s*vect) * in_k * k;
		}
		
		public void ProjectFloorConstraints(double limit, double in_k)
		{
			// chcem aby bola Y suradnica < limit (top je 0)
			// teda chcem p1.Y - limit < 0

			double ca = a.q.Y - limit;
			double cb = b.q.Y - limit;
			if (ca >= 1)
			{
				a.q = a.q + ((a.position - a.q) * (ca/Math.Abs(a.position.Y - a.q.Y)));
			}
			
			if (cb >= 1)
			{
				b.q = b.q + ((b.position - b.q) * (cb/Math.Abs(b.position.Y - b.q.Y)));
				//b.q.Y = b.q.Y - cb - 1;
			}
		}
		
		double mass_total { get { return a.mass + b.mass;}}
		double w_total { get { return a.w + b.w;}}
		Vector4 vect { get { return a.q - b.q;}}
		double distance { get { return vect.Length;}}
	}

	public class ParticleGroup
	{
		public List<ParticlePair> particle_pairs = new List<ParticlePair>();
		public List<Particle> particles = new List<Particle>();
		public int born;
		public Random rnd;
		public double size; //dlzka jedneho paru aspon na teraz
		
		public ParticleGroup(double in_size, int system_step)
		{
			rnd = new Random();
			born = system_step;
			size = in_size;
		}
		
		public virtual void ProjectCollisionConstraint(Particle p)
		{
			throw new NotImplementedException();
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
		
		public void ProjectDistanceConstraints(double in_k)
		{
			foreach(ParticlePair p in particle_pairs)
			{
				p.ProjectDistanceConstraints(in_k);
			}
		}
		
		public void ProjectFloorConstraints(double limit, double in_k)
		{
			foreach(ParticlePair p in particle_pairs)
			{
				p.ProjectFloorConstraints(limit, in_k);
			}
		}
		
		public Particle AddParticle(Brush brush, int seed)
		{
			Particle a = new Particle(brush, seed);
			particles.Add(a);
			return a;
		}
		
		public void AddPair(int in1, int in2)
		{
			particle_pairs.Add(new ParticlePair(particles[in1],particles[in2], size));
		}
		
		public void AddPair(int in1, int in2, double inL)
		{
			particle_pairs.Add(new ParticlePair(particles[in1],particles[in2], inL));
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
		
		public AABBox aabb()
		{
			double x1 = Double.MaxValue;
			double x2 = 0;
			double y1 = Double.MaxValue;
			double y2 = 0;
			foreach (Particle p in particles)
			{
				x1 = Math.Min(x1, p.position.X);
				x2 = Math.Max(x2, p.position.X);
				y1 = Math.Min(y1, p.position.Y);
				y2 = Math.Max(y2, p.position.Y);
			}
			
			return new AABBox(x1,x2,y1,y2);
		}
		
		
		public bool SameSideOfLine(Vector4 p1, Vector4 p2, Vector4 line_p1, Vector4 line_p2)
		{
			Vector4 line = line_p1 - line_p2;
			Vector4 cp1 = line % (p1 - line_p1); //cross product
			Vector4 cp2 = line % (p2 - line_p1); //cross product 
			return (cp1 * cp2 > 0); //dot product - do cp point in same direction?
		}
	}
	
	public class ParticleGroupTriangle : ParticleGroup
	{
		public ParticleGroupTriangle(double in_size, int system_step) : base(in_size, system_step)
		{
			Particle a;
			a = AddParticle(Brushes.Blue, rnd.Next() + 1);
			a.position = new Vector4(400,80,0,0);
			a = AddParticle(Brushes.Red, rnd.Next() + 2);
			a.position = new Vector4(400,50,0,0);
			a = AddParticle(Brushes.Green, rnd.Next() + 3);
			a.position = new Vector4(430,80,0,0);

			AddPair(0,1);
			AddPair(1,2);
			AddPair(2,0);
		}
		
		public bool is_inside(Vector4 p)
		{
			return SameSideOfLine(p, particles[2].q, particles[0].q, particles[1].q)
				&& SameSideOfLine(p, particles[0].q, particles[1].q, particles[2].q)
				&& SameSideOfLine(p, particles[1].q, particles[2].q, particles[0].q);
		}
		
		
		public override void ProjectCollisionConstraint(Particle p)
		{
			if (is_inside(p.q)) //ak je q vnutri trojuholniku
			{
				
				Vector4 line_normal;
				Vector4 line_intersection;
				Particle a = null, b = null;
				if(!SameSideOfLine(p.q, p.position, particles[0].q, particles[1].q))
				{
					a = particles[0];
					b = particles[1];
				}
				else if(!SameSideOfLine(p.q, p.position, particles[1].q, particles[2].q))
				{
					a = particles[1];
					b = particles[2];
				}
				else if(!SameSideOfLine(p.q, p.position, particles[2].q, particles[0].q))
				{
					a = particles[2];
					b = particles[0];
				}
				else{
					// fallback na staticky collision resolving
					List<Particle> origins = new List<Particle>();
					List<Particle> targets = new List<Particle>();
					origins.Add(particles[0]);
					targets.Add(particles[1]);
					
					origins.Add(particles[1]);
					targets.Add(particles[2]);
					
					origins.Add(particles[2]);
					targets.Add(particles[0]);
					
					var pairs = origins.Zip(targets, (origin, target) => new { Origin = origin, Target = target});
					
					double prev_min = Double.MaxValue;
					foreach(var ot in pairs)
					{
						Vector4 line = ot.Target.q - ot.Origin.q;
						double displacement_len = (((p.q - ot.Origin.q)*line.unit_vector()*line.unit_vector())-line).Length;
						
						if (displacement_len<prev_min)
						{
							prev_min = displacement_len;
							a = ot.Target;
							b = ot.Origin;
						}
					}
				}
				if (a == null)
				{throw new NotImplementedException();}
				
				Vector4 l = (a.q - b.q);
				line_intersection = l % (p.position-p.q);
				line_normal = l.normal_2d();
				
				Vector4 gradient = p.q ^ line_normal;
				Vector4 delta_p = -(((p.q - line_intersection)*line_normal)/(gradient * gradient)) * gradient *2.1;
				double total_w = p.w + a.w + b.w;
				p.q += delta_p*(p.w/total_w);
				a.q += -(a.w/total_w)*delta_p;
				b.q += -(b.w/total_w)*delta_p;
			}

		}
	}
	
	public class ParticleGroupRectangle : ParticleGroup
	{
		public ParticleGroupRectangle(double in_size, int system_step) : base(in_size, system_step)
		{
			AddParticle(Brushes.Blue, rnd.Next() + 1);
			AddParticle(Brushes.Red, rnd.Next() + 2);
			AddParticle(Brushes.Green, rnd.Next() + 3);
			AddParticle(Brushes.Green, rnd.Next() + 4);

			AddPair(0,1);
			AddPair(1,2);
			AddPair(2,3);
			AddPair(3,0);
			
			double uhlopriecka =  Math.Sqrt(2*size*size);
			AddPair(0,2,uhlopriecka);
			AddPair(1,3, uhlopriecka);
		}
		
		public void ProjectCollisionConstraint(Particle p)
		{
			throw new NotImplementedException();
		}
	}
}
