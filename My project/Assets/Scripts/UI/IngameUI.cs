using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class IngameUI : MonoBehaviour
{
    public TurretManager turretManager;
    public TurretSpawnButton turretSpawnButton;
    public MoneySystem moneySystem;

    public GameObject upgradeAndSellCanvas;
    public TextMeshProUGUI targetNameText;
    public TextMeshProUGUI targetInfoText;
    public Button upgradeButton;
    public Button repairButton;
    public Button sellButton;
    public Button cancelButton;

    public TextMeshProUGUI announceText;

    public GameObject shopCanvas;
    public GameObject turretBuyCanvas;

    public Button shopButton;
    public Button turretButton;
    private bool shopOn = false;

    [SerializeField]
    private int repairCost;
    private int repairTimes = 0;
    public TextMeshProUGUI repairCostText;

    public GameObject[] upgradePrefabs;

    private GameObject selectedTurret;

    private void Start()
    {
        upgradeButton.onClick.AddListener(UpgradeButton);
        repairButton.onClick.AddListener(RepairButton);
        sellButton.onClick.AddListener(SellButton);
        cancelButton.onClick.AddListener(CancelButton);
        upgradeAndSellCanvas.SetActive(false);

        turretButton.onClick.AddListener(TurretBuyButton);
        shopButton.onClick.AddListener(ShopButton);
        shopCanvas.SetActive(true);
        turretBuyCanvas.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Turret"))
                {
                    selectedTurret = hit.collider.gameObject;
                    if (selectedTurret.GetComponent<Turret>().isSettle)
                    {
                        ShowUpgradeSellMenu();
                    }
                }
            }
        }

        if (selectedTurret != null)
        {
            int currentHealth = selectedTurret.GetComponent<Turret>().health;
            int maxHealth = selectedTurret.GetComponent<Turret>().turretStat.health;
            int repairCostAll = (maxHealth - currentHealth) / 10 * repairCost;
            repairCostAll += repairCostAll * (int)(0.05f * repairTimes);

            repairCostText.text = "Repair (-" + repairCostAll.ToString() + ")";

            targetNameText.text = selectedTurret.name;
            targetInfoText.text = "Health: " + currentHealth.ToString() + "/" + maxHealth.ToString() + "\n"
                                    + "AD: " + selectedTurret.GetComponent<Turret>().attackDamage.ToString() + "\t"
                                    + "Speed: " + selectedTurret.GetComponent<Turret>().attackSpeed.ToString();
        }
    }

    void ShopButton()
    {
        shopOn = !shopOn;
        turretBuyCanvas.SetActive(shopOn);
    }

    void TurretBuyButton()
    {
        if (moneySystem.money >= 100)
        {
            if (!turretSpawnButton.isUsing)
            {
                turretSpawnButton.CreateTurret();
            }
            else
            {
                StartCoroutine(DisplayText("Place turret first then buy a new one"));
            }

        }
        else
        {
            StartCoroutine(DisplayText("NOT enough money!"));
        }
    }

    void ShowUpgradeSellMenu()
    {
        upgradeAndSellCanvas.SetActive(true);
    }

    void UpgradeButton()
    {
        upgradeAndSellCanvas.SetActive(false);
    }

    void RepairButton()
    {
        int currentHealth = selectedTurret.GetComponent<Turret>().health;
        int maxHealth = selectedTurret.GetComponent<Turret>().turretStat.health;
        int repairCostAll = (maxHealth - currentHealth) * repairCost;
        repairCostAll += repairCostAll * (int)(0.05f * repairTimes);

        if (currentHealth == maxHealth)
        {
            StartCoroutine(DisplayText("Turret's health is still full!"));
        }
        else
        {
            if (moneySystem.money >= repairCostAll)
            {
                moneySystem.money -= repairCostAll;
                selectedTurret.GetComponent<Turret>().health = selectedTurret.GetComponent<Turret>().turretStat.health;
            }
            else
            {
                StartCoroutine(DisplayText("NOT enough money!"));
            }
        }

        upgradeAndSellCanvas.SetActive(false);
    }

    void SellButton()
    {
        if (selectedTurret.GetComponent<Turret>().isUpgrade)
        {
            turretManager.GetComponent<TurretManager>().currentUpgradedTurret--;
        }

        if (selectedTurret != null)
        {
            turretManager.DeleteOccupied(selectedTurret.GetComponent<Turret>().spotIndex);
            turretManager.turretSpots[selectedTurret.GetComponent<Turret>().spotIndex].GetComponent<TurretSpot>().isSettle = false;
            moneySystem.money += 70;
            Destroy(selectedTurret);
        }
        upgradeAndSellCanvas.SetActive(false);
    }

    void CancelButton()
    {
        upgradeAndSellCanvas.SetActive(false);
    }

    IEnumerator DisplayText(string text)
    {
        announceText.text = text;

        yield return new WaitForSeconds(5);

        announceText.text = "";
    }
}