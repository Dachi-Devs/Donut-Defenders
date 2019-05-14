using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor, cantBuyColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;


    private SpriteRenderer sprite;
    private Color startColor;

    BuildManager buildManager;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        startColor = sprite.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

       if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBlueprint bp)
    {

        if (PlayerStats.Money < bp.cost)
        {
            Debug.Log("Not enough money");
            return;
        }

        PlayerStats.Money -= bp.cost;

        GameObject _turret = Instantiate(bp.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        turretBlueprint = bp;

        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    public void OverchargeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.overchargeCost)
        {
            return;
        }

        PlayerStats.Money -= turretBlueprint.overchargeCost;

        //Clear old turret
        Destroy(turret);

        //Build new turret
        GameObject _turret = Instantiate(turretBlueprint.overchargePrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);

        isUpgraded = true;
    }

    public void ReclaimTurret()
    {
        PlayerStats.Money += turretBlueprint.cost;

        GameObject effect = Instantiate(buildManager.reclaimEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 2.5f);

        Destroy(turret);
        turretBlueprint = null;
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;
        if (buildManager.HasMoney)
        {
            sprite.color = hoverColor;
        }
        else
        {
            sprite.color = cantBuyColor;
        }

    }

    void OnMouseExit()
    {
        sprite.color = startColor;
    }
}
