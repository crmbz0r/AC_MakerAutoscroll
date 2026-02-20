using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using CharacterCreation;
using HarmonyLib;
using UnityEngine;

namespace AC_MakerAutoscroll;

[BepInPlugin(GUID, PluginName, Version)]
public class Plugin : BasePlugin
{
    public const string GUID = "com.crmbz0r.bepinex.ac.makerautoscroll";
    public const string PluginName = "AC_MakerAutoscroll";
    public const string Version = "1.0.0";

    // Store logger statically so the patch class can access it
    internal static BepInEx.Logging.ManualLogSource Logger = null!;
    internal static BepInEx.Configuration.ConfigEntry<bool> DebugMode = null!;

    public override void Load()
    {
        Logger = Log;
        DebugMode = Config.Bind(
            "Debug",
            "Enable Debug Logging",
            false,
            new ConfigDescription("Logs scroll calculations.", tags: new[] { "Advanced" })
        );
        
        Harmony.CreateAndPatchAll(typeof(ScrollAutoFollowPatch));
        Log.LogInfo($"{PluginName} loaded.");
    }
}

[HarmonyPatch(typeof(CustomThumbnailSelectWindow))]
internal static class ScrollAutoFollowPatch
{
    private const string GREEN = "\u001b[32m";
    private const string RESET = "\u001b[0m";
    private const float ItemHeight = 72f;

    [HarmonyPostfix, HarmonyPatch(nameof(CustomThumbnailSelectWindow.OnNext))]
    private static void AfterOnNext(CustomThumbnailSelectWindow __instance) =>
        ScrollToSelected(__instance);

    [HarmonyPostfix, HarmonyPatch(nameof(CustomThumbnailSelectWindow.OnPrev))]
    private static void AfterOnPrev(CustomThumbnailSelectWindow __instance) =>
        ScrollToSelected(__instance);

    private static void ScrollToSelected(CustomThumbnailSelectWindow win)
    {
        int selectedIndex = win.GetCurrentIndex();
        if (selectedIndex < 0)
            return;

        const int columns = 4;
        int row = selectedIndex / columns;

        var scrollRect = win.GetComponentInChildren<UnityEngine.UI.ScrollRect>();
        if (scrollRect == null)
            return;

        float contentHeight = scrollRect.content.rect.height;
        float viewportHeight = scrollRect.viewport.rect.height;
        float scrollable = contentHeight - viewportHeight;
        if (scrollable <= 0)
            return;

        float normalized = 1f - Mathf.Clamp01((row * ItemHeight) / scrollable);
        scrollRect.verticalNormalizedPosition = normalized;

        if (Plugin.DebugMode.Value)
        {
            Plugin.Logger.LogInfo(
                $"{GREEN}[Scroll] index={selectedIndex}, row={row}, "
                    + $"content={contentHeight}, viewport={viewportHeight}, "
                    + $"scrollable={scrollable}, normalized={normalized}{RESET}"
            );
        }
    }
}
