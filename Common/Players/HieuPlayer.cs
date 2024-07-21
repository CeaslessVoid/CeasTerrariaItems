using HieuLeague.Content.Buffs;
using HieuLeague.Content.Items.Accessories;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using System.Security.Cryptography.X509Certificates;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.WorldBuilding;

namespace HieuLeague.Common.Players
{

    public class HieuModPlayer : ModPlayer
    {
        public bool HasDoranPassive;
        public int DoranCooldown;

        public bool HasCullPassive;

        public bool HasDarkSeal;
        public int DarkSealStack;

        public bool HasTear;
        public int TearStack;

        public bool hasBamisCinder;
        private int BamiCooldown = 300; // 5 seconds * 60 frames per second

        public bool hasBandleglass;

        public bool hasBlighting;

        public bool hasBramble;

        public bool hasPhage;
        public int PhageTime;

        public bool hasVerdant;
        public int VerdantTime;
        public int VerdantCooldown;

        public bool hasSunfire;

        public bool hasThornMail;

        public bool hasDeathDance;
        public int DeathDanceDamage;

        public bool hasVoidStaff;

        public bool hasLordDominik;

        public override void ResetEffects()
        {
            HasDoranPassive = false;
            HasCullPassive = false;
            HasDarkSeal = false;
            HasTear = false;
            hasBamisCinder = false;
            hasBandleglass = false;
            hasBlighting = false;
            hasBramble = false;
            hasPhage = false;
            hasVerdant = false;
            hasSunfire = false;
            hasThornMail = false;
            hasDeathDance = false;
            hasVoidStaff = false;
            hasLordDominik = false;
        }

        public override void UpdateDead()
        {
            base.UpdateDead();

            VerdantCooldown = 0;
            VerdantTime = 0;
            DeathDanceDamage = 0;
        }

        public override void PostUpdate()
        {
            base.PostUpdate();

            if (hasBamisCinder)
            {
                if (BamiCooldown <= 0)
                {
                    BamiDamageToNearbyEnemies();
                    CreateBamiVisualEffects();
                    BamiCooldown = 300;
                }
                else
                {
                    BamiCooldown--;
                }
            }

            if (hasSunfire)
            {
                if (BamiCooldown <= 0)
                {
                    SunfireDamageToNearbyEnemies();
                    CreateBamiVisualEffects();
                    BamiCooldown = 300;
                }
                else
                {
                    BamiCooldown--;
                }
            }

            if (DoranCooldown > 0)
            {
                DoranCooldown--;
            }

            if (PhageTime > 0)
            {
                PhageTime--;
                Player.lifeRegen += (int)(Player.lifeRegen * 0.2f);
            }

            if (VerdantTime > 0)
            {
                VerdantTime--;
            }

            if (VerdantCooldown > 0)
            {
                VerdantCooldown--;
            }

            if (VerdantTime == 0 && VerdantCooldown == 0 && hasVerdant)
            {
                Player.AddBuff(ModContent.BuffType<VerdantReady>(), -1);
            }

            DeathDanceDamage--;

        }

        public override void UpdateEquips()
        {
            base.UpdateEquips();
            
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            base.OnHurt(info);

            if (HasDoranPassive && DoranCooldown <= 0)
            {
                Player.AddBuff(BuffID.Regeneration, 300); // 5 seconds
                DoranCooldown = 900; // 15 seconds
            }

            DarkSealStack = 0;

            if (hasVerdant && VerdantCooldown == 0 && VerdantTime == 0)
            {
                VerdantTime = 180;
                VerdantCooldown = 3600;
                Player.AddBuff(ModContent.BuffType<VerdantNotReady>(), 3600);
            }

        }

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            base.OnHitByNPC(npc, hurtInfo);

            if (hasBramble)
            {
                Player.ApplyDamageToNPC(npc, 10, 1f, 0, true, DamageClass.Melee);
            }

            if (hasThornMail)
            {
                int Damage = (Player.statDefense * 3) + 6;
                Player.ApplyDamageToNPC(npc, Damage, 1f, 0, true, DamageClass.Melee);
            }
            
        }

        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            base.OnHitByProjectile(proj, hurtInfo);


