using Unity.Entities;
using UnityEngine;

namespace Shooter
{
    public class PlayerControlsMono : MonoBehaviour
    {
        public bool autoFire;
        private EntityManager entityManager;
        private Entity playerEntity;

        private void Start()
        {
            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            playerEntity = entityManager.CreateEntityQuery(typeof(PlayerTag)).GetSingletonEntity();
        }

        private void Update()
        {
            PlayerControls controls = entityManager.GetComponentData<PlayerControls>(playerEntity);
            controls.up = Input.GetKey(KeyCode.UpArrow);
            controls.down = Input.GetKey(KeyCode.DownArrow);
            controls.left = Input.GetKey(KeyCode.LeftArrow);
            controls.right = Input.GetKey(KeyCode.RightArrow);
            controls.fire = autoFire || Input.GetKey(KeyCode.Space);
            entityManager.SetComponentData(playerEntity, controls);
        }
    }
}