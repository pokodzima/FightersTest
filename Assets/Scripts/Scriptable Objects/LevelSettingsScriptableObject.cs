using UnityEngine;

namespace Scriptable_Objects
{
    [CreateAssetMenu]
    public class LevelSettingsScriptableObject : ScriptableObject
    {
        public Vector2 LevelSize;
        public int FightersCount;
        public GameObject FighterPrefab;
    }
}