            if (hasBramble && proj.hostile)
            {
                if (proj.owner >= 0 && proj.owner < Main.maxNPCs)
                {
                    NPC npc = Main.npc[proj.owner];

                    Player.ApplyDamageToNPC(npc, 10, 1f, 0, true, DamageClass.Melee);
                }
            }

            if (hasThornMail)
            {
                if (proj.owner >= 0 && proj.owner < Main.maxNPCs)
                {
                    NPC npc = Main.npc[proj.owner];
                    int Damage = (Player.statDefense * 3) + 6;
                    Player.ApplyDamageToNPC(npc, Damage, 1f, 0, true, DamageClass.Melee);

                }

            }

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {

            base.OnHitNPC(target, hit, damageDone);

            if (HasCullPassive)
            {
                target.AddBuff(BuffID.Midas, -1); // Forever
            }

            if (HasDarkSeal && DarkSealStack < 10)
            {
                DarkSealStack++;
            }

            if (HasTear && TearStack < 50)
            {
                TearStack++;
            }

        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPCWithItem(item, target, hit, damageDone);

            if (hasPhage)
            {
                 PhageTime = 180;
            }

        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPCWithProj(proj, target, hit, damageDone);

            if (hasBandleglass && proj.DamageType != DamageClass.Ranged)
            {
                BandleglassRestoreMana(target, damageDone);
            }

        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            base.ModifyHitNPCWithProj(proj, target, ref modifiers);

            if (hasBlighting && proj.DamageType != DamageClass.Ranged)
            {
                modifiers.ArmorPenetration += target.defense * 0.15f;
            }

            if (hasVoidStaff && proj.DamageType != DamageClass.Ranged)
            {
                modifiers.ArmorPenetration += target.defense * 0.45f;
            }

            if (hasLordDominik && proj.DamageType == DamageClass.Ranged)
            {
                modifiers.ArmorPenetration += target.defense * 0.4f;
            }
        }

        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            base.ModifyHitByNPC(npc, ref modifiers);

            if (hasVerdant && (VerdantCooldown == 0 || VerdantTime > 0))
            {
                modifiers.FinalDamage *= 0.5f;
            }

