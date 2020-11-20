using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace Verbess.Unity.Editor
{
    public static class SliceTexture
    {
        public const string ResourcesPath = "Assets/Resources/";


        [MenuItem("Assets/Create/Custom Operate/Slice to PNGs", false, 900)]
        public static void SliceToPNG()
        {
            foreach (Object obj in Selection.objects)
            {
                string selectionPath = AssetDatabase.GetAssetPath(obj);

                if (selectionPath.StartsWith(ResourcesPath))
                {
                    string selectionExtension = Path.GetExtension(selectionPath).ToLower();
                    string loadPath = selectionPath.Replace(ResourcesPath, "")
                                                   .Replace(selectionExtension, "");
                    try
                    {
                        switch (selectionExtension)
                        {
                            case "":
                                Debug.Log($"The selection: \"{selectionPath}\" is not a file.");
                                continue;
                            case ".png":
                                SliceTexture.Slice(loadPath, ".png");
                                break;
                            case ".jpg":
                                SliceTexture.Slice(loadPath, ".jpg");
                                break;
                            default:
                                Debug.Log($"The selection: \"{selectionPath}\"'s extension is not supported. This method currently supports only the following file formats: [.png .jpg]");
                                continue;
                        }
                    }
                    catch (System.Exception exception)
                    {
                        Debug.Log(exception.ToString());
                    }
                }
                else
                {
                    Debug.Log($"(\"{selectionPath}\") is not in the correct path, Put it under the Resources folder(\"{ResourcesPath}\").");
                    continue;
                }
            }
        }

        private static void Slice(string loadPath, string extensionForWrite)
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>(loadPath);

            if (sprites.Length > 0)
            {
                string outputPath = Path.Combine(Application.dataPath, "Resources/Output/", loadPath);
                try
                {
                    if (!Directory.Exists(outputPath))
                    {
                        Directory.CreateDirectory(outputPath);
                    }
                }
                catch (System.Exception exception)
                {
                    Debug.Log(exception.ToString());
                    throw;
                }

                int i = 1;
                foreach (Sprite sprite in sprites)
                {
                    Texture2D texture2D = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height, sprite.texture.format, false);
                    Color[] colors = sprite.texture.GetPixels((int)sprite.rect.xMin, (int)sprite.rect.yMin, (int)sprite.rect.width, (int)sprite.rect.height);
                    texture2D.SetPixels(colors);
                    texture2D.Apply();

                    string writePath = Path.Combine(outputPath, sprite.name) + extensionForWrite;
                    byte[] content = null;
                    switch (extensionForWrite.ToLower())
                    {
                        case ".png":
                            content = texture2D.EncodeToPNG();
                            break;
                        case ".jpg":
                            content = texture2D.EncodeToJPG();
                            break;
                    }
                    try
                    {
                        File.WriteAllBytes(writePath, content);
                    }
                    catch (System.Exception exception)
                    {
                        Debug.Log(exception.ToString());
                        throw;
                    }
                    Debug.Log($"Sprite: {sprite.name} write succeeded! Process: {i}/{sprites.Length}.");
                    i++;
                }
                Debug.Log("All sprites write succeeded!");
                AssetDatabase.Refresh();
            }
        }
    }
}