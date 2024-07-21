using HieuLeague.Content.Items.Accessories;
using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace HieuLeague.Content.Items
{
    public class HNPC : GlobalNPC
    {
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {   
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Cull>(), 1000, 1, 1)); // 1/1000 drop, 1 to 1
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DarkSeal>(), 1000, 1, 1)); // 1/1000 drop, 1 to 1
        }
    }
}