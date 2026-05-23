using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ArchipelagoMIUU;

public static class PatchEntryPoint
{
    private static bool IsPatched = false;

    public static void Start()
    {
        SceneManager.sceneLoaded += BeginPatch;
    }

    private static void BeginPatch(Scene scene, LoadSceneMode mode)
    {
        if (!IsPatched)
        {
            new Harmony("archipelago").PatchAll();
            IsPatched = true;
            SceneManager.sceneLoaded -= BeginPatch;
            Debug.Log("Archipelago Loaded!");
        }
    }
}
