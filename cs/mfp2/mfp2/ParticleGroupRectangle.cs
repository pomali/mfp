/*
 * Created by SharpDevelop.
 * User: pom
 * Date: 1/26/2015
 * Time: 10:31 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace mfp2
{
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
		
		
		public Vector4 cog()
		{
			return (1/4)*(particles[0].q +particles[1].q +particles[2].q +particles[3].q);
		}
		
		public bool is_inside(Vector4 p)
		{
			bool ret = true;
			Vector4 c = cog();
			foreach(ParticlePair pair in particle_pairs.Take(4))
			{
				ret &= SameSideOfLine(p, c, pair.a.q, pair.b.q);
			}
			return ret;
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
					double prev_min = Double.MaxValue;
					foreach(ParticlePair pair in particle_pairs.Take(4))
					{
						Vector4 line = pair.a.q - pair.b.q;
						Vector4 displacement = (((p.q - pair.b.q)*line.unit_vector())*line.unit_vector())-(p.q-pair.b.q);
						if (displacement.W != 0 || displacement.Z != 0)
							throw new NotImplementedException();
						double displacement_len = displacement.Length;
						
						if (displacement_len<prev_min)
						{
							prev_min = displacement_len;
							line_intersection = p.q+displacement;
							a = pair.a;
							b = pair.b;
						}
					}
				}
				else
				{
					foreach(ParticlePair pair in particle_pairs.Take(4)){
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
				
				double total_w = p.w + a.w + b.w;
				double a_l = (a.q-line_intersection).Length;
				double b_l = (b.q-line_intersection).Length;
				double total_l = a_l + b_l;
//				p.q += (p.w/total_w)*diff;
//				a.q += -1*(1-(p.w/total_w))*diff;//-1*(a.w/total_w)*(a_l/total_l)*diff;
//				b.q += -1*(1-(p.w/total_w))*diff;//-1*(b.w/total_w)*(b_l/total_l)*diff;
			}

		}
	}
}
