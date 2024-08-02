using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDNETWK_GameServer
{
    class GameServerHandler
    {
        public static void TCPTestReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();

            Console.WriteLine("_fromClient: " + _fromClient);

            Console.WriteLine($"Received packet via TCP from client. Contains info: {GameServer.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected succesfully and is now player {_fromClient} with username {_username}");

            if (_fromClient != _clientIdCheck)
            {
                Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient} has assumed the wrong client ID ({_clientIdCheck})");
            }

            GameServer.clients[_fromClient].username = _username;
            GameServer.playerCount++;
            //todo: send player into the game
        }

        public static void UDPTestReceived(int _fromClient, Packet _packet)
        {
            string _msg = _packet.ReadString();

            Console.WriteLine($"Received packet via UDP from client. Contains message: {_msg}");
        }

        public static void TCPPlayerReadyReceived(int _fromClient, Packet _packet)
        {
            string player1Name = "", player2Name = "" ;
            bool _isPlayerReady = _packet.ReadBool();

            Console.WriteLine($"Player {_fromClient} set ready to {_isPlayerReady}");

            GameServer.clients[_fromClient].isReady = _isPlayerReady;

            bool _isAllPlayerReady = true;

            int counter = 0;
            foreach (GameClient _client in GameServer.clients.Values)
            {
                counter++;
                if (_client.isTCPConnected() && _client.isReady == false)
                {
                    Console.WriteLine("CLIENT: " +  _client);
                    Console.WriteLine(Server.playerCount);
                    Console.WriteLine("Not all players are ready");

                    if(counter == 1)
                        player1Name = _client.username;
                    else if (counter == 2)
                        player2Name = _client.username;
                    //player2Name = _client.username;
                    _isAllPlayerReady = false;
                    break;
                }
            }

            GameServerSend.TCPPlayerReadyReceivedConfirm(_isAllPlayerReady);

            if (_isAllPlayerReady)
            {
                GameServerSend.TCPLoadRPSGame(player1Name, player2Name);
            }

            //todo: send player into the game
        }
        public static void TCPPromptPlayButton(int _fromClient, Packet _packet)
        {
            bool _hasClickedPlay = _packet.ReadBool();

            if(_hasClickedPlay)
                Console.WriteLine($"Player {_fromClient} clicked PLAY");
            else
                Console.WriteLine($"Player {_fromClient} HAS NOT clicked PLAY");

            GameServer.clients[_fromClient].hasClickedPlay = _hasClickedPlay;

            bool _isAllPlayerReady = true;

            foreach (GameClient _client in GameServer.clients.Values)
            {
                if (_client.isTCPConnected() && _client.isReady == false)
                {
                    Console.WriteLine("Not all clicked play");

                    _isAllPlayerReady = false;
                    break;
                }
            }

            //GameServerSend.TCPPlayerPlayButtonConfirmed(_isAllPlayerReady);

            if (_isAllPlayerReady)
            {
                GameServerSend.TCPPlayerPlayButtonConfirmed(_isAllPlayerReady);
                //GameServerSend.TCPStartRPSGame();
            }
        }

        public static void TCPPromptSelectReceived(int _fromClient, Packet _packet)
        {
            int _choice = _packet.ReadInt();
            GameServer.clients[_fromClient].hasVotedForPrompt = true;
            GameServer.clients[_fromClient].chosenMove = (EChoice) _choice;


            bool _hasAllPlayerSelectedPrompt = true;
            foreach (GameClient _client in GameServer.clients.Values)
            {
                if (_client.isTCPConnected() && _client.hasVotedForPrompt == false)
                {
                    Console.WriteLine(Server.playerCount);
                    Console.WriteLine("Not all players are ready");
                    _hasAllPlayerSelectedPrompt = false;
                    break;
                }
            }
            if (_hasAllPlayerSelectedPrompt)
            {
                GameServerSend.TCPRevealMoves(_fromClient, _choice);
                GameServer.EndSelectTimer();
            }

            //todo: send player into the game
        }

        public static void TCPAnswerAttemptReceived(int _fromClient, Packet _packet)
        {
            string _answerGuess = _packet.ReadString();
            bool _isAnswerCorrect = _packet.ReadBool();

            if (_isAnswerCorrect)
            {
                GameServer.clients[_fromClient].points++;
            }

            Console.WriteLine($"Received packet via TCP from client. Contains info: client {_fromClient} attempted to answer with {_answerGuess}. Did client guess correctly {_isAnswerCorrect}");


            GameServerSend.TCPSendAttemptPromptToAllExcept(_fromClient, _answerGuess, _isAnswerCorrect);
            //todo: send player into the game
        }

        public static void TCPPlayerListRequestReceived(int _fromClient, Packet _packet)
        {
            string _msg = _packet.ReadString();
            Console.WriteLine($"Received packet via TCP from client. Contains info: {_msg}");
            GameServerSend.TCPSendPlayerList(_fromClient);
        }

        public static void TCPChatMessageReceived(int _fromClient, Packet _packet)
        {
            string _msg = _packet.ReadString();
            Console.WriteLine($"Received packet via TCP from client. Contains info: {Server.clients[_fromClient].username} chatted to all users: {_msg}");
            GameServerSend.TCPMessageForward(_fromClient, _msg);
        }

        public static void TCPPromptReplyReceived(int _clientIndex, Packet _packet)
        {
            string _msg = _packet.ReadString();
            Console.WriteLine($"Received packet via TCP from client. Contains info: {Server.clients[_clientIndex].username} replied to the prompt with: {_msg}");
            GameServer.clients[_clientIndex].hasReplied = true;
            GameServerSend.TCPPromptReplyRelaySend(_clientIndex, _msg);

            bool _hasAllPlayersReplied = true;
            foreach (GameClient _client in GameServer.clients.Values)
            {
                if (_client.isTCPConnected() && _client.hasReplied == false)
                {
                    Console.WriteLine(Server.playerCount);
                    Console.WriteLine("Not all players have replied");
                    _hasAllPlayersReplied = false;
                    break;
                }
            }

            if (_hasAllPlayersReplied)
            {
                Console.WriteLine("All players have replied. Enabling voting.");
                GameServerSend.TCPAllPlayersRepliedSend();

            }

        }

        public static void TCPVoteForReplyReceived(int _fromClient, Packet _packet)
        {
            int _receiver = _packet.ReadInt();
            Console.WriteLine($"Received packet via TCP from client. Contains info: {_fromClient} voted for {_receiver}'s reply");
            GameServer.clients[_receiver].votes++;
            GameServer.clients[_fromClient].hasVotedForReply = true;

            GameServerSend.TCPVoteForReplyRelaySend(_receiver);


            bool _hasAllPlayersVoted = true;
            foreach (GameClient _client in GameServer.clients.Values)
            {
                if (_client.isTCPConnected() && _client.hasVotedForReply == false)
                {
                    Console.WriteLine(Server.playerCount);
                    Console.WriteLine("Not all players have voted");
                    _hasAllPlayersVoted = false;
                    break;
                }
            }

            if (_hasAllPlayersVoted)
            {
                //int _highestVotes = 0;
                //int _id = 0;
                //foreach(Client _client in Server.clients.Values)
                //{
                //    if(_client.tcp != null)
                //    {
                //        if(_client.votes > _highestVotes)
                //        {
                //            _highestVotes = _client.votes;
                //            _id = _client.id;
                //        }
                //    }
                //}
                //int _points = Server.clients[_id].points++;
                //ServerSend.TCPHighestVotesSend(_id, _highestVotes);


            }
        }



    }




}
