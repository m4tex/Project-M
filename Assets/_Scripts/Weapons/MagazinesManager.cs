using System;
using TEST_ZONE;
using UnityEngine;

public class MagazinesManager : MonoBehaviour
{
    [Header("Magazines' Variables")]
    public GameObject bulletTemplatePrefab;
    public Magazine[] Magazines;
    public int currentMagazine;

    private void Start()
    {
        Magazines = new Magazine[1];
        Magazines[0] = new Magazine(8, ParabolicBullet.BulletType.BasicParabolic);
    }

    public void OnMagMenu()
    {
        UIManager.ins.ShowMagDisplay(Magazines);
    }

    public void OnHideMagMenu()
    {
        UIManager.ins.HideMagDisplay();
    }

    public ParabolicBullet.BulletType NextBullet()
    {
        return Magazines[currentMagazine].NextBulletType();
    }
}