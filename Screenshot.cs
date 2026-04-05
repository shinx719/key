using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

public class Screenshot
{
	[DllImport("user32.dll")]
	private static extern IntPtr GetDesktopWindow();

	[DllImport("user32.dll")]
	private static extern IntPtr GetWindowDC(IntPtr ptr);

	[DllImport("user32.dll")]
	private static extern int ReleaseDC(IntPtr ptr, IntPtr dc);

	[DllImport("gdi32.dll")]
	private static extern bool BitBlt(IntPtr hObject, int nXDest,
		int nYDest, int nWidth, int nHeight, IntPtr hObjectSource,
		int nXSrc, int nYSrc, int dwRop);

	private const int SRCCOPY = 0x00CC0020;

	public static void Capture(string filePath)
	{
		IntPtr desktopWnd = GetDesktopWindow();
		IntPtr desktopDC = GetWindowDC(desktopWnd);
		Graphics g = Graphics.FromHdc(desktopDC);

		int width = ScreenWidth();
		int height = ScreenHeight();

		Bitmap bmp = new Bitmap(width, height);
		Graphics memoryG = Graphics.FromImage(bmp);
		IntPtr hDC = g.GetHdc();
		IntPtr hMemDC = memoryG.GetHdc();

		BitBlt(hMemDC, 0, 0, width, height, hDC, 0, 0, SRCCOPY);

		g.ReleaseHdc(hDC);
		memoryG.ReleaseHdc(hMemDC);

		ReleaseDC(desktopWnd, desktopDC);

		bmp.Save(filePath, ImageFormat.Png);
	}

	private static int ScreenWidth() => GetSystemMetrics(0);
	private static int ScreenHeight() => GetSystemMetrics(1);

	[DllImport("user32.dll")]
	private static extern int GetSystemMetrics(int nIndex);
}
