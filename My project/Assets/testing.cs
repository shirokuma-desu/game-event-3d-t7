using UnityEngine;
using UnityEngine.UI;

public class testing : MonoBehaviour
{
    private HandleTurretSpawn hts;

    [SerializeField]
    private Button m_shopButton;
    [SerializeField]
    private Button m_turretButton;
    [SerializeField]
    private GameObject shopCanvas;
    [SerializeField]
    private GameObject turretBuyCanvas;

    private bool shopOn = false;

    private void Start()
    {
        hts = GetComponent<HandleTurretSpawn>();

        m_shopButton.onClick.AddListener(ShopButton);
        m_turretButton.onClick.AddListener(TurretBuyButton);

        shopCanvas.SetActive(true);
        turretBuyCanvas.SetActive(false);
    }

    void ShopButton()
    {
        shopOn = !shopOn;
        turretBuyCanvas.SetActive(shopOn);
    }

    void TurretBuyButton()
    {
        if (!hts.IsUsing)
        {
            hts.CreateTurret();
        }
        else
        {
            Debug.Log("Place turret first then buy a new one");
        }
    }
}
