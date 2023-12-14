using UnityEngine;

public class ControleDeLuz : MonoBehaviour
{
    public Transform personagem;
    public Light luz;

    public float distanciaAtivacao = 5f;

    void Update()
    {
        // Calcula a distância entre o personagem e a luz
        float distancia = Vector3.Distance(personagem.position, transform.position);

        // Se o personagem estiver perto o suficiente, ativa a luz
        if (distancia <= distanciaAtivacao)
        {
            luz.enabled = true;
        }
        else
        {
            luz.enabled = false;
        }
    }
}
