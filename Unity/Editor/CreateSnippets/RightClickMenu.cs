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
        /// Used to create template script.
        /// </summary>
        /// <param name="defaultFileName">The default name when file create.</param>
        /// <param name="templateFileName">The file name of template.</param>
        /// <param name="fileExtension">The extension type of file, it's a Enum.</param>
        private static void CreateSnippets(string defaultFileName, string templateFileName, FileExtension fileExtension)
        {
            // Get the directory path of selection.
            // Only get the first selection's directory path.
            #region GetDirectoryOfSelection
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
            #endregion

            string templateFilePath = Application.dataPath + $"/Editor/CreateSnippets/Snippets/{templateFileName}";
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

        [MenuItem("Assets/Create/Custom Operate/Create Snippets/C# Class - MonoBehaviour", false, 0)]
        public static void CreateSnippets_MonoBehaviourCSharp() => CreateSnippets("NewBehaviourScript", "C# Script-NewBehaviourScript.cs.txt", FileExtension.cs);

        [MenuItem("Assets/Create/Custom Operate/Create Snippets/C# Class", false, 100)]
        public static void CreateSnippets_CSharpClass() => CreateSnippets("NewClassScript", "C# Script-NewClassScript.cs.txt", FileExtension.cs);

        [MenuItem("Assets/Create/Custom Operate/Create Snippets/C# Interface", false, 100)]
        public static void CreateSnippets_CSharpInterface() => CreateSnippets("NewInterfaceScript", "C# Script-NewInterfaceScript.cs.txt", FileExtension.cs);

        [MenuItem("Assets/Create/Custom Operate/Create Snippets/C# Enum", false, 100)]
        public static void CreateSnippets_CSharpEnum() => CreateSnippets("NewEnumScript", "C# Script-NewEnumScript.cs.txt", FileExtension.cs);
    }
}
