using UnityEngine;

public class testing : MonoBehaviour
{
    [SerializeField] private GameObject FloatingTextPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (FloatingTextPrefab)
            {
                Debug.Log("sdad");
                ShowFloatingText();
            }
        }
    }

    private void ShowFloatingText()
    {
        var go = Instantiate(FloatingTextPrefab, new Vector3(transform.position.x, 7f, transform.position.z), Quaternion.identity);
    }
}
