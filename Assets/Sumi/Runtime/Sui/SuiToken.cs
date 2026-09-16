using UnityEngine;

namespace Sumi
{
    public class SuiToken
    {
        public string Value;
        public SuiTokenType Type;

        public SuiToken(SuiTokenType type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}
