using System;
using FluentAssertions;
using Xunit;

namespace Doomain.Shared.Tests
{
    public class Coder_Tests
    {
        [Fact]
        public void Code_Decode_String()
        {
            using ICoder coder = new ByteCoder();

            var content = "hello";

            var representation = coder
                  .Encode(content)
                  .Finilize();

            string decodedContent;

            coder
                .Init(representation)
                .Decode(out decodedContent);


            decodedContent.Should().Be(content);
        }
        [Fact]
        public void Code_Decode_Guid()
        {
            using ICoder coder = new ByteCoder();

            var content = Guid.NewGuid();

            var representation = coder
                  .Encode(content)
                  .Finilize();

            Guid decodedContent;

            coder
                .Init(representation)
                .Decode(out decodedContent);


            decodedContent.Should().Be(content);
        }
    }
}
