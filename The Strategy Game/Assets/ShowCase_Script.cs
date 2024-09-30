using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowCase_Script : MonoBehaviour
{
    [SerializeField] private Transform[] models; 
    [SerializeField] private Light[] spotlights;
    [SerializeField] private float transitionSpeed = 5.0f;
    [SerializeField] private GameObject Text;
    private int currentModelIndex = 0; 
    private bool isControllingCamera = false;
    [SerializeField] private Vector3 offset = new Vector3(0, 1.5f, -5f);

    private void Start()
    {
        Text.SetActive(true);
        spotlights[0].enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.C))
        {
            isControllingCamera = true;
            Text.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if (isControllingCamera)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveToNextModel();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveToPreviousModel();
            }

            
            Vector3 targetPosition = models[currentModelIndex].position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, transitionSpeed * Time.deltaTime);
        }

        UpdateSpotlights();
    }

   
    void MoveToNextModel()
    {
        currentModelIndex = (currentModelIndex + 1) % models.Length;
    }

  
    void MoveToPreviousModel()
    {
        currentModelIndex--;
        if (currentModelIndex < 0)
        {
            currentModelIndex = models.Length - 1;
        }
    }

   
    void UpdateSpotlights()
    {
        for (int i = 0; i < spotlights.Length; i++)
        {
            spotlights[i].enabled = (i == currentModelIndex);
        }
    }
}

