﻿using UnityEngine;
using GorillaLocomotion;
using Utilla;
using System.IO;
using System.Reflection;
using HarmonyLib;
using System;
using BepInEx;
using GorillaNetworking;
namespace JoinPrivateLobby
{
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    public class ButtonHandler : MonoBehaviour
    {
        //ty stricker elmishh(cyn) and kyle
        public string button;
        private float touchTime = 0f;
        private const float debounceTime = 0.25f;
        string otherString = Plugin.favcode;

        void Start()
        {
            button = transform.name;
        }
        void OnTriggerEnter(Collider other)
        {
            if (touchTime + debounceTime >= Time.time) return;
            if (other.TryGetComponent(out GorillaTriggerColliderHandIndicator component))
            {
                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(211, component.isLeftHand, 0.12f);
                GorillaTagger.Instance.StartVibration(component.isLeftHand, GorillaTagger.Instance.tapHapticStrength / 2f, GorillaTagger.Instance.tapHapticDuration);
                if(button == "jointhelobby")
                {
                    Utilla.Utils.RoomUtils.JoinPrivateLobby();
                }
                if(button == "leavethelobby")
                {
                    PhotonNetworkController.Instance.AttemptDisconnect();
                }
                if(button == "jointhefavcode")
                {
                    Utilla.Utils.RoomUtils.JoinPrivateLobby(otherString);
                }
            }
        }
    }
}
// Credit to octoburr (def not stolen code!!!!1111!!!)