using Features.Score;
using Features.Score.Config;
using Features.Score.Factory;
using UnityEngine;
using Zenject;

namespace Installers.GameScene
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