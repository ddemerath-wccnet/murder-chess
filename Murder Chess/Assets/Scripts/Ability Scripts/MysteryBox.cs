using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MysteryBox : BaseAbility
{
    public List<BaseAbility> abilities = new List<BaseAbility>();
    public float maxAbilityLengthTimer = 0.5f;
    public float abilityLengthTimer;
    public GameObject attackArea;
    private int numberOfAbilities;
    public BaseAbility abilityCalled;
    private Vector3 playerPosition;
    // protected override void AbilityStart()
    // {
    //     abilityLengthTimer = maxAbilityLengthTimer;
    //     attackArea = transform.GetChild(0).gameObject;
    // }

    // protected override bool AbilityUpdate()
    // {
    //     if (abilityLengthTimer > 0)
    //     {
    //         attackArea.SetActive(true);

    //         abilityLengthTimer -= GlobalVars.DeltaTimePlayer;
    //         GlobalVars.player.GetComponent<Player>().isDangerous = true;
    //         return false;
    //     }
    //     else
    //     {
    //         attackArea.SetActive(false);

    //         GlobalVars.player.GetComponent<Player>().isDangerous = false;
    //         return true;
    //     }
    // }

    void Start() {

        BaseAbility[] abilityItemsTemp = Resources.LoadAll<BaseAbility>("Prefabs/Shop/Abilites");
        abilities = abilityItemsTemp.ToList();
        numberOfAbilities = abilities.Count();
        abilities.Remove(Resources.LoadAll<MysteryBox>("Prefabs/Shop/Abilites")[0]);
        abilities.Remove(Resources.LoadAll<DodgeAbility>("Prefabs/Shop/Abilites")[0]);
        abilities.Remove(Resources.LoadAll<MeleeAttack>("Prefabs/Shop/Abilites")[0]);
        
    }

    protected override void AbilityStart()
    {
        Debug.Log("ability start");
        playerPosition = GlobalVars.player.transform.position;
        abilityCalled = Instantiate(abilities[Random.Range(0 , (numberOfAbilities + 1))], playerPosition, transform.rotation, transform);
        //BaseSpell baseSpellScript = spellCalled.GetComponent<BaseSpell>();
        //abilityCalled.SpellCost = 0;
        abilityCalled.CallActivate();
        abilityLengthTimer = maxAbilityLengthTimer;
        //instanceSpellTimer = spellTimer;
        //baseSpellScript.SpellStart();
    }

    protected override bool AbilityUpdate()
    {
        abilityLengthTimer -= GlobalVars.DeltaTimePlayer;

        if (abilityLengthTimer < 0)
        {
            GameObject.Destroy(abilityCalled.gameObject);

            return true;
        }
        else return false;
    }

}
