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
            int fileNameExtensionindex = filePath.LastIndexOf(".");
            if (fileNameExtensionindex < 0)
            {
                return;
            }
            // check file name extension, function return if it is not ".cs"
            string fileNameExtension = filePath.Substring(fileNameExtensionindex + 1);
            if (fileNameExtension != "cs")
            {
                return;
            }

            int fileIndex = filePath.LastIndexOf("/");
            int index = Application.dataPath.LastIndexOf("Assets");
            string globalFilePath = Application.dataPath.Substring(0, index) + filePath;
            string file = File.ReadAllText(globalFilePath);
            string _namespace = Application.companyName + "." + Application.productName;
            string _fileName = filePath.Substring(fileIndex + 1, fileNameExtensionindex - fileIndex - 1);
            file = file.Replace("#NAMESPACE#", _namespace);
            file = file.Replace("#SCRIPTNAME#", _fileName);
            File.WriteAllText(globalFilePath, file);
            AssetDatabase.Refresh();
        }
    }
}
