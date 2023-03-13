using Unity.Burst;
using Unity.Entities;

namespace Shooter
{
    [BurstCompile]
    public partial struct OutOfBoundsSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            EndSimulationEntityCommandBufferSystem.Singleton ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            new Job
            {
                ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged),
            }.Schedule();
        }

        private partial struct Job : IJobEntity
        {
            public EntityCommandBuffer ecb;

            private void Execute(OutOfBoundsAspect ob)
            {
                if (ob.X <= -50 || ob.X >= 50)
                {
                    ecb.DestroyEntity(ob.entity);
                }
            }
        }
    }
}