using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using CSSParser;
using CSSParser.ExtendedLESSParser;
using CSSParser.ExtendedLESSParser.ContentSections;
using DynamicExpresso;
using Control = System.Windows.Forms.Control;
using Selector = CSSParser.ExtendedLESSParser.ContentSections.Selector;
namespace CSS.NET
{
	public static class CssManager
	{
		/// <summary>
		/// Apply a CSS string to a Window.
		/// </summary>
		/// <param name="window">The window to apply to.</param>
		/// <param name="cssString">The CSS content.</param>
		public static void ApplyWindowCss(this Window window, string cssString)
		{
			ApplyInternal(window, cssString);
		}

        /// <summary>
        /// Apply styles from a css files
        /// </summary>
        /// <param name="window">The window to apply to.</param>
        /// <param name="cssContent">CSS file where styles are held</param>
        public static async Task ApplyWindowCss(this Window window, FileStream cssContent)
	    {
	        if (Path.GetExtension(cssContent.Name) != "css")
	        {
	            return;
	        }
	        using (var reader = new StreamReader(cssContent))
	            ApplyWindowCss(window, await reader.ReadToEndAsync());
	    }

        /// <summary>
        /// Apply a CSS string to a Form.
        /// </summary>
        /// <param name="form">The form to apply to.</param>
        /// <param name="cssString">The CSS content.</param>
        public static void ApplyFormCss(this Form form, string cssString)
		{
			ApplyInternal(form, cssString);
		}

        /// <summary>
        /// Apply styles from a css files
        /// </summary>
        /// <param name="form">From where styles will be applied</param>
        /// <param name="cssContent">CSS file where styles are held</param>
	    public static async Task ApplyFormCss(this Form form, FileStream cssContent)
        {
            if (Path.GetExtension(cssContent.Name) != "css")
            {
                return;
            }
            using (var reader = new StreamReader(cssContent))
                ApplyFormCss(form, await reader.ReadToEndAsync());
        }

		/// <summary>
		/// Apply a CSS string to a control.
		/// </summary>
		/// <param name="parent">The control to apply to.</param>
		/// <param name="cssString">The CSS content.</param>
		private static void ApplyInternal(object parent, string cssString)
		{
			var result = LessCssHierarchicalParser.ParseIntoStructuredData(Parser.ParseLESS(cssString));
			foreach (Selector fragment in result)
			{
				foreach (ContainerFragment.WhiteSpaceNormalisedString selector in fragment.Selectors)
				{
					foreach (var item in fragment.ChildFragments)
					{
						var controls = GetControlsBySelector(parent, selector.Value);
						foreach (object control in controls)
						{
							if (control != null && item is StylePropertyValue styleProp)
							{
								SetProp(control, styleProp.Property.Value, styleProp.ValueSegments.Aggregate((x, y) => x + " " + y));
							}
						}
					}
				}
				
				void SetProp(object target, string property, string valueString)
				{
					if (valueString == "") return;
					valueString = valueString.Trim();
					var prop = target.GetType().GetProperty(property, BindingFlags.Public | BindingFlags.Instance);
					object value;
					if (prop == null)
					{
						return;
					}
					var eval = new Interpreter();
					eval.Reference(prop.PropertyType);
					value = eval.Eval(valueString);
					prop.SetValue(target, value);
				}
			}
		}

		private static List<object> GetControlsBySelector(object parent, string selector)
		{
			List<object> controls = new List<object>();
			var hierarchy = selector.Split(' ');
			bool immediateChild = false;
			foreach (string item in hierarchy)
			{
				controls.Clear();
				if (parent == null)
				{
					return controls;
				}
				if (item == ">")
				{
					immediateChild = true;
					continue;
				}
				if (item.StartsWith("."))
				{
					throw new NotSupportedException("Class selectors are not supported.");
				}
				if (item.StartsWith("#"))
				{
					if (parent is Control winFormsControl)
					{
						parent = winFormsControl.Controls.Find(item.Remove(0, 1), true).FirstOrDefault();
						controls.Add(parent);
					}
					else if (parent is System.Windows.Controls.Control wpfControl)
					{
						parent = wpfControl.FindName(item.Remove(0, 1));
						controls.Add(parent);
					}
				}
				else
				{
					IEnumerable<object> children = new List<object>();
					if (parent is Control winFormsControl)
					{
						children = winFormsControl.Controls.OfType<Control>();
					}
					else if (parent is System.Windows.Controls.Control wpfControl)
					{
						children = wpfControl.GetChildren(false);
					}
					foreach (object control in children)
					{
						Type compare = control.GetType();
						while (compare != null)
						{
							if (compare.Name == item)
							{
								parent = control;
								controls.Add(control);
								goto exitloop;
							}
							compare = compare.BaseType;
						}
						
						if (!immediateChild)
						{
							controls.AddRange(GetControlsBySelector(control, item));
						}
						exitloop:;
					}
				}
				immediateChild = false;
			}
			return controls;
		}
	}
}
