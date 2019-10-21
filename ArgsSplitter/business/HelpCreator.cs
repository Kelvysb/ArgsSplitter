using ArgsSplitter.models;
using System.Text;

namespace ArgsSplitter.business
{
    class HelpCreator
    {
        public static string Create(ASplitterSettings settings, string title, string baseName)
        {
            StringBuilder result = new StringBuilder();

            if(!string.IsNullOrEmpty(title))
                result.AppendLine($"{title}:").AppendLine("");

            settings.Args.ForEach(arg => result.AppendLine(Mount(arg, 0, baseName)));
            
            return result.ToString();
        }

        private static string Mount(Arg arg, int level, string baseName)
        {
            StringBuilder result = new StringBuilder();

            result.Append('\t', level);

            if(!string.IsNullOrEmpty(baseName))
                result.Append($"{baseName} {string.Join("|", arg.Commands)} ");
            else
                result.Append($"{string.Join("|", arg.Commands)} ");

            arg.Params.ForEach(par =>
            {
                if(par.Optional)
                    result.Append($"[{par.Id}] ");
                else if(!par.Void)
                    result.Append($"<{par.Id}> ");
            });
            result.AppendLine($"{arg.Description}");
            arg.Params.ForEach(par =>
            {
                if (par.Optional && !string.IsNullOrEmpty(par.Description))
                    result.Append('\t', level + 1).AppendLine($"[{par.Id}]  {par.Description}");
                else if (!par.Void && !string.IsNullOrEmpty(par.Description))
                    result.Append('\t', level + 1).AppendLine($"<{par.Id}>  {par.Description}");
            });

            arg.Args?.ForEach(item => result.Append(Mount(item, level + 1, "")));

            return result.ToString();
        }
        
    }
}
