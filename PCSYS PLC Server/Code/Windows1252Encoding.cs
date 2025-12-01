// Decompiled with JetBrains decompiler
// Type: PCSYS_PLC_Server.Code.Windows1252Encoding
// Assembly: PCSYS PLC Server, Version=1.0.6.0, Culture=neutral, PublicKeyToken=28b8bb8dc6f31562
// MVID: 4115DC54-E4D9-45A8-9B98-BF079FB0861D
// Assembly location: C:\Users\PEFR\source\repos\PPG2025\PCSYS PLC Server.exe

using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PCSYS_PLC_Server.Code;

public class Windows1252Encoding : Encoding
{
  private char? fallbackCharacter;
  private static char[] byteToChar = new char[256 /*0x0100*/]
  {
    char.MinValue,
    '\u0001',
    '\u0002',
    '\u0003',
    '\u0004',
    '\u0005',
    '\u0006',
    '\a',
    '\b',
    '\t',
    '\n',
    '\v',
    '\f',
    '\r',
    '\u000E',
    '\u000F',
    '\u0010',
    '\u0011',
    '\u0012',
    '\u0013',
    '\u0014',
    '\u0015',
    '\u0016',
    '\u0017',
    '\u0018',
    '\u0019',
    '\u001A',
    '\u001B',
    '\u001C',
    '\u001D',
    '\u001E',
    '\u001F',
    ' ',
    '!',
    '"',
    '#',
    '$',
    '%',
    '&',
    '\'',
    '(',
    ')',
    '*',
    '+',
    ',',
    '-',
    '.',
    '/',
    '0',
    '1',
    '2',
    '3',
    '4',
    '5',
    '6',
    '7',
    '8',
    '9',
    ':',
    ';',
    '<',
    '=',
    '>',
    '?',
    '@',
    'A',
    'B',
    'C',
    'D',
    'E',
    'F',
    'G',
    'H',
    'I',
    'J',
    'K',
    'L',
    'M',
    'N',
    'O',
    'P',
    'Q',
    'R',
    'S',
    'T',
    'U',
    'V',
    'W',
    'X',
    'Y',
    'Z',
    '[',
    '\\',
    ']',
    '^',
    '_',
    '`',
    'a',
    'b',
    'c',
    'd',
    'e',
    'f',
    'g',
    'h',
    'i',
    'j',
    'k',
    'l',
    'm',
    'n',
    'o',
    'p',
    'q',
    'r',
    's',
    't',
    'u',
    'v',
    'w',
    'x',
    'y',
    'z',
    '{',
    '|',
    '}',
    '~',
    '\u007F',
    '€',
    '\u0081',
    '‚',
    'ƒ',
    '„',
    '…',
    '†',
    '‡',
    'ˆ',
    '‰',
    'Š',
    '‹',
    'Œ',
    '\u008D',
    'Ž',
    '\u008F',
    '\u0090',
    '‘',
    '’',
    '“',
    '”',
    '•',
    '–',
    '—',
    '˜',
    '™',
    'š',
    '›',
    'œ',
    '\u009D',
    'ž',
    'Ÿ',
    ' ',
    '¡',
    '¢',
    '£',
    '¤',
    '¥',
    '¦',
    '§',
    '¨',
    '©',
    'ª',
    '«',
    '¬',
    '\u00AD',
    '®',
    '¯',
    '°',
    '±',
    '\u00B2',
    '\u00B3',
    '´',
    'µ',
    '¶',
    '·',
    '¸',
    '\u00B9',
    'º',
    '»',
    '\u00BC',
    '\u00BD',
    '\u00BE',
    '¿',
    'À',
    'Á',
    'Â',
    'Ã',
    'Ä',
    'Å',
    'Æ',
    'Ç',
    'È',
    'É',
    'Ê',
    'Ë',
    'Ì',
    'Í',
    'Î',
    'Ï',
    'Ð',
    'Ñ',
    'Ò',
    'Ó',
    'Ô',
    'Õ',
    'Ö',
    '×',
    'Ø',
    'Ù',
    'Ú',
    'Û',
    'Ü',
    'Ý',
    'Þ',
    'ß',
    'à',
    'á',
    'â',
    'ã',
    'ä',
    'å',
    'æ',
    'ç',
    'è',
    'é',
    'ê',
    'ë',
    'ì',
    'í',
    'î',
    'ï',
    'ð',
    'ñ',
    'ò',
    'ó',
    'ô',
    'õ',
    'ö',
    '÷',
    'ø',
    'ù',
    'ú',
    'û',
    'ü',
    'ý',
    'þ',
    'ÿ'
  };
  private static Dictionary<char, byte> charToByte = new Dictionary<char, byte>()
  {
    {
      char.MinValue,
      (byte) 0
    },
    {
      '\u0001',
      (byte) 1
    },
    {
      '\u0002',
      (byte) 2
    },
    {
      '\u0003',
      (byte) 3
    },
    {
      '\u0004',
      (byte) 4
    },
    {
      '\u0005',
      (byte) 5
    },
    {
      '\u0006',
      (byte) 6
    },
    {
      '\a',
      (byte) 7
    },
    {
      '\b',
      (byte) 8
    },
    {
      '\t',
      (byte) 9
    },
    {
      '\n',
      (byte) 10
    },
    {
      '\v',
      (byte) 11
    },
    {
      '\f',
      (byte) 12
    },
    {
      '\r',
      (byte) 13
    },
    {
      '\u000E',
      (byte) 14
    },
    {
      '\u000F',
      (byte) 15
    },
    {
      '\u0010',
      (byte) 16 /*0x10*/
    },
    {
      '\u0011',
      (byte) 17
    },
    {
      '\u0012',
      (byte) 18
    },
    {
      '\u0013',
      (byte) 19
    },
    {
      '\u0014',
      (byte) 20
    },
    {
      '\u0015',
      (byte) 21
    },
    {
      '\u0016',
      (byte) 22
    },
    {
      '\u0017',
      (byte) 23
    },
    {
      '\u0018',
      (byte) 24
    },
    {
      '\u0019',
      (byte) 25
    },
    {
      '\u001A',
      (byte) 26
    },
    {
      '\u001B',
      (byte) 27
    },
    {
      '\u001C',
      (byte) 28
    },
    {
      '\u001D',
      (byte) 29
    },
    {
      '\u001E',
      (byte) 30
    },
    {
      '\u001F',
      (byte) 31 /*0x1F*/
    },
    {
      ' ',
      (byte) 32 /*0x20*/
    },
    {
      '!',
      (byte) 33
    },
    {
      '"',
      (byte) 34
    },
    {
      '#',
      (byte) 35
    },
    {
      '$',
      (byte) 36
    },
    {
      '%',
      (byte) 37
    },
    {
      '&',
      (byte) 38
    },
    {
      '\'',
      (byte) 39
    },
    {
      '(',
      (byte) 40
    },
    {
      ')',
      (byte) 41
    },
    {
      '*',
      (byte) 42
    },
    {
      '+',
      (byte) 43
    },
    {
      ',',
      (byte) 44
    },
    {
      '-',
      (byte) 45
    },
    {
      '.',
      (byte) 46
    },
    {
      '/',
      (byte) 47
    },
    {
      '0',
      (byte) 48 /*0x30*/
    },
    {
      '1',
      (byte) 49
    },
    {
      '2',
      (byte) 50
    },
    {
      '3',
      (byte) 51
    },
    {
      '4',
      (byte) 52
    },
    {
      '5',
      (byte) 53
    },
    {
      '6',
      (byte) 54
    },
    {
      '7',
      (byte) 55
    },
    {
      '8',
      (byte) 56
    },
    {
      '9',
      (byte) 57
    },
    {
      ':',
      (byte) 58
    },
    {
      ';',
      (byte) 59
    },
    {
      '<',
      (byte) 60
    },
    {
      '=',
      (byte) 61
    },
    {
      '>',
      (byte) 62
    },
    {
      '?',
      (byte) 63 /*0x3F*/
    },
    {
      '@',
      (byte) 64 /*0x40*/
    },
    {
      'A',
      (byte) 65
    },
    {
      'B',
      (byte) 66
    },
    {
      'C',
      (byte) 67
    },
    {
      'D',
      (byte) 68
    },
    {
      'E',
      (byte) 69
    },
    {
      'F',
      (byte) 70
    },
    {
      'G',
      (byte) 71
    },
    {
      'H',
      (byte) 72
    },
    {
      'I',
      (byte) 73
    },
    {
      'J',
      (byte) 74
    },
    {
      'K',
      (byte) 75
    },
    {
      'L',
      (byte) 76
    },
    {
      'M',
      (byte) 77
    },
    {
      'N',
      (byte) 78
    },
    {
      'O',
      (byte) 79
    },
    {
      'P',
      (byte) 80 /*0x50*/
    },
    {
      'Q',
      (byte) 81
    },
    {
      'R',
      (byte) 82
    },
    {
      'S',
      (byte) 83
    },
    {
      'T',
      (byte) 84
    },
    {
      'U',
      (byte) 85
    },
    {
      'V',
      (byte) 86
    },
    {
      'W',
      (byte) 87
    },
    {
      'X',
      (byte) 88
    },
    {
      'Y',
      (byte) 89
    },
    {
      'Z',
      (byte) 90
    },
    {
      '[',
      (byte) 91
    },
    {
      '\\',
      (byte) 92
    },
    {
      ']',
      (byte) 93
    },
    {
      '^',
      (byte) 94
    },
    {
      '_',
      (byte) 95
    },
    {
      '`',
      (byte) 96 /*0x60*/
    },
    {
      'a',
      (byte) 97
    },
    {
      'b',
      (byte) 98
    },
    {
      'c',
      (byte) 99
    },
    {
      'd',
      (byte) 100
    },
    {
      'e',
      (byte) 101
    },
    {
      'f',
      (byte) 102
    },
    {
      'g',
      (byte) 103
    },
    {
      'h',
      (byte) 104
    },
    {
      'i',
      (byte) 105
    },
    {
      'j',
      (byte) 106
    },
    {
      'k',
      (byte) 107
    },
    {
      'l',
      (byte) 108
    },
    {
      'm',
      (byte) 109
    },
    {
      'n',
      (byte) 110
    },
    {
      'o',
      (byte) 111
    },
    {
      'p',
      (byte) 112 /*0x70*/
    },
    {
      'q',
      (byte) 113
    },
    {
      'r',
      (byte) 114
    },
    {
      's',
      (byte) 115
    },
    {
      't',
      (byte) 116
    },
    {
      'u',
      (byte) 117
    },
    {
      'v',
      (byte) 118
    },
    {
      'w',
      (byte) 119
    },
    {
      'x',
      (byte) 120
    },
    {
      'y',
      (byte) 121
    },
    {
      'z',
      (byte) 122
    },
    {
      '{',
      (byte) 123
    },
    {
      '|',
      (byte) 124
    },
    {
      '}',
      (byte) 125
    },
    {
      '~',
      (byte) 126
    },
    {
      '\u007F',
      (byte) 127 /*0x7F*/
    },
    {
      '€',
      (byte) 128 /*0x80*/
    },
    {
      '\u0081',
      (byte) 129
    },
    {
      '‚',
      (byte) 130
    },
    {
      'ƒ',
      (byte) 131
    },
    {
      '„',
      (byte) 132
    },
    {
      '…',
      (byte) 133
    },
    {
      '†',
      (byte) 134
    },
    {
      '‡',
      (byte) 135
    },
    {
      'ˆ',
      (byte) 136
    },
    {
      '‰',
      (byte) 137
    },
    {
      'Š',
      (byte) 138
    },
    {
      '‹',
      (byte) 139
    },
    {
      'Œ',
      (byte) 140
    },
    {
      '\u008D',
      (byte) 141
    },
    {
      'Ž',
      (byte) 142
    },
    {
      '\u008F',
      (byte) 143
    },
    {
      '\u0090',
      (byte) 144 /*0x90*/
    },
    {
      '‘',
      (byte) 145
    },
    {
      '’',
      (byte) 146
    },
    {
      '“',
      (byte) 147
    },
    {
      '”',
      (byte) 148
    },
    {
      '•',
      (byte) 149
    },
    {
      '–',
      (byte) 150
    },
    {
      '—',
      (byte) 151
    },
    {
      '˜',
      (byte) 152
    },
    {
      '™',
      (byte) 153
    },
    {
      'š',
      (byte) 154
    },
    {
      '›',
      (byte) 155
    },
    {
      'œ',
      (byte) 156
    },
    {
      '\u009D',
      (byte) 157
    },
    {
      'ž',
      (byte) 158
    },
    {
      'Ÿ',
      (byte) 159
    },
    {
      ' ',
      (byte) 160 /*0xA0*/
    },
    {
      '¡',
      (byte) 161
    },
    {
      '¢',
      (byte) 162
    },
    {
      '£',
      (byte) 163
    },
    {
      '¤',
      (byte) 164
    },
    {
      '¥',
      (byte) 165
    },
    {
      '¦',
      (byte) 166
    },
    {
      '§',
      (byte) 167
    },
    {
      '¨',
      (byte) 168
    },
    {
      '©',
      (byte) 169
    },
    {
      'ª',
      (byte) 170
    },
    {
      '«',
      (byte) 171
    },
    {
      '¬',
      (byte) 172
    },
    {
      '\u00AD',
      (byte) 173
    },
    {
      '®',
      (byte) 174
    },
    {
      '¯',
      (byte) 175
    },
    {
      '°',
      (byte) 176 /*0xB0*/
    },
    {
      '±',
      (byte) 177
    },
    {
      '\u00B2',
      (byte) 178
    },
    {
      '\u00B3',
      (byte) 179
    },
    {
      '´',
      (byte) 180
    },
    {
      'µ',
      (byte) 181
    },
    {
      '¶',
      (byte) 182
    },
    {
      '·',
      (byte) 183
    },
    {
      '¸',
      (byte) 184
    },
    {
      '\u00B9',
      (byte) 185
    },
    {
      'º',
      (byte) 186
    },
    {
      '»',
      (byte) 187
    },
    {
      '\u00BC',
      (byte) 188
    },
    {
      '\u00BD',
      (byte) 189
    },
    {
      '\u00BE',
      (byte) 190
    },
    {
      '¿',
      (byte) 191
    },
    {
      'À',
      (byte) 192 /*0xC0*/
    },
    {
      'Á',
      (byte) 193
    },
    {
      'Â',
      (byte) 194
    },
    {
      'Ã',
      (byte) 195
    },
    {
      'Ä',
      (byte) 196
    },
    {
      'Å',
      (byte) 197
    },
    {
      'Æ',
      (byte) 198
    },
    {
      'Ç',
      (byte) 199
    },
    {
      'È',
      (byte) 200
    },
    {
      'É',
      (byte) 201
    },
    {
      'Ê',
      (byte) 202
    },
    {
      'Ë',
      (byte) 203
    },
    {
      'Ì',
      (byte) 204
    },
    {
      'Í',
      (byte) 205
    },
    {
      'Î',
      (byte) 206
    },
    {
      'Ï',
      (byte) 207
    },
    {
      'Ð',
      (byte) 208 /*0xD0*/
    },
    {
      'Ñ',
      (byte) 209
    },
    {
      'Ò',
      (byte) 210
    },
    {
      'Ó',
      (byte) 211
    },
    {
      'Ô',
      (byte) 212
    },
    {
      'Õ',
      (byte) 213
    },
    {
      'Ö',
      (byte) 214
    },
    {
      '×',
      (byte) 215
    },
    {
      'Ø',
      (byte) 216
    },
    {
      'Ù',
      (byte) 217
    },
    {
      'Ú',
      (byte) 218
    },
    {
      'Û',
      (byte) 219
    },
    {
      'Ü',
      (byte) 220
    },
    {
      'Ý',
      (byte) 221
    },
    {
      'Þ',
      (byte) 222
    },
    {
      'ß',
      (byte) 223
    },
    {
      'à',
      (byte) 224 /*0xE0*/
    },
    {
      'á',
      (byte) 225
    },
    {
      'â',
      (byte) 226
    },
    {
      'ã',
      (byte) 227
    },
    {
      'ä',
      (byte) 228
    },
    {
      'å',
      (byte) 229
    },
    {
      'æ',
      (byte) 230
    },
    {
      'ç',
      (byte) 231
    },
    {
      'è',
      (byte) 232
    },
    {
      'é',
      (byte) 233
    },
    {
      'ê',
      (byte) 234
    },
    {
      'ë',
      (byte) 235
    },
    {
      'ì',
      (byte) 236
    },
    {
      'í',
      (byte) 237
    },
    {
      'î',
      (byte) 238
    },
    {
      'ï',
      (byte) 239
    },
    {
      'ð',
      (byte) 240 /*0xF0*/
    },
    {
      'ñ',
      (byte) 241
    },
    {
      'ò',
      (byte) 242
    },
    {
      'ó',
      (byte) 243
    },
    {
      'ô',
      (byte) 244
    },
    {
      'õ',
      (byte) 245
    },
    {
      'ö',
      (byte) 246
    },
    {
      '÷',
      (byte) 247
    },
    {
      'ø',
      (byte) 248
    },
    {
      'ù',
      (byte) 249
    },
    {
      'ú',
      (byte) 250
    },
    {
      'û',
      (byte) 251
    },
    {
      'ü',
      (byte) 252
    },
    {
      'ý',
      (byte) 253
    },
    {
      'þ',
      (byte) 254
    },
    {
      'ÿ',
      byte.MaxValue
    }
  };

