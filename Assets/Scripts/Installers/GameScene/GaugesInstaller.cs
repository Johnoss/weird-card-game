using Assets.Scripts.Features.Gauge;
using Assets.Scripts.Features.Gauge.Config;
using Assets.Scripts.Features.Gauge.Factory;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers.GameScene
{
    public class GaugesInstaller : MonoInstaller
    {
        [SerializeField] private Transform gaugesParent;

        [Inject] private GaugesConfig gaugesConfig;

        public override void InstallBindings()
        {
            Container.Bind<GaugeFactory>().AsSingle();

            Container.BindFactory<GaugeModel, GaugesConfig.GaugeSetting, GaugeView, GaugeView.ViewFactory>()
                .FromComponentInNewPrefab(gaugesConfig.GaugePrefab)
                .UnderTransform(gaugesParent);
        }
    }
}