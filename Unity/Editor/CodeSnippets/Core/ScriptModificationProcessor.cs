using System;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace Verbess.Utils.Editor
{
    internal class ScriptModificationProcessor : UnityEditor.AssetModificationProcessor
    {
        public static void OnWillCreateAsset(string metaFilePath)
        {
            try
            {
                string assetPath = metaFilePath.EndsWith(".meta") ? metaFilePath.Replace(".meta", String.Empty) : metaFilePath;

                string assetExtension = Path.GetExtension(assetPath);
                if (assetExtension != ".cs")
                {
                    return;
                }

                // Make sure path not contains two "Assets/".
                int index = Application.dataPath.LastIndexOf("Assets");
                string absolutePath = Path.Combine(Application.dataPath.Substring(0, index), assetPath);

                string content = File.ReadAllText(absolutePath);

                string fileNameSpace = String.Concat(Application.companyName, ".", Application.productName);
                string scriptName = Path.GetFileNameWithoutExtension(assetPath);
                content = content.Replace("#NAMESPACE#", fileNameSpace)
                                 .Replace("#SCRIPTNAME#", scriptName);

                File.WriteAllText(absolutePath, content);

                AssetDatabase.Refresh();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return;
            }
        }
    }
}