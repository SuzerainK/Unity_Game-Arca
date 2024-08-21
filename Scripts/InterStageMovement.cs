using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterStageMovement : MonoBehaviour
{
    //Need to figure out how to control which stages are on even when loading back the scene.
    //Proposed solution: Utilize PlayerPrefs to save players' last access to interstage movement.
    //Based on the PlayerPrefs variable, the other stages will be disabled.
    //"Disabling stages" action will be put in the Start() function for loading
    //and in the ChangeLocation() function for changes in the current action.
    //Dialgoues must be readjusted else they will be able to be invoked again when reloading scenes.

    private GameObject Player;
    [Header("Entrances and Exits")]
    [SerializeField] GameObject PlayerRoomIn;
    [SerializeField] GameObject PlayerRoomOut;
    [SerializeField] GameObject Room1In;
    [SerializeField] GameObject Room1Out;
    [SerializeField] GameObject Room2In;
    [SerializeField] GameObject Room2Out;
    [SerializeField] GameObject Room3In;
    [SerializeField] GameObject Room3Out;
    [SerializeField] GameObject HallwayIn;
    [SerializeField] GameObject HallwayOut;
    [SerializeField] GameObject MainHallIn;
    [SerializeField] GameObject MainHallOut;
    [SerializeField] GameObject GardensIn;
    [SerializeField] GameObject GardensOut;
    [SerializeField] GameObject EscapeRouteIn;
    [SerializeField] GameObject HoVEntranceIn;
    [SerializeField] GameObject HoVInside_1;
    [SerializeField] GameObject HoVInside_2;
    [SerializeField] GameObject HoVReturn;
    [SerializeField] GameObject CathedralIn;

    [Header("Stages")]
    [SerializeField] GameObject PlayerRoom;
    [SerializeField] GameObject Room1;
    [SerializeField] GameObject Room2;
    [SerializeField] GameObject Room3;
    [SerializeField] GameObject Hallway;
    [SerializeField] GameObject MainHall;
    [SerializeField] GameObject Gardens;
    [SerializeField] GameObject RegiaRoad;
    [SerializeField] GameObject EscapeRoute;
    [SerializeField] GameObject HoVEntrance;
    [SerializeField] GameObject HoV_FirstFloor;
    [SerializeField] GameObject HoV_LastFloor;
    [SerializeField] GameObject First_Trial;
    [SerializeField] GameObject Second_Trial;
    [SerializeField] GameObject Third_Trial;

    [Header("Cameras")]
    [SerializeField] GameObject MainCamera;
    [SerializeField] GameObject HOVCamera;
    [SerializeField] GameObject HOVEntranceCamera;

    float pl_x;
    float pl_y;

    public GameObject AudioMGame;

    AudioManager audioManager;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        audioManager = AudioMGame.GetComponent<AudioManager>();
        //HideStages();
    }

    private void HideStages()
    {
        PlayerRoom.SetActive(true);
        Hallway.SetActive(true);
        Room1.SetActive(false);
        Room2.SetActive(false);
        Room3.SetActive(false);
        MainHall.SetActive(false);
        Gardens.SetActive(false);
        RegiaRoad.SetActive(false);
        EscapeRoute.SetActive(false);
        HoVEntrance.SetActive(false);
        HoV_FirstFloor.SetActive(false);
        HoV_LastFloor.SetActive(false);
        First_Trial.SetActive(false);
        Second_Trial.SetActive(false);
        Third_Trial.SetActive(false);

    }
    public void ChangeLocation(string door_name)
    {
        Debug.Log("ChangeLocation Invoked");

        switch (door_name)
        {
            case "PlayerRoomIn":
                audioManager.Play("Door");
                pl_x = PlayerRoomOut.transform.position.x;
                pl_y = PlayerRoomOut.transform.position.y;
                Player.transform.position = new Vector2(pl_x, pl_y);

                break;
            case "PlayerRoomOut":
                audioManager.Play("Door");
                pl_x = PlayerRoomIn.transform.position.x;
                pl_y = PlayerRoomIn.transform.position.y;
                Player.transform.position = new Vector2(pl_x, pl_y);
                break;
            case "Room1In":
                audioManager.Play("Door");
                pl_x = Room1Out.transform.position.x;
                pl_y = Room1Out.transform.position.y;
                Player.transform.position= new Vector2(pl_x, pl_y);     
                break;
            case "Room1Out":
                audioManager.Play("Door");
                pl_x = Room1In.transform.position.x;
                pl_y = Room1In.transform.position.y;
                Player.transform.position = new Vector2(pl_x, pl_y);
                break;
            case "Room2In":
                audioManager.Play("Door");
                pl_x = Room2Out.transform.position.x;
                pl_y = Room2Out.transform.position.y;
                Player.transform.position = new Vector2(pl_x, pl_y);
                break;
            case "Room2Out":
                audioManager.Play("Door");
                pl_x = Room2In.transform.position.x;
                pl_y = Room2In.transform.position.y;
                Player.transform.position = new Vector2(pl_x, pl_y);
                break;
            case "Room3In":
                audioManager.Play("Door");
                pl_x = Room3Out.transform.position.x;
                pl_y = Room3Out.transform.position.y;
                Player.transform.position = new Vector2(pl_x, pl_y);
                break;
            case "Room3Out":
                audioManager.Play("Door");
                pl_x = Room3In.transform.position.x;
                pl_y = Room3In.transform.position.y;
                Player.transform.position = new Vector2(pl_x, pl_y);
                break;
            /*
            case "Room4In":
                pl_x = Room4Out.transform.position.x;
                pl_y = Room4Out.transform.position.y;
                Player.transform.position = new Vector2(pl_x, pl_y);
                break;
            case "Room4Out":
                pl_x = Room4In.transform.position.x;
                pl_y = Room4In.transform.position.y;
                Player.transform.position = new Vector2(pl_x, pl_y);
                break;
            */
            case "HallwayIn":
                pl_x = HallwayOut.transform.position.x;
                pl_y = HallwayOut.transform.position.y;
                Player.transform.position = new Vector2(pl_x, pl_y);
                break;
            case "HallwayOut":
                pl_x = HallwayIn.transform.position.x;
                pl_y = HallwayIn.transform.position.y;
                Player.transform.position = new Vector2(pl_x, pl_y);
                break;
            case "MainHallIn":
                pl_x = MainHallOut.transform.position.x;
                pl_y = MainHallOut.transform.position.y;
                Player.transform.position = new Vector2(pl_x, pl_y);
                break;
            case "MainHallOut":
                pl_x = MainHallIn.transform.position.x;
                pl_y = MainHallIn.transform.position.y;
                Player.transform.position = new Vector2(pl_x, pl_y);
                break;
            
            case "GardensIn":
                pl_x = GardensOut.transform.position.x;
                pl_y = GardensOut.transform.position.y;
                Player.transform.position = new Vector2(pl_x, pl_y);
                break;
            
            case "GardensOut":
                pl_x = GardensIn.transform.position.x;
                pl_y = GardensIn.transform.position.y;
                Player.transform.position= new Vector2(pl_x, pl_y);
                break;

            case "EscapeRouteIn":
                pl_x = EscapeRouteIn.transform.position.x;
                pl_y = EscapeRouteIn.transform.position.y;
                Player.transform.position = new Vector2(pl_x, pl_y);
                break;
            case "HoVEntrance":
                MainCamera.SetActive(false);
                HOVEntranceCamera.SetActive(true);
                pl_x = HoVEntranceIn.transform.position.x;
                pl_y = HoVEntranceIn.transform.position.y;
                Player.transform.position = new Vector2(pl_x, pl_y);
                break;
            case "HoVInside_1":
                MainCamera.SetActive(false);
                HOVEntranceCamera.SetActive(false);
                HOVCamera.SetActive(true);
                pl_x = HoVInside_1.transform.position.x;
                pl_y = HoVInside_1.transform.position.y;
                Player.transform.position = new Vector2(pl_x, pl_y);
                break;
            case "HoVInside_2":
                pl_x = HoVInside_2.transform.position.x;
                pl_y = HoVInside_2.transform.position.y;
                Player.transform.position = new Vector2(pl_x, pl_y);
                break;
            case "HoVReturn":
                pl_x = HoVReturn.transform.position.x;
                pl_y = HoVReturn.transform.position.y;
                Player.transform.position = new Vector2(pl_x, pl_y);
                break;
            case "CathedralIn":
                pl_x = CathedralIn.transform.position.x;
                pl_y = CathedralIn.transform.position.y;
                Player.transform.position = new Vector2(pl_x, pl_y);
                break;
            default:
                Debug.Log("Error In InterStageMovement");
                break;


        }

    }

}

    
