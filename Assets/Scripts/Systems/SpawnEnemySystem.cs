using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Shooter
{
    [BurstCompile]
    public partial struct SpawnEnemySystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            BeginInitializationEntityCommandBufferSystem.Singleton ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
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

            private void Execute(EnemySpawnerAspect spawner)
            {
                spawner.Timer -= deltaTime;
                if (!spawner.IsTimeToSpawn)
                {
                    return;
                }
                spawner.Timer = spawner.SpawnWaitTime;
                Entity entity = ecb.Instantiate(spawner.EnemyPrefab);
                ecb.SetComponent(entity, new LocalTransform { Position = spawner.Position, Scale = 1, Rotation = quaternion.identity });
                spawner.SpawnCount--;
                if (spawner.SpawnCount == 0)
                {
                    ecb.DestroyEntity(spawner.entity);
                }
            }
        }
    }
}