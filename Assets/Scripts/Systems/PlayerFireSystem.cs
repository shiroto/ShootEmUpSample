using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Shooter
{
    [BurstCompile]
    public partial struct PlayerFireSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            EndSimulationEntityCommandBufferSystem.Singleton ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            new Job
            {
                deltaTime = deltaTime,
                ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged),
            }.Schedule();
        }

        private partial struct Job : IJobEntity
        {
            public float deltaTime;
            public EntityCommandBuffer ecb;

            private void Execute(PlayerFireAspect fire)
            {
                fire.CurrentCooldown = math.clamp(fire.CurrentCooldown - fire.CooldownRate * deltaTime, 0, 1);
                if (fire.Fire && fire.CurrentCooldown == 0)
                {
                    fire.CurrentCooldown = 1;
                    float3 spawnPos = fire.PlayerPosition + fire.MuzzleOffset;
                    spawnPos.y -= ((fire.BulletsPerShot - 1) * fire.BulletSpacing) / 2;
                    for (int i = 0; i < fire.BulletsPerShot; i++)
                    {
                        Entity entity = ecb.Instantiate(fire.BulletPrefab);
                        ecb.SetComponent(entity, new LocalTransform { Position = spawnPos, Scale = 1, Rotation = quaternion.identity });
                        spawnPos.y += fire.BulletSpacing;
                    }
                }
            }
        }
    }
}