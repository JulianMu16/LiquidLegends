using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public abstract class Health : MonoBehaviour
{
    public char characterType; 
    public int health;
    // Start is called before the first frame update

    protected void TakeDamage() {

        health -= 1;
        if (health == 0) {
            Death();
        }
    }
    public void Death() {

        //Program death maybe animation
        SceneManager.LoadScene("SampleScene");

    }


   public void processDamage(){
        TakeDamage();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("botBullet") && characterType == 'p') {
            processDamage();
        }
        else if (collision.gameObject.CompareTag("playerBullet") && characterType =='b') {
            processDamage();
        }
    }

    // Update is called once per framei

}
