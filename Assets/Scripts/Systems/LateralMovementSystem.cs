using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace Shooter
{
    [BurstCompile]
    public partial struct LateralMovementSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            new Job
            {
                deltaTime = deltaTime,
            }.ScheduleParallel();
        }

        private partial struct Job : IJobEntity
        {
            public float deltaTime;

            private void Execute(LateralMovementAspect movement)
            {
                float3 pos = movement.Position;
                movement.CurrentTime += movement.Frequency * deltaTime;
                pos.y = movement.Amplitude * math.sin(movement.CurrentTime);
                movement.Position = pos;
            }
        }
    }
}