  public override string WebName => "Windows-1252";

  public char? FallbackCharacter
  {
    get => this.fallbackCharacter;
    set
    {
      this.fallbackCharacter = value;
      if (value.HasValue && !Windows1252Encoding.charToByte.ContainsKey(value.Value))
        throw new EncoderFallbackException($"Cannot use the character [{value.Value}] (int value {(int) value.Value}) as fallback value - the fallback character itself is not supported by the encoding.");
      this.FallbackByte = value.HasValue ? new byte?(Windows1252Encoding.charToByte[value.Value]) : new byte?();
    }
  }

  public byte? FallbackByte { get; private set; }

  public Windows1252Encoding() => this.FallbackCharacter = new char?('?');

  public override int GetBytes(
    char[] chars,
    int charIndex,
    int charCount,
    byte[] bytes,
    int byteIndex)
  {
    return this.FallbackByte.HasValue ? this.GetBytesWithFallBack(chars, charIndex, charCount, bytes, byteIndex) : this.GetBytesWithoutFallback(chars, charIndex, charCount, bytes, byteIndex);
  }

  private int GetBytesWithFallBack(
    char[] chars,
    int charIndex,
    int charCount,
    byte[] bytes,
    int byteIndex)
  {
    for (int index = 0; index < charCount; ++index)
    {
      char key = chars[index + charIndex];
      byte num;
      bool flag = Windows1252Encoding.charToByte.TryGetValue(key, out num);
      bytes[byteIndex + index] = flag ? num : this.FallbackByte.Value;
    }
    return charCount;
  }

