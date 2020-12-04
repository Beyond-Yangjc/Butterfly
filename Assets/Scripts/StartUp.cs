using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ByYoung.Butterfly;
using ByYoung.Butterfly.AutoGenerate;
using UniRx;
using UniRx.Async;
using UnityEngine;

public class StartUp : MonoBehaviour
{
    // Start is called before the first frame update

    private BattleREC _rec;

    void Start()
    {
        _rec = new BattleREC(Application.streamingAssetsPath + "/Rule.json");

        Test();

    }

    async void Test()
    {
        await UniTask.WaitUntil(() => BattleREC.inited);
        
        Write();

        Read();
    }


    void Write()
    {
        Translater.BeginCache<CacheFile>(111, 2, new ulong[] {1000, 2000});
        var _cache = new CacheRec() {actionType = (int)TranslaterDefine.MsgTypes.BuffAction, bFps = 1, eFps = 10};
        _cache.recUnits.Add(new RecUnit(){type = typeof(int), value = 1}); 
        _cache.recUnits.Add(new RecUnit(){type = typeof(int), value = 1});
        _cache.recUnits.Add(new RecUnit(){type = typeof(int), value = 1});
        _cache.recUnits.Add(new RecUnit(){type = typeof(int), value = 1});
        _cache.recUnits.Add(new RecUnit(){type = typeof(int), value = 1});
        _cache.recUnits.Add(new RecUnit(){type = typeof(int), value = 1});
        Translater.AddNewCache(_cache);
        Translater.SaveCache2File(Translater.EndCache());
    }

    void Read()
    {
        string _path = Application.streamingAssetsPath + "/rec.btf";
        using (var _fs = new FileStream(_path, FileMode.Open, FileAccess.Read))
        {
            BinaryFormatter _bf = new BinaryFormatter();
            var _cache = _bf.Deserialize(_fs) as CacheFile;

            var _str = BattleREC.ShowRec(_cache);
            Debug.Log($"Recorder: {_str}");
            
            _fs.Close();
        }
    }
}