using System;

using System.Collections.Generic;

namespace asap.core
{
    public enum KeyCode
    {        
        None = 0,

        // game pad
        DPadUp = -1,
        DPadDown = -2,
        DPadLeft = -3,
        DPadRight = -4,
        Start = -5,
        Back = -6,
        LeftStick = -7,
        RightStick = -8,
        LeftShoulder = -9,
        RightShoulder = -10,
        BigButton = -11,
        A = -12,
        B = -13,
        X = -14,
        Y = -15,
        LeftThumbstickLeft = -16,
        RightTrigger = -17,
        LeftTrigger = -18,
        RightThumbstickUp = -19,
        RightThumbstickDown = -20,
        RightThumbstickRight = -21,
        RightThumbstickLeft = -22,
        LeftThumbstickUp = -23,
        LeftThumbstickDown = -24,
        LeftThumbstickRight = -25,
        
        // keyboard
        VK_Back = 8,
        VK_Tab = 9,
        VK_Enter = 13,
        VK_Pause = 19,
        VK_CapsLock = 20,
        VK_Kana = 21,
        VK_Kanji = 25,
        VK_Escape = 27,
        VK_ImeConvert = 28,
        VK_ImeNoConvert = 29,
        VK_Space = 32,
        VK_PageUp = 33,
        VK_PageDown = 34,
        VK_End = 35,
        VK_Home = 36,
        VK_Left = 37,
        VK_Up = 38,
        VK_Right = 39,
        VK_Down = 40,
        VK_Select = 41,
        VK_Print = 42,
        VK_Execute = 43,
        VK_PrintScreen = 44,
        VK_Insert = 45,
        VK_Delete = 46,
        VK_Help = 47,
        VK_D0 = 48,
        VK_D1 = 49,
        VK_D2 = 50,
        VK_D3 = 51,
        VK_D4 = 52,
        VK_D5 = 53,
        VK_D6 = 54,
        VK_D7 = 55,
        VK_D8 = 56,
        VK_D9 = 57,
        VK_A = 65,
        VK_B = 66,
        VK_C = 67,
        VK_D = 68,
        VK_E = 69,
        VK_F = 70,
        VK_G = 71,
        VK_H = 72,
        VK_I = 73,
        VK_J = 74,
        VK_K = 75,
        VK_L = 76,
        VK_M = 77,
        VK_N = 78,
        VK_O = 79,
        VK_P = 80,
        VK_Q = 81,
        VK_R = 82,
        VK_S = 83,
        VK_T = 84,
        VK_U = 85,
        VK_V = 86,
        VK_W = 87,
        VK_X = 88,
        VK_Y = 89,
        VK_Z = 90,
        VK_LeftWindows = 91,
        VK_RightWindows = 92,
        VK_Apps = 93,
        VK_Sleep = 95,
        VK_NumPad0 = 96,
        VK_NumPad1 = 97,
        VK_NumPad2 = 98,
        VK_NumPad3 = 99,
        VK_NumPad4 = 100,
        VK_NumPad5 = 101,
        VK_NumPad6 = 102,
        VK_NumPad7 = 103,
        VK_NumPad8 = 104,
        VK_NumPad9 = 105,
        VK_Multiply = 106,
        VK_Add = 107,
        VK_Separator = 108,
        VK_Subtract = 109,
        VK_Decimal = 110,
        VK_Divide = 111,
        VK_F1 = 112,
        VK_F2 = 113,
        VK_F3 = 114,
        VK_F4 = 115,
        VK_F5 = 116,
        VK_F6 = 117,
        VK_F7 = 118,
        VK_F8 = 119,
        VK_F9 = 120,
        VK_F10 = 121,
        VK_F11 = 122,
        VK_F12 = 123,
        VK_F13 = 124,
        VK_F14 = 125,
        VK_F15 = 126,
        VK_F16 = 127,
        VK_F17 = 128,
        VK_F18 = 129,
        VK_F19 = 130,
        VK_F20 = 131,
        VK_F21 = 132,
        VK_F22 = 133,
        VK_F23 = 134,
        VK_F24 = 135,
        VK_NumLock = 144,
        VK_Scroll = 145,
        VK_LeftShift = 160,
        VK_RightShift = 161,
        VK_LeftControl = 162,
        VK_RightControl = 163,
        VK_LeftAlt = 164,
        VK_RightAlt = 165,
        VK_BrowserBack = 166,
        VK_BrowserForward = 167,
        VK_BrowserRefresh = 168,
        VK_BrowserStop = 169,
        VK_BrowserSearch = 170,
        VK_BrowserFavorites = 171,
        VK_BrowserHome = 172,
        VK_VolumeMute = 173,
        VK_VolumeDown = 174,
        VK_VolumeUp = 175,
        VK_MediaNextTrack = 176,
        VK_MediaPreviousTrack = 177,
        VK_MediaStop = 178,
        VK_MediaPlayPause = 179,
        VK_LaunchMail = 180,
        VK_SelectMedia = 181,
        VK_LaunchApplication1 = 182,
        VK_LaunchApplication2 = 183,
        VK_OemSemicolon = 186,
        VK_OemPlus = 187,
        VK_OemComma = 188,
        VK_OemMinus = 189,
        VK_OemPeriod = 190,
        VK_OemQuestion = 191,
        VK_OemTilde = 192,
        VK_ChatPadGreen = 202,
        VK_ChatPadOrange = 203,
        VK_OemOpenBrackets = 219,
        VK_OemPipe = 220,
        VK_OemCloseBrackets = 221,
        VK_OemQuotes = 222,
        VK_Oem8 = 223,
        VK_OemBackslash = 226,
        VK_ProcessKey = 229,
        VK_OemCopy = 242,
        VK_OemAuto = 243,
        VK_OemEnlW = 244,
        VK_Attn = 246,
        VK_Crsel = 247,
        VK_Exsel = 248,
        VK_EraseEof = 249,
        VK_Play = 250,
        VK_Zoom = 251,
        VK_Pa1 = 253,
        VK_OemClear = 254,
    }    
}