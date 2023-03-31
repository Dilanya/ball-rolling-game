using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{   
    public static GameManager Instance;
    [Header("Game UI")]
    public GameObject MainMenuUI;
    public GameObject GameHUDUI;
    public GameObject GameCompleteUI;
    public TextMeshProUGUI gameScoreText, GameCompleteScoreText;

    [Space()]
    [Header("Game Play")]
    

    public int collected_count = 0;
    public int max_Collectibles = 1;
    public GameObject Player;
    public GameObject [] collectibles;
    Vector3 startPosition;
    public GameObject Coll_prefab;
    public Transform [] spawnPositions;
    private int spawnPosCount;
    int randomSpawnId = 0 ;

    [Space()]
    [Header("Audio")]
    public AudioSource Sounds;
    public AudioClip Collectible_sound;

    void Awake(){
      Instance = this;
    }

    void Start(){
        spawnPosCount = spawnPositions.Length;
        spawnCubes();
        startPosition = Player.transform.position;
        goToMenu();
    }
    
    public void goToMenu(){
        for (int i = 0; i < max_Collectibles ; i++)
        {
          collectibles[i].GetComponent<MeshRenderer>().enabled = false;  
        }
        Player.SetActive(false);
        MainMenuUI.SetActive(true);
        GameHUDUI.SetActive(false);
        GameCompleteUI.SetActive(false);
    }
    public void goToGame(bool replay){
        collected_count = 0;
        for (int i = 0; i < max_Collectibles ; i++)
        { 
          randomSpawnId = getUniqueRandom(randomSpawnId);
          Vector3 x = spawnPositions[randomSpawnId].position;
          collectibles[i].GetComponent<MeshRenderer>().enabled = true; 
          collectibles[i].transform.position = x ;
        }
        Player.transform.position = startPosition;
        Player.SetActive(true);
        Player.GetComponent<Rigidbody>().isKinematic = false;
        gameScoreText.text = "SCORE : " +collected_count;

        if (replay == true){
            GameCompleteUI.SetActive(false);
        }
        else{
            MainMenuUI.SetActive(false);
        }
        
        GameHUDUI.SetActive(true);
    }
    
    public void goToGameComplete(){
        Player.GetComponent<Rigidbody>().isKinematic = true;
        GameCompleteScoreText.text = "SCORE : " + collected_count;
        GameHUDUI.SetActive(false); 
        GameCompleteUI.SetActive(true);
    }
    public void AddScore(){
        collected_count += 1;
        gameScoreText.text = "SCORE : " +collected_count;
        if(collected_count < max_Collectibles)return;
        goToGameComplete();
    }
    void DestroyCubes(){
        for (int i = 0; i < max_Collectibles ; i++)
        {
            GameObject.Destroy(collectibles[i]);
        }
    }
    public void spawnCubes(){
        collectibles = new GameObject[max_Collectibles];
        for (int i = 0; i < max_Collectibles ; i++)
        {
          randomSpawnId = getUniqueRandom(randomSpawnId);
          Vector3 x = spawnPositions[randomSpawnId].position;
          GameObject obj = GameObject.Instantiate(Coll_prefab,x, Quaternion.identity ); 
          collectibles[i] = obj;  
        }
    }
    int getUniqueRandom(int old){
        int x = Random.Range(0,spawnPosCount);
        if (x==old) {
            return getUniqueRandom(old);

        }
        return x;
    }


    public void goToNext(){
        DestroyCubes();
        max_Collectibles += 1;
        max_Collectibles = max_Collectibles > 5 ? 5 : max_Collectibles;

        spawnCubes();

        goToGame(true);
    }

    public void exit(){
        Application.Quit();
    }

    public void playAudio(){
        Sounds.Play();
    }
}
