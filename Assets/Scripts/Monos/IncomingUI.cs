using System.Collections;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Shooter
{
    public class IncomingUI : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public float popupTime;
        private EntityManager entityManager;

        private IEnumerator GoBlinkyBlinky()
        {
            float time = 0;
            while (time < popupTime)
            {
                time += Time.deltaTime;
                canvasGroup.alpha = 1 - (0.5f * math.cos(time * popupTime) + 0.5f);
                yield return null;
            }
            canvasGroup.alpha = 0;
        }

        private void Start()
        {
            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            canvasGroup.alpha = 0;
        }

        private void Update()
        {
            using NativeArray<Entity> array = entityManager.CreateEntityQuery(typeof(BossPopupTag)).ToEntityArray(Allocator.Temp);
            if (array.Any())
            {
                entityManager.DestroyEntity(array[0]);
                StartCoroutine(GoBlinkyBlinky());
            }
        }
    }
}