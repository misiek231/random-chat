using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomChat.WebApi.Services.Interfaces
{
    public interface IChatsContainer
    {
        bool NewClient(string connectionId, out string secondClient);
        string GetTargetClient(string connectionId);
        string DestroyChat(string connectionId);
    }
}
