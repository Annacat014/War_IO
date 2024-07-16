using UnityEditor;
using UnityEngine;

namespace LearnGame
{
    public static class LayerUtils 
    {
        public const string BulletLayerName = "Bullet";
        public const string PlayerLaerName = "Player";
        public const string EnemyLayerName = "Enemy";
        public const string PickUpLayerName = "PickUp";

        public static readonly int BulletLayer = LayerMask.NameToLayer(BulletLayerName);
        public static readonly int PickUpLayer = LayerMask.NameToLayer(PickUpLayerName);

        public static readonly int CharactersMask = LayerMask.GetMask(EnemyLayerName, PlayerLaerName);
        public static readonly int PickUpMask = LayerMask.GetMask(PickUpLayerName);

        // return == =>
        public static bool IsBullet(GameObject other) => other.layer == BulletLayer;
        public static bool IsPickUp(GameObject other) => other.layer == PickUpLayer;
    }
}