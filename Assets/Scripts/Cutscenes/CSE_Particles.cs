using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSE_Particles : CutsceneElementBase
{

    public ParticleSystem particles;

    public CutsceneHandler cutscene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Execute()
    {
        StartCoroutine(PlayParticles());
    }

    private IEnumerator PlayParticles()
    {
        particles.Play();
        yield return new WaitForSeconds(duration);
        cutscene.PlayNextElement();
    }
}
