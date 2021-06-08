using System.Collections;
using Features.Card.Factory;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.Initializers
{
    [UsedImplicitly]
    public class EditSceneInitializer : IInitializable
    {
        [Inject] private CardFactory cardFactory;

        [Inject]
        public void Initialize()
        {
            Observable.FromCoroutine(StartInitialize).Subscribe();
        }

        private IEnumerator StartInitialize()
        {

            cardFactory.CreateCards();
            yield return new WaitForEndOfFrame();

            Debug.Log("Edit Initialized");
        }
    }
}