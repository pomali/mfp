/*
 * Created by SharpDevelop.
 * User: pom
 * Date: 12/13/2014
 * Time: 12:00 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace mfp1
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		Particle x;
		public Window1()
		{
			InitializeComponent();
			x = new Particle();
		}
		
		void button1_Click(object sender, RoutedEventArgs e)
		{
			//MessageBox.Show("Hi there!");
			x.update();
			x.draw(mainCanvas);
		}
	}
}