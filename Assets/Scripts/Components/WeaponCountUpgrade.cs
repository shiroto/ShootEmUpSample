using Unity.Entities;
using UnityEngine;

namespace Shooter
{
    public struct WeaponCountUpgradeTag : IComponentData
    {
    }

    public class WeaponCountUpgrade : MonoBehaviour
    {
    }

    public class WeaponCountUpgradeBaker : Baker<WeaponCountUpgrade>
    {
        public override void Bake(WeaponCountUpgrade authoring)
        {
            AddComponent(new WeaponCountUpgradeTag());
        }
    }
}