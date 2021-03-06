﻿using UnityEngine;
using System.Collections;

public class magicProjectile : MonoBehaviour, ISkill {

    [SerializeField]
    private GameObject magicCube;
    [SerializeField]
    private float cooldown = 2f;
    [SerializeField]
    private float manaCost = 100;
    [SerializeField]
    private float scaling = 50;
    [SerializeField]
    private float baseDamg = 30;
    [SerializeField]
    private float range = 10;
    [SerializeField]
    private float speed= 30;
    private bool hasShot;

    public void shoot(Vector3 target ,float magicPen, float cooldownReduction, float mana,float apOrAd)
    {
        StartCoroutine(useCooldown(target, magicPen, cooldownReduction, mana, apOrAd));
    }
    IEnumerator useCooldown(Vector3 target, float magicPen, float cooldownReduction, float mana, float apOrAd)
    {
        if (!hasShot && mana >= manaCost)
        {
            IDamageable casterMana = GetComponent<IDamageable>();
            casterMana.changeStat(-manaCost, 14);
            hasShot = true;
            transform.LookAt(target);
            Vector3 point = target;
            gameObject.skillShotProjectile(magicCube, point, baseDamg, magicPen, false, scaling, apOrAd, range, speed);
            Instantiate(magicCube, new Vector3(transform.position.x,1f, transform.position.z), Quaternion.identity);
            yield return new WaitForSeconds(cooldown - ((cooldown/100)*cooldownReduction));
            hasShot = false;
        }
    }
}
