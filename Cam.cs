using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;

public class Cam
{
	public static void Capture(string filePath)
	{
		var devices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

		if (devices.Count == 0)
		{
			Console.WriteLine(" амера не найдена!");
			return;
		}

		var cam = new VideoCaptureDevice(devices[0].MonikerString);
		Bitmap lastFrame = null;

		AutoResetEvent frameReceived = new AutoResetEvent(false);

		cam.NewFrame += (sender, args) =>
		{
			lastFrame?.Dispose();
			lastFrame = (Bitmap)args.Frame.Clone();
			frameReceived.Set();
		};

		cam.Start();

		//ожидание камеры секунд
		if (frameReceived.WaitOne(10000))
		{
			lastFrame.Save(filePath, ImageFormat.Jpeg);
		}
		else
		{
			
		}

		cam.SignalToStop();
		cam.WaitForStop();
		lastFrame?.Dispose();
	}
}
