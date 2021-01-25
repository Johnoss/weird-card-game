using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Utilities.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Scripts.Features.Opponents.Config
{
    [CreateAssetMenu(fileName = "CompetitionConfig", menuName = "Config/CompetitionConfig")]
    public class CompetitionConfig : ScriptableObject
    {
        [Serializable]
        public class OpponentGroupSetting
        {
            [SerializeField] private string groupName;
            [RangedInt(0, 5, RangedIntAttribute.RangeDisplayType.EditableRanges)]
            public RangedInt AmountRange;
            public List<OpponentConfig> OpponentsPool;
        }

        [Header("Opponents")]
        [SerializeField] private int maxOpponents;
        [SerializeField] private List<OpponentGroupSetting> opponentGroups;

        public List<OpponentConfig> GetOpponents()
        {
            var opponents = new List<OpponentConfig>();
            foreach (var group in opponentGroups)
            {
                var opponentAmount = group.AmountRange.GetRandomValue();
                var opponentsAmount = Mathf.Min(opponentAmount, group.OpponentsPool.Count);
                var shuffledPool = group.OpponentsPool.ToList();
                shuffledPool.Shuffle();

                for (var i = 0; i < opponentsAmount; i++)
                {
                    if (opponents.Count >= maxOpponents)
                    {
                        return opponents;
                    }
                    opponents.Add(shuffledPool[i]);
                }
            }

            return opponents;
        }
    }
}