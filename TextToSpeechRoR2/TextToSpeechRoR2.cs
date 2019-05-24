using System;
using System.Speech.Synthesis;
using BepInEx;
using RoR2;
using UnityEngine;

namespace TextToSpeechRoR2
{
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("com.LeoneShamoth.RoR2TTS", "Risk of Rain 2 Text To Speech", "1.0.0")]

    public class TextToSpeechRoR2 : BaseUnityPlugin
    {
        private SpeechSynthesizer synthesizer = new SpeechSynthesizer();

        public void Awake ()
        {
            Chat.AddMessage("Loaded RoR2 TTS");
            synthesizer.SetOutputToDefaultAudioDevice();

            On.RoR2.Chat.AddPickupMessage += textToSpeech;
        }

        private void textToSpeech (On.RoR2.Chat.orig_AddPickupMessage orig, CharacterBody body, string pickupToken, Color32 pickupColor, uint pickupQuantity)
        {
            Chat.PlayerPickupChatMessage pickupMessage = new Chat.PlayerPickupChatMessage
            {
                subjectCharacterBody = body,
                baseToken = "PLAYER_PICKUP",
                pickupToken = pickupToken,
                pickupColor = pickupColor,
                pickupQuantity = pickupQuantity
            };

            string text = pickupMessage.ConstructChatString();

            synthesizer.Speak(text);

            orig(body, pickupToken, pickupColor, pickupQuantity);
        }

    }
}
