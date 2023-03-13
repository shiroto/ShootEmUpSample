using Unity.Entities;
using UnityEngine;

namespace Shooter
{
    public readonly unsafe partial struct DestructableAspect : IAspect
    {
        private readonly RefRW<DestructableData> destructable;

        public int Health
        {
            get => destructable.ValueRO.health;
            set => destructable.ValueRW.health = value;
        }
    }

    public struct DestructableData : IComponentData
    {
        public int health;
    }

    public class Destructable : MonoBehaviour
    {
        public int health;
    }

    public class DestructableBaker : Baker<Destructable>
    {
        public override void Bake(Destructable authoring)
        {
            AddComponent(new DestructableData
            {
                health = authoring.health,
            });
        }
    }
}