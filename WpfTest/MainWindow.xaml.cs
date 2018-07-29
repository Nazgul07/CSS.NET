using System.Windows;
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
