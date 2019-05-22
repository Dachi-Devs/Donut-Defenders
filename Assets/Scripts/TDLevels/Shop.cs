using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public TurretBlueprint ringTosser;
    public TurretBlueprint coldStone;
    public TurretBlueprint sodaStreamer;
    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectRingTosser()
    {
        buildManager.SelectTurretToBuild(ringTosser);
    }

    public void SelectSodaStreamer()
    {
        buildManager.SelectTurretToBuild(sodaStreamer);
    }

    public void SelectColdStone()
    {
        buildManager.SelectTurretToBuild(coldStone);
    }
}
