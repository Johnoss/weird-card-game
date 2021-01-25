using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Features.SceneControl.Config
{
    [CreateAssetMenu(fileName = "SceneConfig", menuName = "Config/SceneConfig")]
    public class SceneConfig : ScriptableObject
    {
        [Serializable]
        public class SceneIndexSetting
        {
            public Scene Scene;
            public int BuildSettingIndex;
        }

        [Header("Scenes")]
        [SerializeField] private List<SceneIndexSetting> sceneIndexSettings;

        public int GetBuildIndexForScene(Scene scene)
        {
            return sceneIndexSettings.First(setting => setting.Scene == scene).BuildSettingIndex;
        }
    }
}