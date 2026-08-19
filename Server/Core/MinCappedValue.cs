namespace Beekeeping.Server
{
    using Eco.Core.Controller;
    using Eco.Gameplay.DynamicValues;

    // Wraps an IDynamicValue and ensures its output never drops below a minimum.
    // Used to cap LayerModifiedValue so craft time bonuses don't exceed a configured limit.
    public class MinCappedValue : IDynamicValue, IController
    {
        int controllerID;
        public ref int ControllerID => ref this.controllerID;

        private readonly IDynamicValue inner;
        private readonly float min;

        public MinCappedValue(IDynamicValue inner, float min)
        {
            this.inner = inner;
            this.min = min;
        }

        public float GetBaseValue => System.Math.Max(this.min, this.inner.GetBaseValue);

        public float GetCurrentValue(IDynamicValueContext context, object obj)
            => System.Math.Max(this.min, this.inner.GetCurrentValue(context, obj));

        public int GetCurrentValueInt(IDynamicValueContext context, object obj, float multiplier = 1f)
            => (int)(this.GetCurrentValue(context, obj) * multiplier);
    }
}
