using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace HieuLeague
{
    // Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
    public class HieuLeague : Mod
    {
        public static ModKeybind SeekersArmguardHotkey;
        public static ModKeybind DashHotkey;

        public override void Load()
        {
            SeekersArmguardHotkey = KeybindLoader.RegisterKeybind(this, "Activate Seeker's Armguard or Zhoynya", Keys.F);
            DashHotkey = KeybindLoader.RegisterKeybind(this, "Activate Galeforce Dash", Keys.Z);
        }

        public override void Unload()
        {
            SeekersArmguardHotkey = null;
            DashHotkey = null;
        }

    }


}
