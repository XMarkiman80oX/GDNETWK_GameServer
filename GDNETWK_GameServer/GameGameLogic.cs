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
            if (GameServer.isSelectTimerRunning)
            {
                GameServer.promptSelectTimer -= _dt;

                if (GameServer.promptSelectTimer <= 0.0f)
                {
                    bool bothHaveSelectedChoice = false;
                    int clientIndex;
                    for (int i = 0; i<GameServer.clients.Count; i++)
                    {
                        if (GameServer.clients[i].chosenMove != EChoice.NONE)
                        {
                            clientIndex = i;
                            bothHaveSelectedChoice = true;
                        }
                    }
                    if (bothHaveSelectedChoice)
                    {
                        GameServer.EndSelectTimer();
                    }
                }
            }

            ThreadManager.UpdateMain();
        }
    }
}
