using UnityEngine;

[CreateAssetMenu(fileName = "Contact", menuName = "Items/Contact")]
public class ContactBlueprint : ScriptableObject
{
    public Sprite spr;

    public float baseExplosionRadius;
    public int baseDamage;
    public float baseSlowPct;
    public float baseSlowDur;
    public GameObject impactEffect;
}
