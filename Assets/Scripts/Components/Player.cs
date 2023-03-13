using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Shooter
{
    public struct PlayerControls : IComponentData
    {
        public bool down;
        public bool fire;
        public bool left;
        public bool right;
        public bool up;
    }

    public readonly partial struct PlayerFireAspect : IAspect
    {
        private readonly RefRO<PlayerControls> controls;
        private readonly RefRO<WorldTransform> transform;
        private readonly RefRW<WeaponData> weapon;

        public Entity BulletPrefab => weapon.ValueRO.bullet;

        public float BulletSpacing => weapon.ValueRO.bulletSpacing;

        public byte BulletsPerShot => weapon.ValueRO.bulletsPerShot;

        public float CooldownRate => weapon.ValueRO.cooldownRate;

        public float CurrentCooldown
        {
            get => weapon.ValueRO.currentCooldown;
            set => weapon.ValueRW.currentCooldown = value;
        }

        public bool Fire => controls.ValueRO.fire;

        public float3 MuzzleOffset => weapon.ValueRO.muzzle;

        public float3 PlayerPosition => transform.ValueRO.Position;
    }

    public readonly partial struct PlayerMovementAspect : IAspect
    {
        public readonly TransformAspect transform;
        private readonly RefRO<PlayerControls> controls;
        private readonly RefRW<PlayerMovementData> movement;

        public float Acceleration => movement.ValueRO.acceleration;

        public bool Down => controls.ValueRO.down;

        public bool Left => controls.ValueRO.left;

        public bool Right => controls.ValueRO.right;

        public float Speed => movement.ValueRO.speed;

        public bool Up => controls.ValueRO.up;

        public float3 Velocity
        {
            get => movement.ValueRO.velocity;
            set => movement.ValueRW.velocity = value;
        }
    }

    public struct PlayerMovementData : IComponentData
    {
        public float acceleration;
        public float speed;
        public float3 velocity;
    }

    public struct PlayerTag : IComponentData
    {
    }

    public struct WeaponData : IComponentData
    {
        public Entity bullet;
        public float bulletSpacing;
        public byte bulletsPerShot;
        public float cooldownRate;
        public float currentCooldown;
        public float3 muzzle;
    }

    public class Player : MonoBehaviour
    {
        #region Movement

        [Header("Movement")]
        public float acceleration;

        public float speed;

        #endregion Movement

        #region Weapon

        [Header("Weapon")]
        public GameObject bullet;

        public float bulletSpacing;
        public byte bulletsPerShot;
        public float cooldownRate;
        public Vector3 muzzle;

        #endregion Weapon

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + muzzle, 0.1f);
        }
    }

    public class PlayerBaker : Baker<Player>
    {
        public override void Bake(Player authoring)
        {
            AddComponent(new PlayerTag());
            AddComponent(new PlayerControls());
            AddComponent(new WeaponData
            {
                bullet = GetEntity(authoring.bullet),
                bulletsPerShot = authoring.bulletsPerShot,
                cooldownRate = authoring.cooldownRate,
                muzzle = authoring.muzzle,
                bulletSpacing = authoring.bulletSpacing,
            });
            AddComponent(new PlayerMovementData
            {
                acceleration = authoring.acceleration,
                speed = authoring.speed,
            });
        }
    }
}