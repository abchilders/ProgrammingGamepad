using WindowsInput;

public static class KeyOutputManager
{
    private static IKeyboardSimulator _keyboard = new InputSimulator().Keyboard;
    /** REQUIRED:
        * Start -> Open Keyboard -> Sub inputs
    X + RSTICK Press -> Period
        * RSTICK Move -> Submenu Navigation (Libraries)
        * Right bumper/left bumper -> Select premade variables
    X + LSTICK move -> Navigate code
    X + DPAD U/D/L/R -> Navigate code
        * LSTICK Press -> Open Symbole Menu
    X + Y -> Save
    X + A -> Space
    X + B -> New Line
    X + X -> Backspace
    X + Menu -> Select Files Tab
    X * Right Trigger Hold -> Start Highlighting
    X + Left Trigger -> Copy
    X + Left Trigger Half 1 second -> Cut
    X + Right Trigger Half 1 second -> Paste
        * 
        */
    public static void MENU_PRESS()
    {
        _keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.MENU);
    }

    public static void RightTriggerHalfHeld()
    {
        _keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.LCONTROL);
        _keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.VK_V);
        _keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.LCONTROL);
    }
    public static void RightTriggerDown()
    {
        _keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.LSHIFT);
    }
    public static void RightTriggerUp()
    {
        _keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.LSHIFT);
    }
    public static void LeftTriggerPress()
    {
        _keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.LCONTROL);
        _keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.VK_C);
        _keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.LCONTROL);
    }
    public static void LeftTriggerHalfHeld()
    {
        _keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.LCONTROL);
        _keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.VK_X);
        _keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.LCONTROL);
    }
    public static void RSTICK_PRESS()
    {
        _keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.OEM_PERIOD);
    }
    public static void Y_PRESS()
    {
        _keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.LCONTROL);
        _keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.VK_S);
        _keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.LCONTROL);
    }
    public static void A_PRESS()
    {
        _keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.SPACE);
    }
    public static void B_PRESS()
    {
        _keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
    }
    public static void X_PRESS()
    {
        _keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.BACK);
    }
    public static void DPAD_UP()
    {
        _keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.UP);
    }
    public static void DPAD_DOWN()
    {
        _keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.DOWN);
    }
    public static void DPAD_LEFT()
    {
        _keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.LEFT);
    }
    public static void DPAD_RIGHT()
    {
        _keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RIGHT);
    }
}
