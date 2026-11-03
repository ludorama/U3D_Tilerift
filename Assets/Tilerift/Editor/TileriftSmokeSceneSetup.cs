using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using Tilerift.Gameplay.Configs;
using Tilerift.Gameplay.Travel;
using Tilerift.Input;
using Tilerift.Presentation.Camera;

namespace Tilerift.Editor
{
    public static class TileriftSmokeSceneSetup
    {
        private const string ScenePath = "Assets/Scenes/Sandbox.unity";
        private const string InputActionsPath = "Assets/Tilerift/Code/Input/TileriftInputActions.inputactions";
        private const string CameraConfigPath = "Assets/Tilerift/Code/Gameplay/Configs/DefaultCameraConfig.asset";
        private const string TravelLevelPath = "Assets/Tilerift/Code/Gameplay/Travel/TravelLevel01.asset";

        [MenuItem("Tilerift/Setup Phase 1 Smoke Scene")]
        public static void Setup()
        {
            var scene = EditorSceneManager.GetActiveScene();
            if (scene.path != ScenePath)
                scene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);

            var inputActions = AssetDatabase.LoadAssetAtPath<InputActionAsset>(InputActionsPath);
            if (inputActions == null)
            {
                Debug.LogError($"Could not load input actions at {InputActionsPath}.");
                return;
            }

            var camera = GameObject.FindWithTag("MainCamera");
            if (camera == null)
            {
                camera = new GameObject("Main Camera");
                camera.tag = "MainCamera";
                camera.AddComponent<Camera>();
            }

            var routerObject = GameObject.Find("Tilerift Input Router") ?? new GameObject("Tilerift Input Router");
            var router = routerObject.GetComponent<TileriftInputRouter>() ?? routerObject.AddComponent<TileriftInputRouter>();
            SetProperty(router, "actions", inputActions);

            var hero = GameObject.Find("Smoke Hero");
            if (hero == null)
            {
                hero = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                hero.name = "Smoke Hero";
                hero.transform.position = Vector3.zero;
                hero.transform.localScale = new Vector3(0.7f, 1f, 0.7f);
            }

            var board = GameObject.Find("Smoke Board") ?? new GameObject("Smoke Board");
            for (var x = -2; x <= 2; x++)
            for (var z = -2; z <= 2; z++)
            {
                var tileName = $"Tile_{x}_{z}";
                if (board.transform.Find(tileName) != null) continue;
                var tile = GameObject.CreatePrimitive(PrimitiveType.Cube);
                tile.name = tileName;
                tile.transform.SetParent(board.transform);
                tile.transform.position = new Vector3(x, -0.6f, z);
                tile.transform.localScale = new Vector3(0.95f, 0.15f, 0.95f);
            }

            var config = AssetDatabase.LoadAssetAtPath<CameraConfig>(CameraConfigPath);
            if (config == null)
            {
                config = ScriptableObject.CreateInstance<CameraConfig>();
                config.panSpeed = 0.02f;
                config.dragDeadzonePixels = 12f;
                config.doubleTapIntervalSeconds = 0.3f;
                config.recenterEasing = 8f;
                AssetDatabase.CreateAsset(config, CameraConfigPath);
                AssetDatabase.SaveAssets();
            }

            var controller = camera.GetComponent<TileriftCameraController>() ?? camera.AddComponent<TileriftCameraController>();
            SetProperty(controller, "config", config);
            SetProperty(controller, "inputRouter", router);
            SetProperty(controller, "heroTarget", hero.transform);
            SetProperty(controller, "targetCamera", camera.GetComponent<Camera>());

            var travelLevel = AssetDatabase.LoadAssetAtPath<TravelLevelConfig>(TravelLevelPath);
            if (travelLevel == null)
            {
                travelLevel = ScriptableObject.CreateInstance<TravelLevelConfig>();
                travelLevel.levelId = "travel_sandbox_01";
                travelLevel.timeLimitSeconds = 30f;
                travelLevel.startingDelaySeconds = 3f;
                travelLevel.cellTravelSeconds = 1f;
                travelLevel.targetCoins = 1;
                travelLevel.targetJewels = 1;
                travelLevel.coinCells.Add(new TravelGridCell { x = 0, y = 1 });
                travelLevel.jewelCells.Add(new TravelGridCell { x = 0, y = 2 });
                for (var y = 0; y <= 2; y++)
                    travelLevel.walkableCells.Add(new TravelGridCell { x = 0, y = y });
                AssetDatabase.CreateAsset(travelLevel, TravelLevelPath);
                AssetDatabase.SaveAssets();
            }

            var travelObject = GameObject.Find("Travel Run") ?? new GameObject("Travel Run");
            var travelController = travelObject.GetComponent<TravelRunController>() ?? travelObject.AddComponent<TravelRunController>();
            SetProperty(travelController, "level", travelLevel);
            SetProperty(travelController, "inputRouter", router);
            SetProperty(travelController, "heroVisual", hero.transform);

            var cameraComponent = camera.GetComponent<Camera>();
            cameraComponent.transform.position = new Vector3(0f, 7f, -7f);
            cameraComponent.transform.rotation = Quaternion.Euler(42f, 0f, 0f);
            cameraComponent.orthographic = true;
            cameraComponent.orthographicSize = 5.5f;

            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene);
            AssetDatabase.SaveAssets();
            Debug.Log("Tilerift Phase 3 Travel slice scene setup complete.");
        }

        [MenuItem("Tilerift/Setup Phase 3 Travel Slice")]
        public static void SetupPhase3TravelSlice() => Setup();

        private static void SetProperty(Object target, string propertyName, Object value)
        {
            var serialized = new SerializedObject(target);
            serialized.FindProperty(propertyName).objectReferenceValue = value;
            serialized.ApplyModifiedPropertiesWithoutUndo();
        }
    }
}
