using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace HieuLeague.Content.Items.Accessories
{
    public class Spectre : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[base.Type] = 1;
        }

        public override void SetDefaults()
        {
            base.Item.width = 38;
            base.Item.height = 38;
            base.Item.accessory = true;
            base.Item.value = Item.sellPrice(0, 2, 0, 0);
            base.Item.rare = 5;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<Reju>(1);
            recipe.AddIngredient<RubyCR>(1);
            recipe.AddIngredient<Mantle>(1);
            recipe.AddIngredient(ItemID.JungleSpores, 5);
            recipe.AddIngredient(ItemID.HallowedBar, 1);
            recipe.AddIngredient(ItemID.GoldCoin, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public static readonly int BaseHealthRegen = 1;
        public static readonly int AdditiveHealthBonus = 85;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeRegen += BaseHealthRegen;
            player.endurance += 0.06f;
            player.statLifeMax2 += AdditiveHealthBonus;
        }
    }
}
