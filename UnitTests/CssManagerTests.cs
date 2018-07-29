using System.Drawing;
using System.Windows.Forms;
using CSS.NET;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
	public class CssManagerTests
	{
		[TestMethod]
		public void ApplyTestBasicNames()
		{
			var form = new Form();
			Button button = new Button() {Name = "button1"};
			form.Controls.Add(button);
			Label label = new Label() {Name = "label1"};
			form.Controls.Add(label);
			form.ApplyFormCss(@"
			#button1 {
				Height:50;
				Width:200;
				Text: ""test"";
			}
			#label1 {
				ForeColor: Color.Red;
				Text: ""test"";
				Location: new Point(10, 10);
			}
			");
			Assert.AreEqual(50, button.Height, "Height should be 50");
			Assert.AreEqual(200, button.Width, "Width should be 200");
			Assert.AreEqual("test", button.Text, "Text should be 'test'");
			Assert.AreEqual("test", label.Text, "Text should be 'test'");
			Assert.AreEqual(Color.Red, label.ForeColor, "Color should be red");
			Assert.AreEqual(new Point(10, 10), label.Location, "Locaton should be '10, 10'");
		}

		[TestMethod]
		public void ApplyTestBasicTypes()
		{
			var form = new Form();
			var panel = new Panel();
			Button button = new Button() { Name = "button1" };
			panel.Controls.Add(button);
			Label label = new Label() { Name = "label1" };
			panel.Controls.Add(label);
			form.Controls.Add(panel);
			form.ApplyFormCss(@"
			Panel > Button {
				Height:50;
				Width:200;
				Text: ""test"";
			}
			Label {
				ForeColor: Color.FromArgb(255,255,0,0);
				Text: ""test"";
				Location: new Point(10, 10);
			}
			");
			
			Assert.AreEqual(50, button.Height, "Height should be 50");
			Assert.AreEqual(200, button.Width, "Width should be 200");
			Assert.AreEqual("test", button.Text, "Text should be 'test'");
			Assert.AreEqual("test", label.Text, "Text should be 'test'");
			Assert.AreEqual(Color.FromArgb(255, 255, 0, 0), label.ForeColor, "Color should be red");
			Assert.AreEqual(new Point(10, 10), label.Location, "Locaton should be '10, 10'");
		}
	}
}
