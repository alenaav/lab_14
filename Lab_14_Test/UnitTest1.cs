using Microsoft.VisualStudio.TestTools.UnitTesting;
using lab14_1;
using AutomobileLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;

namespace lab14_1.Tests
{
    [TestClass]
    public class ProgramTests
    {
        private Queue<List<Automobile>> CreateMockQueue()
        {
            Queue<List<Automobile>> queue = new Queue<List<Automobile>>();

            List<Automobile> list1 = new List<Automobile> { new Car(), new Truck() };
            List<Automobile> list2 = new List<Automobile> { new Car(), new Truck(), new Suv() };

            queue.Enqueue(list1);
            queue.Enqueue(list2);

            return queue;
        }

        [TestMethod]
        public void FillQueue_ShouldFillQueueWithAutomobiles()
        {
            // Arrange
            Queue<List<Automobile>> queue = new Queue<List<Automobile>>();
            Program.FillQueue(queue); // Вызов статического метода через имя типа

            // Assert
            Assert.IsTrue(queue.Count > 0);
            foreach (var list in queue)
            {
                Assert.IsTrue(list.Count > 0);
                foreach (var auto in list)
                {
                    Assert.IsNotNull(auto);
                }
            }
        }

        [TestMethod]
        public void FindQueueMaxAutoExtension_ShouldFindCorrectMax()
        {
            // Arrange
            Queue<List<Automobile>> queue = CreateMockQueue();

            // Redirect console output to capture it
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                Program.FindQueueMaxAutoExtension(queue);

                // Assert
                string result = sw.ToString();
                Assert.IsTrue(result.Contains("Максимальное количество авто = 3, номер цеха = 2"));
            }
        }

        [TestMethod]
        public void FindQueueMaxAutoLINQ_ShouldFindCorrectMax()
        {
            // Arrange
            Queue<List<Automobile>> queue = CreateMockQueue();

            // Redirect console output to capture it
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                Program.FindQueueMaxAutoLINQ(queue);

                // Assert
                string result = sw.ToString();
                Assert.IsTrue(result.Contains("Максимальное количество авто = 3, номер цеха = 2"));
            }
        }

        
    }
}
