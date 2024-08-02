using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDNETWK_GameServer
{
    class GameServerSend
    {
        private static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            GameServer.clients[_toClient].tcp.SendData(_packet);
        }

        private static void SendUDPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            GameServer.clients[_toClient].udp.SendData(_packet);
        }

        private static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= GameServer.MaxPlayers; i++)
            {
                GameServer.clients[i].tcp.SendData(_packet);
            }
        }

        private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= GameServer.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                    GameServer.clients[i].tcp.SendData(_packet);
            }
        }

        private static void SendUDPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= GameServer.MaxPlayers; i++)
            {
                GameServer.clients[i].udp.SendData(_packet);
            }
        }

        private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= GameServer.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                    GameServer.clients[i].udp.SendData(_packet);
            }
        }

        public static void TCPTest(int _toClient, string msg)
        {
            using (Packet _packet = new Packet(((int)GameServerPackets.welcome)))
            {
                _packet.Write(msg + $"You are now player {_toClient}");
                _packet.Write(_toClient);

                SendTCPData(_toClient, _packet);

            }
        }

        public static void UDPTest(int _toClient)
        {
            using (Packet _packet = new Packet(((int)GameServerPackets.udpTest)))
            {
                _packet.Write("A test packet for udp");


                SendUDPData(_toClient, _packet);

            }
        }
        public static void TCPPlayerPlayButtonConfirmed(bool _hasAllPressed)
        {
            string _msg = "player ready status change received";
            bool _gameStart = _hasAllPressed;
            using (Packet _packet = new Packet(((int)GameServerPackets.PressedPlayReceived)))
            {
                _packet.Write(_msg);
                _packet.Write(_gameStart);
                SendTCPDataToAll(_packet);
            }
        }
        public static void TCPPlayerReadyReceivedConfirm(bool _isAllPlayerReady)
        {
            string _msg = "player ready status change received";
            bool _gameStart = _isAllPlayerReady;
            using (Packet _packet = new Packet(((int)GameServerPackets.playerReadyReceived)))
            {
                _packet.Write(_msg);
                _packet.Write(_gameStart);
                SendTCPDataToAll(_packet);
            }
        }
        //public static void TCPStartRPSGame()
        //{
        //    using (Packet _packet = new Packet((int)GameServerPackets.PromptStartGame))
        //    {
        //        _packet.Write("This will send to Start Game");
        //        Console.WriteLine("Start RPS Game Sent to Client");
        //    }

        //    //GameServer.StartSelectTimer();
        //}
        public static void TCPLoadRPSGame(string playerName)
        {
            //GameServer.rockPaperOrScissors[0] = EChoice.ROCK;
            //GameServer.rockPaperOrScissors[1] = EChoice.PAPER;
            //GameServer.rockPaperOrScissors[2] = EChoice.SCISSORS;

            using (Packet _packet = new Packet(((int)GameServerPackets.PromptChoicesSend)))
            {
                //_packet.Write((int)GameServer.rockPaperOrScissors[0]);
                //_packet.Write((int)GameServer.rockPaperOrScissors[1]);
                //_packet.Write((int)GameServer.rockPaperOrScissors[2]);
                Console.WriteLine("Inside TCPSendPromptChoices -> playerName:" + playerName);

                _packet.Write(playerName);
                //_packet.Write(player2Name);
                SendTCPDataToAll(_packet);
            }
        }
        public static void TCPRevealMoves(int playerIndex, int _index) //TCPSendRiddleToClients
        {
            //int randRiddleIndex = rnd.Next(Server.riddleGenerator.riddleBook.Count);
            
            using (Packet _packet = new Packet(((int)GameServerPackets.ChoiceSend)))
            {
                _packet.Write(_index);
                _packet.Write(playerIndex);
                SendTCPDataToAll(_packet);
            }

            foreach (Client _client in Server.clients.Values)
            {
                _client.votes = 0;
                _client.isReady = false;
                _client.hasReplied = false;
            }
            GameServer.StartTimer();
        }

        public static void TCPSendPlayerList(int _toClient)
        {

            for (int i = 1; i <= GameServer.playerCount; i++)
            {
                int _clientID = GameServer.clients[i].id;
                string _username = GameServer.clients[i].username;
                int _points = GameServer.clients[i].points;

                using (Packet _packet = new Packet(((int)GameServerPackets.PlayerListSend)))
                {
                    _packet.Write(_clientID);
                    _packet.Write(_username);
                    _packet.Write(_points);

                    SendTCPData(_toClient, _packet);

                }
            }


        }

        public static void TCPSendAttemptPromptToAllExcept(int _exceptClient, string _answerGuess, bool _isAnswerCorrect)
        {


            using (Packet _packet = new Packet(((int)GameServerPackets.AnswerAttemptReceived)))
            {

                _packet.Write(_exceptClient);   //which client made a guess
                _packet.Write(_answerGuess);
                _packet.Write(_isAnswerCorrect);

                SendTCPDataToAll(_exceptClient, _packet);

            }



        }

        public static void TCPMessageForward(int _senderClient, string _chatMessage)
        {
            using (Packet _packet = new Packet(((int)GameServerPackets.ChatMessageForwardSend)))
            {

                _packet.Write(_chatMessage);   //which client made a guess
                _packet.Write(_senderClient);

                SendTCPDataToAll(_packet);

            }
        }

        public static void TCPPlayerDisconnect(int _senderClient)
        {
            using (Packet _packet = new Packet(((int)GameServerPackets.PlayerDisconnectSend)))
            {

                _packet.Write(_senderClient);   //which client disconnected

                SendTCPDataToAll(_senderClient, _packet);

            }
        }

        public static void TCPPromptReplyRelaySend(int _senderClient, string _promptReply)
        {
            using (Packet _packet = new Packet(((int)GameServerPackets.PromptReplyRelaySend)))
            {
                _packet.Write(_senderClient);   //client that sent a prompt reply
                _packet.Write(_promptReply);   //client reply to prompt


                SendTCPDataToAll(_packet);

            }
        }

        public static void TCPAllPlayersRepliedSend()
        {
            GameServer.StartTimer();
            //string _msg = "All players have replied";
            //using (Packet _packet = new Packet(((int)GameServerPackets.AllPlayersRepliedSend)))
            //{
            //    _packet.Write(_msg);
            //    SendTCPDataToAll(_packet);
            //}

            //Server.isVotingBestReply = true;
        }

        public static void TCPVoteForReplyRelaySend(int _id)
        {
            int _votes = GameServer.clients[_id].votes;
            using (Packet _packet = new Packet(((int)GameServerPackets.VotedForReplyRelaySend)))
            {
                _packet.Write(_id);
                _packet.Write(_votes);
                SendTCPDataToAll(_packet);

            }
        }

        public static void TCPHighestVotesSend(int _highestVotesId, int _highestVotesValue)
        {
            using (Packet _packet = new Packet(((int)GameServerPackets.HighestVotesSend)))
            {
                _packet.Write(_highestVotesId);   //client with highest votes
                _packet.Write(_highestVotesValue);
                SendTCPDataToAll(_packet);

            }
        }

        public static void TCPTimerSend()
        {
            float _currentTimerValue = GameServer.promptReplyTimer;
            using (Packet _packet = new Packet(((int)GameServerPackets.TimerSend)))
            {
                _packet.Write(_currentTimerValue);   //client with highest votes
                SendTCPDataToAll(_packet);

            }
            //Console.WriteLine("sending");
        }
    }
}
