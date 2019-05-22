using UnityEngine;

[CreateAssetMenu(fileName = "Turret", menuName = "Items/Turret")]
public class TurretBlueprint : ScriptableObject
{
    public Sprite spr;
    public string type;

    public float range;
    public float fireRate;
    public int damageOverTime;
    public float slowPct;
    public GameObject bulletPrefab;

    public Sprite ocSpr;
    public int overchargeCost;
}
