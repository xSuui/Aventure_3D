using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundControl : MonoBehaviour
{
    public Text buttonText; // Refer�ncia ao texto no bot�o
    public AudioSource audioSource; // Refer�ncia ao componente de �udio

    private bool isSoundOn = true;

    void Start()
    {
        // Configurar o texto inicial do bot�o
        UpdateButtonText();

        // Certificar-se de que o �udio est� ligado no in�cio
        audioSource.Play();
    }

    // M�todo chamado quando o bot�o � clicado
    public void ToggleSound()
    {
        // Inverter o estado do som
        isSoundOn = !isSoundOn;

        // Atualizar o texto do bot�o
        UpdateButtonText();

        // Ligar ou desligar o �udio conforme o estado
        if (isSoundOn)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }

    // Atualizar o texto do bot�o com base no estado do som
    void UpdateButtonText()
    {
        buttonText.text = "Som: " + (isSoundOn ? "Ligado" : "Desligado");
    }
}
