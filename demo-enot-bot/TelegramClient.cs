using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Diagnostics;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace demo_enot_bot
{
    class TelegramClient : IBotClient
    {
        TelegramBotClient bot;

        public TelegramClient()
        {
            WebProxy proxy = new WebProxy("127.0.0.1", 4711);
            bot = new TelegramBotClient("719406679:AAFF-GaeNImGs5xXoGv6ov4VQcMMQYg_n8I", proxy);
            bot.OnMessage += MessageRecieved;
            bot.StartReceiving();
        }

        private void MessageRecieved(object sender, MessageEventArgs e)
        {
            string messageText = e.Message.Text.ToLower().Trim();
            long chatID = e.Message.Chat.Id;
            Debug.WriteLine($"{e.Message.From.Id} - {messageText}");
            SendAnswer(chatID, Program.GetAnswer(messageText));
        }

        public void SendAnswer(long chatID, string Answer)
        {
            bot.SendTextMessageAsync(chatID, Answer);
        }

        public void StopRecieving()
        {
            bot.StopReceiving();
        }
    }
}
