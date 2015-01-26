/*
 * Created by SharpDevelop.
 * User: pom
 * Date: 1/26/2015
 * Time: 10:32 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace mfp2
{
	
	
	public class ParticleGroupTriangle : ParticleGroup
	{
		public ParticleGroupTriangle(double in_size, int system_step) : base(in_size, system_step)
		{
			Particle a;
			a = AddParticle(Brushes.Blue, rnd.Next() + 1);
			a.position = new Vector4(400,80,1,1);
			a = AddParticle(Brushes.Red, rnd.Next() + 2);
			a.position = new Vector4(400-in_size,80,1,1);
			a = AddParticle(Brushes.Green, rnd.Next() + 3);
			a.position = new Vector4(400-(in_size/2),80+(in_size*0.86602540378),1,1);

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
					foreach(ParticlePair pair in particle_pairs){
						if(!SameSideOfLine(p.q, p.position,pair.a.q, pair.b.q))
						{
							a = pair.a;
							b = pair.b;
						}
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
				
				
				Vector4 ab = (a.q-b.q);
				line_normal = ab.normal_2d();
				if (line_normal * (p.q - b.q)> 0)
				{
					line_normal = -1*line_normal;
				}
					
				Vector4 diff = line_intersection-p.q;
				intersections.Add(line_intersection);
				collision_normals.Add(line_normal);
				collision_vectors.Add(diff);
				
				double total_w = p.w + 1/(a.mass + b.mass);
				double a_l = (a.q-line_intersection).Length;
				double b_l = (b.q-line_intersection).Length;
				double total_l = a_l + b_l;
				double point_diff_contrib = (p.w/total_w);
				p.q += point_diff_contrib*diff;
				a.q += -1*(1-point_diff_contrib)*diff;//-1*(a.w/total_w)*(a_l/total_l)*diff;
				b.q += -1*(1-point_diff_contrib)*diff;//-1*(b.w/total_w)*(b_l/total_l)*diff;
			}
		} /*ProjectCollisionConstraint end*/
		
	} /*class end*/
}
