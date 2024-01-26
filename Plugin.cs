using System.Reflection;
using BepInEx;
using BepInEx.Unity.Mono;
using HarmonyLib;
using UnityEngine.SceneManagement;
using BepInEx.Logging;
using BepInEx.Configuration;

namespace VSVRControllers;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
#pragma warning disable BepInEx002 // Classes with BepInPlugin attribute must inherit from BaseUnityPlugin
public class VSVRControllersMod : BaseUnityPlugin
#pragma warning restore BepInEx002 // Classes with BepInPlugin attribute must inherit from BaseUnityPlugin
{
    public const string sessionScene = "ExtraLoadScene";
    public bool inSession = false;
    public static ManualLogSource logger;
    public static ConfigFile config;

    private void Awake()
    {
        // Plugin startup logic
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        logger = Logger;
        config = Config;

        Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());

        SceneManager.sceneLoaded += OnSceneLoaded;

        Logger.LogInfo("Reached end of Plugin.Awake()");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Logger.LogInfo("A scene was loaded: " + scene.name);
        if (Equals(scene.name, sessionScene))
        {
            Buttons.SetupChoiceButtons();
            Buttons.SetupOtherButtons();
            Buttons.SetupRadialButtons();
            Menus.SetupMenus();

            inSession = true;
        }
        else
        {
            inSession = false;
        }
    }

    void Update()
    {
        if (inSession)
        {
            Controller.ControllerInteract();
            Keyboard.HandleKeyboardInputSession();
        }
    }
}
