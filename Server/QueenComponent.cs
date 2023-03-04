using Eco.Gameplay.Components;
using Eco.Gameplay.Objects;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;
using System;
using System.Linq;

namespace Beekeeping.Server
{
    [Serialized]
    public class QueenComponent : WorldObjectComponent
    {
        private StatusElement? status;

        public override void Initialize()
        {
            status = Parent.GetComponent<StatusComponent>(null).CreateStatusElement(-30);
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            status?.SetStatusMessage(true, Localizer.DoStr("Right click to retrieve the queen bee in this Swarm."));
        }
    }
}
