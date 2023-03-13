using Unity.Burst;
using Unity.Entities;

namespace Shooter
{
    [BurstCompile]
    public partial struct XActivationSystem : ISystem
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

            private void Execute(XActivationAspect activation)
            {
                if (activation.X <= 45)
                {
                    ecb.RemoveComponent<XActivationTag>(activation.entity);
                    ecb.RemoveComponent<HorizontalMovementData>(activation.entity);
                    ecb.AddComponent<ActiveTag>(activation.entity);
                }
            }
        }
    }
}