using HieuLeague.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HieuLeague.Content.Buffs
{ 
	public class VerdantReady : ModBuff
	{
		public override void SetStaticDefaults()
		{
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.GetModPlayer<HieuModPlayer>().VerdantCooldown > 0 || player.GetModPlayer<HieuModPlayer>().VerdantTime > 0)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }

    }

    public class VerdantNotReady : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
        }

    }
}
