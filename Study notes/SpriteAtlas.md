# SpriteAtlas 使用总结
## SpriteAtlas for **UI**
当将 SpriteAtlas 作用于 UI 图片时，请确保注意以下几点，否则将会在 UI 物体边缘生成一些异常的像素。

- **Sprite** 的 **Mesh Type** 格式应为 **Full Rect**
- **SpriteAtlas** 取消勾选 Packing 中的 **Allow Rotation** 和 **Tight Packing** 两项

原因可能是因为 SpriteAtlas 边缘算法的问题。  
内容依据：[Sprifte Atlas Packing bug......](https://forum.unity.com/threads/sprifte-atlas-packing-bug.873448/)