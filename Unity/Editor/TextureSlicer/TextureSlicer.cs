using System;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace Verbess.Utils.Editor
{
    public static class TextureSlicer
    {
        private const string ResourcesPath = "Assets/Resources/";

        [MenuItem("Assets/Create/Custom Operate/Slice To Textures", false, 10)]
        public static void SliceToTextures()
        {
            foreach (UnityEngine.Object obj in Selection.objects)
            {
                string selectionPath = AssetDatabase.GetAssetPath(obj);

                if (selectionPath.StartsWith(ResourcesPath))
                {
                    string selectionExtension = Path.GetExtension(selectionPath);

                    try
                    {
                        string loadPath;
                        switch (selectionExtension)
                        {
                            case ".png":
                                loadPath = selectionPath.Replace(ResourcesPath, String.Empty)
                                                        .Replace(selectionExtension, String.Empty);
                                SliceCore(loadPath, ".png");
                                break;
                            case ".jpg":
                                loadPath = selectionPath.Replace(ResourcesPath, String.Empty)
                                                        .Replace(selectionExtension, String.Empty);
                                SliceCore(loadPath, ".jpg");
                                break;
                            default:
                                throw new Exception($"The selection: \"{selectionPath}\"'s extension is not supported. This method currently supports only the following file formats: [.png .jpg]");
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                    }
                }
                else
                {
                    Debug.LogError($"(\"{selectionPath}\") is not in the correct path, Put it under the Resources folder(\"{ResourcesPath}\").");
                }
            }
        }

        private static void SliceCore(string loadPath, string extension)
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>(loadPath);

            if (sprites.Length > 0)
            {
                string outputPath = Path.Combine(Application.dataPath, "Resources/SlicedOutput/", loadPath);

                // Create directory if not exist.
                try
                {
                    if (!Directory.Exists(outputPath))
                    {
                        Directory.CreateDirectory(outputPath);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                    return;
                }

                // Create a texture2d and write it to 
                int index = 1;
                foreach (Sprite sprite in sprites)
                {
                    // If texture is compressed, SetPixels & EncodeToXXX function will throw a unmanaged exception
                    int width = (int)sprite.rect.width;
                    int height = (int)sprite.rect.height;
                    Texture2D texture2D = new Texture2D(width, height, sprite.texture.format, false);

                    int x = (int)sprite.rect.xMin;
                    int y = (int)sprite.rect.yMin;
                    Color[] colors = sprite.texture.GetPixels(x, y, width, height);
                    texture2D.SetPixels(colors);
                    texture2D.Apply();

                    byte[] content = null;
                    switch (extension.ToLower())
                    {
                        case ".png":
                            content = texture2D.EncodeToPNG();
                            break;
                        case ".jpg":
                            content = texture2D.EncodeToJPG();
                            break;
                    }

                    if (content == null)
                    {
                        throw new Exception($"An error occurred during texture \"{loadPath}\" encoding! Maybe it's an texture formats error, texture should not be compressed. Process: {index}/{sprites.Length}.");
                    }

                    try
                    {
                        string writePath = String.Concat(Path.Combine(outputPath, sprite.name), extension);
                        File.WriteAllBytes(writePath, content);
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                        Debug.LogError($"An error occurred during Sprite: \"{loadPath}\" writing! Process: {index}/{sprites.Length}.");
                        return;
                    }

                    Debug.Log($"Sprite: {sprite.name} wrote succeeded! Process: {index}/{sprites.Length}.");
                    index++;
                }

                Debug.Log($"All sprites of \"{loadPath}\" wrote succeeded!");
                AssetDatabase.Refresh();
            }
        }
    }
}
