using ArgsSplitter.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ArgsSplitter.business
{
    public class ASplitter
    {

        private ASplitterSettings settings;

        public ASplitter(ASplitterSettings settings)
        {
            this.settings = settings;
        }

        public ASplitter(string settings)
        {
            this.settings = JsonConvert.DeserializeObject<ASplitterSettings>(settings);
        }

        public Dictionary<string, string> ProcessArgs(string[] args)
        {
            try
            {
                Dictionary<string, string> result = new Dictionary<string, string>();


                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }




    }
}
