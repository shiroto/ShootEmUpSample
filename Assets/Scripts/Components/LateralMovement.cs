using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Shooter
{
    public readonly partial struct LateralMovementAspect : IAspect
    {
        public readonly Entity entity;
        private readonly RefRW<LateralMovementData> lateralMovement;
        private readonly RefRW<LocalTransform> transform;

        public float Amplitude => lateralMovement.ValueRO.amplitude;

        public float CurrentTime
        {
            get => lateralMovement.ValueRO.currentTime;
            set => lateralMovement.ValueRW.currentTime = value;
        }

        public float Frequency => lateralMovement.ValueRO.frequency;

        public float3 Position
        {
            get => transform.ValueRO.Position;
            set => transform.ValueRW.Position = value;
        }
    }

    public struct LateralMovementData : IComponentData
    {
        public float amplitude;
        public float currentTime;
        public float frequency;
    }

    public class LateralMovement : MonoBehaviour
    {
        public float amplitude;
        public float frequency;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + (transform.up * (amplitude / 2)));
        }
    }

    public class LateralMovementBaker : Baker<LateralMovement>
    {
        public override void Bake(LateralMovement authoring)
        {
            AddComponent(new LateralMovementData
            {
                frequency = authoring.frequency,
                amplitude = authoring.amplitude,
            });
        }
    }
}