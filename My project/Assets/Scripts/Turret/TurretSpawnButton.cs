using System.Collections;
using TMPro;
using UnityEngine;

public class TurretSpawnButton : MonoBehaviour
{
    private TurretManager turretManager;
    private MoneySystem moneySystem;

    public GameObject turretPrefab;

    public TextMeshProUGUI announceText;

    [SerializeField]
    private GameObject[] turretSpots;
    private GameObject nearestSpot;
    private GameObject currentTurret;

    [SerializeField]
    private int snappedIndex = -1;

    public bool isUsing = false;

    private void Awake()
    {
        moneySystem = GetComponent<MoneySystem>();
        turretManager = GetComponent<TurretManager>();
        turretSpots = new GameObject[turretManager.turretSpots.Length];

        for (int i = 0; i <  turretSpots.Length; i++)
        {
            turretSpots[i] = turretManager.turretSpots[i];
        }
    }

    private void Update()
    {
        isUsing = currentTurret != null;

        if (currentTurret != null)
        {
            bool isSettle = currentTurret.GetComponent<Turret>().isSettle;

            if (Input.GetMouseButton(0))
            {
                Vector3 center = new Vector3(currentTurret.transform.position.x, currentTurret.transform.position.y - 2, currentTurret.transform.position.z);
                Collider[] colliders = Physics.OverlapBox(center, new Vector3(1f, 3f, 1f), Quaternion.identity);

                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("TurretSpot") && !collider.GetComponent<TurretSpot>().isSettle)
                    {
                        currentTurret.GetComponent<Turret>().isSettle = true;
                        currentTurret.GetComponent<Turret>().lockPos = new Vector3(collider.transform.position.x, 0f, collider.transform.position.z);
                        collider.GetComponent<TurretSpot>().isSettle = true;
                        moneySystem.money -= 100;
                        if (snappedIndex != -1)
                        {
                            currentTurret.GetComponent<Turret>().spotIndex = snappedIndex;
                            turretManager.SetOccupied(snappedIndex);
                            currentTurret = null;
                        }
                        break;
                    }
                }
            }

            if (Input.GetMouseButton(1))
            {
                if (!isSettle)
                {
                    Destroy(currentTurret);
                }
            }

            if (!isSettle && currentTurret != null)
            {
                Vector3 movPosition = GetMousePoint();
                movPosition.y = currentTurret.transform.position.y;
                currentTurret.transform.position = movPosition;
                SnapToNearestSpot();
            }
        }

    }

    public void CreateTurret()
    {
        if (turretManager.emptySpotAvai)
        {
            SpawnTurretAtPosition();
        }
        else
        {
            StartCoroutine(DisplayText("Full Spots"));
        }
    }

    private void SpawnTurretAtPosition()
    {
        Vector3 mousePosition = GetMousePoint();

        GameObject turret = Instantiate(turretPrefab, mousePosition, Quaternion.identity);
        currentTurret = turret;
    }

    void SnapToNearestSpot()
    {
        float minDistance = 5f;
        nearestSpot = null;

        for (int i = 0; i < turretSpots.Length; i++)
        {
            if (!turretManager.isSpotOccupied[i])
            {
                float distance = Vector3.Distance(currentTurret.transform.position, turretSpots[i].transform.position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestSpot = turretSpots[i];
                    snappedIndex = i;
                }
            }
        }

        if (nearestSpot != null)
        {
            currentTurret.transform.position = new Vector3(nearestSpot.transform.position.x, 0f, nearestSpot.transform.position.z);
        }
    }

    Vector3 GetMousePoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 hitPoint = ray.GetPoint(rayDistance);
            return hitPoint;
        }

        return Vector3.zero;
    }

    IEnumerator DisplayText(string text)
    {
        announceText.text = text;

        yield return new WaitForSeconds(5);

        announceText.text = "";
    }
}