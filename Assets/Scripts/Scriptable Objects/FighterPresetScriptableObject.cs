using UnityEngine;

namespace Scriptable_Objects
{
    [CreateAssetMenu]
    public class FighterPresetScriptableObject : ScriptableObject
    {
        public float AttackRadius;
        public float MaxHealth;
        public float AttackPower;
        public float AttackRate;
    }
}
