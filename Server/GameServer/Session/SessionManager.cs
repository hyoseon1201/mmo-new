﻿using MySqlX.XDevAPI;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class SessionManager : Singleton<SessionManager>
    {
        int _sessionId = 0;

        Dictionary<int, ClientSession> _sessions = new Dictionary<int, ClientSession>();
        object _lock = new object();

        public List<ClientSession> GetSessions()
        {
            List<ClientSession> sessions = new List<ClientSession>();

            lock (_lock)
            {
                sessions = _sessions.Values.ToList();
            }

            return sessions;
        }

        public ClientSession Generate()
        {
            lock (_lock)
            {
                int sessionId = ++_sessionId;
                ClientSession session = new ClientSession();
                _sessions.Add(sessionId, session);
                Console.WriteLine($"Connected : {_sessionId}");
                return session;
            }
        }

        public ClientSession Find(int id)
        {
            lock (_lock)
            {
                _sessions.TryGetValue(id, out ClientSession session);
                return session;
            }
        }

        public void Remove(ClientSession session)
        {
            lock (_lock)
            {
                _sessions.Remove(session.SessionId);
            }
        }
    }
}
