﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace Bohrium.Tools.Sample.SpecflowTests.StepDefinitions
{
    [Binding]
    public class StepDefinitions001
    {
        // For additional details on SpecFlow step definitions see http://go.specflow.org/doc-stepdef

        [Given("I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredSomethingIntoTheCalculator(int number)
        {
            //TODO: implement arrange (precondition) logic
            // For storing and retrieving scenario-specific data see http://go.specflow.org/doc-sharingdata 
            // To use the multiline text or the table argument of the scenario,
            // additional string/Table parameters can be defined on the step definition
            // method. 

            //ScenarioContext.Current.Pending();
        }

        [When("I press add")]
        public void WhenIPressAdd()
        {
            //TODO: implement act (action) logic

            //ScenarioContext.Current.Pending();
        }

        [Given("the result should be (.*) on the screen")]
        [Then("the result should be (.*) on the screen")]
        [Then("the result should be (.*) on the screen(\\s.*)?")]
        public void ResultShouldBe(int result, string screen)
        {
            //TODO: implement assert (verification) logic

            ScenarioContext.Current.Pending();
        }

        [Given("the result is (.*) on the screen")]
        [Then("the result should be (.*) on the screen")]
        public void ResultShouldBe(int result)
        {
            //TODO: implement assert (verification) logic

            //ScenarioContext.Current.Pending();
        }

        [Given(@"I have the following records")]
        public void GivenIHaveTheFollowingRecords(Table table)
        {
            //ScenarioContext.Current.Pending();
        }

        [When(@"I Go to the next when step")]
        public void WhenIGoToTheNextWhenStep()
        {
            //ScenarioContext.Current.Pending();
        }

        [Then(@"I check the value x")]
        public void ThenICheckTheValueX()
        {
            //ScenarioContext.Current.Pending();
        }

        [Then(@"I check the value Z")]
        public void ThenICheckTheValueZ(string multilineText)
        {
            //ScenarioContext.Current.Pending();
        }
    }
}
