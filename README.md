# AC Maker Autoscroll

Auto-scrolls the thumbnail list in AiComi Maker to always show the selected item. No more manual scrolling to see your choice!

![demo](https://github.com/user-attachments/assets/3b6c7538-6324-435c-988f-9329f4435623)


## Features

- Should work on all Maker categories (Hair, Accessories, Eyes, etc.)
- Smooth scrolling that keeps your selection at the top when looking through your hairstyles etc. using the "Next/Prev" button
- Debug logging toggle (Advanced settings) if needed
- Automatic DLL deployment to `Project-Root`

## Installation

1. Download `AC_MakerAutoscroll.dll`
2. Copy to `AiComi/BepInEx/plugins/`
3. Start AiComi!

## Build from source

```bash
dotnet build AC_MakerAutoscroll.csproj
```

→ `AC_MakerAutoscroll.dll` in Projekt-Root
→ Copy to "AICOMI!\BepInEx\plugins"
*wherever you have it installed*

## Configuration (optional)

**F1 → Enable Advanced Options**
**AC_MakerAutoscroll → Debug → Enable Debug Logging**

## Compatibility

- AiComi (IL2CPP)
- Ver 2.0.1+ (w/ Night Tour DLC)
- Probably also 1.0.7+
- BepInEx 6.x
- .NET 6.0

## License

MIT
