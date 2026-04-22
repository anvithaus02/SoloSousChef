using UnityEngine;
using UnityEditor;
using System.Text;
using System.IO;

namespace com.SoloSousChef.EditorTools
{
    public class ScriptFinder : Editor
    {
        private const string TargetFolder = "Assets/SoloSousChef";

        [MenuItem("Tools/SoloSousChef/Copy All Script Names")]
        public static void LogAllScripts() => GatherAssets("t:MonoScript", "Scripts");

        [MenuItem("Tools/SoloSousChef/Copy All Prefab Names")]
        public static void LogAllPrefabs() => GatherAssets("t:Prefab", "Prefabs");

        private static void GatherAssets(string filter, string label)
        {
            string[] guids = AssetDatabase.FindAssets(filter, new[] { TargetFolder });

            if (guids.Length == 0)
            {
                Debug.LogWarning($"No {label} found in {TargetFolder}");
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"--- SoloSousChef {label} ({guids.Length} found) ---");
            sb.AppendLine();

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                string fileName = Path.GetFileName(path);
                sb.AppendLine(fileName);
            }

            sb.AppendLine();
            sb.AppendLine("--- End of List ---");

            // Output to console
            string result = sb.ToString();
            Debug.Log(result);
            
            // Automatically copy to clipboard for convenience
            GUIUtility.systemCopyBuffer = result;
            Debug.Log($"<color=#4AF626>Success!</color> The list of {label} has been copied to your clipboard.");
        }

        // Context Menu options (Right-click any component)
        [MenuItem("CONTEXT/Component/Copy SoloSousChef Script List")]
        private static void ContextScripts(MenuCommand command) => LogAllScripts();

        [MenuItem("CONTEXT/Component/Copy SoloSousChef Prefab List")]
        private static void ContextPrefabs(MenuCommand command) => LogAllPrefabs();
    }
}