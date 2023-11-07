//using System.Collections;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;
//using Button = UnityEngine.UI.Button;

//public class SkillSystem : MonoBehaviour
//{
//    public Animator animator;

//    public GameObject[] skillAoePrefabs;
//    public GameObject[] skillDisablePrefabs;
//    public GameObject[] skillDpsPrefabs;

//    public GameObject skillAoePreview;
//    public GameObject skillDisablePreview;
//    public GameObject skillDpsPreview;

//    public Button skillAoe;
//    public Button skillDisable;
//    public Button skillDps;

//    public TextMeshProUGUI announceText;

//    private int skillAoeEquipedIndex;
//    private int skillDisableEquipedIndex;
//    private int skillDpsEquipedIndex;

//    public float[] skillMultiCastRate = { 0.3f, 0.3f, 0.3f };
//    public float[] skillFreeCastRate = { 0.3f, 0.3f, 0.3f };
//    public float[] skillCooldown = new float[3];
//    [SerializeField]
//    private float[] currentSkillCooldown = { 0f, 0f, 0f };

//    [SerializeField]
//    private GameObject currentPreview;

//    [SerializeField]
//    private Vector3 lockSkillPos;

//    [SerializeField]
//    private bool isUsingSkill = false;

//    private void Awake()
//    {
//        skillAoeEquipedIndex = 0;
//        skillDisableEquipedIndex = 0;
//        skillDpsEquipedIndex = 0;

//        skillAoe.onClick.AddListener(CastSkillAoe);
//        skillDisable.onClick.AddListener(CastSkillDisable);
//        skillDps.onClick.AddListener(CastSkillDps);
//    }

//    private void Update()
//    {
//        if (!isUsingSkill)
//        {
//            CanPressSkill(true);
//        }
//        else
//        {
//            CanPressSkill(false);
//        }

//        if (currentPreview != null)
//        {
//            if (currentPreview.CompareTag("AoePreview"))
//            {
//                HandleAoePreview();
//            }
//            if (currentPreview.CompareTag("DisablePreview"))
//            {
//                HandleDisablePreview();
//            }
//            if (currentPreview.CompareTag("DpsPreview"))
//            {
//                HandleDpsPreview();
//            }
//        }

//        HandleCoolDownSkill();
//    }

//    void CanPressSkill(bool able)
//    {
//        skillAoe.GetComponent<Button>().interactable = able;
//        skillDisable.GetComponent<Button>().interactable = able;
//        skillDps.GetComponent<Button>().interactable = able;
//    }

//    void HandleCoolDownSkill()
//    {
//        if (currentSkillCooldown[0] > 0f && currentSkillCooldown[0] <= skillCooldown[0])
//        {
//            currentSkillCooldown[0] -= Time.deltaTime;
//            skillAoe.GetComponent<Button>().interactable = false;
//        }

//        if (currentSkillCooldown[0] <= 0f)
//        {
//            skillAoe.GetComponent<Button>().interactable = true;
//        }


//        if (currentSkillCooldown[1] > 0f && currentSkillCooldown[1] <= skillCooldown[1])
//        {
//            currentSkillCooldown[1] -= Time.deltaTime;
//            skillDisable.GetComponent<Button>().interactable = false;
//        }

//        if (currentSkillCooldown[1] <= 0f)
//        {
//            skillDisable.GetComponent<Button>().interactable = true;
//        }


//        if (currentSkillCooldown[2] > 0f && currentSkillCooldown[2] <= skillCooldown[2])
//        {
//            currentSkillCooldown[2] -= Time.deltaTime;
//            skillDps.GetComponent<Button>().interactable = false;
//        }

//        if (currentSkillCooldown[2] <= 0f)
//        {
//            skillDps.GetComponent<Button>().interactable = true;
//        }
//    }

//    private void CastSkillAoe()
//    {
//        if (currentSkillCooldown[0] <= 0f)
//        {
//            Vector3 targetPos = GetMousePoint();

