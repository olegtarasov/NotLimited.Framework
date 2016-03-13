using System;
using Microsoft.Win32;

namespace NotLimited.Framework.Wpf
{
	public static class FileDialogHelpers
	{
		public static void LoadFiles(Action<string[]> action, string filter)
		{
			var dlg = new OpenFileDialog
			{
				Filter = filter,
				Multiselect = true
			};
			if (dlg.ShowDialog() == true && dlg.FileNames.Length > 0)
			{
				action(dlg.FileNames);
			}
		}

		public static void LoadFile(Action<string> action, string filter)
		{
			var dlg = new OpenFileDialog
			{
				Filter = filter,
			};
			if (dlg.ShowDialog() == true && !string.IsNullOrEmpty(dlg.FileName))
			{
				action(dlg.FileName);
			}
		}

		public static void SaveFile(Action<string> action, string filter)
		{
			var dlg = new SaveFileDialog
			{
				Filter = filter,
			};
			if (dlg.ShowDialog() == true && !string.IsNullOrEmpty(dlg.FileName))
			{
				action(dlg.FileName);
			}
		}
	}
}