using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Shooter
{
    public readonly partial struct HorizontalMovementAspect : IAspect
    {
        public readonly Entity entity;
        private readonly RefRO<HorizontalMovementData> horizontalMovement;
        private readonly RefRW<LocalTransform> transform;

        public float3 Position
        {
            get => transform.ValueRO.Position;
            set => transform.ValueRW.Position = value;
        }

        public float Speed => horizontalMovement.ValueRO.speed;
    }

    public struct HorizontalMovementData : IComponentData
    {
        public float speed;
    }

    public class HorizontalMovement : MonoBehaviour
    {
        public float speed;
    }

    public class HorizontalMovementBaker : Baker<HorizontalMovement>
    {
        public override void Bake(HorizontalMovement authoring)
        {
            AddComponent(new HorizontalMovementData
            {
                speed = authoring.speed,
            });
        }
    }
}