# Texture Slicer
This feature helps you slice texture that sliced by Sprite Editor to several textures.

## How to use
Put this folder under the directory `"Editor/"` and you could find `"Assets/Create/Custom Operate/Slice To Textures"` button when you right click the Project window.

**ATTENTION**: There are some rules to watch out for using this function:
- Make sure the texture should be **under of `"Assets/Resources/"` folder**.
- Make sure the texture's settings that you select are correct.  
  - **ENABLE `Read/Write Enabled` and `Alpha Is Transparency` options** of the **`Advanced`** Item. Otherwise, it will report an exception.
  - Texture should not be compressed, **`Compression`** should set as **`None`**. Otherwise, it will report an exception.