  private int GetBytesWithoutFallback(
    char[] chars,
    int charIndex,
    int charCount,
    byte[] bytes,
    int byteIndex)
  {
    for (int index = 0; index < charCount; ++index)
    {
      char key = chars[index + charIndex];
      byte num;
      if (!Windows1252Encoding.charToByte.TryGetValue(key, out num))
        throw new EncoderFallbackException($"The encoding [{this.WebName}] cannot encode the character [{key}] (int value {(int) key}). Set the FallbackCharacter property in order to suppress this exception and encode a default character instead.");
      bytes[byteIndex + index] = num;
    }
    return charCount;
  }

  public override int GetChars(
    byte[] bytes,
    int byteIndex,
    int byteCount,
    char[] chars,
    int charIndex)
  {
    return this.FallbackCharacter.HasValue ? this.GetCharsWithFallback(bytes, byteIndex, byteCount, chars, charIndex) : this.GetCharsWithoutFallback(bytes, byteIndex, byteCount, chars, charIndex);
  }

  private int GetCharsWithFallback(
    byte[] bytes,
    int byteIndex,
    int byteCount,
    char[] chars,
    int charIndex)
  {
    for (int index1 = 0; index1 < byteCount; ++index1)
    {
      byte index2 = bytes[index1 + byteIndex];
      char ch = (int) index2 >= Windows1252Encoding.byteToChar.Length ? this.FallbackCharacter.Value : Windows1252Encoding.byteToChar[(int) index2];
      chars[charIndex + index1] = ch;
    }
    return byteCount;
  }

  private int GetCharsWithoutFallback(
    byte[] bytes,
    int byteIndex,
    int byteCount,
    char[] chars,
    int charIndex)
  {
    for (int index1 = 0; index1 < byteCount; ++index1)
    {
      byte index2 = bytes[index1 + byteIndex];
      if ((int) index2 >= Windows1252Encoding.byteToChar.Length)
        throw new EncoderFallbackException($"The encoding [{this.WebName}] cannot decode byte value [{index2}]. Set the FallbackCharacter property in order to suppress this exception and decode the value as a default character instead.");
      chars[charIndex + index1] = Windows1252Encoding.byteToChar[(int) index2];
    }
    return byteCount;
  }

  public override int GetByteCount(char[] chars, int index, int count) => count;

  public override int GetCharCount(byte[] bytes, int index, int count) => count;

  public override int GetMaxByteCount(int charCount) => charCount;

  public override int GetMaxCharCount(int byteCount) => byteCount;

  public static int CharacterCount => Windows1252Encoding.byteToChar.Length;
}
