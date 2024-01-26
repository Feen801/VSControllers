using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Collections;
using System;
     
namespace VSVRControllers;

class Controller
{
    public static int outputControllerDebug = 0;

    private static Dictionary<int, bool> triggerPresses = new Dictionary<int, bool>();

    public static bool WasATriggerClicked(int duplicateID)
    {
        bool clicked = false;
        bool pressed = IsATriggerPressed();
        if (pressed && !triggerPresses.Get(duplicateID))
        {
            clicked = true;
            if (outputControllerDebug >= 1)
            {
                VSVRControllersMod.logger.LogInfo("Trigger click!");
            }
        }
        triggerPresses[duplicateID] = pressed;

        return clicked;
    }

    public static bool IsATriggerPressed()
    {
        float left = 0f;
        float right = 0f;
        bool joystick = false;

        joystick = Input.GetAxis("Fire1") > 0.5f;

        if (outputControllerDebug >= 2)
        {
            VSVRControllersMod.logger.LogInfo("Trigger: Left: " + left + " Right: " + right);
        }

        return right > 0.5 || left > 0.5 || joystick;
    }

    private static Dictionary<int, bool> stickPresses = new Dictionary<int, bool>();

    public static bool WasAStickClicked(int duplicateID)
    {
        bool clicked = false;
        bool pressed = IsAStickPressed();
        if (pressed && !stickPresses.Get(duplicateID))
        {
            clicked = true;
            if (outputControllerDebug >= 1)
            {
                VSVRControllersMod.logger.LogInfo("Stick click!");
            }
        }
        stickPresses[duplicateID] = pressed;

        return clicked;
    }

    public static bool IsAStickPressed()
    {
        bool left = false;
        bool right = false;
        bool joystick = false;

        joystick = Input.GetAxis("Fire2") > 0.5f;

        if (outputControllerDebug >= 2)
        {
            VSVRControllersMod.logger.LogInfo("Stick: Left: " + left + " Right: " + right);
        }

        return right || left || joystick;
    }

    private static Dictionary<int, bool> facePresses = new Dictionary<int, bool>();
    public static bool WasAFaceButtonClicked(int duplicateID)
    {
        bool clicked = false;
        bool pressed = IsAFaceButtonPressed();
        if (pressed && !facePresses.Get(duplicateID))
        {
            clicked = true;
            if (outputControllerDebug >= 1)
            {
                VSVRControllersMod.logger.LogInfo("Face click!");
            }
        }
        facePresses[duplicateID] = pressed;
        return clicked;
    }

    public static int CountGripsPressed() 
    {
        bool left = false;
        bool right = false;
        bool joystick = false;
        joystick = Input.GetAxis("Fire3") > 0.5f;

        return Math.Clamp(Convert.ToInt32(left) + Convert.ToInt32(right) + Convert.ToInt32(joystick), 0, 2);
    }

    public static bool IsAFaceButtonPressed()
    {
        bool left1 = false;
        bool left2 = false;
        bool right1 = false;
        bool right2 = false;
        bool joystick = false;

        joystick = Input.GetAxis("Jump") > 0.5f;

        if (outputControllerDebug >= 2)
        {
            VSVRControllersMod.logger.LogInfo("Face Button: Left: " + (left1 || left2) + " Right: " + (right1 || right2));
        }

        return left1 || left2 || right1 || right2 || joystick;
    }

    public static Vector2 GetMaximalJoystickValue()
    {
        Vector2 leftJoystickValue = Vector2.zeroVector;
        Vector2 rightJoystickValue = Vector2.zeroVector;

        Vector2 joystick = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        leftJoystickValue = leftJoystickValue.magnitude > joystick.magnitude ? leftJoystickValue : joystick;

        if (outputControllerDebug >= 3)
        {
            VSVRControllersMod.logger.LogInfo("Joystick right: x: " + rightJoystickValue.x + " y: " + rightJoystickValue.y);
            VSVRControllersMod.logger.LogInfo("Joystick left: x: " + leftJoystickValue.x + " y: " + leftJoystickValue.y);
        }

        return leftJoystickValue.magnitude > rightJoystickValue.magnitude ? leftJoystickValue : rightJoystickValue;
    }

    public static double GetMaximalJoystickAngle()
    {
        Vector2 maximal = GetMaximalJoystickValue();
        double angle = Vector2.Angle(maximal, Vector2.right);
        angle = maximal.y > 0 ? angle : 360 - angle;

        if (outputControllerDebug >= 4)
        {
            VSVRControllersMod.logger.LogInfo("Joystick angle: " + angle);
        }

        return angle;
    }

    public static double GetMaximalJoystickMagnitude()
    {
        Vector2 maximal = GetMaximalJoystickValue();

        if (outputControllerDebug >= 4)
        {
            VSVRControllersMod.logger.LogInfo("Joystick magnitude: " + maximal.magnitude);
        }

        return maximal.magnitude;
    }

    public static void ControllerInteract()
    {
        //Ordered by priority
        if (Menus.SafewordMenuInteract())
        {
            return;
        }
        if (Buttons.TemporaryButtonInteract())
        {
            return;
        }
        if (Menus.FindomInputInteract())
        {
            return;
        }
        if (Buttons.RadialMenuInteract())
        {
            return;
        }
        if (Menus.StakesMenuInteract())
        {
            return;
        }
        if (Menus.ChoiceMenuInteract())
        {
            return;
        }
        if (Menus.IntInputInteract())
        {
            return;
        }
        if (Buttons.ChoiceButtonInteract())
        {
            return;
        }
    }
}
