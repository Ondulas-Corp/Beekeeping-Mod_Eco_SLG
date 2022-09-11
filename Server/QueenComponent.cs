using Eco.Gameplay.Objects;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;

namespace Eco.Gameplay.Components
{
    [Serialized]
    public class QueenComponent : WorldObjectComponent
    {
        private StatusElement status;

        public override void Initialize()
        {
            this.status = this.Parent.GetComponent<StatusComponent>((string)null).CreateStatusElement(-30);
            this.UpdateStatus();
        }

        private void UpdateStatus()
        {
            this.status?.SetStatusMessage(true, Localizer.DoStr("Right click to retrieve the queen bee in this Swarm."));
        }
    }
}
