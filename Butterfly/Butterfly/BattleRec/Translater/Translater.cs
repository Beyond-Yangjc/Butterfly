using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using Salar.Bois;
using Microsoft.CSharp;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
#endif

namespace ByYoung.Butterfly
{
    public class Translater
    {
        public static void GenerateCode(List<string> enums)
        {
            CodeCompileUnit unit = new CodeCompileUnit();
            CodeNamespace _nameSpace = new CodeNamespace("ByYoung.Butterfly.AutoGenerate");
            CodeTypeDeclaration _class = new CodeTypeDeclaration("TranslaterDefine") { IsClass = true, TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed };
            _nameSpace.Types.Add(_class);
            unit.Namespaces.Add(_nameSpace);
#if UNITY_EDITOR
            string outputFile = Application.dataPath + "/Scripts/TranslaterDefine.cs";
#endif
            string outputFile = "";

            if (File.Exists(outputFile))
            {
                File.Delete(outputFile);
#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif
            }
            //生成特性
            CodeTypeDeclaration e_MsgTypes = new CodeTypeDeclaration("MsgTypes") { IsClass = true, TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed };
            e_MsgTypes.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(DescriptionAttribute)),
                new CodeAttributeArgument(new CodePrimitiveExpression("操作类型"))));
            //指定这是一个Enum
            e_MsgTypes.IsEnum = true;
            e_MsgTypes.TypeAttributes = TypeAttributes.Public;
            //添加字段

            for (var i = 0; i < enums.Count; i++)
            {
                CodeMemberField _enumField = new CodeMemberField(typeof(Enum), $"{enums[i]} = {i}");
                e_MsgTypes.Members.Add(_enumField);
            }

            //生成特性 
            _class.Members.Add(e_MsgTypes);
            //生成代码 
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");

            CodeGeneratorOptions options = new CodeGeneratorOptions();

            options.BracingStyle = "C";

            options.BlankLinesBetweenMembers = true;

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(outputFile))
            {
                provider.GenerateCodeFromCompileUnit(unit, sw, options);
            }
#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif
        }

        private static CacheFile curCacheFile = null;

        /// <summary>
        /// 开始记录
        /// </summary>
        public static void BeginCache<T>(ulong _markID = 0, int _eCombatType = 0, ulong[] _userIds = null) where T : CacheFile, new()
        {
            curCacheFile = new T
            {
                BattleID = _markID,
                eCombatType = _eCombatType,
                UserIDs = _userIds,
                dateTime = DateTime.Now.ToString(CultureInfo.CurrentCulture),
                //recList = new List<CacheRec>()
            };
        }

        public static void AddNewCache(CacheRec _cacheRec)
        {
            if (curCacheFile?.recList == null)
            {
#if UNITY_EDITOR
                Debug.Log("Butterfly：no valid CacheFile, pls Check BeginCache() firstly!");
#else
                MessageBox.Show("Butterfly：no valid CacheFile, pls Check BeginCache() firstly!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
            }

            curCacheFile?.recList?.Add(_cacheRec);
        }

        /// <summary>
        /// 结束记录
        /// </summary>
        public static CacheFile EndCache()
        {
            if (curCacheFile?.recList == null)
            {
#if UNITY_EDITOR
                Debug.Log("Butterfly：no valid CacheFile, pls Check BeginCache() firstly!");
#else
                MessageBox.Show("Butterfly：no valid CacheFile, pls Check BeginCache() firstly!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
                return null;
            }

            return curCacheFile;
        }

        public static void SaveCache2File(CacheFile _cache)
        {
#if UNITY_EDITOR
            string savePath = Application.streamingAssetsPath + "/rec.btf";
#else
            string savePath = Application.StartupPath + "/rec.btf"; ;
#endif
            using (var _fs = new FileStream(savePath, FileMode.Create))
            {
                BoisSerializer.Initialize<CacheFile>();
                var _serializer = new BoisSerializer();
                _serializer.Serialize<CacheFile>(_cache, _fs);
                _fs.Close();
            }
        }
    }
}