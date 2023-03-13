using Unity.Entities;
using UnityEngine;

namespace Shooter
{
    public struct EnemyTag : IComponentData
    {
    }

    public class Enemy : MonoBehaviour
    {
    }

    public class EnemyBaker : Baker<Enemy>
    {
        public override void Bake(Enemy authoring)
        {
            AddComponent(new EnemyTag());
        }
    }
}