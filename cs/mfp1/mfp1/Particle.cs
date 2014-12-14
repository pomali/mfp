/*
 * Created by SharpDevelop.
 * User: karci
 * Date: 12/13/2014
 * Time: 9:59 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq.Expressions;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace mfp1
{
	/// <summary>
	/// Description of Particle.
	/// </summary>
	public class Particle:ModelVisual3D
	{
		double mass = 1; 
		Vector3D velocity;
		Vector3D position;
		const Vector3D shift_A0 = new Vector3D(0.05,0.05,0.05);
		const Vector3D shift_A1 = new Vector3D(0.1,0,0);
		const Vector3D shift_A2 = new Vector3D(0,0.1,0);
		const Vector3D shift_A3 = new Vector3D(0,0,0.1);
		
		public Particle()
		{
			velocity = new Vector3D(0,0,0);
			position = new Vector3D(10,10,10);
		}
		
		
		public void draw(Viewport3D view)
		{
			MeshGeometry3D repr = new MeshGeometry3D();
			// Vrcholy
			repr.Positions.Add(this.position + shift_A0);
			repr.Positions.Add(this.position + shift_A1);
			repr.Positions.Add(this.position + shift_A2);
			repr.Positions.Add(this.position + shift_A3);
			
			// Steny
			repr.TriangleIndices.Add(0);
			repr.TriangleIndices.Add(1);
			repr.TriangleIndices.Add(2);
			
			repr.TriangleIndices.Add(0);
			repr.TriangleIndices.Add(1);
			repr.TriangleIndices.Add(3);
			
			repr.TriangleIndices.Add(0);
			repr.TriangleIndices.Add(2);
			repr.TriangleIndices.Add(3);
			
			repr.TriangleIndices.Add(1);
			repr.TriangleIndices.Add(2);
			repr.TriangleIndices.Add(3);
			
		}
	}
}
