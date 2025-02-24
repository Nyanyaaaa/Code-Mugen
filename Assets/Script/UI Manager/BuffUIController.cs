using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffUIController : MonoBehaviour
{
    public GameObject panel;
    public Button[] buttons;
    [SerializeField] private BuffSystem buffSystem;
    [SerializeField] private Sprite nullPlaceholder;
    [SerializeField] private AudioSource bgmController;
    [SerializeField] private AudioManager sfxController;
    [SerializeField] private PauseGame pauseGame;

    private void Start()
    {
        panel.SetActive(false);
    }

    public void ShowBuffSelection()
    {
        Time.timeScale = 0;
        pauseGame.isPaused = true;
        sfxController.PlaySFX(sfxController.LevelUp);
        bgmController.volume = 0.3f;
        panel.SetActive(true);
        RandomizeBuffList();
    }

    private void RandomizeBuffList()
    {
        List<GameObject> shuffleBuffs = buffSystem.runtimeBuffs.OrderBy(x => Random.value).Take(3).ToList();
        Debug.Log(shuffleBuffs.Count);

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < shuffleBuffs.Count) {

                GameObject buff = shuffleBuffs[i];
                BuffBase buffData = buff.GetComponent<BuffBase>();

                if (buffData != null) {
                    buffData.Initialize();

                    buttons[i].transform.GetChild(0).GetComponent<Image>().sprite = buffData.sprite;
                    buttons[i].transform.GetChild(1).GetComponentInChildren<TMP_Text>().text = buffData.buffName;
                    buttons[i].transform.GetChild(2).GetComponentInChildren<TMP_Text>().text = buffData.buffDescription;
                    buttons[i].onClick.RemoveAllListeners();
                    buttons[i].onClick.AddListener(() => SelectBuff(buffData.buffName));
                }
            } else
            {
                    buttons[i].GetComponentInChildren<TMP_Text>().text = "No more buffs, will add more soon!";
                    buttons[i].transform.GetChild(2).GetComponentInChildren<TMP_Text>().text = "";
                    buttons[i].transform.GetChild(0).GetComponent<Image>().sprite = nullPlaceholder;
                    buttons[i].onClick.RemoveAllListeners();
            }
        }
    }

    private void SelectBuff(string buffName)
    {
        buffSystem.ApplyBuff(buffName);
        panel.SetActive(false);
        pauseGame.isPaused = false;
        Time.timeScale = 1;
        bgmController.volume = 0.8f;
    }
}
