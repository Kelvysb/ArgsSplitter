using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArgsSplitter.models
{

    [JsonObject("settings")]
    public class ASplitterSettings
    {
        public ASplitterSettings(List<Arg> args)
        {
            Args = args;
        }

        [JsonProperty("args")]
        public List<Arg> Args { get; private set; }
    }

    [JsonObject("arg")]
    public class Arg
    {
        public Arg(List<string> commands, List<string> @params, List<Arg> args, string description)
        {
            Commands = commands;
            Params = @params;
            Args = args;
            Description = description;
        }

        [JsonProperty("commands")]
        public List<string> Commands { get; private set; }

        [JsonProperty("params")]
        public List<string> Params { get; private set; }

        [JsonProperty("args")]
        public List<Arg> Args { get; private set; }

        [JsonProperty("description")]
        public string Description { get; private set; }
    }

    [JsonObject("param")]
    public class Param
    {
        public Param(string name, bool optional, string @default, string description)
        {
            Name = name;
            Optional = optional;
            Default = @default;
            Description = description;
        }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("optional")]
        public bool Optional { get; private set; }

        [JsonProperty("default")]
        public string Default { get; private set; }

        [JsonProperty("description")]
        public string Description { get; private set; }
    }
}
