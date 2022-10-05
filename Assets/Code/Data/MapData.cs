using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "MapData", menuName = "MapData")]
public class MapData : ScriptableObject
{
    public Sprite snapshot;
    public bool locked;
    [HideInInspector] public int mapIndex;
    [HideInInspector] public string mapName;

    public string[] GetMapsInBuildSettingsArray()
    {
        List<string> maps = new List<string>();
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string name = System.IO.Path.GetFileNameWithoutExtension(path);
            maps.Add(name);
        }

        return maps.ToArray();
    }
}
