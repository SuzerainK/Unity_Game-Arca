using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSFX : MonoBehaviour
{
    Animator animateEffect;
    Animator animateEffectEnemy;
    AudioManager SFX;

    CameraShake CShake;

    // Start is called before the first frame update
    void Start()
    {
        CShake = GameObject.FindGameObjectWithTag("CameraParent").GetComponent<CameraShake>();
        SFX = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void SingleSlash()
    {
        SFX.Play("KatanaSlash");
        StartCoroutine(CShake.Shake(0.25f, 0.2f));
    }

    void PlayerSlash()
    {
        SFX.Play("PlayerSlash");
        StartCoroutine(CShake.Shake(0.25f, 0.2f));
    }

    void FinalKatanaSlash()
    {
        SFX.Play("FinalKatanaSlash");
        StartCoroutine(CShake.Shake(0.25f, 1f));
    }

    void MagicReload()
    {
        SFX.Play("MagicReload");
    }

    void MagicRelease()
    {
        SFX.Play("MagicRelease");
        StartCoroutine(DelayedShake());
    }

    void FallingSwords()
    {
        SFX.Play("FallingSwords");
        StartCoroutine(CShake.Shake(0.25f, 0.2f));
    }

    void SingleSlash1()
    {
        SFX.Play("KatanaSlash1");
        StartCoroutine(CShake.Shake(0.25f, 0.2f));
    }

    void SingleSlash2()
    {
        SFX.Play("KatanaSlash2");
        StartCoroutine(CShake.Shake(0.25f, 0.2f));
    }

    void FallingSwords1()
    {
        SFX.Play("FallingSwords1");
        StartCoroutine(CShake.Shake(0.25f, 0.2f));
    }

    void FallingSwords2()
    {
        SFX.Play("FallingSwords2");
        StartCoroutine(CShake.Shake(0.25f, 0.2f));
    }

    void HeraldAttack()
    {
        SFX.Play("HeraldAttack");
        StartCoroutine(CShake.Shake(0.25f, 0.2f));
    }

    void SelfHeal()
    {
        SFX.Play("Heal");
    }

    void PlayerBlock()
    {
        SFX.Play("PlayerBlock");
    }

    void HeraldBlock()
    {
        SFX.Play("HeraldBlock");
    }

    void RageAttackEffect()
    {
        SFX.Play("RageAttack");
        StartCoroutine(CShake.Shake(0.25f, 1f));
    }

    void ShieldStartEffect()
    {
        SFX.Play("ShieldStart");
    }

    void ShieldBreakEffect()
    {
        SFX.Play("ShieldBreak");
    }

    void GunFire()
    {
        SFX.Play("Gunfire");
    }

    void GunFire2()
    {
        SFX.Play("Gunfire2");
    }

    void JustCameraShake()
    {
        StartCoroutine(CShake.Shake(0.25f, 0.2f));
    }

    void DaggerAttackEffect()
    {
        SFX.Play("DaggerAttack");
        StartCoroutine(CShake.Shake(0.25f, 0.2f));
    }

    void ParryEffect()
    {
        SFX.Play("Parry");
    }

    void GunPrepareEffect()
    {
        SFX.Play("GunPrepare");
    }

    void ReloadEffect()
    {
        SFX.Play("Reload");
    }

    void GroundBreakEffect()
    {
        SFX.Play("GroundBreak");
        StartCoroutine(CShake.Shake(0.25f, 0.5f));
    }

    void PowerUpEffect()
    {
        SFX.Play("PowerUp");
        StartCoroutine(CShake.Shake(0.25f, 0.2f));
    }

    void FirstShootEffect()
    {
        SFX.Play("FirstShoot");
        StartCoroutine(CShake.Shake(0.25f, 0.2f));
    }

    void SecondShootEffect()
    {
        SFX.Play("SecondShoot");
        StartCoroutine(CShake.Shake(0.25f, 0.2f));
    }

    void ThirdShootEffect()
    {
        SFX.Play("ThirdShoot");
        StartCoroutine(CShake.Shake(0.25f, 0.2f));
    }

    void ParrySlashEffect()
    {
        SFX.Play("ParrySlash");
    }

    /**void PlayerReceiveHit()
    {
        animateEffect.Play("CombatReceiveHit");
    }

    void EnemyReceiveHit()
    {
        animateEffectEnemy.Play("ReceiveHit");
    }

    void PlayerReceiveHit()
    {
        animateEffect.Play("CombatReceiveHit");
    }**/

    IEnumerator DelayedShake()
    {

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(CShake.Shake(0.25f, 0.2f));
    }
}
