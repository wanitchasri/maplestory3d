using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkills : MonoBehaviour
{
    public GameObject projectilePrefab1;
    public GameObject projectilePrefab2;
    public void Skill(int damageAmount)
    {
        PlayerLife.HP -= damageAmount;
        Instantiate(projectilePrefab1, transform.position, projectilePrefab1.transform.rotation);
        Instantiate(projectilePrefab2, transform.position, projectilePrefab2.transform.rotation);
    }
}