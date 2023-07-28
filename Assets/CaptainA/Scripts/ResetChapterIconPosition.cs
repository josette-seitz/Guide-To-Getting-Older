using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetChapterIconPosition : MonoBehaviour
{
    public List<GameObject> ChapterIcons = new List<GameObject>();
    public List<GameObject> ChapterBubbles = new List<GameObject>();
    [Space]
    public GameObject Toilet;
    public GameObject Hangover;
    public GameObject Question;
    public GameObject TrashCan;
    [Space]
    public GameObject TutorialHand;

    private Dictionary<string, Vector3> chapterIconDict = new Dictionary<string, Vector3>();
    private AudioSource playNope;
    
    void Start()
    {
        foreach (var icon in ChapterIcons)
        {
            // Grab icon tag & position
            chapterIconDict.Add(icon.tag, icon.transform.position);
        }

        playNope = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // User missed Target Release Object
        playNope.Play();

        // Reset Rigidbody properties
        Rigidbody rb = other.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;

        // Reset Rotation (0, 0, 0)
        other.transform.rotation = Quaternion.identity;

        // Enable Rotate Chapter script
        RotateChapter rc = other.GetComponent<RotateChapter>();
        rc.enabled = true;

        //Reset Icon position
        foreach(var name in chapterIconDict)
        {
            if (other.tag == name.Key)
            {
                other.transform.position = name.Value;
            }
        }
        
        switch (other.tag)
        {
            case nameof(Enums.ChapterTarget.Poop):
                Toilet.SetActive(false);
                EnableChapterBubbles();
                break;
            case nameof(Enums.ChapterTarget.Hangover):
                Hangover.SetActive(false);
                EnableChapterBubbles();
                break;
            case nameof(Enums.ChapterTarget.Question):
                Question.SetActive(false);
                EnableChapterBubbles();
                break;
            case nameof(Enums.ChapterTarget.Tutorial):
                TrashCan.SetActive(false);
                TutorialHand.SetActive(true);
                break;
            default:
                Debug.LogError("Captain-A: Invalid Chapter Icon.");
                break;
        }
    }

    private void EnableChapterBubbles()
    {
        // User missed the matching object, enable all chapters to select again
        foreach (var bubble in ChapterBubbles)
        {
            bubble.SetActive(true);
        }
    }
}
