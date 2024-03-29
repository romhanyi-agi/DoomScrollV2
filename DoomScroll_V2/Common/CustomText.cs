﻿using TMPro;
using UnityEngine;

namespace DoomScroll_V2.Common
{
    public class CustomText
    {
        public GameObject TextObject { get; }
        public TextMeshPro TextMP { get; }

        public CustomText(string name, GameObject parent, string text)
        {
            TextObject = new GameObject();
            TextObject.layer = LayerMask.NameToLayer("UI");
            TextObject.name = name;
            TextObject.transform.SetParent(parent.transform);
            TextObject.AddComponent<MeshRenderer>();
            TextObject.transform.localScale = Vector3.one;
            TextMP = TextObject.AddComponent<TextMeshPro>();
            TextMP.text = text;
            TextMP.m_enableWordWrapping = true;
            TextMP.alignment = TextAlignmentOptions.Center;
            TextMP.color = Color.black;
        }

        public void SetColor(Color col)
        {
            TextMP.color = col;
        }

        public void SetlocalPosition(Vector3 pos)
        {
            TextObject.transform.localPosition = pos;
        }

        public void SetText(string text)
        {
            TextMP.text = text;
        }

        public void SetSize(float size)
        {
            TextMP.fontSize = size;
        }

        public void DisplayLabel(bool value)
        {
            TextObject.SetActive(value);
        }
    }
}
