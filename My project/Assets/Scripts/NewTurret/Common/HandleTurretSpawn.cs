using UnityEngine;

public class HandleTurretSpawn : MonoBehaviour
{
    private TurretManager tm;

    [SerializeField]
    private GameObject turretPrefab;

    private GameObject[] m_turretSpots;
    private GameObject m_nearestSpot;
    private GameObject m_currentTurret;

    private int m_snappedIndex = -1;

    private bool m_isUsing = false;

    public GameObject TurretPrefab { get { return turretPrefab; } }
    public bool IsUsing {  get { return m_isUsing; } }

    private void Awake()
    {
        tm = GetComponent<TurretManager>();
        m_turretSpots = new GameObject[tm.TurretSpots.Length];

        for (int i = 0; i < m_turretSpots.Length; i++)
        {
            m_turretSpots[i] = tm.TurretSpots[i];
        }
    }

    private void Update()
    {
        m_isUsing = m_currentTurret != null;

        if (m_currentTurret != null)
        {
            bool isSettle = m_currentTurret.GetComponent<Turret>().IsSettled;

            if (Input.GetMouseButton(0))
            {
                Vector3 center = new Vector3(m_currentTurret.transform.position.x, m_currentTurret.transform.position.y - 2, m_currentTurret.transform.position.z);
                Collider[] colliders = Physics.OverlapBox(center, new Vector3(1f, 3f, 1f), Quaternion.identity);

                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("TurretSpot") && !collider.GetComponent<TurretSpot>().IsSettled)
                    {
                        m_currentTurret.GetComponent<Turret>().IsSettled = true;
                        m_currentTurret.GetComponent<Turret>().LockPos = new Vector3(collider.transform.position.x, 0f, collider.transform.position.z);
                        collider.GetComponent<TurretSpot>().IsSettled = true;
                        if (m_snappedIndex != -1)
                        {
                            m_currentTurret.GetComponent<Turret>().SpotIndex = m_snappedIndex;
                            tm.SetOccupied(m_snappedIndex);
                            m_currentTurret = null;
                        }
                        break;
                    }
                }
            }

            if (Input.GetMouseButton(1))
            {
                if (!isSettle)
                {
                    Destroy(m_currentTurret);
                }
            }

            if (!isSettle && m_currentTurret != null)
            {
                Vector3 movPosition = GetMousePoint();
                movPosition.y = m_currentTurret.transform.position.y;
                m_currentTurret.transform.position = movPosition;
                SnapToNearestSpot();
            }
        }

    }

    public void CreateTurret()
    {
        if (tm.EmptySpotAvai)
        {
            SpawnTurretAtPosition();
        }
        else
        {
            Debug.Log("full");
        }
    }

    private void SpawnTurretAtPosition()
    {
        Vector3 mousePosition = GetMousePoint();

        GameObject turret = Instantiate(turretPrefab, mousePosition, Quaternion.identity);
        m_currentTurret = turret;
    }

    void SnapToNearestSpot()
    {
        float minDistance = 5f;
        m_nearestSpot = null;

        for (int i = 0; i < m_turretSpots.Length; i++)
        {
            if (!tm.IsSpotsOccupied[i])
            {
                float distance = Vector3.Distance(m_currentTurret.transform.position, m_turretSpots[i].transform.position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    m_nearestSpot = m_turretSpots[i];
                    m_snappedIndex = i;
                }
            }
        }

        if (m_nearestSpot != null)
        {
            m_currentTurret.transform.position = new Vector3(m_nearestSpot.transform.position.x, 0f, m_nearestSpot.transform.position.z);
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
}
