using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] AudioSource explosionSource;
    [SerializeField] ParticleSystem BurnParticles;
    [SerializeField] ParticleSystem ExplosionParticles;

    [Header("Otros")]
    [SerializeField] int explosionDelay = 2;

    public void ExplodeBomb()
    {
        Invoke(nameof(Explode), explosionDelay);
        BurnParticles.Play();
    }

    private void Explode()
    {
        explosionSource.Play();
        ExplosionParticles.Play();
        BurnParticles.Stop();
    }
}
