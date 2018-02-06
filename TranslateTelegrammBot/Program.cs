using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TranslateTelegrammBot
{
    static class Program
    {
        private static readonly TelegramBotClient bot = new TelegramBotClient("510996833:AAGmt1-tqWJmblYPpyuMa8xeY9ZAu73kzNs");
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                bot.OnMessage += Bot_OnMessage;
                bot.SetWebhookAsync("");

                var me = bot.GetMeAsync().Result;

                bot.StartReceiving();

                

                Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());



                bot.StopReceiving();
            }
            catch(Exception ex)
            {
                Form1.logger.Error(ex.StackTrace);
            }
        }
        private static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            Telegram.Bot.Types.Message msg = e.Message;
            if (msg == null)
            {
                return;
            }
            if (msg.Type == Telegram.Bot.Types.Enums.MessageType.TextMessage)
            {
                try
                {

                    //string s = Regex.Split(msg.Text.ToString(), "(?<=[.;!?])")[0];
                    string s =LogicTranslator.PostRequest2(msg.Text).Result;

                    if (s == null) s = "null";
                    await bot.SendTextMessageAsync(msg.Chat.Id, s );

                }
                catch (Exception ex)
                {
                    Form1.logger.Error(ex.StackTrace+ex.Message);
                }
            }
        }
    }
}
