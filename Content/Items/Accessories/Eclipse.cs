using HieuLeague.Common.Players;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace HieuLeague.Content.Items.Accessories
{
    public class Eclipse : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[base.Type] = 1;
        }

        public override void SetDefaults()
        {
            base.Item.width = 36;
            base.Item.height = 36;
            base.Item.accessory = true;
            base.Item.value = Item.sellPrice(0, 4, 0, 0);
            base.Item.rare = ItemRarityID.Master;
        }

        public override void AddRecipes()
        {
            Mod templateMod2 = null;
            ModLoader.TryGetMod("WeaponAugs", out templateMod2);

            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(templateMod2.Find<ModItem>("PowerShardUlt"), 15);
            recipe.AddIngredient(templateMod2.Find<ModItem>("UnleashJewelUlt"), 1);
            recipe.AddIngredient<Dirk>(1);
            recipe.AddIngredient<Picaxe>(1);
            recipe.AddIngredient(ItemID.SpectreBar, 15);
            recipe.AddIngredient(ItemID.AvengerEmblem, 1);
            recipe.AddIngredient(ItemID.PlatinumCoin, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public static readonly int AdditiveDamageBonus = 15;

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Melee) += AdditiveDamageBonus / 100f;
            player.GetDamage(DamageClass.Ranged) += AdditiveDamageBonus / 100f;

            player.GetArmorPenetration(DamageClass.Melee) += 25;
            player.GetArmorPenetration(DamageClass.Ranged) += 25; //hasEclipse

            player.GetModPlayer<HieuModPlayer2>().hasEclipse = true;
        }
    }
}
