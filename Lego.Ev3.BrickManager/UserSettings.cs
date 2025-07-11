﻿using System;

namespace Lego.Ev3.BrickManager
{
    public static class UserSettings
    {
        public static Mode Mode { get; set; }

        public static void Initialize()
        {
            Mode = Mode.BASIC;
            try
            {
                Mode = Enum.Parse<Mode>(Properties.Settings.Default.Mode);
            }
            catch 
            {
                Save();
            }
        }

        public static void Save()
        {
            Properties.Settings.Default.Mode = Mode.ToString();
            Properties.Settings.Default.Save();
        }
    }

    public enum Mode
    {
        BASIC = 0,
        ADVANCED = 1,
    }
}
