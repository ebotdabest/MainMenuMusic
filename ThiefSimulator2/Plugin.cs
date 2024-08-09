using BepInEx;
using HarmonyLib;

namespace CustomMusic;

[BepInPlugin("hu.ebot.music", "Custom Music", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    private void Awake()
    {
        Logger.LogInfo("Custom Music loaded!");
        Patch();
    }

    void Patch()
    {
        Harmony harmony = new Harmony("hu.ebot.music");
        harmony.PatchAll();
    }
}
