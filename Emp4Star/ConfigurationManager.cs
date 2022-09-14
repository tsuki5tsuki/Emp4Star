﻿using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Emp4Star
{
  static class ConfigurationManager
  {
    public static IConfiguration AppSetting { get; }
    static ConfigurationManager()
    {
      AppSetting = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();
    }
  }
}
