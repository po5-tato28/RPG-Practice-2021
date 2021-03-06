using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCombat : Combat
{
    private static PlayerCombat instance = null;
    public static PlayerCombat Instance()
    {
        if (instance == null)
        {
            Debug.LogError("해당 오브젝트가 없음");
        }
        return instance;
    }

    PlayerController playerController;
    PlayerInput charInput;

    [SerializeField] FollowCamera cam;

    public List<Health> targets;
    int currentIndex = 0;

    public Transform center;  // 캐릭터 중심 pivot
    //[HideInInspector] public Vector3 centerHeight = new Vector3(0, 0.7f, 0); // pivot의 높이
    [SerializeField] Transform handTransform = null;


    [SerializeField] Weapon defaultWeapon = null;
    public Weapon currentWeapon = null;
    public Skill currentSkill = null;
    //float currentDamage = 0;

    //GameObject combatEffect;

    bool isSkill = false;
    bool isTriggerAttack = false;
    public bool IsTriggerAttack { get { return isTriggerAttack; } }


    protected override void Awake()
    {
        instance = this;

        base.Awake();

        charInput = GetComponent<PlayerInput>();
        playerController = GetComponent<PlayerController>();

        targets = new List<Health>();
    }

    private void Start()
    {
        EquipWeapon(defaultWeapon);
    }

    private void Update()
    {
        //if (target == null) return;
        //if (target.IsDead()) return;

        if (isSkill == true) return;

        playerController.FindTarget();
        UpdateAnimator();

        // 플래그
        if (isTriggerAttack)
        {
            animator.ResetTrigger("Hit");
        }
    }

    public void TriggerSkill(Skill skill)
    {
        currentSkill = skill;
        isSkill = true;

        playerController.FindTargetWhenUseSkill(currentSkill.AttackPivot);

        Animator animator = GetComponent<Animator>();
        skill.ChangeAnimatorFoSkill(animator);

        PlayerMp mp = GetComponent<PlayerMp>();
        mp.TakeMp(currentSkill.NeedMp);

        animator.ResetTrigger("StopSkill");
        animator.SetTrigger("Skill");
    }

    void SkillEffect()
    {
        if (targets == null) return;
        if (currentSkill == null) return;

        //Transform parent = gameObject.transform;
        Vector3 pivot = currentSkill.effectPosition + currentSkill.CalculatePivot(currentSkill.AttackPivot);

        combatEffect = Instantiate(currentSkill.effectPrefab, pivot, Quaternion.Euler(currentSkill.effectRotation));
        //combatEffect.transform.SetParent(parent, false);
    }
    void DestroySkillEffect()
    {
        animator.ResetTrigger("Skill");
        animator.SetTrigger("StopSkill");

        Destroy(combatEffect);
        isSkill = false;
    }

    public override void WeaponEffect()
    {
        if (targets == null) return;
        if (currentWeapon.EffectPrefab == null) return;

        // 이펙트를 넣어볼까        
        Transform parent = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Transform>();

        combatEffect = Instantiate(currentWeapon.EffectPrefab, currentWeapon.EffectPosition, Quaternion.Euler(currentWeapon.EffectRotation));
        combatEffect.transform.SetParent(parent, false);
    }

    public override void DestroyWeaponEffect()
    {
        animator.ResetTrigger("Skill");
        animator.SetTrigger("StopSkill");
        Destroy(combatEffect);
    }

    public override void Hit()
    {
        if (targets == null) return;
        if (isSkill == true)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].TakeDamage((int)baseStats.GetStat(StatsType.Damage));
            }
        }
        else if (isSkill == false)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].TakeDamage((int)baseStats.GetStat(StatsType.Damage));
            }
        }
    }

    void Heal()
    {
        if (targets == null) return;
        if (isSkill == true)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                //targets[i].RecoverHealth((int)currentSkill.AttackDamage);
                targets[i].RecoverHealth();
            }
        }
    }


    protected override void UpdateAnimator()
    {
        if (charInput.attack > 0f)
        {
            TriggerAttack();
        }
        else
        {
            StopAttack();
        }
    }

    public override bool GetIsInRange(Transform targetTransform)
    {
        return Vector3.Distance(transform.position, targetTransform.position) > currentWeapon.AttackRange;
    }

    public void EquipWeapon(Weapon weapon)
    {
        currentWeapon = weapon;

        Animator animator = GetComponent<Animator>();
        weapon.Spawn(handTransform, animator);
    }
    protected override void TriggerAttack()
    {
        //isTriggerAttack = true;
        animator.SetBool("Attack", true);
    }

    protected override void StopAttack()
    {
        animator.SetBool("Attack", false);
        //isTriggerAttack = false;
    }
}
