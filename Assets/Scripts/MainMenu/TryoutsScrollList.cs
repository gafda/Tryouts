using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TryoutsScrollList : MonoBehaviour
{
    public SimpleObjectPool buttonObjectPool;
    public Transform contentPanel;
    public Text Header;
    public List<MenuItem> itemList;
    private const string HeaderTemplate = @"Unity3D 2018 Tryouts ({0})";

    public void RefreshDisplay()
    {
        this.Header.text = string.Format(HeaderTemplate, this.itemList.Count);

        this.RemoveButtons();
        this.AddButtons();
    }

    private void AddButtons()
    {
        foreach (var item in this.itemList)
        {
            var tempItem = item;
            var newButton = this.buttonObjectPool.GetObject();
            newButton.transform.SetParent(this.contentPanel);

            var menuItemButton = newButton.GetComponent<MenuItemButton>();
            menuItemButton.Setup(tempItem, this);
        }
    }

    private void AddItem(MenuItem itemToAdd, TryoutsScrollList scrollList)
    {
        scrollList.itemList.Add(itemToAdd);
    }

    private void RemoveButtons()
    {
        while (this.contentPanel.childCount > 0)
        {
            var toRemove = transform.GetChild(0).gameObject;
            this.buttonObjectPool.ReturnObject(toRemove);
        }
    }

    private void RemoveItem(MenuItem itemToRemove, TryoutsScrollList scrollList)
    {
        for (int i = scrollList.itemList.Count - 1; i >= 0; i--)
        {
            if (scrollList.itemList.ElementAt(i) == itemToRemove)
            {
                scrollList.itemList.Remove(itemToRemove);
            }
        }
    }

    // Use this for initialization
    private void Start()
    {
        this.RefreshDisplay();
    }
}