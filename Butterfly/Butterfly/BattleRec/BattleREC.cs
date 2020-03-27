using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
#if UNITY_EDITOR 
using UnityEngine;
#endif 
using Newtonsoft.Json;

namespace ByYoung.Butterfly
{
    public class BattleREC : System.IDisposable
    {
        public static Dictionary<ulong, CacheFile> cacheFiles = new Dictionary<ulong, CacheFile>();

        public static Rule rule;

#if UNITY_EDITOR
        public static string Rule_Path = Application.streamingAssetsPath + "/Rule.json";
#endif
        public static string Rule_Path = "";

        public static bool inited { private set; get; }

        private static int offsetIdx;

        private static List<string> colorList = new List<string>();

        public BattleREC(string _rulePath, Stream _stream)
        {
            Initial(_rulePath, _stream);
            colorList.AddRange(new[]
            {
                "#BF0060",
                "#930093",
                "#0000C6",
                "#007500",
                "#00CACA",
                "#0066CC",
                "#984B4B",
                "#73BF00",
                "#E1E100",
                "#977C00",
                "#EA7500",
                "#842B00",
                "#5A5AAD",
                "#8F4586",
                "#5B00AE",
            });
        }


        public static void ShowRecs(ulong _battleID)
        {
            if (cacheFiles.TryGetValue(_battleID, out var _recFile))
            {
            }
        }

        public static string ShowRec(CacheFile _recFile)
        {
            StringBuilder _sBuilder = new StringBuilder();

            _sBuilder.AppendLine($"战场ID：{_recFile.BattleID}");
            _sBuilder.AppendLine($"战场类型：{_recFile.eCombatType}");
            _sBuilder.AppendLine($"时间：{_recFile.dateTime}");
            if (_recFile.UserIDs != null && _recFile.UserIDs.Length > 1)
                _sBuilder.AppendLine($"发起者ID：{_recFile.UserIDs[0]}, 被攻击者：{_recFile.UserIDs[1]}");

            for (offsetIdx = 0; offsetIdx < _recFile.recList.Count; offsetIdx++)
            {
                CacheRec _rec = _recFile.recList[offsetIdx];
                if (rule.MsgTypes.Count <= _rec.actionType) continue;
                //先识别消息类型
                var _name = rule.MsgTypes[_rec.actionType];
                _sBuilder.Append("【bFps:").Append(_rec.bFps).Append("->");
                _sBuilder.Append("eFps:").Append(_rec.eFps).Append("】  ");
                if (rule.rules.TryGetValue(_name, out var _ruleUnit))
                {
                    var _dic = new Dictionary<string, object>();
                    for (var i = 0; i < _rec.recUnits.Count; i++)
                    {
                        if (i >= _ruleUnit.units.Count) continue;
                        //var _value = _rec.recUnits[i].value;

                        var _value = _rec.recUnits[i];
                        if (_ruleUnit.units[i].StartsWith("E_"))
                        {
                            //认为是枚举
                            if (rule.enumDescribes.TryGetValue(_ruleUnit.units[i], out var _list))
                            {
                                if (int.TryParse(_value.ToString(), out var _enumIdx) && _list.Count > _enumIdx)
                                {
                                    if (_dic.ContainsKey(_ruleUnit.units[i]))
                                        throw new System.Exception($"{_name}消息中已存在{_ruleUnit.units[i]}key，同一key不可以使用多次！");
                                    _dic.Add(_ruleUnit.units[i], _list[_enumIdx]);
                                    continue;
                                }
                            }
                        }
                        if (_dic.ContainsKey(_ruleUnit.units[i]))
                            throw new System.Exception($"{_name}消息中已存在{_ruleUnit.units[i]}key，同一key不可以使用多次！");
                        _dic.Add(_ruleUnit.units[i], _value);
                    }

                    var _describe = _ruleUnit.describe;
                    int _idx = 0;
                    foreach (var _d in _dic)
                    {
                        var _colStr = "#FF0000";
                        if (colorList.Count > _idx)
                            _colStr = colorList[_idx];
                        //_describe = _describe.Replace($"[{_d.Key}]", $"<color={_colStr}>{_d.Value}</color>");
                        _describe = _describe.Replace($"[{_d.Key}]", $"{_d.Value}");
                        _idx++;
                    }

                    _sBuilder.AppendLine(_describe);
                }
            }

            return _sBuilder.ToString();
        }  

        private async void Initial(string _path, Stream _stream = null)
        {
            if (inited == false)
            {
                Rule_Path = _path;
                if (_stream == null)
                {
                    rule = await Loader.LoadRuleFile(Rule_Path);
                }
                else
                {
                    rule = await Loader.LoadRuleFile(_stream);
                }
                inited = !inited;
            }
        }

        public void Dispose()
        {
            rule = null;
            Rule_Path = null;
        }
    }
}