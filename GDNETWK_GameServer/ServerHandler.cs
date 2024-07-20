using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDNETWK_GameServer
{
    class ServerHandler
    {
        public static void TCPTestReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();

            Console.WriteLine($"Received packet via TCP from client. Contains info: {Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected succesfully and is now player {_fromClient} with username {_username}");

            if (_fromClient != _clientIdCheck)
            {
                Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient} has assumed the wrong client ID ({_clientIdCheck})");
            }

            Server.clients[_fromClient].username = _username;
            Server.playerCount++;
            //todo: send player into the game
        }

        public static void UDPTestReceived(int _fromClient, Packet _packet)
        {
            string _msg = _packet.ReadString();

            Console.WriteLine($"Received packet via UDP from client. Contains message: {_msg}");
        }

        public static void TCPPlayerReadyReceived(int _fromClient, Packet _packet)
        {
            bool _isPlayerReady = _packet.ReadBool();

            

            Console.WriteLine($"Player {_fromClient} set ready to {_isPlayerReady}");

            Server.clients[_fromClient].isReady = _isPlayerReady;



            bool _isAllPlayerReady = true;
            //for (int i = 1; i <= Server.playerCount; i++)
            //{
            //    if (Server.clients[i].isReady == false)
            //    {
            //        Console.WriteLine(Server.playerCount);
            //        Console.WriteLine("Not all players are ready");
            //        _isAllPlayerReady = false;
            //        break;
            //    }

            //}

            foreach(Client _client in Server.clients.Values)
            {
                if (_client.isTCPConnected() && _client.isReady == false)
                {
                    Console.WriteLine(Server.playerCount);
                    Console.WriteLine("Not all players are ready");
                    _isAllPlayerReady = false;
                    break;
                }
            }

            ServerSend.TCPPlayerReadyReceivedConfirm(_isAllPlayerReady);

            if (_isAllPlayerReady) 
                ServerSend.TCPSendPromptChoices();

            //todo: send player into the game
        }

        public static void TCPPromptSelectReceived(int _fromClient, Packet _packet)
        {
            int _choice = _packet.ReadInt();
            Server.promptChoiceVotes[_choice]++;
            Server.clients[_fromClient].hasVotedForPrompt = true;



            bool _hasAllPlayerSelectedPrompt = true;
            foreach (Client _client in Server.clients.Values)
            {
                if (_client.isTCPConnected() && _client.hasVotedForPrompt == false)
                {
                    Console.WriteLine(Server.playerCount);
                    Console.WriteLine("Not all players are ready");
                    _hasAllPlayerSelectedPrompt = false;
                    break;
                }
            }
            int _highestVotedPrompt = 0;
            int promptIndex = 0;
            
            for (int i = 0; i < 3; i++)
            {
                if(Server.promptChoiceVotes[i] > _highestVotedPrompt)
                {
                    _highestVotedPrompt = Server.promptChoiceVotes[i];
                    //promptIndex = Server.riddleIndexes[i];
                }
            }

            Console.WriteLine(promptIndex);
            if (_hasAllPlayerSelectedPrompt)
            {
                int rngIndex = 0;
                Random rng = new Random();
                do
                {
                    rngIndex = rng.Next(3);
                    promptIndex = Server.riddleIndexes[rngIndex];
                }
                while (Server.promptChoiceVotes[rngIndex] != _highestVotedPrompt);

                ServerSend.TCPSendRiddleToClients(promptIndex);
                Server.EndSelectTimer();
                
            }
                

            //todo: send player into the game
        }

        public static void TCPAnswerAttemptReceived(int _fromClient, Packet _packet)
        {
            string _answerGuess = _packet.ReadString();
            bool _isAnswerCorrect = _packet.ReadBool();

            if(_isAnswerCorrect)
            {
                Server.clients[_fromClient].points++;
            }

            Console.WriteLine($"Received packet via TCP from client. Contains info: client {_fromClient} attempted to answer with {_answerGuess}. Did client guess correctly {_isAnswerCorrect}");


            ServerSend.TCPSendAttemptPromptToAllExcept(_fromClient, _answerGuess, _isAnswerCorrect);
            //todo: send player into the game
        }

        public static void TCPPlayerListRequestReceived(int _fromClient, Packet _packet)
        {

            string _msg = _packet.ReadString();
            Console.WriteLine($"Received packet via TCP from client. Contains info: {_msg}");
            ServerSend.TCPSendPlayerList(_fromClient);

        }

        public static void TCPChatMessageReceived(int _fromClient, Packet _packet)
        {
            string _msg = _packet.ReadString();
            Console.WriteLine($"Received packet via TCP from client. Contains info: {Server.clients[_fromClient].username} chatted to all users: { _msg}");
            ServerSend.TCPMessageForward(_fromClient, _msg);
        }

        public static void TCPPromptReplyReceived(int _fromClient, Packet _packet)
        {
            string _msg = _packet.ReadString();
            Console.WriteLine($"Received packet via TCP from client. Contains info: {Server.clients[_fromClient].username} replied to the prompt with: { _msg}");
            Server.clients[_fromClient].hasReplied = true;
            ServerSend.TCPPromptReplyRelaySend(_fromClient, _msg);

            bool _hasAllPlayersReplied = true;
            foreach (Client _client in Server.clients.Values)
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
                ServerSend.TCPAllPlayersRepliedSend();
                
            }
                
        }

        public static void TCPVoteForReplyReceived(int _fromClient, Packet _packet)
        {
            int _receiver = _packet.ReadInt();
            Console.WriteLine($"Received packet via TCP from client. Contains info: {_fromClient} voted for {_receiver}'s reply");
            Server.clients[_receiver].votes++;
            Server.clients[_fromClient].hasVotedForReply = true;

            ServerSend.TCPVoteForReplyRelaySend(_receiver);


            bool _hasAllPlayersVoted = true;
            foreach (Client _client in Server.clients.Values)
            {
                if (_client.isTCPConnected() && _client.hasVotedForReply == false)
                {
                    Console.WriteLine(Server.playerCount);
                    Console.WriteLine("Not all players have voted");
                    _hasAllPlayersVoted = false;
                    break;
                }
            }

            if(_hasAllPlayersVoted)
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
