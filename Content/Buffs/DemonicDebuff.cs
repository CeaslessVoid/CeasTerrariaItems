using HieuLeague.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HieuLeague.Content.Buffs
{ 
	public class DemonicDebuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
            Main.debuff[Type] = true; // This makes it a debuff
            Main.buffNoSave[Type] = true; // The buff will not be saved when exiting the game
            Main.buffNoTimeDisplay[Type] = false; // The buff time will be shown
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            int damage = (int)(npc.lifeMax * 0.05f);
            if (damage > 500)
            {
                damage = 500;
            }

            if (damage < 1)
            {
                damage = 1;
            }

            npc.lifeRegen -= damage * 2; // Buffs apply life regen in 1/2 HP per second, so multiplying by 2 gets us the correct amount

        }

    }
}
