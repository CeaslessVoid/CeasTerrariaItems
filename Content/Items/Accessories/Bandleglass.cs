using HieuLeague.Common.Players;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace HieuLeague.Content.Items.Accessories
{
    public class Bandleglass : ModItem
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
            base.Item.value = Item.sellPrice(0, 5, 0, 0);
            base.Item.rare = 4;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<Charm>(2);
            recipe.AddIngredient(ItemID.MythrilBar, 5);
            recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddIngredient(ItemID.GoldCoin, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient<Charm>(2);
            recipe.AddIngredient(ItemID.OrichalcumBar, 5);
            recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddIngredient(ItemID.GoldCoin, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public static readonly int AdditiveDamageBonus = 12;
        public static readonly int AdditiveDamageBonus2 = 4;

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Summon) += AdditiveDamageBonus / 100f;
            player.GetDamage(DamageClass.Magic) += AdditiveDamageBonus2 / 100f;
            player.manaRegen += (int)((float)player.manaRegen * 0.75f);
            player.maxMinions += 2;

            player.GetModPlayer<HieuModPlayer>().hasBandleglass = true;
        }
    }
}
