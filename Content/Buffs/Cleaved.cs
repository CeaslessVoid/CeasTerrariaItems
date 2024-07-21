using HieuLeague.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HieuLeague.Content.Buffs
{ 
	public class Cleaved : ModBuff
	{
		public override void SetStaticDefaults()
		{
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = false; 
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.defense /= 2;

        }

    }
}
