using UnityEngine;

public class TurretSpot : MonoBehaviour
{
    [SerializeField]
    private bool m_isSettled = false;

    public bool IsSettled { get { return m_isSettled; } set { m_isSettled = value; } }
}
