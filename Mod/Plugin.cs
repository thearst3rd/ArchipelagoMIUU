using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using System.IO;
using System.Reflection;

namespace ArchipelagoMIUU;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;
    public static Sprite APIcon;

    private void Awake()
    {
        MiscHandler.setConfig(base.Config);
        //Harmony setup
        Harmony harmony = new Harmony("archipelago");
        harmony.PatchAll();
        // Plugin startup logic
        Logger = base.Logger;
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
    }

    public static void LoadAPIcon()
    {
        var iconData = File.ReadAllBytes(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Archipelago.png"));
        Texture2D iconTexture = new(2,2,TextureFormat.RGBA32, false);
        ImageConversion.LoadImage(iconTexture, iconData);
        APIcon = Sprite.Create(iconTexture, new(0, 0, iconTexture.width, iconTexture.height), new());
    }
}
