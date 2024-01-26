using UnityEngine;

namespace VSVRControllers;

class Keyboard
{
    public static void HandleKeyboardInputSession()
    {
        
    }

    public static void HandleKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.Quote))
        {
            Controller.outputControllerDebug = (Controller.outputControllerDebug + 1) % 5;
            VSVRControllersMod.logger.LogInfo("Controller debug level is now " + Controller.outputControllerDebug);
        }
    }
}
