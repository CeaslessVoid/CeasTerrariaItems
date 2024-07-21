using HieuLeague.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HieuLeague.Content.Buffs
{ 
	public class GuardianAngelBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = false;
            Main.buffNoTimeDisplay[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.GetModPlayer<HieuModPlayer2>().GardianAngelCooldown <= 0)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }

    }
}
