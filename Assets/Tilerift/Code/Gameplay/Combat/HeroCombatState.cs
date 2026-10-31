using System;

namespace Tilerift.Gameplay.Combat
{
    public sealed class HeroCombatState
    {
        public HeroCombatState(int combatBonus, int baseHitPoints, int armorHitPoints = 0)
        {
            Stats = new CombatantStats(combatBonus, baseHitPoints, armorHitPoints);
        }

        public CombatantStats Stats { get; private set; }

        public void ReplaceArmor(int armorHitPoints)
        {
            Stats = new CombatantStats(Stats.CombatBonus, Stats.BaseHitPoints, Math.Max(0, armorHitPoints));
        }

        public void Apply(CombatResolution resolution)
        {
            Stats = new CombatantStats(Stats.CombatBonus, resolution.HeroBaseHitPointsAfter, resolution.HeroArmorAfter);
        }
    }
}
