using JetBrains.Annotations;
using LeakyAbstraction;
using UnityEngine;

public class SkillVisualScript : MonoBehaviour
{
    private SkillThingContainer container;
    private SkillCastController controller;
    private int[] equippedSkillIndex = new int[3];

    [HideInInspector] public int skillPressedIndex = -1;
    [HideInInspector] public int skillPrefabUsingIndex;
    private GameObject currentPreview;
    private Vector3 lockSkillPos = Vector3.zero;
    [HideInInspector] public bool isUsingSkill = false;

    private GameObject currentSkillToMulti;

    private int multiCastTimes = 0;

    private void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("BaseTower").GetComponent<SkillCastController>();
        container = GetComponent<SkillThingContainer>();
        equippedSkillIndex = container.EquippedSkill;
    }

    private void Update()
    {
        if (currentPreview != null)
        {
            switch (skillPrefabUsingIndex)
            {
                case 0:
                    HandlePreviewMeteor();
                    break;
                case 1:
                    HandlePreviewAR();
                    break;
                case 2:
                    HandlePreviewLB();
                    break;
                case 3:
                    HandlePrevewSF();
                    break;
                case 4:
                    HandlePreviewShade();
                    break;
                case 5:
                    HandlePreviewLava();
                    break;
            }
        }

        Multicast();
    }

    #region Meteor Skill
    public void UseMeteorSkill()
    {
        GameObject skillPreview = Instantiate(container.SkillPreview[0], transform.position, Quaternion.identity);
        float skillRangeAffect = container.SkillPrefabs[0].GetComponent<SkillStats>().Range;
        skillPreview.GetComponent<Transform>().localScale = new Vector3(2f * skillRangeAffect, 0.05f, 2f * skillRangeAffect);
        currentPreview = skillPreview;
        isUsingSkill = true;
    }

    private void HandlePreviewMeteor()
    {
        Vector3 targetPos = GetMousePoint();
        targetPos.y = currentPreview.transform.position.y;
        currentPreview.transform.position = targetPos;

        if (Input.GetMouseButton(0))
        {
            lockSkillPos = currentPreview.transform.position;
            isUsingSkill = false;
            controller.skillLastUsed[skillPressedIndex] = Time.time;
            Destroy(currentPreview);
            HandleMeteorSkill(lockSkillPos);
            multiCastTimes = 0;
        }

        if (Input.GetMouseButton(1))
        {
            isUsingSkill = false;
            Destroy(currentPreview);
        }
    }

    private void HandleMeteorSkill(Vector3 targetPos)
    {
        Vector2 randomOffset = Random.insideUnitCircle * 10f;
        Vector3 spawnPos = targetPos + new Vector3(randomOffset.x, 25f, randomOffset.y);

        GameObject meteor = Instantiate(container.SkillPrefabs[0], spawnPos, Quaternion.identity);
        MeteorCasting meteorScript = meteor.GetComponent<MeteorCasting>();

        if (meteorScript != null)
        {
            meteorScript.SetTarget(targetPos);
        }

        container.CastSkill.RaiseEvent();
        SoundManager.Instance.PlaySound(GameSound.MeteorCast);
    }
    #endregion

    #region Acid Rain Skill
    public void UseAcidRainSkill()
    {
        Vector3 targetPos = GetMousePoint();
        GameObject skillPreview = Instantiate(container.SkillPreview[0], transform.position, Quaternion.identity);
        float scalePreview = container.SkillPrefabs[1].GetComponent<SkillStats>().Range;
        skillPreview.transform.localScale = new Vector3(2 * scalePreview, 0.05f, 2 * scalePreview);
        currentPreview = skillPreview;
        isUsingSkill = true;
    }

    private void HandlePreviewAR()
    {
        Vector3 targetPos = GetMousePoint();
        targetPos.y = currentPreview.transform.position.y;
        currentPreview.transform.position = targetPos;

        if (Input.GetMouseButton(0))
        {
            lockSkillPos = currentPreview.transform.position;
            isUsingSkill = false;
            controller.skillLastUsed[skillPressedIndex] = Time.time;
            Destroy(currentPreview);
            HandleAcidRainSkill();
            multiCastTimes = 0;
        }

        if (Input.GetMouseButton(1))
        {
            isUsingSkill = false;
            Destroy(currentPreview);
        }
    }

    private void HandleAcidRainSkill()
    {
        GameObject acidRain = Instantiate(container.SkillPrefabs[1], lockSkillPos, Quaternion.identity);
        currentSkillToMulti = acidRain;
        float scaleSkill = container.SkillPrefabs[1].GetComponent<SkillStats>().Range;
        acidRain.transform.localScale = new Vector3(2 * scaleSkill, 0.05f, 2 * scaleSkill);
        AcidRainCasting acidScript = acidRain.GetComponent<AcidRainCasting>();
        if (acidScript != null)
        {
            acidScript.SetTarget(lockSkillPos);
        }

        container.CastSkill.RaiseEvent();
        SoundManager.Instance.PlaySound(GameSound.AcidCast);
    }
    #endregion

    #region Lazer Beam Skill
    public void UseLazerBeamSkill()
    {
        GameObject skillPreview = Instantiate(container.SkillPreview[1], transform.position, Quaternion.identity);
        float scalePreview = container.SkillPrefabs[2].GetComponent<SkillStats>().Range;
        skillPreview.transform.localScale = new Vector3(0.5f, 0.1f, scalePreview);
        currentPreview = skillPreview;
        isUsingSkill = true;
    }

    private void HandlePreviewLB()
    {
        Vector3 targetPos = GetMousePoint();
        Vector3 skillDir = targetPos - transform.position;
        skillDir.y = targetPos.y;
        Vector3 skillCenterPos = skillDir.normalized * (container.SkillPrefabs[2].GetComponent<SkillStats>().Range / 2f);
        currentPreview.transform.position = skillCenterPos;
        currentPreview.transform.forward = skillDir.normalized;

        if (Input.GetMouseButton(0))
        {
            lockSkillPos = currentPreview.transform.position;
            isUsingSkill = false;
            controller.skillLastUsed[skillPressedIndex] = Time.time;
            Destroy(currentPreview);
            HandleLazerBeamSkill(lockSkillPos);
            multiCastTimes = 0;
        }

        if (Input.GetMouseButton(1))
        {
            isUsingSkill = false;
            Destroy(currentPreview);
        }
    }

    private void HandleLazerBeamSkill(Vector3 targetPos)
    {
        GameObject lazer = Instantiate(container.SkillPrefabs[2], transform.position, Quaternion.identity);
        float scaleSkill = container.SkillPrefabs[2].GetComponent<SkillStats>().Range;
        lazer.transform.localScale = new Vector3(0.5f, 0.5f, scaleSkill);
        lazer.transform.position = (targetPos - transform.position).normalized * (scaleSkill / 2f);
        lazer.transform.position = new Vector3(lazer.transform.position.x, 1.5f, lazer.transform.position.z);
        lazer.transform.forward = (targetPos - transform.position).normalized;

        LazerBeamCasting lazerScript = lazer.GetComponent<LazerBeamCasting>();
        if (lazerScript != null)
        {
            lazerScript.SetTarget(targetPos);
        }

        container.CastSkill.RaiseEvent();
        SoundManager.Instance.PlaySound(GameSound.LazerCast);
    }
    #endregion

    #region StarFall Skill
    public void UseStarFallSkill()
    {
        GameObject skillPreview = Instantiate(container.SkillPreview[0], transform.position, Quaternion.identity);
        float skillRangeAffect = container.SkillPrefabs[3].GetComponent<SkillStats>().Range;
        skillPreview.GetComponent<Transform>().localScale = new Vector3(2f * skillRangeAffect, 0.05f, 2f * skillRangeAffect);
        currentPreview = skillPreview;
        isUsingSkill = true;
    }

    private void HandlePrevewSF()
    {
        Vector3 targetPos = GetMousePoint();
        targetPos.y = currentPreview.transform.position.y;
        currentPreview.transform.position = targetPos;

        if (Input.GetMouseButton(0))
        {
            lockSkillPos = currentPreview.transform.position;
            isUsingSkill = false;
            controller.skillLastUsed[skillPressedIndex] = Time.time;
            Destroy(currentPreview);
            HandleStarFallSkill(container.SkillPrefabs[3].GetComponent<SkillStats>().NumberOfPieces);
            multiCastTimes = 0;
        }

        if (Input.GetMouseButton(1))
        {
            isUsingSkill = false;
            Destroy(currentPreview);
        }
    }

    private void HandleStarFallSkill(int numberPieces)
    {
        for (int i = 1; i <= numberPieces; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * container.SkillPrefabs[3].GetComponent<SkillStats>().Range;
            Vector3 spawnPos = lockSkillPos + new Vector3(randomOffset.x, lockSkillPos.y, randomOffset.y);

            GameObject star = Instantiate(container.SkillPrefabs[3], spawnPos, Quaternion.identity);
            StarFallCasting starScript = star.GetComponent<StarFallCasting>();
            if (starScript != null )
            {
                starScript.SetDamage();
            }
        }

        container.CastSkill.RaiseEvent();
        SoundManager.Instance.PlaySound(GameSound.StarCast);
    }
    #endregion

    #region Shade Skill
    public void UseShadeSkill()
    {
        GameObject skillPreview = Instantiate(container.SkillPreview[1], transform.position, Quaternion.identity);
        skillPreview.GetComponent<Transform>().localScale = new Vector3(1f, 0.05f, 1f);
        currentPreview = skillPreview;
        isUsingSkill = true;
    }

    private void HandlePreviewShade()
    {
        Vector3 targetPos = GetMousePoint();
        targetPos.y = currentPreview.transform.position.y;
        currentPreview.transform.position = targetPos;

        if (Input.GetMouseButton(0))
        {
            lockSkillPos = currentPreview.transform.position;
            isUsingSkill = false;
            controller.skillLastUsed[skillPressedIndex] = Time.time;
            Destroy(currentPreview);
            HandleShadeSkill(lockSkillPos);
            multiCastTimes = 0;
        }

        if (Input.GetMouseButton(1))
        {
            isUsingSkill = false;
            Destroy(currentPreview);
        }
    }

    private void HandleShadeSkill(Vector3 targetPos)
    {
        GameObject shade = Instantiate(container.SkillPrefabs[4], targetPos, Quaternion.identity);
        ShadeCasting shadeScript = shade.GetComponent<ShadeCasting>();
        if (shadeScript != null)
        {
            shadeScript.Settarget(targetPos);
        }

        container.CastSkill.RaiseEvent();
        SoundManager.Instance.PlaySound(GameSound.ShadeCast);
    }
    #endregion

    #region Lava Skill
    public void UseLavaSkill()
    {
        Vector3 targetPos = GetMousePoint();
        GameObject skillPreview = Instantiate(container.SkillPreview[0], transform.position, Quaternion.identity);
        float scalePreview = container.SkillPrefabs[5].GetComponent<SkillStats>().Range;
        skillPreview.transform.localScale = new Vector3(2 * scalePreview, 0.05f, 2 * scalePreview);
        currentPreview = skillPreview;
        isUsingSkill = true;
    }

    private void HandlePreviewLava()
    {
        Vector3 targetPos = GetMousePoint();
        targetPos.y = currentPreview.transform.position.y;
        currentPreview.transform.position = targetPos;

        if (Input.GetMouseButton(0))
        {
            lockSkillPos = currentPreview.transform.position;
            isUsingSkill = false;
            controller.skillLastUsed[skillPressedIndex] = Time.time;
            Destroy(currentPreview);
            HandleLavaSkill();
            multiCastTimes = 0;
        }

        if (Input.GetMouseButton(1))
        {
            isUsingSkill = false;
            Destroy(currentPreview);
        }
    }

    private void HandleLavaSkill()
    {
        GameObject lava = Instantiate(container.SkillPrefabs[5], lockSkillPos, Quaternion.identity);
        float scaleSkill = container.SkillPrefabs[5].GetComponent<SkillStats>().Range;
        lava.transform.localScale = new Vector3(2 * scaleSkill, 0.05f, 2 * scaleSkill);
        LavaCasting lavaScript = lava.GetComponent<LavaCasting>();
        if (lavaScript != null)
        {
            lavaScript.SetTarget(lockSkillPos);
        }

        container.CastSkill.RaiseEvent();
        SoundManager.Instance.PlaySound(GameSound.LavaCast);
    }
    #endregion

    #region Multicast Skill
    private void Multicast()
    {
        if (skillPressedIndex != -1)
        {
            if (controller.skillLastUsed[skillPressedIndex] != 0 && (Time.time - controller.skillLastUsed[skillPressedIndex] < 0.2f) && multiCastTimes < 4)
            {
                switch (skillPrefabUsingIndex)
                {
                    case 0:
                        float random0 = Random.value;
                        Vector3 targetPos = lockSkillPos;
                        while (random0 <= container.SkillPrefabs[0].GetComponent<SkillStats>().MulticastRate && multiCastTimes < 4)
                        {
                            multiCastTimes++;
                            Vector2 randomNewTargetMeteor = Random.insideUnitCircle * container.SkillPrefabs[0].GetComponent<SkillStats>().Range;
                            Vector3 newTargetPos = targetPos + new Vector3(randomNewTargetMeteor.x, transform.position.y, randomNewTargetMeteor.y);
                            Debug.Log("Multicast x" + multiCastTimes);
                            HandleMeteorSkill(newTargetPos);
                            container.UseMulticast.RaiseEvent();
                            random0 = Random.value;
                        }
                        break;
                    case 1:
                        if (currentSkillToMulti != null)
                        {
                            float random1 = Random.value;
                            while (random1 <= container.SkillPrefabs[1].GetComponent<SkillStats>().MulticastRate && multiCastTimes < 4)
                            {
                                multiCastTimes++;
                                Vector3 baseScale = currentSkillToMulti.transform.localScale;
                                currentSkillToMulti.transform.localScale = new Vector3(baseScale.x + 3, baseScale.y, baseScale.z + 3);
                                Debug.Log("Multicast x" + multiCastTimes);
                                container.UseMulticast.RaiseEvent();
                                random1 = Random.value;
                            }
                        }
                        break;
                    case 2:
                        float random2 = Random.value;
                        while (random2 <= container.SkillPrefabs[2].GetComponent<SkillStats>().MulticastRate && multiCastTimes < 4)
                        {
                            multiCastTimes++;
                            Vector3 newTargetPos = new Vector3(Random.value, transform.position.y, Random.value).normalized;
                            HandleLazerBeamSkill(newTargetPos);
                            container.UseMulticast.RaiseEvent();
                            random2 = Random.value;
                        }
                        break;
                    case 3:
                        float random3 = Random.value;
                        while (random3 <= container.SkillPrefabs[3].GetComponent<SkillStats>().MulticastRate && multiCastTimes < 4)
                        {
                            multiCastTimes++;
                            int numberMore = 2;
                            HandleStarFallSkill(numberMore);
                            container.UseMulticast.RaiseEvent();
                            random3 = Random.value;
                        }
                        break;
                    case 4:
                        float random4 = Random.value;
                        while (random4 <= container.SkillPrefabs[4].GetComponent<SkillStats>().MulticastRate && multiCastTimes < 4)
                        {
                            multiCastTimes++;
                            Vector3 targetPos1 = lockSkillPos;
                            Vector2 randomNewTargetShade = Random.insideUnitCircle * container.SkillPrefabs[0].GetComponent<SkillStats>().Range;
                            Vector3 newTargetPos = targetPos1 + new Vector3(randomNewTargetShade.x, transform.position.y, randomNewTargetShade.y);
                            HandleShadeSkill(newTargetPos);
                            container.UseMulticast.RaiseEvent();
                            random4 = Random.value;
                        }
                        break;
                    case 5:
                        if (currentSkillToMulti != null)
                        {
                            float random1 = Random.value;
                            while (random1 <= container.SkillPrefabs[5].GetComponent<SkillStats>().MulticastRate && multiCastTimes < 4)
                            {
                                multiCastTimes++;
                                Vector3 baseScale = currentSkillToMulti.transform.localScale;
                                currentSkillToMulti.transform.localScale = new Vector3(baseScale.x + 3, baseScale.y, baseScale.z + 3);
                                Debug.Log("Multicast x" + multiCastTimes);
                                container.UseMulticast.RaiseEvent();
                                random1 = Random.value;
                            }
                        }
                        break;
                }
            }
        }
    }
    #endregion

    public bool isCurrentPreviewAvai()
    {
        if (currentPreview == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private Vector3 GetMousePoint()
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
