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
		static Vector4 g_acceleration = new Vector4(0,0.981,0,0);
		public PBDSystem()
		{
		}
		
		public void Draw(Graphics g)
		{
			foreach (ParticleGroup x in particle_groups)
            {
            	x.Draw(g);
            }
		}
		public void Update()
		{
			double dt = 0.01;
			double kd = 0.001;
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
			for (int i=0; i<3; i++)
			{
				foreach (ParticleGroup x in particle_groups)
	            {
					// DistanceConstraints of springs
					x.ProjectDistanceConstraints();
					x.ProjectFloorConstraints();
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
