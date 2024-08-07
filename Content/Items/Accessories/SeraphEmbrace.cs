using HieuLeague.Common.Players;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace HieuLeague.Content.Items.Accessories
{
    public class SeraphEmbrace : ModItem
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
            base.Item.rare = ItemRarityID.Master;
        }

        public override void AddRecipes()
        {
            Mod templateMod2 = null;
            ModLoader.TryGetMod("WeaponAugs", out templateMod2);


            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(templateMod2.Find<ModItem>("PowerShardUlt"), 15);
            recipe.AddIngredient(templateMod2.Find<ModItem>("BarrageJewelUlt"), 1);
            recipe.AddIngredient<Tear>(1);
            recipe.AddIngredient<LostChapter>(1);
            recipe.AddIngredient<Fiendish>(1);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 15);
            recipe.AddIngredient(ItemID.CelestialEmblem, 1);
            recipe.AddIngredient(ItemID.PlatinumCoin, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public static readonly int AdditiveDamageBonus = 20;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statManaMax2 += 250;
            player.GetDamage(DamageClass.Magic) += AdditiveDamageBonus / 100f;
            player.GetDamage(DamageClass.Summon) += AdditiveDamageBonus / 100f;
            player.maxMinions += 2;


            player.GetModPlayer<HieuModPlayer>().HasTear = true;
            player.GetModPlayer<HieuModPlayer2>().HasSeraphEmbrace = true;
        }
    }
}
