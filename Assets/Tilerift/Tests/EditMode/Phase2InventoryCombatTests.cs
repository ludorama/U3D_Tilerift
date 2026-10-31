using NUnit.Framework;
using Tilerift.Gameplay.Combat;
using Tilerift.Gameplay.Inventory;
using Tilerift.Gameplay.Tiles;

namespace Tilerift.Tests.EditMode
{
    public sealed class Phase2InventoryCombatTests
    {
        [Test]
        public void StartingHandAndQueueUseConfiguredCapacityAndFifoOrder()
        {
            var first = new ActionTileState("first", false);
            var second = new ActionTileState("second", false);
            var third = new ActionTileState("third", false);
            var fourth = new ActionTileState("fourth", false);
            var fifth = new ActionTileState("fifth", false);
            var inventory = new TileInventory(3, 5, 3, new[] { first, second, third, fourth, fifth });

            Assert.That(inventory.Hand, Is.EqualTo(new[] { first, second, third }));
            Assert.That(inventory.QueuedCount, Is.EqualTo(2));
            Assert.That(inventory.Preview(), Is.EqualTo(new[] { fourth, fifth }));

            inventory.RemoveFromHand(second);
            Assert.That(inventory.Hand, Is.EqualTo(new[] { first, third, fourth }));
            Assert.That(inventory.QueuedCount, Is.EqualTo(1));
        }

        [Test]
        public void RetrievalEnqueuesAtQueueTailAndHiddenPreviewCountIsStable()
        {
            var a = new ActionTileState("a", false);
            var b = new ActionTileState("b", false);
            var c = new ActionTileState("c", false);
            var d = new ActionTileState("d", false);
            var inventory = new TileInventory(1, 1, 3, new[] { a, b, c });

            inventory.Enqueue(d);
            Assert.That(inventory.Preview(), Is.EqualTo(new[] { b, c, d }));
            Assert.That(inventory.HiddenQueuedCount, Is.EqualTo(0));
        }

        [TestCase(CombatApproach.Front, 3, 2)]
        [TestCase(CombatApproach.Side, 2, 3)]
        [TestCase(CombatApproach.Back, 1, 4)]
        public void HeroAttackingUsesDocumentedFacingBonuses(CombatApproach approach, int expectedCost, int expectedHeroAfter)
        {
            var result = CombatResolver.Resolve(new CombatantStats(2, 5), new CombatantStats(4, 1), true, approach);

            Assert.That(result.RequiredHitPointCost, Is.EqualTo(expectedCost));
            Assert.That(result.HeroBaseHitPointsAfter, Is.EqualTo(expectedHeroAfter));
            Assert.That(result.HeroSurvives, Is.True);
            Assert.That(result.EnemyDefeated, Is.True);
        }

        [TestCase(CombatApproach.Front, 3, 2)]
        [TestCase(CombatApproach.Side, 4, 1)]
        [TestCase(CombatApproach.Back, 5, 0)]
        public void EnemyAttackingAppliesFacingBonusToEnemy(CombatApproach approach, int expectedCost, int expectedBaseAfter)
        {
            var result = CombatResolver.Resolve(new CombatantStats(2, 5), new CombatantStats(4, 1), false, approach);

            Assert.That(result.RequiredHitPointCost, Is.EqualTo(expectedCost));
            Assert.That(result.HeroBaseHitPointsAfter, Is.EqualTo(expectedBaseAfter));
            Assert.That(result.HeroSurvives, Is.EqualTo(expectedBaseAfter > 0));
        }

        [Test]
        public void ArmorIsConsumedBeforeBaseHitPointsAndReplacementDoesNotHealBase()
        {
            var result = CombatResolver.Resolve(new CombatantStats(2, 2, 1), new CombatantStats(4, 1), false, CombatApproach.Front);

            Assert.That(result.RequiredHitPointCost, Is.EqualTo(3));
            Assert.That(result.HeroArmorAfter, Is.EqualTo(0));
            Assert.That(result.HeroBaseHitPointsAfter, Is.EqualTo(0));
            Assert.That(result.HeroSurvives, Is.False);
        }

        [Test]
        public void ArmorReplacementChangesArmorOnly()
        {
            var hero = new HeroCombatState(2, 2, 1);

            hero.ReplaceArmor(4);

            Assert.That(hero.Stats.BaseHitPoints, Is.EqualTo(2));
            Assert.That(hero.Stats.ArmorHitPoints, Is.EqualTo(4));
        }

        [Test]
        public void CombatPresentationIsCappedAtHalfSecond()
        {
            var result = CombatResolver.Resolve(new CombatantStats(2, 5), new CombatantStats(4, 1), true, CombatApproach.Front, 2f);

            Assert.That(result.PresentationDurationSeconds, Is.EqualTo(0.5f));
        }

        [Test]
        public void HeroPriorityWinsSimultaneousContention()
        {
            var heroAttacks = CombatResolver.Resolve(new CombatantStats(2, 5), new CombatantStats(4, 1), true, CombatApproach.Front);
            Assert.That(heroAttacks.EnemyDefeated, Is.True);
            Assert.That(heroAttacks.HeroSurvives, Is.True);
        }
    }
}
