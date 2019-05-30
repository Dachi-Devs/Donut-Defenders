using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;

    [HideInInspector]
    public GameObject turretObject;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public TurretController turret;
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
        return transform.position;
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

        buildManager.BuildTurret(this);
    }

    private void CannotBuild()
    {
        Debug.Log("Can't Build");
    }

    public void SetNodeTurret(TurretBlueprint turretBP, TurretController turretCont)
    {
        turretBlueprint = turretBP;
        turret = turretCont;
    }

    public void OverchargeTurret()
    {
        if (PlayerStats.energy < turretBlueprint.overchargeCost)
        {
            return;
        }

        PlayerStats.energy -= turretBlueprint.overchargeCost;

        turret.Overcharge();

        isUpgraded = true;
    }

    public void ReclaimTurret()
    {
        buildManager.inv.AddTurret(turretBlueprint.type, 1);
        buildManager.shopUI.UpdateText();

        GameObject effect = Instantiate(buildManager.reclaimEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 2.5f);

        Destroy(turret.gameObject);
        turretBlueprint = null;

        isUpgraded = false;
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        sprite.color = hoverColor;
    }

    void OnMouseExit()
    {
        sprite.color = startColor;
    }
}
