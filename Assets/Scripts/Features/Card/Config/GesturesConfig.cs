using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Features.Card.Config
{
    [CreateAssetMenu(fileName = "GesturesConfig", menuName = "Config/GesturesConfig")]
    public class GesturesConfig : ScriptableObject
    {
        [Serializable]
        private class SwipeThresholdSetting
        {
            public SwipeDirection Direction;
            public Vector2 ThresholdVector;
        }

        [Header("SwipeThresholds")]
        [SerializeField] private List<SwipeThresholdSetting> swipeThresholdSettings;

        public SwipeDirection GetSwipeDirection(Vector2 totalVector)
        {
            var validMagnitudeDirections = swipeThresholdSettings
                .Where(setting => setting.ThresholdVector.magnitude <= totalVector.magnitude)
                    .ToArray();
            
            return !validMagnitudeDirections.Any()
                ? SwipeDirection.None
                : GetClosestDirection(totalVector, validMagnitudeDirections);
        }

        private static SwipeDirection GetClosestDirection(Vector2 totalVector, IEnumerable<SwipeThresholdSetting> validMagnitudeDirections)
        {
            var orderedSettings = validMagnitudeDirections
                .OrderByDescending(setting => Vector2.Distance(totalVector, setting.ThresholdVector));
            return orderedSettings.First().Direction;
        }
    }
}