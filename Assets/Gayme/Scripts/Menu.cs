using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    

    public GameObject Menu_Object;// the obejcts of the menu

    public GameObject Convas_Menu;// the obejcts of the convas

    public GameObject Loader;//the loading panel
    public Sprite[] Stades = new Sprite[4];// the image of the diffrent stade

    public  SpriteRenderer Sprite_Stade;// the choosing stade 

    public int Num_Stade=0;// number of the stade choosing
    void Start()
    {
        StartCoroutine(trans0());// loading event

       Update_Stade(0);
    }


public void Start_Game()// when clik on the button start 
{
 StartCoroutine(trans1());
}


    public void next()// when clik on the button next choose the stade 
    {
	Num_Stade+=1;
if(Num_Stade>3)
{
	Num_Stade=3;
}

Update_Stade(Num_Stade);

    }



    public void prev()// when clik on the button prev choose the stade 
    {
Num_Stade-=1;
if(Num_Stade<0) 
{
	Num_Stade=0;
}


Update_Stade(Num_Stade);

    }



  public IEnumerator trans0()// function of the loading process

{



        yield return new WaitForSeconds(3f);


        Menu_Object.SetActive(true);

        Convas_Menu.SetActive(true);

        Loader.SetActive(false);


    }


public IEnumerator trans1()// the evnt of before start the game match

{


  Loader.SetActive(true);
 Menu_Object.SetActive(false);

        Convas_Menu.SetActive(false);

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(1);


       


    }

    private void Update_Stade(int i)// function of update the number of the stade choosing
    {
Sprite_Stade.sprite = Stades[i];


PlayerPrefs.SetInt("Stade", i);


    }
}
