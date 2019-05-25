using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public TurretBlueprint ringTosser;
    public Text ringCount;
    public TurretBlueprint coldStone;
    public Text coldCount;
    public TurretBlueprint sodaStreamer;
    public Text sodaCount;
    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
        UpdateText();
    }

    public void UpdateText()
    {
        Debug.Log(buildManager.inv.GetAmount(ringTosser.type));
        ringCount.text = buildManager.inv.GetAmount(ringTosser.type).ToString();
        coldCount.text = buildManager.inv.GetAmount(coldStone.type).ToString();
        sodaCount.text = buildManager.inv.GetAmount(sodaStreamer.type).ToString();
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
