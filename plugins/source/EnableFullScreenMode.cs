namespace FullScreenMode;

using System;
using ImGuiNET;
using REFrameworkNET;
using REFrameworkNET.Attributes;
using REFrameworkNET.Callbacks;

public class EnableFullScreenMode
{
    private static bool? IsRunningWilds => Environment.ProcessPath?.Contains("Wilds", StringComparison.CurrentCultureIgnoreCase);

    private static readonly string[] WindowModeList = Enum.GetNames(typeof(via.render.WindowMode));
    private static int _windowMode = (int)via.render.Renderer.WindowMode;

    [Callback(typeof(ImGuiRender), CallbackType.Pre)]
    public static void ImGuiCallback()
    {
        if (!ImGui.Begin("FullScreenMode")) return;

        ImGui.Text("EnableFullScreenMode");
        if (ImGui.Combo("Fullscreen Mode", ref _windowMode, WindowModeList, WindowModeList.Length))
        {
            via.render.Renderer.WindowMode = (via.render.WindowMode)_windowMode;
        }

        ImGui.End();
    }

    [PluginExitPoint]
    public static void OnUnload()
    {
    }

    [PluginEntryPoint]
    public static void Main()
    {
        if (!IsRunningWilds ?? true)
        {
            API.LogWarning("This plugin is only compatible with Wilds");
            return;
        }
    }
}