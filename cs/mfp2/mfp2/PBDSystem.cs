﻿/*
 * Created by SharpDevelop.
 * User: pom
 * Date: 12/14/2014
 * Time: 4:11 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;

namespace mfp2
{
	/// <summary>
	/// Description of PBDSystem.
	/// http://matthias-mueller-fischer.ch/publications/posBasedDyn.pdf
	/// </summary>
	/// 
	
	public struct CollisionPair
	{
		public Particle p;
		public ParticleGroup pg;
		public CollisionPair(Particle in_p, ParticleGroup in_pg)
		{
			p = in_p;
			pg = in_pg;
		}
	}
	
	public class PBDSystem
	{
		List<ParticleGroup> particle_groups = new List<ParticleGroup>();
		static Vector4 g_acceleration = new Vector4(0,981000,0,0); // gravitacne zrychlenie
		static int system_step_mod = (int)1e6;
		static int particle_spawn_mod = 40;
		public double limit_Y = 550; // "vyska" podlahy
		public double limit_X = 550; // "sirka"
		int system_step = 0; // aktualny krok systemu (modulo system_step_mod kvoli citatelnosti a podobne)
		public int lifetime = 350; // particle liftime v krokoch systemu
		public bool draw_aabb = true;
		
		public double dt = 3e-10; // krok interpolacie (delta t)
		public double kd = 0.99999;  // velocity damping konstanta, cim mensie tym viac umieraju rychlosti
		public int spring_size = 80; // dlzka springu ktory spawnujeme

		double _kc = 0.99999;   // corrections damping konstanta aka cast korekcie je pouzivana (cim vacsia tym viac sa upravuju)
		public double kc { // corrections damping
			get { return _kc; }
			set { _kc = value; refresh_in_k();}
		}


		int _ns = 1;        // pocet iteracii 
		public int ns { // pocet iteracii
			get { return _ns; }
			set { _ns = value; refresh_in_k();}
		}
		
		public int System_step {
			get { return system_step; }
		}
		
		
		double in_k; // vypocitavany damping
			
		public PBDSystem()
		{
			refresh_in_k();
		}
		
		void refresh_in_k()
		{	
			in_k = 1.0 -  Math.Pow((1.0 - kc), 1.0/ns);	
		}
		
		public void Draw(Graphics g)
		{
			foreach (ParticleGroup x in particle_groups)
            { 
            	x.Draw(g);
            	
            	if (draw_aabb){
					AABBox a = x.aabb();
					g.DrawRectangle(Pens.BurlyWood, (float)a.x1, (float)a.y1, (float)(a.x2-a.x1), (float)(a.y2-a.y1));
            	}
            }
			
			g.DrawLine(Pens.Black,0,(float)limit_Y,g.VisibleClipBounds.Width,(float)limit_Y);
		}
		public void Update()
		{
			system_step = (system_step + 1) % system_step_mod;
			
			if (system_step % particle_spawn_mod == 1){
				Spawn(spring_size);
			}
			
			// 1: Remove old particles
			List<ParticleGroup> to_remove = new List<ParticleGroup>();
			foreach (ParticleGroup x in particle_groups)
            {
				if (Math.Abs((System_step - x.born)%system_step_mod) > lifetime)
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
					p.velocity += dt * p.mass * g_acceleration;
				}
            }
			
			//4: damp velocities
			foreach (ParticleGroup x in particle_groups)
            {
				foreach(Particle p in x.particles)
				{
					//p.velocity += kd * x.velocity +  - p.velocity;
					p.velocity = kd * p.velocity;
				}
            }
			
			//5: predict positions (simple explicit euler)
			foreach (ParticleGroup x in particle_groups)
            {
				foreach(Particle p in x.particles)
				{
					p.q = p.position + dt * p.velocity;
				}
            }
			
			List<CollisionPair> collisions = new List<CollisionPair>();
			//6: detect and construct collision constraints
			foreach (ParticleGroup pg1 in particle_groups)
            {
				AABBox aabb = pg1.aabb();
				foreach(ParticleGroup pg2 in particle_groups)
				{
					if (pg1!=pg2)
					{
						foreach(Particle p in pg2.particles)
						{
							if (aabb.is_inside(p.q))
							{
								collisions.Add(new CollisionPair(p,pg1));
							}
						}
					}
				}
            }
			
			
			//7: apply "projection" several times on all constraints
			for (int i=0; i<ns; i++)
			{
				foreach (ParticleGroup x in particle_groups)
	            {
					// DistanceConstraints of springs
					x.ProjectDistanceConstraints(in_k);
					x.ProjectFloorConstraints(limit_X, limit_Y, in_k);
	            }
				
				foreach(CollisionPair c in collisions)
				{
					c.pg.ProjectCollisionConstraint(c.p);
				}
			}
			
			
			//8: find correct velocities
			foreach (ParticleGroup x in particle_groups)
            {
				foreach(Particle p in x.particles)
				{
					p.velocity = (1.0/dt)*(p.q - p.position);
					p.position = p.q;
				}
            }
			
			//9: apply friction and resistution impulses on velocities
			//no friction or resistution is needed
		}
		
		public void Spawn(double size)
		{
			particle_groups.Add(new ParticleGroupTriangle(size, System_step));
		}
	}
}
