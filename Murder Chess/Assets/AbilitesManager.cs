using System.Collections.Generic;
using UnityEngine;

public class AbilitesManager : MonoBehaviour
{ 
    Player player;
    public List<BaseAbility> myAbilites = new List<BaseAbility>();
    float oldAbilityAmount;
    int maxAbilites = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GlobalVars.player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (oldAbilityAmount != transform.childCount)
        {
            oldAbilityAmount = transform.childCount;
            myAbilites.Clear();

            int i = 0;
            foreach (Transform child in transform)
            {
                BaseAbility ability;
                if (child.TryGetComponent<BaseAbility>(out ability))
                {
                    i++;
                    if (i > maxAbilites) GameObject.Destroy(ability.gameObject);
                    else
                    {
                        myAbilites.Add(ability);
                        switch (i)
                        {
                            case 1:
                                player.Ability1 = ability;
                                break;
                            case 2:
                                player.Ability2 = ability;
                                break;
                            case 3:
                                player.Ability3 = ability;
                                break;
                            case 4:
                                player.Ability4 = ability;
                                break;
                            case 5:
                                player.Ability5 = ability;
                                break;
                            default:
                                // code block
                                break;
                        }
                    }
                }
            }
        }
    }
}
