using ArgsSplitter.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgsSplitter.business
{
    public class ASplitter
    {

        private ASplitterSettings settings;

        public ASplitter(ASplitterSettings settings)
        {
            this.settings = settings;
        }

        public string Settings => JsonConvert.SerializeObject(settings, Formatting.Indented);

        public string CreateHelp(string title, string baseName) => HelpCreator.Create(settings, title, baseName);

        public ASplitter(string settings)
        {
            this.settings = JsonConvert.DeserializeObject<ASplitterSettings>(settings);
        }

        public Dictionary<string, string> ProcessArgs(string[] args)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (args.Length > 0)
                result = SeparateArgs(SeparatePairs(args));
            return result;
        }

        private List<ASParameterPair> SeparatePairs(string[] args)
        {
            List<ASParameterPair> result = new List<ASParameterPair>();

            foreach (string item in args)
            {
                if (!result.Any())
                    result.Add(new ASParameterPair("", new List<string>()));

                if (item.StartsWith("-"))
                    result.Add(new ASParameterPair(item, new List<string>()));
                else
                    result.Last().values.Add(item);
            }

            return result;
        }

        private Dictionary<string, string> SeparateArgs(List<ASParameterPair> pairs)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            List<KeyValuePair<string, string>> auxResult = new List<KeyValuePair<string, string>>();

            settings.Args.ForEach(item => auxResult.AddRange(FindArgs(item, pairs)));
            auxResult.ForEach(item => result.Add(item.Key, item.Value));

            return result;
        }

        private List<KeyValuePair<string, string>> FindArgs(Arg arg, List<ASParameterPair> pairs)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

            ASParameterPair pair = pairs.Find(item => arg.Commands.Contains(item.name));

            if (pair != null)
            {

                arg.Params.ForEach(par =>
                {
                    if (pair.values.Count() >= arg.Params.IndexOf(par) + 1)
                        result.Add(new KeyValuePair<string, string>(par.Key, pair.values[arg.Params.IndexOf(par)]));
                    else if (pair.values.Count() < arg.Params.IndexOf(par) + 1 && par.Optional)
                        result.Add(new KeyValuePair<string, string>(par.Key, par.Default));
                    else if (pair.values.Count() < arg.Params.IndexOf(par) + 1 && par.Void)
                        result.Add(new KeyValuePair<string, string>(par.Key, ""));
                    else if (pair.values.Count() < arg.Params.IndexOf(par) + 1 && !par.Optional)
                        throw new Exception($"Missing parameter {par.Id } on {arg.Id}.");
                });

                pairs.Remove(pair);
            }

            arg.Args?.ForEach(item => result.AddRange(FindArgs(item, pairs)));

            return result;
        }
    }
}
