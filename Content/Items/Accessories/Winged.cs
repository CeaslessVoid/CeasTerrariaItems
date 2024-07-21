using HieuLeague.Common.Players;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace HieuLeague.Content.Items.Accessories
{
    public class Winged : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[base.Type] = 1;
        }

        public override void SetDefaults()
        {
            base.Item.width = 40;
            base.Item.height = 40;
            base.Item.accessory = true;
            base.Item.value = Item.sellPrice(0, 2, 0, 0);
            base.Item.rare = 4;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<RubyCR>(2);
            recipe.AddIngredient(ItemID.SoulofNight, 15);
            recipe.AddIngredient(ItemID.GoldCoin, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

        }

        public static readonly int AdditiveHealthBonus = 125;

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += AdditiveHealthBonus;
            player.moveSpeed += 0.15f;
        }
    }
}
