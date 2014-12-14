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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace mfp1
{
	/// <summary>
	/// Description of Particle.
	/// </summary>
	public class Particle
	{
		double mass = 1; 
		Vector velocity;
		Vector position;
		
		public Particle()
		{
			velocity = new Vector(1,1);
			position = new Vector(1,1);
		}
		
		
		public void draw(Canvas c)
		{
			update();
			Ellipse circle = new Ellipse();
			SolidColorBrush brush = new SolidColorBrush();
			brush.Color = Color.FromArgb(255,225,255,0);
			circle.Fill = brush;
			circle.StrokeThickness = 1;
			circle.Stroke = Brushes.Black;
			circle.Width = 10;
			circle.Height = 10;
			
			c.Children.Add(circle);
			
		}
		
		public void update()
		{
			position = position + velocity;
			MessageBox.Show(position.ToString());
		}
	}
}
