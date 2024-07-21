using HieuLeague.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HieuLeague.Content.Buffs
{ 
	public class GaleforceCD : ModBuff
	{
		public override void SetStaticDefaults()
		{
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = false; 
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.GetModPlayer<HieuModPlayer2>().GaleforceCooldown >= 0)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}
