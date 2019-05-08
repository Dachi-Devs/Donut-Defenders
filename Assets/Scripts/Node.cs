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


    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

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

        Debug.Log("Turret built, money left: " + PlayerStats.Money);
    }

    public void OverchargeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.overchargeCost)
        {
            Debug.Log("Not enough money to upgrade");
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

        Debug.Log("Turret upgraded, money left: " + PlayerStats.Money);
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
        rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = cantBuyColor;
        }

    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
