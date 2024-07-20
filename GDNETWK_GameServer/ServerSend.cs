using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDNETWK_GameServer
{
    class ServerSend
    {

        private static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].tcp.SendData(_packet);
        }

        private static void SendUDPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].udp.SendData(_packet);
        }

        private static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }

        private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if(i != _exceptClient)
                    Server.clients[i].tcp.SendData(_packet);
            }
        }

        private static void SendUDPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].udp.SendData(_packet);
            }
        }

        private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                    Server.clients[i].udp.SendData(_packet);
            }
        }

        public static void TCPTest(int _toClient, string msg)
        {
            using (Packet _packet = new Packet(((int)ServerPackets.welcome)))
            {
                _packet.Write(msg + $"You are now player {_toClient}");
                _packet.Write(_toClient);

                SendTCPData(_toClient, _packet);

            }
        }

        public static void UDPTest(int _toClient)
        {
            using (Packet _packet = new Packet(((int)ServerPackets.udpTest)))
            {
                _packet.Write("A test packet for udp");


                SendUDPData(_toClient, _packet);

            }
        }

        public static void TCPPlayerReadyReceivedConfirm(bool _isAllPlayerReady)
        {
            string _msg = "player ready status change received";
            bool _gameStart = _isAllPlayerReady;
            using (Packet _packet = new Packet(((int)ServerPackets.playerReadyReceived)))
            {
                _packet.Write(_msg);
                _packet.Write(_gameStart);
                SendTCPDataToAll(_packet);
            }
        }

        public static void TCPSendPromptChoices()
        {
            Server.promptChoiceVotes = new int[3] { 0, 0, 0 };
            Random rnd = new Random();

            Server.riddleIndexes = new int[3];

            for (int i = 0; i < 3; i++)
            {
                int rndIndex;
                do
                {
<<<<<<< Updated upstream
                    rndIndex = rnd.Next(Server.riddleGenerator.riddleBook.Count);
=======
                    rndIndex = rnd.Next(Server.riddleGenerator.promptList.Count);
>>>>>>> Stashed changes
                    Console.WriteLine(rndIndex);
                }

                while (Server.riddleIndexes.Contains(rndIndex));
                Server.riddleIndexes[i] = rndIndex;
            }

            using (Packet _packet = new Packet(((int)ServerPackets.PromptChoicesSend)))
            {
<<<<<<< Updated upstream
                _packet.Write(Server.riddleGenerator.riddleBook[Server.riddleIndexes[0]].Key);
                _packet.Write(Server.riddleGenerator.riddleBook[Server.riddleIndexes[1]].Key);
                _packet.Write(Server.riddleGenerator.riddleBook[Server.riddleIndexes[2]].Key); 
                SendTCPDataToAll(_packet);
            }
=======
                _packet.Write(Server.riddleGenerator.promptList[Server.riddleIndexes[0]]);
                _packet.Write(Server.riddleGenerator.promptList[Server.riddleIndexes[1]]);
                _packet.Write(Server.riddleGenerator.promptList[Server.riddleIndexes[2]]); 
                SendTCPDataToAll(_packet);
            }

            Server.StartSelectTimer();
