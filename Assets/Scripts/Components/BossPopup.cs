using Unity.Entities;
using UnityEngine;

namespace Shooter
{
    public struct BossPopupTag : IComponentData
    {
    }

    public class BossPopup : MonoBehaviour
    {
    }

    public class BossPopupBaker : Baker<BossPopup>
    {
        public override void Bake(BossPopup authoring)
        {
            AddComponent(new BossPopupTag());
        }
    }
}