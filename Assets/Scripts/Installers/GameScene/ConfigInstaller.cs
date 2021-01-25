using Assets.Scripts.Features.Actor.Config;
using Assets.Scripts.Features.Card.Config;
using Assets.Scripts.Features.Gauge.Config;
using Assets.Scripts.Features.Opponents.Config;
using Assets.Scripts.Features.SceneControl.Config;
using Assets.Scripts.Features.Score.Config;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers.GameScene
{
    [CreateAssetMenu(fileName = "ConfigInstaller", menuName = "Installers/ConfigInstaller")]
    public class ConfigInstaller : ScriptableObjectInstaller<ConfigInstaller>
    {
        [SerializeField] private GaugesConfig gaugesConfig;
        [SerializeField] private CommonStatsConfig commonStatsConfig;
        [SerializeField] private DeckConfig deckConfig;
        [SerializeField] private ScoreConfig scoreConfig;
        [SerializeField] private CompetitionConfig competitionConfig;
        [SerializeField] private ActorConfig actorConfig;
        [SerializeField] private SceneConfig sceneConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(gaugesConfig);
            Container.BindInstance(commonStatsConfig);
            Container.BindInstance(deckConfig);
            Container.BindInstance(scoreConfig);
            Container.BindInstance(competitionConfig);
            Container.BindInstance(actorConfig);
            Container.BindInstance(sceneConfig);
        }
    }
}