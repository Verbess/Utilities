# Create Template Scripts

Put this folder under the directory `"/Assets/Editor"` and you could find `"Custom Operate/Create Snippets"` button when you right click the Project window.

If you want to extend for more templates, add codes in `"RightClickMenu.cs"` file, and script template in `"/Snippets"` folder.

Function code sample:
```
[MenuItem("Assets/Custom Operate/Create Snippets/C# Class - MonoBehaviour", false, 0)]
public static void CreateSnippets_Sample() => CreateSnippets("NewSampleScript", "C# Script-NewSampleScript.cs.txt", FileExtension.cs);
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