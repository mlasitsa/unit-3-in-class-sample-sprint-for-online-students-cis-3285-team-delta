﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sermo.UI.Contracts;
using Sermo.Markdown;

using NUnit.Framework;

using Moq;

// Changes for Sprint 1 -- I want to view a list of rooms that represent conversations. -- Max Lasitsa
// Pseudo-code: Initialize message record with room ID, author name, and text

// Changes for Sprint 2 -- As a system admin, I want to be able to set a limit to the number of users in any one room. -- Max Lasitsa
// Pseudo-code: Adjust user limit when editing room settings
namespace Sermo.UnitTests
{
    using Markdown = MarkdownDeep.Markdown;

    [TestFixture]
    public class MarkdownTests
    {
        [Test]
        [TestCase("This message has only paragraph markdown...",                "<p>This message has only paragraph markdown...</p>\n")]
        [TestCase("This message has *some emphasized* markdown...",             "<p>This message has <em>some emphasized</em> markdown...</p>\n")]
        [TestCase("This message has **some strongly emphasized** markdown...",  "<p>This message has <strong>some strongly emphasized</strong> markdown...</p>\n")]
        public void MessageTextIsAsExpectedAfterMarkdownTransform(string markdownText, string expectedText)
        {
            message1.Text = markdownText;
            var markdownDecorator = new RoomViewModelReaderMarkdownDecorator(mockRoomViewModelReader.Object, markdown);

            var roomMessages = markdownDecorator.GetRoomMessages(12345);

            var actualMessage = roomMessages.FirstOrDefault();

            Assert.That(actualMessage, Is.Not.Null);

            Assert.That(actualMessage.Text, Is.EqualTo(expectedText));
        }

        [SetUp]
        public void SetUp()
        {
            markdown = new Markdown();
            message1 = new MessageViewModel
            {
                AuthorName = "Dianne",
                ID = 1,
                RoomID = 12345,
                Text = "Test!"
            };         
            mockRoomViewModelReader = new Mock<IRoomViewModelReader>();
            var roomMessages = new MessageViewModel[] 
            {
                message1
            };
            mockRoomViewModelReader.Setup(reader => reader.GetRoomMessages(It.IsAny<int>())).Returns(roomMessages);
        }

        private MessageViewModel message1;
        private Mock<IRoomViewModelReader> mockRoomViewModelReader;
        private Markdown markdown;
    }
}