>>>>>>> Stashed changes
        }

        public static void TCPSendRiddleToClients(int _index)
        {
<<<<<<< Updated upstream
            

            
            

            //int randRiddleIndex = rnd.Next(Server.riddleGenerator.riddleBook.Count);
            string riddle = Server.riddleGenerator.riddleBook[_index].Key;
            string answer = Server.riddleGenerator.riddleBook[_index].Value;
            using (Packet _packet = new Packet(((int)ServerPackets.RiddleSend)))
            {
                _packet.Write(riddle);
                _packet.Write(answer);
=======




            Server.StartReplyTimer();
            Server.isVotingBestReply = false;
            //int randRiddleIndex = rnd.Next(Server.riddleGenerator.riddleBook.Count);
            string riddle = Server.riddleGenerator.promptList[_index];
            using (Packet _packet = new Packet(((int)ServerPackets.RiddleSend)))
            {
                _packet.Write(riddle);
>>>>>>> Stashed changes
                SendTCPDataToAll(_packet);
            }

            foreach(Client _client in Server.clients.Values)
            {
                _client.votes = 0;
                _client.isReady = false;
                _client.hasReplied = false;
                _client.hasVotedForReply = false;
                _client.hasVotedForPrompt = false;
            }
          
        }

        public static void TCPSendPlayerList(int _toClient)
        {

            for(int i = 1; i <= Server.playerCount; i++)
            {
                int _clientID = Server.clients[i].id;
                string _username = Server.clients[i].username;
                int _points = Server.clients[i].points;

                using (Packet _packet = new Packet(((int)ServerPackets.PlayerListSend)))
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
            
                
            using (Packet _packet = new Packet(((int)ServerPackets.AnswerAttemptReceived)))
            {

                _packet.Write(_exceptClient);   //which client made a guess
                _packet.Write(_answerGuess);
                _packet.Write(_isAnswerCorrect);

                SendTCPDataToAll(_exceptClient, _packet);

            }
            


        }

        public static void TCPMessageForward(int _senderClient, string _chatMessage)
        {
            using (Packet _packet = new Packet(((int)ServerPackets.ChatMessageForwardSend)))
            {

                _packet.Write(_chatMessage);   //which client made a guess
                _packet.Write(_senderClient);

                SendTCPDataToAll(_packet);

            }
        }

        public static void TCPPlayerDisconnect(int _senderClient)
        {
            using (Packet _packet = new Packet(((int)ServerPackets.PlayerDisconnectSend)))
            {

                _packet.Write(_senderClient);   //which client disconnected

                SendTCPDataToAll(_senderClient, _packet);

            }
        }

        public static void TCPPromptReplyRelaySend(int _senderClient, string _promptReply)
        {
            using (Packet _packet = new Packet(((int)ServerPackets.PromptReplyRelaySend)))
            {
                _packet.Write(_senderClient);   //client that sent a prompt reply
                _packet.Write(_promptReply);   //client reply to prompt
                

                SendTCPDataToAll(_packet);

            }
        }

        public static void TCPAllPlayersRepliedSend()
        {
<<<<<<< Updated upstream
=======
            Server.StartReplyTimer();
>>>>>>> Stashed changes
            string _msg = "All players have replied";
            using (Packet _packet = new Packet(((int)ServerPackets.AllPlayersRepliedSend)))
            {
                _packet.Write(_msg);   
                SendTCPDataToAll(_packet);

            }
<<<<<<< Updated upstream
=======

            Server.isVotingBestReply = true;
>>>>>>> Stashed changes
        }

        public static void TCPVoteForReplyRelaySend(int _id)
        {
            int _votes = Server.clients[_id].votes;
            using (Packet _packet = new Packet(((int)ServerPackets.VotedForReplyRelaySend)))
            {
                _packet.Write(_id);
                _packet.Write(_votes);
                SendTCPDataToAll(_packet);

            }
        }

        public static void TCPHighestVotesSend(int _highestVotesId, int _highestVotesValue)
        {
            using (Packet _packet = new Packet(((int)ServerPackets.HighestVotesSend)))
            {
                _packet.Write(_highestVotesId);   //client with highest votes
                _packet.Write(_highestVotesValue);
                SendTCPDataToAll(_packet);

            }
        }

        public static void TCPTimerSend()
        {
<<<<<<< Updated upstream
            float _currentTimerValue = Server.timer;
=======
            float _currentTimerValue = Server.promptReplyTimer;
>>>>>>> Stashed changes
            using (Packet _packet = new Packet(((int)ServerPackets.TimerSend)))
            {
                _packet.Write(_currentTimerValue);   //client with highest votes
                SendTCPDataToAll(_packet);

            }
<<<<<<< Updated upstream
            Console.WriteLine("sending");
=======
            //Console.WriteLine("sending");
>>>>>>> Stashed changes
        }
    }
}
