using System;
using System.Collections.Generic;
using UnityEngine;
using MIU;
using HarmonyLib;

namespace ArchipelagoMIUU
{
    public static class MiscHandler
    {
        public const string VERSION = "v0.1.1-alpha";

        public static string config_APip = "archipelago.gg:38281";
        public static string config_APslot = "MIUUPlayer";
        public static string config_APpassword = "";
        public static int config_overrideDL = -1;
        public static int config_overrideDLAmnesty = -1;
        public static bool disallowDeathlink = false;

        public static string connectString = "Not connected to Archipelago";

        public static void doTimeTravelItem()
        {
            MarbleController[] array = GameProcess.ServerProcess.FindObjectsOfType<MarbleController>();
            if (array.Length == 0)
            {
                UnityEngine.Debug.Log("Failed to find any marbles to give time travel credit to.");
                return;
            }
            foreach(MarbleController marble in array)
            {
                marble.AddTimeCredit(5f);
            }
        }

        public static void killMarbles()
        {
            MarbleController[] array = GameProcess.ServerProcess.FindObjectsOfType<MarbleController>();
            if (array.Length == 0)
            {
                UnityEngine.Debug.Log("Failed to find any marbles to kill.");
                return;
            }
            foreach(MarbleController marble in array)
            {
                disallowDeathlink = true;
                marble.ForceOutOfBounds();
            }
            disallowDeathlink = false;

        }

        public static void handleDeath(MarbleController marble, int reason)
        {
            string deathlinkMessage;
            ConnectHandler.deathAmnesty++;
            if (ConnectHandler.deathAmnesty >= ConnectHandler.deathAmnestyMax)
            {
                ConnectHandler.deathAmnesty = 0;
                deathlinkMessage = "You've died "+ConnectHandler.deathAmnestyMax+" times. Death Link sent.";
                if(ConnectHandler.deathAmnestyMax <= 1)
                {
                    deathlinkMessage = "Death Link sent.";
                }
                ConnectHandler.sendDeathLink(reason);
            }
            else
            {
                deathlinkMessage = "You've died "+ ConnectHandler.deathAmnesty +" out of "+ ConnectHandler.deathAmnestyMax +" times...";
            }
            GamePlayManager.Get().SetTutorial(deathlinkMessage, null);
            float expiry = Time.time + 3f;
            Traverse.Create(marble).Field("TutorialHideTime").SetValue(expiry);
        }

        public static void setConnectString(string connectString)
        {
            MiscHandler.connectString = connectString;
            if (MainMenuPanel.instance && MainMenuPanel.instance.XBoxUserName)
                MainMenuPanel.instance.XBoxUserName.text = "ArchipelagoMIUU Mod " + VERSION + "\n" + connectString;
        }
    }
}
