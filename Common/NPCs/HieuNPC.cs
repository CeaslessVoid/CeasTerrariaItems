using HieuLeague.Content.Items.Accessories;
using MonoMod.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Text;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using HieuLeague.Content.Buffs;

namespace HieuLeague.Common.NPCs
{
    public class HIEUNPC : GlobalNPC

    {

        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }


        public override void SetDefaults(NPC npc)
        {
            if (npc.type == 269 || npc.type == 270 || npc.type == 271 || npc.type == 272 || npc.type == 273 || npc.type == 274 || npc.type == 275 || npc.type == 276 || npc.type == 277 || npc.type == 278 || npc.type == 279 || npc.type == 280 || npc.type == 281 || npc.type == 282 || npc.type == 283 || npc.type == 284 || npc.type == 285 || npc.type == 286 || npc.type == 287 || npc.type == 288 || npc.type == 289 || npc.type == 290 || npc.type == 291 || npc.type == 292 || npc.type == 293 || npc.type == 294 || npc.type == 295 || npc.type == 296)
            {
                npc.defense += 25;
                npc.lifeMax += (npc.lifeMax / 3);
                npc.damage += 10;

            }

            if (npc.type == 290)
            {
                npc.defense += npc.defense * 2;
                npc.lifeMax -= 5000;
            }

            if (Main.hardMode && !npc.boss && !npc.CountsAsACritter)
            {
                if (npc.type == 488)
                {
                    return;
                }
                if (npc.type == 134 || npc.type == 135 || npc.type == 136 || npc.type == 7 || npc.type == 8 || npc.type == 9)
                {
                    npc.lifeMax += npc.lifeMax;
                }
                else
                {
                    npc.defense += 15;
                    npc.lifeMax += (npc.lifeMax / 3);
                    npc.damage += 25;
                    if (NPC.downedPlantBoss)
                    {
                        npc.defense += (npc.defense / 2) + 15;
                        npc.lifeMax += (npc.lifeMax * 3 / 2) + 30;
                        npc.damage += (npc.damage * 2 / 3);
                    }
                    if (NPC.downedMoonlord)
                    {
                        npc.defense += (npc.defense / 2) + 30;
                        npc.lifeMax += (npc.lifeMax * 2) + 30;
                        npc.damage += (npc.damage *3 /2);
                    }
                }
            }

            if (Main.hardMode && npc.boss)
            {
                npc.damage += (npc.damage / 5);
                npc.lifeMax += (npc.lifeMax / 3);
                npc.defense += (npc.defense / 3);

                if (NPC.downedPlantBoss)
                {
                    npc.defense += (npc.defense / 3);
                    npc.lifeMax += (npc.lifeMax / 2);
                    npc.damage += npc.damage / 2;
                }
                if (NPC.downedMoonlord)
                {
                    npc.defense += (npc.defense / 2);
                    npc.lifeMax += npc.lifeMax;
                    npc.damage += (npc.damage * 2 / 3);
                }
            }
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {

            Mod templateMod2 = null;
            ModLoader.TryGetMod("WeaponAugs", out templateMod2);
            

            if (npc.type == 269 || npc.type == 270 || npc.type == 271 || npc.type == 272 || npc.type == 273 || npc.type == 274 || npc.type == 275 || npc.type == 276 || npc.type == 277 || npc.type == 278 || npc.type == 279 || npc.type == 280 || npc.type == 281 || npc.type == 282 || npc.type == 283 || npc.type == 284 || npc.type == 285 || npc.type == 286 || npc.type == 287 || npc.type == 288 || npc.type == 289 || npc.type == 290 || npc.type == 291 || npc.type == 292 || npc.type == 293 || npc.type == 294 || npc.type == 295 || npc.type == 296)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.CopperCoin, 1, 3, 99));
                npcLoot.Add(ItemDropRule.Common(ItemID.SilverCoin, 1, 3, 99)); //WeaponAugs
                npcLoot.Add(ItemDropRule.Common(ItemID.GoldCoin, 1, 1, 3));
                npcLoot.Add(ItemDropRule.Common(ItemID.PlatinumCoin, 50, 1, 1));

                if (templateMod2 != null)
                {
                    npcLoot.Add(ItemDropRule.Common(templateMod2.Find<ModItem>("PowerShardUlt").Type, 75, 1, 2)); //VoidShard
                    npcLoot.Add(ItemDropRule.Common(templateMod2.Find<ModItem>("PowerCrystalUlt").Type, 250, 1, 1));
                    npcLoot.Add(ItemDropRule.Common(templateMod2.Find<ModItem>("VoidShard").Type, 50, 1, 2));
                    npcLoot.Add(ItemDropRule.Common(templateMod2.Find<ModItem>("PrestigeGem").Type, 500, 1, 1));
                    npcLoot.Add(ItemDropRule.Common(templateMod2.Find<ModItem>("UncannyGem").Type, 500, 1, 1));
                    npcLoot.Add(ItemDropRule.Common(templateMod2.Find<ModItem>("PowerShardExo").Type, 500, 1, 1));
                }
            }

            if (npc.boss && NPC.downedPlantBoss && templateMod2 != null)
            {
                npcLoot.Add(ItemDropRule.Common(templateMod2.Find<ModItem>("PowerShardUlt").Type, 15, 3, 5)); //VoidShard
                npcLoot.Add(ItemDropRule.Common(templateMod2.Find<ModItem>("PowerCrystalUlt").Type, 100, 1, 1)); //PowerCrystalUnc
                npcLoot.Add(ItemDropRule.Common(templateMod2.Find<ModItem>("VoidShard").Type, 1, 1, 1)); //PowerShardUnc
                npcLoot.Add(ItemDropRule.Common(templateMod2.Find<ModItem>("PowerShardUnc").Type, 1, 5, 10));
                npcLoot.Add(ItemDropRule.Common(templateMod2.Find<ModItem>("PowerCrystalUnc").Type, 1, 1, 1));
                npcLoot.Add(ItemDropRule.Common(templateMod2.Find<ModItem>("PrestigeGem").Type, 50, 1, 1));
                npcLoot.Add(ItemDropRule.Common(templateMod2.Find<ModItem>("UncannyGem").Type, 50, 1, 1));
                npcLoot.Add(ItemDropRule.Common(templateMod2.Find<ModItem>("PowerShardExo").Type, 50, 1, 1));
            }
        }

        public int burnTimer = 60; // 60 ticks = 1 second

        public override void ResetEffects(NPC npc)
        {
            if (npc.HasBuff(ModContent.BuffType<DemonicDebuff>()))
            {
                burnTimer = 60; // Reset the timer every tick
            }
        }

        public override void AI(NPC npc)
        {
            if (npc.HasBuff(ModContent.BuffType<DemonicDebuff>()))
            {
                if (Main.rand.NextFloat() < 0.5f) // Adjust the spawn rate of the dust
                {
                    Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.PurpleTorch, 0f, 0f, 100, default, 1.5f);
                    dust.noGravity = true;
                    dust.velocity *= 0.75f;
                }
            }

        }

    }

}