using BepInEx;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

namespace TheLightMotionSicknessFix
{
    [BepInPlugin("dev.lone.TheLightMotionSicknessFix", "TheLightMotionSicknessFix", "1.0.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        PlayerScript player;
        Scene_Controller sceneController;
        RigidbodyFirstPersonController fpsController;

        Rigidbody rb;

        void Awake()
        {
            var harmony = new Harmony("dev.lone.TheLightMotionSicknessFix");
            harmony.PatchAll();

            SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        }

        private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
        {
            player = FindObjectOfType<PlayerScript>();
            sceneController = FindObjectOfType<Scene_Controller>();
            fpsController = FindObjectOfType<RigidbodyFirstPersonController>();
            rb = fpsController.GetComponent<Rigidbody>();

            player.CameraSpeed = 2;
            player.MouseSpeed = 2;

            player.Fov_Base = 70;
            player.Fov_Fin = 70;
            player.BobHeadY = 0;
            player.BobHeadZ = 0;
            player.BobHead_Power = 0;
            player.BobHead_Power_half = 0;

            player.BaseSpeed = 2;
            player.PlayerSpeed= 2;

            sceneController.NewFOV = 70;

            fpsController.mouseLook.smoothTime = 100;
            fpsController.mouseLook.CameraSmooth = 0;

            fpsController.PlayeSpeed = 0;

            player.MenuScript.HeadBobOff = true;
        }

        void Update()
        {
            if(
                (Input.GetKeyUp(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) ||
                (Input.GetKeyUp(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            )
            {
                rb.velocity = Vector3.zero;
                rb.Sleep();
            }
        }
    }
}