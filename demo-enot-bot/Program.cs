using System;
using System.Collections.Generic;
using System.Text;
using System.IO; 

namespace demo_enot_bot
{
    class Program
    {
        static Dictionary<string[], string> answers = new Dictionary<string[], string>();
        static Dictionary<string, IBotClient> initializedBots = new Dictionary<string, IBotClient>();

        static void LoadCommandList()
        {
            using (StreamReader sr = new StreamReader(File.Open(@"command-list.txt", FileMode.Open)))
            {
                try
                {
                    while (true)
                    {
                        var keys = sr.ReadLine().ToLower().Trim().Split(' ');
                        var answer = sr.ReadLine();
                        answers.Add(keys, answer);
                    }
                }
                catch
                {
                    Console.WriteLine("answer list is formed");
                }
            }
        }

        static void Main(string[] args)
        {
            LoadCommandList();   

            initializedBots.Add("telegram", new TelegramClient());

            while(true)
            {
                string[] input = Console.ReadLine().ToLower().Trim().Split(' ');

                switch (input[0])
                {
                    case "/stop":
                        initializedBots[input[1]].StopRecieving();
                        Console.WriteLine($"{input[1]} bot stoped recieving");
                        break;
                    case "/commands":
                        foreach (var e in answers)
                        {
                            Console.WriteLine($"{string.Join(',', e.Key)}:");
                            Console.WriteLine($"\t{e.Value}");
                        }
                        break;
                    default:
                        Console.WriteLine("undefined command");
                        break;
                }
            }
        }

        public static string GetAnswer(string message)
        {
            string answer = "";

            foreach (var keys in answers.Keys)
            {
                foreach (var word in keys)
                {
                    if (message.Contains(word))
                    {
                        answer = answers[keys];
                        break;
                    }
                }
                if (answer != "")
                    break;
            }

            if (answer == "")
                answer = "Извините, данная информация мне неизвестна, однако вам может повезти, если вы переформулируете свой запрос.";

            return answer;
        }
    }
}
