using ArgsSplitter.business;
using ArgsSplitter.models;
using System;
using Xunit;

namespace ArgsSplitterTests
{
    public class SplitterTests
    {
        [Fact]
        public void SingleArgumentTest()
        {
            ASplitter splitter = new ASplitter(new ASplitterSettings());
        }
    }
}
