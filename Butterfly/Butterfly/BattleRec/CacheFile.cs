using System;
using System.Collections.Generic;

namespace ByYoung.Butterfly
{
    [Serializable]
    public class CacheFile
    {
        public ulong BattleID;
        public int eCombatType;
        public ulong[] UserIDs;
        public string dateTime;
        public List<CacheRec> recList = new List<CacheRec>();
    }

    [Serializable]
    public class CacheRec
    {
        public int actionType;
        public int bFps, eFps;
        public List<int> recUnits = new List<int>(); 
    }

    //[Serializable]
    //public class RecUnit
    //{
    //    public System.Type type;
    //    public int IntValue;
    //    public string StringValue;
    //    public float FloatValue;
    //    public bool BoolValue; 
    //}
}