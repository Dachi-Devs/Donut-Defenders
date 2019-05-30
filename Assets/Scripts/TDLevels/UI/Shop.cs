using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public TurretBlueprint[] towerTypes;
    public Text[] towerCounts;
    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
        UpdateText();
    }

    public void UpdateText()
    {
        for(int i = 0; i < towerTypes.Length; i++)
        {
            towerCounts[i].text = buildManager.inv.GetAmount(towerTypes[i].type).ToString();
        }
    }

    public void SelectRingTosser()
    {
        buildManager.SelectTurretToBuild(towerTypes[0]);
    }

    public void SelectSodaStreamer()
    {
        buildManager.SelectTurretToBuild(towerTypes[1]);
    }

    public void SelectColdStone()
    {
        buildManager.SelectTurretToBuild(towerTypes[2]);
    }

    public void SelectSprinkler()
    {
        buildManager.SelectTurretToBuild(towerTypes[3]);
    }
}
