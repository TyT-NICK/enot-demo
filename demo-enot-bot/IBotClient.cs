using System;
using System.Collections.Generic;
using System.Text;

namespace demo_enot_bot
{
    interface IBotClient
    {
        void SendAnswer(long chatID, string answer);
        void StopRecieving();
    }
}
