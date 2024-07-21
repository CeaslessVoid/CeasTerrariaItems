using HieuLeague.Common.Players;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace HieuLeague.Content.Items.Accessories
{
    public class Galeforce : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[base.Type] = 1;
        }

        public override void SetDefaults()
        {
            base.Item.width = 36;
            base.Item.height = 32;
            base.Item.accessory = true;
            base.Item.value = Item.sellPrice(0, 2, 0, 0);
            base.Item.rare = ItemRarityID.Master;
        }

        public override void AddRecipes()
        {
            Mod templateMod2 = null;
            ModLoader.TryGetMod("WeaponAugs", out templateMod2);


            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(templateMod2.Find<ModItem>("PowerShardUlt"), 15);
            recipe.AddIngredient(templateMod2.Find<ModItem>("LightweightJewelUlt"), 1);
            recipe.AddIngredient<Zeal>(1);
            recipe.AddIngredient<Dagger>(2);
            recipe.AddIngredient(ItemID.ShroomiteBar, 15);
            recipe.AddIngredient(ItemID.AvengerEmblem, 1);
            recipe.AddIngredient(ItemID.PlatinumCoin, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public static readonly int BaseAttackSpeed = 19;
        public static readonly int AdditiveDamageBonus = 10;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetCritChance(DamageClass.Generic) += 25f;

            player.GetAttackSpeed(DamageClass.Generic) += BaseAttackSpeed / 100f;

            player.GetDamage(DamageClass.Melee) += AdditiveDamageBonus / 100f;
            player.GetDamage(DamageClass.Ranged) += AdditiveDamageBonus / 100f;

            player.GetModPlayer<HieuModPlayer2>().HasGaleforce = true;
        }
    }

    public class GaleforceProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.light = 0.5f;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            // Homing behavior
            float maxDetectRadius = 400f; // The maximum radius at which a target will be detected
            float projSpeed = 10f; // The speed at which the projectile will pursue the target

            NPC closestNPC = FindClosestNPC(maxDetectRadius);
            if (closestNPC == null) return;

            // Change the velocity to point towards the target
            Vector2 direction = closestNPC.Center - Projectile.Center;
            direction.Normalize();
            direction *= projSpeed;

            Projectile.velocity = (Projectile.velocity * (Projectile.extraUpdates - 1) + direction) / Projectile.extraUpdates;
        }

        private NPC FindClosestNPC(float maxDetectDistance)
        {
            NPC closestNPC = null;
            float closestDistance = maxDetectDistance;

            foreach (NPC npc in Main.npc)
            {
                if (npc.CanBeChasedBy(Projectile))
                {
                    float distance = Vector2.Distance(Projectile.Center, npc.Center);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestNPC = npc;
                    }
                }
            }

            return closestNPC;
        }
    }
}
