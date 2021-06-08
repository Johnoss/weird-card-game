using Features.Actor.Config;
using Features.Audio.Config;
using Features.Card.Config;
using Features.Gauge.Config;
using Features.Opponents.Config;
using Features.SceneControl.Config;
using Features.Score.Config;
using UnityEngine;
using Zenject;

namespace Installers.Persistent
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
        [SerializeField] private AudioConfig audioConfig;
        [SerializeField] private GesturesConfig gesturesConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(gaugesConfig);
            Container.BindInstance(commonStatsConfig);
            Container.BindInstance(deckConfig);
            Container.BindInstance(scoreConfig);
            Container.BindInstance(competitionConfig);
            Container.BindInstance(actorConfig);
            Container.BindInstance(sceneConfig);
            Container.BindInstance(audioConfig);
            Container.BindInstance(gesturesConfig);
        }
    }
}