using NUnit.Framework;
using Tilerift.Core.Grid;
using Tilerift.Gameplay.Travel;

namespace Tilerift.Tests.EditMode
{
    public sealed class Phase3TravelTests
    {
        [Test]
        public void RunStartsInCountdownThenBecomesActive()
        {
            var run = new TravelRunState(10f, 3f, new GridCoordinate(0, 2, 0), 0, 0);

            run.Tick(2.5f);
            Assert.That(run.Status, Is.EqualTo(TravelRunStatus.Countdown));
            Assert.That(run.CountdownRemaining, Is.EqualTo(0.5f).Within(0.001f));

            run.Tick(0.5f);
            Assert.That(run.Status, Is.EqualTo(TravelRunStatus.Active));
        }

        [Test]
        public void TimerExpiresAsFailedRun()
        {
            var run = new TravelRunState(2f, 0f, new GridCoordinate(0, 1, 0), 0, 0);

            run.Tick(2f);

            Assert.That(run.Status, Is.EqualTo(TravelRunStatus.Failed));
            Assert.That(run.FailureReason, Is.EqualTo(TravelFailureReason.TimeExpired));
        }

        [Test]
        public void ExitRequiresConfiguredObjectives()
        {
            var run = new TravelRunState(10f, 0f, new GridCoordinate(0, 1, 0), 1, 1);

            Assert.That(run.TryReachExit(new GridCoordinate(0, 1, 0)), Is.False);
            run.CollectCoin();
            run.CollectJewel();

            Assert.That(run.TryReachExit(new GridCoordinate(0, 1, 0)), Is.True);
            Assert.That(run.Status, Is.EqualTo(TravelRunStatus.Succeeded));
        }

        [Test]
        public void WrongCellCannotCompleteRun()
        {
            var run = new TravelRunState(10f, 0f, new GridCoordinate(0, 1, 0), 0, 0);

            Assert.That(run.TryReachExit(new GridCoordinate(1, 1, 0)), Is.False);
            Assert.That(run.Status, Is.EqualTo(TravelRunStatus.Active));
        }

        [Test]
        public void HazardFailureIsTerminal()
        {
            var run = new TravelRunState(10f, 0f, new GridCoordinate(0, 1, 0), 0, 0);

            run.Fail(TravelFailureReason.Hazard);
            run.Tick(5f);

            Assert.That(run.Status, Is.EqualTo(TravelRunStatus.Failed));
            Assert.That(run.FailureReason, Is.EqualTo(TravelFailureReason.Hazard));
        }

        [Test]
        public void RetryResetsTimerObjectivesAndFailure()
        {
            var run = new TravelRunState(10f, 0f, new GridCoordinate(0, 1, 0), 1, 1);
            run.CollectCoin();
            run.CollectJewel();
            run.Tick(4f);
            run.Fail(TravelFailureReason.Blocked);

            run.Retry();

            Assert.That(run.Status, Is.EqualTo(TravelRunStatus.Active));
            Assert.That(run.RemainingSeconds, Is.EqualTo(10f));
            Assert.That(run.Coins, Is.EqualTo(0));
            Assert.That(run.Jewels, Is.EqualTo(0));
            Assert.That(run.FailureReason, Is.EqualTo(TravelFailureReason.None));
        }

        [Test]
        public void ZeroTargetsAllowImmediateExit()
        {
            var run = new TravelRunState(10f, 0f, new GridCoordinate(0, 1, 0), 0, 0);

            Assert.That(run.TryReachExit(new GridCoordinate(0, 1, 0)), Is.True);
        }

        [Test]
        public void TimerDoesNotAdvanceDuringCountdown()
        {
            var run = new TravelRunState(10f, 3f, new GridCoordinate(0, 1, 0), 0, 0);

            run.Tick(2f);

            Assert.That(run.RemainingSeconds, Is.EqualTo(10f));
        }
    }
}
