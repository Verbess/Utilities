using System;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace Verbess.Utils.Editor
{
    internal class CodeSnippets : UnityEditor.Editor
    {
        private const string DefaultFolder = "Assets/";
        private static readonly string SnippetsFolder;

        static CodeSnippets()
        {
            SnippetsFolder = Path.Combine(Application.dataPath, "Plugins/Verbess/Editor/CodeSnippets/Snippets");
        }

        private static string GetSelectionPath()
        {
            if (Selection.objects.Length == 0)
            {
                // If don't select an object, return the default folder path.
                return DefaultFolder;
            }
            else
            {
                return AssetDatabase.GetAssetPath(Selection.objects[0]);
            }
        }

        private static string GetDirectoryPath(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (path.Contains("."))
            {
                // If path contains "." means that path contains file extension.
                path = path.Replace(Path.GetFileName(path), String.Empty);
            }

            return path;
        }

        private static void CreateSnippets(string defaultName, string snippetName, FileExtension extension)
        {
            try
            {
                // Get the directory path of selection.
                // Only get the first selection's directory path.
                // In other words, script will be create under the first selection's directory path.
                string path = GetSelectionPath();
                path = GetDirectoryPath(path);

                int fileIndex = 1;
                string fileNameWithExtension = String.Concat(defaultName, ".", extension.ToString());
                string targetPath = Path.Combine(path, fileNameWithExtension);
                while (true)
                {
                    if (!File.Exists(targetPath))
                    {
                        string snippetPath = Path.Combine(SnippetsFolder, snippetName);
                        string content = File.ReadAllText(snippetPath);
                        ProjectWindowUtil.CreateAssetWithContent(targetPath, content);
                        break;
                    }
                    else
                    {
                        fileIndex++;
                        fileNameWithExtension = String.Concat(defaultName, fileIndex.ToString(), ".", extension.ToString());
                        targetPath = Path.Combine(path, fileNameWithExtension);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return;
            }
        }

        [MenuItem("Assets/Create/Custom Operate/Create Snippets/C# Class - MonoBehaviour", false, 0)]
        public static void CreateCSharp_MonoBehaviourClass()
        {
            CreateSnippets("NewBehaviourScript", "C# Script-NewBehaviourScript.cs.txt", FileExtension.cs);
        }

        [MenuItem("Assets/Create/Custom Operate/Create Snippets/C# Class", false, 100)]
        public static void CreateCSharp_Class()
        {
            CreateSnippets("NewClassScript", "C# Script-NewClassScript.cs.txt", FileExtension.cs);
        }

        [MenuItem("Assets/Create/Custom Operate/Create Snippets/C# Struct", false, 100)]
        public static void CreateCSharp_Struct()
        {
            CreateSnippets("NewStructScript", "C# Script-NewStructScript.cs.txt", FileExtension.cs);
        }

        [MenuItem("Assets/Create/Custom Operate/Create Snippets/C# Interface", false, 100)]
        public static void CreateCSharp_Interface()
        {
            CreateSnippets("NewInterfaceScript", "C# Script-NewInterfaceScript.cs.txt", FileExtension.cs);
        }

        [MenuItem("Assets/Create/Custom Operate/Create Snippets/C# Enum", false, 100)]
        public static void CreateCSharp_Enum()
        {
            CreateSnippets("NewEnumScript", "C# Script-NewEnumScript.cs.txt", FileExtension.cs);
        }

        [MenuItem("Assets/Create/Custom Operate/Create Snippets/C# NameSpace", false, 100)]
        public static void CreateCSharp_NameSpace()
        {
            CreateSnippets("NewNameSpaceScript", "C# Script-NewNameSpaceScript.cs.txt", FileExtension.cs);
        }
    }
}