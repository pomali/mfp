﻿/*
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
			return x1 <= p.X && p.X <= x2 && y1 <= p.Y && p.Y <= y2;
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
			Draw(g,Pens.SlateGray);
		}
		
		public void Draw(Graphics g, Pen p)
		{
			g.DrawLine(p, (float)a.position.X, (float)a.position.Y, (float)b.position.X, (float)b.position.Y);
		}
		
		public void ProjectDistanceConstraints(double in_k)
		{
			double k = 1;
			double s = (distance - L)/distance;
			a.q += (((-a.w)/(w_total))*s*vect) * in_k * k;
			b.q += (((b.w)/(w_total))*s*vect) * in_k * k;
		}
		
		public void ProjectFloorConstraints(double limitX, double limitY, double in_k)
		{
			// chcem aby bola Y suradnica < limit (top je 0)
			// teda chcem p1.Y - limit < 0

			//Y zdola
			double ca = a.q.Y - limitY;
			double cb = b.q.Y - limitY;
			if (ca >= 1)
			{
				a.q = a.q + ((a.position - a.q) * (ca/Math.Abs(a.position.Y - a.q.Y)));
			}
			
			if (cb >= 1)
			{
				b.q = b.q + ((b.position - b.q) * (cb/Math.Abs(b.position.Y - b.q.Y)));
				//b.q.Y = b.q.Y - cb - 1;
			}
			
			//Y zhora
			 ca = -a.q.Y;
			 cb = -b.q.Y;
			if (ca >= 1)
			{
				a.q = a.q + ((a.position - a.q) * (ca/Math.Abs(a.position.Y - a.q.Y)));
			}
			
			if (cb >= 1)
			{
				b.q = b.q + ((b.position - b.q) * (cb/Math.Abs(b.position.Y - b.q.Y)));
				//b.q.Y = b.q.Y - cb - 1;
			}
			
			//X zlava
			 ca = -a.q.X;
			 cb = -b.q.X;
			if (ca >= 1)
			{
				a.q = a.q + ((a.position - a.q) * (ca/Math.Abs(a.position.X - a.q.X)));
			}
			
			if (cb >= 1)
			{
				b.q = b.q + ((b.position - b.q) * (cb/Math.Abs(b.position.X - b.q.X)));
				//b.q.Y = b.q.Y - cb - 1;
			}
			
			//X sprava
			 ca = a.q.X-limitX;
			 cb = b.q.X-limitX;
			if (ca >= 1)
			{
				 a.q = a.q + ((a.position - a.q) * (ca/Math.Abs(a.position.X - a.q.X)));
			}
			
			if (cb >= 1)
			{
				b.q = b.q + ((b.position - b.q) * (cb/Math.Abs(b.position.X - b.q.X)));
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
		
		// Vykrelsovacie pomocne veci
		public List<Vector4> intersections = new List<Vector4>();
		public List<Vector4> collision_normals = new List<Vector4>();
		public List<Vector4> collision_vectors = new List<Vector4>();
		public bool colliding;
		
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
				if (colliding)
				{
					p.Draw(g,Pens.Red);
				}
				else{
					p.Draw(g);
				}
			}
			
			
			var ic = intersections.Zip(collision_normals, (i, n) => new { Intersection = i, CNormal = n});
			foreach(var k in ic){
				Vector4 intersection = k.Intersection;
				g.DrawRectangle(Pens.CadetBlue, (float)intersection.X-2, (float)intersection.Y-2, 4 , 4);
				Vector4 v  = intersection + k.CNormal;
				g.DrawLine(Pens.CadetBlue, (float)intersection.X, (float)intersection.Y, (float)v.X, (float)v.Y);
			}
			
			ic = intersections.Zip(collision_vectors, (i, n) => new { Intersection = i, CNormal = n});
			foreach(var k in ic){
				Vector4 intersection = k.Intersection;
				Vector4 v  = intersection + k.CNormal;
				g.DrawLine(Pens.Cyan, (float)intersection.X, (float)intersection.Y, (float)v.X, (float)v.Y);
			}
			
			intersections.Clear();
			collision_normals.Clear();
			collision_vectors.Clear();
			colliding = false;
		}
		
		public void ProjectDistanceConstraints(double in_k)
		{
			foreach(ParticlePair p in particle_pairs)
			{
				p.ProjectDistanceConstraints(in_k);
			}
		}
		
		public void ProjectFloorConstraints(double limitX, double limitY, double in_k)
		{
			foreach(ParticlePair p in particle_pairs)
			{
				p.ProjectFloorConstraints(limitX, limitY, in_k);
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
				Vector4 center = new Vector4(0,0,1,1);
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
				x1 = Math.Min(x1, p.q.X);
				x2 = Math.Max(x2, p.q.X);
				y1 = Math.Min(y1, p.q.Y);
				y2 = Math.Max(y2, p.q.Y);
			}
			
			return new AABBox(x1,x2,y1,y2);
		}
		
		
		public bool SameSideOfLine(Vector4 p1, Vector4 p2, Vector4 line_p1, Vector4 line_p2)
		{
			
			//throw new NotImplementedException();
			Vector4 line = line_p1 - line_p2;
			Vector4 cp1 = line % (p1 - line_p1); //cross product FIXME?
			Vector4 cp2 = line % (p2 - line_p1); //cross product FIXME?
			return (cp1 * cp2 > 0); //dot product - do cp point in same direction?
			
			
		}
	}
	
	public class ParticleGroupTriangle : ParticleGroup
	{
		public ParticleGroupTriangle(double in_size, int system_step) : base(in_size, system_step)
		{
			Particle a;
			a = AddParticle(Brushes.Blue, rnd.Next() + 1);
			a.position = new Vector4(400,80,1,1);
			a = AddParticle(Brushes.Red, rnd.Next() + 2);
			a.position = new Vector4(385,106,1,1);
			a = AddParticle(Brushes.Green, rnd.Next() + 3);
			a.position = new Vector4(370,80,1,1);

			AddPair(0,1);
			AddPair(1,2);
			AddPair(2,0);
		}
		
		public bool is_inside(Vector4 p)
		{
			return SameSideOfLine(p, particles[2].q, particles[0].q, particles[1].q)
				&& SameSideOfLine(p, particles[0].q, particles[1].q, particles[2].q)
				&& SameSideOfLine(p, particles[1].q, particles[2].q, particles[0].q);
			
//			
//			Vector4 v0 = particles[1].q - particles[0].q;
//			Vector4 v1 = particles[2].q - particles[0].q;
//			Vector4 v2 = p - particles[0].q;
//			
//			double dot00 = v0*v0;
//			double dot01 = v0*v1;
//			double dot02 = v0*v2;
//			double dot11 = v1*v1;
//			double dot12 = v1*v2;
//			
//			// Compute barycentric coordinates
//			double invDenom = 1 / (dot00 * dot11 - dot01 * dot01);
//			double u = (dot11 * dot02 - dot01 * dot12) * invDenom;
//			double v = (dot00 * dot12 - dot01 * dot02) * invDenom;
//			
//			// Check if point is in triangle
//			return (u >= 0) && (v >= 0) && ( 1 - (u + v) >= 0);
		}
		
		
		public override void ProjectCollisionConstraint(Particle p)
		{
			if (is_inside(p.q)) //ak je q vnutri trojuholniku
			{
				colliding = true;
				Vector4 line_normal;
				Vector4 line_intersection = new Vector4();
				Particle a = null, b = null;
				if (is_inside(p.position))
				{
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
						Vector4 displacement = (((p.q - ot.Origin.q)*line.unit_vector())*line.unit_vector())-(p.q-ot.Origin.q); // FIXME
						if (displacement.W != 0 || displacement.Z != 0)
							throw new NotImplementedException();
						double displacement_len = displacement.Length;
						
						if (displacement_len<prev_min)
						{
							prev_min = displacement_len;
							line_intersection = p.q+displacement;
							a = ot.Target;
							b = ot.Origin;
						}
					}
				}
				else
				{
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
					Vector4 r,s,qp;
					double u,t,rxs;
					
					r = (a.q - b.q);
					s = (p.position - p.q);
					qp = (p.q - b.q);
					rxs = r.X*s.Y - r.Y*s.X;
					if (rxs==0)
					{
						return; //su kolinearne alebo paralelne
					}
					
					t = (qp.X*s.Y - qp.Y*s.X)/rxs;
					u = (qp.X*r.Y - qp.Y*r.X)/rxs;
					if ((0<=t && t<=1) && (0<=u && u<=1)){
						line_intersection = b.q + t*r;
					}
					else{
						return;
					}
				}
				if (a == null)
					{throw new NotImplementedException();}
				
				
				
				line_normal = (a.q-b.q).normal_2d();
				if (line_normal * (p.q - b.q)> 0)
				{
					line_normal = -1*line_normal;
				}
					
				intersections.Add(line_intersection);
				collision_normals.Add(line_normal);
				collision_vectors.Add(p.q-line_intersection);
				
				
				Vector4 gradient = (p.q) ^ line_normal;
				
				Vector4 mv1 = (p.q - line_intersection);
				double mv2 = (mv1*line_normal);
				double mv3 = (gradient * gradient);
				double mv4 = (mv2/mv3);
				Vector4 delta_p = - mv4 * gradient;
				double total_w = p.w + a.w + b.w;
				
				delta_p = (line_intersection-p.q);
				p.q += (p.w/total_w)*delta_p;
				a.q += -1*(a.w/total_w)*delta_p;
				b.q += -1*(b.w/total_w)*delta_p;
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
		
		public override void ProjectCollisionConstraint(Particle p)
		{
			throw new NotImplementedException();
		}
	}
}
