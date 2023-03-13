using Unity.Entities;
using Unity.Transforms;

namespace Shooter
{
    public struct ActiveTag : IComponentData
    {
    }

    public readonly partial struct XActivationAspect : IAspect
    {
        public readonly Entity entity;
        private readonly RefRO<XActivationTag> activationTag;
        private readonly RefRO<HorizontalMovementData> movement;
        private readonly RefRO<WorldTransform> transform;

        public float X => transform.ValueRO.Position.x;
    }

    public struct XActivationTag : IComponentData
    {
    }
}