using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDNETWK_GameServer
{
    class GameLogic
    {
        public static void Update(float _dt)
        {
            if(Server.isTimerRunning)
            {
                Server.timer += _dt;
                ServerSend.TCPTimerSend();
                if (Server.timer >= 35.0f)
                {
                    Server.EndTimer();

                    int _highestVotes = 0;
                    int _id = 0;
                    foreach (Client _client in Server.clients.Values)
                    {
                        if (_client.tcp != null)
                        {
                            if (_client.votes > _highestVotes)
                            {
                                _highestVotes = _client.votes;
                                _id = _client.id;
                            }
                        }
                    }
                    if(_highestVotes != 0)
                        Server.clients[_id].points++;

                    ServerSend.TCPHighestVotesSend(_id, _highestVotes);
                }
                    
            }

            ThreadManager.UpdateMain();
        }
    }
}
