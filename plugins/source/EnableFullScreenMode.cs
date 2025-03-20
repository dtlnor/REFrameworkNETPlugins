namespace FullScreenMode;

using System;
using ImGuiNET;
using REFrameworkNET;
using REFrameworkNET.Attributes;
using REFrameworkNET.Callbacks;

public class EnableFullScreenMode
{
    private static bool IsRunningWilds => Environment.ProcessPath?.Contains("Wilds", StringComparison.CurrentCultureIgnoreCase) ?? false;

    private static readonly string[] WindowModeList = Enum.GetNames(typeof(via.render.WindowMode));
    private static int _windowMode = (int)via.render.Renderer.WindowMode;
    private static bool _isWinOpen = true;

    [Callback(typeof(ImGuiDrawUI), CallbackType.Pre)]
    public static void ImGuiCallback()
    {
        if (!_isWinOpen) return;
        // if (!ImGui.Begin("FullScreenMode", ref _isWinOpen)) return;

        // ImGui.Text("EnableFullScreenMode");
        if (ImGui.Combo("Fullscreen Mode", ref _windowMode, WindowModeList, WindowModeList.Length))
        {
            via.render.Renderer.WindowMode = (via.render.WindowMode)_windowMode;
        }

        // ImGui.End();
    }

    [PluginExitPoint]
    public static void OnUnload()
    {
    }

    [PluginEntryPoint]
    public static void Main()
    {
        if (!IsRunningWilds)
        {
            API.LogWarning("This plugin is only compatible with Wilds");
            return;
        }
    }
}