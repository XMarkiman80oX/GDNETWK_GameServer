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
            string Username = _packet.ReadString();

            Console.WriteLine($"Received packet via TCP from client. Contains info: {Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected succesfully and is now player {_fromClient} with username {Username}");

            if (_fromClient != _clientIdCheck)
            {
                Console.WriteLine($"Player \"{Username}\" (ID: {_fromClient} has assumed the wrong client ID ({_clientIdCheck})");
            }

            //todo: send player into the game
        }

        public static void UDPTestReceived(int _fromClient, Packet _packet)
        {
            string _msg = _packet.ReadString();

            Console.WriteLine($"Received packet via UDP from client. Contains message: {_msg}");
        }
    }
}
