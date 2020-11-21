# UI 学习总结
## UI 适配
- 对于未使用锚点进行屏幕边缘跟踪的 UI 物体
1. 使用 Canvas Scaler 进行屏幕适配
2. 设置 UI Scale Mode 为 Scale With Screen Size，使 Canvas 画布随着屏幕分辨率进行缩放
3. 设置一个合适的 Reference Resolution（比如 1920 : 1080）
4. 设置 Screen Match Mode 为 Match Width Or Height
5. 设置 Reference Pixels Per Unit 为合适的大小（可以参考素材的大小进行换算）