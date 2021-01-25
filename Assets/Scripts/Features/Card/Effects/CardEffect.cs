using UnityEngine;

namespace Assets.Scripts.Features.Card.Effects
{
    public enum CardEffect
    {
        None,
        [InspectorName("-")]
        LowerLow,
        [InspectorName("---")]
        LowerHigh,
        [InspectorName("+")]
        RaiseLow,
        [InspectorName("+++")]
        RaiseHigh,
    }
}