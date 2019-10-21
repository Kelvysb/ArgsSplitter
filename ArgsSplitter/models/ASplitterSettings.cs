using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ArgsSplitter.models
{
    public class ASplitterSettings
    {

        public ASplitterSettings(List<Arg> args)
        {
            Args = args;
        }

        [JsonProperty("args")]
        public List<Arg> Args { get; private set; }
    }

    public class Arg
    {

        public Arg(string name, List<string> commands, List<Param> @params, List<Arg> args, string description)
        {
            Name = name;
            Commands = commands;
            Params = @params;
            Args = args;
            Description = description;
        }

        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue("")]
        public string Name { get; private set; }

        [JsonProperty("commands"), JsonRequired]
        public List<string> Commands { get; private set; }

        [JsonProperty("params"), JsonRequired]
        public List<Param> Params { get; private set; }

        [JsonProperty("args")]
        public List<Arg> Args { get; private set; }


        [JsonProperty("description", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue("")]
        public string Description { get; private set; }

        [JsonIgnore]
        public string Id => !string.IsNullOrEmpty(Name) ? Name : 
                                !string.IsNullOrEmpty(Description) ? Description : 
                                    !string.IsNullOrEmpty(Commands.First()) ? Commands.First() : "command";

    }

    public class Param
    {
        public Param(string name, string key, bool optional, string @default, string description, bool @void)
        {
            Name = name;
            Key = key;
            Optional = optional;
            Default = @default;
            Description = description;
            Void = @void;
        }

        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue("")]
        public string Name { get; private set; }

        [JsonProperty("key"), JsonRequired]
        public string Key { get; private set; }

        [JsonProperty("optional", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(false)]
        public bool Optional { get; private set; }
      
        [JsonProperty("default", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue("")]
        public string Default { get; private set; }

        [JsonProperty("description", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue("")]
        public string Description { get; private set; }

        [JsonProperty("void", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(false)]        
        public bool Void { get; private set; }

        [JsonIgnore]
        public string Id => !string.IsNullOrEmpty(Name) ? Name : 
                                !string.IsNullOrEmpty(Description) ? Description : Key;

    }
}
