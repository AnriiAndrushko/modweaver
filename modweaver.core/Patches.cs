using System;
using System.Text;
using HarmonyLib;
using HarmonyLib.Tools;
using modweaver.core;
using UnityEngine.SceneManagement;

namespace modweaver.preload {
    public static class Patches {
        public static void Patch() {
            Harmony harmony = new Harmony("org.modweaver.loader");
            harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(VersionNumberTextMesh), "Start")]
    internal class VersionTextPatch {
        internal static bool hasDoneTextPatch = false;
        internal static VersionNumberTextMesh instance;

        [HarmonyPostfix]
        public static void Postfix(ref VersionNumberTextMesh __instance) {
            if (!hasDoneTextPatch) {
                /*var textMesh = textMeshInfo.GetValue(__instance);

                //Logger.Log(LogLevel.Warning, "TextMesh via reflection: " + textMesh.ToString());

                var setTextInfo = textMesh.GetType().GetMethods().Where(m => m.Name == "SetText").Where(m => m.GetParameters().Length == 1).First();

                var currentText = (string)textMesh.GetType().GetProperty("text").GetValue(textWMesh, null);
                StringBuilder sb = new StringBuilder(currentText);
                sb.Append("\n\nMods:");asa

                foreach (var plugin in Util.Plugins)
                {
                    var name = plugin.Value.Metadata.Name;
                    var version = plugin.Value.Metadata.Version;

                    sb.Append("\n- ");
                    sb.Append(name);
                    sb.Append(" v");
                    sb.Append(version);
                }

                setTextInfo.Invoke(textMesh, new object[] { sb });

                textMeshInfo.SetValue(__instance, textMesh);

                hasDoneTextPatch = true;*/
                Console.WriteLine("[modweaver] trying to patch versionnumbertextmesh");
                //__instance.textMesh.text = "SPIDERHECK; ModWeaver ALPHA 0.1.0";
                __instance.textMesh.SetText("SPIDERHECK; ModWeaver ALPHA 0.1.0");
                hasDoneTextPatch = true;
            }
        }
    }

    [HarmonyPatch(typeof(CustomTiersScreen), "Start")]
    public static class AddModMenu {
        [HarmonyPostfix]
        public static void Postfix() {
            HudController.instance.EnableModsButton(); // we do this here to ensure everything has loaded
            CoreMain.addModsToMenu();
        }
    }
}