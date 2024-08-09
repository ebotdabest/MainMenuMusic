using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Policy;
using BepInEx;
using HarmonyLib;
using Pathfinding.Util;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CustomMenuTS1;

[BepInPlugin("hu.ebot.music", "hu.ebot.music", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    private void Awake()
    {
        Harmony harmony = new Harmony("hu.ebot.music");
        harmony.PatchAll();
    }
}

[HarmonyPatch(typeof(RadioController))]
[HarmonyPatch("Update")]
public static class RadioControllerPatch
{
    static bool Prefix(RadioController __instance)
    {
        if (SceneManager.GetActiveScene().name == "mainmenu")
        {
            if (__instance.audioSource != null)
            {
                __instance.audioSource.Stop();
                __instance.StartCoroutine(LoadAndPlay(__instance));
            }
            return false;
        }

        return true;
    }

    static IEnumerator LoadAndPlay(RadioController instance)
    {
        string path = Path.Combine(Paths.GameRootPath, "main_menu_music.wav");
        if (File.Exists(path))
        {
            WWW www = new WWW("file:///" + path);
            yield return www;

            AudioClip clip = www.GetAudioClip();
            if (clip != null)
            {
                instance.audioSource.clip = clip;
                instance.audioSource.Play();
            }
        }
    }
}