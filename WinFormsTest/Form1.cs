using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSS.NET;

namespace WinFormsTest
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			this.ApplyFormCss(@"
			#button1 {
				Height:50;
				Width:200;
				Text: ""An awesome button"";
			}
			#label1 {
				ForeColor: Color.Red;
				Text: ""An awesome label"";
				Location: new Point(10, 10);
			}
			");
		}
	}
}
