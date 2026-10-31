using System;

namespace Tilerift.Gameplay.Combat
{
    public enum CombatApproach
    {
        Front,
        Side,
        Back
    }

    public readonly struct CombatantStats
    {
        public readonly int CombatBonus;
        public readonly int BaseHitPoints;
        public readonly int ArmorHitPoints;

        public CombatantStats(int combatBonus, int baseHitPoints, int armorHitPoints = 0)
        {
            CombatBonus = Math.Max(0, combatBonus);
            BaseHitPoints = Math.Max(0, baseHitPoints);
            ArmorHitPoints = Math.Max(0, armorHitPoints);
        }

        public int TotalHitPoints => BaseHitPoints + ArmorHitPoints;
    }

    public readonly struct CombatResolution
    {
        public readonly int FacingBonus;
        public readonly int EffectiveHeroCombatBonus;
        public readonly int EffectiveEnemyCombatBonus;
        public readonly int RequiredHitPointCost;
        public readonly int HeroArmorAfter;
        public readonly int HeroBaseHitPointsAfter;
        public readonly bool HeroSurvives;
        public readonly bool EnemyDefeated;
        public readonly float PresentationDurationSeconds;

        public CombatResolution(int facingBonus, int effectiveHeroCombatBonus, int effectiveEnemyCombatBonus,
            int requiredHitPointCost, int heroArmorAfter, int heroBaseHitPointsAfter,
            bool heroSurvives, bool enemyDefeated, float presentationDurationSeconds)
        {
            FacingBonus = facingBonus;
            EffectiveHeroCombatBonus = effectiveHeroCombatBonus;
            EffectiveEnemyCombatBonus = effectiveEnemyCombatBonus;
            RequiredHitPointCost = requiredHitPointCost;
            HeroArmorAfter = heroArmorAfter;
            HeroBaseHitPointsAfter = heroBaseHitPointsAfter;
            HeroSurvives = heroSurvives;
            EnemyDefeated = enemyDefeated;
            PresentationDurationSeconds = presentationDurationSeconds;
        }
    }

    public static class CombatResolver
    {
        public static CombatResolution Resolve(CombatantStats hero, CombatantStats enemy,
            bool heroIsAttacker, CombatApproach approach, float presentationDurationSeconds = 0.5f)
        {
            var facingBonus = approach switch
            {
                CombatApproach.Front => 0,
                CombatApproach.Side => 1,
                CombatApproach.Back => 2,
                _ => throw new ArgumentOutOfRangeException(nameof(approach))
            };

            var effectiveHero = hero.CombatBonus + (heroIsAttacker ? facingBonus : 0);
            var effectiveEnemy = enemy.CombatBonus + (heroIsAttacker ? 0 : facingBonus);
            var cost = Math.Max(0, effectiveEnemy - effectiveHero + 1);
            var armorAfter = Math.Max(0, hero.ArmorHitPoints - cost);
            var baseCost = Math.Max(0, cost - hero.ArmorHitPoints);
            var baseAfter = Math.Max(0, hero.BaseHitPoints - baseCost);
            var survives = baseAfter + armorAfter >= 1;

            return new CombatResolution(facingBonus, effectiveHero, effectiveEnemy, cost,
                armorAfter, baseAfter, survives, survives && enemy.BaseHitPoints > 0,
                Math.Min(0.5f, Math.Max(0f, presentationDurationSeconds)));
        }
    }
}
