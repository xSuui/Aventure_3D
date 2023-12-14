using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundControl : MonoBehaviour
{
    public Text buttonText; // Referência ao texto no botão
    public AudioSource audioSource; // Referência ao componente de áudio

    private bool isSoundOn = true;

    void Start()
    {
        // Configurar o texto inicial do botão
        UpdateButtonText();

        // Certificar-se de que o áudio está ligado no início
        audioSource.Play();
    }

    // Método chamado quando o botão é clicado
    public void ToggleSound()
    {
        // Inverter o estado do som
        isSoundOn = !isSoundOn;

        // Atualizar o texto do botão
        UpdateButtonText();

        // Ligar ou desligar o áudio conforme o estado
        if (isSoundOn)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }

    // Atualizar o texto do botão com base no estado do som
    void UpdateButtonText()
    {
        buttonText.text = "Som: " + (isSoundOn ? "Ligado" : "Desligado");
    }
}
