using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoalController : MonoBehaviour
{
    private AudioSource BGM;
    [Header("SFX")]
    public AudioClip buttonOnClick;
    public AudioClip SFXJawabanBenar;
    public AudioClip SFXJawabanSalah;

    [Header("Prefabs soal")]
    public GameObject[] soalPrefabs;

    public GameObject soalObject;
    public GameObject benarObject;
    public GameObject popUpUIBenar;
    public GameObject salahObject;
    public GameObject akhirGame;
    public GameObject aboutUs;

    [Header("List Button")]
    public Button buttonA;
    public Button buttonB;
    public Button buttonC;

    [Header("List TextButton")]
    public TextMeshProUGUI textA, textB, textC;

    [Header("List TextButton Soal")]
    public string[] textButtonA;
    public string[] textButtonB;
    public string[] textButtonC;

    // [Header("Container")]
    // public GameObject containerSoal;

    [Header("Button Lanjut Soal")]
    public Button JawabanBenar;
    public Button JawabanSalah;
    public Button buttonKembali;
    public Button cobaLagi;
    public Button AboutUs;
    public Button KembaliMainMenu;
    public Button ButtonKembaliAboutUs;
    public Button keluarAplikasi;

    private string[] jawabanBenar;
    private int currentSoalIndex = 0;

    private void Start()
    {
        BGM = GetComponent<AudioSource>();
        jawabanBenar = new string[] { "B", "A", "C", "A", "C" };


        // Ensure soalPrefabs and jawabanBenar have the same number of elements
        if (soalPrefabs.Length != jawabanBenar.Length ||
            soalPrefabs.Length != textButtonA.Length ||
            soalPrefabs.Length != textButtonB.Length ||
            soalPrefabs.Length != textButtonC.Length)
        {
            Debug.LogError("Number of soal prefabs and answers must match!");
            return;
        }

        buttonA.onClick.AddListener(() => { OnAnswerSelected("A"); PlaySFX(buttonOnClick); });
        buttonB.onClick.AddListener(() => { OnAnswerSelected("B"); PlaySFX(buttonOnClick); });
        buttonC.onClick.AddListener(() => { OnAnswerSelected("C"); PlaySFX(buttonOnClick); });
        buttonKembali.onClick.AddListener(() => { ButtonKembali(); PlaySFX(buttonOnClick); });
        JawabanBenar.onClick.AddListener(() => { NextSoal(); PlaySFX(buttonOnClick); });
        JawabanSalah.onClick.AddListener(() => { NextSoal(); PlaySFX(buttonOnClick); });
        cobaLagi.onClick.AddListener(() => { CobaLagi(); PlaySFX(buttonOnClick); });
        AboutUs.onClick.AddListener(() => { AboutUsPanel(); PlaySFX(buttonOnClick); });
        ButtonKembaliAboutUs.onClick.AddListener(() => { LastPagePanel(); PlaySFX(buttonOnClick); });
        keluarAplikasi.onClick.AddListener(() => { ExitGame(); PlaySFX(buttonOnClick); });
        KembaliMainMenu.onClick.AddListener(() => { ButtonKembali(); PlaySFX(buttonOnClick); });




        // Display the first soal initially
        ShowSoal(currentSoalIndex);
    }

    public void PlaySFX(AudioClip clip)
    {
        BGM.PlayOneShot(clip);
    }


    private void SetButtonsInteractable(bool interactable)
    {
        buttonA.interactable = interactable;
        buttonB.interactable = interactable;
        buttonC.interactable = interactable;
    }

    private void ShowSoal(int soalIndex)
    {
        if (soalIndex >= 0 && soalIndex < soalPrefabs.Length) // Periksa apakah soalIndex valid
        {
            foreach (GameObject soal in soalPrefabs)
            {
                soal.SetActive(false);
            }

            soalPrefabs[soalIndex].SetActive(true);

            UpdateTextButton(soalIndex);
        }
        else
        {
            Debug.LogError("SoalIndex tidak valid: " + soalIndex);
        }
    }

    private void UpdateTextButton(int soalIndex)
    {
        textA.text = "A. " + textButtonA[soalIndex];
        textB.text = "B. " + textButtonB[soalIndex];
        textC.text = "C. " + textButtonC[soalIndex];
    }

    private void OnAnswerSelected(string selectedAnswer)
    {
        bool isCorrect = selectedAnswer == jawabanBenar[currentSoalIndex];
        int soalIndex = currentSoalIndex;
        if (soalIndex >= 0 && soalIndex < soalPrefabs.Length) // Periksa apakah soalIndex valid
        {
            foreach (GameObject soal in soalPrefabs)
            {
                soal.SetActive(false);
                soalObject.SetActive(false);
            }

            soalPrefabs[soalIndex].SetActive(false);

        }
        else
        {
            Debug.LogError("SoalIndex tidak valid: " + soalIndex);
        }

        // Update "benar" and "salah" objects based on answer check
        popUpUIBenar.SetActive(isCorrect);
        salahObject.SetActive(!isCorrect);

        // Handle answer checking and feedback here

        if (currentSoalIndex == jawabanBenar.Length - 1)
        {

            if (!isCorrect)
            {
                salahObject.SetActive(true);
            }
            else
            {
                popUpUIBenar.SetActive(true);
                BGM.PlayOneShot(SFXJawabanBenar);
                Invoke("LastPagePanel", 1f);
            }
        }
        else
        {
            if (isCorrect)
            {
                // Delay next question transition to allow feedback time
                BGM.PlayOneShot(SFXJawabanBenar);
                Invoke("ShowUIJawabanBenar", 1f); // Replace 1f with desired delay time in seconds
            }
            else
            {
                salahObject.SetActive(true);
                BGM.PlayOneShot(SFXJawabanSalah);
            }
        }
    }

    private void ShowUIJawabanBenar()
    {
        benarObject.SetActive(true);
        popUpUIBenar.SetActive(false);

        int soalIndex = currentSoalIndex;
        if (soalIndex >= 0 && soalIndex < soalPrefabs.Length) // Periksa apakah soalIndex valid
        {
            foreach (GameObject soal in soalPrefabs)
            {
                soal.SetActive(false);
            }

            soalPrefabs[soalIndex].SetActive(false);

        }
        else
        {
            Debug.LogError("SoalIndex tidak valid: " + soalIndex);
        }

    }

    private void NextSoal()
    {
        currentSoalIndex++;
        ShowSoal(currentSoalIndex);

        // Reset "benar" and "salah" objects to inactive
        benarObject.SetActive(false);
        salahObject.SetActive(false);
        soalObject.SetActive(true);

        // Re-enable buttons after updating soalIndex
        SetButtonsInteractable(true);
    }

    private void CobaLagi()
    {
        ShowSoal(currentSoalIndex);

        // Reset "benar" and "salah" objects to inactive
        benarObject.SetActive(false);
        salahObject.SetActive(false);
        soalObject.SetActive(true);

        // Re-enable buttons after updating soalIndex
        SetButtonsInteractable(true);
    }

    private void ButtonKembali()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void ButtonKembaliLastPage()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void AboutUsPanel()
    {
        akhirGame.SetActive(false);
        aboutUs.SetActive(true);
    }

    private void LastPagePanel()
    {
        akhirGame.SetActive(true);
        aboutUs.SetActive(false);
    }

    private void ExitGame()
    {
        Debug.Log("Keluar Aplikasi");
        Application.Quit();
    }
}
