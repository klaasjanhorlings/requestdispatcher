using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.Test.RequestDispatcher
{
    public abstract class SendContentBase: SendBase
    {
        public virtual async Task CallsSerializeWithPassedContent()
        {
            // Act
            await CallSend(HttpMethod.Get, "http://localhost");

            // Assert
            serializerMock.Verify(s => s.Serialize(requestContent));
        }

        public virtual async Task SetsSerializedContentToRequestMessage()
        {
            // Act
            await CallSend(HttpMethod.Get, "http://localhost");

            // Assert
            Assert.AreEqual(serializedRequestContent, messageHandler.LastRequestMessage.Content);
        }

        public virtual async Task CallsDeserializeWithReturnedContent()
        {
            messageHandler.ResponseHandler = r => responseMessage;

            await CallSend(HttpMethod.Get, "http://localhost");

            Assert.AreEqual(1, messageHandler.CallCount);
            serializerMock.Verify(s => s.DeserializeAsync<object>(serializedResponseContent));
        }

        public virtual async Task ReturnsDeserializedResult()
        {
            messageHandler.ResponseHandler = r => responseMessage;

            await CallSend(HttpMethod.Get, "http://localhost");

            Assert.AreEqual(1, messageHandler.CallCount);
            Assert.AreEqual(responseContent, sendResult);
        }
    }
}
