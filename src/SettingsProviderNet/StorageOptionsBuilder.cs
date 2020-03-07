﻿using System;
using System.IO;

namespace SettingsProviderNet
{
  /// <summary> 
  /// Necessarily you should specify AppName xor SpecifyPathToConfigFile.
  /// Optionally you might set SpecialFolder, FileName, etc.
  /// </summary>
  public class StorageOptionsBuilder
  {
    public StorageOptionsBuilder SpecifyPathToConfigFile(string path)
    {
      if (string.IsNullOrEmpty(path))
        throw new ArgumentException(nameof(path));

      if (_config.SpecialFolder != null)
        throw new InvalidOperationException("target special folder already specified");

      try
      {
        var dir = Path.GetDirectoryName(path);
        var name = Path.GetFileNameWithoutExtension(path);
        var ext = Path.GetExtension(path);
      }
      catch (Exception ex)
      {
        throw new ArgumentException(ex.Message);
      }

      _config.TargetFileName = path;
      return this;
    }

    public StorageOptionsBuilder SetFolder(Environment.SpecialFolder specialFolder)
    {
      if (_config.TargetFileName != null)
        throw new InvalidOperationException("Target file already specified");

      _config.SpecialFolder = specialFolder;
      return this;
    }

    public StorageOptionsBuilder SetAppName(string appName)
    {
      if (_config.TargetFileName != null)
        throw new InvalidOperationException("Target file already specified");

      _config.AppName = appName;
      return this;
    }

    public StorageOptionsBuilder FileName(string fileName)
    {
      if (_config.TargetFileName != null)
        throw new InvalidOperationException("Target file already specified");

      _config.FileName = fileName;
      return this;
    }

    /// <summary>
    /// Automaticaly settings file creation if not exist
    /// </summary>
    public StorageOptionsBuilder CreateIfNotExist(bool value)
    {
      _config.CreateIfNotExist = value;
      return this;
    }

    public StorageOptions Build()
    {
      if (_config.TargetFileName != null)
        return _config;

      if (_config.AppName == null)
        throw new InvalidOperationException($"{nameof(_config.AppName)} expected");

      if (_config.FileName == null)
        _config.FileName = DefaultFileName;

      if (_config.SpecialFolder == null)
        _config.SpecialFolder = DefaultSpecialFolder;

      return _config;
    }

    private readonly StorageOptions _config = new StorageOptions();
    private const string DefaultFileName = "Settings.json";
    private const Environment.SpecialFolder DefaultSpecialFolder = Environment.SpecialFolder.LocalApplicationData;
  }
}