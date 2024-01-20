using System;
using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using Oculus.Platform;
using UnityEngine;
using Utilla;

namespace JoinPrivateLobby
{
	[ModdedGamemode]
	[BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
	[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
	public class Plugin : BaseUnityPlugin
	{
        private GameObject button;
        private GameObject button2;
        private GameObject button3;

        public static string favcode = "Ex_Code";

        public void ConfigVal(bool reset)
        {
            string favcodeconfig = Config.Bind("Fav Code", "True", "Ex_Code", "Set the code that you can join with a button").Value;

            favcode = favcodeconfig;
        }

        void Start()
		{
			Utilla.Events.GameInitialized += OnGameInitialized;
		}

		void OnEnable()
		{
			HarmonyPatches.ApplyHarmonyPatches();
		}

		void OnDisable()
		{
			HarmonyPatches.RemoveHarmonyPatches();
		}

		void OnGameInitialized(object sender, EventArgs e)
		{
            var assetBundle = LoadAssetBundle("JoinPrivateLobby.Assets.jointhelobby");
            GameObject assetBundleGameobject = assetBundle.LoadAsset<GameObject>("jointhelobby");

            var assetBundle2 = LoadAssetBundle("JoinPrivateLobby.Assets.leavethelobby");
            GameObject assetBundleGameobject2 = assetBundle2.LoadAsset<GameObject>("leavethelobby");

            var assetBundle3 = LoadAssetBundle("JoinPrivateLobby.Assets.jointhefavlobby");
            GameObject assetBundleGameobject3 = assetBundle3.LoadAsset<GameObject>("jointhefavlobby");

            button = Instantiate(assetBundleGameobject);
            button.transform.position = new Vector3(-65.0404f, 11.5817f, -81.2842f);
            button.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
            button.transform.localRotation = Quaternion.Euler(0f, 36.086f, 0f);
            button.name = "jointhelobby";
            button.GetComponent<Collider>().isTrigger = true;
            button.layer = 18;
            button.AddComponent<ButtonHandler>();

            button2 = Instantiate(assetBundleGameobject2);
            button2.transform.position = new Vector3(-65.119f, 11.5817f, -81.228f);
            button2.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
            button2.transform.localRotation = Quaternion.Euler(0f, 36.086f, 0f);
            button2.name = "leavethelobby";
            button2.GetComponent<Collider>().isTrigger = true;
            button2.layer = 18;
            button2.AddComponent<ButtonHandler>();

            /*button3 = Instantiate(assetBundleGameobject3);
            button3.transform.position = new Vector3(-65.0404f, 11.658f, -81.2842f);
            button3.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
            button3.transform.localRotation = Quaternion.Euler(0f, 36.086f, 0f);
            button3.name = "jointhefavcode";
            button3.GetComponent<Collider>().isTrigger = true;
            button3.layer = 18;
            button3.AddComponent<ButtonHandler>();*/

            ConfigVal(false);
        }
        AssetBundle LoadAssetBundle(string path)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            AssetBundle bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
            return bundle;
        }
    }
}
