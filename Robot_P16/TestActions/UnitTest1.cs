using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Robot_P16.Actions;

namespace TestActions
{


    class ActionNull : Robot_P16.Actions.Action
    {

        public ActionNull(String description)
            : base(description)
        {

        }

        public override void Execute()
        {
            this.Status = ActionStatus.SUCCESS;
        }

        protected override bool PostStatusChangeCheck(ActionStatus previousStatus)
        {
            return true;
        }

        public override void Feedback(Robot_P16.Actions.Action a)
        {
            throw new NotImplementedException();
        }
    }

    [TestClass]
    public class UnitTest1
    {



        [TestMethod]
        public void TestActionEnSerie()
        {

            ActionNull actionNull = new ActionNull("Action null");
            ActionEnSerie serialSuccess = new ActionBuilder("Action Série Test")
            .Add(actionNull)
            .Add(actionNull.Clone())
            .Add(actionNull.Clone())
            .BuildActionEnSerie();

            serialSuccess.Execute();
            Assert.AreEqual(serialSuccess.Status, ActionStatus.SUCCESS);            

        }
    }
}
