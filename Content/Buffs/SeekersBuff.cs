using HieuLeague.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HieuLeague.Content.Buffs
{ 
	public class SeekersBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
            Main.debuff[Type] = true; // This makes it a debuff (cannot be canceled)
            Main.buffNoTimeDisplay[Type] = false; // The buff time will be shown
            Main.buffNoSave[Type] = true; // The buff will not be saved when exiting the game
        }

        public override void Update(Player player, ref int buffIndex)
        {

            player.immune = true;

            if (player.GetModPlayer<HieuModPlayer2>().SeekerCooldown <= 0)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }

    }
}
