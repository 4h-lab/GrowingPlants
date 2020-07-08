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

    [Tooltip("Particle system to affect. Get one from this component object if none is specified.")]
    [SerializeField] private ParticleSystem particleSystem;
    [Tooltip("Particles are destroyed when they're in a certain position relative to the limit object (Water by default)")]
    [SerializeField] private Direction DestroyIf;
    [Tooltip("Sets remainingLifetime to this value when the limit is reached. Death subemitters won't work if this is set to 0.")]
    [SerializeField] private float setLifetimeTo = 0f;
    [Tooltip("How much beyond (before if negative) the limit the particles are destroyed.")]
    [SerializeField] private float offset = 0f;
    [Header("Alternative non-water limit")]
    [Tooltip("Use a custom object transform as limit. Water is used if none is specified.")]
    [SerializeField] private GameObject customLimit;

    private ParticleSystem.Particle[] particles;
    private GameObject limit;

    public delegate bool CheckLimit(ParticleSystem.Particle p);
    private CheckLimit checkLimit;

    // Start is called before the first frame update
    void Start()
    {
        if (!particleSystem) particleSystem = GetComponent<ParticleSystem>();
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
            if (checkLimit(particles[i]) && particles[i].remainingLifetime > setLifetimeTo)
                particles[i].remainingLifetime = setLifetimeTo;
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
