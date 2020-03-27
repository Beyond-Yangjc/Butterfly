using System.Collections;
using System.Collections.Generic; 

namespace ByYoung.Butterfly
{
   public sealed class Rule
    {
        public string content;

        public List<string> MsgTypes = new List<string>();
        /// <summary>
        /// keyName, RulerUnit
        /// </summary>
        public Dictionary<string, RuleUnit> rules = new Dictionary<string, RuleUnit>();
        
        /// <summary>
        /// 枚举描述,
        /// such as
        /// "E_BuffOp": [
        ///       "添加",
        ///       "移除",
        ///       "一次性施放"
        /// ]
        /// </summary>
        public Dictionary<string, List<string>> enumDescribes = new Dictionary<string, List<string>>(); 
    }

   public sealed class RuleUnit
    {
        public string name;
        public List<string> units = new List<string>();
        public string describe;//描述
    } 
}