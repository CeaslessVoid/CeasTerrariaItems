using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace HieuLeague.Content.Items.Accessories
{
    public class AmpTome : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[base.Type] = 1;
        }

        public override void SetDefaults()
        {
            base.Item.width = 30;
            base.Item.height = 42;
            base.Item.accessory = true;
            base.Item.value = Item.sellPrice(0, 2, 0, 0);
            base.Item.rare = 2;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddIngredient(ItemID.Book, 5);
            recipe.AddIngredient(ItemID.Silk, 5);
            recipe.AddIngredient(ItemID.GoldCoin, 5);
            recipe.AddTile(TileID.Bookcases);
            recipe.Register();
        }

        public static readonly int AdditiveDamageBonus = 7;
        public static readonly int FlatDamageBonus = 1;

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Magic) += AdditiveDamageBonus / 100f;
            player.GetDamage(DamageClass.Summon).Flat += FlatDamageBonus;
            player.maxMinions += 1;
        }
    }
}
