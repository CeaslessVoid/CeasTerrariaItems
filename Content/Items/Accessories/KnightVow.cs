using HieuLeague.Common.Players;
using HieuLeague.Content.Buffs;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;


namespace HieuLeague.Content.Items.Accessories
{
    public class KnightVow : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[base.Type] = 1;
        }

        public override void SetDefaults()
        {
            base.Item.width = 42;
            base.Item.height = 42;
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
            recipe.AddIngredient(templateMod2.Find<ModItem>("ResurgenceJewelUlt"), 1);
            recipe.AddIngredient<BFS>(1);
            recipe.AddIngredient<ChainVest>(1);
            recipe.AddIngredient<LongSword>(1);
            recipe.AddIngredient(ItemID.SpectreBar, 15);
            recipe.AddIngredient(ItemID.DestroyerEmblem, 1);
            recipe.AddIngredient(ItemID.PlatinumCoin, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

        }

        public static readonly int AdditiveDamageBonus = 17;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddBuff(ModContent.BuffType<KnightVowBuff>(), 2);
            player.GetModPlayer<HieuModPlayer2>().HasKnightVow = true;

            if (player.ownedProjectileCounts[ModContent.ProjectileType<Cerula>()] < 1)
            {
                Projectile.NewProjectile(player.GetSource_Accessory(Item), player.Center, player.velocity, ModContent.ProjectileType<Cerula>(), 0, 0f, player.whoAmI);
            }

        }
    }




    public class Cerula : ModProjectile
    {
        private static Texture2D healthBarBackgroundTexture;
        private static Texture2D healthBarFillTexture;

        //public int maxHealth = 0;
        //public int currentHealth;
        //public int passiveCooldown;
        //public int timeSinceLastDamage;

        public override void SetStaticDefaults()
        {
            Main.projPet[Projectile.type] = true;
            Main.projFrames[Projectile.type] = 19;
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.minionSlots = 1f;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.aiStyle = -1;

            
        }

        public override void Load()
        {
            healthBarBackgroundTexture = ModContent.Request<Texture2D>("HieuLeague/Textures/HealthBarBackground").Value;
            healthBarFillTexture = ModContent.Request<Texture2D>("HieuLeague/Textures/HealthBarFill").Value;
        }

        public override void Unload()
        {
            healthBarBackgroundTexture = null;
            healthBarFillTexture = null;
        }

        public override void PostDraw(Color lightColor)
        {
            base.PostDraw(lightColor);

            if (healthBarBackgroundTexture == null || healthBarFillTexture == null)
            {
                Main.NewText("Health bar textures not loaded!");
                return;
            }

            //DrawHealthBar(Main.spriteBatch, Projectile.Center);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteBatch spriteBatch = Main.spriteBatch;
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawPosition = Projectile.Center - Main.screenPosition;
            Rectangle frame = texture.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
            Vector2 origin = frame.Size() / 2;

            spriteBatch.Draw(texture, drawPosition, frame, lightColor, Projectile.rotation, origin, Projectile.scale, Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);

            //DrawHealthBar(spriteBatch, drawPosition);

            return false;
        }

        //private void DrawHealthBar(SpriteBatch spriteBatch, Vector2 drawPosition)
        //{
        //    int barWidth = 40;
        //    int barHeight = 6;
        //    int barPadding = 2;
        //    int healthWidth = (int)((float)currentHealth / maxHealth * (barWidth - barPadding * 2));
        //    Vector2 barPosition = drawPosition - new Vector2(barWidth / 2, Projectile.height / 2 + barHeight + 5);

        //    spriteBatch.Draw(healthBarBackgroundTexture, new Rectangle((int)barPosition.X, (int)barPosition.Y, barWidth, barHeight), Color.Gray);
        //    spriteBatch.Draw(healthBarFillTexture, new Rectangle((int)barPosition.X + barPadding, (int)barPosition.Y + barPadding, healthWidth, barHeight - barPadding * 2), Color.Blue);
        //}

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            // Check if the player has the buff
            if (player.dead || !player.active)
            {
                player.ClearBuff(ModContent.BuffType<KnightVowBuff>());
            }
            if (player.HasBuff(ModContent.BuffType<KnightVowBuff>()))
            {
                Projectile.timeLeft = 2;
            }
            
            //if (maxHealth == 0)
            //{
            //    maxHealth = 1000 * (player.statManaMax2 / 100);
            //    currentHealth = maxHealth;
            //}

            //if (passiveCooldown > 0)
            //{
            //    passiveCooldown--;
            //    if (passiveCooldown == 0) // && timeSinceLastDamage >= 600
            //    {
            //        currentHealth = maxHealth;
            //    }
            //    return;
            //}

            //if (timeSinceLastDamage <= 600)
            //{
            //    timeSinceLastDamage++;
            //    if (timeSinceLastDamage <= 600)
            //    {
            //        currentHealth = maxHealth;
            //    }
            //}

            // Idle position (above the player)
            Vector2 idlePosition = player.Center + new Vector2((player.direction * -60f), -60f); // Adjust idle position here
            float idleDistance = Vector2.Distance(idlePosition, Projectile.Center);

            // If not attacking, return to idle position
            if (idleDistance > 600f)
            {
                Projectile.position = player.Center;
            }
            else if (idleDistance > 20f)
            {
                Vector2 direction = idlePosition - Projectile.Center;
                direction.Normalize();
                Projectile.velocity = direction * 4f;
            }
            else
            {
                Projectile.velocity *= 0.2f;
            }

            // Find the closest NPC
            Vector2 targetPosition = player.Center;
            float distanceFromTarget = 750f;
            bool targetFound = false;
            int targetIndex = -1;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.CanBeChasedBy(this))
                {
                    float distance = Vector2.Distance(npc.Center, Projectile.Center);
                    if (distance < distanceFromTarget)
                    {
                        distanceFromTarget = distance;
                        targetPosition = npc.Center;
                        targetFound = true;
                        targetIndex = i;
                    }
                }
            }

            // Move towards the target and attack if found
            if (targetFound)
            {
                Vector2 direction = targetPosition - Projectile.Center;
                direction.Normalize();
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, direction * 6f, 0.1f); // Adjust speed for attack movement

                // Position on the side closest to the player
                Vector2 playerToTarget = Main.npc[targetIndex].Center - player.Center;
                if (playerToTarget.X > 0)
                {
                    Projectile.Center = Vector2.Lerp(Projectile.Center, new Vector2(Main.npc[targetIndex].Center.X - 30, Main.npc[targetIndex].Center.Y), 0.1f);
                }
                else
                {
                    Projectile.Center = Vector2.Lerp(Projectile.Center, new Vector2(Main.npc[targetIndex].Center.X + 30, Main.npc[targetIndex].Center.Y), 0.1f);
                }

                Projectile.frameCounter++;
                if (Projectile.frameCounter >= 5)
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame++;
                    if (Projectile.frame < 9 || Projectile.frame > 18)
                    {
                        Projectile.frame = 9;
                    }
                }

                if (Projectile.frame == 15)
                {
                    int radius = 50 * (player.maxMinions / 2);
                    int damage = (int)((125 + (player.statDefense/2)) * ( 1 + player.endurance));
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC npc = Main.npc[i];
                        if (npc.active && !npc.friendly && npc.Distance(Projectile.Center) < radius)
                        {
                            player.ApplyDamageToNPC(npc, damage, 0f, 0, crit: true, DamageClass.Summon);
                            npc.AddBuff(BuffID.Frostburn, 120);
                            npc.AddBuff(ModContent.BuffType<Cleaved>(), -1);
                            // Add some dust or gore effects
                            for (int j = 0; j < 10; j++)
                            {
                                Dust.NewDust(npc.position, npc.width, npc.height, DustID.BlueTorch, 0f, 0f, 0, default, 1.5f);
                            }
                        }
                    }

                    // Create a circle of blue particles
                    int particleCount = 20 * (player.maxMinions / 2);
                    for (int i = 0; i < particleCount; i++)
                    {
                        Vector2 particlePosition = Projectile.Center + Vector2.UnitX.RotatedBy(MathHelper.TwoPi / particleCount * i) * radius;
                        Dust.NewDustPerfect(particlePosition, DustID.BlueTorch, Vector2.Zero, 100, default, 1.5f);
                    }
                }
            }
            else
            {
                Projectile.frameCounter++;
                if (Projectile.frameCounter >= 19)
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame++;
                }

                if (Projectile.frame > 7)
                {
                    Projectile.frame = 0;
                }
            }

            // Set direction based on the player's direction
            Projectile.spriteDirection = player.direction;

            // Set direction towards target when attacking
            if (targetFound)
            {
                Projectile.spriteDirection = Projectile.Center.X > targetPosition.X ? -1 : 1;
            }
        }
        //public void ReduceHealth(int amount)
        //{
        //    currentHealth -= amount;
        //    if (currentHealth <= 0)
        //    {
        //        currentHealth = 0;
        //        passiveCooldown = 0;
        //    }
        //    timeSinceLastDamage = 0;
        //}

    }
}
