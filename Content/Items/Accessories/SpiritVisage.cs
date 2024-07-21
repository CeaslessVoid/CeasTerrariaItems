using HieuLeague.Common.Players;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace HieuLeague.Content.Items.Accessories
{
    public class SpiritVisage : ModItem
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
            base.Item.rare = ItemRarityID.Master;
        }
        public override void AddRecipes()
        {
            Mod templateMod2 = null;
            ModLoader.TryGetMod("WeaponAugs", out templateMod2);


            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(templateMod2.Find<ModItem>("PowerShardUlt"), 15);
            recipe.AddIngredient(templateMod2.Find<ModItem>("RevitalizeJewelUlt"), 1);
            recipe.AddIngredient<Spectre>(1);
            recipe.AddIngredient<RubyCR>(2);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 15);
            recipe.AddIngredient(ItemID.CharmofMyths, 1);
            recipe.AddIngredient(ItemID.PlatinumCoin, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public static readonly int BaseHealthRegen = 2;
        public static readonly int AdditiveHealthBonus = 200;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeRegen += BaseHealthRegen;
            player.endurance += 0.09f;
            player.statLifeMax2 += AdditiveHealthBonus;

            player.GetModPlayer<HieuModPlayer2>().hasSpiritVisage = true;
        }
    }
}
