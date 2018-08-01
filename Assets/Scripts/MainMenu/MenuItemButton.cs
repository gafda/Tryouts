using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuItemButton : MonoBehaviour
{
    public Button button;
    public Image iconImage;
    public Text nameLabel;
    private MenuItem item;
    private TryoutsScrollList scrollList;

    public void HandleClick()
    {
        if (!string.IsNullOrWhiteSpace(this.item.SceneName))
        {
            SceneManager.LoadScene(this.item.SceneName);
        }
    }

    public void Setup(MenuItem currentItem, TryoutsScrollList currentScrollList)
    {
        this.item = currentItem;
        this.nameLabel.text = this.item.Name;
        this.iconImage.sprite = this.item.Icon;

        this.scrollList = currentScrollList;
    }

    // Use this for initialization
    private void Start()
    {
        this.button.onClick.AddListener(this.HandleClick);
    }
}