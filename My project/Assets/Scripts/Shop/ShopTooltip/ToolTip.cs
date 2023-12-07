using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ToolTip : MonoBehaviour
{
    public TextMeshProUGUI headerField;

    public TextMeshProUGUI chanceField;

    public TextMeshProUGUI contentField;

    public TextMeshProUGUI damageField;

    public TextMeshProUGUI RadiusField;

    public TextMeshProUGUI cooldownField;

    public TextMeshProUGUI durationField;

    public TextMeshProUGUI EffectvieText;

    public TextMeshProUGUI InstaceText;

    public LayoutElement layoutElement;

    public int characterWrapLimit;

    public RectTransform rectTransform;

    //const string for text
    private const string PREFIX_LEVEL = "Lv.";
    private const string PREFIX_TITLE = "Upgrade ";
    private const string PREFIX_COOLDOWN = "Cooldown: ";
    private const string PREFIX_RADIUS = "Radius: ";
    private const string PREFIX_DAMAGE = "Damage: ";
    private const string PREFIX_DURATION = "Duration: ";
    private const string PREFIX_CHANCE = "Chance: ";

    //const string for compare
    private const string SKILL_ACID = "Boiling Acid";
    private const string SKILL_LAVA = "Lava Pool";
    private const string SKILL_LAZER = "Arcane Beam";
    private const string SKILL_MEOTEOR = "Meteor";
    private const string SKILL_SHADE = "Shade";
    private const string SKILL_STARFALL = "Ghostflame";


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (Application.isEditor)
        {
            int headerLength = headerField.text.Length;
            int contentLength = contentField.text.Length;

            layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit ) ? true : false;
        }

        Vector2 position = Input.mousePosition;

        transform.position = position;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;
    }

    public void SetText(DataContainer dataContainer)
    {
        ItemDataSO itemdata = dataContainer.Get();
        
        if (itemdata.ID_Skill == 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }

        headerField.text = itemdata.is_upgraded ? PREFIX_TITLE + itemdata.skill_name.Trim() + "\n"+ PREFIX_LEVEL + itemdata.level_skill + "+" : "Buy " + itemdata.skill_name;
        contentField.text = itemdata.description;
        handleDisplayText(itemdata);
    }

    public void SetText(SellDataContainer sellDataContainer)
    {
        ItemDataSO itemdata = sellDataContainer.Get();

        if (itemdata.ID_Skill == 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }

        headerField.text = itemdata.is_upgraded ? PREFIX_TITLE + itemdata.skill_name + PREFIX_LEVEL + itemdata.level_skill + "+" : "Buy " + itemdata.skill_name;
        contentField.text = itemdata.description;

    }


    private void handleDisplayText(ItemDataSO data)
    {
        string name = data.skill_name.ToString();

        switch (name)
        {
            case SKILL_ACID:
                chanceField.text = PREFIX_CHANCE + data.multi_cast_chance + "%";
                damageField.text = PREFIX_DAMAGE + data.damage +"/s " + "( + " + data.damage_increase + " /lv)";
                RadiusField.text = PREFIX_RADIUS + data.radius;
                cooldownField.text = PREFIX_COOLDOWN + data.cooldown + "( - " + data.cooldown_decrease + "s /lv)";
                durationField.text = PREFIX_DURATION + data.debuff_duration +"s" + " ( + " + data.debuff_duration_increase +"s /lv)";
                EffectvieText.gameObject.SetActive(false);
                InstaceText.gameObject.SetActive(false);
                break;
            case SKILL_LAVA:
                InstaceText.gameObject.SetActive(false);
                chanceField.text = PREFIX_CHANCE + data.multi_cast_chance + "%";
                damageField.text = PREFIX_DAMAGE + data.damage + "/s " + "( + " + data.damage_increase + " /lv)";
                RadiusField.text = PREFIX_RADIUS + data.radius;
                cooldownField.text = PREFIX_COOLDOWN + data.cooldown + "( - " + data.cooldown_decrease + "s /lv)";
                durationField.text = PREFIX_DURATION + data.debuff_duration + "s" + " ( + " + data.debuff_duration_increase + "s /lv)";
                float debuff = data.debuff_effective;
                float debuff_increase = data.debuff_effective_increase;
                EffectvieText.text = "Slow: " + (debuff*100).ToString() +"%"+ "( +"+(debuff_increase*100).ToString()+"%/lv)";
                break;
            case SKILL_LAZER:
                chanceField.text = PREFIX_CHANCE + data.multi_cast_chance + "%";
                damageField.text = PREFIX_DAMAGE + data.damage + "( + " + data.damage_increase + " /lv)";
                RadiusField.gameObject.SetActive(false);
                cooldownField.text = PREFIX_COOLDOWN + data.cooldown + "( - " + data.cooldown_decrease + "s /lv)";
                durationField.gameObject.SetActive(false);
                EffectvieText.gameObject.SetActive(false);
                InstaceText.gameObject.SetActive(false);
                break;
            case SKILL_MEOTEOR:
                chanceField.text = PREFIX_CHANCE + data.multi_cast_chance + "%";
                damageField.text = PREFIX_DAMAGE + data.damage + "( + " + data.damage_increase + " /lv)";
                RadiusField.text = PREFIX_RADIUS + data.radius;
                cooldownField.text = PREFIX_COOLDOWN + data.cooldown + "( - " + data.cooldown_decrease + "s /lv)";
                durationField.gameObject.SetActive(false);
                EffectvieText.gameObject.SetActive(false);
                InstaceText.gameObject.SetActive(false);
                break;
            case SKILL_SHADE:
                chanceField.text = PREFIX_CHANCE + data.multi_cast_chance + "%";
                damageField.text = PREFIX_DAMAGE + data.damage + "( + " + data.damage_increase + " /lv)";
                RadiusField.text = PREFIX_RADIUS + data.radius;
                cooldownField.text = PREFIX_COOLDOWN + data.cooldown + "( - " + data.cooldown_decrease + "s /lv)";
                InstaceText.text = "Demon Spawn: " + data.instance_per_cast;
                durationField.gameObject.SetActive(false);
                EffectvieText.gameObject.SetActive(false);
                break;
            case SKILL_STARFALL:
                chanceField.text = PREFIX_CHANCE + data.multi_cast_chance + "%";
                damageField.text = PREFIX_DAMAGE + data.damage + "( + " + data.damage_increase + " /lv)";
                RadiusField.text = PREFIX_RADIUS + data.radius;
                cooldownField.text = PREFIX_COOLDOWN + data.cooldown + "( - " + data.cooldown_decrease + "s /lv)";
                InstaceText.text = "Ghost Spawn: " + data.instance_per_cast + "( + " + data.instance_increase +" /lv)";
                durationField.gameObject.SetActive(false);
                EffectvieText.gameObject.SetActive(false);
                break;

        }
    }
}