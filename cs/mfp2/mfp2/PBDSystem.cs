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

namespace mfp2
{
	/// <summary>
	/// Description of PBDSystem.
	/// </summary>
	public class PBDSystem
	{
		List<ParticleGroup> particle_groups = new List<ParticleGroup>();
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
			foreach (ParticleGroup x in particle_groups)
            {
				x.Update();
            }
		}
		
		public void Spawn()
		{
			particle_groups.Add(new ParticleGroup());
		}
	}
}