//            GameObject skillPreview = Instantiate(skillAoePreview, targetPos, Quaternion.identity);
//            Transform skillPreviewTransform = skillPreview.GetComponent<Transform>();
//            float scalePreview = skillAoePrefabs[skillAoeEquipedIndex].GetComponent<SkillStat>().SkillAffectRange;
//            skillPreviewTransform.localScale = new Vector3(scalePreview, 0.05f, scalePreview);
//            currentPreview = skillPreview;
//            isUsingSkill = true;
//            announceText.text = "Select position to cast";
//        }
//    }

//    void HandleAoePreview()
//    {
//        Vector3 movDirection = GetMousePoint();
//        movDirection.y = currentPreview.transform.position.y;
//        currentPreview.transform.position = movDirection;

//        if (Input.GetMouseButton(0))
//        {
//            lockSkillPos = currentPreview.transform.position;
//            isUsingSkill = false;
//            currentSkillCooldown[0] = skillCooldown[0];
//            Destroy(currentPreview);
//            HandleSkillAoe();
//        }

//        if (Input.GetMouseButton(1))
//        {
//            isUsingSkill = false;
//            Destroy(currentPreview);
//        }
//    }

//    void HandleSkillAoe()
//    {
//        animator.SetTrigger("isCasting");

//        StartCoroutine(DisplayText("Meteor is coming!"));
//        Vector3 randomOffset = Random.insideUnitCircle * 10f;
//        Vector3 spawnPos = lockSkillPos + new Vector3(randomOffset.x, 25f, randomOffset.y);

//        GameObject skillAoe = Instantiate(skillAoePrefabs[skillAoeEquipedIndex], spawnPos, Quaternion.identity);
//        AoeCasting aoeCasting = skillAoe.GetComponent<AoeCasting>();

//        if (aoeCasting != null)
//        {
//            aoeCasting.SetTargetPosition(lockSkillPos);
//        }

//        //MultiCast && FreeCast
//        float random = Random.value;
//        if (random <= skillMultiCastRate[0])
//        {
//            Invoke("HandleSkillAoe", 1f); ;
//        }

//        for (int i = 1; i < 3; i++)
//        {
//            float random2 = Random.value;
//            if (random2 <= skillFreeCastRate[i])
//            {
//                if (i == 1)
//                {
//                    Invoke("HandleSkillDisable", 1f);
//                }
//                if (i == 2)
//                {
//                    Invoke("HandleSkillDps", 1f);
//                }
//            }
//        }
//    }

//    private void CastSkillDisable()
//    {
//        if (currentSkillCooldown[1] <= 0f)
//        {
//            Vector3 targetPos = GetMousePoint();

//            GameObject skillPreview = Instantiate(skillDisablePreview, targetPos, Quaternion.identity);
//            Transform skillPreviewTransform = skillPreview.GetComponent<Transform>();
//            float scalePreview = skillDisablePrefabs[skillDisableEquipedIndex].GetComponent<SkillStat>().SkillAffectRange;
//            skillPreviewTransform.localScale = new Vector3(scalePreview, 0.05f, scalePreview);
//            currentPreview = skillPreview;
//            isUsingSkill = true;
//            announceText.text = "Select position to cast";
//        }
//    }

//    void HandleDisablePreview()
//    {
//        Vector3 movDirection = GetMousePoint();
//        movDirection.y = currentPreview.transform.position.y;
//        currentPreview.transform.position = movDirection;

//        if (Input.GetMouseButton(0))
//        {
//            lockSkillPos = currentPreview.transform.position;
//            isUsingSkill = false;
//            currentSkillCooldown[1] = skillCooldown[1];
//            Destroy(currentPreview);
//            HandleSkillDisable();
//        }

//        if (Input.GetMouseButton(1))
//        {
//            Destroy(currentPreview);
//            isUsingSkill = false;
//        }
//    }

//    void HandleSkillDisable()
//    {
//        StartCoroutine(DisplayText("Stun Grenade is coming!"));
//        Vector3 randomOffset = Random.insideUnitCircle * 10f;
//        Vector3 spawnPos = lockSkillPos + new Vector3(randomOffset.x, 25f, randomOffset.y);

