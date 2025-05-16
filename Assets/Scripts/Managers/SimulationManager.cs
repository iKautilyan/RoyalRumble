using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimulationManager : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> simCharacters;

    [SerializeField]
    int characterCount = 10;

    [SerializeField]
    List<Transform> spawnPoints = new List<Transform>();

    [SerializeField]
    GameObject characterPrefab;

    [SerializeField]
    GameObject uiPanel;

    [SerializeField]
    Text resultDisplayText;

    public static SimulationManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        InitializeSimulation();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(simCharacters.Count == 1)
        {
            uiPanel.SetActive(true);
            resultDisplayText.text = simCharacters.FirstOrDefault().name + " has prevailed";
            Debug.Log(simCharacters.FirstOrDefault().name + " has prevailed");
            simCharacters.Clear();  
        }
    }

    public void InitializeSimulation()
    {
        for(int i = 0; i < characterCount; i++)
        {
            simCharacters.Add(Instantiate(characterPrefab, RandomSpawnPoint(), Quaternion.identity));
            simCharacters[i].name += " " + i.ToString();
        }
    }

    public Vector3 RandomSpawnPoint()
    {
        Vector3 randomPoint = Vector3.zero;
        randomPoint += spawnPoints[Random.Range(0, spawnPoints.Count - 1)].position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        return randomPoint;
    }

    public void ReloadScene()
    {
        uiPanel.SetActive(false);
        simCharacters.Clear();
        SceneManager.LoadScene(0);
    }

    public void ExitApplication()
    {
        Application.Quit(); 
    }
}
