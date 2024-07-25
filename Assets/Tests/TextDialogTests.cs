/*************************************************************************
 *  Copyright (C) 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  TextDialogTests.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/20
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections;
using MGS.QianWen;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TextDialogTests
{
    const string API_KEY = "";

    IQianWenHub qianWenHub;
    bool isDone;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        qianWenHub = new QianWenHub(API_KEY);
    }

    [UnityTest]
    public IEnumerator TextDialogTest()
    {
        isDone = false;

        var question = "This is a test question.";
        Debug.Log(question);

        var textDialog = qianWenHub.NewTextDialog();
        textDialog.OnComplete += TextDialog_OnComplete;
        textDialog.Quest(question);
        while (!isDone)
        {
            yield return null;
        }
    }

    private void TextDialog_OnComplete(string answer, Exception error)
    {
        if (error == null)
        {
            Debug.Log(answer);
        }
        else
        {
            Debug.LogError(error.Message);
        }
        isDone = true;
    }
}