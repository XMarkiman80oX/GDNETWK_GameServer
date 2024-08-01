using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDNETWK_GameServer
{
    class GameGameLogic
    {
        public static void Update(float _dt)
        {
            if (GameServer.isReplyTimerRunning)
            {
                GameServer.promptReplyTimer -= _dt;
                GameServerSend.TCPTimerSend();
                if (GameServer.promptReplyTimer <= 0.0f)
                {
                    GameServer.EndTimer();
                    //if (GameServer.isVotingBestReply == false)
                    //{
                    //    bool isThereAReply = false;

                    //    foreach (GameClient _client in GameServer.clients.Values)
                    //    {
                    //        if (_client.hasReplied == true)
                    //        {
                    //            isThereAReply = true;
                    //            break;
                    //        }

                    //    }

                    //    if (isThereAReply)
                    //    {
                    //        GameServerSend.TCPAllPlayersRepliedSend();
                    //    }
                    //}

                    //else
                    //{
                        int _highestVotes = 0;
                        int _id = 0;
                        foreach (GameClient _client in GameServer.clients.Values)
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
                            GameServer.clients[_id].points++;


                        GameServerSend.TCPHighestVotesSend(_id, _highestVotes);
                    //}

                }

            }

            else if (GameServer.isSelectTimerRunning)
            {
                GameServer.promptSelectTimer -= _dt;

                if (GameServer.promptSelectTimer <= 0.0f)
                {
                    Random rng = new Random();
                    int riddleIndex = rng.Next(3);
                    //GameServerSend.TCPSendRiddleToClients(Server.riddleIndexes[riddleIndex]);
                    GameServer.EndSelectTimer();
                }
            }

            ThreadManager.UpdateMain();
        }
    }
}
