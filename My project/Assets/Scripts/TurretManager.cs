using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public GameObject[] turretSpots;
    public bool[] isSpotOccupied;
    public bool emptySpotAvai = true;
    public bool upgradeAvai = true;

    public int currentUpgradedTurret = 0;

    private void Awake()
    {
        isSpotOccupied = new bool[turretSpots.Length];

        for (int i = 0; i < isSpotOccupied.Length; i++)
        {
            isSpotOccupied[i] = false;
        }
    }

    private void Update()
    {
        CheckUpgradeAvai();

        int count = 0;
        for (int i = 0; i < isSpotOccupied.Length; i++)
        {
            if (isSpotOccupied[i])
            {
                count++;
            }
        }

        if (count == isSpotOccupied.Length)
        {
            emptySpotAvai = false;
        }
        else
        {
            emptySpotAvai = true;
        }
    }

    void CheckUpgradeAvai()
    {
        if (currentUpgradedTurret == 2)
        {
            upgradeAvai = false;
        }
        else
        {
            upgradeAvai = true;
        }
    }

    public void SetOccupied(int spotIndex)
    {
        isSpotOccupied[spotIndex] = true;
    }

    public void DeleteOccupied(int spotIndex)
    {
        isSpotOccupied[spotIndex] = false;
    }
}
