using System.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using DataTable = System.Data.DataTable;
using File = System.IO.File;

namespace TelegramBot
{
    class Program
    {
        static TelegramBotClient Bot = new TelegramBotClient("6225563156:AAEb2E51Qyb4P5JxbJA3G0kFPWPaZGh6kuQ");
        static string filePath = "../Users/lengo/Pictures/Saved Pictures/ngon-ngu-meo.jpg";
        static string destinationPath = "../BUCA/demo/TelegramBot/TelegramBot/demo.json";
        static string fileName = "demo.xlsx";

        private static List<Book> lstBooks = new List<Book>()
        {
            new Book { Id = 1, Name = "name 1", Author = "author 1" },
            new Book { Id = 2, Name = "name 2", Author = "author 2" },
            new Book { Id = 3, Name = "name 3", Author = "author 3" },
        };

        static void Main(string[] args)
        {
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = new UpdateType[]
                {
                    UpdateType.Message,
                    UpdateType.EditedMessage,
                }
            };

            Bot.StartReceiving(UpdateHandler, ErrorHandler, receiverOptions);

            Console.ReadLine();
        }

        private static async Task ErrorHandler(ITelegramBotClient botClient, Exception e,
            CancellationToken cancellationToken)
        {
            Console.WriteLine(e.Message);
        }

        private static async Task UpdateHandler(ITelegramBotClient bot, Update update, CancellationToken arg3)
        {
            if (update.Type == UpdateType.Message)
            {
                if (update.Message.Type == MessageType.Text)
                {
                    //write an update
                    var _botUpdate = new BotUpdate
                    {
                        Text = update.Message.Text,
                        Id = update.Message.Chat.Id,
                        Username = update.Message.Chat.Username
                    };
                    Console.WriteLine(_botUpdate.Username + ": " + _botUpdate.Text);
                    if (_botUpdate.Text.StartsWith("/count"))
                    {
                        var txt = _botUpdate.Text.ToString()[5..].Trim() ?? "";
                        await Bot.SendTextMessageAsync(_botUpdate.Id, txt.Length.ToString());
                    }
                    else if (_botUpdate.Text.StartsWith("/file"))
                    {
                        Console.WriteLine("Send File");
                        await Bot.SendPhotoAsync(_botUpdate.Id,
                            new InputFileUrl(
                                "https://thinksaveretire.com/wp-content/uploads/2019/01/money-memes-12.jpg"));
                    }
                    else if (_botUpdate.Text.StartsWith("/report"))
                    {
                        try
                        {
                            SaveToFile();
                            await using Stream stream = System.IO.File.OpenRead(fileName);
                            await Bot.SendDocumentAsync(_botUpdate.Id,
                                InputFile.FromStream(stream: stream, fileName: "hamlet.xlsx"));
                        }
                        finally
                        {
                            if(File.Exists(fileName))
                            {
                                try
                                {
                                    File.Delete(fileName);
                                } 
                                catch(Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                } 
                            }  
                        }
                    }
                    else
                    {
                        await Bot.SendTextMessageAsync(_botUpdate.Id, "Hello World");
                    }
                }
            }
        }
    }

    public class BotUpdate
    {
        public string Text { get; set; }
        public long Id { get; set; }
        public string? Username { get; set; }
    }

    
}