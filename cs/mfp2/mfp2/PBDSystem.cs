/*
 * Created by SharpDevelop.
 * User: karci
 * Date: 12/14/2014
 * Time: 4:11 PM
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
	/// Description of PBDSystem.
	/// </summary>
	public class PBDSystem
	{
		List<ParticleGroup> particle_groups = new List<ParticleGroup>();
		static Vector4 g_acceleration = new Vector4(0,9.81,0,0);
		double limit_Y = 500;
		public PBDSystem()
		{
		}
		
		public void Draw(Graphics g)
		{
			foreach (ParticleGroup x in particle_groups)
            {
            	x.Draw(g);
            }
			
			g.DrawLine(Pens.Black,0,(float)limit_Y,800,(float)limit_Y);
		}
		public void Update()
		{
			double dt = 0.01;
			double kd = 0.001;
			double lifetime = 50;
			int ns = 10;
			
			List<ParticleGroup> to_remove = new List<ParticleGroup>();
			foreach (ParticleGroup x in particle_groups)
            {
				if (x.born < DateTime.Now.AddSeconds(-lifetime))
				{
					to_remove.Add(x);
				}
			}
			
			foreach (ParticleGroup x in to_remove)
			{
				particle_groups.Remove(x);
			}
			
			//3: external forces
			foreach (ParticleGroup x in particle_groups)
            {
				foreach(Particle p in x.particles)
				{
					p.velocity += dt * p.w * g_acceleration;
				}
            }
			
			//4: damp velocities
			foreach (ParticleGroup x in particle_groups)
            {
				foreach(Particle p in x.particles)
				{
					//p.velocity += kd * x.velocity +  - p.velocity;
					p.velocity -= kd * p.velocity;
				}
            }
			
			//5: predict positions (simple euler)
			foreach (ParticleGroup x in particle_groups)
            {
				foreach(Particle p in x.particles)
				{
					p.q = p.position + dt * p.velocity;
					p.position = p.q;
				}
            }
			
//			//6: detect and construct collision constraints
			foreach (ParticleGroup x in particle_groups)
            {
				foreach(Particle p in x.particles)
				{
					p.q = p.position + dt * p.velocity;
				}
            }
			
			
			//7: apply "projection" several times on all constraints
			for (int i=0; i<ns; i++)
			{
				foreach (ParticleGroup x in particle_groups)
	            {
					// DistanceConstraints of springs
					x.ProjectDistanceConstraints();
					x.ProjectFloorConstraints(limit_Y);
	            }
			}
			
			//8: find correct velocities
			foreach (ParticleGroup x in particle_groups)
            {
				foreach(Particle p in x.particles)
				{
					p.velocity = (1/dt)*(p.q - p.position);
					p.position = p.q;
				}
            }
			
			
			//9: apply friction and resistution impulses on velocities
		}
		
		public void Spawn()
		{
			particle_groups.Add(new ParticleGroup());
		}
	}
}
