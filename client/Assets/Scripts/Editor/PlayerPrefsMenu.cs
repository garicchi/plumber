using UnityEngine;
using UnityEditor;
 
public class PlayerPrefsMenu : EditorWindow
{
    [MenuItem("Window/Delete PlayerPrefs (All)")]
    static void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}