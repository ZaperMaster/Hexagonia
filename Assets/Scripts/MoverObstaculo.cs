using UnityEngine;

public class MoverObstaculo : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 0.5f;       // Velocidad del movimiento
    public float distance = 2f;    // Hasta dónde se mueve abajo (desde y=0) - ¡Sube este valor para bajar más!
    
    // Variables internas
    private float randomPhase;     // Fase aleatoria
    private bool startFromTop;     // Determina si empieza desde arriba (0) o desde abajo (-distance)

    void Start()
    {
        distance = 2.3f;
        speed = 0.5f;
        // Generamos una fase aleatoria para que cada obstáculo empiece en un punto distinto del ciclo
        randomPhase = Random.Range(0f, 5f);
        
        // Elegimos aleatoriamente si empieza "arriba" o "abajo"
        // Si es true, inicia en y=0 y baja. Si es false, inicia en y=-distance y sube.
        startFromTop = (Random.value > 0.5f);
    }

    void Update()
    {
        // offset oscila entre 0 y 'distance' con PingPong
        float offset = Mathf.PingPong((Time.time + randomPhase) * speed, distance);

        float newY;
        if (startFromTop)
        {
            // Empieza en y=0 y va hasta y=-distance
            newY = 0 - offset; 
        }
        else
        {
            // Empieza en y=-distance y va hacia y=0
            // Básicamente: -distance + offset => de -distance (cuando offset=0) a 0 (cuando offset=distance)
            newY = -distance + offset;
        }
        
        // Aplicamos la nueva posición
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
