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

        void Start()
		{
            Debug.Log("[JoinPrivateLobby] Waiting for OnGameInitialized Event.");
			Utilla.Events.GameInitialized += OnGameInitialized;
		}

		void OnEnable()
		{
            Debug.Log("[JoinPrivateLobby] Enabled the mod.");
            button.SetActive(true);
            button2.SetActive(true);
            HarmonyPatches.ApplyHarmonyPatches();
		}

		void OnDisable()
		{
            Debug.Log("[JoinPrivateLobby] Disabled the mod.");
            button.SetActive(false);
            button2.SetActive(false);
            HarmonyPatches.RemoveHarmonyPatches();
		}

		void OnGameInitialized(object sender, EventArgs e)
		{
            Debug.Log("[JoinPrivateLobby] Loading AssetBundles");
            var assetBundle = LoadAssetBundle("JoinPrivateLobby.Assets.jointhelobby");
            GameObject assetBundleGameobject = assetBundle.LoadAsset<GameObject>("jointhelobby");

            var assetBundle2 = LoadAssetBundle("JoinPrivateLobby.Assets.leavethelobby");
            GameObject assetBundleGameobject2 = assetBundle2.LoadAsset<GameObject>("leavethelobby");

            Debug.Log("[JoinPrivateLobby] Loading GameObjects");
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

            Debug.Log("[JoinPrivateLobby] Everything went fine.");
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
