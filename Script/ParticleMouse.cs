using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMouse : MonoBehaviour
{

    private ParticleSystem partSys;

    private void Start()
    {
        partSys = this.GetComponent<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        this.transform.position = GameManager.instace.GetMousePositionAtWorld();
    }

    public void SetColor(Color newColor)
    {
        var mainModelParticleSystem = partSys.main;
        mainModelParticleSystem.startColor = newColor;
    }

}
