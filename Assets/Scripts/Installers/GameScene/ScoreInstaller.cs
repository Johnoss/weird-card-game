using Assets.Scripts.Features.Score;
using Assets.Scripts.Features.Score.Config;
using Assets.Scripts.Features.Score.Factory;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers.GameScene
{
    public class ScoreInstaller : MonoInstaller
    {
        [SerializeField] private Transform scoreViewsParent;

        [Inject] private ScoreConfig scoreConfig;

        public override void InstallBindings()
        {
            Container.Bind<ScoreFactory>().AsSingle();

            Container.BindFactory<IScoreModel, ScoreView, ScoreView.ViewFactory>()
                .FromComponentInNewPrefab(scoreConfig.ScorePrefab)
                .UnderTransform(scoreViewsParent);
        }
    }
}