            if (hasDeathDance)
            {
                if (npc.type == NPCID.HallowBoss && Main.dayTime)
                {
                    return;
                }

                if (DeathDanceDamage <= 601)
                {
                    modifiers.FinalDamage *= 0;
                    Player.statLife -= (int)(Player.statLifeMax2 * 0.35f);
                    DeathDanceDamage += 200;
                }
                else
                {
                    Player.statLife -= 9999;
                }
            }
            
        }

        public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        {
            base.ModifyHitByProjectile(proj, ref modifiers);
            if (hasVerdant && (VerdantCooldown == 0 || VerdantTime > 0))
            {
                modifiers.FinalDamage *= 0.5f;
            }

            if (hasDeathDance)
            {
                NPC projectileOwner = Main.npc[proj.owner];
                if (projectileOwner.type == NPCID.HallowBoss && Main.dayTime)
                {
                    return;
                }

                if (DeathDanceDamage <= 601)
                {
                    modifiers.FinalDamage *= 0;
                    Player.statLife -= (int)(Player.statLifeMax2 * 0.35f);
                    DeathDanceDamage += 200;
                }
                else
                {
                    Player.statLife -= 9999;
                }
            }
        }

        private void BamiDamageToNearbyEnemies()
        {
            float damageRadius = 100f; // Radius within which enemies will take damage
            int damageAmount = (int)(Player.statLifeMax2 * 0.05f); // 5% of max health as damage

            foreach (NPC npc in Main.npc)
            {
                if (npc.active && !npc.friendly && npc.Distance(Player.Center) < damageRadius)
                {
                    int direction = npc.Center.X > Player.Center.X ? 1 : -1;
                    Player.ApplyDamageToNPC(npc, damageAmount, 0f, direction, true, DamageClass.Melee);
                }
            }
        }

        private void SunfireDamageToNearbyEnemies()
        {
            float damageRadius = 100f; // Radius within which enemies will take damage
            int damageAmount = (int)(Player.statLifeMax2 * 1.5f); // 5% of max health as damage

            foreach (NPC npc in Main.npc)
            {
                if (npc.active && !npc.friendly && npc.Distance(Player.Center) < damageRadius)
                {
                    int direction = npc.Center.X > Player.Center.X ? 1 : -1;
                    Player.ApplyDamageToNPC(npc, damageAmount, 0f, direction, true, DamageClass.Melee);
                }
            }
        }

        private void CreateBamiVisualEffects()
        {
            float effectRadius = 100f; // Radius of the visual effect
            for (int i = 0; i < 30; i++)
            {
                Vector2 position = Player.Center + Vector2.UnitX.RotatedByRandom(MathHelper.TwoPi) * effectRadius;
                Dust.NewDustPerfect(position, DustID.Torch, Vector2.Zero, 100, Color.Orange, 1.5f);
            }
        }

        private void BandleglassRestoreMana(NPC target, int damage)
        {
            // Restore a portion of the mana based on the damage dealt
            //int manaRestored = damage / 100; // 1% of the damage dealt
            int manaRestored = 3;
            Player.statMana += manaRestored;
            Player.ManaEffect(manaRestored);

            for (int i = 0; i < 10; i++)
            {
                Vector2 position = target.Center + new Vector2(Main.rand.Next(-20, 21), Main.rand.Next(-20, 21));
                Vector2 velocity = Player.Center - position;
                velocity.Normalize();
                velocity *= 5f;
                Dust dust = Dust.NewDustPerfect(position, DustID.BlueCrystalShard, velocity, 150, Color.Blue, 1.5f);
                dust.noGravity = true;
            }
        }

    }


    public class HieuModPlayer2 : ModPlayer
    {
        public bool hasSheen;

        public bool hasSeekersArmguard; // Use for the Zhonya
        public int SeekerTime;
        public int SeekerCooldown;

        public bool hasDemonicEmbrace;

        public bool hasGuardianAngel;
        public int GardianAngelCooldown;

        public bool hasSpiritVisage;

        public bool hasPhantomDancer;

        public bool HasSeraphEmbrace;

        public bool hasBlackCleaver;

        public bool hasZeke;

        public bool hasFrozenHeart;

        public bool hasEclipse;

        public bool hasInfinityEdge;

        public bool HasRapidFire;

        public bool HasGaleforce;
        public int GaleforceCooldown;

        public bool HasGuinsooRageblade;
        public int GuinsooRagebladestacks = 0;
        public int GuinsooReset;

        public bool HasLichBane;

        public bool HasKnightVow;

        public bool HasRedemption;
        private int healTimer;

        public bool HasRabadon;

        public override void ResetEffects()
        {
            base.ResetEffects();
            hasSheen = false;
            hasSeekersArmguard = false;
            hasGuardianAngel = false;
            hasSpiritVisage = false;
            hasPhantomDancer = false;
            HasSeraphEmbrace = false;
            hasBlackCleaver = false;
            hasZeke = false;
            hasFrozenHeart = false;
            hasEclipse = false;
            hasInfinityEdge = false;
            HasRapidFire = false;
            HasGaleforce = false;
            HasGuinsooRageblade = false;
            HasLichBane = false;
            HasKnightVow= false;
            HasRedemption = false;
            HasRabadon = false;
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            base.ModifyHitNPCWithProj(proj, target, ref modifiers);

            if (hasSheen && proj.DamageType == DamageClass.Ranged && Player.statMana >= Player.statManaMax2 * 0.05f)
            {
                int manaToConsume = (int)(Player.statManaMax2 * 0.07f);
                Player.statMana -= manaToConsume;

                modifiers.FinalDamage *= 1.1f; // Increase damage by 10%

                CreateCircularDustEffect(target);

            }

            if (HasRabadon && proj.DamageType == DamageClass.Ranged)
            {
                modifiers.FinalDamage *= 1.5f;
            }

            if (HasLichBane)
            {
                int manaToConsume = (int)(Player.statMana);
                Player.statMana -= manaToConsume;
                modifiers.FinalDamage *= manaToConsume * 0.02f;
            }
        }

        public override void OnHitAnything(float x, float y, Entity victim)
        {
            base.OnHitAnything(x, y, victim);

            
        }

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            base.OnHitByNPC(npc, hurtInfo);

            if (hasFrozenHeart)
            {
                npc.AddBuff(BuffID.Slow, 300);
            }
        }

        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            base.OnHitByProjectile(proj, hurtInfo);

            if (hasFrozenHeart && proj.hostile)
            {
                if (proj.owner >= 0 && proj.owner < Main.maxNPCs)
                {
                    NPC npc = Main.npc[proj.owner];
                    npc.AddBuff(BuffID.Slow, 300);
                }
            }

        }

        public override void PostUpdate()
        {
            base.PostUpdate();
            if (HasRedemption)
            {
                healTimer++;
                if (healTimer >= 300) // 5 seconds in ticks (60 ticks per second)
                {
                    HealPlayer(Player);
                    HealAllies();
                    healTimer = 0;
                }
            }
        }

        public override bool CanConsumeAmmo(Item weapon, Item ammo)
        {
            if (HasRapidFire && Main.rand.NextFloat() < 0.5f)
            {
                return false;
            }
            return base.CanConsumeAmmo(weapon, ammo);
        }


        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);

            if (hasDemonicEmbrace && hit.DamageType == DamageClass.Magic || hit.DamageType == DamageClass.Summon)
            {
                target.AddBuff(ModContent.BuffType<DemonicDebuff>(), 3600);
            }

            if (hasBlackCleaver)
            {
                target.AddBuff(ModContent.BuffType<Cleaved>(), -1);
            }

            if (hasEclipse)
            {
                int damage = (int)(target.lifeMax * 0.001f);
                if (damage > 1000)
                {
                    damage = 1000;
                }
                target.life -= damage;
            }

            if (HasGuinsooRageblade && GuinsooRagebladestacks <= 35)
            {
                GuinsooRagebladestacks += 5;
                GuinsooReset = 300;
                //Player.GetAttackSpeed(DamageClass.Generic) += 5;
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPCWithProj(proj, target, hit, damageDone);

            if (hasZeke && proj.minion)
            {
                target.AddBuff(BuffID.OnFire, 300); // 5 seconds
                target.AddBuff(BuffID.Frostburn, 300); // 5 seconds
            }

        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            base.ModifyHitNPC(target, ref modifiers);

            if (HasSeraphEmbrace)
            {
                modifiers.FinalDamage *= (int)(Player.statManaMax2 * 0.001);
            }

            if (hasInfinityEdge)
            {
                modifiers.CritDamage += 0.5f;
            }
        }

        private void CreateCircularDustEffect(NPC target)
        {
            int dustCount = 20;
            float radius = 50f;

            for (int i = 0; i < dustCount; i++)
            {
                float angle = MathHelper.TwoPi / dustCount * i;
                Vector2 dustPosition = target.Center + new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * radius;
                Dust dust = Dust.NewDustPerfect(dustPosition, DustID.BlueCrystalShard, Vector2.Zero, 100, Color.Blue, 1.5f);
                dust.noGravity = true;
                dust.velocity = Vector2.Zero;
            }
        }

        public override void PreUpdate()
        {
            base.PreUpdate();

            if (HasGaleforce && HieuLeague.DashHotkey.JustPressed && GaleforceCooldown <= 0)
            {
                DashTowardsMouse();
                GaleforceCooldown = 600;
                Player.AddBuff(ModContent.BuffType<GaleforceCD>(), GaleforceCooldown);
            }

            if (SeekerTime > 0)
            {
                SeekerTime--;
                Player.immune = true;
                Player.immuneTime = 2; // Keeps the player invulnerable
                Player.velocity = Vector2.Zero; // Prevents the player from moving
                Player.controlUseItem = false; // Prevents item use
                Player.controlUseTile = false; // Prevents tile interaction
                Player.controlJump = false; // Prevents jumping
                Player.controlHook = false; // Prevents grappling hook use
                Player.controlMount = false; // Prevents mount use
            }

            if (SeekerCooldown > 0)
            {
                SeekerCooldown--;
            }

            if (GardianAngelCooldown > 0)
            {
                GardianAngelCooldown--;
            }

            if (GaleforceCooldown  > 0)
            {
                GaleforceCooldown--;
            }

            if (GuinsooReset >= 0) ;
            {
                GuinsooReset--;

                if (GuinsooReset <= 0)
                {
                    GuinsooRagebladestacks = 0;
                }
            }

        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {

            base.ProcessTriggers(triggersSet);

            if (hasSeekersArmguard && HieuLeague.SeekersArmguardHotkey.JustPressed && SeekerCooldown <= 0)
            {
                SeekerTime = 180; // 3 seconds (60 ticks per second)
                SeekerCooldown = 3600; // 60 seconds
                Player.AddBuff(ModContent.BuffType<SeekersBuff>(), SeekerCooldown); // Assuming you use ShadowDodge as the invulnerability effect
            }
            
        }

        public override void UpdateEquips()
        {
            base.UpdateEquips();

            if (SeekerCooldown > 0)
            {
                Player.immuneAlpha = 255;

                // Apply a golden glow effect
                Lighting.AddLight(Player.Center, 1f, 0.84f, 0f); // RGB for golden color
            }
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genDust, ref PlayerDeathReason damageSource)
        {
            if (hasGuardianAngel && GardianAngelCooldown <= 0) // && GardianAngelCooldown <= 0
            {
                base.Player.immuneTime = 180;
                Player.statLife += Player.statLifeMax2 / 2;
                Player.statMana = Player.statManaMax2;
                GardianAngelCooldown = 10800;
                Player.AddBuff(ModContent.BuffType<GuardianAngelBuff>(), GardianAngelCooldown);
                SoundEngine.PlaySound(SoundID.Item29, Player.position);

                CreateStarDustEffect(Player.Center);
                return false;
            }


            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genDust, ref damageSource);
        }

        private void HealPlayer(Player player)
        {
            float missingHealth = player.statLifeMax2 - player.statLife;
            int healAmount = (int)(missingHealth * 0.05f); // 5% of missing health
            player.statLife += healAmount;
            player.HealEffect(healAmount);
        }

        private void HealAllies()
        {
            foreach (Player ally in Main.player)
            {
                if (ally.active && !ally.dead && ally.team == Player.team && ally.whoAmI != Player.whoAmI)
                {
                    float missingHealth = ally.statLifeMax2 - ally.statLife;
                    int healAmount = (int)(missingHealth * 0.05f); // 5% of missing health
                    ally.statLife += healAmount;
                    ally.HealEffect(healAmount);
                }
            }
        }

        private void CreateStarDustEffect(Vector2 position)
        {
            int dustType = DustID.GoldCoin;
            int numDustParticles = 30;
            float radius = 50f;

            for (int i = 0; i < numDustParticles; i++)
            {
                double angle = MathHelper.TwoPi * i / numDustParticles;
                Vector2 dustPosition = position + radius * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                Dust dust = Dust.NewDustPerfect(dustPosition, dustType, Vector2.Zero, 0, Color.Goldenrod, 1.5f);
                dust.noGravity = true;
                dust.fadeIn = 1f;
                dust.alpha = 100;  // Make dust last longer
                dust.velocity *= 0.3f;  // Slow down the dust particles to make them stay around longer
            }
        }

        public override void UpdateLifeRegen()
        {
            base.UpdateLifeRegen();

            if (hasSpiritVisage)
            {
                foreach (Player ally in Main.player)
                {
                    if (ally.active && !ally.dead && ally.team == Player.team && ally.whoAmI != Player.whoAmI && Player.Distance(ally.Center) < 1000f)
                    {

                        HieuModPlayer2 modPlayer = ally.GetModPlayer<HieuModPlayer2>();
                        if (!modPlayer.hasSpiritVisage)
                        {
                            ally.lifeRegen += (Player.lifeRegen / 2);

                        }
                    }
                }
            }
        }

        private void DashTowardsMouse()
        {
            Vector2 dashDirection = Main.MouseWorld - Player.Center;
            dashDirection.Normalize();
            Player.velocity = dashDirection * 20f;

            // Create a dash trail effect
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Player.position, Player.width, Player.height, DustID.Smoke, Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 150, default(Color), 1.2f);
            }

            // Shoot a homing projectile
            Vector2 projectileDirection = Main.MouseWorld - Player.Center;
            projectileDirection.Normalize();
            int damage = (int)(Player.GetTotalDamage(DamageClass.Ranged).ApplyTo(50));
            Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, projectileDirection * 10f, ModContent.ProjectileType<GaleforceProjectile>(), damage, 1f, Player.whoAmI);
        }

    }

}