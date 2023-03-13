using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace Shooter
{
    [BurstCompile]
    public partial struct HorizontalMovementSystem : ISystem
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

            private void Execute(HorizontalMovementAspect enemy)
            {
                float3 pos = enemy.Position;
                pos.x -= enemy.Speed * deltaTime;
                enemy.Position = pos;
            }
        }
    }
}