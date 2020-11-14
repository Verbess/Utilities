using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Verbess.Unity.Editor
{
    public class ScriptModificationProcessor : UnityEditor.AssetModificationProcessor
    {
        /// <summary>
        /// Called when you create script asset.
        /// </summary>
        /// <param name="metaFilePath">script asset's meta file path.</param>
        public static void OnWillCreateAsset(string metaFilePath)
        {
            string filePath = metaFilePath.EndsWith(".meta") ? metaFilePath.Replace(".meta", "") : metaFilePath;

            // If file path has no file name extension, function return.
            int fileExtensionIndex = filePath.LastIndexOf(".");
            if (fileExtensionIndex < 0)
            {
                return;
            }
            // check file name extension, function return if it is not ".cs".
            string fileExtension = filePath.Substring(fileExtensionIndex + 1);
            if (fileExtension != "cs")
            {
                return;
            }

            int fileNameIndex = filePath.LastIndexOf("/");
            string fileNamespace = Application.companyName + "." + Application.productName;
            string fileScriptName = filePath.Substring(fileNameIndex + 1, fileExtensionIndex - fileNameIndex - 1);

            int index = Application.dataPath.LastIndexOf("Assets");
            string absoluteFilePath = Application.dataPath.Substring(0, index) + filePath;

            string content = File.ReadAllText(absoluteFilePath);
            content = content.Replace("#NAMESPACE#", fileNamespace)
                             .Replace("#SCRIPTNAME#", fileScriptName);
            File.WriteAllText(absoluteFilePath, content);

            AssetDatabase.Refresh();
        }
    }
}