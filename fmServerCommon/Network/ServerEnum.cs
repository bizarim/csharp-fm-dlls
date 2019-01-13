

// port

// chat         : 17501 for client
// chat         : 10500 for server

// op           : 18900 for client
// op           : 11200 for server
// Center       : 18910 for client
// Center       : 11210 for server

// AuthServer   : 18920 for client
// GameServer   : 18930 for client
// Center       : 11230 for server


// mysql db     : 6361
// redis db     : 5311

// mysql db     : 15101
// redis db     : 15102

// mysql
// root, tkfkwu82

namespace fmServerCommon
{

    public enum eServerType
    {
        None = 0,
        Center,
        Auth,
        Game,
        Op,
        Chat,
    }

    public enum eState
    {
        eState_Stop,
        eState_Ready,
        eState_Run,
    }
}
