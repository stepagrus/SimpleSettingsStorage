﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SettingsProviderNet
{
  public class StorageOptions
  {
    internal StorageOptions() { }

    public static StorageOptionsBuilder Create() => new StorageOptionsBuilder();

    public bool CreateIfNotExist { get; internal set; }

    public Environment.SpecialFolder? SpecialFolder { get; internal set; }

    public string AppName { get; internal set; }

    public string FileName { get; internal set; }

    public string TargetFileName { get; internal set; }

    public string GetPath()
    {
      return TargetFileName ?? Path.Combine(Environment.GetFolderPath(SpecialFolder.Value), AppName, TargetFileName);
    }
  }
}