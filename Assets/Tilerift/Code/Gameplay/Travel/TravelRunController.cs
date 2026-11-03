using System.Collections.Generic;
using Tilerift.Core.Grid;
using Tilerift.Core.Simulation;
using Tilerift.Input;
using UnityEngine;

namespace Tilerift.Gameplay.Travel
{
    public sealed class TravelRunController : MonoBehaviour
    {
        [SerializeField] private TravelLevelConfig level;
        [SerializeField] private TileriftInputRouter inputRouter;
        [SerializeField] private Transform heroVisual;
        [SerializeField] private float visualCellSize = 1f;

        private TravelRunState run;
        private HeroMovementSimulation movement;
        private readonly HashSet<GridCoordinate> collectedCoins = new HashSet<GridCoordinate>();
        private readonly HashSet<GridCoordinate> collectedJewels = new HashSet<GridCoordinate>();

        public TravelRunState Run => run;

        private void Awake()
        {
            if (level == null)
            {
                Debug.LogError("TravelRunController requires a TravelLevelConfig.", this);
                enabled = false;
                return;
            }

            CreateRun();
            if (inputRouter != null)
            {
                inputRouter.Navigated += OnNavigate;
                inputRouter.Submitted += OnRetryInput;
                inputRouter.Pressed += OnRetryInput;
            }
        }

        private void OnDestroy()
        {
            if (inputRouter != null)
            {
                inputRouter.Navigated -= OnNavigate;
                inputRouter.Submitted -= OnRetryInput;
                inputRouter.Pressed -= OnRetryInput;
            }
        }

        private void Update()
        {
            if (run == null) return;
            run.Tick(Time.deltaTime);
            if (run.Status != TravelRunStatus.Active) return;

            movement.Tick(Time.deltaTime);
            SyncHeroVisual();
        }

        public void Retry()
        {
            CreateRun();
        }

        public void CollectCoin()
        {
            if (run.Status == TravelRunStatus.Active) run.CollectCoin();
        }

        public void CollectJewel()
        {
            if (run.Status == TravelRunStatus.Active) run.CollectJewel();
        }

        public void FailFromHazard() => run?.Fail(TravelFailureReason.Hazard);

        private void CreateRun()
        {
            collectedCoins.Clear();
            collectedJewels.Clear();
            run = new TravelRunState(level.timeLimitSeconds, level.startingDelaySeconds,
                level.ExitCoordinate, level.targetCoins, level.targetJewels);
            movement = new HeroMovementSimulation(level.StartCoordinate, level.startingFacing, 0f, level.cellTravelSeconds, 0f,
                level.IsWalkable);
            movement.EnteredCell += OnEnteredCell;
            movement.Blocked += () => run.Fail(TravelFailureReason.Blocked);
            SyncHeroVisual();
        }

        private void OnEnteredCell(GridCoordinate coordinate)
        {
            if (level.IsHazard(coordinate))
            {
                run.Fail(TravelFailureReason.Hazard);
                return;
            }

            if (level.HasCoin(coordinate) && collectedCoins.Add(coordinate)) run.CollectCoin();
            if (level.HasJewel(coordinate) && collectedJewels.Add(coordinate)) run.CollectJewel();
            run.TryReachExit(coordinate);
        }

        private void OnNavigate(Vector2 input)
        {
            if (run == null || run.Status != TravelRunStatus.Active || input.sqrMagnitude < 0.25f) return;
            var target = input.x switch
            {
                > 0.5f => CardinalDirection.East,
                < -0.5f => CardinalDirection.West,
                _ => input.y >= 0f ? CardinalDirection.North : CardinalDirection.South
            };
            var difference = ((int)target - (int)movement.Facing + 4) % 4;
            if (difference == 1) movement.TurnRight();
            else if (difference == 2) movement.TurnAround();
            else if (difference == 3) movement.TurnLeft();
        }

        private void OnRetryInput()
        {
            if (run.Status is TravelRunStatus.Succeeded or TravelRunStatus.Failed)
                Retry();
        }

        private void SyncHeroVisual()
        {
            if (heroVisual == null || movement == null) return;
            heroVisual.position = new Vector3(movement.Position.X * visualCellSize, heroVisual.position.y,
                movement.Position.Y * visualCellSize);
        }

        private void OnGUI()
        {
            if (run == null) return;
            var status = run.Status == TravelRunStatus.Failed
                ? $"Failed: {run.FailureReason}"
                : run.Status.ToString();
            GUI.Label(new Rect(16f, 16f, 420f, 24f),
                $"Travel  {status}  Time {run.RemainingSeconds:0.0}  Coins {run.Coins}  Jewels {run.Jewels}");
            if (run.Status == TravelRunStatus.Countdown)
                GUI.Label(new Rect(16f, 42f, 420f, 24f), $"Starting in {run.CountdownRemaining:0.0}");
            if (run.Status is TravelRunStatus.Succeeded or TravelRunStatus.Failed)
            {
                GUI.Label(new Rect(16f, 68f, 420f, 24f), "Run complete. Press Submit or tap Retry.");
                if (GUI.Button(new Rect(16f, 94f, 120f, 32f), "Retry")) Retry();
            }
        }
    }
}
