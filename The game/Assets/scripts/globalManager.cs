using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class globalManager : MonoBehaviour
{

    private static globalManager _instance;
    public static globalManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject GM = new GameObject("globalManager");
                GM.AddComponent<globalManager>();
            }

            return _instance;
        }
    }
    public GameObject inv;
    public int bananas = 100;
    // power up var
    public int defenceBuffs = 0;
    public float dmgBuffs = 0;
    public float bltSpeedBuffs = 0;
    public float runSpeedBuffs = 0;
    public float reloadBuffs = 0;
    public bool[] aquiredItems = new bool[6];
    public GameObject bananaSlot;
    public GameObject defenceSlot;
    public GameObject dmgSlot;
    public GameObject bltSpeedSlot;
    public GameObject runSpeedSlot;
    public GameObject reloadSlot;
    public TextMeshProUGUI bananas_UI;
    public TextMeshProUGUI defence_UI;
    public TextMeshProUGUI dmg_UI;
    public TextMeshProUGUI runSpeed_UI;
    public TextMeshProUGUI bltSpeed_UI;
    public TextMeshProUGUI reload_UI;
    void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        // show/hide slots
        inventoryLength();
    }

    // Update is called once per frame
    void Update()
    {
        bananas_UI.text = bananas.ToString();
        defence_UI.text = defenceBuffs.ToString();
        dmg_UI.text = dmgBuffs.ToString();
        runSpeed_UI.text = runSpeedBuffs.ToString();
        bltSpeed_UI.text = bltSpeedBuffs.ToString();
        reload_UI.text = reloadBuffs.ToString();
    }

    public void inventoryLength()
    {
        var counter = 0;
        for (int i = 0; i < aquiredItems.Length; i++) // 6 slots we have, the length of the inventory is 600
        {
            if (aquiredItems[i])
            {
                if (i == 0)
                    bananaSlot.SetActive(true);
                if (i == 1)
                    defenceSlot.SetActive(true);
                if (i == 2)
                    bltSpeedSlot.SetActive(true);
                if (i == 3)
                    dmgSlot.SetActive(true);
                if (i == 4)
                    reloadSlot.SetActive(true);
                if (i == 5)
                    runSpeedSlot.SetActive(true);
                counter++;
            }
        }
        if (counter == 0)
        {
            inv.gameObject.SetActive(false);
        }
        else
        {
            inv.gameObject.SetActive(true);
            inv.GetComponent<RectTransform>().sizeDelta = new Vector2(counter * 100, 100);
        }
    }
}
