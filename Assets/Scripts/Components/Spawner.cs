using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Shooter
{
    public readonly partial struct EnemySpawnerAspect : IAspect
    {
        public readonly Entity entity;
        private readonly RefRO<ActiveTag> active;
        private readonly RefRW<EnemySpawnerData> spawner;
        private readonly RefRO<WorldTransform> transform;

        public Entity EnemyPrefab => spawner.ValueRO.spawnedEnemy;

        public bool IsTimeToSpawn => Timer <= 0;

        public float3 Position => transform.ValueRO.Position;

        public int SpawnCount
        {
            get => spawner.ValueRO.count;
            set => spawner.ValueRW.count = value;
        }

        public float SpawnWaitTime => spawner.ValueRO.waitTime;

        public float Timer
        {
            get => spawner.ValueRO.currentTime;
            set => spawner.ValueRW.currentTime = value;
        }
    }

    public struct EnemySpawnerData : IComponentData
    {
        public int count;
        public float currentTime;
        public Entity spawnedEnemy;
        public float waitTime;
    }

    public class Spawner : MonoBehaviour
    {
        public int count;
        public GameObject spawnedEnemy;
        public float waitTime;
    }

    public class EnemySpawnerBaker : Baker<Spawner>
    {
        public override void Bake(Spawner authoring)
        {
            AddComponent(new EnemySpawnerData
            {
                spawnedEnemy = GetEntity(authoring.spawnedEnemy),
                count = authoring.count,
                waitTime = authoring.waitTime,
            });
            AddComponent(new XActivationTag());
        }
    }
}