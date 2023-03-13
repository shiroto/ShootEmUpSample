using Unity.Entities;
using UnityEngine;

namespace Shooter
{
    public class ScoreUI : MonoBehaviour
    {
        public TMPro.TMP_Text text;
        private EntityManager entityManager;
        private Entity scoreEntity;

        private void Start()
        {
            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            scoreEntity = entityManager.CreateEntityQuery(typeof(ScoreData)).GetSingletonEntity();
        }

        private void Update()
        {
            ScoreData data = entityManager.GetComponentData<ScoreData>(scoreEntity);
            text.text = data.score + "";
        }
    }
}