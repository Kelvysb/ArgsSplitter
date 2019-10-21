using ArgsSplitter.business;
using ArgsSplitter.models;
using System;
using System.Collections.Generic;
using Xunit;

namespace ArgsSplitterTests
{
    public class SplitterTests
    {
        [Fact]
        public void SingleArgument()
        {

            Dictionary<string, string> expected = new Dictionary<string, string>();
            Dictionary<string, string> result = new Dictionary<string, string>();
            expected.Add("PARAM1", "testData");
            ASplitter splitter = new ASplitter(@"{
                                                    ""args"": [
                                                        {
                                                            ""commands"": [""""],
                                                            ""params"": [{""key"": ""PARAM1"", ""optional"": false}]
                                                        }
                                                    ]
                                                }");
            string[] input = { "testData" };
            result = splitter.ProcessArgs(input);

            Assert.Equal(expected.Keys.Count, result.Keys.Count);
            Assert.NotNull(result.GetValueOrDefault("PARAM1"));
            Assert.Equal(result.GetValueOrDefault("PARAM1"), result.GetValueOrDefault("PARAM1"));
        }

        [Fact]
        public void SingleNamedArgument()
        {

            Dictionary<string, string> expected = new Dictionary<string, string>();
            Dictionary<string, string> result = new Dictionary<string, string>();
            expected.Add("PARAM1", "testData");
            ASplitter splitter = new ASplitter(@"{
                                                    ""args"": [
                                                        {
                                                            ""commands"": [""-test""],
                                                            ""params"": [{""key"": ""PARAM1""}]
                                                        }
                                                    ]
                                                }");
            string[] input = { "-test", "testData" };
            result = splitter.ProcessArgs(input);

            Assert.Equal(expected.Keys.Count, result.Keys.Count);
            Assert.NotNull(result.GetValueOrDefault("PARAM1"));
            Assert.Equal(result.GetValueOrDefault("PARAM1"), result.GetValueOrDefault("PARAM1"));
        }

        [Fact]
        public void SingleNamedArgumentWithOptionalAndDefaultValue()
        {

            Dictionary<string, string> expected = new Dictionary<string, string>();
            Dictionary<string, string> result = new Dictionary<string, string>();
            expected.Add("PARAM1", "testData");
            ASplitter splitter = new ASplitter(@"{
                                                    ""args"": [
                                                        {
                                                            ""commands"": [""-test""],
                                                            ""params"": [{""key"": ""PARAM1"", ""optional"": true, ""default"": ""testData""}]
                                                        }
                                                    ]
                                                }");
            string[] input = { "-test" };
            result = splitter.ProcessArgs(input);

            Assert.Equal(expected.Keys.Count, result.Keys.Count);
            Assert.NotNull(result.GetValueOrDefault("PARAM1"));
            Assert.Equal(result.GetValueOrDefault("PARAM1"), result.GetValueOrDefault("PARAM1"));
        }

        [Fact]
        public void MultipleArgument()
        {

            Dictionary<string, string> expected = new Dictionary<string, string>();
            Dictionary<string, string> result = new Dictionary<string, string>();
            expected.Add("PARAM1", "testData1");
            expected.Add("PARAM2", "testData2");
            ASplitter splitter = new ASplitter(@"{
                                                    ""args"": [
                                                        {
                                                            ""commands"": [""""],
                                                            ""params"": [{""key"": ""PARAM1""},
                                                                         {""key"": ""PARAM2""}]
                                                        }
                                                    ]
                                                }");
            string[] input = { "testData1", "testData2" };
            result = splitter.ProcessArgs(input);

            Assert.Equal(expected.Keys.Count, result.Keys.Count);
            Assert.NotNull(result.GetValueOrDefault("PARAM1"));
            Assert.NotNull(result.GetValueOrDefault("PARAM2"));
            Assert.Equal(result.GetValueOrDefault("PARAM1"), result.GetValueOrDefault("PARAM1"));
            Assert.Equal(result.GetValueOrDefault("PARAM2"), result.GetValueOrDefault("PARAM2"));
        }

        [Fact]
        public void MultipleNamedArgument()
        {

            Dictionary<string, string> expected = new Dictionary<string, string>();
            Dictionary<string, string> result = new Dictionary<string, string>();
            expected.Add("PARAM1", "testData1");
            expected.Add("PARAM2", "testData2");
            ASplitter splitter = new ASplitter(@"{
                                                    ""args"": [
                                                        {
                                                            ""commands"": [""-test""],
                                                            ""params"": [{""key"": ""PARAM1""},
                                                                         {""key"": ""PARAM2""}]
                                                        }
                                                    ]
                                                }");
            string[] input = { "-test", "testData1", "testData2" };
            result = splitter.ProcessArgs(input);

            Assert.Equal(expected.Keys.Count, result.Keys.Count);
            Assert.NotNull(result.GetValueOrDefault("PARAM1"));
            Assert.NotNull(result.GetValueOrDefault("PARAM2"));
            Assert.Equal(result.GetValueOrDefault("PARAM1"), result.GetValueOrDefault("PARAM1"));
            Assert.Equal(result.GetValueOrDefault("PARAM2"), result.GetValueOrDefault("PARAM2"));
        }

        [Fact]
        public void SingleArgumentPlusVoidNamedArgument()
        {

            Dictionary<string, string> expected = new Dictionary<string, string>();
            Dictionary<string, string> result = new Dictionary<string, string>();
            expected.Add("PARAM1", "testData1");
            expected.Add("PARAM2", "");
            ASplitter splitter = new ASplitter(@"{
                                                    ""args"": [
                                                        {
                                                            ""commands"": [""""],
                                                            ""params"": [{""key"": ""PARAM1""}]
                                                        },
                                                        {
                                                            ""commands"": [""-test""],
                                                            ""params"": [{""key"": ""PARAM2"", ""void"": true}]
                                                        }
                                                    ]
                                                }");
            string[] input = { "testData1", "-test" };
            result = splitter.ProcessArgs(input);

            Assert.Equal(expected.Keys.Count, result.Keys.Count);
            Assert.NotNull(result.GetValueOrDefault("PARAM1"));
            Assert.NotNull(result.GetValueOrDefault("PARAM2"));
            Assert.Equal(result.GetValueOrDefault("PARAM1"), result.GetValueOrDefault("PARAM1"));
            Assert.Equal(result.GetValueOrDefault("PARAM2"), result.GetValueOrDefault("PARAM2"));
        }

        [Fact]
        public void NestedArguments()
        {

            Dictionary<string, string> expected = new Dictionary<string, string>();
            Dictionary<string, string> result = new Dictionary<string, string>();
            expected.Add("PARAM1", "testData1");
            expected.Add("PARAM2", "testData2");
            ASplitter splitter = new ASplitter(@"{
                                                    ""args"": 
                                                    [
                                                        {
                                                            ""commands"": [""""],
                                                            ""params"": [{""key"": ""PARAM1""}],
                                                            ""args"": 
                                                            [ 
                                                                {
                                                                    ""commands"": [""-test""],
                                                                    ""params"": [{""key"": ""PARAM2""}]
                                                                }
                                                            ]
                                                        }                                                        
                                                    ]
                                                }");
            string[] input = { "testData1", "-test", "testData2" };
            result = splitter.ProcessArgs(input);

            Assert.Equal(expected.Keys.Count, result.Keys.Count);
            Assert.NotNull(result.GetValueOrDefault("PARAM1"));
            Assert.NotNull(result.GetValueOrDefault("PARAM2"));
            Assert.Equal(result.GetValueOrDefault("PARAM1"), result.GetValueOrDefault("PARAM1"));
            Assert.Equal(result.GetValueOrDefault("PARAM2"), result.GetValueOrDefault("PARAM2"));
        }

        [Fact]
        public void NestedArgumentsPlusIndependentFlag()
        {

            Dictionary<string, string> expected = new Dictionary<string, string>();
            Dictionary<string, string> result = new Dictionary<string, string>();
            expected.Add("PARAM1", "testData1");
            expected.Add("PARAM2", "testData2");
            expected.Add("PARAM3", "");
            ASplitter splitter = new ASplitter(@"{
                                                    ""args"": 
                                                    [
                                                        {
                                                            ""commands"": [""""],
                                                            ""params"": [{""key"": ""PARAM1""}],
                                                            ""args"": 
                                                            [ 
                                                                {
                                                                    ""commands"": [""-test1""],
                                                                    ""params"": [{""key"": ""PARAM2""}]
                                                                }
                                                            ]
                                                        },
                                                        {
                                                            ""commands"": [""-test2""],
                                                            ""params"": [{""key"": ""PARAM3"", ""void"": true}]
                                                        }
                                                    ]
                                                }");
            string[] input = { "testData1", "-test1", "testData2", "-test2" };
            result = splitter.ProcessArgs(input);

            Assert.Equal(expected.Keys.Count, result.Keys.Count);
            Assert.NotNull(result.GetValueOrDefault("PARAM1"));
            Assert.NotNull(result.GetValueOrDefault("PARAM2"));
            Assert.NotNull(result.GetValueOrDefault("PARAM3"));
            Assert.Equal(result.GetValueOrDefault("PARAM1"), result.GetValueOrDefault("PARAM1"));
            Assert.Equal(result.GetValueOrDefault("PARAM2"), result.GetValueOrDefault("PARAM2"));
            Assert.Equal(result.GetValueOrDefault("PARAM3"), result.GetValueOrDefault("PARAM3"));
        }


        [Fact]
        public void CreateHelp()
        {

            string expected = @"test title:

program  <param1> test arg description 1
	<param1>  Description 1
	--test1|-t <param2> test arg description 2
		<param2>  description 2

program -test2|-s [param4] 
	[param4]  optional 1

";
            string result = "";

            ASplitter splitter = new ASplitter(@"{
                                                    ""args"": 
                                                    [
                                                        {
                                                            ""name"": ""testArg"",
                                                            ""description"": ""test arg description 1"",
                                                            ""commands"": [""""],
                                                            ""params"": [{""key"": ""PARAM1"", ""description"": ""Description 1"", ""name"": ""param1""}],
                                                            ""args"": 
                                                            [ 
                                                                {
                                                                    ""name"": ""testArg2"",
                                                                    ""description"": ""test arg description 2"",
                                                                    ""commands"": [""--test1"", ""-t""],
                                                                    ""params"": [{""key"": ""PARAM2"", ""description"": ""description 2"", ""name"": ""param2""}]
                                                                }
                                                            ]
                                                        },
                                                        {
                                                            ""commands"": [""-test2"", ""-s""],
                                                            ""params"": [{""key"": ""PARAM3"", ""void"": true, ""description"": ""void 1"", ""name"": ""param3""},
                                                                         {""key"": ""PARAM4"", ""optional"": true, ""description"": ""optional 1"", ""name"": ""param4""}]                            
                                                        }
                                                    ]
                                                }");

            result = splitter.CreateHelp("test title", "program");

            Assert.Equal(expected, result);

        }


    }
}
