using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Shooter
{
    public struct DestroyWhenOutOfBoundsTag : IComponentData
    {
    }

    public readonly partial struct OutOfBoundsAspect : IAspect
    {
        public readonly Entity entity;
        private readonly RefRO<DestroyWhenOutOfBoundsTag> tag;
        private readonly RefRO<LocalTransform> transform;

        public float X => transform.ValueRO.Position.x;
    }

    public class OutOfBounds : MonoBehaviour
    {
    }

    public class OutOfBoundsBaker : Baker<OutOfBounds>
    {
        public override void Bake(OutOfBounds authoring)
        {
            AddComponent(new DestroyWhenOutOfBoundsTag());
        }
    }
}