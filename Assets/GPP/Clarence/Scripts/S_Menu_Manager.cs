using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Menu_Manager : MonoBehaviour
{
    private static S_Menu_Manager instance;
    private S_Menu_Manager() { } //au cas où certains fous tenteraient qd même d'utiliser le mot clé "new"

    // Méthode d'accès statique (point d'accès global)
    public static S_Menu_Manager Instance
    {// ajout ET création du composant à un GameObject nommé "SingletonHolder"
        get { return instance ?? (instance = new GameObject("MenuManager").AddComponent<S_Menu_Manager>()); }
        private set { instance = value; }
    }

    public S_PlayerController playerController;

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);    // Suppression d'une instance précédente (sécurité...sécurité...)

        instance = this;
        playerController = GameObject.FindAnyObjectByType<S_PlayerController>();
    }

    public void stopPlayer(bool b)
    {
        playerController.setIsNotInMenu(b);
    }
}
