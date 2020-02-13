# ArgsSplitter
Command line args splitter and validator.

Install:

Nuget:
```
PM> Install-Package ArgsSplitter -Version 1.0.0
```


Usage Example:

```C#
 ASplitter splitter = new ASplitter(@"{
                                            ""args"": [
                                                {
                                                    ""commands"": [""""],
                                                    ""params"": [{""key"": ""PARAM1"", ""optional"": false}]
                                                }
                                            ]
                                        }");
            result = splitter.ProcessArgs(args);
```

* The result will be a Dictionary<string, string>


Settings:

Param with sub-param:
```Json
{
    "args": 
    [
        {
            "commands": [""],
            "params": [{"key": "PARAM1"}],
            "args": 
            [ 
                {
                    "commands": ["-test"],
                    "params": [{"key": "PARAM2"}]
                }
            ]
        }                                                        
    ]
}
```

Param with other optional param:
```Json
{
    "args": [
        {
            "commands": [""],
            "params": [{"key": "PARAM1"}]
        },
        {
            "commands": ["-test"],
            "params": [{"key": "PARAM2", "optional": true}]
        }
    ]
}
```

Param with other void param:
```Json
{
    "args": [
        {
            "commands": [""],
            "params": [{"key": "PARAM1"}]
        },
        {
            "commands": ["-test"],
            "params": [{"key": "PARAM2", "void": true}]
        }
    ]
}
```

All settings options:
```Json
{
  "args": [
    {
      "name": "",
      "commands": [
        "-test"
      ],
      "params": [
        {
          "name": "",
          "key": "",
          "optional": false,
          "default": "",
          "description": "",
          "void": false
        }
      ],
      "args": [],
      "description": ""
    }
  ]
}
```
