using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.XInput;

class GamepadInputManager
{
    enum submenu
    {
        None = 0,
        Keyboard = 1,
        SpecialCharacters = 2,
        FileMenu = 3,
        Libraries = 4
    }
    private static submenu currentSubmenu = submenu.None;
    private static Dictionary<String, int> lastStateFlags = new Dictionary<string, int>();

    private static void checkToAdd(string key, GamepadButtonFlags value, State state, Dictionary<String, int> stateFlags)
    {
        stateFlags.Add(key, state.Gamepad.Buttons.HasFlag(value) ? 1 : 0);
    }

    private static void checkButtons(State state, Dictionary<String, int> stateFlags)
    {
        checkToAdd("A", GamepadButtonFlags.A, state, stateFlags);
        checkToAdd("B", GamepadButtonFlags.B, state, stateFlags);
        checkToAdd("X", GamepadButtonFlags.X, state, stateFlags);
        checkToAdd("Y", GamepadButtonFlags.Y, state, stateFlags);
        checkToAdd("Back", GamepadButtonFlags.Back, state, stateFlags);
        checkToAdd("Start", GamepadButtonFlags.Start, state, stateFlags);
        checkToAdd("LeftShoulder", GamepadButtonFlags.LeftShoulder, state, stateFlags);
        checkToAdd("RightShoulder", GamepadButtonFlags.RightShoulder, state, stateFlags);
        checkToAdd("LeftThumb", GamepadButtonFlags.LeftThumb, state, stateFlags);
        checkToAdd("RightThumb", GamepadButtonFlags.RightThumb, state, stateFlags);
        checkToAdd("DPadDown", GamepadButtonFlags.DPadDown, state, stateFlags);
        checkToAdd("DPadLeft", GamepadButtonFlags.DPadLeft, state, stateFlags);
        checkToAdd("DPadRight", GamepadButtonFlags.DPadRight, state, stateFlags);
        checkToAdd("DPadUp", GamepadButtonFlags.DPadUp, state, stateFlags);
    }

    private static void checkThumbs(State state, Dictionary<String, int> stateFlags)
    {
        stateFlags.Add("RightThumbX", state.Gamepad.RightThumbX);
        stateFlags.Add("LeftThumbX", state.Gamepad.LeftThumbX);
        stateFlags.Add("RightThumbY", state.Gamepad.RightThumbY);
        stateFlags.Add("LeftThumbY", state.Gamepad.LeftThumbY);
        stateFlags.Add("RightTrigger", state.Gamepad.RightTrigger);
        stateFlags.Add("LeftTrigger", state.Gamepad.LeftTrigger);
    }

    public static void Update(State state)
    {
        Dictionary<String, int> stateFlags = new Dictionary<string, int>();
        checkButtons(state, stateFlags);
        checkThumbs(state, stateFlags);

        switch (currentSubmenu)
        {
            case submenu.None:
                break;
            case submenu.Keyboard:
                break;
            case submenu.SpecialCharacters:
                break;
            case submenu.FileMenu:
                break;
            case submenu.Libraries:
                break;
        }

        lastStateFlags = stateFlags;


    }
}





/**
 * Gamepad input manager
     * Virtual keyboard manager
     * Submenu manager (Symbols, Libraries)
 * Key output manager
 * Current File manager -> All accessible variables, all references libraries
 * */
