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
            if(Server.isReplyTimerRunning)
            {
                Server.promptReplyTimer -= _dt;
                ServerSend.TCPTimerSend();
                if (Server.promptReplyTimer <= 0.0f)
                {
                    Server.EndReplyTimer();
                    if(Server.isVotingBestReply == false)
                    {
                        bool isThereAReply = false;

                        foreach (Client _client in Server.clients.Values)
                        {
                            if (_client.hasReplied == true)
                            {
                                isThereAReply = true;
                                break;
                            }
                                
                        }

                        if(isThereAReply)
                        {
                            ServerSend.TCPAllPlayersRepliedSend();
                        }
                    }

                    else
                    {
                        int _highestVotes = 0;
                        int _id = 0;
                        foreach (Client _client in Server.clients.Values)
                        {
                            if (_client.tcp != null)
                            {
                                if (_client.votes == _highestVotes)
                                {

                                    Random rng = new Random();

                                    int rngIndex = rng.Next(0, 1);

                                    Console.WriteLine(rngIndex);
                                    if (rngIndex == 0)
                                    {
                                        _id = _client.id;
                                    }

                                }

                                else if (_client.votes > _highestVotes)
                                {
                                    _highestVotes = _client.votes;
                                    _id = _client.id;


                                }
                            }
                        }
                        if (_highestVotes != 0)
                            Server.clients[_id].points++;


                        ServerSend.TCPHighestVotesSend(_id, _highestVotes);
                    }
                    
                }
                    
            }

            else if(Server.isSelectTimerRunning)
            {
                Server.promptSelectTimer -= _dt;

                if(Server.promptSelectTimer <= 0.0f)
                {
                    Random rng = new Random();
                    int riddleIndex = rng.Next(3);
                    Console.WriteLine("Errors in Game Logic");
                    ServerSend.TCPSendRiddleToClients(Server.riddleIndexes[riddleIndex]);
                    Server.EndSelectTimer();
                }
            }

            ThreadManager.UpdateMain();
        }
    }
}
