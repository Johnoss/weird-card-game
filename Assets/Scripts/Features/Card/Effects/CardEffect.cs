using UnityEngine;

namespace Features.Card.Effects
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