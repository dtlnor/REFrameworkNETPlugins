namespace ShowNoHit;

using System;
using ImGuiNET;
using REFrameworkNET;
using REFrameworkNET.Attributes;
using REFrameworkNET.Callbacks;

public class ShowNoHit
{
    private static bool IsRunningWilds =>
        Environment.ProcessPath?.Contains("Wilds", StringComparison.CurrentCultureIgnoreCase) ?? false;

    private static bool _isWinOpen = true;

    private static float speed = 1.0f;

    [Callback(typeof(ImGuiRender), CallbackType.Pre)]
    public static void ImGuiCallback()
    {
        if (!API.IsDrawingUI())
        {
            // reset the window state
            _isWinOpen = true;
            return;
        }

        if (!_isWinOpen) return;
        if (!ImGui.Begin("Misc", ref _isWinOpen)) return;

        ImGui.Columns(2);
        
        ImGui.Text($"FrameCount");
        ImGui.NextColumn();
        ImGui.Text($"{via.Application.FrameCount}");
        ImGui.NextColumn();
                
        ImGui.Text($"RenderFrame");
        ImGui.NextColumn();
        ImGui.Text($"{via.render.Renderer.RenderFrame}");
        ImGui.NextColumn();

        ImGui.Text($"ProtectFrame");
        ImGui.NextColumn();
        ImGui.Text($"{via.render.Renderer.ProtectFrame}");
        ImGui.NextColumn();

        ImGui.Text($"SafeRenderFrame");
        ImGui.NextColumn();
        ImGui.Text($"{via.render.Renderer.SafeRenderFrame}");
        ImGui.NextColumn();

        ImGui.Text($"UpTimeSecond");
        ImGui.NextColumn();
        ImGui.Text($"{via.Application.UpTimeSecond}");
        ImGui.NextColumn();

        ImGui.Text($"ElapsedSecond");
        ImGui.NextColumn();
        ImGui.Text($"{via.Application.ElapsedSecond}");
        ImGui.NextColumn();

        ImGui.Text($"TimeMilliSecond");
        ImGui.NextColumn();
        ImGui.Text($"{via.render.Renderer.TimeMilliSecond}");
        ImGui.NextColumn();

        ImGui.Text($"RenderTime");
        ImGui.NextColumn();
        ImGui.Text($"{via.render.Renderer.RenderTime}");
        ImGui.NextColumn();

        ImGui.Text($"RenderWaitTimeMillisecond");
        ImGui.NextColumn();
        ImGui.Text($"{via.render.Renderer.RenderWaitTimeMillisecond}");
        ImGui.NextColumn();

        ImGui.Text($"DelayJobWaitTimeMillisecond");
        ImGui.NextColumn();
        ImGui.Text($"{via.render.Renderer.DelayJobWaitTimeMillisecond}");
        ImGui.NextColumn();
        ImGui.Columns(1);
        
        if (ImGui.DragFloat("Global Speed", ref speed, 0.01f, 0, 100))
        {
            via.Application.GlobalSpeed = speed;
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
        if (!IsRunningWilds)
        {
            API.LogWarning("This plugin is only compatible with Wilds");
            return;
        }
    }
}