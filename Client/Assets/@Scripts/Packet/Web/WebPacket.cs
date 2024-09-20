using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WebPacket
{
    [Serializable]
    public class LoginRequest
    {
        public string username;
        public string password;
    }

    [Serializable]
    public class LoginResponse
    {
        public bool success;
        public int accountDbId;
        public string accessToken;
        public string refreshToken;
    }
}
