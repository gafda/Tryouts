using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonCommands : MonoBehaviour
{
    private const string ClearedDataText = "database data";

    private CreateDb createDb;
    private ExistingDb existingDb;

    public Text DebugTextCreate;
    public Text DebugTextExisting;

    private CreateDb GetCreateDb
    {
        get
        {
            if (this.createDb == null)
            {
                this.createDb = new CreateDb
                {
                    DebugText = this.DebugTextCreate
                };
            }

            return this.createDb;
        }
    }

    private ExistingDb GetExistingDb
    {
        get
        {
            if (this.existingDb == null)
            {
                this.existingDb = new ExistingDb
                {
                    DebugText = this.DebugTextExisting
                };
            }

            return this.existingDb;
        }
    }

    public void BtnCreateDb_OnClick()
    {
        Debug.Log(nameof(BtnCreateDb_OnClick));
        this.GetCreateDb.Start();
    }

    public void BtnExistingDb_OnClick()
    {
        Debug.Log(nameof(BtnExistingDb_OnClick));
        this.GetExistingDb.Start();
    }

    public void BtnHome_OnPointerUp()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void BtnClearTexts_OnPointerUp()
    {
        this.DebugTextCreate.text = ClearedDataText;
        this.DebugTextExisting.text = ClearedDataText;
    }
}
