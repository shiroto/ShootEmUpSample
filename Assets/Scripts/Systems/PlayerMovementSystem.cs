using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace Shooter
{
    [BurstCompile]
    public partial struct PlayerMovementSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            new Job
            {
                deltaTime = deltaTime,
            }.Schedule();
        }

        private partial struct Job : IJobEntity
        {
            public float deltaTime;

            private static void ComputeHorizontal(PlayerMovementAspect movement, float frameAcceleration)
            {
                float3 velocity = movement.Velocity;
                if (movement.Right)
                {
                    velocity.x = math.clamp(velocity.x + frameAcceleration, -1, 1);
                }
                else if (movement.Left)
                {
                    velocity.x = math.clamp(velocity.x - frameAcceleration, -1, 1);
                }
                else
                {
                    velocity.x = math.lerp(velocity.x, 0, frameAcceleration);
                }
                movement.Velocity = velocity;
            }

            private static void ComputeVertical(PlayerMovementAspect movement, float frameAcceleration)
            {
                float3 velocity = movement.Velocity;
                if (movement.Up)
                {
                    velocity.y = math.clamp(velocity.y + frameAcceleration, -1, 1);
                }
                else if (movement.Down)
                {
                    velocity.y = math.clamp(velocity.y - frameAcceleration, -1, 1);
                }
                else
                {
                    velocity.y = math.lerp(velocity.y, 0, frameAcceleration);
                }
                movement.Velocity = velocity;
            }

            private void Execute(PlayerMovementAspect movement)
            {
                float frameAcceleration = movement.Acceleration * deltaTime;
                ComputeVertical(movement, frameAcceleration);
                ComputeHorizontal(movement, frameAcceleration);
                movement.transform.LocalPosition += movement.Speed * movement.Velocity * deltaTime;
            }
        }
    }
}