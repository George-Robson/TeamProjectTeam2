using TMPro;
using UnityEngine;

public class AudioVolume : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    private AudioSource audioSource;
    public AudioClip audioClip;
    private GameObject subtitleBackground;
    private canChangeLevel canChangeLevel;
    [SerializeField, Range(0f, 1f)] private float volume = 1;
    public bool requiresCondition;
    public string condition;
    public bool onlyPlaysOnce;
    private bool hasPlayed = false;
    private GameManager gameManager;
    public bool requiresCC = true;
    public float cC1Duration = 1f;
    public float cC2Duration = 1f;
    public float cC3Duration = 1f;
    public float cC4Duration = 1f;
    public float cC5Duration = 1f;
    [SerializeField, TextArea] public string closedCaption1;
    [SerializeField, TextArea] public string closedCaption2;
    [SerializeField, TextArea] public string closedCaption3;
    [SerializeField, TextArea] public string closedCaption4;
    [SerializeField, TextArea] public string closedCaption5;
    private float startTime = 0;
    private bool triggered = false;
    private Transform UILocation;
    private bool UISpawned;

    void Start()
    {
        subtitleBackground = GameObject.Find("UI Canvas").transform.GetChild(0).gameObject;
        canChangeLevel = GameObject.Find("Player").GetComponent<canChangeLevel>();
        audioSource = GameObject.Find("Player").transform.GetChild(0).GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").transform.GetComponent<GameManager>();
        UILocation = GameObject.Find("UI Canvas").transform;
    }

    private void Update() {
        if (triggered)
        {
            ShowCC();
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(gameObject.transform.position, GetComponent<BoxCollider>().size);
    }

    void ShowCC() {
        CreateText();
        UISpawned = true;
        textMeshProUGUI.gameObject.SetActive(true);
        canChangeLevel.setCanChangeLevel(false);
        subtitleBackground.SetActive(true);
        if (startTime < cC1Duration)
            textMeshProUGUI.text = closedCaption1;
        else if (startTime < cC1Duration + cC2Duration)
            textMeshProUGUI.text = closedCaption2;
        else if (startTime < cC1Duration + cC2Duration + cC3Duration)
            textMeshProUGUI.text = closedCaption3;
        else if (startTime < cC1Duration + cC2Duration + cC3Duration + cC4Duration)
            textMeshProUGUI.text = closedCaption4;
        else if (startTime < cC1Duration + cC2Duration + cC3Duration + cC4Duration + cC5Duration)
            textMeshProUGUI.text = closedCaption5;
        else
        {
            textMeshProUGUI.text = "";
            textMeshProUGUI.gameObject.SetActive(false);
            canChangeLevel.setCanChangeLevel(true);
            subtitleBackground.SetActive(false);
            return;
        }
        startTime += Time.deltaTime;
    }

    private void CreateText() {
        if (!UISpawned)
            textMeshProUGUI = Instantiate(textMeshProUGUI, UILocation);
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (requiresCondition && !gameManager.FindState(condition))
            return;
        if (onlyPlaysOnce && hasPlayed)
            return;
        if (other.CompareTag("Player") && audioClip != null)
        {
            audioSource.PlayOneShot(audioClip, volume);
            hasPlayed = true;
            if (requiresCC)
            {
                startTime = 0;
                triggered = true;
            }
        } else Debug.LogError("No audio clip assigned to play");
    }
}
