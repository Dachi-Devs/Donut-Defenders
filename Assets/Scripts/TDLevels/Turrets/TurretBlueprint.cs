using UnityEngine;

[CreateAssetMenu(fileName = "Turret", menuName = "Items/Turret")]
public class TurretBlueprint : ScriptableObject
{
    public Sprite spr;
    public string type;

    public float range;
    public float fireRate;
    public int damageMod;
    public int damageOverTimeMod;
    public float explosionRadiusMod;
    public float slowPctMod;
    public float slowDurMod;
    public bool isSpawner;
    public GameObject objectToSpawn;
    public BulletBlueprint bullet;
    public ContactBlueprint contact;

    public Sprite ocSpr;
    public int overchargeCost;
}
