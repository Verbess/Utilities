# Create Template Scripts

Put this folder under the directory `"/Assets/Editor"` and you could find `"Create Script Templates"` button when you right click the Project window.

If you want to extend for more templates, add codes in `"RightClickMenu.cs"` file, and script template in `"/ScriptTemplates"` folder.

Function code sample:
```
[MenuItem("Assets/Create/Create Script Templates/C# Class - MonoBehaviour", false, 0)]
public static void CreateScriptTemplates_Sample() => CreateScriptTemplates("NewSampleScript", "C# Script-NewSampleScript.cs.txt", FileExtension.cs);
```

If you want to extend file extension types, add code for `"FileExtension"` enum in `"RightClickMenu.cs"`.

```
private enum FileExtension
        {
            cs = 0,
            ...
            ...
        }
```