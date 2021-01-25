using System.Collections.Generic;
using Assets.Scripts.Features.MVC;
using UniRx;

namespace Assets.Scripts.Features.Card.Hand
{
    public class HandModel : AbstractModel
    {
        private readonly IReactiveCollection<MCBundle<CardModel, CardController>> handCardBundles;
        public IReactiveCollection<MCBundle<CardModel, CardController>> HandCardBundles => handCardBundles;

        public HandModel(List<MCBundle<CardModel, CardController>> handCardBundles)
        {
            this.handCardBundles = handCardBundles.ToReactiveCollection();
        }

        public void SetCard(int slotIndex, CardData cardData)
        {
            handCardBundles[slotIndex].Controller.RepopulateCard(cardData);
        }
    }
}