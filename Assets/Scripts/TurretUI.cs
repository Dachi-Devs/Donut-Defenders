using UnityEngine;
using UnityEngine.UI;

public class TurretUI : MonoBehaviour
{
    public GameObject UI;

    public Text overchargeCost;
    public Button overchargeButton;

    private Node target;

    public void SetTarget(Node _target)
    {
        target = _target;

        if (!target.isUpgraded)
        {
            overchargeCost.text = "£" + target.turretBlueprint.overchargeCost;
            overchargeButton.interactable = true;
        }
        else
        {
            overchargeCost.text = "MAX LEVEL";
            overchargeButton.interactable = false;
        }

        UI.SetActive(true);
    }

    public void Hide()
    {
        UI.SetActive(false);
    }

    public void Overcharge()
    {
        target.OverchargeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Reclaim()
    {
        target.ReclaimTurret();
        BuildManager.instance.DeselectNode();
    }
}
