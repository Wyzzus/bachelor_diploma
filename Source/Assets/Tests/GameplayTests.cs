using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;


namespace Tests
{
    public class GameplayTests
    {
        public GameObject Game;

        [SetUp]
        public void Setup()
        {
            Game = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestScenes/Game"));

        }

        [TearDown]
        public void Teardown()
        {
            Object.Destroy(Game);
            if (GameObject.Find("Mark(Clone)") != null)
                Object.Destroy(GameObject.Find("Mark(Clone)"));
        }

        [UnityTest]
        public IEnumerator DiceRollTest()
        {
            DiceComponent dice = GameObject.FindGameObjectWithTag("Player").GetComponent<DiceComponent>();
            int testRoll = 4;
            bool res = false;
            dice.DiceRoll(testRoll);
            Debug.Log(dice.GetDiceCount());

            yield return new WaitForSeconds(4);

            Debug.Log(dice.GetDiceCount());

            if (dice.diceCount > 0 && dice.diceCount <= testRoll)
                res = true;

            Assert.AreEqual(res, true);
        }

        [UnityTest]
        public IEnumerator DiceRollResultDisplay()
        {
            DiceComponent dice = GameObject.FindGameObjectWithTag("Player").GetComponent<DiceComponent>();
            int testRoll = 4;

            dice.DiceRoll(testRoll);
            Debug.Log(dice.GetDiceCount());

            yield return new WaitForSeconds(4);
            string res = "Выпало " + dice.diceCount;
            Text diceRes = GameObject.Find("RollResult").GetComponent<Text>();

            Debug.Log(res);
            Debug.Log(diceRes.text);
            Assert.AreEqual(res, diceRes.text);
        }

        [UnityTest]
        public IEnumerator PlayerMovementToNewPos()
        {
            PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            Vector3 startPos = playerController.transform.position;
            playerController.NewPosition = new Vector3(10, 0, 10);
            playerController.Movement();
            yield return new WaitForSeconds(4);
            Vector3 newPos = playerController.transform.position;

            Assert.AreNotEqual(startPos, newPos);
        }

        [UnityTest]
        public IEnumerator MarkStateChangingToTrue()
        {
            MapComponent mapComponent = GameObject.Find("Area").GetComponent<MapComponent>();
            bool startState = mapComponent.MarkAddMode;
            mapComponent.ChangeMarkMode();
            yield return new WaitForSeconds(1);
            bool newState = mapComponent.MarkAddMode;

            Debug.Log(startState);
            Debug.Log(newState);

            Assert.AreNotEqual(startState, newState);
        }

        [UnityTest]
        public IEnumerator MarkCreate()
        {
            MapComponent mapComponent = GameObject.Find("Area").GetComponent<MapComponent>();
            mapComponent.ChangeMarkMode();
            mapComponent.MarkPosition = new Vector3(-2, 0, -2);

            if (mapComponent.MarkAddMode == true)
                MonoBehaviour.Instantiate(mapComponent.MarkPrefab, mapComponent.MarkPosition, Quaternion.identity);

            yield return new WaitForSeconds(2);

            bool res = false;

            if (GameObject.Find("Mark(Clone)") != null)
                res = true;

            Assert.AreEqual(res, true);
        }

        [UnityTest]
        public IEnumerator MarkDelete()
        {
            MapComponent mapComponent = GameObject.Find("Area").GetComponent<MapComponent>();
            mapComponent.ChangeMarkMode();
            mapComponent.MarkPosition = new Vector3(-2, 0, -2);

            if (mapComponent.MarkAddMode == true)
                MonoBehaviour.Instantiate(mapComponent.MarkPrefab, mapComponent.MarkPosition, Quaternion.identity);

            yield return new WaitForSeconds(2);

            GameObject Mark = GameObject.Find("Mark(Clone)");
            Mark.transform.SetParent(GameObject.Find("Area").transform);
            Mark.GetComponent<MarkComponent>().DestroyMark(Mark);

            yield return new WaitForSeconds(2);
            bool res = false;

            if (GameObject.Find("Mark(Clone)") == null)
                res = true;

            Assert.AreEqual(res, true);
        }

        [UnityTest]
        public IEnumerator SetupPlayerName()
        {
            InputField InputField = GameObject.Find("InputName").GetComponent<InputField>();
            InputField.text = "TestName";
            Text nickname = GameObject.Find("nickname").GetComponent<Text>();
            nickname.text = InputField.text;

            PlayerComponent Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerComponent>();
            Player.SetupName();
            yield return new WaitForSeconds(1);
            
            Assert.AreEqual(Player.playerData.Name, InputField.text);
        }        
    }
}
