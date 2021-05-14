using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuthUI : MonoBehaviour
{
    public GameObject loginPanel;
    public GameObject signupPanel;
    public GameObject mainPanel;

    public InputField loginId;
    public InputField loginPw;

    public InputField signupId;
    public InputField signupPw;
    public InputField confirm;

    public Text loggedin;


    // Start is called before the first frame update
    void Start()
    {

    }

    public void ShowLoginPanel()
    {
        ShowPanel(loginPanel);
    }

    public void ShowSignupPanel()
    {
        ShowPanel(signupPanel);
    }

    public void ShowMainPanel()
    {
        ShowPanel(mainPanel);
    }

    public void ShowPanel(GameObject panel)
    {
        loginPanel.SetActive(false);
        signupPanel.SetActive(false);
        mainPanel.SetActive(false);

        panel.SetActive(true);
    }
}
