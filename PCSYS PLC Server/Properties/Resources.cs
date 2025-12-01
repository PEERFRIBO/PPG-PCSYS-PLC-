// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Server.Properties.Resources
// Assembly: PCSYS PLC Server, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 4115DC54-E4D9-45A8-9B98-BF079FB0861D
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Server.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace PCSYS_PLC_Server.Properties;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
internal class Resources
{
  private static ResourceManager resourceMan;
  private static CultureInfo resourceCulture;

  internal Resources()
  {
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static ResourceManager ResourceManager
  {
    get
    {
      if (PCSYS_PLC_Server.Properties.Resources.resourceMan == null)
        PCSYS_PLC_Server.Properties.Resources.resourceMan = new ResourceManager("PCSYS_PLC_Server.Properties.Resources", typeof (PCSYS_PLC_Server.Properties.Resources).Assembly);
      return PCSYS_PLC_Server.Properties.Resources.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => PCSYS_PLC_Server.Properties.Resources.resourceCulture;
    set => PCSYS_PLC_Server.Properties.Resources.resourceCulture = value;
  }

  internal static Bitmap bullet_triangle_green
  {
    get
    {
      return (Bitmap) PCSYS_PLC_Server.Properties.Resources.ResourceManager.GetObject(nameof (bullet_triangle_green), PCSYS_PLC_Server.Properties.Resources.resourceCulture);
    }
  }

  internal static Bitmap document_add
  {
    get
    {
      return (Bitmap) PCSYS_PLC_Server.Properties.Resources.ResourceManager.GetObject(nameof (document_add), PCSYS_PLC_Server.Properties.Resources.resourceCulture);
    }
  }

  internal static Bitmap document_delete
  {
    get
    {
      return (Bitmap) PCSYS_PLC_Server.Properties.Resources.ResourceManager.GetObject(nameof (document_delete), PCSYS_PLC_Server.Properties.Resources.resourceCulture);
    }
  }
}
