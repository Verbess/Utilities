using System.IO;
using UnityEditor;
using UnityEngine;

namespace Verbess.Unity.Editor
{
    public partial class RightClickMenu : UnityEditor.Editor
    {
        private enum FileExtension
        {
            cs = 0,
        }

        /// <summary>
        /// Get the directory path of selection.
        /// </summary>
        /// <returns>The directory path of selection.</returns>
        private static string GetSelectionDirectoryPath()
        {
            string[] assetGUIDs = Selection.assetGUIDs;
            string path = AssetDatabase.GUIDToAssetPath(assetGUIDs[0]);

            if (path.Contains("."))
            {
                string[] splitPaths = path.Split('/');
                path = "";
                for (int i = 0; i < splitPaths.Length - 1; i++)
                {
                    path += splitPaths[i] + "/";
                }
            }
            else
            {
                path += "/";
            }

            return path;
        }

        /// <summary>
        /// Use to create template script.
        /// </summary>
        /// <param name="defaultFileName">The default name when file create.</param>
        /// <param name="templateFileName">The file name of template.</param>
        /// <param name="fileExtension">The extension type of file, it's a Enum.</param>
        private static void CreateScriptTemplates(string defaultFileName, string templateFileName, FileExtension fileExtension)
        {
            string path = GetSelectionDirectoryPath();
            string templateFilePath = Application.dataPath + $"/Editor/CreateTemplateScripts/ScriptTemplates/{templateFileName}";
            string TargetFilePath = path + defaultFileName + $".{fileExtension.ToString()}";

            bool IsCreated = false;
            int index = 1;
            while (!IsCreated)
            {
                if (!File.Exists(TargetFilePath))
                {
                    string content = File.ReadAllText(templateFilePath);
                    ProjectWindowUtil.CreateAssetWithContent(TargetFilePath, content);
                    IsCreated = true;
                }
                else
                {
                    TargetFilePath = path + defaultFileName + $"{index}.{fileExtension.ToString()}";
                    index++;
                }
            }
        }

        [MenuItem("Assets/Create/Create Script Templates/C# Class - MonoBehaviour", false, 0)]
        public static void CreateScriptTemplates_MonoBehaviourCSharp() => CreateScriptTemplates("NewBehaviourScript", "C# Script-NewBehaviourScript.cs.txt", FileExtension.cs);

        [MenuItem("Assets/Create/Create Script Templates/C# Class", false, 100)]
        public static void CreateScriptTemplates_CSharpClass() => CreateScriptTemplates("NewClassScript", "C# Script-NewClassScript.cs.txt", FileExtension.cs);

        [MenuItem("Assets/Create/Create Script Templates/C# Interface", false, 100)]
        public static void CreateScriptTemplates_CSharpInterface() => CreateScriptTemplates("NewInterfaceScript", "C# Script-NewInterfaceScript.cs.txt", FileExtension.cs);

        [MenuItem("Assets/Create/Create Script Templates/C# Enum", false, 100)]
        public static void CreateScriptTemplates_CSharpEnum() => CreateScriptTemplates("NewEnumScript", "C# Script-NewEnumScript.cs.txt", FileExtension.cs);
    }
}
