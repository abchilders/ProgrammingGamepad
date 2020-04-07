using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.XInput;


class InputControl
{
    int delay;
    int speed;
    bool repeats;
    long countdownStart;

    long deadzone;

    public int value;
    public bool triggered;
    public bool startRepeat;

    public InputControl(int value, bool repeats = false, long deadzone = 0)
    {
        delay = 250 * (1 + System.Windows.Forms.SystemInformation.KeyboardDelay);
        speed = 400 - (int)(11.8*System.Windows.Forms.SystemInformation.KeyboardSpeed);
        this.repeats = repeats;
        this.value = value;
        this.deadzone = deadzone;
        triggered = false;
        startRepeat = false;
    }

    public InputControl clone()
    {
        InputControl ret = new InputControl(value, repeats, deadzone);
        ret.triggered = triggered;
        ret.countdownStart = countdownStart;
        ret.startRepeat = startRepeat;
        return ret;
    }

    public bool triggers()
    {
        bool ret = (value != 0);
        if (repeats)
        {
            if (ret)
            {
                if (startRepeat)
                    ret = speed < DateTime.UtcNow.Ticks / 10000 - countdownStart;
                else
                    ret = delay < DateTime.UtcNow.Ticks / 10000 - countdownStart;
                if (ret)
                {
                    countdownStart = DateTime.UtcNow.Ticks / 10000;
                    if (triggered)
                       startRepeat = true;
                }
                else
                    return false;
            }
            else
            {
                countdownStart = 0;
                triggered = false;
                startRepeat = false;
            }
        }
        else if (ret && triggered) return false;
        triggered = ret;
        return ret;
    }
}













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
    private static Dictionary<String, InputControl> lastStateFlags = new Dictionary<string, InputControl>();

    private static void checkToAdd(string key, Object value, State state, Dictionary<String, InputControl> stateFlags, bool repeats = true, long deadzone = 0)
    {
        int result = 0;

        if (value is GamepadButtonFlags)
            result = state.Gamepad.Buttons.HasFlag((GamepadButtonFlags)value) ? 1 : 0;
        else if (value is short)
            result = Convert.ToInt32(value);

        if (stateFlags.ContainsKey(key))
            stateFlags[key].value = result;
        else
            stateFlags.Add(key, new InputControl(result, repeats, deadzone));
    }

    private static void checkStates(State state, Dictionary<String, InputControl> stateFlags)
    {
        checkToAdd("A", GamepadButtonFlags.A, state, stateFlags);
        checkToAdd("B", GamepadButtonFlags.B, state, stateFlags);
        checkToAdd("X", GamepadButtonFlags.X, state, stateFlags);
        checkToAdd("Y", GamepadButtonFlags.Y, state, stateFlags, false);
        checkToAdd("Back", GamepadButtonFlags.Back, state, stateFlags, false);
        checkToAdd("Start", GamepadButtonFlags.Start, state, stateFlags, false);
        checkToAdd("LeftShoulder", GamepadButtonFlags.LeftShoulder, state, stateFlags, false);
        checkToAdd("RightShoulder", GamepadButtonFlags.RightShoulder, state, stateFlags, false);
        checkToAdd("LeftThumb", GamepadButtonFlags.LeftThumb, state, stateFlags, false);
        checkToAdd("RightThumb", GamepadButtonFlags.RightThumb, state, stateFlags, false);
        checkToAdd("DPadDown", GamepadButtonFlags.DPadDown, state, stateFlags);
        checkToAdd("DPadLeft", GamepadButtonFlags.DPadLeft, state, stateFlags);
        checkToAdd("DPadRight", GamepadButtonFlags.DPadRight, state, stateFlags);
        checkToAdd("DPadUp", GamepadButtonFlags.DPadUp, state, stateFlags);
        checkToAdd("RightThumbX", state.Gamepad.RightThumbX, state, stateFlags, true, 100);
        checkToAdd("RightThumbY", state.Gamepad.RightThumbX, state, stateFlags, true, 100);
        checkToAdd("LeftThumbX", state.Gamepad.RightThumbX, state, stateFlags, true, 100);
        checkToAdd("LeftThumbY", state.Gamepad.RightThumbX, state, stateFlags, true, 100);
        checkToAdd("RightTrigger", state.Gamepad.RightThumbX, state, stateFlags, true);
        checkToAdd("LeftTrigger", state.Gamepad.RightThumbX, state, stateFlags, true);
    }

    private static bool isPushed(string key, Dictionary<String, InputControl> stateFlags)
    {
        bool ret = stateFlags[key].triggers() && !lastStateFlags[key].triggered;
        if (ret) System.Diagnostics.Debug.WriteLine("Triggered at " + DateTime.UtcNow.Ticks.ToString());
        return ret;
    }

    private static Dictionary<String, InputControl> deepCopy()
    {
        Dictionary<String, InputControl> stateFlags = new Dictionary<string, InputControl>();
        if (lastStateFlags != null)
            foreach(string key in lastStateFlags.Keys)
            {
                stateFlags.Add(key, lastStateFlags[key].clone());
            }
        return stateFlags;
    }

    public static void Update(State state)
    {
        Dictionary<String, InputControl> stateFlags  = deepCopy();
        checkStates(state, stateFlags);

        switch (currentSubmenu)
        {
            case submenu.None:
                if (stateFlags["A"].triggers()) KeyOutputManager.A_PRESS();
                if (stateFlags["B"].triggers()) KeyOutputManager.B_PRESS();
                if (stateFlags["X"].triggers()) KeyOutputManager.X_PRESS();
                if (isPushed("Y", stateFlags)) KeyOutputManager.Y_PRESS();
                if (stateFlags["DPadDown"].triggers()) KeyOutputManager.DPAD_DOWN();
                if (stateFlags["DPadLeft"].triggers()) KeyOutputManager.DPAD_LEFT();
                if (stateFlags["DPadUp"].triggers()) KeyOutputManager.DPAD_UP();
                if (stateFlags["DPadRight"].triggers()) KeyOutputManager.DPAD_RIGHT();
                if (isPushed("Back", stateFlags)) KeyOutputManager.MENU_PRESS();

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
