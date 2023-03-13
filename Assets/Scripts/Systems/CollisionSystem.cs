using Unity.Burst;
using Unity.Entities;
using Unity.Physics;

namespace Shooter
{
    [BurstCompile]
    public partial struct CollisionSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            state.Dependency.Complete();
            foreach (TriggerEvent e in SystemAPI.GetSingleton<SimulationSingleton>().AsSimulation().TriggerEvents)
            {
                TryApplyDamage(state, e.EntityA);
                TryApplyDamage(state, e.EntityB);
                TryApplyWeaponCountUpgrade(state, e.EntityA, e.EntityB);
                TryApplyWeaponSpeedUpgrade(state, e.EntityA, e.EntityB);
            }
        }

        private static void IncreaseScore(SystemState state)
        {
            Entity scoreEntity = state.EntityManager.CreateEntityQuery(typeof(ScoreData)).GetSingletonEntity();
            ScoreData scoreData = state.EntityManager.GetComponentData<ScoreData>(scoreEntity);
            scoreData.score++;
            state.EntityManager.SetComponentData(scoreEntity, scoreData);
        }

        private static void TryApplyDamage(SystemState state, Entity entity)
        {
            if (!state.EntityManager.HasComponent<DestructableData>(entity))
            {
                return;
            }
            DestructableData data = state.EntityManager.GetComponentData<DestructableData>(entity);
            data.health--;
            if (data.health == 0)
            {
                if (state.EntityManager.HasComponent<EnemyTag>(entity))
                {
                    IncreaseScore(state);
                }
                state.EntityManager.DestroyEntity(entity);
            }
            else
            {
                state.EntityManager.SetComponentData(entity, data);
            }
        }

        private static void TryApplyWeaponCountUpgrade(SystemState state, Entity entityA, Entity entityB)
        {
            Entity player;
            Entity pickup;
            if (state.EntityManager.HasComponent<PlayerTag>(entityA) && state.EntityManager.HasComponent<WeaponCountUpgradeTag>(entityB))
            {
                player = entityA;
                pickup = entityB;
            }
            else if (state.EntityManager.HasComponent<PlayerTag>(entityB) && state.EntityManager.HasComponent<WeaponCountUpgradeTag>(entityA))
            {
                player = entityB;
                pickup = entityA;
            }
            else
            {
                return;
            }

            WeaponData data = state.EntityManager.GetComponentData<WeaponData>(player);
            data.bulletsPerShot += 1;
            state.EntityManager.SetComponentData(player, data);
            state.EntityManager.DestroyEntity(pickup);
        }

        private static void TryApplyWeaponSpeedUpgrade(SystemState state, Entity entityA, Entity entityB)
        {
            Entity player;
            Entity pickup;
            if (state.EntityManager.HasComponent<PlayerTag>(entityA) && state.EntityManager.HasComponent<WeaponSpeedUpgradeTag>(entityB))
            {
                player = entityA;
                pickup = entityB;
            }
            else if (state.EntityManager.HasComponent<PlayerTag>(entityB) && state.EntityManager.HasComponent<WeaponSpeedUpgradeTag>(entityA))
            {
                player = entityB;
                pickup = entityA;
            }
            else
            {
                return;
            }

            WeaponData data = state.EntityManager.GetComponentData<WeaponData>(player);
            data.cooldownRate += 2;
            state.EntityManager.SetComponentData(player, data);
            state.EntityManager.DestroyEntity(pickup);
        }
    }
}