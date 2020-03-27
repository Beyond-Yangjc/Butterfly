using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Salar.Bois;
#if UNITY_EDITOR
using UnityEngine;
#endif

namespace ByYoung.Butterfly
{
    public static class Loader
    {
        public static CacheFile LoadBtfFile(Stream _stream)
        {
            BoisSerializer.Initialize<CacheFile>();
            BoisSerializer boisSerializer = new BoisSerializer();
            var _cache = boisSerializer.Deserialize<CacheFile>(_stream);
            return _cache;
        }
        public static CacheFile LoadBtfFile(string _recPath)
        {
            using (var _stream = new FileStream(_recPath, FileMode.Open, FileAccess.Read))
            {
                BoisSerializer.Initialize<CacheFile>();
                BoisSerializer boisSerializer = new BoisSerializer();
                var _cache = boisSerializer.Deserialize<CacheFile>(_stream);
                _stream.Close();
                return _cache;
            }
        }

        public static async Task<Rule> LoadRuleFile(string _filePath)
        {
            var _rule = await ReadJsonFileFromPath(_filePath, data =>
            {
                var _rObj = Json2Rule(JsonConvert.DeserializeObject(data) as Newtonsoft.Json.Linq.JObject);
                _rObj.content = data;
                return _rObj;
            });
            return _rule;
        }

        public static async Task<Rule> LoadRuleFile(Stream _stream)
        {
            var _rule = await ReadJsonFileFronStream<Rule>(_stream, data =>
            {
                var _rObj = Json2Rule(JsonConvert.DeserializeObject(data) as Newtonsoft.Json.Linq.JObject);
                _rObj.content = data;
                return _rObj;
            });
            return _rule;
        }

        public static async Task<T> ReadJsonFileFronStream<T>(Stream _stream, Func<string, T> _convert) where T : class
        {
            using (var _sr = new StreamReader(_stream, Encoding.UTF8))
            {
                var _content = await _sr.ReadToEndAsync();
                _sr.Close();
                var _rule = string.IsNullOrEmpty(_content) == false ? _convert.Invoke(_content) : null;
                return _rule;
            }
        }

        public static async Task<T> ReadJsonFileFromPath<T>(string _filePath, Func<string, T> _convert) where T : class
        {
            using (var _fs = new FileStream(_filePath, FileMode.Open))
            {
                using (var _sr = new StreamReader(_fs, Encoding.UTF8))
                {
                    var _content = await _sr.ReadToEndAsync();
                    _sr.Close();
                    _fs.Close();
                    var _rule = string.IsNullOrEmpty(_content) == false ? _convert.Invoke(_content) : null;
                    return _rule;
                }
            }
        }

#if UNITY_EDITOR
        private static Rule Json2Rule(JsonData _jData)
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
                    var _describe = _jr["describe"].ToString();
                    var _junits = _jr["units"];
                    var _ruleUnit = new RuleUnit
                    {
                        name = _name,
                        describe = _describe,
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
#else
        private static Rule Json2Rule(Newtonsoft.Json.Linq.JObject _jData)
        {
            var _rule = new Rule();
            var _jmsgTypes = _jData["MsgTypes"];
            if (_jmsgTypes != null && _jmsgTypes is Newtonsoft.Json.Linq.JArray)
            {
                foreach (var _t in _jmsgTypes)
                {
                    _rule.MsgTypes.Add(_t.ToString());
                }
            }

            var _jruleUnits = _jData["rules"];
            if (_jruleUnits != null && _jruleUnits is Newtonsoft.Json.Linq.JArray)
            {
                foreach (var _jr in _jruleUnits)
                {
                    var _name = _jr["name"].ToString();
                    if (_rule.rules.ContainsKey(_name)) continue;
                    var _describe = _jr["describe"].ToString();
                    var _junits = _jr["units"];
                    var _ruleUnit = new RuleUnit
                    {
                        name = _name,
                        describe = _describe,
                    };
                    if (_junits is Newtonsoft.Json.Linq.JArray)
                    {
                        foreach (var _junit in _junits)
                        {
                            _ruleUnit.units.Add(_junit.ToString());
                        }
                    }

                    _rule.rules.Add(_name, _ruleUnit);
                }
            }

            var _jenums = _jData["enums"];
            if (_jenums != null && _jenums is Newtonsoft.Json.Linq.JArray)
            {
                foreach (var _jenum in _jenums)
                {
                    var _ename = _jenum["name"].ToString();
                    var _jenumD = _jenum["enum"];
                    var _enumList = new List<string>();
                    foreach (var _d in _jenumD)
                    {
                        _enumList.Add(_d.ToString());
                    }

                    _rule.enumDescribes.Add(_ename, _enumList);
                }
            }

            return _rule;
        }
#endif
    }
}