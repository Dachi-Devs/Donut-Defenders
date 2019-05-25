using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Items/Bullet")]
public class BulletBlueprint : ScriptableObject
{
    public Sprite spr;

    public float speed;
    public float baseExplosionRadius;
    public int baseDamage;
    public float baseSlowPct;
    public float baseSlowDur;
    public GameObject impactEffect;
}
