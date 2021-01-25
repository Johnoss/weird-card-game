namespace Assets.Scripts.Features.MVC
{
    public class MCBundle<TModel, TController>
        where TModel : AbstractModel
        where TController : AbstractController
    {
        private readonly TModel model;
        public TModel Model => model;

        private readonly TController controller;
        public TController Controller => controller;

        public MCBundle(TModel model, TController controller)
        {
            this.model = model;
            this.controller = controller;
        }
    }
}