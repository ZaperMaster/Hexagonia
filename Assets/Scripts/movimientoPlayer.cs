using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoPlayer : MonoBehaviour
{
    private bool jumping; // Booleano por saber si está saltando
    private Rigidbody rb; // Referencia al componente Ridigbody
    public bool movimientoAxis; // Si utilizamos movimiento por axis o teclas
    public float speed;
    public enum tipoFuerza
    {
        fuerzaCoordenasasAbsolutas, fuerzaCoordenadasRelativas, fuerzaTorsionCoordenadasAbsolutas,
        fuerzaTorsionCoordenadasRelativas, fuerzaEnPosicion
    }

    // Enumeración para establecer los modos de fuerza
    public enum modoFuerza
    {
        Force, Acceleration, Impulse, VelocityChange
    }

    public tipoFuerza fuerza;
    public modoFuerza fuerzaSalto;

    void Start()
    {
        // Guardamos en rb el componente Rigidbody del objeto
        rb = GetComponent<Rigidbody>();
        jumping = false;
        movimientoAxis = false;
        speed = 20.0f;
        fuerza = tipoFuerza.fuerzaCoordenasasAbsolutas; // Para aplicar por defecto AddForce
        fuerzaSalto = modoFuerza.Force;
    }


    // Detección de colisiones
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Superficie"))
        {
            jumping = false;
        }
    }

    void OnTriggerEnter (Collider other){
            if (other.name=="Destructor"){
                Destroy(gameObject);
            }
    }

    void FixedUpdate()
    {

        Vector3 vectorMovimiento = Vector3.zero;

        if (!movimientoAxis)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                //Movimiento a la izquierda
                if (rb != null)
                {
                    // vectorMovimiento = new Vector3(-1.0f, 0.0f, 0.0f);
                    // Ponte a prueba5
                    Vector3 cameraDir=Camera.main.transform.TransformDirection (-1.0f, 0, 0);
                    vectorMovimiento = new Vector3 (cameraDir.x, 0 , cameraDir.z);


                }
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                //Movimiento a la derecha
                if (rb != null)
                {
                    //vectorMovimiento = new Vector3(1.0f, 0.0f, 0.0f);
                    Vector3 cameraDir=Camera.main.transform.TransformDirection (1.0f, 0, 0);
                    vectorMovimiento = new Vector3 (cameraDir.x, 0 , cameraDir.z);
                }
            }

            // Movimiento adelante
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                if (rb != null)
                {
                    // vectorMovimiento = new Vector3(0.0f, 00.0f, 1.0f);
                    Vector3 cameraDir=Camera.main.transform.TransformDirection (0, 0, 1);
                    vectorMovimiento = new Vector3 (cameraDir.x, 0 , cameraDir.z);
                }
            }

            // Movimiento hacia atrás
            if (Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.DownArrow))
            {
                if (rb != null)
                {
                    // vectorMovimiento = new Vector3(0.0f, 0.0f, -1.0f);
                    Vector3 cameraDir=Camera.main.transform.TransformDirection (0,0,-1);
                    vectorMovimiento = new Vector3 (cameraDir.x, 0 , cameraDir.z);
                }

            }
        } // Fin if MovimientoAxis
        else
        {   // Ejemplo basado en el Roll-a-ball

            // Valores de los ejes horizontal y vertical
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            // Vector con los componentes horizontal y vertical, para moverse en 
            // los ejex X y Z.
            vectorMovimiento = new Vector3(moveHorizontal, 0.0f, moveVertical);
        }

        // Aplicación del movimiento

        // Se añade la fuerza sobre el RigidBody, multiplicando la dirección por
        // la velocidad. Esta velocidad se puede modificar desde el Inspector.

        // Discriminamos por el tipo de fuerza aplicada:

        switch (fuerza)
        {
            case tipoFuerza.fuerzaCoordenasasAbsolutas:
                // Aplicación de fuerza estándard
                rb.AddForce(vectorMovimiento * speed);
                break;
            case tipoFuerza.fuerzaCoordenadasRelativas:
                // Aplicación de fuerza estándard relativa al objeto
                rb.AddRelativeForce(vectorMovimiento * speed);
                break;
            case tipoFuerza.fuerzaTorsionCoordenadasAbsolutas:
                // Aplicación de fuerza de torsión
                rb.AddTorque(vectorMovimiento * speed);
                break;
            case tipoFuerza.fuerzaTorsionCoordenadasRelativas:
                // Aplicación de fuerza de torsión relativa al objeto
                rb.AddRelativeTorque(vectorMovimiento * speed);
                break;
            case tipoFuerza.fuerzaEnPosicion:
                // Aplicación de fuerza a una posición
                rb.AddForceAtPosition(vectorMovimiento * speed, new Vector3(0, 0, 0));
                break;
        }


        // Gestión del salto

        if (Input.GetKeyDown(KeyCode.Z) && jumping == false)
        {
            jumping = true;
            if (rb != null)
            {
                switch (fuerzaSalto)
                {
                    case modoFuerza.Force:
                        rb.AddForce(new Vector3(0.0f, 300.0f, 0.0f), ForceMode.Force);
                        break;
                    case modoFuerza.Acceleration:
                        rb.AddForce(new Vector3(0.0f, 300.0f, 0.0f), ForceMode.Acceleration);
                        break;
                    case modoFuerza.Impulse: 
                        rb.AddForce(new Vector3(0.0f, 6.0f, 0.0f), ForceMode.Impulse);
                        break;
                    case modoFuerza.VelocityChange:
                        rb.AddForce(new Vector3(0.0f, 6.0f, 0.0f), ForceMode.VelocityChange);
                        break;
                }

            }

        }
    }
}
