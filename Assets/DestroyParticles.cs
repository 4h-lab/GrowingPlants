using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticles : MonoBehaviour
{
    private enum Direction
    {
        above,
        left,
        right,
        below
    };

    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private Direction DestroyIf;
    [Tooltip("How much beyond (before if negative) the limit the particles are destroyed")]
    [SerializeField] private float offset = 0.5f;
    [Header("alternative non-water limit")]
    [SerializeField] private GameObject customLimit;

    private ParticleSystem.Particle[] particles;
    private GameObject limit;

    public delegate bool CheckLimit(ParticleSystem.Particle p);
    private CheckLimit checkLimit;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
        if (customLimit) limit = customLimit;
        else limit = GameObject.FindGameObjectWithTag("Water");
        AssignCheckLimit();
    }

    // Update is called once per frame
    void Update()
    {
        int particlesNum = particleSystem.GetParticles(particles);
        for (int i = 0; i < particlesNum; i++)
            if (checkLimit(particles[i]) && particles[i].remainingLifetime > 0.1f)
                particles[i].remainingLifetime = 0.1f;
        particleSystem.SetParticles(particles, particlesNum);
    }

    private void AssignCheckLimit()
    {
        switch (DestroyIf)
        {
            case Direction.above:
                checkLimit = AboveLimit;
                break;
            case Direction.left:
                checkLimit = LeftOfLimit;
                break;
            case Direction.right:
                checkLimit = RightOfLimit;
                break;
            case Direction.below:
                checkLimit = BelowLimit;
                break;
        }
    }

    private bool AboveLimit(ParticleSystem.Particle p) { return p.position.y >= limit.transform.position.y + offset; }

    private bool LeftOfLimit(ParticleSystem.Particle p) { return p.position.x <= limit.transform.position.x + offset; }

    private bool RightOfLimit(ParticleSystem.Particle p) { return p.position.x >= limit.transform.position.x - offset; }

    private bool BelowLimit(ParticleSystem.Particle p) { return p.position.y <= limit.transform.position.y - offset; }

}
