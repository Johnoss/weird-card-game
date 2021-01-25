using Assets.Scripts.Features.MVC;

namespace Assets.Scripts.Features.Actor
{
    public class ActorController : AbstractController
    {
        private readonly ActorModel model;

        public ActorController(ActorModel model)
        {
            this.model = model;
        }
    }
}