using System.Collections.Generic;
using System.Windows.Media;

namespace CSS.NET
{
	public static class WpfControlExtensions
	{
		public static IEnumerable<Visual> GetChildren(this Visual parent, bool recurse = true)
		{
			if (parent != null)
			{
				int count = VisualTreeHelper.GetChildrenCount(parent);
				for (int i = 0; i < count; i++)
				{
					// Retrieve child visual at specified index value.

				    if (VisualTreeHelper.GetChild(parent, i) is Visual child)
					{
						yield return child;

						if (recurse)
						{
							foreach (var grandChild in child.GetChildren(true))
							{
								yield return grandChild;
							}
						}
					}
				}
			}
		}
	}
}
