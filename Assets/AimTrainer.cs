using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTrainer : MonoBehaviour
{
    [SerializeField] List<GameObject> _targets;

    private void Start()
    {
        PickRandomTarget();
    }

    public void PickRandomTarget()
    {
        foreach (GameObject target in _targets)
        {
            target.SetActive(false);
        }
        //Pick random target and active 
        var randomIndex = Random.Range(0, _targets.Count);
        _targets[randomIndex].SetActive(true);
    }


}
