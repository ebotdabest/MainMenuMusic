using System.IO;
using BepInEx;
using HarmonyLib;
using UnityEngine;

[HarmonyPatch(typeof(MainMenu), "Start")]
public class MainMenuPatch
{
    static void Postfix(MainMenu __instance)
    {
        string path = Path.Combine(Paths.GameRootPath, "main_menu_music.wav"); //Important that this is a wav pepole!
        if (File.Exists(path))
        {
            string fileName = "file:///" + path;
            WWW url = new WWW(fileName);
            AudioClip clip = url.GetAudioClip(false, true);
            __instance.musicSource.clip = clip;
            __instance.musicSource.Play();
        }
    }
}