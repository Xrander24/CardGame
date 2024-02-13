using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectCreator : MonoBehaviour
{
    [SerializeField]private GameObject status_effect;
    public StatusEffect AddEffect(string effect,int turns){
        GameObject new_status_effect = Instantiate(status_effect,Vector3.zero,Quaternion.identity,transform);
        new_status_effect.SetActive(true);
        new_status_effect.name=effect;
        new_status_effect.GetComponent<StatusEffect>().setEffect(effect,turns);
        return new_status_effect.GetComponent<StatusEffect>();
    }
}
