using HieuLeague.Common.Players;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace HieuLeague.Content.Items.Accessories
{
    public class PhantomDancer : ModItem
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
            recipe.AddIngredient(ItemID.ChlorophyteBar, 15);
            recipe.AddIngredient(ItemID.AvengerEmblem, 1);
            recipe.AddIngredient(ItemID.PlatinumCoin, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public static readonly int BaseAttackSpeed = 17;
        public static readonly int AdditiveDamageBonus = 14;
        public static readonly int AdditiveDamageBonus2 = 9;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetCritChance(DamageClass.Generic) += 25f;

            player.GetAttackSpeed(DamageClass.Generic) += BaseAttackSpeed / 100f;

            player.GetDamage(DamageClass.Melee) += AdditiveDamageBonus2 / 100f;
            player.GetDamage(DamageClass.Ranged) += AdditiveDamageBonus / 100f;


            player.GetModPlayer<HieuModPlayer2>().hasPhantomDancer = true;
        }
    }

    public class PenetrationProjectile : GlobalProjectile
    {
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (projectile.owner == Main.myPlayer)
            {
                Player player = Main.player[projectile.owner];
                if (player.GetModPlayer<HieuModPlayer2>().hasPhantomDancer)
                {
                    if (projectile.penetrate != -1)
                    {
                        projectile.penetrate += 3;
                    }
                }
            }
        }
    }
}
