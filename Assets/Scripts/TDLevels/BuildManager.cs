using System.Collections;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public Inventory inv;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene");
            return;
        }
        instance = this;
    }

    public GameObject buildEffect;
    public GameObject reclaimEffect;
    [SerializeField]
    private TurretBlueprint turretToBuild;
    [SerializeField]
    private GameObject baseTurret;
    private Node selectedNode;

    public TurretUI turretUI;
    public BuildUI buildUI;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inv.OutputDict();
        }
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    public bool HasTurret()
    {
        if (turretToBuild != null)
        {
            return inv.HasTurret(turretToBuild.type);
        }
        return false;
    } 

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        turretUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        turretUI.Hide();
    }

    public void BuildTurret(Node node)
    {
        if (!HasTurret())
        {
            buildUI.CantBuild();
            return;
        }

        inv.Build(turretToBuild.type);

        GameObject turretGO = Instantiate(baseTurret, node.GetBuildPosition(), Quaternion.identity);
        TurretController turret = turretGO.GetComponent<TurretController>();
        turret.Setup(turretToBuild);
        node.SetNodeTurret(turretToBuild, turret);

        GameObject effect = Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
}
