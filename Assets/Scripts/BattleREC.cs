using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using UniRx.Async;
using UnityEngine;
using LitJson;

namespace ByYoung.Services
{
    public class CacheFile
    {
        public ulong BattleID;
        public int eCombatType;
        public ulong[] UserIDs;
        public string dateTime;
        public List<CacheRec> recList = new List<CacheRec>();
    }

    public class CacheRec
    {
        public int actionType;
        public int bFps, eFps;
        public List<RecUnit> recUnits = new List<RecUnit>();
    }

    public struct RecUnit
    {
        public System.Type type;
        public System.Object value;
    }

    public static class StringBuilderExtension
    {
        public static void AppendRow2Builder(ref StringBuilder _sBuilder, string _row)
        {
            _sBuilder.Append(_row).Append("\n");
        }
    }

    public class BattleREC
    {
        public static Dictionary<ulong, CacheFile> cacheFiles = new Dictionary<ulong, CacheFile>();

        public static Rule rule;

        public static string ruleFile_Path = Application.streamingAssetsPath + "/Rule.json";

        private static bool inited = false;

        private static int offsetIdx;

        public BattleREC()
        {
            Initial();
        }

        public static void ShowRec(ulong _battleID)
        {
            if (cacheFiles.TryGetValue(_battleID, out var _recFile))
            {
                StringBuilder _sBuilder = new StringBuilder();

                _sBuilder.AppendLine($"战场ID：{_recFile.BattleID}");
                _sBuilder.AppendLine($"战场类型：{_recFile.eCombatType}");
                _sBuilder.AppendLine($"时间：{_recFile.dateTime}");
                _sBuilder.AppendLine($"发起者ID：{_recFile.UserIDs[0]}, 被攻击者：{_recFile.UserIDs[1]}");

                for (; offsetIdx < _recFile.recList.Count; offsetIdx++)
                {
                    CacheRec _rec = _recFile.recList[offsetIdx];
                    var _name = rule.MsgTypes[_rec.actionType];
                    if (rule.rules.TryGetValue(_name, out var _ruleUnit))
                    {
                        var _dic = new Dictionary<string, object>();
                        for (var i = 0; i < _rec.recUnits.Count; i++)
                        {
                            if (i >= _ruleUnit.units.Count) continue;
                            var _value = _rec.recUnits[i].value;
                            if (_ruleUnit.units[i].StartsWith("E_"))
                            {
                                //认为是枚举
                                rule.enumDescribes.TryGetValue(_ruleUnit.units[i], out var _list);
                                var _realValue = _list[(int) _value];
                                _value = _realValue;
                            }

                            _dic.Add(_ruleUnit.units[i], _value);
                        }

                        foreach (var _d in _dic)
                        {
                            _ruleUnit.desctipt.Replace(_d.Key, _d.Value.ToString());
                        }
                    }
                }
            }
        }


        private async void Initial()
        {
            rule = await ReadJsonFile(ruleFile_Path);
            inited = true;
        }

        private async UniTask<Rule> ReadJsonFile(string _filePath)
        {
            using (var _fs = new FileStream(_filePath, FileMode.Open))
            {
                using (var _sr = new StreamReader(_fs, Encoding.UTF8))
                {
                    var _content = await _sr.ReadToEndAsync().AsUniTask();
                    _sr.Close();
                    _fs.Close();
                    var _rule = string.IsNullOrEmpty(_content) == false ? Json2Rule(JsonMapper.ToObject(_content)) : null;
                    return _rule;
                }
            }
        }

        private Rule Json2Rule(JsonData _jData)
        {
            var _rule = new Rule();
            var _jmsgTypes = _jData["MsgTypes"];
            if (_jmsgTypes != null && _jmsgTypes.IsArray)
            {
                foreach (JsonData _t in _jmsgTypes)
                {
                    _rule.MsgTypes.Add(_t.ToString());
                }
            }

            var _jruleUnits = _jData["rules"];
            if (_jruleUnits != null && _jruleUnits.IsArray)
            {
                foreach (JsonData _jr in _jruleUnits)
                {
                    var _name = _jr["name"].ToString();
                    if (_rule.rules.ContainsKey(_name)) continue;
                    var _descript = _jr["describe"].ToString();
                    var _junits = _jr["units"];
                    var _ruleUnit = new RuleUnit
                    {
                        name = _name,
                        desctipt = _descript,
                    };
                    if (_junits.IsArray)
                    {
                        foreach (JsonData _junit in _junits)
                        {
                            _ruleUnit.units.Add(_junit.ToString());
                        }
                    }

                    _rule.rules.Add(_name, _ruleUnit);
                }
            }

            var _jenums = _jData["enums"];
            if (_jenums != null && _jenums.IsArray)
            {
                foreach (JsonData _jenum in _jenums)
                {
                    var _ename = _jenum["name"].ToString();
                    var _jenumD = _jenum["enum"];
                    var _enumList = new List<string>();
                    foreach (JsonData _d in _jenumD)
                    {
                        _enumList.Add(_d.ToString());
                    }

                    _rule.enumDescribes.Add(_ename, _enumList);
                }
            }

            return _rule;
        }
    }
}