﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TowerDefence.Utils;

public class TowerBuyButton : MonoBehaviour {

    public buttonInformation setup(buttonInformation data)
    {
        Image i;
        Button b;
        Text t;
        buttonInformation outputData = data;

        i = transform.FindChild("icon").GetComponentInChildren<Image>();

        if (data.icon != null)
        {
            i.sprite = data.icon;
        }
        else
        {
            i.gameObject.SetActive(false);
        }

        t = transform.FindChild("name").GetComponentInChildren<Text>();
        t.text = data.type.ToString();

        b = GetComponentInChildren<Button>();


        int tmpInt = (int)data.type;
        b.onClick.AddListener(delegate
        {
            int tow = tmpInt; TowerDefence.Managers.BuildManager.instance.PlaceTower(tow);
        });
        outputData.name = t;
        outputData.price = transform.FindChild("price").GetComponentInChildren<Text>();
        outputData.img = i;

        return outputData;
    }
}
