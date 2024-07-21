using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using HieuLeague.Common.Players;
using Terraria.ID;

namespace HieuLeague.Content.Items.Accessories
{
    public class RubyCR : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[base.Type] = 1;
        }

        public override void SetDefaults()
        {
            base.Item.width = 40;
            base.Item.height = 32;
            base.Item.accessory = true;
            base.Item.value = Item.sellPrice(0, 1, 0, 0);
            base.Item.rare = 2;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Ruby, 5);
            recipe.AddIngredient(ItemID.GoldCoin, 7);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public static readonly int AdditiveHealthBonus = 65;

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += AdditiveHealthBonus;
        }
    }
}
