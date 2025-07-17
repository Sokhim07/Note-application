using System.Text;

namespace Application_noteapi.Utils.Security;

public static class Security
{
    public static string EncryptPassword(string password)
    {
        if (password == "" || password == null) return "";
        char[] char1 = password.ToArray();
        dynamic col = char1.Length - 1;
        password = "";
        for (int i = char1.Length - 1; i >= 0; i += -1)
        {
            password += char1[i];
        }
        password = StringToBinary(password);
        char1 = password.ToArray();
        for (int i = 0; i <= char1.Length - 1; i++)
        {
            if (char1[i] == '0')
            {
                char1[i] = '1';
            }
            else
            {
                char1[i] = '0';
            }
        }
        password = new string(char1);
        password = StringToBinary(password);
        char1 = password.ToArray();
        for (int i = 0; i <= char1.Length - 1; i++)
        {
            if (char1[i] == '0')
            {
                char1[i] = '%';
            }
            else
            {
                char1[i] = '$';
            }
        }
        password = new string(char1);
        return password;
    }

    public static string DecryptPassword(string password)
    {
        char[] char1 = password.ToArray();
        password = "";
        for (int i = 0; i <= char1.Length - 1; i++)
        {
            if (char1[i] == '%')
            {
                char1[i] = '0';
            }
            else
            {
                char1[i] = '1';
            }
            password += char1[i];
        }
        string[] char2 = new string[password.Length / 8 /*+ 1*/];
        for (int i = 0; i <= char2.Length - 1; i++)
        {
            char2[i] = "";
        }
        for (int i = 0; i <= char2.Length - 1; i++)
        {
            char2[i] = "";
        }
        for (int i = 0; i <= char1.Length - 1; i++)
        {
            char2[i / 8] += char1[i];
        }
        for (int i = 0; i <= char2.Length - 1; i++)
        {
            char2[i] = BinaryToString(char2[i]);
        }
        password = "";
        for (int i = 0; i <= char2.Length - 1; i++)
        {
            password += char2[i];
            if (char2[i] == "0")
            {
                char2[i] = "1";
            }
            else
            {
                char2[i] = "0";
            }
        }
        string[] char3 = new string[char2.Length / 8 /*+ 1*/];
        for (int i = 0; i <= char3.Length - 1; i++)
        {
            char3[i] = "";
        }
        for (int i = 0; i <= char2.Length - 1; i++)
        {
            char3[i / 8] += char2[i];
        }
        for (int i = 0; i <= char3.Length - 1; i++)
        {
            char3[i] = BinaryToString(char3[i]);
        }
        string[] char4 = new string[char3.Length + 1];
        dynamic col = char3.Length - 1;
        for (int i = char3.Length - 1; i >= 0; i += -1)
        {
            char4[col - i] = char3[i];
        }
        password = "";
        for (int i = 0; i <= char4.Length - 1; i++)
        {
            password += char4[i];
        }
        return password;
    }

    public static string StringToBinary(string data, string Separator = "")
    {
        StringBuilder sb = new StringBuilder();

        foreach (char c in data.ToCharArray())
        {
            sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
        }
        return sb.ToString();
    }

    public static string BinaryToString(string data)
    {
        List<byte> byteList = new List<byte>();

        for (int i = 0; i < data.Length; i += 8)
        {
            byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
        }
        return Encoding.ASCII.GetString(byteList.ToArray());
    }
}
