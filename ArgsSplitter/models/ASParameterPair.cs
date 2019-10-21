using System;
using System.Collections.Generic;
using System.Text;

namespace ArgsSplitter.models
{
    class ASParameterPair
    {
        public ASParameterPair(string name, List<string> values)
        {
            this.name = name;
            this.values = values;
        }

        public string name { get; private set; }
        public List<string> values { get; private set; }
    }
}
