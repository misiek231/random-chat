﻿using RandomChat.WebApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomChat.WebApi.Services
{
    public class ChatsContainer : IChatsContainer
    {
        private string waitingClientId = null;
        private readonly List<Chat> chats = new List<Chat>();

        public string DestroyChat(string connectionId)
        {
            Chat chat = chats.Where(c => c.GetTargetClient(connectionId) != null).FirstOrDefault();
            chats.Remove(chat);
            return chat.GetTargetClient(connectionId);
        }

        public string GetTargetClient(string connectionId)
        {
            return chats.Where(c => c.GetTargetClient(connectionId) != null).Select(c => c.GetTargetClient(connectionId)).FirstOrDefault() ?? throw new Exception();
        }

        public bool NewClient(string connectionId, out string secondClient)
        {
            if (waitingClientId != null)
            {
                chats.Add(new Chat(connectionId, waitingClientId));
                secondClient = waitingClientId;
                waitingClientId = null;
                return true;
            }
            else
            {
                waitingClientId = connectionId;
                secondClient = null;
                return false;
            }
        }
    }

    public class Chat
    {
        public string FirstClient { get; }
        public string SecondClient { get; }

        public Chat(string firstClient, string secondClient)
        {
            FirstClient = firstClient;
            SecondClient = secondClient;
        }

        public string GetTargetClient(string connectionId)
        {
            if (connectionId == FirstClient)
                return SecondClient;
            else if (connectionId == SecondClient)
                return FirstClient;
            else
                return null;
        }
    }
}
