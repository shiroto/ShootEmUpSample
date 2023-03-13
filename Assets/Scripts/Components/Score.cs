using Unity.Entities;
using UnityEngine;

namespace Shooter
{
    public struct ScoreData : IComponentData
    {
        public int score;
    }

    public class Score : MonoBehaviour
    {
    }

    public class ScoreBaker : Baker<Score>
    {
        public override void Bake(Score authoring)
        {
            AddComponent(new ScoreData());
        }
    }
}