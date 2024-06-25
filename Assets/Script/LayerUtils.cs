using UnityEditor;
using UnityEngine;

namespace LearnGame
{
    public static class LayerUtils 
    {
        public const string BulletLayerName = "Bullet";
        public const string EnemyLayerName = "Emeny";

        public static readonly int BulletLayer = LayerMask.NameToLayer(BulletLayerName);
        public static readonly int EnemyMask = LayerMask.GetMask(EnemyLayerName);
        
        // return == =>
        public static bool IsBullet(GameObject other) => other.layer == BulletLayer;
    }
}