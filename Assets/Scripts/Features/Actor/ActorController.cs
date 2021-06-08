using Features.MVC;

namespace Features.Actor
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