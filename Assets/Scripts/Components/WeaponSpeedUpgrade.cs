using Unity.Entities;
using UnityEngine;

namespace Shooter
{
    public struct WeaponSpeedUpgradeTag : IComponentData
    {
    }

    public class WeaponSpeedUpgrade : MonoBehaviour
    {
    }

    public class WeaponSpeedUpgradeBaker : Baker<WeaponSpeedUpgrade>
    {
        public override void Bake(WeaponSpeedUpgrade authoring)
        {
            AddComponent(new WeaponSpeedUpgradeTag());
        }
    }
}