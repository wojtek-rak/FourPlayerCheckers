using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using FourPlayers;
using UnityEngine.SceneManagement;

public class MoveTest
{

    [UnityTest]
    public IEnumerator MoveTestWithEnumeratorPasses()
    {
        SceneManager.LoadScene(0);
        yield return new WaitForSeconds(0.3f);
        var boardController = GameObject.FindGameObjectWithTag("Board").GetComponent<BoardController>();
        var gameManager = GameObject.FindGameObjectWithTag("GameManager");
        var moveController = gameManager.GetComponent<MoveManager>();
        var pawn = boardController.fields[11, 13].PawnController.gameObject;
        var posStart = pawn.GetComponent<PawnController>().Field;
        moveController.StartHolding(pawn);
        pawn.transform.localPosition = pawn.transform.localPosition + new Vector3(boardController.fieldSize, boardController.fieldSize, 0f);
        moveController.StopHolding(pawn);
        var posEnd = pawn.GetComponent<PawnController>().Field;
        Assert.AreEqual(posStart, posEnd);
    }
}