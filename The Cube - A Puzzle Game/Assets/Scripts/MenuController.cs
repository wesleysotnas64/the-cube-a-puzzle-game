using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject fadeFXGameObject;
    [SerializeField] private FadeFX fadeFX;


    void Start()
    {
        fadeFXGameObject = GameObject.Find("FadeFX_Image");
        fadeFX = fadeFXGameObject.GetComponent<FadeFX>();
        StartCoroutine(StartMenuEnum());
    }

    private IEnumerator StartMenuEnum()
    {
        fadeFX.TriggerFadeIn();
        yield return new WaitForSeconds(2.2f);
        fadeFXGameObject.SetActive(false);

    }

    public void InitPlay()
    {
        StartCoroutine(InitPlayEnum());
    }

    private IEnumerator InitPlayEnum()
    {
        GetComponent<AudioSource>().Play();
        fadeFXGameObject.SetActive(true);

        // Aqui faz a verificação se o jogo já foi iniciado antes
        // usar PlayerPrefs.GetBoll("FirstPlay");
        // Se sim vai direto pra gameplay
        // Caso contrário abre o tutorial

        //desabilitar o botão para não ser clicado novamente;
        fadeFX.TriggerFadeOut();
        yield return new WaitForSeconds(2.2f);

        SceneManager.LoadScene("Gameplay");

    }
}
