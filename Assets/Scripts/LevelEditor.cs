#if UNITY_EDITOR
using Behaviours;
using EasyButtons;
using Scriptable_Objects;
using UnityEditor.AI;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEditor : MonoBehaviour
{
    [SerializeField] private LevelSettingsScriptableObject levelSettings;
    [SerializeField] private GameObject ground;
    [SerializeField] private Vector3 groundDefaultScaleSize;
    [SerializeField] private FighterPresetScriptableObject[] fighterPresets;

    [Button]
    public void SetAreaSize()
    {
        Vector3 newScale = new Vector3(levelSettings.LevelSize.x / groundDefaultScaleSize.x,
            1f / groundDefaultScaleSize.y, levelSettings.LevelSize.y / groundDefaultScaleSize.z);
        ground.transform.localScale = newScale;
        NavMeshBuilder.ClearAllNavMeshes();
        NavMeshBuilder.BuildNavMesh();

        EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
    }

    [Button]
    public void SpawnFighters()
    {
        ClearFighters();

        for (int i = 0; i < levelSettings.FightersCount; i++)
        {
            GameObject fighterInstance = Instantiate(levelSettings.FighterPrefab);
            Vector3 position = new Vector3(
                Random.Range(-levelSettings.LevelSize.x / 2f, levelSettings.LevelSize.x / 2f),
                0f,
                Random.Range(-levelSettings.LevelSize.y / 2f, levelSettings.LevelSize.y / 2f));
            fighterInstance.transform.position = position;
            
            SetFighterProperties(fighterInstance.GetComponent<Fighter>());
        }

        EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
    }

    [Button]
    public void ClearFighters()
    {
        foreach (var fighter in FindObjectsOfType<Fighter>())
        {
            DestroyImmediate(fighter.gameObject);
        }

        EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
    }

    private void SetFighterProperties(Fighter fighter)
    {
        int presetToUse = Random.Range(0, fighterPresets.Length);

        fighter.AttackPower = fighterPresets[presetToUse].AttackPower;
        fighter.AttackRadius = fighterPresets[presetToUse].AttackRadius;
        fighter.AttackRate = fighterPresets[presetToUse].AttackRate;
        fighter.MaxHealth = fighterPresets[presetToUse].MaxHealth;
    }
}
#endif