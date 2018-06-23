﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CSS.NET;

namespace WpfTest
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			this.ApplyWindowCss(@"
			#button1 {
				Height:50;
				Width:200;
				Content: ""An awesome button"";
			}
			#label1 {
				ForeColor: Color.Red;
				Content: ""An awesome label"";
				Location: new Point(10, 10);
			}
			");
		}
	}
}
