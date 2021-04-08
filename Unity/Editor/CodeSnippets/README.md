# Code Snippets
This feature helps you create code snippets.

## How to use
Put this folder under the directory `"Editor/"` and you could find `"Assets/Create/Custom Operate/Create Snippets"` button when you right click the Project window.

**ATTENTION**: According to the difference of the `"Editor/"` folder structure in each project, you may get a `FilePathNotFoundException`. Fix it by change static field `"SnippetsFolder"` to the correct folder path of `"Snippets/"` folder in `"CodeSnippets.cs"`.

If you want to extend for more templates, add codes in "RightClickMenu.cs" file, and script template in "/Snippets" folder.

Function code sample:
```
[MenuItem("Assets/Custom Operate/Create Snippets/C# Class - MonoBehaviour", false, 0)]
public static void CreateSnippets_Sample()
{
    CreateSnippets("NewSampleScript", "C# Script-NewSampleScript.cs.txt", FileExtension.cs);
}
```

If you want to extend file extension types, add code to "FileExtension" enum.

```
private enum FileExtension
{
    cs = 0,
    ...
    ...
}
```