//        GameObject skillDisable = Instantiate(skillDisablePrefabs[skillDisableEquipedIndex], spawnPos, Quaternion.identity);
//        DisableCasting disableCasting = skillDisable.GetComponent<DisableCasting>();

//        if (disableCasting != null)
//        {
//            disableCasting.SetTargetPosition(lockSkillPos);
//        }

//        //MultiCast && FreeCast
//        float random = Random.value;
//        if (random <= skillMultiCastRate[1])
//        {
//            Invoke("HandleSkillDisable", 1f);
//        }

//        for (int i = 0; i < 3; i += 2)
//        {
//            float random2 = Random.value;
//            if (random2 <= skillFreeCastRate[i])
//            {
//                if (i == 0)
//                {
//                    Invoke("HandleSkillAoe", 1f);
//                }
//                if (i == 2)
//                {
//                    Invoke("HandleSkillDps", 1f);
//                }
//            }
//        }
//    }

//    private void CastSkillDps()
//    {
//        if (currentSkillCooldown[2] <= 0f)
//        {
//            GameObject skillPreview = Instantiate(skillDpsPreview, transform.position, Quaternion.identity);
//            Transform skillPreviewTransform = skillPreview.GetComponent<Transform>();
//            float scalePreview = skillDpsPrefabs[skillDpsEquipedIndex].GetComponent<SkillStat>().SkillAffectRange;
//            skillPreviewTransform.localScale = new Vector3(0.5f, 0.1f, scalePreview);
//            currentPreview = skillPreview;
//            isUsingSkill = true;
//            announceText.text = "Select position to cast";
//        }
//    }

//    void HandleDpsPreview()
//    {
//        Vector3 targetPos = GetMousePoint();
//        Vector3 skillDirection = new Vector3(targetPos.x - transform.position.x, targetPos.y, targetPos.z - transform.position.z).normalized;
//        float scalePreview = skillDpsPrefabs[skillDpsEquipedIndex].GetComponent<SkillStat>().SkillAffectRange;
//        Vector3 skillCenterPos = skillDirection * (scalePreview / 2f);
//        currentPreview.transform.position = skillCenterPos;

//        currentPreview.transform.forward = skillDirection;

//        if (Input.GetMouseButton(0))
//        {
//            lockSkillPos = currentPreview.transform.position;
//            isUsingSkill = false;
//            currentSkillCooldown[2] = skillCooldown[2];
//            Destroy(currentPreview);
//            HandleSkillDps();
//        }

//        if (Input.GetMouseButton(1))
//        {
//            Destroy(currentPreview);
//            isUsingSkill = false;
//        }
//    }

//    void HandleSkillDps()
//    {
//        StartCoroutine(DisplayText("Sniper is shooting"));

//        GameObject skillDps = Instantiate(skillDpsPrefabs[skillDpsEquipedIndex], transform.position, Quaternion.identity);
//        DpsCasting dpsCasting = skillDps.GetComponent<DpsCasting>();

//        if (dpsCasting != null )
//        {
//            dpsCasting.SetTargetPosition(lockSkillPos);
//        }

//        //MultiCast && FreeCast
//        float random = Random.value;
//        if (random <= skillMultiCastRate[2])
//        {
//            Invoke("HandleSkillAoe", 1f);
//        }

//        for (int i = 0; i < 2; i++)
//        {
//            float random2 = Random.value;
//            if (random2 <= skillFreeCastRate[i])
//            {
//                if (i == 1)
//                {
//                    Invoke("HandleSkillDisable", 1f);
//                }
//                if (i == 0)
//                {
//                    Invoke("HandleSkillAoe", 1f);
//                }
//            }
//        }
//    }

//    Vector3 GetMousePoint()
//    {
//        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

//        float rayDistance;
//        if (groundPlane.Raycast(ray, out rayDistance))
//        {
//            Vector3 hitPoint = ray.GetPoint(rayDistance);
//            return hitPoint;
//        }

//        return Vector3.zero;
//    }

//    IEnumerator DisplayText(string text)
//    {
//        announceText.text = text;

//        yield return new WaitForSeconds(10);

//        announceText.text = "";
//    }
//}
