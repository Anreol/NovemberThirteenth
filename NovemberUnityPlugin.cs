using BepInEx;
using System;
using System.IO;

namespace NovemberThirteenth
{
    [BepInPlugin(ModGuid, ModIdentifier, ModVer)]
    public class NovemberUnityPlugin : BaseUnityPlugin
    {
        internal const string ModVer =
#if DEBUG
            "9999." +
#endif
            "0.0.2";

        internal const string ModIdentifier = "NovemberThirteenth";
        internal const string ModGuid = "com.Anreol." + ModIdentifier;

        public static NovemberUnityPlugin instance;
        public static PluginInfo pluginInfo;
        public static string soundBankDirectory => System.IO.Path.Combine(Path.GetDirectoryName(pluginInfo.Location), "soundbanks");
        public void Awake()
        {
            pluginInfo = Info;
            RoR2.CharacterBody.onBodyStartGlobal += Gummyfy;
        }

        private void Gummyfy(RoR2.CharacterBody obj)
        {
            if(obj && obj.gameObject && obj.inventory)
            {
                if (obj.inventory.GetItemCount(RoR2.DLC1Content.Items.GummyCloneIdentifier.itemIndex) > 0)
                {
                    obj.gameObject.AddComponent<GummyBearMusicDisc>();
                }
            }
        }

        [RoR2.SystemInitializer]
        public static void Init()
        {
            uint akBankID;
            AkSoundEngine.AddBasePath(soundBankDirectory);
            AkSoundEngine.LoadBank("GummyInit.bnk", out akBankID);
            AkSoundEngine.LoadBank("GummyBank.bnk", out akBankID);
        }
    }
}
