using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace FlagMiner
{
	public class WindowsApi
	{
		[DllImport("User32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		private static extern bool FlashWindowEx(ref FLASHWINFO fwInfo);

		// As defined by: http://msdn.microsoft.com/en-us/library/ms679347(v=vs.85).aspx
		public enum FlashWindowFlags : uint
		{
			// Stop flashing. The system restores the window to its original state.
			FLASHW_STOP = 0,
			// Flash the window caption.
			FLASHW_CAPTION = 1,
			// Flash the taskbar button.
			FLASHW_TRAY = 2,
			// Flash both the window caption and taskbar button.
			// This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags.
			FLASHW_ALL = 3,
			// Flash continuously, until the FLASHW_STOP flag is set.
			FLASHW_TIMER = 4,
			// Flash continuously until the window comes to the foreground.
			FLASHW_TIMERNOFG = 12
		}

		public struct FLASHWINFO
		{
			public UInt32 cbSize;
			public IntPtr hwnd;
			public FlashWindowFlags dwFlags;
			public UInt32 uCount;
			public UInt32 dwTimeout;
		}

		public static bool FlashWindow( IntPtr handle, bool FlashTitleBar, bool FlashTray, int FlashCount)
		{
			if (handle == null)
				return false;

			try {
				FLASHWINFO fwi = new FLASHWINFO();
				var _with1 = fwi;
				_with1.hwnd = handle;
				if (FlashTitleBar)
					_with1.dwFlags = _with1.dwFlags | FlashWindowFlags.FLASHW_CAPTION;
				if (FlashTray)
					_with1.dwFlags = _with1.dwFlags | FlashWindowFlags.FLASHW_TRAY;
				_with1.uCount = Convert.ToUInt32(FlashCount);
				if (FlashCount == 0)
					_with1.dwFlags = _with1.dwFlags | FlashWindowFlags.FLASHW_TIMERNOFG;
				_with1.dwTimeout = 0;
				// Use the default cursor blink rate.
				_with1.cbSize = Convert.ToUInt32(System.Runtime.InteropServices.Marshal.SizeOf(fwi));

				return FlashWindowEx(ref fwi);
			} catch {
				return false;
			}
		}
	}
}
